/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 12/12/2022          *
 * *************************************/

namespace MiliSoftware
{
    public interface ICRUD<C, R, U, D>
    {
        bool Create(C data);
        R Read(R data);
        bool Update(U data);
        bool Delete(D data);
    }
}
