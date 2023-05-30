using GrapInterCom.Interfaces.Email;
/*
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
*/
using MiliSoftware.Email;
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

namespace MiliSoftware.Views.Email
{
    /// <summary>
    /// Lógica de interacción para PageEmail.xaml
    /// </summary>
    public partial class PageEmails : Page, IEmailsGUI
    {
        public ICRUD<string, object[], string, string> controller { get; set; }
        public DialogResult DialogResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        private Frame parent;
        object[] emails = null;
        private string emailType = "received";

        public PageEmails(Frame parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ((EmailsController)controller).Connect("imap.gmail.com", 993, "milisoftware.asc@gmail.com", "krjwsyjklxweshdu", "ssl");

            cbType.DisplayMemberPath = "Header";
            cbType.ItemsSource = new object[] {
                        new { Header="Recibidos",Value="received" },
                        new { Header="Enviados",Value="sent" }
                    };
            cbType.SelectedIndex = 0;

            LoadData();
            LoadEvents();
        }

        #region Events

        private void btNewClick(object sender,EventArgs e) {
            PageEmail pageEmail = new PageEmail(Main.MainWindow.Instace.GetFrameDialog());
          //  Main.MainWindow.Instace.parentFrame.IsEnabled = false;
            pageEmail.OnClosed += delegate {
            //    Main.MainWindow.Instace.dialogFrame.Content = null;
              //  Main.MainWindow.Instace.parentFrame.IsEnabled = true;
            };
            EmailController emailController = new EmailController(pageEmail);
        }

        private void lvEmailsMouseDoubleClick(object sender, MouseEventArgs e)
        {            
            var item = ((FrameworkElement)e.OriginalSource).DataContext;
            if (item != null && !(item is string))
            {            
                dynamic dObject = (DObject)lvEmails.SelectedItem;          
                int Id = (int)dObject.Id;
                dynamic message = ((EmailsController)controller).GetMenssage("received", Id);
                string from = message.From;
                string subject = message.Subject;

               // Main.MainWindow.Instace.parentFrame.IsEnabled = false;
                PageEmailView pageEmailView = new PageEmailView(Main.MainWindow.Instace.GetFrameDialog());
                pageEmailView.SetData(from, message.Date, subject, message.Body);
                pageEmailView.OnClosed += delegate {
                  //  Main.MainWindow.Instace.dialogFrame.Content = null;
                  //  Main.MainWindow.Instace.parentFrame.IsEnabled = true;
                };
                EmailViewController emailViewController = new EmailViewController(pageEmailView);
            }
            
        }

        private void cbTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic value = cbType.SelectedItem;
            emailType = value.Value;
            LoadData();
        }

        #endregion

        #region Functions

        private void LoadEvents()
        {
            lvEmails.MouseDoubleClick += lvEmailsMouseDoubleClick;
            btNew.Click += btNewClick;
            cbType.SelectionChanged += cbTypeSelectionChanged;
        }    

        private void LoadData() {
            stTools.Visibility = Visibility.Hidden;
            progBar.Visibility = Visibility.Visible;

           Task.Run(() => {
                LoadEmails();
                Dispatcher.Invoke(new Action(delegate
                {
                    lvHeaders.Columns.Clear();

                    if (emailType == "received")
                    {
                        lvHeaders.Columns.Add(new GridViewColumn() { Header = "Nombre", DisplayMemberBinding = new Binding("Name") });
                        lvHeaders.Columns.Add(new GridViewColumn() { Header = "Correo", DisplayMemberBinding = new Binding("Email") });
                        lvHeaders.Columns.Add(new GridViewColumn() { Header = "Asunto", DisplayMemberBinding = new Binding("Subject") });
                        lvHeaders.Columns.Add(new GridViewColumn() { Header = "Fecha", DisplayMemberBinding = new Binding("Date") });
                    }

                    if (emailType == "sent")
                    {
                        lvHeaders.Columns.Add(new GridViewColumn() { Header = "Para", DisplayMemberBinding = new Binding("Name") });
                        lvHeaders.Columns.Add(new GridViewColumn() { Header = "Asunto", DisplayMemberBinding = new Binding("Subject") });
                        lvHeaders.Columns.Add(new GridViewColumn() { Header = "Fecha", DisplayMemberBinding = new Binding("Date") });
                    }

                    lvEmails.ItemsSource = null;
                    lvEmails.ItemsSource = emails;
                    stTools.Visibility = Visibility.Visible;
                    progBar.Visibility = Visibility.Hidden;
                }));
            });
        }

        private void LoadEmails() {
           
            emails = ((EmailsController)controller).GetEmails(emailType);
          //  Close();
        }

        #endregion

        #region IEmailGUI

        public void Close()
        {
            ((EmailsController)controller).Disconnect();
            OnClosed?.Invoke(this, new EventArgs());
        }

        public object GetGUI()
        {
            throw new NotImplementedException();
        }

        public GUIState GetGUIState()
        {
            throw new NotImplementedException();
        }

        public IntPtr GetHandle()
        {
            throw new NotImplementedException();
        }

        public int GetHeight()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public IGUI<string, object[], string, string> GetParent()
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

        public object[] GetValues()
        {
            throw new NotImplementedException();
        }

        public int GetWidth()
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

        public void SetValues(object[] datas)
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            OnOpen?.Invoke(this, new EventArgs());
            parent.Content = this;
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
