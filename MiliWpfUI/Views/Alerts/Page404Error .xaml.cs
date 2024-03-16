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
    /// Lógica de interacción para Page500Error.xaml
    /// </summary>
    public partial class Page404Error : Page
    {
        public MessageBoxXAction MessageBoxXAction { get; private set; } = new MessageBoxXAction();
        
        public Page404Error()
        {
            InitializeComponent();
            LoadEvents();
        }

        private void LoadEvents() {
            btnAceptar.Click += BtAceptarClick;
        }

        private void BtAceptarClick(object sender, EventArgs ev) {
            Main.MainWindow.Instace.CloseFrameDialog();
            MessageBoxXAction?.action?.Invoke(UI.DialogResult.OK);
        }
    }
}
