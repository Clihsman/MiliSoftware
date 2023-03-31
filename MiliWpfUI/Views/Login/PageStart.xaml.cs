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

namespace MiliSoftware.Paginas
{
    /// <summary>
    /// Lógica de interacción para PgStart.xaml
    /// </summary>
    public partial class PgStart : Page
    {
        public bool showWindows = false;
        private const int ticks = 10;
        public PgStart()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(showWindows)
            Task.Run(delegate
            {
             //   System.Threading.Thread.Sleep(300);
                for (int i = 0; i < ticks; i++)
                {
                    Dispatcher.Invoke(() => prodressBar.Value++);
                    System.Threading.Thread.Sleep(50);
                }

                Dispatcher.Invoke(
                    delegate
                    {
                        Application.Current.MainWindow = new MainWindow();
                        FormStart.Instace.Close();
                        Application.Current.MainWindow.Show();
                    });

            });
        }
    }
}
