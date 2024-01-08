using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliWebService.WebServices
{
    public class Response
    {
        public object Body { get; private set; }
        public int Status { get; private set; }

        public Response(object body, int status) { 
            Body = body;
            Status = status;
        }

        public static implicit operator bool(Response response)
        {
            return response != null && response.Status == 200 || response.Status == 201;
        }
    }
}
