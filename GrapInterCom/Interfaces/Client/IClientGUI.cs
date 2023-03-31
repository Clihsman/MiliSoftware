/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 02/11/2022          *
 * Assembly : GrapInterCom             *
 * *************************************/

namespace MiliSoftware.UI
{
    public interface IClientGUI : IGUI<string, object[], string, string>
    {
         object[] GetValues();
         void SetValues(object[] datas);
    }
}
