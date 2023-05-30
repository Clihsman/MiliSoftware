/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 22/05/2023          *
 * Assembly : MiliModels               *
 * *************************************/

using MiliSoftware.SqlLite;
using Newtonsoft.Json.Linq;

namespace MiliSoftware
{
    [SqlTable("InventoryGroup")]
    public class InventoryGroup : IJsonObject
    {
        [SqlField(SqlFieldType.Text)]
        public string _id { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Code { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Name { get; protected set; }

        public InventoryGroup() { }

        public InventoryGroup(string _id, string Code, string Name)
        {
            this._id = _id;
            this.Code = Code;
            this.Name = Name;
        }

        public InventoryGroup(string Code, string Name)
        {
            this.Code = Code;
            this.Name = Name;
        }

        public void FromJson(string json)
        {
            JObject jObject = JObject.Parse(json);
            _id = jObject.Value<string>(nameof(_id));
            Code = jObject.Value<string>(nameof(Code));
            Name = jObject.Value<string>(nameof(Name));
        }

        public string ToJson()
        {
            JObject jObject = new JObject();
            jObject.Add(nameof(_id), _id);
            jObject.Add(nameof(Code), Code);
            jObject.Add(nameof(Name), Name);
            return jObject.ToString();
        }
    }
}
