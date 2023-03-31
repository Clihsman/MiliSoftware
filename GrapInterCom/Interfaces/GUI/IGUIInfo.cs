/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 02/11/2022          *
 * Assembly : GrapInterCom             *
 * *************************************/

using System;

namespace MiliSoftware.UI
{
    public interface IGUIInfo
    {
        string GetTitle();
        string GetName();

        GUIState GetGUIState();

        object GetGUI();
        IntPtr GetHandle();
    }
}
