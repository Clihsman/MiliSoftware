using GrapInterCom.Interfaces.Inventory;
using MiliSoftware.SqlLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.Inventory
{
    public class InventoryStoreController : IController<InventoryStore, InventoryStore[], InventoryStore, InventoryStore>
    {
        public InventoryStoreController(IInventoryStoreGUI inventoryGroup)
        {
            inventoryGroup.controller = this;
            inventoryGroup.Show();
        }

        public bool Create(InventoryStore data)
        {
            try
            {
                SqlLiteDatabase database = new SqlLiteDatabase();
                database.Open();
                SqlLoader loader = new SqlLoader();
                SqlSchema schema = loader.LoadSqlSchema(typeof(InventoryStore));
                schema.Save(database, schema.GetDataArray(data));
                database.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Delete(InventoryStore data)
        {
            try
            {
                SqlLiteDatabase database = new SqlLiteDatabase();
                database.Open();
                SqlLoader loader = new SqlLoader();
                SqlSchema schema = loader.LoadSqlSchema(typeof(InventoryStore));
                schema.Delete(database, schema.GetDataArray(data));
                database.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public InventoryStore[] Read(InventoryStore[] a)
        {
            SqlLiteDatabase database = new SqlLiteDatabase();
            database.Open();
            SqlLoader loader = new SqlLoader();
            SqlSchema schema = loader.LoadSqlSchema(typeof(InventoryStore));
            InventoryStore[] data = schema.Find<InventoryStore>(database);
            database.Close();
            return data;
        }

        public bool Update(InventoryStore data)
        {
            try
            {
                SqlLiteDatabase database = new SqlLiteDatabase();
                database.Open();
                SqlLoader loader = new SqlLoader();
                SqlSchema schema = loader.LoadSqlSchema(typeof(InventoryStore));
                schema.Edit(database, schema.GetDataArray(data));
                Console.WriteLine(data.ToJson());
                database.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }
    }
}
