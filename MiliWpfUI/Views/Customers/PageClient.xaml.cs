using MaterialDesignThemes.Wpf;
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
            LoadLanguage();
            /*
            List<string> list = new List<string>();
            list.Add("Cedula de Identidad");

            //cbTipoIdentificacion.ItemsSource = list;

            List<string> paises = new List<string>();

            foreach (System.Globalization.CultureInfo cultureInfo in System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures))
            {
                System.Globalization.RegionInfo regionInfo = new System.Globalization.RegionInfo(cultureInfo.Name);

                if (!paises.Contains(regionInfo.DisplayName))
                    paises.Add(regionInfo.DisplayName);
            }*/

            this.parent = parent;
          //  cbPais.ItemsSource = paises.OrderBy(key => key);
            // SetDecimalTextBox(tbCodigo);
            
            this.parent = parent;
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
            /*
            editMode = true;
            tbCodigo.IsEnabled = false;
            */
        }

        #region Functions

        /// <summary>
        /// En metodo se encarga de cargar el idioma del formulario
        /// </summary>
        private void LoadLanguage()
        {
            // data supplier
            tbCode.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbCode);
            cbCategory.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintCbCategory);
            cbCategory.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintCbCategory);
            cbLineOfBusiness.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintCbLineOfBusiness);
            cbGrup.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintCbGrup);
            tbName.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbName);
            cbType.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintCbType);
            cbDocumentType.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintCbDocumentType);
            tbDocument.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbDocument);
            tbPicture.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbPicture);
            tbDescription.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbDescription);
            chSaveImage.Content = languaje.PageSupplier.contentChSaveImage;
            tbEmail1.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbEmail);
            tbEmail2.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbEmail);
            tbEmail3.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbEmail);
            tbBusinessRegistration.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbBusinessRegistration);
            chTaxIncluded.Content = languaje.PageSupplier.contentChTaxIncluded;
            // Contact 0
            tbCelCode0.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbCelCode);
            tbContact0.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbContact);
            // Contact 1
            tbCelCode1.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbCelCode);
            tbContact1.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbContact);
            // Contact 2
            tbCelCode2.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbCelCode);
            tbContact2.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbContact);
            // Amountry 0
            cbAmountry0.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintCbCountry);
            tbCondition0.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbCondition);
            tbCity0.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbCity);
            tbPostalCode0.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbPostalCode);
            tbDirection0.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbDirection);
            // Amountry 1
            cbAmountry1.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintCbCountry);
            tbCondition1.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbCondition);
            tbCity1.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbCity);
            tbPostalCode1.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbPostalCode);
            tbDirection1.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbDirection);
            // Amountry 2
            cbAmountry2.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintCbCountry);
            tbCondition2.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbCondition);
            tbCity2.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbCity);
            tbPostalCode2.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbPostalCode);
            tbDirection2.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbDirection);
            //Tools
            btAccountingData.ToolTip = languaje.PageSupplier.toolTipBtAccountingData;
            btSave.Content = languaje.PageSupplier.contentBtSave;
            btCancel.Content = languaje.PageSupplier.contentBtCancel;
            //***************************

        }

        #endregion

        #region ClientGUI

        public object[] GetValues()
        {/*
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
            */
            return new object[] { null };
        }

        public void SetValues(object[] value)
        {
            /*
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
            */
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
