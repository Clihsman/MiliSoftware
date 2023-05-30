/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 26/12/2022          *
 * Assembly : MiliController           *
 * *************************************/

using MiliSoftware;
using MiliSoftware.UI;

namespace GrapInterCom.Interfaces.Inventory
{
    public interface EquivalentProductsGUI : IGUI<string, EquivalentProduct[], string, string>
    {
        EquivalentProduct[] GetValues();
        void SetValues(EquivalentProduct[] datas);
    }
}
