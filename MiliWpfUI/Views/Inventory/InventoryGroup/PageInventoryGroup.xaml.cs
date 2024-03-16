using GrapInterCom.Interfaces.Inventory;
using MiliSoftware.GUI.Styles;
using MiliSoftware.Inventory;
using MiliSoftware.UI;
using MiliSoftware.Views.Alerts;
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
using InvGroup = MiliSoftware.InventoryGroup;

namespace MiliSoftware.Views.Inventory.InventoryGroup
{
    /// <summary>
    /// Lógica de interacción para PageInventoryGroup.xaml
    /// </summary>
    public partial class PageInventoryGroup : Page, IInventoryGroupGUI
    {

        public DialogResult DialogResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICRUD<InvGroup, InvGroup[], InvGroup, InvGroup> controller { get; set; }
        private InventoryGroupController InventoryGroupController { get; set; }

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        public PageInventoryGroup()
        {
            InitializeComponent();
            LoadEvents();
            LoadStyles();
            LoadLanguage();
        }


        #region Functions

        private void LoadEvents()
        {
            Loaded += OnLoaded;
            btNew.Click += btNewClick;
            btDelete.Click += btDeleteClick;
            btEdit.Click += btEditClick;
            lvInventoryGroups.SelectionChanged += lvInventoryGroupsSelectionChanged;
            btExit.Click += btExitClick;
        }

        private void LoadLanguage()
        {
            btNew.ToolTip = languaje.PageInventoryGroup.toolTipBtnNew;
            btEdit.ToolTip = languaje.PageInventoryGroup.toolTipBtnEdit;
            btDelete.ToolTip = languaje.PageInventoryGroup.toolTipBtnDelete;
            btExport.ToolTip = languaje.PageInventoryGroup.toolTipBtnExport;
            btUpdate.ToolTip = languaje.PageInventoryGroup.toolTipBtnUpdate;
            btExit.ToolTip = languaje.PageInventoryGroup.toolTipBtnExit;
        }

        private async void LoadData() {
            InventoryGroupController = controller as InventoryGroupController;
            List<InvGroup> inventoryGroups = await InventoryGroupController.GetInventoryGroups();
            lvInventoryGroups.ItemsSource = inventoryGroups;
           
            // se habilita la interfas grafica
            lvInventoryGroups.Visibility = Visibility.Visible;
            progBar.Visibility = Visibility.Collapsed;
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

        #endregion

        #region Events

        private void OnLoaded(object sender, EventArgs e)
        {
            // se desabilita la interfas grafica
            lvInventoryGroups.Visibility = Visibility.Collapsed;
            progBar.Visibility = Visibility.Visible;

            // Se crea los campos para el GridView
            lvHeaders.Columns.Add(new GridViewColumn() { Header = languaje.PageInventoryGroup.headCode, DisplayMemberBinding = new Binding("Code") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = languaje.PageInventoryGroup.headName, DisplayMemberBinding = new Binding("Name") });
            // Se carga los datos del controlador
            LoadData();
          
        }

        private void btNewClick(object o, EventArgs e)
        {
            PageNewInventoryGroup pageNewInventoryGroup = new PageNewInventoryGroup();
            InventoryGroupController inventoryGroupController = new InventoryGroupController(pageNewInventoryGroup);
            pageNewInventoryGroup.OnClosed += (sender, ev) =>
            {
                if (pageNewInventoryGroup.DialogResult == DialogResult.OK)
                    LoadData();
            };
        }

        private void btEditClick(object o, EventArgs e) {
            PageNewInventoryGroup pageNewInventoryGroup = new PageNewInventoryGroup();
            pageNewInventoryGroup.EditMode = true;
            pageNewInventoryGroup.InvGroup = (InvGroup)lvInventoryGroups.SelectedItem;
            InventoryGroupController inventoryGroupController = new InventoryGroupController(pageNewInventoryGroup);
            pageNewInventoryGroup.OnClosed += (sender, ev) =>
            {
                if (pageNewInventoryGroup.DialogResult == DialogResult.OK)
                    LoadData();
            };
        }

        private void btDeleteClick(object o, EventArgs e)
        {
            MessageBoxX.Show("Esta seguro bro de eliminar el grupo de inventario?", MessageBoxXType.DeleteNO).Then(async (dialogResult) => {
                if (dialogResult == DialogResult.No) return;

                int[] ids = lvInventoryGroups.SelectedItems.Select<InvGroup, int>(grupo => grupo.Id).ToArray();

                InventoryGroupController = controller as InventoryGroupController;

                if (await InventoryGroupController.EliminarGrupo(ids) == ids.Length)
                {
                    Main.MainWindow.Instace.AlertDialog("Datos Eliminados");
                    LoadData();
                }
            });       
        }

        private void lvInventoryGroupsSelectionChanged(object sender, SelectionChangedEventArgs e) {
            bool enable = lvInventoryGroups.SelectedItem != null;
            btEdit.IsEnabled = enable && lvInventoryGroups.SelectedItems.Count == 1;
            btDelete.IsEnabled = enable;
        }

        private void btExitClick(object sender, EventArgs e) {
            Close();
        }

        #endregion

        #region IInventoryGroupGUI

        public void Close()
        {
            Main.MainWindow.Instace.CloseFrameDialog();
            OnClosed?.Invoke(this, new EventArgs());
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

        public ScreenNumber GetScreen()
        {
            throw new NotImplementedException();
        }

        public string GetTitle()
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

        public void Show()
        {
            Main.MainWindow.Instace.GetFrameDialog().Content = this;
            OnOpen?.Invoke(this, new EventArgs());
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
