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

   // [SqlTable("InventoryGroup")]
    public class InventoryGroup : IJsonObject
    {
        public int Id { get; set; }
        [SqlField(SqlFieldType.Text)]
        public string _id { get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Code { get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Name { get; set; }

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
            Code = jObject.Value<string>("Codigo");
            Name = jObject.Value<string>("Nombre");
        }

        public void FromJObject(JObject jObject)
        {
            _id = jObject.Value<string>(nameof(_id));
            Code = jObject.Value<string>("Codigo");
            Name = jObject.Value<string>("Nombre");
        }

        public string ToJson()
        {
            JObject jObject = new JObject
            {
                { nameof(_id), _id },
                { nameof(Code), Code },
                { nameof(Name), Name }
            };
            return jObject.ToString();
        }

        public JObject GetJObject() {
            JObject jObject = new JObject
            {
                { nameof(_id), _id },
                { nameof(Code), Code },
                { nameof(Name), Name }
            };
            return jObject;
        }

        public static InventoryGroup[] FromJsonArray(string json) 
        {
            JArray inventoryGroupsJArray =  JArray.Parse(json);
            InventoryGroup[] inventoryGroups = new InventoryGroup[inventoryGroupsJArray.Count];

            for (int i = 0; i < inventoryGroupsJArray.Count; i++) {
                inventoryGroups[i] = new InventoryGroup();
                inventoryGroups[i].FromJObject((JObject)inventoryGroupsJArray[i]);
            }
            return inventoryGroups;
        } 
    }
}
