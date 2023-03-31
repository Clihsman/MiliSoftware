/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 17/11/2022          *
 * *************************************/

namespace MiliSoftware.Model.WebServices
{
    public interface IRSADecrypt : IRSAServices
    {
         string DecryptString(string @string);
    }
}
