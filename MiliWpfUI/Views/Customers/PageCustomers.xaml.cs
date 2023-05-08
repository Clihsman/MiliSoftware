using Microsoft.Win32;
using MiliSoftware.Customers;
using MiliSoftware.UI;
using Newtonsoft.Json;
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

namespace MiliSoftware.Views.Customers
{
    /// <summary>
    /// Lógica de interacción para PageCustomers.xaml
    /// </summary>
    public partial class PageCustomers : Page, IClientGUI
    {
        private Frame parent;
        private dynamic[] customers;

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        public ICRUD<string, object[], string, string> controller { set; get; }
        public DialogResult DialogResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public PageCustomers(Frame parent)
        {
            InitializeComponent();
            this.parent = parent;
            LoadEvents();
            LoadLanguage();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            progBar.Visibility = Visibility.Visible;
            stTools.Visibility = Visibility.Hidden;
            new Thread(new ThreadStart(delegate
            {
                LoadData();
                Thread.Sleep(100);
                Dispatcher.Invoke(() =>
                {
                   // var alert = new Alerts.Alert();
                   /*
                    alert.btnClose.Click += (o, ev) =>
                    {
                        alert = null;
                        Main.MainWindow.Instace.dialogFrame.Content = null;
                        MessageBox.Show("Close");
                    };
                    */
                   // Main.MainWindow.Instace.dialogFrame.Content = alert;       
                    progBar.Visibility = Visibility.Hidden;
                    stTools.Visibility = Visibility.Visible;
                 //   lvCustomers.ItemsSource = customers;
                });
            })).Start();       
        }

        #region Functions

        private void LoadLanguage()
        {
            tbSearch.SetValue(MaterialDesignThemes.Wpf.HintAssist.HintProperty, languaje.PageCustomers.hintTbSearch);
            btSearch.ToolTip = languaje.PageCustomers.toolTipBtSearchProduct;
            btSetting.ToolTip = languaje.PageCustomers.tooTipBtSetting;
            btAssignGroup.ToolTip = languaje.PageCustomers.tooTipBtAssignGroup;
            btUpdate.ToolTip = languaje.PageCustomers.toolTipBtUpdate;
            btExport.ToolTip = languaje.PageCustomers.toolTipBtExport;
            btDelete.ToolTip = languaje.PageCustomers.toolTipBtDelete;
            btEdit.ToolTip = languaje.PageCustomers.toolTipBtEdit;
            btNew.ToolTip = languaje.PageCustomers.toolTipBtNew;

            lvHeaders.Columns.Add(new GridViewColumn() { Header = (string)languaje.PageCustomers.headCode, DisplayMemberBinding = new Binding("Codigo") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = (string)languaje.PageCustomers.headNames, DisplayMemberBinding = new Binding("Nombres") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = (string)languaje.PageCustomers.headSurnames, DisplayMemberBinding = new Binding("Apellidos") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = (string)languaje.PageCustomers.headId, DisplayMemberBinding = new Binding("Identificacion") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = (string)languaje.PageCustomers.headMail, DisplayMemberBinding = new Binding("Correo") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = (string)languaje.PageCustomers.headAddress, Width = 250, DisplayMemberBinding = new Binding("Direccion") });
        }

        private void LoadEvents()
        {
            Loaded += Page_Loaded;
            lvCustomers.SelectionChanged += lvCustomersSelectItem;
            btNew.Click += btNewClick;
            btSearch.Click += btSearchClick;
            btAZ.Click += btAZClick;
            btZA.Click += btZAClick;
            btDelete.Click += btDeleteClick;
            btEdit.Click += btEditClick;
            btExport.Click += btExportClick;
            tbSearch.KeyDown += (o,e)=> { if (e.Key == Key.Enter) btSearchClick(o, e); };
        }

        private void LoadData()
        {
            customers = (DObject[])controller.Read(null);
        }

        #endregion

        #region Codigo Prueba

        private Dictionary<string, object> GetParams(object[] rows)
        {
            Dictionary<string, object> @params = new Dictionary<string, object>();

            for (int i = 0; i < rows.Length; i++)
            {
                @params.Add(string.Format("@{0}", i), rows[i]);
            }

            return @params;
        }

        #endregion

        #region Events

        private void lvCustomersSelectItem(object sender, SelectionChangedEventArgs e)
        {
            bool enable = lvCustomers.SelectedItem != null;
            btEdit.IsEnabled = enable && lvCustomers.SelectedItems.Count == 1;
            btDelete.IsEnabled = enable;
            btAssignGroup.IsEnabled = enable;
        }

        private void btNewClick(object o, EventArgs e)
        {
            PageClient pageClient = new PageClient(Main.MainWindow.Instace.dialogFrame);
            ClientController clientController = new ClientController(pageClient);

            pageClient.Show();
            /*
            DoubleAnimation animation = new DoubleAnimation(1, 0, new Duration(new TimeSpan(0, 0, 0, 0, 200)));

            animation.Completed += delegate {
                PageClient pageClient = new PageClient(Main.MainWindow.Instace.dialogFrame) { Opacity = 0 };
                ClientController clientController = new ClientController(pageClient);

                DoubleAnimation animation2 = new DoubleAnimation(0, 1, new Duration(new TimeSpan(0, 0, 0, 0, 800)));
                pageClient.Show();
                pageClient.BeginAnimation(OpacityProperty, animation2);

            };
            BeginAnimation(OpacityProperty, animation);
            */
        }

