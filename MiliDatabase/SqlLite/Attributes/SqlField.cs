using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.SqlLite
{
    public class SqlField : Attribute
    {
        public SqlFieldType FieldType { private set; get; }
        public int Length { private set; get; }
        public string Name { private set; get; }

        public SqlField(SqlFieldType fieldType, int length = -1)
        {
            FieldType = fieldType;
            Length = length;
            Name = null;
        }

        public SqlField(SqlFieldType fieldType,string name, int length = -1)
        {
            FieldType = fieldType;
            Length = length;
            Name = name;
        }
    }
}
