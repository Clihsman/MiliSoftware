using System;
using MiliSoftware.SqlLite;
using Newtonsoft.Json.Linq;

namespace MiliSoftware
{
    [SqlTable("Customers")]
    public class Client : IJsonObject
    {
        #region Varialbles
        [SqlField(SqlFieldType.Text)]
        public string Code { get; set; }

        [SqlField(SqlFieldType.Text)]
        public string Type { get; set; }

        [SqlField(SqlFieldType.Text)]
        public string Names { get; set; }

        [SqlField(SqlFieldType.Text)]
        public string Surnames { get; set; }

        [SqlField(SqlFieldType.Text)]
        public string Email { get; set; }

        [SqlField(SqlFieldType.Text)]
        public string IdentificationType { get; set; }

        [SqlField(SqlFieldType.Text)]
        public string Identification { get; set; }

        [SqlField(SqlFieldType.Text)]
        public string Picture { get; set; }

        [SqlField(SqlFieldType.Text)]
        public string BranchOffice { get; set; }

        [SqlField(SqlFieldType.Text)]
        public string Country { get; set; }

        [SqlField(SqlFieldType.Text)]
        public string Department { get; set; }

        [SqlField(SqlFieldType.Text)]
        public string City { get; set; }

        [SqlField(SqlFieldType.Text)]
        public string PostalCode { get; set; }

        [SqlField(SqlFieldType.Text)]
        public string Direction { get; set; }
        #endregion Variables

        public void FromJson(string json)
        {
            JObject jObject = JObject.Parse(json);
            Code = jObject.Value<string>(nameof(Code));
            Type = jObject.Value<string>(nameof(Type));
            Names = jObject.Value<string>(nameof(Names));
            Email = jObject.Value<string>(nameof(Email));
            IdentificationType = jObject.Value<string>(nameof(IdentificationType));
            Identification = jObject.Value<string>(nameof(Identification));
            Picture = jObject.Value<string>(nameof(Picture));
            BranchOffice = jObject.Value<string>(nameof(BranchOffice));
            Country = jObject.Value<string>(nameof(Country));
            Department = jObject.Value<string>(nameof(Department));
            City = jObject.Value<string>(nameof(City));
            PostalCode = jObject.Value<string>(nameof(PostalCode));
            Direction = jObject.Value<string>(nameof(Direction));
        }

        public string ToJson()
        {
            JObject jObject = new JObject();
            jObject.Add(nameof(Code), Code);
            jObject.Add(nameof(Type), Type);
            jObject.Add(nameof(Names), Surnames);
            jObject.Add(nameof(Email), Email);
            jObject.Add(nameof(IdentificationType), IdentificationType);
            jObject.Add(nameof(Identification), Identification);
            jObject.Add(nameof(Picture), Picture);
            jObject.Add(nameof(BranchOffice), BranchOffice);
            jObject.Add(nameof(Country), Country);
            jObject.Add(nameof(Department), Department);
            jObject.Add(nameof(City), City);
            jObject.Add(nameof(PostalCode), PostalCode);
            jObject.Add(nameof(Direction), Direction);
            return jObject.ToString();
        }
    }
}
