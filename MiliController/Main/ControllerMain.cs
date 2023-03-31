using MiliSoftware.SqlLite;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace MiliSoftware
{
    public class ControllerMain
    {
        public static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ControllerMain));
            Type[]  types = assembly.GetTypes();

            MiliSoftware.SqlLite.SqlLiteDatabase database = new MiliSoftware.SqlLite.SqlLiteDatabase();
            database.Open();

            SqlLoader loader = new SqlLoader();

            foreach (Type type in types)
            {
                SqlSchema sqlSchema = loader.LoadSqlSchema(type);
                if (sqlSchema != null)
                {
                    Console.WriteLine("\n" + sqlSchema.GetCreateTable());
                    database.ExecuteNomQuery(sqlSchema.GetCreateTable());

                    sqlSchema.Save(database, 150, "Pepito");
                }
            }

            database.Close();
        }
    }
}
