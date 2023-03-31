/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 17/11/2022          *
 * *************************************/

using System.Security.Cryptography;
using System.Text;

namespace MiliSoftware.Model.WebServices
{
    public interface IRSAServices
    {
        /// <summary>
     /// Codificación
     /// </summary>
        Encoding Encoding { get; }

        /// <summary>
        /// Servicio de encriptación RSA
        /// </summary>
        RSACryptoServiceProvider ServiceProvider { get; }

        RSAKeyFormat RSAKeyFormat { get; }
    }
}
