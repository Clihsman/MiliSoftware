using MiliEngine.SqlLite;
using MiliSoftware.Customers;
using MiliSoftware.Model.WebServices;
using MiliSoftware.Paginas.Inventory;
using MiliSoftware.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

namespace MiliSoftware.Paginas
{
    /// <summary>
    /// Lógica de interacción para PgClientes.xaml
    /// </summary>
    public partial class PgClientes : Page, IClientGUI
    {
        private Frame parent;
        private dynamic[] customers;

        public ICRUD<string, object[], string, string> controller { set; get; }

        public PgClientes(Frame parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            progBar.Visibility = Visibility.Visible;
            stTools.Visibility = Visibility.Hidden;
            LoadEvents();

            new Thread(new ThreadStart(delegate
            {
                CargarNube();
                //Thread.Sleep(8000);
                Dispatcher.Invoke(new Action(delegate
                {
                    lvHeaders.Columns.Add(new GridViewColumn() { Header = "Código", DisplayMemberBinding = new Binding("Codigo") });
                    lvHeaders.Columns.Add(new GridViewColumn() { Header = "Nombres", DisplayMemberBinding = new Binding("Nombres") });
                    lvHeaders.Columns.Add(new GridViewColumn() { Header = "Apellidos", DisplayMemberBinding = new Binding("Apellidos") });
                    lvHeaders.Columns.Add(new GridViewColumn() { Header = "Identificación", DisplayMemberBinding = new Binding("Identificacion") });
                    //   lvHeaders.Columns.Add(new GridViewColumn() { Header = "Teléfono", DisplayMemberBinding = new Binding("Telefono") });
                    lvHeaders.Columns.Add(new GridViewColumn() { Header = "Correo", DisplayMemberBinding = new Binding("Correo") });
                    lvHeaders.Columns.Add(new GridViewColumn() { Header = "Dirección", Width = 250, DisplayMemberBinding = new Binding("Direccion") });
                    lvCustomers.ItemsSource = customers;
                    progBar.Visibility = Visibility.Hidden;
                    stTools.Visibility = Visibility.Visible;
                }));
            })).Start();

        }

        #region Codigo Prueba

        private void CargarNube() {

            //      WebService.InitServices("", "");
            //    string json = WebService.GetString("apirest/clientes");
            customers = (DObject[])controller.Read(null);

        }

        private void CargarLocal() {
            SqlLiteDatabase sqlLiteDatabase = new SqlLiteDatabase();
            sqlLiteDatabase.Open();

            Dictionary<string, object>[] clientes = sqlLiteDatabase.ExecuteQueryD("SELECT * FROM clientes");

            customers = DObject.GetArray(clientes);
            sqlLiteDatabase.Close();
        }

        private void Sincronizar(Dictionary<string, object>[] clientes) {
            SqlLiteDatabase sqlLiteDatabase = new SqlLiteDatabase();
            sqlLiteDatabase.Open();

            string command = "INSERT INTO \"clientes\" (\"Id\", \"Codigo\", \"Tipo\", \"Nombres\", \"Apellidos\", \"Correo\", \"TipoIdentificacion\", \"Identificacion\", \"Sucursal\", \"Pais\", \"Departamento\", \"Ciudad\", \"CodigoPostal\", \"Direccion\") VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13)";

            foreach (Dictionary<string, object> client in clientes)
            {

                sqlLiteDatabase.ExecuteNomQuery(command, GetParams(client.Values.ToArray()));
            }

            sqlLiteDatabase.Close();
        }

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

        private void LoadEvents()
        {
            lvCustomers.SelectionChanged += lvCustomersSelectItem;
            btNew.Click += btNewClick;
            btSearch.Click += btSearchClick;
            btAZ.Click += btAZClick;
            btZA.Click += btZAClick;
            btDelete.Click += btDeleteClick;
            btEdit.Click += btEditClick;
        }

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
            DoubleAnimation animation = new DoubleAnimation(1, 0, new Duration(new TimeSpan(0, 0, 0, 0, 200)));

            animation.Completed += delegate {
                PgInventory pgNewClient = new PgInventory(parent) { Opacity = 0 };
                ClientController clientController = new ClientController(pgNewClient);

                DoubleAnimation animation2 = new DoubleAnimation(0, 1, new Duration(new TimeSpan(0, 0, 0, 0, 800)));
                pgNewClient.Show();
                pgNewClient.BeginAnimation(OpacityProperty, animation2);

            };
            BeginAnimation(OpacityProperty, animation);
        }

        private void btSearchClick(object o, EventArgs e) {
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

                foreach (dynamic client in lvCustomers.ItemsSource) {
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
                PgInventory pgNewClient = new PgInventory(parent) { Opacity = 0 };
                ClientController clientController = new ClientController(pgNewClient);
                pgNewClient.SetValues(new DObject[] {(DObject)lvCustomers.SelectedItem});
                DoubleAnimation animation2 = new DoubleAnimation(0, 1, new Duration(new TimeSpan(0, 0, 0,0, 800)));
                pgNewClient.SetEditMode();
                pgNewClient.Show();
                pgNewClient.BeginAnimation(OpacityProperty, animation2);

            };
            BeginAnimation(OpacityProperty, animation);
        }

        #endregion

        #region Funciones

        private bool ForeachIf<T>(IEnumerable<T> enumerable, Func<T, bool> action)
        {
            foreach (T value in enumerable)
            {
                if (action(value))
                    return true;
            }
            return false;
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
            parent.NavigationService.Navigate(this);
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
            throw new NotImplementedException();
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
