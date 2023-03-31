/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 30/03/2023          *
 * Assembly : MiliDatabase             *
 * *************************************/
 
using System.Collections.Generic;
using System.Text;

namespace MiliSoftware.SqlLite
{
    public class SqlSchema
    {
        public SqlTable SqlTable { get; private set; }
        public SqlField[] Fields { get; private set; }

        public SqlSchema(SqlTable sqlTable, SqlField[] sqlFields)
        {
            SqlTable = sqlTable;
            Fields = sqlFields;
        }

        public string GetCreateTable()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("CREATE TABLE IF NOT EXISTS {0}", SqlTable.TableName));
            stringBuilder.Append(GetFields());
            return stringBuilder.ToString();
        }

        private string GetFields() {
            List<string> fields = new List<string>();
            foreach (SqlField sqlField in Fields)
            {
                fields.Add(string.Format("\"{0}\" {1}", sqlField.Name, sqlField.FieldType.ToString().ToUpper()));
            }
            return string.Format("({0})", string.Join(",", fields));
        }

        public void Save(SqlLiteDatabase database, params object[] data)
        {

        }
    }
}
