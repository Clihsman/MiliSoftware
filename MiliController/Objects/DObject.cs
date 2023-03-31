/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 13/12/2022          *
 * Assembly : MiliController           *
 * *************************************/

using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace MiliSoftware
{
    public class DObject : DynamicObject
    {
        Dictionary<string, object> values = new Dictionary<string, object>();

        public DObject(Dictionary<string, object> values)
        {
            this.values = new Dictionary<string, object>(values);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return values.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            bool contains = values.ContainsKey(binder.Name);

            if (contains)
                values[binder.Name] = value;

            return contains;
        }

        public static DObject[] GetArray(Dictionary<string, object>[] values)
        {
            DObject[] result = new DObject[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = new DObject(values[i]);
            }
            return result;
        }

        public string ToJson() {
            return JsonConvert.SerializeObject(values);
        }
    }
}
