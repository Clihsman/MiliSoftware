/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 17/12/2022          *
 * Assembly : MiliController           *
 * *************************************/

using GrapInterCom.Interfaces.Inventory;
using MiliSoftware.Errores;
using MiliSoftware.Inventario.Modelos;
using MiliSoftware.Inventario;
using OpenRestClient;
using System.Threading.Tasks;
using System.Linq;

namespace MiliSoftware.Inventory
{
    public class InventoryController : IController<string, object[], string, string>
    {
        private InventarioProductoApi inventarioProductoApi = new InventarioProductoApi();

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

        public async Task<Product[]> TodosLosProductos()
        {
            
            RestResponse<Producto[], AccesoError> resultado = await inventarioProductoApi.ObtenerTodosLosProductos();
            if (resultado)
            {
                return resultado.Value0.Select(producto => new Product() {
                    Name = producto.Nombre,
                    Code = producto.Codigo,
                    Type = producto.Tipo,
                    InventoryGroup = producto.GrupoInventario,
                    ProductComponents = producto.ProductoComponentes?.
                        Select(componente => new ProductComponent(componente.Id,componente.Codigo,componente.Nombre, componente.Cantidad)).ToArray(),
                    Store = producto.Bodega
                }).ToArray();
            }
            
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
