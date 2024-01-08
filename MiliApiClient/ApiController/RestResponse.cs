using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRestClient
{
    public class RestResponse<T, R>
    {
        public T Value0 { get; set; }
        public R Value1 { get; set; }

        public static implicit operator T(RestResponse<T, R> response) { 
            return response.Value0;
        }

        public static implicit operator R(RestResponse<T, R> response)
        {
            return response.Value1;
        }

        public static implicit operator bool(RestResponse<T, R> response)
        {
            if(response == null) return false;
            return response.Value0 != null;
        }
    }
}
