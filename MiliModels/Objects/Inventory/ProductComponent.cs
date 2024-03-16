/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 02/04/2023          *
 * Assembly : MiliController           *
 * *************************************/

using MiliSoftware.SqlLite;
using Newtonsoft.Json.Linq;

namespace MiliSoftware
{
    [SqlTable("ProductComponents")]
    public class ProductComponent : IJsonObject
    {
        [SqlField(SqlFieldType.Text)]
        public int id { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Code { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Name { get; protected set; }
        [SqlField(SqlFieldType.Integer)]
        public int Amount { get; protected set; }

        public ProductComponent() { }

        public ProductComponent(int id, string Code, string Name, int Amount)
        {
            this.id = id;
            this.Code = Code;
            this.Name = Name;
            this.Amount = Amount;
        }

        public string ToJson()
        {
            JObject jObject = new JObject();
            jObject.Add(nameof(id), id);
            jObject.Add(nameof(Code), Code);
            jObject.Add(nameof(Amount), Amount);
            return jObject.ToString();
        }

        public void FromJson(string json)
        {
            JObject jObject = JObject.Parse(json);
            id = jObject.Value<int>(nameof(id));
            Code = jObject.Value<string>(nameof(Code));
            Amount = jObject.Value<int>(nameof(Amount));
        }
    }
}
