/*
   Proyecto : MiliSoftware
   Desarrolador : Clihsman Isaac Iscala Salas
   Fecha de creación : 28/05/2022
   CC : 30.499.354
   Numero de telefono : 312-546-6498
   Empresa : AllSoftwareCompany
   SHA1 : 17D8720E-DE073486-DEC0B92D-AA43F809
*/

using MaterialDesignThemes.Wpf;
using MiliSoftware.Login.View;
using System.Windows;
using System.Windows.Controls;

// Pagina para crear un nuevo usuario
namespace MiliSoftware.Paginas.Login
{
    /// <summary>
    /// Lógica de interacción para PgNewUser.xaml
    /// </summary>
    public partial class PgNewUser : Page
    {
        // variable que contiene el contenedor principal
        private Frame parent_frame;
        // variable para el modo oscuro o claro
        public bool IsDarkMode { get; set; }
        // variable de paleta
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        // Metodo Constructor
        public PgNewUser(Frame frame)
        {
            InitializeComponent();
            parent_frame = frame;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tbUserName.Focus();
        }

        private void toogleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkMode = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkMode = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkMode = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void btRegresar_Click(object sender, RoutedEventArgs e)
        {
            parent_frame.NavigationService.Navigate(new PgLogin(parent_frame));
        }

        private void CrearBtn_Click(object sender, RoutedEventArgs e)
        {
            parent_frame.NavigationService.Navigate(new PgVUser(parent_frame));
        }
    }
}