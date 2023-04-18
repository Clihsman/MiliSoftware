using MiliSoftware.WpfUI;
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

namespace MiliSoftware.Views.Inventory.UnitOfMeasurement
{
    /// <summary>
    /// Lógica de interacción para PageNewUnitOfMeasurement.xaml
    /// </summary>
    public partial class PageNewUnitOfMeasurement : Page
    {
        public PageNewUnitOfMeasurement()
        {
            InitializeComponent();
            LoadControls();
            cbUnit1.ItemsSource = new string[] {"kg","g","mg"};
            cbUnit2.ItemsSource = new string[] { "kg", "g", "mg" };
            cbUnit3.ItemsSource = new string[] { "kg", "g", "mg" };
            cbUnit4.ItemsSource = new string[] { "kg", "g", "mg" };
            cbUnit5.ItemsSource = new string[] { "kg", "g", "mg" };
            cbUnit6.ItemsSource = new string[] { "kg", "g", "mg" };
            cbUnit7.ItemsSource = new string[] { "kg", "g", "mg" };
            cbUnit8.ItemsSource = new string[] { "kg", "g", "mg" };
            cbUnit9.ItemsSource = new string[] { "kg", "g", "mg" };
        }

        private void LoadControls()
        {
            IntTextBox intTextBox = new IntTextBox(tbMaxUnit);
        }
    }
}
