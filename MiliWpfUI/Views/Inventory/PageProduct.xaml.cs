/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 23/12/2022          *
 * Assembly : MiliWpfUI                *
 * *************************************/

using CefSharp.Callback;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using GrapInterCom.Interfaces.Inventory;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using MiliSoftware.Inventory;
using MiliSoftware.UI;
using MiliSoftware.Views.Alerts;
using MiliSoftware.Views.Inventory.InventoryGroup;
using MiliSoftware.Views.Inventory.InventoryStore;
using MiliSoftware.Views.Main;
using MiliSoftware.WpfUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using InvGroup = MiliSoftware.InventoryGroup;

namespace MiliSoftware.Views.Inventory
{
    /// <summary>
    /// Lógica de interacción para PageProduct.xaml
    /// </summary>
    public partial class PageProduct : Page, IProductGUI<Product,string,string,string>
    {
        public DialogResult DialogResult { get; set; }
        public ICRUD<Product, string, string, string> controller { get => Controller; set { Controller = (ProductController)value; } }
        private ProductController Controller;

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        private ProductComponent[] components;
        private EquivalentProduct[] equivalents;

        public PageProduct()
        {
            InitializeComponent();
            LoadLanguage();
            LoadExternalControls();
            LoadEvents();
        }

        #region Functions

        public void SetProductEdit(Product product) { 
            tbCode.Text = product.Code;
            components = product.ProductComponents;
        }

