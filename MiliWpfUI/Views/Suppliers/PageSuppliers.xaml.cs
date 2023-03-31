using GrapInterCom.Interfaces.Suppliers;
using MiliSoftware.Suppliers;
using MiliSoftware.UI;
using MiliSoftware.Views.Main;
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

namespace MiliSoftware.Views.Suppliers
{
    /// <summary>
    /// Lógica de interacción para PageSuppliers.xaml
    /// </summary>
    public partial class PageSuppliers : Page, ISuppliersGUI
    {
        public ICRUD<string, object[], string, string> controller { get; set; }
        public DialogResult DialogResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        private Frame parent;

        public PageSuppliers(Frame parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLanguage();
            LoadEvents();
        }

        private void LoadLanguage()
        {
            tbSearch.SetValue(MaterialDesignThemes.Wpf.HintAssist.HintProperty, languaje.PageSuppliers.hintTbSearch);
            btSearch.ToolTip = languaje.PageSuppliers.toolTipBtSearchProduct;
            btSetting.ToolTip = languaje.PageSuppliers.tooTipBtSetting;
            btAssignGroup.ToolTip = languaje.PageSuppliers.tooTipBtAssignGroup;
            btUpdate.ToolTip = languaje.PageSuppliers.toolTipBtUpdate;
            btExport.ToolTip = languaje.PageSuppliers.toolTipBtExport;
            btDelete.ToolTip = languaje.PageSuppliers.toolTipBtDelete;
            btEdit.ToolTip = languaje.PageSuppliers.toolTipBtEdit;
            btNew.ToolTip = languaje.PageSuppliers.toolTipBtNew;

            lvHeaders.Columns.Add(new GridViewColumn() { Header = (string)languaje.PageSuppliers.headCode, DisplayMemberBinding = new Binding("Codigo") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = (string)languaje.PageSuppliers.headNames, DisplayMemberBinding = new Binding("Nombres") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = (string)languaje.PageSuppliers.headSurnames, DisplayMemberBinding = new Binding("Apellidos") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = (string)languaje.PageSuppliers.headId, DisplayMemberBinding = new Binding("Identificacion") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = (string)languaje.PageSuppliers.headMail, DisplayMemberBinding = new Binding("Correo") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = (string)languaje.PageSuppliers.headAddress, Width = 250, DisplayMemberBinding = new Binding("Direccion") });
        }

        private void LoadEvents() {
            btNew.Click += btNewClick;
        }

        #region Events

        private void btNewClick(object sender,EventArgs eventArgs) {
            MainWindow.Instace.parentFrame.IsEnabled = false;
            PageSupplier pageSupplier = new PageSupplier(MainWindow.Instace.dialogFrame);
            SuppliersController suppliersController = new SuppliersController(pageSupplier);

            pageSupplier.OnClosed += delegate {
                MainWindow.Instace.dialogFrame.Content = null;
                MainWindow.Instace.parentFrame.IsEnabled = true;
            };

            pageSupplier.Show();
        }

        #endregion

        #region ISuppliersGUI

        public void Close()
        {
            OnClosed?.Invoke(this,new EventArgs());
        }

        public object GetGUI()
        {
            throw new NotImplementedException();
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
    }
}
