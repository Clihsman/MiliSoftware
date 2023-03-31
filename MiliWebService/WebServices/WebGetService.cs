/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 08/02/2023          *
 * Assembly : MiliWebService           *
 * *************************************/

using MiliSoftware.Model.WebServices;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
        public void SetCredentials(string username, string password)
        {
            byte[] data = Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password);
            string encoded = Convert.ToBase64String(data);
            webRequest.Headers.Add("Authorization", "Basic " + encoded);
        }

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
