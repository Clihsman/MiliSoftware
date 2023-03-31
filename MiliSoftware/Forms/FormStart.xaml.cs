using MiliSoftware.Login.View;
using MiliSoftware.Paginas;
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

namespace MiliSoftware.Forms
{
    /// <summary>
    /// Lógica de interacción para FormStart.xaml
    /// </summary>
    public partial class FormStart : Window
    {
        private static FormStart instance;

        public FormStart()
        {
            InitializeComponent();
            myFrame.NavigationService.Navigate(new PgStart());
            instance = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var pgLogin = new PgLogin(myFrame);

            Thread hilo1 = new Thread(new ThreadStart(delegate {
                //Thread.Sleep(2800);
                languaje.Init();
                Dispatcher.Invoke(delegate {
                    myFrame.NavigationService.Navigate(pgLogin);
                   
                });
            }));
            hilo1.Start();
        }

        public static FormStart GetWindow() {
            return instance;
        }
    }
}
