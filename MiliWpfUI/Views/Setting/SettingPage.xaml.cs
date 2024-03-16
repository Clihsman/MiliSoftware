using DocumentFormat.OpenXml.Drawing.Diagrams;
using GrapInterCom.Interfaces.Setting;
using MiliSoftware.Setting;
using MiliSoftware.UI;
using MiliSoftware.Views.Main;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiliSoftware.Views.Setting
{
    /// <summary>
    /// Lógica de interacción para SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Page, ISettingGUI
    {
        public SettingController SettingController { get; set; }
        private SolidColorBrush SelectBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEAEAEA"));
        private bool activarA = true, activarB;

        public SettingPage() 
        { 
            InitializeComponent();
            LoadEvents();
        }

        private void LoadEvents() {
            bdGeneral.MouseEnter += (o, e) => {
                bdGeneral.Background = SelectBrush;
            };

            bdGeneral.MouseLeave += (o, e) => {
                if (!activarA)
                    bdGeneral.Background = Brushes.Transparent;
            };

            bdGeneral.MouseDown += (o,e) => { 
                activarA = true;
                activarB = false;
                bdGeneral.Background = SelectBrush;
                bdActA.Visibility = Visibility.Visible;
                bdActB.Visibility = Visibility.Hidden;
                bdAccount.Background = Brushes.Transparent;

               
            };

            bdAccount.MouseEnter += (o, e) => {
                bdAccount.Background = SelectBrush;
            };

            bdAccount.MouseLeave += (o, e) => {
                if (!activarB)
                    bdAccount.Background = Brushes.Transparent;
            };

            bdAccount.MouseDown += (o, e) => {
                activarB = true;
                activarA = false;
                bdAccount.Background = SelectBrush;
                //bdActB.Visibility = Visibility.Visible;

                bdActA1.Height = 0;
                bdActA1.Visibility = Visibility.Visible;

                bdGeneral.Background = Brushes.Transparent;

                DoubleAnimation thicknessAnimation = new DoubleAnimation(0, 25, TimeSpan.FromMilliseconds(200));
                thicknessAnimation.Completed += (i, z) => {
                    bdActA.Visibility = Visibility.Hidden;
                    bdActA1.Visibility = Visibility.Hidden;

                    bdActB1.Height = 0;
                    bdActB1.Visibility = Visibility.Visible;

                    DoubleAnimation anim2 = new DoubleAnimation(0, 45, TimeSpan.FromMilliseconds(200));
                    anim2.Completed += (v, b) => {
                        bdActB.Visibility = Visibility.Visible;
                        bdActB1.Visibility = Visibility.Hidden;
                    };

                    bdActB1.BeginAnimation(HeightProperty, anim2);

                };

                bdActA1.BeginAnimation(HeightProperty, thicknessAnimation);
               
            };
        }

        public void Close()
        {
            
        }

        public void Show()
        {
            Console.WriteLine("Show Dialog");
            MainWindow.Instace.GetFrameDialog().Content = this;
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        #region Events

        private void BtGeneralClick(object sender, EventArgs ev) {

        }

        #endregion
    }
}
