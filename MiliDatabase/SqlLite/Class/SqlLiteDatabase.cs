/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 30/10/2022          *
 * Assembly : MiliDatabase              *
 * *************************************/

using System;
using System.IO;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using MiliFileEngine.src.Resources;

namespace MiliSoftware.SqlLite
{
    /// <summary>
    /// Esta clase se encarga de conectarse
    /// a la base de datos local SqlLite
    /// </summary>
    public class SqlLiteDatabase
    {
        /// <summary>
        /// Instacia de la base de datos SqlLite
        /// </summary>
        private SQLiteConnection connection;
        /// <summary>
        /// Nombre de la base de datos
        /// </summary>
        private string databaseName;

        public SqlLiteDatabase()
        {
            CreateDB();
            InitSqlLiteServices();
        }

        /// <summary>
        /// Metodo para abrir la base de datos
        /// </summary>
        public void Open()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();   
            }
        }

        /// <summary>
        /// Metodo para cerrar la base de datos
        /// </summary>
        public void Close() {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Metodo para ejecutar una consulta sin retorno de datos
        /// </summary>
        /// <param name="query">
        /// Consulta SQL
        /// </param>
        public void ExecuteNomQuery(string query)
        {
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        /// <summary>
        /// Metodo para ejecutar una consulta sin retorno de datos
        /// </summary>
        /// <param name="query">
        /// Consulta SQL
        /// </param>
        public void ExecuteNomQuery(string query, Dictionary<string, object> @params)
        {
            SQLiteCommand command = new SQLiteCommand(query, connection);
            foreach (KeyValuePair<string, object> param in @params)
            {
                SQLiteParameter parameter = new SQLiteParameter();
                parameter.ParameterName = param.Key;
                parameter.Value = param.Value;
                command.Parameters.Add(parameter);
            }
            command.ExecuteNonQuery();
            command.Dispose();
        }

        /// <summary>
        /// Metodo para ejecutar una consulta sin retorno de datos este metodo 
        /// retorna en numero de Row Insertado o Actualizado
        /// </summary>
        /// <param name="query">
        /// Consulta SQL
        /// </param>
        public int ExecuteNomQueryID(string query)
        {
            SQLiteCommand command = new SQLiteCommand(query, connection);
            int id = command.ExecuteNonQuery();
            command.Dispose();
            return id;
        }

        /// <summary>
        /// Metodo para ejecutar una consulta con retorno de datos
        /// </summary>
        /// <param name="query">
        /// Consulta SQL
        /// </param>
        /// <returns>
        /// valores pedidos en la consulta SQL
        /// </returns>
        public List<object[]> ExecuteQuery(string query)
        {
            SQLiteCommand command = new SQLiteCommand(query, connection);
            var reader = command.ExecuteReader();
            List<object[]> data = new List<object[]>();

            int fieldCount = reader.FieldCount;

            while (reader.Read())
            {
                object[] rows = new object[fieldCount];
                for(int i = 0;i< fieldCount; i++)
                {
                    rows[i] = reader.GetValue(i);
                }
                data.Add(rows);
            }

            command.Dispose();
            return data;
        }

        /// <summary>
        /// Metodo para ejecutar una consulta con retorno de datos
        /// </summary>
        /// <param name="query">
        /// Consulta SQL
        /// </param>
        /// <returns>
        /// valores pedidos en la consulta SQL
        /// </returns>
        public Dictionary<string,object>[] ExecuteQueryD(string query)
        {
            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                var reader = command.ExecuteReader();
                int fieldCount = reader.FieldCount;

                while (reader.Read())
                {
                    Dictionary<string, object> rows = new Dictionary<string, object>();

                    for (int i = 0; i < fieldCount; i++)
                    {
                        string name = reader.GetName(i);
                        object value = reader.GetValue(i);
                        rows.Add(name, value);
                    }

                    data.Add(rows);
                }
            }
            return data.ToArray();
        }

        /// <summary>
        /// Metodo que crea la base de datos
        /// </summary>
        private void CreateDB() {
            string hash = MiliResources.Utils.GetHashToName(GetDatabasePath());
            databaseName = MiliResources.Externs.GetFileName(hash, true);
            Console.WriteLine(databaseName);
            if (!MiliResources.Externs.ExistFile(hash))
            {
                MiliResources.Externs.CreateFile(hash).Close();
            }
        }

        /// <summary>
        /// Metodo que Inicia la instacia de la base de datos
        /// </summary>
        private void InitSqlLiteServices()
        {
            string fullname = Path.GetFullPath(databaseName);
            connection = new SQLiteConnection("Data Source=" + fullname);
        }

        /// <summary>
        /// Metodo que obtiene la ubicación de la base de datos
        /// </summary>
        /// <returns></returns>
        private static string GetDatabasePath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(),"database.db");
        }
    }
}
