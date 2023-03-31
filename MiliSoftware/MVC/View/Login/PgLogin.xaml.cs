using MaterialDesignThemes.Wpf;
using MiliSoftware.Forms;
using MiliSoftware.Login.Controller;
using MiliSoftware.Paginas.Login;
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

namespace MiliSoftware.Login.View
{
    /// <summary>
    /// Lógica de interacción para PgLogin.xaml
    /// </summary>
    public partial class PgLogin : Page
    {
        private Frame frame;

        public Func<string,string, bool> onLogin;

        public PgLogin(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            /*
            MiliFileEngine.src.Resources.MiliResources.Externs.SetString(
                MiliFileEngine.src.Resources.ResHash.GetHash("LANGUAJE"), "{es:spanish}");
                */
            LoginController controller = new LoginController(this, new Model.LoginModel());
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            bool? login = onLogin?.Invoke(tbUserName.Text, tbPassword.Password);
            if (login == true)
            {
                Application.Current.MainWindow = new FormPrincipal();
                FormStart.GetWindow().Close();
                Application.Current.MainWindow.Show();
            }
            
        }

        private void SignupBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PgNewUser(frame));
        }

        private void help_click(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
