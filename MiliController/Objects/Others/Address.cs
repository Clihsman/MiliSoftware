/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 28/04/2023          *
 * Assembly : MiliController           *
 * *************************************/

using MiliSoftware.SqlLite;

namespace MiliSoftware
{
    [SqlTable("Address")]
    public struct Address
    {
        [SqlField(SqlFieldType.Text)]
        public string Country { get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Condition { get; set; }
        [SqlField(SqlFieldType.Text)]
        public string City { get; set; }
        [SqlField(SqlFieldType.Text)]
        public string PostalCode { get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Direction { get; set; }
    }
}
