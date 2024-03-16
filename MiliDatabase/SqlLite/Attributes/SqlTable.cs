using System;

namespace MiliSoftware.SqlLite
{
    /// <summary>
    /// esta clase permite crear una tabla en sqlLite a traves de una clase
    /// </summary>
    public class SqlTable : Attribute
    {
        /// <summary>
        /// Obtiene el nombre de la tabla
        /// </summary>
        public string TableName {private set; get;}

        public SqlTable(string tableName) {
            TableName = tableName;
        }

        public SqlTable()
        {
            TableName = null;
        }
    }
}
