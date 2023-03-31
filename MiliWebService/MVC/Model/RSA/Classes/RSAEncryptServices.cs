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
    /// Esta clase permite encriptación en formato RSA
    /// </summary>
    public class RSAEncryptServices : RSAService, IRSAEncrypt, IDisposable
    {
        /// <summary>
        /// Codificación
        /// </summary>
        public Encoding Encoding { get; private set; }

        /// <summary>
        /// Servicio de encriptación RSA
        /// </summary>
        public RSACryptoServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Formato en que esta la llave de 
        /// </summary>
        public RSAKeyFormat RSAKeyFormat { get;private set; }

        /// <summary>
        /// Metodo contructor privado encriptación
        /// </summary>
        private RSAEncryptServices() {
            Encoding = Encoding.UTF8;
            RSAKeyFormat = RSAKeyFormat.PEM;
        }

        /// <summary>
        /// Crea los servicios necesarios para de encriptación
        /// </summary>
        private void Init()
        {
            ServiceProvider = new RSACryptoServiceProvider();
            ImportRSAPublicParameters(this);
        }

        /// <summary>
        /// Metodo contructor de la clase RSAEncryptServices
        /// </summary>
        /// <param name="reader">Stream donde se encuentra la llave publica</param>
        /// <param name="password">Contraseña de llave publica este valor puede ser nulo</param>
        /// <param name="encoding">Tipo de codificación en la que se va a encriptar</param>
        /// <param name="keyFormat">Formato de la llave en que se codificara</param>
        public RSAEncryptServices(StreamReader reader, IPasswordFinder password, Encoding encoding, RSAKeyFormat keyFormat) : this()
        {
            Key = reader.ReadToEnd();
            Password = password;
            Encoding = encoding;
            RSAKeyFormat = keyFormat;
            Init();
        }

        /// <summary>
        /// Metodo contructor de la clase RSAEncryptServices
        /// </summary>
        /// <param name="key">Llave publica</param>
        /// <param name="password">Contraseña de llave publica este valor puede ser nulo</param>
        /// <param name="encoding">Tipo de codificación en la que se va a encriptar</param>
        /// <param name="keyFormat">Formato de la llave en que se codificara</param>
        public RSAEncryptServices(string key, IPasswordFinder password, Encoding encoding, RSAKeyFormat keyFormat) : this()
        {
            Key = key;
            Password = password;
            Encoding = encoding;
            RSAKeyFormat = keyFormat;
            Init();
        }

        /// <summary>
        /// Metodo contructor de la clase RSAEncryptServices
        /// </summary>
        /// <param name="key">Llave publica</param>
        /// <param name="password">contraseña de llave publica este valor puede ser nulo</param>
        public RSAEncryptServices(StreamReader reader, IPasswordFinder password) : this()
        {
            Key = reader.ReadToEnd();
            Password = password;
            Init();
        }

        /// <summary>
        /// Metodo contructor de la clase RSAEncryptServices
        /// </summary>
        /// <param name="key">Llave publica</param>
        /// <param name="password">contraseña de llave publica este valor puede ser nulo</param>
        public RSAEncryptServices(string key, IPasswordFinder password) : this()
        {
            Key = key;
            Password = password;
            Init();
        }

        /// <summary>
        /// Este metodo encripta una cadena de texto a formato RSA
        /// </summary>
        /// <param name="@string"></param>
        /// <returns></returns>
        public string EncryptString(string @string)
        {
            byte[] data = Encoding.GetBytes(@string);
            var cypher = ServiceProvider.Encrypt(data, false);
            return Convert.ToBase64String(cypher);
        }

        /// <summary>
        /// libera los recursos
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
