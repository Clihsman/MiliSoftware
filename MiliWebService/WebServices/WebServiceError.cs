/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 08/02/2023          *
 * Assembly : MiliWebService           *
 * *************************************/

using System.Collections.Generic;

namespace MiliWebService.WebServices
{
    public sealed class WebServiceError
    {
        public string Error { get; private set; }
        public string Method { get; private set; }
        public string Uri { get; private set; }
        public string Exception { get; private set; }

        public WebServiceError(string error, string method, string uri, string exception) {
            Error = error;
            Method = method;
            Uri = uri;
            Exception = exception;
        }

        public string ToJson() {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        public static implicit operator bool(WebServiceError foo)
        {
            return !(foo is null);
        }
    }
}
