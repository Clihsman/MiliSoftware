using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MiliSoftware
{
    public static class Extencion
    {
        public static void Invoke(this Page page, Action action)
        {
            page.Dispatcher.Invoke(action);
        }

        public static void Invoke(this Page page, Action action, params object[] args)
        {
            page.Dispatcher.Invoke(action, args);
        }

        public static void Invoke(this Page page,TimeSpan time, Action action)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(delegate {
                page.Dispatcher.Invoke(action);
                dispatcherTimer.Stop();
            });
            dispatcherTimer.Interval = time;
            dispatcherTimer.Start();
        }

        public static void Invoke(this Page page, TimeSpan time, Action action, params object[] args)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(delegate {
                page.Dispatcher.Invoke(action, args);
                dispatcherTimer.Stop();
            });
            dispatcherTimer.Interval = time;
            dispatcherTimer.Start();
        }
    }
}