        private void btSearchClick(object o, EventArgs e)
        {
            if (tbSearch.Text == "*")
            {
                lvCustomers.ItemsSource = customers;
            }
            else
            {
                string search = tbSearch.Text.ToUpper();
                List<dynamic> achieved = new List<dynamic>();

                foreach (dynamic client in customers)
                {
                    if (
                        client.Identificacion == search ||
                        client.Nombres.ToUpper().Contains(search) ||
                        client.Apellidos.ToUpper().Contains(search) ||
                        (client.Nombres + " " + client.Apellidos).Contains(search)
                        )
                    {
                        achieved.Add(client);
                    }
                }

                lvCustomers.ItemsSource = achieved;
            }
        }

        private void btAZClick(object o, EventArgs e)
        {
            lvCustomers.ItemsSource = customers.OrderBy(i => i.Nombres);
        }

        private void btZAClick(object o, EventArgs e)
        {
            lvCustomers.ItemsSource = customers.OrderByDescending(i => i.Nombres);
        }

        private void btDeleteClick(object o, EventArgs e)
        {
            if (lvCustomers.SelectedItems.Count > 0)
            {
                List<object> list = new List<object>();
                foreach (dynamic client in lvCustomers.SelectedItems)
                {
                    list.Add(new { Id = client.Identificacion });
                }

                string json = JsonConvert.SerializeObject(list.ToArray());

                controller.Delete(json);

                List<dynamic> nCustomers = new List<dynamic>();

                foreach (dynamic client in lvCustomers.ItemsSource)
                {
                    bool @continue = false;
                    foreach (dynamic dt_client in lvCustomers.SelectedItems)
                        if (client.Identificacion == dt_client.Identificacion)
                            @continue = true;

                    if (@continue) continue;

                    nCustomers.Add(client);
                }

                lvCustomers.ItemsSource = nCustomers.ToArray();
            }
        }

        private void btEditClick(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(1, 0, new Duration(new TimeSpan(0, 0, 0, 0, 200)));

            animation.Completed += delegate {
                PageClient pgNewClient = new PageClient(parent) { Opacity = 0 };
                ClientController clientController = new ClientController(pgNewClient);
                pgNewClient.SetValues(new DObject[] { (DObject)lvCustomers.SelectedItem });
                DoubleAnimation animation2 = new DoubleAnimation(0, 1, new Duration(new TimeSpan(0, 0, 0, 0, 800)));
                pgNewClient.SetEditMode();
                pgNewClient.Show();
                pgNewClient.BeginAnimation(OpacityProperty, animation2);

            };
            BeginAnimation(OpacityProperty, animation);
        }

        private void btExportClick(object o, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Libro de Excel (*.xlsx)|*.xlsx";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveFileDialog.ShowDialog() == true)
            {
                SpreadsheetLight.SLDocument sLDocument = new SpreadsheetLight.SLDocument();
                sLDocument.RenameWorksheet(SpreadsheetLight.SLDocument.DefaultFirstSheetName, "Products");


                int id = 1;
                sLDocument.SetCellValue("A" + id++, (string)lvHeaders.Columns[1].Header);
                sLDocument.SetColumnWidth("A" + id, 20);
                foreach (dynamic client in customers)
                {
                    sLDocument.SetCellValue("A" + id++, client.Nombres);
                }
                sLDocument.SaveAs(saveFileDialog.FileName);
            }
          //  sLDocument.SaveAs("document.xlsx");
        }

        #endregion

        #region ClientGUI

        public object[] GetValues()
        {
            return customers;
        }

        public void SetValues(object[] values)
        {
            customers = values;
            lvCustomers.ItemsSource = null;
            lvCustomers.ItemsSource = customers;
        }

        public void Show()
        {
            parent.Content = this;
            OnOpen?.Invoke(this, new EventArgs());
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        public IGUI<string, object[], string, string> GetParent()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            lvCustomers.ItemsSource = null;
            progBar.Visibility = Visibility.Hidden;
            lvHeaders.Columns.Clear();
            GC.SuppressFinalize(customers);
            customers = null;
            OnClosed?.Invoke(this, new EventArgs());
            GC.SuppressFinalize(this);
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        public int GetWidth()
        {
            return (int)Width;
        }

        public int GetHeight()
        {
            return (int)Height;
        }

        public int GetX()
        {
            return (int)Margin.Left;
        }

        public int GetY()
        {
            return (int)Margin.Top;
        }

        public ScreenNumber GetScreen()
        {
            throw new NotImplementedException();
        }

        public string GetTitle()
        {
            return Title;
        }

        public string GetName()
        {
            return Name;
        }

        public GUIState GetGUIState()
        {
            return GUIState.Normal;
        }

        public object GetGUI()
        {
            return this;
        }

        public IntPtr GetHandle()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
