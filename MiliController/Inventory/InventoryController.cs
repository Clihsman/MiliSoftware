/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 17/12/2022          *
 * Assembly : MiliController           *
 * *************************************/

using GrapInterCom.Interfaces.Inventory;

namespace MiliSoftware.Inventory
{
    public class InventoryController : IController<string, object[], string, string>
    {
        #region Constucts

        public InventoryController(IInventoryGUI inventoryGUI)
        {
            inventoryGUI.controller = this;
            inventoryGUI.Show();
        }

        #endregion

        #region CRUD

        /// <summary>
        /// Metodo Para Crear un Cliente
        /// </summary>
        /// <param name="data">Datos del cliente a crear</param>
        public bool Create(string data)
        {
            // WebService.InitServices("abc", "123");
            // string response = WebService.PostString("apirest/clientes/agregar", data);
            return false;
        }

        /// <summary>
        /// Metodo para Leer datos de un Cliente
        /// </summary>
        /// <param name="data">Datos del Cliente a Leer</param>
        public object[] Read(object[] data)
        {
            /*
            WebService.InitServices("", "");
            string json = WebService.GetString("apirest/clientes");
            Dictionary<string, object>[] customers = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(json);
            */

            //            return DObject.GetArray(clientes);
            return null;
        }

        /// <summary>
        /// MetodopPara Actualizar un cliente
        /// </summary>
        /// <param name="data"></param>
        public bool Update(string data)
        {
            //      WebService.InitServices("abc", "123");
            //     string response = WebService.PutString("apirest/clientes/agregar", data);
            return false;
        }

        /// <summary>
        /// Metodo para Eliminar un cliente
        /// </summary>
        /// <param name="data"></param>
        public bool Delete(string data)
        {
            //  WebService.InitServices("abc", "123");
            //string response = WebService.DeleteString("apirest/clientes/agregar", data);
            return false;
        }

        #endregion
    }
}
