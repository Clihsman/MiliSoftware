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
using System.IO;
using GrapInterCom.Interfaces.Email;
using MiliSoftware.UI;

namespace MiliSoftware.Views.Email
{
    /// <summary>
    /// Lógica de interacción para PageEmailView.xaml
    /// </summary>
    public partial class PageEmailView : Page, IEmailViewGUI
    {
        private string tempHtml;

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        public ICRUD<string, object[], string, string> controller { get; set; }
        public DialogResult DialogResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private Frame parent;

        public PageEmailView(Frame parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        public void SetData(string sender,string time,string subject, string body) {
            tbName.Text = sender;
            tbTime.Text = time;
            tbSubject.Text = subject;
            tempHtml = System.IO.Path.GetTempPath() + "miliEmail.html";
            File.WriteAllText(tempHtml, body);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEvents();
            webView.Load("file:///" + tempHtml);
        }

        private void LoadEvents() {
            btClose.Click += (o, e) => Close();

            webView.LoadError += delegate {
                MessageBox.Show("Error");
            };

            webView.FrameLoadStart += delegate {
                Dispatcher.Invoke(new Action(() => progBar.Visibility = Visibility));
            };

            webView.FrameLoadEnd += delegate {
                Dispatcher.Invoke(new Action(() => progBar.Visibility = Visibility.Hidden));
            };
        }

        #region IEmailViewGUI

        public void Show()
        {
            parent.Content = this;
            OnOpen?.Invoke(this, new EventArgs());
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
            webView.Dispose();
            OnClosed?.Invoke(this, new EventArgs());
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

        #endregion
    }
}
