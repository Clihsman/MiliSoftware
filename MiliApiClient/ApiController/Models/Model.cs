using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.Modelos
{
    public abstract class Model
    {
        private string Message { get; set; } = null;

        public static implicit operator string(Model modelo) => modelo.Message;
        public static implicit operator Model(string message) => new MessageObject() { Message = message };
        public static implicit operator bool(Model modelo) => modelo.Message != null;
    }
}
