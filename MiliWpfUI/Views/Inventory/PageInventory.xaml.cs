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
        public DialogResult DialogResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private Frame parent;

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        public PageInventory(Frame parent)
        {
            InitializeComponent();
            this.parent = parent;
            LoadLanguage();
            LoadEvents();


            // Codigo Basura

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
            //************************************
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
            btNew.Click += btNewClick;
            btExport.Click += (o,e) =>  {
             MainWindow.Instace.GetFrameDialog().Content = new PageExport();
            };
        }

        #region Events

        private void btNewClick(object o, EventArgs e)
        {
            //MainWindow.Instace.parentFrame.IsEnabled = false;
            PageProduct pageProduct = new PageProduct(MainWindow.Instace.GetFrameDialog()) { };
            ProductController productController = new ProductController(pageProduct);

            pageProduct.OnClosed += delegate {
                Main.MainWindow.Instace.CloseFrameDialog();
                //    MainWindow.Instace.dialogFrame.Content = null;
                //  MainWindow.Instace.parentFrame.IsEnabled = true;
            };

            pageProduct.Show();
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
            parent.Content = this;
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
