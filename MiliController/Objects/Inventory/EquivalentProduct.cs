/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 02/04/2023          *
 * Assembly : MiliController           *
 * *************************************/
 
using MiliSoftware.SqlLite;
using Newtonsoft.Json.Linq;
using System;

namespace MiliSoftware
{
    [SqlTable("EquivalentProducts")]
    public class EquivalentProduct : IJsonObject
    {
        [SqlField(SqlFieldType.Text)]
        public string _id { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Code { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Name { get; protected set; }

        public EquivalentProduct() {}

        public EquivalentProduct(string _id, string Code, string Name) {
            this._id = _id;
            this.Code = Code;
            this.Name = Name;
        }

        public void FromJson(string json)
        {
            JObject jObject = JObject.Parse(json);
            _id = jObject.Value<string>(nameof(_id));
            Code = jObject.Value<string>(nameof(Code));
        }

        public string ToJson()
        {
            JObject jObject = new JObject();
            jObject.Add(nameof(_id), _id);
            jObject.Add(nameof(Code), Code);
            return jObject.ToString();
        }
    }
}
