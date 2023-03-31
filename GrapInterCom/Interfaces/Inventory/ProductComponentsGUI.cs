/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 24/12/2022          *
 * Assembly : GrapInterCom             *
 * *************************************/

using MiliSoftware.UI;

namespace GrapInterCom.Interfaces.Inventory
{
    public  interface ProductComponentsGUI : IGUI<string, object[], string, string>
    {
        object[] GetValues();
        void SetValues(object[] datas);
    }
}
