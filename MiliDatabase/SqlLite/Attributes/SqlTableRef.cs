/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 30/03/2023          *
 * Assembly : MiliDatabase             *
 * *************************************/

namespace MiliSoftware.SqlLite
{
    public class SqlTableRef : SqlField
    {
        public SqlTableRefType SqlTableRefType { get; private set; }

        public SqlTableRef(string tableName, SqlTableRefType sqlTableRefType) : base(SqlFieldType.Numeric, tableName)
        {
            SqlTableRefType = sqlTableRefType;
        }
    }
}
