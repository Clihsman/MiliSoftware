using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xwt;
namespace MiliGtkGUI
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.Initialize(ToolkitType.Gtk);
            var mainWindow = new Window()
            {
                Title = "Xwt Demo Application",
            };

          //  mainWindow.

            mainWindow.Closed += delegate {
                Application.Exit();
            };

            mainWindow.InitialLocation = WindowLocation.CenterScreen;
            

            Button button = new Button();
            mainWindow.Content = button;

            mainWindow.Show();
            Application.Run();
            mainWindow.Dispose();
        }
    }
}
