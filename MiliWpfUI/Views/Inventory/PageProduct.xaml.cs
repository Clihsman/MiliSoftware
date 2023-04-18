/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 23/12/2022          *
 * Assembly : MiliWpfUI                *
 * *************************************/

using GrapInterCom.Interfaces.Inventory;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using MiliSoftware.Inventory;
using MiliSoftware.UI;
using MiliSoftware.Views.Customers;
using MiliSoftware.WpfUI;
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

namespace MiliSoftware.Views.Inventory
{
    /// <summary>
    /// Lógica de interacción para PageProduct.xaml
    /// </summary>
    public partial class PageProduct : Page, IProductGUI<Product,string,string,string>
    {
        public DialogResult DialogResult { get; set; }
        public ICRUD<Product, string, string, string> controller { get; set; }

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        private Frame parent;

        private object[] components;
        private object[] equivalents;

        public PageProduct(Frame parent)
        {
            InitializeComponent();
            this.parent = parent;
            LoadLanguage();
            LoadExternalControls();
            LoadEvents();    
        }

        private void LoadLanguage()
        {
            tbCode.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintTbCode);
            cbType.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintCbType);
            cbType.ItemsSource = languaje.PageProduct.cbTypesItems;
            cbGrup.SetValue(HintAssist.HintProperty, languaje.PageProduct.hintCbGroup);
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
                    saveValue = saveValue * 100.0M;
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

        private void LoadEvents()
        {
            btCancel.Click += btCancelClick;
            btSave.Click += btSaveClick;
            btComponents.Click += btComponentsClick;
            btEquivalentProducts.Click += btEquivalentProductsClick;
            tbPicture.TextChanged += tbPictureTextChanged;
        }

        #region Events

        private void btCancelClick(object o, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btSaveClick(object o, EventArgs e)
        {
            string Code = tbCode.Text;
            int Type = cbType.SelectedIndex;
            int Group = cbGrup.SelectedIndex;
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

            Product product = new Product(Code, Type, Group, Name, Barcode, UnitOfMeasurement, TaxClassification, Store, Picture, Description,  SaveImage,  Key0, 
                Value0,  Key1,  Value1,  Key2, Value2,  Key3,  Value3,  Key4,  Value4,  Key5,  Value5,(EquivalentProduct[])equivalents, (ProductComponent[])components);

            controller.Create(product);

            MessageBox.Show("Producto Guardado ;)");
            DialogResult = DialogResult.OK;
        }

        private void btComponentsClick(object o, EventArgs e)
        {
            PageProductComponents pageProductComponents = new PageProductComponents(Main.MainWindow.Instace.dialogFrame1);
            pageProductComponents.SetValues(components);
            ProductComponentsContoller productComponentsContoller = new ProductComponentsContoller(pageProductComponents);

            Main.MainWindow.Instace.dialogFrame.IsEnabled = false;

            pageProductComponents.OnClosed += delegate
            {
                Main.MainWindow.Instace.dialogFrame1.Content = null;
                Main.MainWindow.Instace.dialogFrame.IsEnabled = true;

                if (pageProductComponents.DialogResult == DialogResult.OK)
                {
                    components = pageProductComponents.GetValues();
                    Console.WriteLine(pageProductComponents.GetValues().Length);
                }
            };
        }

        private void btEquivalentProductsClick(object o, EventArgs e)
        {
            PageEquivalentProducts pageEquivalentProducts = new PageEquivalentProducts(Main.MainWindow.Instace.dialogFrame1);
            pageEquivalentProducts.SetValues(equivalents);
            EquivalentProductsController equivalentProductsController = new EquivalentProductsController(pageEquivalentProducts);
            Main.MainWindow.Instace.dialogFrame.IsEnabled = false;

            pageEquivalentProducts.OnClosed += delegate
            {
                Main.MainWindow.Instace.dialogFrame1.Content = null;
                Main.MainWindow.Instace.dialogFrame.IsEnabled = true;

                if (pageEquivalentProducts.DialogResult == DialogResult.OK)
                {
                    equivalents = pageEquivalentProducts.GetValues();
                }
            };
        }

        private void tbPictureTextChanged(object sender, TextChangedEventArgs e)
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

        #endregion

        #region IProductGUI

        public void Close()
        {
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
            parent.Content = this;
            OnOpen?.Invoke(this, new EventArgs());
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            const string Prefijo = "BBC*";
            tbCode.Text = Prefijo;
        }
    }
}
