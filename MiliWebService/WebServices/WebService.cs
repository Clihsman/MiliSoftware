/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 29/10/2022          *
 * Assembly : MiliWebService           *
 * *************************************/

/* Salmos 1
 * Bienaventurado el varón que no anduvo en consejo de malos,
 * Ni estuvo en camino de pecadores,
 * Ni en silla de escarnecedores se ha sentado;
 * 2 Sino que en la Torah de Adonai está su delicia,
 * Y en su Torah medita de día y de noche.
 * 3 Será como árbol plantado junto a corrientes de aguas,
 * Que da su fruto en su tiempo,
 * Y su hoja no cae;
 * Y todo lo que hace, prosperará.
 * 4 No así los malos,
 * Que son como el tamo que arrebata el viento.
 * 5 Por tanto, no se levantarán los malos en el juicio,
 * Ni los pecadores en la congregación de los justos.
 * 6 Porque Adonai conoce el camino de los justos;
 * Mas la senda de los malos perecerá. 
*/

using MiliWebService.WebServices;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.WebServices
{
    public class WebService
    {
        private static WebClient M_WebClient;
        //private static readonly string M_ServerUri = "https://milidatabase.com/";
        private static readonly string M_ServerUri = "http://localhost:8000/";
       // private static readonly string M_ServerUri = "http://localhost:80/";
        public static void InitServices(string username, string password)
        {
            M_WebClient = new WebClient();
            SetCredentials(M_WebClient, username, password);
        }

        public static Response GetString(string route)
        {
            Uri uri = new Uri(M_ServerUri + route);

            string result = null;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            
            webRequest.Method = "get";

            try
            {
                using (WebResponse webResponse = webRequest.GetResponse())
                using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    result = reader.ReadToEnd().Trim();
                }
            }
            catch {
                return new Response(result, 404);
            }

            

            return new Response(result, 200);
        }

        public static async Task<Response> GetStringAsync(string route)
        {
            /*
            Uri uri = new Uri(M_ServerUri + route);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.KeepAlive = false; // Por ejemplo, ajusta según tus necesidades
            webRequest.Timeout = 5000; // 5 segundos de tiempo de espera, ajusta según tus necesidades
            webRequest.ServicePoint.ConnectionLimit = 10; // Número máximo de conexiones simultáneas permitidas
            webRequest.ServicePoint.Expect100Continue = false; // Deshabilitar el encabezado Expect: 100-continue
            webRequest.Method = "get";

            try
            {
                using (WebResponse webResponse = await webRequest.GetResponseAsync())
                using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    return new Response(await reader.ReadToEndAsync(), 200);
                }
            }
            catch {
                return new Response(null, 404);
            }
            */
            /*
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", AppStaticStore.GetToken()));
                HttpResponseMessage response = await client.GetAsync("http://localhost:8000/api/inventario/grupos");
                // Procesar la respuesta...
                return new Response(await response.Content.ReadAsStringAsync(), (int)response.StatusCode);
            }
            */
            /*
            using (InventoryGruposApi rest = new InventoryGruposApi()) {
                return  new Response(await rest.All(), 200);
            }
            */
            return null;
            
        }

        

        public static MemoryStream GetData(string route)
        {
            Uri uri = new Uri(route);
            MemoryStream stream = new MemoryStream(M_WebClient.DownloadData(uri));
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public static string PostData(Stream data,string filename,string route)
        {
            Uri uri = new Uri(M_ServerUri + route);
            
            string result = "";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Headers.Add("FileName", filename);
            webRequest.Method = "post";
           // webRequest.ContentType = "multipart/form-data";
            
            using (Stream stream = webRequest.GetRequestStream())
            {

                data.CopyTo(stream);
                stream.Flush();
            }

            WebResponse webResponse = webRequest.GetResponse();

            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                result = reader.ReadToEnd().Trim();

            }
            //      string result = M_WebClient.UploadString(uri,"post", "");
            return result;
        }

        public static bool IsConnectedToInternet()
        {
            dynamic networkListManager = Activator.CreateInstance(
            Type.GetTypeFromCLSID(new Guid("{DCB00C01-570F-4A9B-8D69-199FDBA5723B}")));

            bool isConnected = networkListManager.IsConnectedToInternet;
            return isConnected;
        }

        public static string PostString(string route,string data)
        {
            Uri uri = new Uri(M_ServerUri + route);

            string result = "";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.KeepAlive = false; // Por ejemplo, ajusta según tus necesidades
            webRequest.Timeout = 5000; // 5 segundos de tiempo de espera, ajusta según tus necesidades
            webRequest.ServicePoint.ConnectionLimit = 10; // Número máximo de conexiones simultáneas permitidas
            webRequest.ServicePoint.Expect100Continue = false; // Deshabilitar el encabezado Expect: 100-continue
            webRequest.Method = "post";
            webRequest.ContentType = "application/json;charset=UTF-8";

            using (StreamWriter writer = new StreamWriter(webRequest.GetRequestStream()))
            {
                writer.Write(data);
                writer.Flush();
            }

            WebResponse webResponse = webRequest.GetResponse();

            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                result = reader.ReadToEnd().Trim();

            }
                //      string result = M_WebClient.UploadString(uri,"post", "");
                return result;
        }

        public static string DeleteString(string route, string data)
        {
            Uri uri = new Uri(M_ServerUri + route);

            string result = "";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = "DELETE";
            webRequest.ContentType = "application/json;charset=UTF-8";

            using (StreamWriter writer = new StreamWriter(webRequest.GetRequestStream()))
            {
                writer.Write(data);
                writer.Flush();
            }

            WebResponse webResponse = webRequest.GetResponse();

            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                result = reader.ReadToEnd().Trim();

            }
            return result;
        }

        public static string PutString(string route, string data)
        {
            Uri uri = new Uri(M_ServerUri + route);

            string result = "";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = "PUT";
            webRequest.ContentType = "application/json;charset=UTF-8";

            using (StreamWriter writer = new StreamWriter(webRequest.GetRequestStream()))
            {
                writer.Write(data);
                writer.Flush();
            }

            WebResponse webResponse = webRequest.GetResponse();

            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                result = reader.ReadToEnd().Trim();

            }
            return result;
        }

        public static string GetString()
        {
            Uri uri = new Uri(M_ServerUri);
            string result = M_WebClient.DownloadString(uri);
            return result;
        }

        public static void AddQuery(string name, string value)
        {
            M_WebClient.QueryString.Add(name, value);
        }

        public static void CleanQueries()
        {
            M_WebClient.QueryString.Clear();
        }

        private static void SetCredentials(WebClient webClient, string username, string password)
        {
            byte[] data = Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password);
            string encoded = Convert.ToBase64String(data);
            webClient.Headers.Add("Authorization", "Basic " + encoded);
        }
    }
}