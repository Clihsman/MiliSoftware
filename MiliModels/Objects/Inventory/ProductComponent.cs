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
        public string _id { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Code { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Name { get; protected set; }
        [SqlField(SqlFieldType.Integer)]
        public int Amount { get; protected set; }

        public ProductComponent() { }

        public ProductComponent(string _id, string Code, string Name, int Amount)
        {
            this._id = _id;
            this.Code = Code;
            this.Name = Name;
            this.Amount = Amount;
        }

        public string ToJson()
        {
            JObject jObject = new JObject();
            jObject.Add(nameof(_id), _id);
            jObject.Add(nameof(Code), Code);
            jObject.Add(nameof(Amount), Amount);
            return jObject.ToString();
        }

        public void FromJson(string json)
        {
            JObject jObject = JObject.Parse(json);
            _id = jObject.Value<string>(nameof(_id));
            Code = jObject.Value<string>(nameof(Code));
            Amount = jObject.Value<int>(nameof(Amount));
        }
    }
}
