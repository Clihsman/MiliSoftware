/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 26/12/2022          *
 * Assembly : MiliController           *
 * *************************************/


using GrapInterCom.Interfaces.Inventory;
using MiliSoftware.Inventario;
using MiliSoftware.Inventario.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiliSoftware.Inventory
{
    public class EquivalentProductsController : IController<string, EquivalentProduct[], string, string>
    {
        private InventarioProductoApi InventarioProductoApi { get; set; } = new InventarioProductoApi();

        public EquivalentProductsController(EquivalentProductsGUI equivalentProductsGUI)
        {
            equivalentProductsGUI.controller = this;
            equivalentProductsGUI.Show();
        }

        public bool Create(string data)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(string data)
        {
            throw new System.NotImplementedException();
        }

        public EquivalentProduct[] Read(EquivalentProduct[] data)
        {
            /*
            List<EquivalentProduct> products = new List<EquivalentProduct>();

            SqlLite.SqlLiteDatabase sqlLiteDatabase = new SqlLite.SqlLiteDatabase();
            sqlLiteDatabase.Open();

            foreach (Dictionary<string, object> value in sqlLiteDatabase.ExecuteQueryD("SELECT _id, Code, Name FROM Products"))
            {
                products.Add(new EquivalentProduct(
                    (string)value[nameof(ProductComponent._id)],
                    (string)value[nameof(ProductComponent.Code)],
                    (string)value[nameof(ProductComponent.Name)]));
            }
            */
         //   sqlLiteDatabase.Close();
            /*
            sqlLiteDatabase.Close();
             SqlConnection connection = new SqlConnection("Data Source=DESKTOP-V156BBR\\TECHNOTEL;Initial Catalog=monica_9;Persist Security Info=True;User ID=sa;Password=Admin2012");
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].productos", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string codigo = reader.GetValue(0).ToString();
                    string nombre = reader.GetValue(2).ToString();

                    DObject dObject = new DObject(new Dictionary<string, object>() {
                        {"Codigo", codigo},
                        {"Nombre", nombre}
                    });

                    products.Add(dObject);
                }

                connection.Close();

            }
            catch
            {
                // Console.WriteLine("Error");
            }
            */
            return null;
        }

        public async Task<EquivalentProduct[]> ObtenerListaProductos() {
            Producto[] productos = await InventarioProductoApi.ObtenerTodosLosProductos();
            EquivalentProduct[] productosEquivalentes = productos.Select(producto => new EquivalentProduct(producto.Id, producto.Codigo, producto.Nombre)).ToArray();
            return productosEquivalentes;
        }

        public bool Update(string data)
        {
            throw new System.NotImplementedException();
        }
    }
}
