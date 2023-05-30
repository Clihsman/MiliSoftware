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

namespace MiliSoftware.Views.Alerts
{
    /// <summary>
    /// Lógica de interacción para PageDeleteNo.xaml
    /// </summary>
    public partial class PageDeleteNo : Page
    {
        public MessageBoxXAction MessageBoxXAction { get; private set; }

        public PageDeleteNo()
        {
            InitializeComponent();
            LoadLanguage();
            LoadEvents();

            MessageBoxXAction = new MessageBoxXAction();
        }

        #region Functions

        private void LoadLanguage()
        {
            btnNo.Content = "No";
            tbkDelete.Text = "Eliminar";
        }

        private void LoadEvents()
        {
            btnDelete.Click += btnDeleteClick;
            btnNo.Click += btnNoClick;
        }

        #endregion

        #region Events

        private void btnDeleteClick(object o, EventArgs e)
        {
            Main.MainWindow.Instace.CloseFrameDialog();
            MessageBoxXAction.action.Invoke(UI.DialogResult.Yes);
        }

        private void btnNoClick(object o, EventArgs e)
        {
            Main.MainWindow.Instace.CloseFrameDialog();
            MessageBoxXAction.action.Invoke(UI.DialogResult.No);
        }

        #endregion
    }
}
