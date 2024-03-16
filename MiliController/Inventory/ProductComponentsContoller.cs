/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 24/12/2022          *
 * Assembly : MiliController           *
 * *************************************/

using GrapInterCom.Interfaces.Inventory;
using MiliSoftware.Inventario.Modelos;
using MiliSoftware.Inventario;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MiliSoftware.Inventory
{
    public class ProductComponentsContoller : IController<string, ProductComponent[], string, string>
    {
        private InventarioProductoApi InventarioProductoApi { get; set; } = new InventarioProductoApi();

        public ProductComponentsContoller(ProductComponentsGUI productComponentsGUI)
        {
            productComponentsGUI.controller = this;
            productComponentsGUI.Show();
        }

        public bool Create(string data)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(string data)
        {
            throw new System.NotImplementedException();
        }

        public ProductComponent[] Read(ProductComponent[] data)
        {
            List<ProductComponent> products = new List<ProductComponent>();

            SqlLite.SqlLiteDatabase sqlLiteDatabase = new SqlLite.SqlLiteDatabase();
            sqlLiteDatabase.Open();

            foreach (Dictionary<string, object> value in sqlLiteDatabase.ExecuteQueryD("SELECT _id,Code,Name FROM Products"))
            {
                products.Add(new ProductComponent(
                       (int)value[nameof(ProductComponent.id)],
                       (string)value[nameof(ProductComponent.Code)],
                       (string)value[nameof(EquivalentProduct.Name)], 0
                    ));
            }

            sqlLiteDatabase.Close();

            /*
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
            return products.ToArray();
        }

        public async Task<ProductComponent[]> ObtenerListaProductos()
        {
            Producto[] productos = await InventarioProductoApi.ObtenerTodosLosProductos();
            ProductComponent[] productosComponentes = productos.Select(producto => new ProductComponent(producto.Id, producto.Codigo, producto.Nombre, 0)).ToArray();
            return productosComponentes;
        }

        public bool Update(string data)
        {
            throw new System.NotImplementedException();
        }
    }
}
