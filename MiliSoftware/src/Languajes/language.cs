using MiliSoftware.src.Exceptions;
using MiliSoftware.src.IDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.src.Languajes
{
    public class language
    {
        public const int classID = ClassIDS.LANGUAJE;

        public static class Login
        {
            public const int classID = ClassIDS.LANGUAJE_LOGIN;

            private static Dictionary<short, string> data;

            public static string Get(short id)
            {
                string value;
                if (data.TryGetValue(id, out value))
                    return value;
                throw new LanguajeException("El indice de texto no establecido ID:{0} class[{0}.{1}]:26",id,language.classID,classID);
            }
        }
    }
}
