/*
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
*/
using MaterialDesignThemes.Wpf;
using MiliSoftware.Login.Controller;
using MiliSoftware.Paginas;
using MiliSoftware.Paginas.Login;
using MiliSoftware.UI;
using MiliSoftware.Views.Main;
using MiliWpfUI;
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
    public partial class PageLogin : Page, ILoginGUI
    {
        private Frame frame;

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;
        public event Func<bool> OnLogin;

        public ICRUD<string, object[], string, string> controller { get; set; }
        public DialogResult DialogResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        LoginController con;

        public PageLogin(Frame frame)
        {
            languaje.Init();
            InitializeComponent();
            LoadLanguage();
            LoadEvents();
            this.frame = frame;
            con = new LoginController(this);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            FormStart.Instace.DragMove();
        }
        
        private void LoadLanguage()
        {
            tbWelcome.Text = languaje.PageLogin.contentTbWelcome;
            tbInfo.Text = languaje.PageLogin.contentTbInfo;
            tbUserEmail.SetValue(HintAssist.HintProperty, languaje.PageLogin.hintTbEmail);
            tbUserPassword.SetValue(HintAssist.HintProperty, languaje.PageLogin.hintTbPassword);
            loginBtn.Content = languaje.PageLogin.contentBtnLogin;
            signupBtn.Content = languaje.PageLogin.contentBtnSignup;
            btnHelp.Content = languaje.PageLogin.contentBtnHelp;
            btnExit.Content = languaje.PageLogin.contentBtnExit;
            btnHelp.ToolTip = languaje.PageLogin.toolTipBtnHelp;
            btnExit.ToolTip = languaje.PageLogin.toolTipBtnExit;
        }

        private void LoadEvents()
        {
            loginBtn.Click += LoginBtnClick;
            signupBtn.Click += SignupBtnClick;
            btnExit.Click += BtnExitClick;
            btnHelp.Click += BtnHelpClick;
            tbUserPassword.KeyDown += TbUserPasswordKeyDown;
            Loaded += PageLoaded;
        }

        #region Events

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            tbUserEmail.Focus();
        }

        private void LoginBtnClick(object sender, RoutedEventArgs e)
        {        
            bool login = OnLogin?.Invoke() is true;
            
            if (login)
            {
                /*
                Application.Current.MainWindow = new MainWindow();
                FormStart.Instace.Close();
                Application.Current.MainWindow.Show();
                */
                FormStart.Instace.myFrame.NavigationService.Navigate(new PgStart() { showWindows = true });
            }
            else
            {
                if (con.GetError()?.Value<int>("statusCode") == 404)
                    MessageBox.Show("Error de conexion con el servidor");
                else
                    MessageBox.Show("Correo o contraseña incorrecta");
            }
        }

        private void SignupBtnClick(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PgNewUser(frame));
        }

        private void BtnExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnHelpClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Conected");
        }

        private void TbUserPasswordKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) LoginBtnClick(sender, e);
        }

        #endregion

        #region ILoginGUI

        public object[] GetValues()
        {
            throw new NotImplementedException();
        }

        public void SetValues(object[] datas)
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        public IGUI<string, object[], string, string> GetParent()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public int GetWidth()
        {
            throw new NotImplementedException();
        }

        public int GetHeight()
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

        public ScreenNumber GetScreen()
        {
            throw new NotImplementedException();
        }

        public string GetTitle()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public GUIState GetGUIState()
        {
            throw new NotImplementedException();
        }

        public object GetGUI()
        {
            throw new NotImplementedException();
        }

        public IntPtr GetHandle()
        {
            throw new NotImplementedException();
        }

        public string GetUserEmail()
        {
            return tbUserEmail.Text;
        }

        public string GetUserPassword()
        {
            return tbUserPassword.Password;
        }

        #endregion
    }
}
