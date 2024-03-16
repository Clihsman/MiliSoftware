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
using MiliSoftware.Model.WebServices;
using MiliSoftware.SqlLite;
using MiliSoftware.WebServices;
using MiliWebService.WebServices;
using OpenRestClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;
using MiliSoftware;

namespace MiliSoftware.Inventory
{
    public class ProductController: IController<Product, string, string, string>
    {
        private InventarioGrupoApi inventarioGrupoApi = new InventarioGrupoApi();
        private InventarioProductoApi inventarioProductoApi = new InventarioProductoApi();
        #region Constucts

        public ProductController(IProductGUI<Product,string,string,string> productGUI)
        {
            productGUI.controller = this;
            productGUI.Show();
        }

        #endregion

        #region CRUD

        /// <summary>
        /// Metodo Para Crear un Producto
        /// </summary>
        /// <param name="data">Datos del producto a crear</param>
        public async Task<string> CreateA(Product product)
        {
            /*   
               SqlLite.SqlLiteDatabase database = new SqlLite.SqlLiteDatabase();
               database.Open();
               SqlLite.SqlSchema sqlSchema = (new SqlLite.SqlLoader()).LoadSqlSchema(typeof(Product));
               if (sqlSchema.SqlTable.TableName == "Products")
               {


                   sqlSchema.Save(database, sqlSchema.GetDataArray(product));

                   foreach (var item in sqlSchema.FindOne<AccountingSeat>(database, "3a0a4852650c000000000000").AccountingPositions)
                   {
                       Console.WriteLine(item.Description);
                   }

                   AccountingPosition accountingPosition = new AccountingPosition();
                   accountingPosition.Description = "Venta del señor luis";
                   sqlSchema.Save(database, "DC001", DateTime.Now, "DCAD444", "15421", 30, 30499354, 0, 0, 0, new object[] { accountingPosition, accountingPosition, accountingPosition });

               }
               database.Close();
            */
            /*
             WebPostService webPostService = new WebPostService("http://localhost/apirest/products");
             Console.WriteLine(webPostService.PostJson(product.ToJson()));      

            if (product.SaveImage)
            {
                webPostService = new WebPostService("http://localhost/apirest/files");
                string name = Path.GetFileName(product.Picture);
                MiliTools.IO.WebFile webFile = new MiliTools.IO.WebFile(product.Picture);
                Stream stream = webFile.GetStream();
                webPostService.PostStream(stream, name, WebFileFormat.PNG);
            }

            // Console.WriteLine(webPostService.PostStream(image, hash + ".png", WebFileFormat.PNG));
            //  throw new System.NotImplementedException();
            */

            Console.WriteLine(product.ToJson());

          

            /*
                        using (InventoryApi api = new InventoryApi("http://localhost:8000")) {
                            _ = await api.Store(product.ToJson());
                        }
            */
            //  WebService.PostString("api/inventario/productos", product.ToJson());

            await inventarioProductoApi.GuardarProducto(product.ToProducto());



            if (inventarioProductoApi.StatusCode == System.Net.HttpStatusCode.NotFound) return "not_found";
            if (inventarioProductoApi.StatusCode == System.Net.HttpStatusCode.BadRequest) return "bad_request";
            if (inventarioProductoApi.StatusCode == System.Net.HttpStatusCode.InternalServerError) return "internal_server_error";
            if (inventarioProductoApi.StatusCode == System.Net.HttpStatusCode.Created) return "created";
            if (inventarioProductoApi.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) return "service_unavailable";
            if (inventarioProductoApi.StatusCode == System.Net.HttpStatusCode.Conflict) return "conflict";

            return null;
        }

        /// <summary>
        /// Metodo para Leer los datos de un Producto
        /// </summary>
        /// <param name="data">Datos del Producto a Leer</param>
        public string Read(string data)
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
        /// Metodo para actualizar un Producto
        /// </summary>
        /// <param name="data"></param>
        public bool Update(string data)
        {
            //      WebService.InitServices("abc", "123");
            //     string response = WebService.PutString("apirest/clientes/agregar", data);
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Metodo para eliminar un Producto
        /// </summary>
        /// <param name="data"></param>
        public bool Delete(string data)
        {
            //  WebService.InitServices("abc", "123");
            //string response = WebService.DeleteString("apirest/clientes/agregar", data);
            throw new System.NotImplementedException();
        }

        public async Task<InventoryGroup[]> GetProductsGroups()
        {
            RestResponse<InventarioGrupo[], AccesoError> repuesta = await inventarioGrupoApi.All();
            if (repuesta)
                return repuesta.Value0.Select(o => new InventoryGroup(o.Codigo, o.Nombre)).ToArray();
            return new InventoryGroup[0];
        }

        public string[] GetProductsStores() {
            SqlLiteDatabase database = new SqlLiteDatabase();
            database.Open();
            SqlLoader loader = new SqlLoader();
            SqlSchema schema = loader.LoadSqlSchema(typeof(InventoryStore));
            InventoryStore[] data = schema.Find<InventoryStore>(database);
            database.Close();

            List<string> names = new List<string>();
            foreach (var invStore in data) names.Add(invStore.Name);
            names.Add("Nueva Bodega");
            return names.ToArray();
        }

        public bool Create(Product data)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
