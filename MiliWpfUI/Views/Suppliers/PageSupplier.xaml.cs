using GrapInterCom.Interfaces.Suppliers;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using MiliSoftware.UI;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiliSoftware.Views.Suppliers
{
    /// <summary>
    /// Lógica de interacción para PageSupplier.xaml
    /// </summary>
    public partial class PageSupplier : Page, ISuppliersGUI
    {
        public ICRUD<string, object[], string, string> controller { get; set; }
        public DialogResult DialogResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        private Frame parent;

        public PageSupplier(Frame parent)
        {
            InitializeComponent();
            this.parent = parent;
            LoadLanguage();
            LoadEvents();
            LoadExternsControls();
        }

        private void LoadExternsControls()
        {
            IntTextBox intDocument = new IntTextBox(tbDocument);
        }

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
            cbAmountry0.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintCbAmountry);
            tbCondition0.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbCondition);
            tbCity0.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbCity);
            tbPostalCode0.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbPostalCode);
            tbDirection0.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbDirection);
            // Amountry 1
            cbAmountry1.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintCbAmountry);
            tbCondition1.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbCondition);
            tbCity1.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbCity);
            tbPostalCode1.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbPostalCode);
            tbDirection1.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintTbDirection);
            // Amountry 2
            cbAmountry2.SetValue(HintAssist.HintProperty, languaje.PageSupplier.hintCbAmountry);
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

        private void LoadEvents()
        {
            tbPicture.TextChanged += tbPictureTextChange;
            btSave.Click += delegate
            {
                VerifyMandatoryData();
            };
            btCancel.Click += btCancelClick;
        }

        private void VerifyMandatoryData() {
            string code = tbCode.Text.Trim();
            string name = tbName.Text.Trim();
            string document = tbDocument.Text;
            bool documentType = cbDocumentType.SelectedIndex >= 1;
            bool type = cbType.SelectedIndex >= 1;

            if (code == "*")
                tbCode.Text = (new Random().Next().ToString());
            else
            {
                MySnackbar.MessageQueue.Enqueue("El Codigo es oblidatorio");
                int count = 0;
                System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(delegate {
                    if (count >= 12)
                    {
                        dispatcherTimer.Stop();
                        iconCode.Foreground = Brushes.Black;
                        return;
                    }

                    if (iconCode.Foreground == Brushes.Black)
                        iconCode.Foreground = Brushes.Red;
                    else
                        iconCode.Foreground = Brushes.Black;
                    count++;
                });
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0,0, 160);
                dispatcherTimer.Start();
            }
        }

        #region Events

        private void tbPictureTextChange(object sender, EventArgs e)
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

        private void btCancelClick(object sender, EventArgs e)
        {
            OnClosed?.Invoke(sender, e);
        }

        #endregion

        #region ISuppliersGUI

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
            return Name;
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
            return Title;
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

        public void Show()
        {
            parent.Content = this;
            OnOpen?.Invoke(this, new EventArgs());
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        public object[] GetValues()
        {
            string code = tbCode.Text;
            int category = cbCategory.SelectedIndex;
            int group = cbGrup.SelectedIndex;
            int lineOfBusiness = cbLineOfBusiness.SelectedIndex;
            string name = tbName.Text;
            int type = cbType.SelectedIndex;
            int documentType = cbDocumentType.SelectedIndex;
            ulong document = ulong.Parse(tbDocument.Text.Replace(".",""));
            string pictureSource = tbPicture.Text;
            string description = tbDescription.Text;
            bool saveImage = chSaveImage.IsChecked.Value;
            string contact0 = string.Format("{0}&{1}", tbCelCode0.Text, tbContact0.Text);
            string contact1 = string.Format("{0}&{1}", tbCelCode1.Text, tbContact1.Text);
            string contact2 = string.Format("{0}&{1}", tbCelCode2.Text, tbContact2.Text);
            string email = tbEmail1.Text;
            string businessRegistration = tbBusinessRegistration.Text;
            bool taxIncluded = chTaxIncluded.IsChecked.Value;
            // direcction 0
            string country0 = cbAmountry0.Text;
            string condition0 = tbCondition0.Text;
            string city0 = tbCity0.Text;
            string postalCode0 = tbPostalCode0.Text;
            string direction0 = tbDirection0.Text;
            // direcction 1
            string country1 = cbAmountry1.Text;
            string condition1 = tbCondition1.Text;
            string city1 = tbCity1.Text;
            string postalCode1 = tbPostalCode1.Text;
            string direction1 = tbDirection1.Text;
            // direcction 2
            string country2 = cbAmountry2.Text;
            string condition2 = tbCondition2.Text;
            string city2 = tbCity2.Text;
            string postalCode2 = tbPostalCode2.Text;
            string direction2 = tbDirection2.Text;

            Supplier supplier = new Supplier();
            supplier.Code = code;
            supplier.Category = category;
            supplier.Group = group;
            supplier.LineOfBusiness = lineOfBusiness;
            supplier.Name = name;
            supplier.Type = type;
            supplier.DocumentType = documentType;
            supplier.Document = document;
            supplier.PictureSource = pictureSource;
            supplier.Description = description;
            supplier.SaveImage = saveImage;
            supplier.Contact0 = contact0;
            supplier.Contact1 = contact1;
            supplier.Contact2 = contact2;
            supplier.Email = email;
            supplier.BusinessRegistration = businessRegistration;
            supplier.TaxIncluded = taxIncluded;

            supplier.Direction0 = new Address {
                Country = country0,
                Condition = condition0,
                City = city0,
                PostalCode = postalCode0,
                Direction = direction0
            };
            supplier.Direction1 = new Address
            {
                Country = country1,
                Condition = condition1,
                City = city1,
                PostalCode = postalCode1,
                Direction = direction1
            };
            supplier.Direction2 = new Address
            {
                Country = country2,
                Condition = condition2,
                City = city2,
                PostalCode = postalCode2,
                Direction = direction2
            };

            supplier.FromJson(supplier.ToJson());

            return new object[] { code, category, group, lineOfBusiness, name, type, documentType, document, pictureSource,
                description, saveImage, contact0, contact1, contact2, email, businessRegistration, taxIncluded, country0,
                condition0, city0, postalCode0, direction0, country1, condition1, city1, postalCode1, direction1, country2,
                condition2, city2, postalCode2, direction2};
        }

        public void SetValues(object[] datas)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
