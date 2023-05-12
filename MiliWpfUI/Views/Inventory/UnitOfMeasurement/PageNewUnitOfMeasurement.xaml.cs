using MiliSoftware.WpfUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        private SymbolTextBox SymbolTextBox;

        public PageNewUnitOfMeasurement()
        {
            InitializeComponent();
            LoadControls();
            LoadLanguaje();
            string[] source = new string[] { "kg", "g", "mg" };
            int pointer = 0;

            

            tbMaxUnit.PreviewKeyDown += (o, e) => {
                if (e.Key == Key.Up)
                {
                    pointer++;
                    if (!(pointer < source.Length)) pointer = 0;
                    SymbolTextBox.Symbol = source[pointer];
                }

                if (e.Key == Key.Down)
                {
                    pointer--;
                    if (!(pointer > -1)) pointer = source.Length - 1;
                    SymbolTextBox.Symbol = source[pointer];
                }
            };

            cbUnit1.ItemsSource = source;
            cbUnit2.ItemsSource = source;
            btSave.Click += (o, e) =>
            {
                SymbolTextBox.Symbol = source[pointer++];
            };
        }

        private void LoadControls()
        {
            SymbolTextBox = new SymbolTextBox(tbMaxUnit,"kl");
        }

        private void LoadLanguaje() {
           
        }
    }
}
