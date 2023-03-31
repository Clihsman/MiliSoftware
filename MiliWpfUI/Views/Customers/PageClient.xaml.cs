using MiliSoftware.UI;
using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiliSoftware.Views.Customers
{
    /// <summary>
    /// Lógica de interacción para PageClient.xaml
    /// </summary>
    public partial class PageClient : Page, IClientGUI
    {
        public ICRUD<string, object[], string, string> controller { get; set; }
        public DialogResult DialogResult { get; set; }

        public bool editMode = false;
        private Frame parent;

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        public PageClient(Frame parent)
        {
            InitializeComponent();

            List<string> list = new List<string>();
            list.Add("Cedula de Identidad");

            cbTipoIdentificacion.ItemsSource = list;

            List<string> paises = new List<string>();

            foreach (System.Globalization.CultureInfo cultureInfo in System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures))
            {
                System.Globalization.RegionInfo regionInfo = new System.Globalization.RegionInfo(cultureInfo.Name);

                if (!paises.Contains(regionInfo.DisplayName))
                    paises.Add(regionInfo.DisplayName);
            }

            this.parent = parent;
            cbPais.ItemsSource = paises.OrderBy(key => key);
            // SetDecimalTextBox(tbCodigo);
        }

        public static void SetDecimalTextBox(TextBox texbox)
        {
            texbox.KeyDown += delegate (object o, KeyEventArgs e)
            {
                if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab)
                    e.Handled = false;
                else
                    e.Handled = true;
            };

            texbox.TextChanged += delegate
            {
                if (!string.IsNullOrWhiteSpace(texbox.Text))
                {
                    double value = double.Parse(texbox.Text.Replace(".", "").Replace(",", ""));
                    texbox.Text = GetTotal(value);
                    texbox.Select(texbox.Text.Length, 0);
                }
                else
                {
                    texbox.Text = "0";
                    texbox.Select(texbox.Text.Length, 0);
                }
            };
        }

        public static string GetTotal(double total)
        {
            string tlString = total.ToString();
            string result = "";

            string currentTotal = "";
            int id = -1;
            for (int i = tlString.Length - 1; i > -1; i--)
            {
                if (id < 2)
                    id++;
                else
                {
                    currentTotal += ".";
                    id = 0;
                }

                currentTotal += tlString[i];
            }

            for (int i = currentTotal.Length - 1; i > -1; i--)
            {
                result += currentTotal[i];
            }

            return result;
        }

        public static void SetIntTextBox(TextBox texbox)
        {
            texbox.KeyDown += delegate (object o, KeyEventArgs e)
            {

                if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab)
                    e.Handled = false;
                else
                    e.Handled = true;
            };

            texbox.TextChanged += delegate
            {
                if (string.IsNullOrWhiteSpace(texbox.Text))
                {
                    texbox.Text = "0";
                    texbox.Select(texbox.Text.Length, 0);
                }
                else if (texbox.Text.StartsWith("0"))
                {
                    try
                    {
                        texbox.Text = int.Parse(texbox.Text).ToString();
                        texbox.Select(texbox.Text.Length, 0);
                    }
                    catch { }
                }
            };

            texbox.Text = "0";
        }

        private void BtGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (editMode)
            {
                string json = JsonConvert.SerializeObject(GetValues());
                controller.Update(json);
            }
            else
            {
                string json = JsonConvert.SerializeObject(GetValues());
                controller.Create(json);
            }
        }

        public void SetEditMode()
        {
            editMode = true;
            tbCodigo.IsEnabled = false;
        }

        #region ClientGUI

        public object[] GetValues()
        {
            string codigo = tbCodigo.Text;
            string tipo = cbTipo.Text;
            string nombres = tbNombres.Text;
            string apellidos = tbApellidos.Text;
            string correo = tbCorreo.Text;
            string tipoIdentificacion = cbTipoIdentificacion.Text;
            string identificacion = tbIdentificacion.Text;
            string foto = tbFoto.Text;
            string sucursal = tbSucursal.Text;
            string pais = cbPais.Text;
            string departamento = tbDepartamento.Text;
            string ciudad = tbCiudad.Text;
            string codigoPostal = tbCodigoPostal.Text;
            string direccion = tbDireccion.Text;

            object client = new Dictionary<string, object>()
            {
                { "Codigo" , codigo },
                { "Tipo", tipo },
                { "Nombres", nombres },
                { "Apellidos", apellidos },
                { "Correo" , correo },
                { "TipoIdentificacion" , tipoIdentificacion },
                { "Identificacion", identificacion },
                { "Sucursal" , sucursal },
                {"Pais" , pais },
                { "Departamento" , departamento },
                { "Ciudad" , ciudad },
                { "CodigoPostal" , codigoPostal },
                { "Direccion" , direccion }
            };

            return new object[] { client };
        }

        public void SetValues(object[] value)
        {
            dynamic client = null;

            if (value is DObject[] && value.Length > 0)
                client = (DObject)value[0];
            else if (value is Dictionary<string, object>[] && value.Length > 0)
                client = new DObject((Dictionary<string, object>)value[0]);

            if (client != null)
            {
                tbCodigo.Text = client.Codigo;
                cbTipo.Text = client.Tipo;
                tbNombres.Text = client.Nombres;
                Console.WriteLine(client.Nombres);
                tbApellidos.Text = client.Apellidos;
                tbCorreo.Text = client.Correo;
                cbTipoIdentificacion.Text = client.TipoIdentificacion;
                tbIdentificacion.Text = client.Identificacion;
                //      tbFoto.Text = client.Foto;
                tbSucursal.Text = client.Sucursal;
                cbPais.Text = client.Pais;
                tbDepartamento.Text = client.Departamento;
                tbCiudad.Text = client.Ciudad;
                tbCodigoPostal.Text = client.CodigoPostal;
                tbDireccion.Text = client.Direccion;
            }
        }

        public void Show()
        {
            parent.NavigationService.Navigate(null);
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
