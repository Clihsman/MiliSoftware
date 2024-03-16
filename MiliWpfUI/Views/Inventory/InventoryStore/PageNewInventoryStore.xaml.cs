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
using InvStore = MiliSoftware.InventoryStore;
namespace MiliSoftware.Views.Inventory.InventoryStore
{
    /// <summary>
    /// Lógica de interacción para PageNewInventoryStore.xaml
    /// </summary>
    public partial class PageNewInventoryStore : Page, IInventoryStoreGUI
    {
        public bool EditMode { get; set; }
        public InvStore InvStore { get; set; }

        public PageNewInventoryStore()
        {
            InitializeComponent();
            LoadEvents();
            LoadLanguage();
            btCancel.Focus();
        }

        public DialogResult DialogResult { get; set; }
        public ICRUD<InvStore, InvStore[], InvStore, InvStore> controller { get; set; }

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        #region Functions

        private void LoadEvents()
        {
            btCancel.Click += btCancelClick;
            btSave.Click += btSaveClick;
            Loaded += OnLoad;
        }

        private void LoadLanguage()
        {
            tbCode.SetValue(HintAssist.HintProperty, languaje.PageInventoryNewGroup.hintTbCode);
            tbName.SetValue(HintAssist.HintProperty, languaje.PageInventoryNewGroup.hintTbName);
            btSave.Content = languaje.PageInventoryNewGroup.toolTipBtnSave;
            btCancel.Content = languaje.PageInventoryNewGroup.toolTipBtnCancel;
        }

        #endregion

        #region Events

        private void OnLoad(object o, EventArgs e)
        {
            if (EditMode)
            {
                tbCode.Text = InvStore?.Code;
                tbName.Text = InvStore?.Name;
            }
        }

        private void btSaveClick(object sender, EventArgs e)
        {
            string code = tbCode.Text;
            string name = tbName.Text;
            if (EditMode) controller.Update(new InvStore(InvStore._id, code, name));
            else controller.Create(new InvStore(code, name));
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btCancelClick(object sender, EventArgs e)
        {
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

        #region IInventoryStoreGUI
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
