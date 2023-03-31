/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 02/11/2022          *
 * Assembly : GrapInterCom             *
 * *************************************/

using System;

namespace MiliSoftware.UI
{
    public interface IGUI<C, R, U, D> : IGUITransform, IGUIInfo
    {
        ICRUD<C, R, U, D> controller { get; set; }
        DialogResult DialogResult { get; set; }
        #region IGUI

        void Show();
        DialogResult ShowDialog();
        IGUI<C, R, U, D> GetParent();
        void Close();

        event EventHandler OnOpen;
        event EventHandler OnClosed;
        
        #endregion
    }
}
