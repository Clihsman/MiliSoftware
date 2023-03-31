using MiliSoftware.Login.View;
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

namespace MiliSoftware.Paginas.Login
{
    /// <summary>
    /// Lógica de interacción para PgVUser.xaml
    /// </summary>
    public partial class PgVUser : Page
    {
        private Frame parent_frame;

        public PgVUser(Frame frame)
        {
            InitializeComponent();
            parent_frame = frame;
        }

        private void TbC0_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbC1.Focus();
        }

        private void TbC1_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbC2.Focus();
        }

        private void TbC2_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbC3.Focus();
        }

        private void TbC3_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void PgVUser_Loaded(object sender, RoutedEventArgs e)
        {
            tbC0.Focus();
        }

        private void BtRegresar_Click(object sender, RoutedEventArgs e)
        {
            parent_frame.NavigationService.Navigate(new PgLogin(parent_frame));
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
