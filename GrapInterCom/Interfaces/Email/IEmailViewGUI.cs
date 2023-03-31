/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 01/01/2023          *
 * Assembly : GrapInterCom             *
 * *************************************/

using MiliSoftware.UI;

namespace GrapInterCom.Interfaces.Email
{
    public interface IEmailViewGUI : IGUI<string, object[], string, string>
    {
        void SetData(string sender, string time, string subject, string body);
    }
}
