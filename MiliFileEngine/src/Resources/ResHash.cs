using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MiliFileEngine.src.Resources
{
    public class ResHash
    {
        private static Dictionary<string, string> files = new Dictionary<string, string>()
        {
            {"LANGUAJE","FAAD656CF2C149963F2C6D6B2500674A"},
            {"SETTING","FAAD651CF2C1E9963F2C6D6D2500674A"},
            {"COUNTRIES","53B1021E5263F8DE65B7E57A79E29A4"}
            /*@HASH*/
        };

        public static string GetHash(string name)
        {
            string hash = "";

            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var data = sha1.ComputeHash(Encoding.UTF8.GetBytes(name));
                var sb = new StringBuilder(data.Length * 2);

                foreach (byte b in data)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                hash = sb.ToString();
            }

            return hash;
        }
    }
}
