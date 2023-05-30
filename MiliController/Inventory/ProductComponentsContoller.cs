/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 24/12/2022          *
 * Assembly : MiliController           *
 * *************************************/

using GrapInterCom.Interfaces.Inventory;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MiliSoftware.Inventory
{
    public class ProductComponentsContoller : IController<string, ProductComponent[], string, string>
    {
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
                       (string)value[nameof(ProductComponent._id)],
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

        public bool Update(string data)
        {
            throw new System.NotImplementedException();
        }
    }
}
