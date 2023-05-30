/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 22/05/2023          *
 * Assembly : MiliController           *
 * *************************************/

using GrapInterCom.Interfaces.Inventory;
using MiliSoftware.SqlLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.Inventory
{
    public class InventoryGroupController : IController<InventoryGroup, InventoryGroup[], InventoryGroup, InventoryGroup>
    {
        public InventoryGroupController(IInventoryGroupGUI inventoryGroup) {
            inventoryGroup.controller = this;
            inventoryGroup.Show();
        }

        public bool Create(InventoryGroup data)
        {
            try
            {
                SqlLiteDatabase database = new SqlLiteDatabase();
                database.Open();
                SqlLoader loader = new SqlLoader();
                SqlSchema schema = loader.LoadSqlSchema(typeof(InventoryGroup));
                schema.Save(database, schema.GetDataArray(data));
                database.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Delete(InventoryGroup data)
        {
            try
            {
                SqlLiteDatabase database = new SqlLiteDatabase();
                database.Open();
                SqlLoader loader = new SqlLoader();
                SqlSchema schema = loader.LoadSqlSchema(typeof(InventoryGroup));
                schema.Delete(database, schema.GetDataArray(data));
                database.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public InventoryGroup[] Read(InventoryGroup[] a)
        {
            SqlLiteDatabase database = new SqlLiteDatabase();
            database.Open();
            SqlLoader loader = new SqlLoader();
            SqlSchema schema = loader.LoadSqlSchema(typeof(InventoryGroup));
            InventoryGroup[] data = schema.Find<InventoryGroup>(database);
            database.Close();
            return data;
        }

        public bool Update(InventoryGroup data)
        {
            try
            {
                SqlLiteDatabase database = new SqlLiteDatabase();
                database.Open();
                SqlLoader loader = new SqlLoader();
                SqlSchema schema = loader.LoadSqlSchema(typeof(InventoryGroup));
                schema.Edit(database, schema.GetDataArray(data));
                Console.WriteLine(data.ToJson());
                database.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }
    }
}
