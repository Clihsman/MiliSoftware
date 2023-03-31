/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 28/10/2022          *
 * *************************************/

using static Newtonsoft.Json.JsonConvert;

namespace MiliSoftware.Model.Json
{
    public class JsonUtils
    {
        public static string ToJson(object o) {
            return SerializeObject(o);
        }

        public static T FromJson<T>(string json)
        {
            return DeserializeObject<T>(json);
        }
    }
}