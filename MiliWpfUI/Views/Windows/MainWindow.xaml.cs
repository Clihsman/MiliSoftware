using MaterialDesignThemes.Wpf;
using MiliSoftware.Customers;
using MiliSoftware.Email;
using MiliSoftware.Inventory;
using MiliSoftware.Suppliers;
using MiliSoftware.UI;
using MiliSoftware.Views.Customers;
using MiliSoftware.Views.Debug;
using MiliSoftware.Views.Email;
using MiliSoftware.Views.Inventory;
using MiliSoftware.Views.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MiliSoftware.Views.Main
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static PageMain Instace { private set; get; }
        private const string TITLE = "MiliSoftware";

        public MainWindow()
        {
            InitializeComponent();
            Instace = new PageMain();
            FContainer.Content = Instace;
        }

        protected override void OnClosed(EventArgs e)
        {
            Instace = null;
            base.OnClosed(e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
