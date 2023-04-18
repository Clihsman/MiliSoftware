using MiliSoftware.GUI.Styles;
using MiliSoftware.Views.Inventory.UnitOfMeasurement;
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

namespace MiliSoftware.Views.Inventory
{
    /// <summary>
    /// Lógica de interacción para PageUnitOfMeasurement.xaml
    /// </summary>
    public partial class PageUnitOfMeasurement : Page
    {
        public PageUnitOfMeasurement()
        {
            InitializeComponent();
            loadLanguaje();
            LoadStyles();
            LoadEvents();
            LoadData();
        }

        private void loadLanguaje() {
            btNew.ToolTip = languaje.PageUnitOfMeasurement.toolTipBtnNew;
            btDelete.ToolTip = languaje.PageUnitOfMeasurement.toolTipBtnDelete;
            btEdit.ToolTip = languaje.PageUnitOfMeasurement.toolTipBtnEdit;
            btExport.ToolTip = languaje.PageUnitOfMeasurement.toolTipBtnExport;
        }

        private void LoadStyles() {
            // Codigo Basura

            StyleSheet styleSheet = StyleSheet.Load("assets/styles/coloredStyle.json");

            if (styleSheet.GetFormStyle("form_inventory") != null)
            {
                btDelete.Foreground = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_delete").GetForegroundBrush();
                btDelete.BorderBrush = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_delete").GetBackgroundBrush();
                btDelete.Background = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_delete").GetBackgroundBrush();

                btNew.Foreground = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_new").GetForegroundBrush();
                btNew.BorderBrush = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_new").GetBackgroundBrush();
                btNew.Background = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_new").GetBackgroundBrush();

                btEdit.Foreground = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_edit").GetForegroundBrush();
                btEdit.BorderBrush = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_edit").GetBackgroundBrush();
                btEdit.Background = styleSheet.GetFormStyle("form_inventory").GetButtonStyle("btn_edit").GetBackgroundBrush();
            }
            //
        }

        private void LoadData() {
            lvHeaders.Columns.Add(new GridViewColumn() { Header = "Nombre" , DisplayMemberBinding = new Binding("Nombre") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = "Unidad" , DisplayMemberBinding = new Binding("Unidad") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = "Unidad maxima", DisplayMemberBinding = new Binding("UnidadMaxima") });
            lvHeaders.Columns.Add(new GridViewColumn() { Header = "Dependencias", DisplayMemberBinding = new Binding("Dependencias") });

            lvUnitOfMeasurement.ItemsSource = new List<DObject>() {
                new DObject(new Dictionary<string, object>()
                {
                    { "Nombre", "Kilo" },
                    { "Unidad", "kg" },
                    { "UnidadMaxima", 1000 },
                    { "Dependencias", "kg, g" }
                }),
                new DObject(new Dictionary<string, object>()
                {
                    { "Nombre","Gramo" },
                    { "Unidad", "g" },
                     { "UnidadMaxima", 1000 },
                    { "Dependencias", "Sin datos" }
                }),
                 new DObject (new Dictionary<string, object>()
                {
                    {"Nombre","Tonelada"},
                    {"Unidad", "t"},
                    { "UnidadMaxima", 1000 },
                    { "Dependencias", "t, kg, g" }
                })
            };
        }

        private void LoadEvents() {
            btNew.Click += (o,e) => {
                Main.MainWindow.Instace.dialogFrame.Content = new PageNewUnitOfMeasurement();
            };
        }
    }
}