        /// <summary>
        /// En este metodo se carga el idioma de la interfaz
        /// </summary>
        private void LoadLanguage()
        {
            tbCode.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbCode);
            cbType.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintCbType);
            cbType.ItemsSource = languaje.PageProduct.cbTypesItems;
            cbGroup.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintCbGroup);
            tbName.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbName);
            tbBarcode.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbBarcode);
            cbUnitOfMeasurement.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintCbUnitOfMeasurement);
            cbUnitOfMeasurement.ItemsSource = languaje.PageProduct.itemsCbUnitOfMeasurement;
            cbTaxClassification.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintCbTaxClassification);
            cbTaxClassification.ItemsSource = languaje.PageProduct.itensCbTaxClassification;
            cbStore.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbStore);
            tbPicture.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbPicture);
            tbDescription.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbDescription);
            chSaveImage.Content = languaje.PageProduct.contentChSaveImage;
            // key value group
            tbKey0.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbKey);
            tbValue0.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbValue);
            tbKey1.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbKey);
            tbValue1.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbValue);
            tbKey2.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbKey);
            tbValue2.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbValue);
            tbKey3.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbKey);
            tbValue3.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbValue);
            tbKey4.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbKey);
            tbValue4.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbValue);
            tbKey5.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbKey);
            tbValue5.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbValue);
            //***************************

            // data supplier
            chSupplier0.ToolTip = languaje.PageProduct.toolTipCbSupplier;
            tbNameSupplier0.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbNameSupplier);
            tbPurchaseValue0.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbPurchaseValue);
            cbVAT0.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintCbVAT);
            tbPurchaseValueWithVAT0.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbPurchaseValueWithVAT);
            tbWinPercentage0.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbtbWinPercentage);
            tbSaleValue0.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbSaleValue);
            chVatIncludedInTheSaleValue0.Content = languaje.PageProduct.contentChVatIncludedInTheSaleValue;

            tbWinPercentage0_1.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbtbWinPercentage);
            tbSaleValue0_1.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbSaleValue);

            tbWinPercentage0_2.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbtbWinPercentage);
            tbSaleValue0_2.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbSaleValue);

            tbWinPercentage0_3.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbtbWinPercentage);
            tbSaleValue0_3.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbSaleValue);

            tbCantMin0.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbCantMin);

            cbInvoiceWithoutExistence0.Content = languaje.PageProduct.contentCbInvoiceWithoutExistence;
            chVatIncludedInTheSaleValue0.Content = languaje.PageProduct.contentChVatIncludedInTheSaleValue;
            //***************************

            // data supplier
            chSupplier1.ToolTip = languaje.PageProduct.toolTipCbSupplier;
            tbNameSupplier1.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbNameSupplier);
            tbPurchaseValue1.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbPurchaseValue);
            cbVAT1.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintCbVAT);
            tbPurchaseValueWithVAT1.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbPurchaseValueWithVAT);
            tbWinPercentage1.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbtbWinPercentage);
            tbSaleValue1.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbSaleValue);
            chVatIncludedInTheSaleValue1.Content = languaje.PageProduct.contentChVatIncludedInTheSaleValue;

            tbWinPercentage1_1.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbtbWinPercentage);
            tbSaleValue1_1.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbSaleValue);

            tbWinPercentage1_2.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbtbWinPercentage);
            tbSaleValue1_2.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbSaleValue);

            tbWinPercentage1_3.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbtbWinPercentage);
            tbSaleValue1_3.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbSaleValue);

            tbCantMin1.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbCantMin);

            cbInvoiceWithoutExistence1.Content = languaje.PageProduct.contentCbInvoiceWithoutExistence;
            chVatIncludedInTheSaleValue1.Content = languaje.PageProduct.contentChVatIncludedInTheSaleValue;
            //***************************

            btSave.Content = languaje.PageProduct.contentBtSave;
            btCancel.Content = languaje.PageProduct.contentBtCancel;
        }

        /// <summary>
        /// Carga controles externos
        /// </summary>
        private void LoadExternalControls()
        {
            // controls supplier
            MoneyTextBox dtPurchaseValue0 = MoneyTextBox.GenerateMoneyTextBox(tbPurchaseValue0);

            PercentageTextBox dtWinPercentage0 = new PercentageTextBox(tbWinPercentage0);
            MoneyTextBox dtSaleValue0 = MoneyTextBox.GenerateMoneyTextBox(tbSaleValue0);

            PercentageTextBox dtWinPercentage0_1 = new PercentageTextBox(tbWinPercentage0_1);
            MoneyTextBox dtSaleValue0_1 = MoneyTextBox.GenerateMoneyTextBox(tbSaleValue0_1);

            PercentageTextBox dtWinPercentage0_2 = new PercentageTextBox(tbWinPercentage0_2);
            MoneyTextBox dtSaleValue0_2 = MoneyTextBox.GenerateMoneyTextBox(tbSaleValue0_2);

            PercentageTextBox dtWinPercentage0_3 = new PercentageTextBox(tbWinPercentage0_3);
            MoneyTextBox dtSaleValue0_3 = MoneyTextBox.GenerateMoneyTextBox(tbSaleValue0_3);

            IntTextBox itCantMin0 = new IntTextBox(tbCantMin0);
            //***************************

            // controls supplier
            MoneyTextBox dtPurchaseValue1 = MoneyTextBox.GenerateMoneyTextBox(tbPurchaseValue1);

            PercentageTextBox dtWinPercentage1 = new PercentageTextBox(tbWinPercentage1);
            MoneyTextBox dtSaleValue1 = MoneyTextBox.GenerateMoneyTextBox(tbSaleValue1);

            PercentageTextBox dtWinPercentage1_1 = new PercentageTextBox(tbWinPercentage1_1);
            MoneyTextBox dtSaleValue1_1 = MoneyTextBox.GenerateMoneyTextBox(tbSaleValue1_1);

            PercentageTextBox dtWinPercentage1_2 = new PercentageTextBox(tbWinPercentage1_2);
            MoneyTextBox dtSaleValue1_2 = MoneyTextBox.GenerateMoneyTextBox(tbSaleValue1_2);

            PercentageTextBox dtWinPercentage1_3 = new PercentageTextBox(tbWinPercentage1_3);
            MoneyTextBox dtSaleValue1_3 = MoneyTextBox.GenerateMoneyTextBox(tbSaleValue1_3);

            IntTextBox itCantMin1 = new IntTextBox(tbCantMin1);
            //***************************

            Action<TextBox, MoneyTextBox, PercentageTextBox, TextBox> method =
                delegate (TextBox textBox, MoneyTextBox purchase, PercentageTextBox winPercentage, TextBox sale)
            {
                if (!textBox.IsFocused) return;
                decimal valor = purchase.Value();
                decimal porcentaje = winPercentage.Value();
                decimal total = valor + (valor * (porcentaje / 100.0m));
                sale.Text = total.ToString("N2");
            };

            Action<TextBox, MoneyTextBox, TextBox, MoneyTextBox> method2 =
                delegate (TextBox textBox, MoneyTextBox purchase, TextBox winPercentage, MoneyTextBox sale)
                {
                    if (!textBox.IsFocused) return;
                    decimal value = purchase.Value();
                    decimal saveValue = sale.Value() - value;
                    saveValue *= 100.0M;
                    decimal percenta = saveValue / value;
                    winPercentage.Text = percenta.ToString("N3");
                };

            // *************
            dtPurchaseValue0.TextChanged += (o, e) => method(tbPurchaseValue0, dtPurchaseValue0, dtWinPercentage0, tbSaleValue0);
            dtWinPercentage0.TextChanged += (o, e) => method(tbWinPercentage0, dtPurchaseValue0, dtWinPercentage0, tbSaleValue0);
            dtSaleValue0.TextChanged += (o, e) => method2(tbSaleValue0, dtPurchaseValue0, tbWinPercentage0, dtSaleValue0);

            dtPurchaseValue0.TextChanged += (o, e) => method(tbPurchaseValue0, dtPurchaseValue0, dtWinPercentage0_1, tbSaleValue0_1);
            dtWinPercentage0_1.TextChanged += (o, e) => method(tbWinPercentage0_1, dtPurchaseValue0, dtWinPercentage0_1, tbSaleValue0_1);
            dtSaleValue0_1.TextChanged += (o, e) => method2(tbSaleValue0_1, dtPurchaseValue0, tbWinPercentage0_1, dtSaleValue0_1);

            dtPurchaseValue0.TextChanged += (o, e) => method(tbPurchaseValue0, dtPurchaseValue0, dtWinPercentage0_2, tbSaleValue0_2);
            dtWinPercentage0_2.TextChanged += (o, e) => method(tbWinPercentage0_2, dtPurchaseValue0, dtWinPercentage0_2, tbSaleValue0_2);
            dtSaleValue0_2.TextChanged += (o, e) => method2(tbSaleValue0_2, dtPurchaseValue0, tbWinPercentage0_2, dtSaleValue0_2);

            dtPurchaseValue0.TextChanged += (o, e) => method(tbPurchaseValue0, dtPurchaseValue0, dtWinPercentage0_3, tbSaleValue0_3);
            dtWinPercentage0_3.TextChanged += (o, e) => method(tbWinPercentage0_3, dtPurchaseValue0, dtWinPercentage0_3, tbSaleValue0_3);
            dtSaleValue0_3.TextChanged += (o, e) => method2(tbSaleValue0_3, dtPurchaseValue0, tbWinPercentage0_3, dtSaleValue0_3);
            // *************

            // *************
            dtPurchaseValue1.TextChanged += (o, e) => method(tbPurchaseValue1, dtPurchaseValue1, dtWinPercentage1, tbSaleValue1);
            dtWinPercentage1.TextChanged += (o, e) => method(tbWinPercentage1, dtPurchaseValue1, dtWinPercentage1, tbSaleValue1);
            dtSaleValue1.TextChanged += (o, e) => method2(tbSaleValue1, dtPurchaseValue1, tbWinPercentage1, dtSaleValue1);

            dtPurchaseValue1.TextChanged += (o, e) => method(tbPurchaseValue1, dtPurchaseValue1, dtWinPercentage1_1, tbSaleValue1_1);
            dtWinPercentage1_1.TextChanged += (o, e) => method(tbWinPercentage1_1, dtPurchaseValue1, dtWinPercentage1_1, tbSaleValue1_1);
            dtSaleValue1_1.TextChanged += (o, e) => method2(tbSaleValue1_1, dtPurchaseValue1, tbWinPercentage1_1, dtSaleValue1_1);

            dtPurchaseValue1.TextChanged += (o, e) => method(tbPurchaseValue1, dtPurchaseValue1, dtWinPercentage1_2, tbSaleValue1_2);
            dtWinPercentage1_2.TextChanged += (o, e) => method(tbWinPercentage1_2, dtPurchaseValue1, dtWinPercentage1_2, tbSaleValue1_2);
            dtSaleValue1_2.TextChanged += (o, e) => method2(tbSaleValue1_2, dtPurchaseValue1, tbWinPercentage1_2, dtSaleValue1_2);

            dtPurchaseValue1.TextChanged += (o, e) => method(tbPurchaseValue1, dtPurchaseValue1, dtWinPercentage1_3, tbSaleValue1_3);
            dtWinPercentage1_3.TextChanged += (o, e) => method(tbWinPercentage1_3, dtPurchaseValue1, dtWinPercentage1_3, tbSaleValue1_3);
            dtSaleValue1_3.TextChanged += (o, e) => method2(tbSaleValue1_3, dtPurchaseValue1, tbWinPercentage1_3, dtSaleValue1_3);
            // *************
        }

        /// <summary>
        /// Carga los eventos del sistema
        /// </summary>
        private void LoadEvents()
        {
            btCancel.Click += BtCancelClick;
            btSave.Click += BtSaveClick;
            btComponents.Click += BtComponentsClick;
            btEquivalentProducts.Click += BtEquivalentProductsClick;
            tbPicture.TextChanged += TbPictureTextChanged;
            cbGroup.SelectionChanged += CbGroupSelectionChange;
           
            cbStore.SelectionChanged += CbStoreSelectionChange;
            cbStore.DropDownOpened += (o, e) => cbStore.ItemsSource = Controller.GetProductsStores();
            tbNameSupplier0.PreviewKeyDown += TbNameSupplier0_PreviewKeyDown;
            tbNameSupplier0.GotFocus += TbNameSupplier0_GotFocus;
            tbNameSupplier0.LostFocus += TbNameSupplier0_LostFocus;
            tbNameSupplier0.TextChanged += TbNameSupplier0_TextChange;
            stItemsSupplier.MouseWheel += StItemsSupplier_MouseWheel;
        }

        private void StItemsSupplier_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int option = -1;
            if (e.Delta == -120) option = 0;
            if (e.Delta == 120) option = 1;

            MoveCursosItems(option);
        }

        #endregion

        #region Events

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
          //  const string Prefijo = "BBC*";
           // tbCode.Text = Prefijo;

            cbGroup.DisplayMemberPath = "Name";
            //    cbStore.DisplayMemberPath = "Name";

            
            List<InvGroup> inventoryGroups = new List<InvGroup>(await Controller.GetProductsGroups())
            {
                new InvGroup("-1", "Nuevo Grupo")
            };

            cbGroup.ItemsSource = inventoryGroups.ToArray();
            cbStore.ItemsSource = Controller.GetProductsStores();

            stItemsSupplier.Tag = 0;
            ((Border)stItemsSupplier.Children[0]).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEFEDED"));

           
            ActualizarListDatos();
        }

        private void CbGroupSelectionChange(object sender, SelectionChangedEventArgs e) {
            if (cbGroup.SelectedIndex == cbGroup.Items.Count - 1) {
                PageInventoryGroup pageInventoryGroup = new PageInventoryGroup();
                _ = new InventoryGroupController(pageInventoryGroup);
                cbGroup.SelectedIndex = -1;
            }
        }

        private void CbStoreSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (cbStore.SelectedIndex == cbStore.Items.Count - 1)
            {
                PageInventoryStore pageInventoryStore= new PageInventoryStore();
                InventoryStoreController groupController = new InventoryStoreController(pageInventoryStore);
                cbStore.SelectedIndex = -1;
            }
         }

        private void BtCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void BtSaveClick(object sender, EventArgs e)
        {
            string Code = tbCode.Text;
            int Type = cbType.SelectedIndex;
            InvGroup Group = cbGroup.SelectedIndex == -1 ? null : (InvGroup)cbGroup.Items[cbGroup.SelectedIndex];
            string Name = tbName.Text;
            string Barcode = tbBarcode.Text;
            int UnitOfMeasurement = cbUnitOfMeasurement.SelectedIndex;
            int TaxClassification = cbTaxClassification.SelectedIndex;
            int Store = cbStore.SelectedIndex;
            string Picture = tbPicture.Text;
            string Description = tbDescription.Text;
            bool SaveImage = chSaveImage.IsChecked.Value;
            string Key0 = tbKey0.Text;
            string Value0 = tbValue0.Text;
            string Key1 = tbKey1.Text;
            string Value1 = tbValue1.Text;
            string Key2 = tbKey2.Text;
            string Value2 = tbValue2.Text;
            string Key3 = tbKey3.Text;
            string Value3 = tbValue3.Text;
            string Key4 = tbKey4.Text;
            string Value4 = tbValue4.Text;
            string Key5 = tbKey5.Text;
            string Value5 = tbValue5.Text;

            Product product = new Product(Code, Type, Group, Name, Barcode, UnitOfMeasurement, TaxClassification, Store, Picture, Description, SaveImage, Key0,
                Value0, Key1, Value1, Key2, Value2, Key3, Value3, Key4, Value4, Key5, Value5, equivalents, components);

            IsEnabled = false;
            progBar.Visibility = Visibility.Visible;

            string respuesta = await Controller.CreateA(product);

            IsEnabled = true;
            progBar.Visibility = Visibility.Collapsed;

            if (respuesta == "internal_server_error")
            {
                MessageBoxX.Show("Error Interno del Servidor", MessageBoxXType.Error500).Then((dialogResult) => Console.WriteLine("Reporte de error"));
            }
            else if (respuesta == "not_found")
            {
                MessageBoxX.Show("Error Interno del Servidor", MessageBoxXType.Error404).Then((dialogResult) => Console.WriteLine("Reporte de error"));
            }
            else if (respuesta == "service_unavailable")
            {
                MessageBoxX.Show("Error Interno del Servidor", MessageBoxXType.Error503).Then((dialogResult) => Console.WriteLine("Reporte de error"));
            }
            else if (respuesta == "conflict")
            {
                MessageBox.Show("Producto ya Existe :(");
            }
            else
            {
                MessageBox.Show("Producto Guardado ;)");
            }

        }

        private void BtComponentsClick(object sender, EventArgs e)
        {
            PageProductComponents pageProductComponents = new PageProductComponents(Main.MainWindow.Instace.GetFrameDialog());
            pageProductComponents.SetValues(components);
            ProductComponentsContoller productComponentsContoller = new ProductComponentsContoller(pageProductComponents);

            pageProductComponents.OnClosed += delegate
            {
                Main.MainWindow.Instace.CloseFrameDialog();

                if (pageProductComponents.DialogResult == DialogResult.OK)
                {
                    components = pageProductComponents.GetValues();
                }
            };
        }

        private void BtEquivalentProductsClick(object sender, EventArgs e)
        {
            PageEquivalentProducts pageEquivalentProducts = new PageEquivalentProducts(Main.MainWindow.Instace.GetFrameDialog());
            pageEquivalentProducts.SetValues(equivalents);
            EquivalentProductsController equivalentProductsController = new EquivalentProductsController(pageEquivalentProducts);

            pageEquivalentProducts.OnClosed += delegate
            {
                Main.MainWindow.Instace.CloseFrameDialog();

                if (pageEquivalentProducts.DialogResult == DialogResult.OK)
                {
                    equivalents = pageEquivalentProducts.GetValues();
                }
            };
        }

        private void TbPictureTextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbPicture.Text == "*")
            {
                tbPicture.Text = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Todos los archivos de imagen|*.png;*.jpg;*.jpeg;*.bmp|PNG Image (*.png)|*.png|JPEG files (*.jpeg)|*.jpeg|JPG files (*.jpg)|*.jpg|BMP files (*.bmp)|*.bmp";
                if (openFileDialog.ShowDialog() == true)
                {
                    tbPicture.Text = openFileDialog.FileName;
                }
            }
        }

        private void TbNameSupplier0_GotFocus(object sender, EventArgs e)
        {
            cdListSupplier.Visibility = Visibility.Visible;
        }

        private void TbNameSupplier0_LostFocus(object sender, EventArgs e)
        {
            cdListSupplier.Visibility = Visibility.Collapsed;
        }

        struct Dato {
            public string Nombre { get; set; }
            public string Nit { get; set; }

            public Dato(string nombre, string nit) {
                Nombre = nombre;
                Nit = nit;
            }
        }

        private static Dato[] allDatos = {
            new Dato("López Ana","23456789"),
            new Dato("García Juan","34567890"),
            new Dato("Martínez María","45678901"),
            new Dato("Rodríguez Alejandro","56789012"),
            new Dato("Pérez Laura","67890123"),
            new Dato("Sánchez Daniel","78901234"),
            new Dato("González Sofía","89012345"),
            new Dato("Ramírez Carlos","90123456"),
            new Dato("Fernández Lucía","10234567"),
            new Dato("Torres Javier","21098765"),
            new Dato("Jiménez Andrea","32109876"),
            new Dato("Díaz Diego","43210987"),
            new Dato("Moreno Paula","54321098"),
            new Dato("Ruiz Miguel","65432109"),
            new Dato("Silva Marta","76543210"),
            new Dato("Ortega Pedro","87654321"),
            new Dato("Vargas Valentina","98765432"),
            new Dato("Castro Rafael","19876543"),
            new Dato("Flores Elena","21987654"),
            new Dato("Mendoza Sergio","32198765"),
            new Dato("Ríos Natalia","43219876"),
            new Dato("Núñez André","54321987"),
            new Dato("Cordero Adriana","65432198"),
            new Dato("Álvarez José","76543219"),
            new Dato("Gómez Carla","87654321"),
            new Dato("Herrera Francisco","98765432"),
            new Dato("Vega Verónica","29876543"),
            new Dato("Aguilar Alberto","32987654"),
            new Dato("Paredes Victoria","43298765"),
            new Dato("Medina Luis","54329876"),
            new Dato("Rojas Gabriela","65432987"),
            new Dato("Peralta Ricardo","76543298"),
            new Dato("Castillo Camila","87654329"),
            new Dato("León Manuel","98765432"),
            new Dato("Morales Isabella","39876543"),
            new Dato("Camacho Gabriel","43987654"),
            new Dato("Rivera Anaís","54398765"),
            new Dato("Montes Eduardo","65439876"),
            new Dato("Solís Martina","76543987"),
            new Dato("Soto Juan Pablo","87654398"),
            new Dato("Benavides Antonia","98765439"),
            new Dato("Ureña Guillermo","49876543"),
            new Dato("Delgado Renata","54987654"),
            new Dato("Guzmán Raúl","65498765"),
            new Dato("Guerra Beatri","76549876"),
            new Dato("Chacón Emilio","87654987"),
            new Dato("Herrera Belén","98765498"),
            new Dato("Chaves Arturo","59876543"),
            new Dato("Rojas Daniela","65987654"),
            new Dato("Salazar Ángel","76598765"),
            new Dato("Castro Paloma","87659876"),
            new Dato("Araya Julián","98765987"),
            new Dato("Avilés Alejandra","59876543"),
            new Dato("Méndez Sebastián","65987654"),
            new Dato("Navarro Clara","76598765"),
            new Dato("Alvarado Óscar","87659876"),
            new Dato("Marín Tatiana","98765987"),
            new Dato("Zamora Mateo","59876543"),
            new Dato("Sandoval Lourdes","65987654"),
            new Dato("Campos Jorge","76598765"),
            new Dato("Umaña Violet","87659876"),
            new Dato("Brenes Esteban","98765987"),
            new Dato("Morales Mariana","59876543"),
            new Dato("Bolaños Sergio","65987654"),
            new Dato("Arce Ximena","76598765"),
            new Dato("Esquivel Alonso","87659876"),
            new Dato("Del Valle Patricia","98765987"),
            new Dato("Méndez Gonzalo","59876543"),
            new Dato("Porras Regina","65987654"),
            new Dato("Segura Tomás","76598765"),
            new Dato("Quesada Irene","87659876"),
            new Dato("Fonseca Federico","98765987"),
            new Dato("Alfaro Constanza","59876543"),
            new Dato("Araya René","65987654"),
            new Dato("Durán Victoria","76598765"),
            new Dato("Acosta César","87659876"),
            new Dato("Céspedes Olivia","98765987"),
            new Dato("Zúñiga Ignacio","59876543"),
            new Dato("Rojas Isabe","65987654"),
            new Dato("Carvajal Leonel","76598765"),
            new Dato("Sibaja Ángela","87659876"),
            new Dato("Mora Ulises","98765987"),
            new Dato("Gutiérrez Valeria","59876543"),
            new Dato("Ulloa Javier","65987654"),
            new Dato("Badilla Bárbara","76598765"),
            new Dato("Pérez Alexis","87659876"),
            new Dato("Cordero Fernanda","98765987"),
            new Dato("Bonilla Gilberto","59876543"),
            new Dato("Castillo Jessica","65987654"),
            new Dato("Ávila Benjamín","76598765"),
            new Dato("Chinchilla Marisol","87659876"),
            new Dato("Mena Eduardo","98765987"),
            new Dato("Morales Pamela","59876543"),
            new Dato("Esquivia Héctor","65987654"),
            new Dato("Granados Gisela","76598765"),
            new Dato("Benavente Samuel","87659876"),
            new Dato("Morales Elena","98765987"),
            new Dato("Barquero Simón","59876543"),
            new Dato("Venegas Cynthia","65987654"),
            new Dato("Lizano Ariel","12345678")
        };

        List<Dato> datos = new List<Dato>(allDatos);

        private void TbNameSupplier0_PreviewKeyDown(object sender, KeyEventArgs e) {

            int option = -1;
            if(e.Key == Key.Down) option = 0;
            if (e.Key == Key.Up) option = 1;
            if (e.Key == Key.Enter) option = 2;
  
            MoveCursosItems(option);
            /*
            SolidColorBrush selectColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEFEDED"));
            int index = (int)stItemsSupplier.Tag;

            Collection<Border> items = stItemsSupplier.Children.Where<Border>(item => item.Visibility == Visibility.Visible);

            Console.WriteLine(items.Count);

            if (e.Key == Key.Down && datos.Count > 1)
            {

                if (indexS > datos.Count - items.Count - 1 && index >= items.Count - 1)
                {
                    index = items.Count - 1;
                    items[items.Count - 1].Background = selectColor;
                    items[0].Background = Brushes.White;
                }
                else
                {
                    if (index >= items.Count - 1)
                    {
                        index = items.Count - 2;
                        indexS++;
                        ActualizarListDatos();

                    }
                    else
                    {
                        stItemsSupplier.Tag = index + 1;
                    }


                    Border current = items[index];
                    Border next;

                    if (index == items.Count - 1) next = items[0];
                    else next = items[index + 1];

                    current.Background = Brushes.White;
                    next.Background = selectColor;
                }

            }

            if (e.Key == Key.Up && (indexS + index ) > 0 && datos.Count > 1)
            {

                if (index <= 0)
                {
                    indexS--;
                    stItemsSupplier.Tag = 0;
                    ActualizarListDatos();

                }
                else
                    stItemsSupplier.Tag = index - 1;

                index = (int)stItemsSupplier.Tag;

                Border current = items[index];
                Border next;

                if (index == items.Count - 1) next = items[0];
                else next = items[index + 1];

                current.Background = selectColor;
                next.Background = Brushes.White;

            }

            if (e.Key == Key.Enter)
            {
                string text = ((TextBlock)((StackPanel)((Border)stItemsSupplier.Children[index]).Child).Children[1]).Text;
                string text2 = ((TextBlock)((StackPanel)((Border)stItemsSupplier.Children[index]).Child).Children[3]).Text;
                cdSupplier0.ToolTip = "Nombre: " + text + "\nNIT: " + text2;
                tbNameSupplier0.Clear();
                tbNameSupplier0.IsEnabled = false;
                cdSupplier0.Visibility = Visibility.Visible;
            }
            */
        }

        private void MoveCursosItems(int option) {
            SolidColorBrush selectColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEFEDED"));
            int index = (int)stItemsSupplier.Tag;

            Collection<Border> items = stItemsSupplier.Children.Where<Border>(item => item.Visibility == Visibility.Visible);

            Console.WriteLine(items.Count);

            if (option == 0 && datos.Count > 1)
            {

                if (indexS > datos.Count - items.Count - 1 && index >= items.Count - 1)
                {
                    index = items.Count - 1;
                    items[items.Count - 1].Background = selectColor;
                    items[0].Background = Brushes.White;
                }
                else
                {
                    if (index >= items.Count - 1)
                    {
                        index = items.Count - 2;
                        indexS++;
                        ActualizarListDatos();

                    }
                    else
                    {
                        stItemsSupplier.Tag = index + 1;
                    }


                    Border current = items[index];
                    Border next;

                    if (index == items.Count - 1) next = items[0];
                    else next = items[index + 1];

                    current.Background = Brushes.White;
                    next.Background = selectColor;
                }

            }

            if (option == 1 && (indexS + index) > 0 && datos.Count > 1)
            {

                if (index <= 0)
                {
                    indexS--;
                    stItemsSupplier.Tag = 0;
                    ActualizarListDatos();

                }
                else
                    stItemsSupplier.Tag = index - 1;

                index = (int)stItemsSupplier.Tag;

                Border current = items[index];
                Border next;

                if (index == items.Count - 1) next = items[0];
                else next = items[index + 1];

                current.Background = selectColor;
                next.Background = Brushes.White;

            }

            if (option == 2)
            {
                string text = ((TextBlock)((StackPanel)((Border)stItemsSupplier.Children[index]).Child).Children[1]).Text;
                string text2 = ((TextBlock)((StackPanel)((Border)stItemsSupplier.Children[index]).Child).Children[3]).Text;
                cdSupplier0.ToolTip = "Nombre: " + text + "\nNIT: " + text2;
                tbNameSupplier0.Clear();
                tbNameSupplier0.IsEnabled = false;
                cdSupplier0.Visibility = Visibility.Visible;
            }
        }

        private void TbNameSupplier0_TextChange(object o, EventArgs e) {
            datos = allDatos.Where(dato => dato.Nombre.Contains(tbNameSupplier0.Text)).ToList();
            indexS = 0;
            stItemsSupplier.Tag = 0;
            ColocarEnBlancoLista();
            ActualizarListDatos();
            if (datos.Count > 0) {
                SolidColorBrush selectColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEFEDED"));
                Border border = (Border)stItemsSupplier.Children[0];
                border.Background = selectColor;
            }
        }

        private void ColocarEnBlancoLista() {
            for (int i = 0; i < stItemsSupplier.Children.Count; i++)
            {
                Border border = (Border)stItemsSupplier.Children[i];

                if (i + indexS >= datos.Count)
                {
                    border.Background = Brushes.White;
                }
            }
        }

        int indexS = 0;

        private void ActualizarListDatos() {
            int add = 0;

            for (int i = 0; i < stItemsSupplier.Children.Count; i++) {
                Border border = (Border)stItemsSupplier.Children[i];

                if (i + indexS >= datos.Count)
                {
                    border.Visibility = Visibility.Collapsed;
                }
                else
                {
                    border.Visibility = Visibility.Visible;
                    ((TextBlock)((StackPanel)((Border)stItemsSupplier.Children[i]).Child).Children[1]).Text = datos[i + indexS].Nombre;
                    ((TextBlock)((StackPanel)((Border)stItemsSupplier.Children[i]).Child).Children[3]).Text = datos[i + indexS].Nit;
                    add++;
                }
            }

            double size = datos.Count;

          

            double current = indexS / (size - add);

            double scrollSize = -100 + (200 * current);

          
            if (!double.IsNaN(scrollSize)) {
                bdScroll.Margin = new System.Windows.Thickness(247, scrollSize, 0, 0);
            }
            
        }

        #endregion

        #region IProductGUI

        public void Close()
        {
            MainWindow.Instace.CloseFrameDialog();
            OnClosed?.Invoke(this, new EventArgs());
        }

        public object GetGUI()
        {
            return this;
        }

        public GUIState GetGUIState()
        {
            throw new NotImplementedException();
        }

        public IntPtr GetHandle()
        {
            throw new NotImplementedException();
        }

        public int GetHeight()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public IGUI<string, object[], string, string> GetParent()
        {
            throw new NotImplementedException();
        }

        public ScreenNumber GetScreen()
        {
            throw new NotImplementedException();
        }

        public string GetTitle()
        {
            throw new NotImplementedException();
        }

        public object[] GetValues()
        {
            throw new NotImplementedException();
        }

        public int GetWidth()
        {
            throw new NotImplementedException();
        }

        public int GetX()
        {
            throw new NotImplementedException();
        }

        public int GetY()
        {
            throw new NotImplementedException();
        }

        public void SetValues(object[] datas)
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            MainWindow.Instace.GetFrameDialog().Content = this;
            OnOpen?.Invoke(this, new EventArgs());
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
