using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MiliWpfUI
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex _instanceMutex = null;

        protected override void OnStartup(StartupEventArgs e)
        {
          ///  MessageBox.Show(System.Threading.Thread.CurrentThread.m);

            // check that there is only one instance of the control panel running...
            bool createdNew;
            _instanceMutex = new Mutex(true, @"Global\ControlPanel", out createdNew);
            if (!createdNew)
            {
                var prc = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
                if (prc.Length > 0)
                {
                    BringProcessToFront(prc[0]);
                }

                _instanceMutex = null;
                Current.Shutdown();
                return;
            }

            base.OnStartup(e);
        }
        public static void BringProcessToFront(Process process)
        {
            SwitchToThisWindow(process.MainWindowHandle, true);
        }

        const int SW_RESTORE = 9;

        [DllImport("User32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        protected override void OnExit(ExitEventArgs e)
        {
            if (_instanceMutex != null)
                _instanceMutex.ReleaseMutex();
            base.OnExit(e);
        }
    }
}
