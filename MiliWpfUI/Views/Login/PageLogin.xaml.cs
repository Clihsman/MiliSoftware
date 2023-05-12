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

        public DialogResult DialogResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private LoginController controller;

        public PageLogin(Frame frame)
        {
            languaje.Init();
            InitializeComponent();
            LoadLanguage();
            LoadEvents();
            this.frame = frame;
            controller = new LoginController(this);
        }

        #region Functions

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
            tbUserPassword.PasswordChanged += TbUserPasswordTextChange;
            tbUserEmail.TextChanged += TbUserEmailTextChange;
            Loaded += PageLoaded;
        }

        #endregion

        #region Events

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            tbUserEmail.Focus();
        }

        private async void LoginBtnClick(object sender, RoutedEventArgs e)
        {
            if (!VerifyData()) return;
            DisableUI();

            bool logIn = await controller.LogIn();
            if (logIn)
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
                if (controller.GetError()?.Value<int>("statusCode") == 404)
                {
                    MessageBox.Show(controller.GetError()?.Value<string>("message"), "MiliSoftware: " + controller.GetError()?.Value<int>("statusCode"));
                }
                else if (controller.GetError()?.Value<int>("statusCode") == 401)
                {
                    MessageBox.Show("Correo o contraseña incorrecta");
                }
            }
            
            EnableUI();
        }
       
        private void DisableUI() {
            pgBar.Visibility = Visibility.Visible;
            tbUserEmail.IsEnabled = false;
            tbUserPassword.IsEnabled = false;
            signupBtn.IsEnabled = false;
            loginBtn.IsEnabled = false;
            pbTools.IsEnabled = false;
        }

        private void EnableUI()
        {
            pgBar.Visibility = Visibility.Collapsed;
            tbUserEmail.IsEnabled = true;
            tbUserPassword.IsEnabled = true;
            signupBtn.IsEnabled = true;
            loginBtn.IsEnabled = true;
            pbTools.IsEnabled = true;
        }

        private bool VerifyData()
        {
            bool target = true;

            if (string.IsNullOrWhiteSpace(tbUserEmail.Text))
            {
                tbAlertEmail.Visibility = Visibility.Visible;
                tbUserEmail.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FAABAB"));
                tbUserEmail.Focus();
                target = false;
            }
            else
            {
                tbAlertEmail.Visibility = Visibility.Collapsed;
                tbUserEmail.BorderBrush = (Brush)FindResource("MaterialDesignDivider");
            }

            if (string.IsNullOrWhiteSpace(tbUserPassword.Password))
            {
                tbAlertPassword.Visibility = Visibility.Visible;
                tbUserPassword.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FAABAB"));
                if(target) tbUserPassword.Focus();
                target = false;
            }
            else
            {
                tbAlertPassword.Visibility = Visibility.Collapsed;
                tbUserPassword.BorderBrush = (Brush)FindResource("MaterialDesignDivider");
            }

            return target;
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

        private void TbUserEmailTextChange(object sender, RoutedEventArgs e)
        {
            if (tbUserEmail.Text.Length > 0 && tbAlertEmail.Visibility == Visibility.Visible)
            {
                tbAlertEmail.Visibility = Visibility.Collapsed;
                tbUserEmail.BorderBrush = (Brush)FindResource("MaterialDesignDivider");
            }
        }

        private void TbUserPasswordTextChange(object sender, RoutedEventArgs e)
        {
            if (tbUserPassword.Password.Length > 0 && tbAlertPassword.Visibility == Visibility.Visible)
            {
                tbAlertPassword.Visibility = Visibility.Collapsed;
                tbUserPassword.BorderBrush = (Brush)FindResource("MaterialDesignDivider");
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            FormStart.Instace.DragMove();
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
