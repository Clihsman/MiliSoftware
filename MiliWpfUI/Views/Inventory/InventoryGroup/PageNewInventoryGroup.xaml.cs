using GrapInterCom.Interfaces.Inventory;
using MaterialDesignThemes.Wpf;
using MiliSoftware.UI;
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
    /// Lógica de interacción para PageNewInventoryGroup.xaml
    /// </summary>
    public partial class PageNewInventoryGroup : Page, IInventoryGroupGUI
    {
        public bool EditMode { get; set; }
        public InvGroup InvGroup { get; set; }

        public PageNewInventoryGroup()
        {
            InitializeComponent();
            LoadEvents();
            LoadLanguage();
            btCancel.Focus();
        }

        public DialogResult DialogResult { get; set; }
        public ICRUD<InvGroup, InvGroup[], InvGroup, InvGroup> controller { get; set; } 

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        #region Functions

        private void LoadEvents()
        {
            btCancel.Click += btCancelClick;
            btSave.Click += btSaveClick;
            Loaded += OnLoad;
        }

        private void LoadLanguage() {
            tbCode.SetValue(HintAssist.HintProperty, languaje.PageInventoryNewGroup.hintTbCode);
            tbName.SetValue(HintAssist.HintProperty, languaje.PageInventoryNewGroup.hintTbName);       
            btSave.Content = languaje.PageInventoryNewGroup.toolTipBtnSave;
            btCancel.Content = languaje.PageInventoryNewGroup.toolTipBtnCancel;
        }

        #endregion

        #region Events

        private void OnLoad(object o, EventArgs e) {
            if (EditMode)
            {
                tbCode.Text = InvGroup?.Code;
                tbName.Text = InvGroup?.Name;
            }
        }

        private void btSaveClick(object sender, EventArgs e)
        {
            string code = tbCode.Text;
            string name = tbName.Text;
            if (EditMode) controller.Update(new InvGroup(InvGroup._id, code, name));
            else controller.Create(new InvGroup(code, name));
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btCancelClick(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();          
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.X))
            {
                Close();
            }

            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.S))
            {
                MessageBox.Show("Guardar Datos");
            }
        }

        #endregion

        #region IInventoryGroupGUI

        public void Close()
        {
            OnClosed?.Invoke(this, new EventArgs());
            Main.MainWindow.Instace.CloseFrameDialog();
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
            OnOpen?.Invoke(this, new EventArgs());
            Main.MainWindow.Instace.GetFrameDialog().Content = this;
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
