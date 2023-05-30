/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 28/03/2023          *
 * Assembly : MiliController           *
 * *************************************/

using MiliSoftware.SqlLite;

namespace MiliSoftware
{
    [SqlTable("AccountingPosition")]
    public class AccountingPosition
    {
        [SqlField(SqlFieldType.Integer)]
        public uint AccountCode { get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Description { get; set; }
        [SqlField(SqlFieldType.Real)]
        public decimal CentCost { get; set; }
        [SqlField(SqlFieldType.Real)]
        public decimal Third { get; set; }
        [SqlField(SqlFieldType.Real)]
        public decimal Base { get; set; }
        [SqlField(SqlFieldType.Real)]
        public decimal Debit { get; set; }
        [SqlField(SqlFieldType.Real)]
        public decimal Credit { get; set; }
    }
}
        