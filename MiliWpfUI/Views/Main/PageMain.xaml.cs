using MaterialDesignThemes.Wpf;
using MiliSoftware.Customers;
using MiliSoftware.Email;
using MiliSoftware.Inventory;
using MiliSoftware.Suppliers;
using MiliSoftware.Views.Customers;
using MiliSoftware.Views.Debug;
using MiliSoftware.Views.Email;
using MiliSoftware.Views.Inventory;
using MiliSoftware.Views.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiliSoftware.Views.Main
{
    /// <summary>
    /// Lógica de interacción para PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        private Thread thread;
        public static MainWindow Instace { private set; get; }

        private const string TITLE = "MiliSoftware";

        // pages
        private PageInventory pageInventory = null;
        private PageCustomers pageCustomers = null;
        private PageSuppliers pageSuppliers = null;
        private PageEmails pageEmails;
        //*******************

        private Dictionary<PackIcon, bool> buttons;

        public PageMain()
        {
            InitializeComponent();
            loadLanguaje();
            LoadEvents();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            buttons = new Dictionary<PackIcon, bool>() {
                {btCustomers, false},
                {btSuppliers, false},
                {btInventory, false},
                {btReports, false},
                {btShopping, false},
                {btSales, false},
                {btAccounting, false},
                {btSetting, false},
                {btHelp, false},
                {btAccount, false},
                {btEmail, false},
                {btDebug, false},
            };

            thread = new Thread(new ThreadStart(delegate {
                while (MainWindow.Instace != null)
                {
                    IsNetworkAvailable();
                    Thread.Sleep(5000);
                }
            }));

            Uri resourceUri = new Uri("/MiliSoftware;component/Resources/Logo.png", UriKind.Relative);
            imgIcon.Source = new BitmapImage(resourceUri);

            thread.Start();
        }

        private void LoadEvents()
        {
            Loaded += Window_Loaded;
            btCustomers.MouseDown += btCustomersClick;
            btInventory.MouseDown += btInventoryClick;
            btSuppliers.MouseDown += btSuppliersClick;
            btEmail.MouseDown += btEmailClick;
            btDebug.MouseDown += btDebugClick;
            btHelp.MouseDown += btHetpClick;
            btHome.MouseDown += btHomeClick;

            foreach (Control control in stMenuBar.Children)
            {
                control.MouseEnter += PackIcon_MouseEnter;
                control.MouseLeave += PackIcon_MouseLeave;
            }

            foreach (Control control in stToolBar.Children)
            {
                control.MouseEnter += PackIcon_MouseEnter;
                control.MouseLeave += PackIcon_MouseLeave;
            }
        }

        private void btHetpClick(object sender, MouseButtonEventArgs e)
        {
        }

        private void loadLanguaje()
        {
            pbTasks.ToolTip = languaje.MainWindow.toolTipTask;
            btAccount.ToolTip = languaje.MainWindow.toolTipAccount;
            btHelp.ToolTip = languaje.MainWindow.toolTipHelp;
            btSetting.ToolTip = languaje.MainWindow.toolTipSetting;
            btCustomers.ToolTip = languaje.MainWindow.toolTipCustomers;
            btInventory.ToolTip = languaje.MainWindow.toolTipInventory;
            btSuppliers.ToolTip = languaje.MainWindow.toolTipSuppliers;
            btReports.ToolTip = languaje.MainWindow.toolTipReports;
            btShopping.ToolTip = languaje.MainWindow.toolTipShopping;
            btSales.ToolTip = languaje.MainWindow.toolTipSales;
            btAccounting.ToolTip = languaje.MainWindow.toolTipAccounting;
        }

        #region Events

        private void btDebugClick(object sender, EventArgs e)
        {
            parentFrame.Content = new PageDebug();
        }

        private void btInventoryClick(object sender, RoutedEventArgs e)
        {
            pageInventory = new PageInventory(parentFrame);
            Title = string.Format("{0}@{1}", TITLE, languaje.MainWindow.toolTipInventory);
            InventoryController inventoryController = new InventoryController(pageInventory);
            UpdateButtonChecked((PackIcon)sender);
        }

        private void btCustomersClick(object sender, RoutedEventArgs e)
        {
            if (pageCustomers != null)
            {
                pageCustomers.Close();
                pageCustomers = null;
            }

            pageCustomers = new PageCustomers(parentFrame);
            ClientController clientController = new ClientController(pageCustomers);
            UpdateButtonChecked((PackIcon)sender);
        }

        private void btSuppliersClick(object sender, RoutedEventArgs e)
        {
            if (pageSuppliers != null)
            {
                pageSuppliers.Close();
                pageSuppliers = null;
            }

            pageSuppliers = new PageSuppliers(parentFrame);
            SuppliersController suppliersController = new SuppliersController(pageSuppliers);
            UpdateButtonChecked((PackIcon)sender);

        }

        private void btEmailClick(object sender, MouseButtonEventArgs e)
        {
            if (pageEmails != null)
            {
                pageEmails.Close();
                pageEmails = null;
            }

            pageEmails = new PageEmails(parentFrame);
            EmailsController emailController = new EmailsController(pageEmails);
            UpdateButtonChecked((PackIcon)sender);
        }

        private void btHomeClick(object sender, MouseButtonEventArgs e)
        {
            parentFrame.Content = null;
            dialogFrame.Content = null;
            dialogFrame1.Content = null;

            UpdateButtonChecked((PackIcon)sender, false);
        }

        private void PackIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                if (!ButtonIsChecked((PackIcon)sender))
                {
                    ((Control)sender).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#345ceb"));
                }

            }
            catch { }
           // ((Control)sender).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#345ceb"));
        }

        private void PackIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                if (!ButtonIsChecked((PackIcon)sender))
                {
                    ((Control)sender).Foreground = Brushes.Black;
                }
            }
            catch { }
          //  ((Control)sender).Foreground = Brushes.Black;
        }

        #endregion

        #region Funtions

        public bool IsConnectedToInternet()
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
            else
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    pinetwork.Kind = MaterialDesignThemes.Wpf.PackIconKind.WifiOff;
                }));
            }
        }

        private bool ButtonIsChecked(PackIcon button)
        {
            return buttons[button];
        }

        private void UpdateButtonChecked(PackIcon button, bool showSubOptions = true)
        {
            var keys = buttons.Keys.ToArray();
            foreach (var sender in keys)
            {
                if (sender == button)
                    buttons[button] = true;
                else
                {
                    buttons[sender] = false;
                    PackIcon_MouseLeave(sender, null);
                }
            }

            if (showSubOptions)
            {
                stSubOptions.Visibility = Visibility.Visible;
                stDate.Visibility = Visibility.Hidden;
                stTools.Visibility = Visibility.Hidden;
                DoubleAnimation animation = new DoubleAnimation(0, 250, new Duration(new TimeSpan(0, 0, 0, 0, 650)));

                animation.Completed += delegate
                {
                };
                tnElement.BeginAnimation(WidthProperty, animation);
            }
            else
            {
                stSubOptions.Visibility = Visibility.Hidden;
                stDate.Visibility = Visibility.Visible;
                stTools.Visibility = Visibility.Visible;
            }
        }

        #endregion

    }
}
