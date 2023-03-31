/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 12/12/2022          *
 * Assembly : MiliController           *
 * *************************************/

using MiliSoftware.SqlLite;
using MiliSoftware.Model.WebServices;
using MiliSoftware.UI;
using MiliWebService.WebServices;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MiliSoftware.Customers
{
    public sealed class ClientController : IController<string, object[], string, string>
    {
        #region Constucts

        public ClientController(IClientGUI clientGUI)
        {
            clientGUI.controller = this;
            clientGUI.Show();  
        }

        #endregion

        #region CRUD

        /// <summary>
        /// Metodo Para Crear un Cliente
        /// </summary>
        /// <param name="data">Datos del cliente a crear</param>
        public bool Create(string data)
        {

            SqlLiteDatabase database = new SqlLiteDatabase();
            database.Open();
            database.ExecuteNomQuery("INSERT INTO Clients(Code,Names) VALUES (\"FCD1451\",\"Clihsman Iscala\")");
            database.Close();

           // WebService.InitServices("abc", "123");
            //string response = WebService.PostString("apirest/clientes/agregar", data);
            return true;
        }

        /// <summary>
        /// Metodo para Leer datos de un Cliente
        /// </summary>
        /// <param name="data">Datos del Cliente a Leer</param>
        public object[] Read(object[] data)
        {
            /*
            WebGetService webService = new WebGetService("http://localhost/apirest/clientes");
            webService.SetCredentials("local", "14503034");
            string json = webService.GetString();
            if (!webService.Error)
            {           
                WebService.InitServices("", "");
                string json = WebService.GetString("apirest/clientes");
                

                Dictionary<string, object>[] customers = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(json);
                return DObject.GetArray(customers);
            }
            else
            {
                System.Console.WriteLine(webService.Error.ToJson());
            }
            */

            
            
            SqlLiteDatabase sqlLiteDatabase = new SqlLiteDatabase();
            sqlLiteDatabase.Open();
            
            Dictionary<string, object>[] customers = sqlLiteDatabase.ExecuteQueryD("SELECT * FROM clientes");
            sqlLiteDatabase.Close();
            
          //  return null;

            return DObject.GetArray(customers);
        }

        /// <summary>
        /// MetodopPara Actualizar un cliente
        /// </summary>
        /// <param name="data"></param>
        public bool Update(string data)
        {
            WebService.InitServices("abc", "123");
            string response = WebService.PutString("apirest/clientes/agregar", data);
            return true;
        }

        /// <summary>
        /// Metodo para Eliminar un cliente
        /// </summary>
        /// <param name="data"></param>
        public bool Delete(string data)
        {
            WebService.InitServices("abc", "123");
            string response = WebService.DeleteString("apirest/clientes/agregar", data);
            return true;
        }

        #endregion
    }
}
