/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 08/02/2023          *
 * Assembly : MiliWebService           *
 * *************************************/

using MiliSoftware.WebServices;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiliWebService.WebServices
{
    public class WebGetService
    {
        // variables privadas
        private HttpWebRequest webRequest;
        private readonly string uri;

        // variables publicas
        public WebServiceError Error { get; private set; }

        /// <summary>
        /// Metodo para obtener consumir un servicio web
        /// </summary>
        /// <param name="uri"></param>
        public WebGetService(string uri)
        {
            this.uri = uri;
            webRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
            webRequest.Method = "get";
        }

        /// <summary>
        /// Establece las credenciales de acceso para el servidor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
 

        /// <summary>
        /// Ace una peticion al servidor y este la devuelve en string
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            string result = "";
            Error = null;
            try
            {
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        result = reader.ReadToEnd().Trim();
                    }
                }
            }
            catch(WebException ex) {
                Error = new WebServiceError(ex.Message, "GET", uri, ex.ToString());
            }

            return result;
        }

        public async Task<string> GetStringAsync()
        {
            Error = null;
            try
            {
                using (WebResponse webResponse = await webRequest.GetResponseAsync())
                {
                    using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }
            catch (WebException ex)
            {
                Error = new WebServiceError(ex.Message, "GET", uri, ex.ToString());
            }

            return null;
        }

        public byte[] GetBuffer()
        {
            byte[] buffer = null;
            Error = null;
            try
            {
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (Stream stream = webResponse.GetResponseStream())
                    {
                        buffer = new byte[stream.Length];
                        stream.Read(buffer,0, buffer.Length);
                    }
                }
            }
            catch (WebException ex)
            {
                Error = new WebServiceError(ex.Message, "GET", uri, ex.ToString());
            }

            return buffer;
        }
    }
}
