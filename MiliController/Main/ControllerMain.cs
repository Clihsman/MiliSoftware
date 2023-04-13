using MiliSoftware.SqlLite;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;

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

                    /*
                   if (sqlSchema != null && sqlSchema.SqlTable.TableName == "AccountingSeat")
                   {


                       foreach (var item in sqlSchema.FindOne<AccountingSeat>(database, "3a0a4852650c000000000000").AccountingPositions)
                       {
                           Console.WriteLine(item.Description);
                       }


                       AccountingPosition accountingPosition = new AccountingPosition();
                       accountingPosition.Description = "Venta del señor luis";
                       sqlSchema.Save(database, "DC001", DateTime.Now, "DCAD444", "15421", 30, 30499354, 0, 0, 0, new object[] { accountingPosition, accountingPosition, accountingPosition });

                   }

                   if (sqlSchema.SqlTable.TableName == "Products")
                   {
                     //  Product producto = new Product();

                   //    sqlSchema.Save(database, sqlSchema.GetDataArray(producto));

                       foreach (var item in sqlSchema.FindOne<AccountingSeat>(database, "3a0a4852650c000000000000").AccountingPositions)
                       {
                           Console.WriteLine(item.Description);
                       }

                       AccountingPosition accountingPosition = new AccountingPosition();
                       accountingPosition.Description = "Venta del señor luis";
                       sqlSchema.Save(database, "DC001", DateTime.Now, "DCAD444", "15421", 30, 30499354, 0, 0, 0, new object[] { accountingPosition, accountingPosition, accountingPosition });

                   }
                   */

                    if (sqlSchema.SqlTable.TableName == "Products")
                    {
                        Product product = sqlSchema.FindOne<Product>(database, "3a0a5c1d2899000000030000");
                        ParseJson(product.ToJson());
                     

                    }
                }
            }

            database.Close();

            Montar();

            

          //  StringBuilder volumeMap = new StringBuilder(1024);
        //    QueryDosDevice("S:", volumeMap, 1024);

          //  Console.WriteLine(volumeMap.ToString());

            Console.ReadKey();
            Desmontar();
        }
        
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern long DefineDosDevice(long dwFlags, string lpDeviceName, string lpTargetPath);

        [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
        internal static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, uint ucchMax);

        private const long DDD_REMOVE_DEFINITION = 0x2;

        private static void Montar()
        {
            string letra;
            string ruta;
            letra = "S:";
            ruta = @"C:\AppServ";
            // Montar
            DefineDosDevice(0, letra, ruta);
        }

        private static void Desmontar()
        {
            string letra;
            string ruta;
            letra = "S:";
            ruta = @"C:\AppServ";
            // Montar
            DefineDosDevice(DDD_REMOVE_DEFINITION, letra, ruta);
        }

        private static void ParseJson(string json)
        {
          //  json = JsonPrettify(json);
            StringReader reader = new StringReader(json);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                ParseLine(line);
                Console.WriteLine();
            }
        }

        private static void ParseLine(string line)
        {
            bool alternarColor = true;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '{' || c == '}' || c == ':' || c == ' ')
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(c.ToString());
                }

                if (c == '[' || c == ']')
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(c.ToString());
                }

                if (c == ',')
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(c.ToString());
                    alternarColor = false;
                }

                if (c == '"')
                {
                    if (alternarColor)
                    {
                         Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(c.ToString());
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(c.ToString());
                    }

                    bool b = false;
                    while (true)
                    {
                        i++;
                        c = line[i];
                        if (c == '\\')
                            b = true;

                        if (c == '"')
                        {
                            if (!b)
                                break;
                            else
                                b = false;
                        }

                        if (alternarColor)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(c.ToString());
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(c.ToString());
                        }
                    }

                    if (alternarColor)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(c.ToString());
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(c.ToString());
                    }
                    alternarColor = !alternarColor;
                }

                if (char.IsNumber(c))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(c.ToString());
                }

                if (char.IsLetter(c))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(c.ToString());
                }
            }         
        }

        public static string JsonPrettify(string json)
        {
            using (var stringReader = new StringReader(json))
            using (var stringWriter = new StringWriter())
            {
                var jsonReader = new JsonTextReader(stringReader);
                var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                jsonWriter.WriteToken(jsonReader);
                return stringWriter.ToString();
            }
        }
    }
}
