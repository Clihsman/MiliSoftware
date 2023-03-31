/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 28/10/2022          *
 * *************************************/

using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace MiliSoftware.Model.Security
{
    public class Encrypt
    {
        public static string GetMD5(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static string GetSHA1(string str)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static string GetSHA384(string str)
        {
            SHA384 sha384 = SHA384.Create();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha384.ComputeHash(Encoding.UTF8.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static string GetSHA512(string str)
        {
            SHA512 sha521 = SHA512.Create();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha521.ComputeHash(Encoding.UTF8.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}