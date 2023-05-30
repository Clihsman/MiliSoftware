/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 24/12/2022          *
 * Assembly : GrapInterCom             *
 * *************************************/

using MiliSoftware;
using MiliSoftware.UI;

namespace GrapInterCom.Interfaces.Inventory
{
    public  interface ProductComponentsGUI : IGUI<string, ProductComponent[], string, string>
    {
        ProductComponent[] GetValues();
        void SetValues(object[] datas);
    }
}
