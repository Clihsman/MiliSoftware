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

namespace MiliSoftware.Views.Alerts
{
    /// <summary>
    /// Lógica de interacción para Alert.xaml
    /// </summary>
    public partial class Alert : Page
    {
        private IAction action;

        public Alert()
        {
            InitializeComponent();
        }

        public Alert(IAction action)
        {
            InitializeComponent();
            this.action = action;
            btnYes.Click += (o,e) => btnYesClick();
            btnNo.Click += (o, e) => btnNoClick();
        }

        private void btnYesClick() {
            action?.action?.Invoke(DialogResult.Yes);
            Main.MainWindow.Instace.CloseFrameDialog();
        }

        private void btnNoClick()
        {
            action?.action?.Invoke(DialogResult.No);
            Main.MainWindow.Instace.CloseFrameDialog();
        }

        public static IAction ShowDialog() {
            IAction action = new IAction();
            Alert alert = new Alert(action);
            Main.MainWindow.Instace.GetFrameDialog().Content = alert;
            return action;
        }
    }

    public class IAction {
        public Action<DialogResult> action { get; private set; }
        public void Then(Action<DialogResult> action) {
            this.action = action;
        }
    }
}
