/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 26/12/2022          *
 * Assembly : MiliController           *
 * *************************************/

using MiliSoftware.UI;

namespace GrapInterCom.Interfaces.Inventory
{
    public interface EquivalentProductsGUI : IGUI<string, object[], string, string>
    {
        object[] GetValues();
        void SetValues(object[] datas);
    }
}
