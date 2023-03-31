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
    public class ProductComponentsContoller : IController<string, object[], string, string>
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

        public object[] Read(object[] data)
        {
            List<dynamic> products = new List<dynamic>();

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
            return products.ToArray();
        }

        public bool Update(string data)
        {
            throw new System.NotImplementedException();
        }
    }
}
