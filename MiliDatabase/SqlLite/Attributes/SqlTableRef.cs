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

        public SqlTableRef(string name, SqlTableRefType sqlTableRefType) : base(SqlFieldType.Text, name)
        {
            SqlTableRefType = sqlTableRefType;
        }

        public override bool Equals(object obj)
        {
            SqlTableRef sqlTable = (SqlTableRef)obj;
            return sqlTable.Name == Name && sqlTable.SqlTableRefType == SqlTableRefType;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
