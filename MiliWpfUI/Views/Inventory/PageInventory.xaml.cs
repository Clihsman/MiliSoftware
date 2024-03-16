using GrapInterCom.Interfaces.Inventory;
using MaterialDesignThemes.Wpf;
using MiliSoftware.GUI.Styles;
using MiliSoftware.GUI.Styles.Components;
using MiliSoftware.Inventory;
using MiliSoftware.UI;
using MiliSoftware.Views.Export;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiliSoftware.Views.Inventory
{
    /// <summary>
    /// Lógica de interacción para PageInventory.xaml
    /// </summary>
    public partial class PageInventory : Page, IInventoryGUI
    {
        public ICRUD<string, object[], string, string> controller { get; set; }
        private InventoryController Controller { get; set; }
        public DialogResult DialogResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        private Product[] Products { get; set; }
        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        public PageInventory()
        {
            InitializeComponent();
            LoadStyles();
            LoadLanguage();
            LoadEvents();
        }

        private void LoadStyles()
        {
            StyleSheet styleSheet = StyleSheet.Load("assets/styles/coloredStyle.json");

            if (styleSheet.GetFormStyle("form_inventory") != null)
            {
                btDelete.Foreground = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_delete").GetForegroundBrush();
                btDelete.BorderBrush = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_delete").GetBackgroundBrush();
                btDelete.Background = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_delete").GetBackgroundBrush();

                btNew.Foreground = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_new").GetForegroundBrush();
                btNew.BorderBrush = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_new").GetBackgroundBrush();
                btNew.Background = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_new").GetBackgroundBrush();

                btEdit.Foreground = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_edit").GetForegroundBrush();
                btEdit.BorderBrush = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_edit").GetBackgroundBrush();
                btEdit.Background = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_edit").GetBackgroundBrush();
            }
        }

        private void LoadLanguage() {
            tbSearch.SetValue(HintAssist.HintProperty, languaje.PageInventory.hintTbSearch);
            btSearch.ToolTip = languaje.PageInventory.toolTipBtSearchProduct;
            btSetting.ToolTip = languaje.PageInventory.tooTipBtSetting;
            btAssignGroup.ToolTip = languaje.PageInventory.tooTipBtAssignGroup;
            btUpdate.ToolTip = languaje.PageInventory.toolTipBtUpdate;
            btExport.ToolTip = languaje.PageInventory.toolTipBtExport;
            btDelete.ToolTip = languaje.PageInventory.toolTipBtDelete;
            btEdit.ToolTip = languaje.PageInventory.toolTipBtEdit;
            btNew.ToolTip = languaje.PageInventory.toolTipBtNew;
        }

        private void LoadEvents() {
            btNew.Click += BtNewClick;
            btEdit.Click += BtEditClick;
            lvCustomers.SelectionChanged += LvProductsChange;

            btExport.Click += (o,e) =>  {
             MainWindow.Instace.GetFrameDialog().Content = new PageExport();
            };
        }

        private async void LoadData() {
            Controller = (InventoryController)controller;

            progBar.Visibility = Visibility.Visible;
            IsEnabled = false;
            Products = await Controller.TodosLosProductos();

            lvHeaders.Columns.Add(new GridViewColumn() { Header = languaje.PageProductComponents.headCode, DisplayMemberBinding = new Binding("Code") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = languaje.PageProductComponents.headName, DisplayMemberBinding = new Binding("Name") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = "Tipo de Producto", DisplayMemberBinding = new Binding("Type") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = "Grupo", DisplayMemberBinding = new Binding("InventoryGroup") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = "Bodega", DisplayMemberBinding = new Binding("Store") });

            lvCustomers.ItemsSource = Products;

            progBar.Visibility = Visibility.Collapsed;
            IsEnabled = true;
        }

        #region Events

        private void LvProductsChange(object o, EventArgs e) {
            btEdit.IsEnabled = lvCustomers.SelectedItems.Count == 1;
        }

        private void BtNewClick(object o, EventArgs e)
        {
            //MainWindow.Instace.parentFrame.IsEnabled = false;
            PageProduct pageProduct = new PageProduct();
            _ = new ProductController(pageProduct);
        }

        private void BtEditClick(object o, EventArgs e) {
            PageProduct pageProduct = new PageProduct();
            pageProduct.SetProductEdit(Products[lvCustomers.SelectedIndex]);
            _ = new ProductController(pageProduct);
        }

        #endregion

        #region IInventoryGUI

        public void Close()
        {
            OnClosed?.Invoke(this, new EventArgs());
            lvCustomers.ItemsSource = null;
            progBar.Visibility = Visibility.Hidden;
            lvHeaders.Columns.Clear();
            OnClosed?.Invoke(this, new EventArgs());
            GC.SuppressFinalize(this);
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        public object GetGUI()
        {
            return this;
        }

        public GUIState GetGUIState()
        {
            return GUIState.Normal;
        }

        public IntPtr GetHandle()
        {
            throw new NotImplementedException();
        }

        public int GetHeight()
        {
            return (int)Height;
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

        public object[] GetValues()
        {
            throw new NotImplementedException();
        }

        public int GetWidth()
        {
            return (int)Width;
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
            LoadData();
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
