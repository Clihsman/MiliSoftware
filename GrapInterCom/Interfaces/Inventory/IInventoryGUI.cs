/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 13/12/2022          *
 * Assembly : GrapInterCom             *
 * *************************************/

using MiliSoftware.UI;

namespace GrapInterCom.Interfaces.Inventory
{
    public interface IInventoryGUI : IGUI<string, object[], string, string>
    {
        /*InventoryController Controller { get; set; }*/
        object[] GetValues();
        void SetValues(object[] datas);
    }
}
