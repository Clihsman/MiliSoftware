using MaterialDesignThemes.Wpf;
using MiliSoftware.Customers;
using MiliSoftware.Email;
using MiliSoftware.Inventory;
using MiliSoftware.Setting;
using MiliSoftware.Suppliers;
using MiliSoftware.UI;
using MiliSoftware.Views.Customers;
using MiliSoftware.Views.Debug;
using MiliSoftware.Views.Email;
using MiliSoftware.Views.Export;
using MiliSoftware.Views.Inventory;
using MiliSoftware.Views.Setting;
using MiliSoftware.Views.Suppliers;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Dynamic;
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
        private SettingPage pageSetting = null;
        private PageEmails pageEmails;
        //*******************

        private Dictionary<PackIcon, bool> buttons;

        public PageMain()
        {
            //C:\Users\Clihsman\Downloads\176123.jpg
            //  BitmapImage image = new BitmapImage(new Uri("https://images.alphacoders.com/176/176123.jpg", UriKind.Absolute));
            //  BitmapImage image = new BitmapImage(new Uri("file://C:/Users/Clihsman/Downloads/176123.jpg", UriKind.Absolute));
            BitmapImage image = new BitmapImage(new Uri("https://cdn.pixabay.com/photo/2016/12/16/15/25/christmas-1911637_1280.jpg", UriKind.RelativeOrAbsolute));
            //https://cafetunetole.com/wp-content/uploads/2020/10/porque%CC%81-compramos.jpg
            Background = new ImageBrush(image);

            InitializeComponent();
            LoadLanguaje();
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
                    Dispatcher.Invoke(new Action(()=> {
                        tbDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy").SetUpperPrimaryChars();
                    }));
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
            btCustomers.MouseDown += BtCustomersClick;
            btInventory.MouseDown += BtInventoryClick;
            btSuppliers.MouseDown += BtSuppliersClick;
            btEmail.MouseDown += BtEmailClick;
            btDebug.MouseDown += BtDebugClick;
            btHelp.MouseDown += BtHetpClick;
            btHome.MouseDown += BtHomeClick;
            btSetting.MouseDown += BtSettingClick;
            // Containers
            /*
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
                if (puntero > 0) puntero--;D:\MiliDatos\Visual Studio 2015\Projects\MiliSoftware
                else puntero = musics.Length - 1;
                D:\MiliDatos\Visual Studio 2015\Projects\MiliSoftware
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
            */
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

        private void BtHetpClick(object sender, MouseButtonEventArgs e)
        {
            // parentFrame.Content = new PageUnitOfMeasurement();
            
        }

        private void LoadLanguaje()
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

        private void BtDebugClick(object sender, EventArgs e)
        {
            GetFrame().Content = new PageDebug();
        }

        private void BtInventoryClick(object sender, RoutedEventArgs e)
        {
            CloseFrameDialog();
            pageInventory = new PageInventory();
            iGUI = pageInventory;
            Title = string.Format("{0}@{1}", TITLE, languaje.MainWindow.toolTipInventory);
            InventoryController inventoryController = new InventoryController(pageInventory);
            UpdateButtonChecked((PackIcon)sender);
        }

        private void BtCustomersClick(object sender, RoutedEventArgs e)
        {
            CloseFrameDialog();
            pageCustomers = new PageCustomers(GetFrame());
            iGUI = pageCustomers;
            ClientController clientController = new ClientController(pageCustomers);
            UpdateButtonChecked((PackIcon)sender);
        }

        private void BtSuppliersClick(object sender, RoutedEventArgs e)
        {
            CloseFrameDialog();
            pageSuppliers = new PageSuppliers();
            iGUI = pageSuppliers;
            SuppliersController suppliersController = new SuppliersController(pageSuppliers);
            UpdateButtonChecked((PackIcon)sender);

        }

        private void BtEmailClick(object sender, MouseButtonEventArgs e)
        {
            CloseFrameDialog();
            pageEmails = new PageEmails();
            iGUI = pageEmails;
            EmailsController emailController = new EmailsController(pageEmails);
            UpdateButtonChecked((PackIcon)sender);
        }

        private void BtHomeClick(object sender, MouseButtonEventArgs e)
        {
            CloseFrameDialog();
            UpdateButtonChecked((PackIcon)sender, false);
        }

        private void BtSettingClick(object sender, MouseButtonEventArgs e) {
            CloseFrameDialog();
            pageSetting = new SettingPage();
            SettingController controller = new SettingController(pageSetting);
            pageSetting.SettingController = controller;
            iGUI = pageSetting;
            UpdateButtonChecked((PackIcon)sender);
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
                    pinetwork.Kind = PackIconKind.WifiCheck;
                }));
            }
            else
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    pinetwork.Kind = PackIconKind.WifiOff;
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
                stMedia.Visibility = Visibility.Hidden;
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

        public Frame GetFrameDialog() {   
            foreach (UIElement panel in gdDialog.Children) panel.IsEnabled = false;

            // Se crea un nuevo Frame para el dialogo
            Frame frame = new Frame();
            frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            frame.HorizontalAlignment = HorizontalAlignment.Stretch;
            gdDialog.Children.Add(frame);

            // Se desabilitan los controles para que solo quede habilitado el Dialogo
            bool enable = gdDialog.Children.Count <= 1;
            stMenuBar.IsEnabled = enable;
            stToolBar.IsEnabled = enable;

            return frame;
        }

        public Frame GetFrame()
        {
            // Se eliminan los frames creados
            gdDialog.Children.Clear();
            // Se crea un nuevo Frame
            Frame frame = new Frame();
            frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            frame.HorizontalAlignment = HorizontalAlignment.Stretch;
            gdDialog.Children.Add(frame);
            return frame;
        }

        public void CloseFrameDialog(Frame frame)
        {
            gdDialog.Children.Remove(frame);
            frame.Content = null;
            gdDialog.Children[gdDialog.Children.Count - 1].IsEnabled = true;
            if (gdDialog.Children.Count == 1) {
                stMenuBar.IsEnabled = true;
                stToolBar.IsEnabled = true;
            }
        }

        public void CloseFrameDialog()
        {
            if (gdDialog.Children.Count > 0) gdDialog.Children.RemoveAt(gdDialog.Children.Count - 1);
            if (gdDialog.Children.Count > 0) gdDialog.Children[gdDialog.Children.Count - 1].IsEnabled = true;

            if (gdDialog.Children.Count <= 1)
            {
                stMenuBar.IsEnabled = true;
                stToolBar.IsEnabled = true;
            }
        }

        public void AlertDialog(string msg) {
            alertDialogText.Text = msg;
            ThicknessAnimation animation = new ThicknessAnimation(new Thickness(0, 25, -276, 10), new Thickness(0, 25, 10, 10), TimeSpan.FromMilliseconds(290));
            alertDialog.Visibility = Visibility.Visible;
            animation.Completed += delegate
            {
                this.Invoke(new TimeSpan(0, 0, 0, 2, 500),delegate {
                    animation = new ThicknessAnimation(new Thickness(0, 25, 10, 10), new Thickness(0, 25, -276, 10), TimeSpan.FromMilliseconds(250));
                    animation.Completed += delegate
                    {
                        alertDialog.Visibility = Visibility.Hidden;
                    };
                    alertDialog.BeginAnimation(MarginProperty, animation);
                });
            };
            alertDialog.BeginAnimation(MarginProperty, animation);
        }

        #endregion

    }
}
