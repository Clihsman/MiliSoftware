/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 30/03/2023          *
 * Assembly : MiliDatabase             *
 * *************************************/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
namespace MiliSoftware.SqlLite
{
    public class SqlLoader : IDisposable
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
            Dictionary<SqlField, object> fields = GetSqlFields(type);
            SqlField[] sqlFields = fields.Keys.ToArray();

            // Verifica que los campos de la tabla sean mayor a cero, en caso contrario retorna nulo
            if (!(sqlFields.Length > 0)) return null;

            // Crea el esquema de la tabla
            return new SqlSchema(sqlTable, sqlFields, fields);
        }

        /// <summary>
        /// Obtiene los campos de la tabla a partir de un tipo
        /// </summary>
        /// <param name="type">Ingrese el tipo de la clase para obtener los campos</param>
        /// <returns></returns>
        private Dictionary<SqlField,object> GetSqlFields(Type type)
        {
            Dictionary<SqlField, object> fields = new Dictionary<SqlField, object>();

            foreach (PropertyInfo property in type.GetProperties())
            {
                SqlField sqlField = property.GetCustomAttribute<SqlField>(false);
                if (sqlField != null)
                {
                    string name = sqlField.Name != null ? sqlField.Name : property.Name;

                    if (sqlField is SqlTableRef)
                        fields.Add(new SqlTableRef(name, ((SqlTableRef)sqlField).SqlTableRefType), property);
                    else if(sqlField is SqlField)
                        fields.Add(new SqlField(sqlField.FieldType, name), property);
                }
            }

            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                SqlField sqlField = fieldInfo.GetCustomAttribute<SqlField>(false);
                if (sqlField != null)
                {
                    string name = sqlField.Name != null ? sqlField.Name : fieldInfo.Name;

                    if (sqlField is SqlTableRef)
                        fields.Add(new SqlTableRef(name, ((SqlTableRef)sqlField).SqlTableRefType), fieldInfo);
                    else if (sqlField is SqlField)
                        fields.Add(new SqlField(sqlField.FieldType, name), fieldInfo);
                }
            }

            return fields;
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

        /// <summary>
        /// Libera los recursos usados
        /// </summary>

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
