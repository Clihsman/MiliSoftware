/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 02/11/2022          *
 * Assembly : GrapInterCom             *
 * *************************************/

namespace MiliSoftware.UI
{
    public interface IGUITransform
    {
        int GetWidth();
        int GetHeight();

        int GetX();
        int GetY();

        ScreenNumber GetScreen();
    }
}
