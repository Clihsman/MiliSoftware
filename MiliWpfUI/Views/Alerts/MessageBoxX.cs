using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.Views.Alerts
{
    public static class MessageBoxX
    {
        public static MessageBoxXAction Show(string message, MessageBoxXType type)
        {
            if (type == MessageBoxXType.DeleteNO)
            {
                PageDeleteNo pageDeleteNo = new PageDeleteNo();
                pageDeleteNo.tbkMessage.Text = message;
                Main.MainWindow.Instace.GetFrameDialog().Content = pageDeleteNo;
                return pageDeleteNo.MessageBoxXAction;
            }

            if (type == MessageBoxXType.YesNo)
            {
                PageYesNo pageYesNo = new PageYesNo();
                pageYesNo.tbkMessage.Text = message;
                Main.MainWindow.Instace.GetFrameDialog().Content = pageYesNo;
                return pageYesNo.MessageBoxXAction;
            }

            throw new NotImplementedException();
        }
    }
}
