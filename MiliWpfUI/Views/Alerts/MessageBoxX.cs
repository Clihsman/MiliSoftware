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

            if (type == MessageBoxXType.Error500)
            {
                Page500Error page500Error = new Page500Error();
               // page500Error.tbkMessage.Text = message;
                Main.MainWindow.Instace.GetFrameDialog().Content = page500Error;
                return page500Error.MessageBoxXAction;
            }

            if (type == MessageBoxXType.Error404)
            {
                Page404Error page404Error = new Page404Error();
                // page500Error.tbkMessage.Text = message;
                Main.MainWindow.Instace.GetFrameDialog().Content = page404Error;
                return page404Error.MessageBoxXAction;
            }

            if (type == MessageBoxXType.Error503)
            {
                PageCloudError page503Error = new PageCloudError();
                // page500Error.tbkMessage.Text = message;
                Main.MainWindow.Instace.GetFrameDialog().Content = page503Error;
                return page503Error.MessageBoxXAction;
            }

            throw new NotImplementedException();
        }
    }
}
