/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 30/03/2023          *
 * Assembly : MiliDatabase             *
 * *************************************/

using System;
using System.Collections.Generic;
using System.Reflection;

namespace MiliSoftware.SqlLite
{
    public class SqlLoader
    {
        /// <summary>
        /// Crea un esquema de una tabla a partir de un tipo
        /// </summary>
        /// <param name="type">Ingrese el tipo de la clase para crear el esquema</param>
        /// <returns></returns>
        public SqlSchema LoadSqlSchema(Type type) {
            // Obtiene la tabla
            SqlTable sqlTable = GetSqlTable(type);

            // Verifica que la tabla no sea nula en caso de que sea nula retorna null
            if (sqlTable is null) return null;

            // Obtiene los campos de la tabla
            SqlField[] sqlFields = GetSqlFields(type);

            // Verifica que los campos de la tabla sean mayor a cero, en caso contrario retorna nulo
            if (!(sqlFields.Length > 0)) return null;

            // Crea el esquema de la tabla
            return new SqlSchema(sqlTable, sqlFields);
        }

        /// <summary>
        /// Obtiene los campos de la tabla a partir de un tipo
        /// </summary>
        /// <param name="type">Ingrese el tipo de la clase para obtener los campos</param>
        /// <returns></returns>
        private SqlField[] GetSqlFields(Type type)
        {
            List<SqlField> fields = new List<SqlField>();

            foreach (PropertyInfo property in type.GetProperties())
            {
                SqlField sqlField = property.GetCustomAttribute<SqlField>(false);
                if (sqlField != null)
                {
                    string name = sqlField.Name != null ? sqlField.Name : property.Name;
                    fields.Add(new SqlField(sqlField.FieldType, name));
                }
            }

            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                SqlField sqlField = fieldInfo.GetCustomAttribute<SqlField>(false);
                if (sqlField != null)
                {
                    string name = sqlField.Name != null ? sqlField.Name : fieldInfo.Name;
                    fields.Add(new SqlField(sqlField.FieldType, name));
                }
            }

            return fields.ToArray();
        }

        /// <summary>
        /// Obtiene la informacion de la tabla a partir de un tipo
        /// </summary>
        /// <param name="type">Ingrese el tipo de la clase para obtener la informacion de la tabla</param>
        /// <returns></returns>
        private SqlTable GetSqlTable(Type type)
        {
            SqlTable sqlTable = type.GetCustomAttribute<SqlTable>(false);
            return sqlTable;
        }
    }
}
