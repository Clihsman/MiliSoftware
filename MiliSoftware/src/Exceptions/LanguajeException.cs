using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.src.Exceptions
{
    class LanguajeException : Exception
    {
        public LanguajeException(string message) : base(message)
        {

        }

        public LanguajeException(string format,params object[] messages)
        {
           Source = string.Format(format,messages);
        }
    }
}
