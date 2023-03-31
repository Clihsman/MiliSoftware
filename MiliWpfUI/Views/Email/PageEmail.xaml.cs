using GrapInterCom.Interfaces.Email;
/*
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
*/
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using MiliSoftware.UI;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class PageEmail : Page, IEmailGUI
    {
        private Dictionary<StackPanel, string> files = new Dictionary<StackPanel, string>();

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        public ICRUD<string, object[], string, string> controller { get; set; }
        public DialogResult DialogResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private Frame parent;

        private string from;
        private string to;
        private string subject;
        private string body;

        public PageEmail(Frame parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEvents();
            LoadLanguage();
        }

        #region Functions

        private void LoadEvents() {
            btSend.Click += btSendClick;
            btAttached.Click += btAttachedClick;
            btClose.Click += btCloseClick;
        }

        private void LoadLanguage() {

            tbTo.SetValue(HintAssist.HintProperty, languaje.PageEmail.hintTbTo);
            tbSubject.SetValue(HintAssist.HintProperty, languaje.PageEmail.hintTbSubject);
            tbContent.SetValue(HintAssist.HintProperty, languaje.PageEmail.hintTbContent);
            btClose.ToolTip = languaje.PageEmail.toolTipBtClose;
            btSend.ToolTip = languaje.PageEmail.toolTipBtSend;
            btAttached.ToolTip = languaje.PageEmail.toolTipBtAttached;
        }

        private void CreateControl(string filePath)
        {
            // Controls
            PackIcon icon = new PackIcon();
            icon.Foreground = Brushes.Black;
            icon.Margin = new Thickness(0, 0, 5, 0);
            icon.Width = 20;
            icon.Height = 20;
            icon.HorizontalAlignment = HorizontalAlignment.Left;
            icon.Kind = GetPackIcon(System.IO.Path.GetExtension(filePath).ToUpper());

            TextBlock textBlock = new TextBlock();
            textBlock.FontFamily = new FontFamily("Comic Sans MS");
            textBlock.FontSize = 14;
            textBlock.Text = System.IO.Path.GetFileName(filePath);
            // ********

            // Container
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Margin = new Thickness(0, 0, 10, 0);
            stackPanel.Height = 20;
            stackPanel.ContextMenu = new ContextMenu();
            var menuItem = new MenuItem
            {
                Header = languaje.PageEmail.contentBtDelete,
                Icon = new PackIcon
                {
                    Foreground = Brushes.Black,
                    Kind = PackIconKind.DeleteOutline
                },
            };
            menuItem.Click += delegate {
                files.Remove(stackPanel);
            };
            stackPanel.ContextMenu.Items.Add(menuItem);
            stackPanel.Children.Add(icon);
            stackPanel.Children.Add(textBlock);
            // ********

            files.Add(stackPanel, filePath);
            wpFiles.Children.Add(stackPanel);
        }

        private PackIconKind GetPackIcon(string extension)
        {

            if (Compare(extension, ".PNG", ".JPG", "JPEG", ".BMP", ".GIF", ".TIF", ".HEIC"))
                return PackIconKind.FileImageOutline;

            if (Compare(extension, ".DOCX", ".DOCM", ".DOTX", ".DOTM", ".CSV"))
                return PackIconKind.MicrosoftWord;

            if (Compare(extension, ".XLSX", ".XLSM", ".XLTX", "	.XLTM", ".XLSB", ".XLAM"))
                return PackIconKind.MicrosoftExcel;

            if (Compare(extension, ".PPTX", ".PPTM", ".POTX", ".POTM", ".PPAM", ".PPSX", ".PPSM", ".SLDX", ".SLDM", ".THMX"))
                return PackIconKind.FilePowerpointBoxOutline;

            if (Compare(extension, ".ZIP", ".RAR", ".TAR", ".GZIP"))
                return PackIconKind.ZipBoxOutline;

            if (Compare(extension, ".MP3", ".WAV", ".OGG"))
                return PackIconKind.FileMusicOutline;

            if (Compare(extension, ".MP4", ".MOV", ".FLV", ".WMV"))
                return PackIconKind.FileVideoOutline;

            return PackIconKind.FileOutline;
        }

        private bool Compare(string src, params string[] values)
        {
            foreach (string value in values)
                if (value == src) return true;
            return false;
        }

        #endregion

        #region Events

        private void btAttachedClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog().Value)
                CreateControl(openFileDialog.FileName);
        }

        private void btSendClick(object sender, EventArgs e) {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                Host = "smtp.gmail.com",
                Port = 465,
                UserName = "milisoftware.asc@gmail.com",
                Passworld = "krjwsyjklxweshdu",
                SecureSocket = "ssl"
            });

            from = "milisoftware.asc@gmail.com";
            to = tbTo.Text;
            subject = tbSubject.Text;
            body = tbContent.Text;

            Task.Run(()=> {
                Dispatcher.Invoke(new Action(delegate
                {
                    grData.IsEnabled = false;
                    stTools.IsEnabled = false;
                    progBar.Visibility = Visibility.Visible;
                }));

                if (!controller.Create(data))
                {
                    Dispatcher.Invoke(new Action(delegate
                    {
                        MessageBox.Show("Error al Enviar el Mensaje");
                        grData.IsEnabled = true;
                        stTools.IsEnabled = true;
                        progBar.Visibility = Visibility.Hidden;
                    }));
                }
                else {

                    Dispatcher.Invoke(new Action(delegate
                    {
                        MessageBox.Show("Mensaje Enviado Correctamente");
                        Close();
                    }));
                }
            });
        }

        private void btCloseClick(object sender, EventArgs e) {
            Close();
        }

        #endregion

        #region IEmailGUI

        public string GetTo()
        {
            return to;
        }

        public string GetFrom()
        {
            return from;
        }

        public string GetBody()
        {
            return body;
        }

        public string GetSubject()
        {
            return subject;
        }

        public string[] GetFiles()
        {
            return files.Values.ToArray();
        }

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
