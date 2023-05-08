/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 16/11/2022          *
 * *************************************/

using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.IO;
using System.Security.Cryptography;

namespace MiliSoftware.Model.WebServices
{
    /// <summary>
    /// Esta clase permite obtener funciones para leer llaves del sevicio RSA
    /// </summary>
    public abstract class RSAService
    {
        /// <summary>
        /// llave RSA formato PEM o XML
        /// </summary>
        protected string Key { get; set; }

        /// <summary>
        /// Contraseña de la llave
        /// </summary>
        protected IPasswordFinder Password;

        /// <summary>
        /// Obtiene la llave privada en formato PEM
        /// </summary>
        /// <returns>llave privada</returns>
        protected void FromPrivatePemParameters(IRSAServices services)
        {
            RSAParameters parameters;
            using (StringReader stringReader = new StringReader(Key))
            {
                PemReader pemReader;

                RsaPrivateCrtKeyParameters keyParams = null;
                
                if (Password is null)
                {
                    pemReader = new PemReader(stringReader);
                    keyParams = (RsaPrivateCrtKeyParameters)pemReader.ReadObject();
                }
                else
                {
                    pemReader = new PemReader(stringReader, Password);
                    keyParams = (RsaPrivateCrtKeyParameters)pemReader.ReadObject();
                }

                parameters = DotNetUtilities.ToRSAParameters(keyParams);
            }
            services.ServiceProvider.ImportParameters(parameters);
        }

        /// <summary>
        /// Obtiene la llave publica en Formato PEM
        /// </summary>
        /// <returns>llave publica</returns>
        protected void FromPublicPemParameters(IRSAServices services)
        {
            RSAParameters parameters;
            using (StringReader stringReader = new StringReader(Key))
            {
                PemReader pemReader;

                RsaKeyParameters keyParams = null;
                if (Password is null)
                {
                    pemReader = new PemReader(stringReader);
                    keyParams = (RsaKeyParameters)pemReader.ReadObject();
                }
                else
                {
                    pemReader = new PemReader(stringReader, Password);
                    keyParams = (RsaKeyParameters)pemReader.ReadObject();
                }

                parameters = DotNetUtilities.ToRSAParameters(keyParams);
            }

            services.ServiceProvider.ImportParameters(parameters);
        }

        /// <summary>
        /// Obtiene la llave publica de formato XML
        /// </summary>
        /// <returns>llave publica</returns>
        protected void FromRSAXmlParameters(IRSAServices services)
        {
            services.ServiceProvider.FromXmlString(Key);
        }

        /// <summary>
        /// Carga la llave dependiendo el formato ingresado
        /// </summary>
        /// <returns>llave publica</returns>
        /// <param name="services">Servicios RSA</param>
        /// <param name="format">Formato de la llave</param>
        protected void ImportRSAPublicParameters(IRSAServices services)
        {
            if (services.RSAKeyFormat == RSAKeyFormat.PEM)
                FromPublicPemParameters(services);
            else if (services.RSAKeyFormat == RSAKeyFormat.XML)
                FromRSAXmlParameters(services);
            else
                throw new System.NotImplementedException();
        }

        /// <summary>
        /// Carga la llave dependiendo el formato ingresado
        /// </summary>
        /// <returns>llave publica</returns>
        /// <param name="services">Servicios RSA</param>
        /// <param name="format">Formato de la llave</param>
        protected void ImportRSAPrivateParameters(IRSAServices services)
        {
            if (services.RSAKeyFormat == RSAKeyFormat.PEM)
                FromPrivatePemParameters(services);
            else if (services.RSAKeyFormat == RSAKeyFormat.XML)
                FromRSAXmlParameters(services);
            else
                throw new System.NotImplementedException();
        }

        /// <summary>
        /// Retorna la llave privada en formato PEM
        /// </summary>
        /// <param name="rsa">Servicio de encriptación RSA</param>
        /// <returns></returns>
        protected string GetPemPrivateKey(IRSAServices services) {
            string Key = null;
            using (StringWriter stringWriter = new StringWriter())
            {
                PemWriter pemWriter = new PemWriter(stringWriter);
                pemWriter.WriteObject(services.ServiceProvider.ExportParameters(true));
                Key = stringWriter.ToString();
            }
            return Key;
        }

        /// <summary>
        /// Retorna la llave publica en formato PEM
        /// </summary>
        /// <param name="rsa">Servicio de encriptación RSA</param>
        /// <returns></returns>
        protected string GetPemPublicKey(IRSAServices services)
        {
            string Key = null;
            using (StringWriter stringWriter = new StringWriter())
            {
                PemWriter pemWriter = new PemWriter(stringWriter);
                pemWriter.WriteObject(services.ServiceProvider.ExportParameters(false));
                Key = stringWriter.ToString();
            }
            return Key;
        }

        /// <summary>
        /// Retorna la llave privada en formato XML
        /// </summary>
        /// <param name="rsa">Servicio de encriptación RSA</param>
        /// <returns></returns>
        protected string GetXMLPrivateKey(IRSAServices services)
        {
           return services.ServiceProvider.ToXmlString(true);
        }

        /// <summary>
        /// Retorna la llave publica en formato XML
        /// </summary>
        /// <param name="rsa">Servicio de encriptación RSA</param>
        /// <returns></returns>
        protected string GetXMLPublicaKey(IRSAServices services)
        {
            return services.ServiceProvider.ToXmlString(false);
        }
    }
}
