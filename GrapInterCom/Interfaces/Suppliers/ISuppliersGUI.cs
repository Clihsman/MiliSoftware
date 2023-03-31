/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 13/12/2022          *
 * Assembly : GrapInterCom             *
 * *************************************/


using MiliSoftware.UI;

namespace GrapInterCom.Interfaces.Suppliers
{
    public interface ISuppliersGUI : IGUI<string, object[], string, string>
    {
        object[] GetValues();
        void SetValues(object[] datas);
    }
}
