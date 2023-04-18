using MaterialDesignThemes.Wpf;
using MiliSoftware.Customers;
using MiliSoftware.Email;
using MiliSoftware.Inventory;
using MiliSoftware.Suppliers;
using MiliSoftware.UI;
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
        public static PageMain Instace { private set; get; }

        private const string TITLE = "MiliSoftware";

        // pages
        private IGUIEvents iGUI;
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

            // Containers

            int puntero = 0;
            bool playSound = false;
            string[] musics = System.IO.Directory.GetFiles(@"F:\antiguop\Nueva carpeta (2)","*.mp3");

            meMedia.Source = new Uri(musics[puntero]);

            mcBtNext.MouseDown += (o, e) => {
                if (puntero < musics.Length) puntero++;
                else puntero = 0;

                if(System.IO.File.Exists(musics[puntero]))
                    mcTbTitle.Text = System.IO.Path.GetFileName(musics[puntero]);

                meMedia.Source = new Uri(musics[puntero]);
            };

            mcBtPrevious.MouseDown += (o, e) => {
                if (puntero > 0) puntero--;
                else puntero = musics.Length - 1;

                if (System.IO.File.Exists(musics[puntero]))
                    mcTbTitle.Text = System.IO.Path.GetFileName(musics[puntero]);

                meMedia.Source = new Uri(musics[puntero]);
            };

            meMedia.MediaOpened += (o, e) => {
                mePgMedia.Maximum = TimeSpan.FromSeconds(meMedia.NaturalDuration.TimeSpan.TotalSeconds).TotalSeconds;
            };

            mcBtPausePlay.MouseDown += (o,e) => {
                if (mcBtPausePlay.Kind == PackIconKind.Play)
                {
                    playSound = true;
                    Task.Run(() => {
                        while (playSound)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                mePgMedia.Value = meMedia.Position.TotalSeconds;
                            });
                            Thread.Sleep(50);
                        }
                    });

                    meMedia.Play();
                    mcBtPausePlay.Kind = PackIconKind.Pause;

                    mcTbTitle.Text = System.IO.Path.GetFileName(musics[puntero]);

                    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(1, 1);
                    System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
                    var width = graphics.MeasureString(mcTbTitle.Text, new System.Drawing.Font(mcTbTitle.FontFamily.Source, (float)mcTbTitle.FontSize)).Width / 2;
                    
                    ThicknessAnimation animation = new ThicknessAnimation(
                        new Thickness(-(width + 90), 0, 0, 0),
                        new Thickness(width - 50, 0, 0, 0),
                        new Duration(new TimeSpan(0, 0, 0, 20, 0)));
                    
                    animation.Completed += delegate
                    {
                        mcTbTitle.BeginAnimation(MarginProperty, animation);
                    };
                    mcTbTitle.BeginAnimation(MarginProperty, animation);
                }
                else
                {
                    playSound = false;
                    meMedia.Pause();
                    mcBtPausePlay.Kind = PackIconKind.Play;
                }
            };

            //***********

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
            parentFrame.Content = new PageUnitOfMeasurement();
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
            if (iGUI != null)
            {
                iGUI.Close();
                iGUI = null;
            }

            pageInventory = new PageInventory(parentFrame);
            iGUI = pageInventory;
            Title = string.Format("{0}@{1}", TITLE, languaje.MainWindow.toolTipInventory);
            InventoryController inventoryController = new InventoryController(pageInventory);
            UpdateButtonChecked((PackIcon)sender);
        }

        private void btCustomersClick(object sender, RoutedEventArgs e)
        {
            if (iGUI != null)
            {
                iGUI.Close();
                iGUI = null;
            }

            pageCustomers = new PageCustomers(parentFrame);
            iGUI = pageCustomers;
            ClientController clientController = new ClientController(pageCustomers);
            UpdateButtonChecked((PackIcon)sender);
        }

        private void btSuppliersClick(object sender, RoutedEventArgs e)
        {
            if (iGUI != null)
            {
                iGUI.Close();
                iGUI = null;
            }

            pageSuppliers = new PageSuppliers(parentFrame);
            iGUI = pageSuppliers;
            SuppliersController suppliersController = new SuppliersController(pageSuppliers);
            UpdateButtonChecked((PackIcon)sender);

        }

        private void btEmailClick(object sender, MouseButtonEventArgs e)
        {
            if (iGUI != null)
            {
                iGUI.Close();
                iGUI = null;
            }

            pageEmails = new PageEmails(parentFrame);
            iGUI = pageEmails;
            EmailsController emailController = new EmailsController(pageEmails);
            UpdateButtonChecked((PackIcon)sender);
        }

        private void btHomeClick(object sender, MouseButtonEventArgs e)
        {
            if (iGUI != null)
            {
                iGUI.Close();
                iGUI = null;
            }

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
