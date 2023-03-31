/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 02/01/2023          *
 * Assembly : GrapInterCom             *
 * *************************************/

using MiliSoftware.UI;

namespace GrapInterCom.Interfaces.Email
{
    public interface IEmailsGUI : IGUI<string, object[], string, string>
    {
        object[] GetValues();
        void SetValues(object[] datas);
    }
}
