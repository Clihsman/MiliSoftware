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
    [SqlTable("EquivalentProducts")]
    public class EquivalentProduct : IJsonObject
    {
        [SqlField(SqlFieldType.Text)]
        public int id { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Code { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Name { get; protected set; }

        public EquivalentProduct() {}

        public EquivalentProduct(int id, string Code, string Name) {
            this.id = id;
            this.Code = Code;
            this.Name = Name;
        }

        public void FromJson(string json)
        {
            JObject jObject = JObject.Parse(json);
            id = jObject.Value<int>(nameof(id));
            Code = jObject.Value<string>(nameof(Code));
            Name = jObject.Value<string>(nameof(Name));
        }

        public string ToJson()
        {
            JObject jObject = new JObject();
            jObject.Add(nameof(id), id);
            jObject.Add(nameof(Code), Code);
            jObject.Add(nameof(Name), Name);
            return jObject.ToString();
        }
    }
}
