using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MiliSoftware.Paginas.Inventory;
using MiliSoftware.Paginas;
using System.Windows.Threading;
using System.Threading;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.Net;
using MiliSoftware.Customers;

namespace MiliSoftware.Forms
{
    /// <summary>
    /// Lógica de interacción para FormPrincipal.xaml
    /// </summary>
    public partial class FormPrincipal : Window
    {
        private Thread thread;

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          //  myFrame.NavigationService.Navigate(new PgClientes(myFrame));
            //   myFrame.NavigationService.Navigate(new PgInventory());
            //  setScreenSize();

            thread = new Thread(new ThreadStart(delegate {
                while (true)
                {
                    IsNetworkAvailable();
                    Thread.Sleep(5000);
                }
            }));
                thread.Start();
            loadLanguaje();
        }

        private void loadLanguaje() {
            pbTasks.ToolTip = languaje.MainWindow.toolTipTask;
            btAccount.ToolTip = languaje.MainWindow.toolTipAccount;
            btHelp.ToolTip = languaje.MainWindow.toolTipHelp;
            btSetting.ToolTip = languaje.MainWindow.toolTipSetting;
        }

        private void PackIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Control)sender).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#345ceb"));
        }

        private void PackIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Control)sender).Foreground = Brushes.Black;
        }

        public static bool IsConnectedToInternet()
        {
            dynamic networkListManager = Activator.CreateInstance(
            Type.GetTypeFromCLSID(new Guid("{DCB00C01-570F-4A9B-8D69-199FDBA5723B}")));

            bool isConnected = networkListManager.IsConnectedToInternet;
            return isConnected;
        }

        private void IsNetworkAvailable()
        {
            if (IsConnectedToInternet())
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    pinetwork.Kind = MaterialDesignThemes.Wpf.PackIconKind.WifiCheck;
                }));
            }
            else {
                Dispatcher.Invoke(new Action(delegate
                {
                    pinetwork.Kind = MaterialDesignThemes.Wpf.PackIconKind.WifiOff;
                }));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            thread.Abort();
        }

        private void BtCustomers_Click(object sender, RoutedEventArgs e)
        {
            PgClientes pgCustomers = new PgClientes(myFrame);
            ClientController clientController = new ClientController(pgCustomers);
        }
    }
}
