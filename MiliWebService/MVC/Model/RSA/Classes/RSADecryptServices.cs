/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 24/10/2022          *
 * *************************************/

using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MiliSoftware.Model.WebServices
{
    /// <summary>
    /// Esta clase permite desencriptación en formato RSA
    /// </summary>
    public class RSADecryptServices : RSAService, IRSADecrypt, IDisposable
    {
        /// <summary>
        /// Codificación
        /// </summary>
        public Encoding Encoding { get; private set; }

        /// <summary>
        /// Servicio de encriptación RSA
        /// </summary>
        public RSACryptoServiceProvider ServiceProvider { get;private set; }

        public RSAKeyFormat RSAKeyFormat { get; private set; }

        /// <summary>
        /// Metodo contructor privado
        /// </summary>
        private RSADecryptServices()
        {
            Encoding = Encoding.UTF8;
            RSAKeyFormat = RSAKeyFormat.PEM;
        }

        /// <summary>
        /// Crea los servicios necesarios para de desencriptación
        /// </summary>
        private void Init()
        {
            ServiceProvider = new RSACryptoServiceProvider();
            ImportRSAPrivateParameters(this);
        }

        /// <summary>
        /// Contructor de el servicio de encriptación RSA bajo el formato PEM
        /// </summary>
        /// <param name="reader">llave privada formato PEM</param>
        /// <param name="password">Contraseña de la llave este valor pueder ser nulo</param>
        public RSADecryptServices(StreamReader reader, IPasswordFinder password) : this()
        {
            Key = reader.ReadToEnd();
            Password = password;
            Init();
        }

        /// <summary>
        /// Metodo contructor de la clase RSADecryptServices
        /// </summary>
        /// <param name="reader">Stream donde se encuentra la llave privada</param>
        /// <param name="password">Contraseña de llave privada este valor puede ser nulo</param>
        /// <param name="encoding">Tipo de codificación en la que se va a desencriptar</param>
        /// <param name="keyFormat">Formato de la llave en que se decodificara</param>
        public RSADecryptServices(StreamReader reader, IPasswordFinder password, Encoding encoding, RSAKeyFormat keyFormat) : this()
        {
            Key = reader.ReadToEnd();
            Password = password;
            Encoding = encoding;
            RSAKeyFormat = keyFormat;
            Init();
        }

        /// <summary>
        /// Metodo contructor de la clase RSADecryptServices
        /// </summary>
        /// <param name="key">Llave privada</param>
        /// <param name="password">Contraseña de llave privada este valor puede ser nulo</param>
        /// <param name="encoding">Tipo de codificación en la que se va a desencriptar</param>
        /// <param name="keyFormat">Formato de la llave en que se decodificara</param>
        public RSADecryptServices(string key, IPasswordFinder password, Encoding encoding, RSAKeyFormat keyFormat) : this()
        {
            Key = key;
            Password = password;
            Encoding = encoding;
            RSAKeyFormat = keyFormat;
            Init();
        }

        /// <summary>
        /// Contructor de el servicio de desencriptación RSA bajo el formato PEM
        /// </summary>
        /// <param name="reader">llave privada formato PEM</param>
        /// <param name="password">Contraseña de la llave este valor pueder ser nulo</param>
        public RSADecryptServices(string key, IPasswordFinder password) : this()
        {
            Key = key;
            Password = password;
            Init();
        }

        /// <summary>
        /// Este metodo permite la desencriptación en formato RSA
        /// </summary>
        /// <param name="@string">Datos cifrados en Base64</param>
        public string DecryptString(string @string)
        {
            byte[] decryptData = ServiceProvider.Decrypt(Convert.FromBase64String(@string), false);
            return Encoding.GetString(decryptData);
        }

        /// <summary>
        /// Libera los recursos
        /// </summary>
        public void Dispose()
        {
            Key = null;
            Password = null;
            Encoding = null;
            ServiceProvider.Dispose();
            ServiceProvider = null;
        }
    }
}
