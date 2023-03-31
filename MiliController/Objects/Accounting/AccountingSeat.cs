/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 28/03/2023          *
 * Assembly : MiliController           *
 * *************************************/

using MiliSoftware.SqlLite;
using System;

namespace MiliSoftware
{
    [SqlTable("AccountingSeat")]
    public class AccountingSeat
    {
        [SqlField(SqlFieldType.Text)]
        public string Code { get; set; }
        [SqlField(SqlFieldType.DateTime)]
        public DateTime Date { get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Reference { get; set; }
        [SqlField(SqlFieldType.Text)]
        public string SeatNumber { get; set; }
        [SqlField(SqlFieldType.Integer)]
        public uint Document { get; set; }
        [SqlField(SqlFieldType.Integer)]
        public uint DocumentNumber { get; set; }
        [SqlField(SqlFieldType.Integer)]
        public uint Book { get; set; }
        [SqlField(SqlFieldType.Integer)]
        public uint Correlative { get; set; }
        [SqlField(SqlFieldType.Integer)]
        public uint Fountain { get; set; }
        [SqlTableRef("AccountingPosition", SqlTableRefType.Array)]
        public AccountingPosition[] AccountingPositions { get; set; }
    }
}
