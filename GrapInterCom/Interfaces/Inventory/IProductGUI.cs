/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 17/12/2022          *
 * Assembly : GrapInterCom             *
 * *************************************/

using MiliSoftware.UI;

namespace GrapInterCom.Interfaces.Inventory
{
    public interface IProductGUI<C, R, U, D> : IGUI<C, R, U, D>
    {
        object[] GetValues();
        void SetValues(object[] datas);
    }
}
