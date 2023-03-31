using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.SqlLite
{
    public class SqlTable : Attribute
    {
        public string TableName {private set;get;}

        public SqlTable(string tableName) {
            TableName = tableName;
        }

        public SqlTable()
        {
            TableName = null;
        }
    }
}
