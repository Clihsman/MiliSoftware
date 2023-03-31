/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 01/01/2023          *
 * Assembly : GrapInterCom             *
 * *************************************/

using MiliSoftware.UI;

namespace GrapInterCom.Interfaces.Email
{
    public interface IEmailGUI : IGUI<string, object[], string, string>
    {
        string GetTo();
        string GetFrom();
        string GetBody();
        string GetSubject();
        string[] GetFiles();
    }
}
