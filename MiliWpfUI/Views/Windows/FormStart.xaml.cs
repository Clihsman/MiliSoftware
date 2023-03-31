using MiliSoftware.Login.View;
using MiliSoftware.Paginas;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MiliSoftware
{
    /// <summary>
    /// Lógica de interacción para FormStart.xaml
    /// </summary>
    public partial class FormStart : Window
    {
        public static FormStart Instace { private set; get; }

        public FormStart()
        {
            InitializeComponent();
            myFrame.NavigationService.Navigate(new PgStart());
            Instace = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {     
                SaveBuild("MiliSoftware");
                var pgLogin = new PageLogin(myFrame);

                Thread hilo1 = new Thread(new ThreadStart(delegate
                {
                    //Thread.Sleep(2800);
                    Dispatcher.Invoke(delegate
                    {
                        myFrame.NavigationService.Navigate(pgLogin);

                    });
                }));
                hilo1.Start();
        }

        public static void SaveBuild(string build)
        {
            int numberBuild = 0;

            using (FileStream file = new FileStream(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), build + ".txt"), FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                if (file.Length > 0)
                {
                    StreamReader reader = new StreamReader(file);
                    string content = reader.ReadToEnd();
                    int.TryParse(content, out numberBuild);

                    file.Seek(0, SeekOrigin.Begin);
                }

                StreamWriter writer = new StreamWriter(file);
                writer.Write(numberBuild + 1);
                writer.Flush();
            }
        }
    }
}
