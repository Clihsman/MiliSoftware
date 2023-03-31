﻿using GrapInterCom.Interfaces.Inventory;
using MaterialDesignThemes.Wpf;
using MiliSoftware.UI;
using MiliSoftware.WpfUI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Lógica de interacción para PageListCustomers.xaml
    /// </summary>
    public partial class PageProductComponents : Page, ProductComponentsGUI
    {
        private List<object> products;

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        public ICRUD<string, object[], string, string> controller { get; set; }
        public DialogResult DialogResult { get; set; }
        private Frame parent;

        public PageProductComponents(Frame parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            IntTextBox intText = new IntTextBox(tbCant);

            products = new List<object>();
            products.AddRange(controller.Read(null));

            lvProducts.ItemsSource = products;

            var myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(800));
            MySnackbar.MessageQueue = myMessageQueue;

            LoadLanguage();
            LoadEvents();
            RemoveFromExistingProducts();
        }

        private void RemoveFromExistingProducts()
        {
            foreach (dynamic product in products.ToArray())
            {
                foreach (dynamic component in lvProductsComponets.Items)
                {
                    if (product.Codigo == component.Codigo)
                        products.Remove(product);
                }
            }

            lvProducts.ItemsSource = null;
            lvProducts.ItemsSource = products;
        }

        private void LoadEvents() {
            lvProducts.SelectionChanged += lvProductsSelectItem;
            lvProducts.MouseDoubleClick += lvProductMouseDoubleClick;
            lvProducts.KeyDown += lvProductsKeyDown;
            lvProductsComponets.SelectionChanged += lvProductsComponetsSelectItem;
            btSearch.Click += btSearchClick;
            btAdd.Click += btAddClick;
            btRemove.Click += btRemoveClick;
            btExit.Click += btExitClick;
            btSave.Click += btSaveClick;
        }

        private void LoadLanguage()
        {
            // ToolBar
            tbProducts.Text = languaje.PageProductComponents.textTbProducts;
            tbProductsComponents.Text = languaje.PageProductComponents.textTbProductsComponents;
            tbSearch.SetValue(HintAssist.HintProperty, languaje.PageProductComponents.hintTbSearch);
            btSearch.ToolTip = languaje.PageProductComponents.toolTipBtSearchProduct;
            tbCant.SetValue(HintAssist.HintProperty, languaje.PageProductComponents.hintTbCant);
            btRemove.ToolTip = languaje.PageProductComponents.toolTipBtRemove;
            btAdd.ToolTip = languaje.PageProductComponents.toolTipBtAdd;
            btExit.ToolTip = languaje.PageProductComponents.toolTipBtExit;
            btSave.ToolTip = languaje.PageProductComponents.toolTipBtSave;
            //************

            // List View Products
            lvHeadersProducts.Columns.Add(new GridViewColumn() { Header = languaje.PageProductComponents.headCode, DisplayMemberBinding = new Binding("Codigo") });
            lvHeadersProducts.Columns.Add(new GridViewColumn() { Header = languaje.PageProductComponents.headName, DisplayMemberBinding = new Binding("Nombre") });
            //************

            // List View Products Components
            lvHeadersProductsComponets.Columns.Add(new GridViewColumn() { Header = languaje.PageProductComponents.headCode, DisplayMemberBinding = new Binding("Codigo") });
            lvHeadersProductsComponets.Columns.Add(new GridViewColumn() { Header = languaje.PageProductComponents.headName, DisplayMemberBinding = new Binding("Nombre") });
            lvHeadersProductsComponets.Columns.Add(new GridViewColumn() { Header = languaje.PageProductComponents.headAmount, DisplayMemberBinding = new Binding("Cantidad") });
            //************
        }

        #region Events

        private void lvProductsSelectItem(object sender, SelectionChangedEventArgs e)
        {
            bool enable = lvProducts.SelectedItem != null;
            btAdd.IsEnabled = enable;
            tbCant.IsEnabled = enable;
        }

        private void lvProductsComponetsSelectItem(object sender, SelectionChangedEventArgs e)
        {
            bool enable = lvProductsComponets.SelectedItem != null;
            btRemove.IsEnabled = enable;
        }

        private void lvProductMouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext;
            if (item != null && !(item is string))
            {
                tbCant.Focus();
            }
        }

        private void btAddClick(object sender, EventArgs e)
        {
            dynamic product = lvProducts.SelectedItem;

            string codigo = product.Codigo;
            string nombre = product.Nombre;
            string cant = tbCant.Text;

            if (cant.Length > 0 && cant != "0")
            {
                DObject dObject = new DObject(new Dictionary<string, object>() { 
                    {"Codigo", codigo},
                    {"Nombre", nombre},
                    { "Cantidad",cant}});

                lvProductsComponets.Items.Add(dObject);

                products.Remove(product);
                lvProducts.ItemsSource = null;
                lvProducts.ItemsSource = products;
                tbCant.Text = "0";
                btSave.IsEnabled = true;
            }
            else
            {
                MySnackbar.MessageQueue.Enqueue("La cantidad debe ser mayor a cero");
            } 
        }

        private void btRemoveClick(object sender, EventArgs e)
        {
            dynamic product = lvProductsComponets.SelectedItem;

            string codigo = product.Codigo;
            string nombre = product.Nombre;

            DObject dObject = new DObject(new Dictionary<string, object>() {
                        {"Codigo", codigo },
                        {"Nombre", nombre }});

            products.Add(dObject);
            lvProducts.ItemsSource = null;
            lvProducts.ItemsSource = products;

            lvProductsComponets.Items.Remove(lvProductsComponets.SelectedItem);
            btSave.IsEnabled = true;
        }

        private void btSearchClick(object o, EventArgs e)
        {
            if (tbSearch.Text == "*")
            {
                lvProducts.ItemsSource = products;
            }
            else
            {
                string search = tbSearch.Text.ToUpper();
                List<dynamic> achieved = new List<dynamic>();

                foreach (dynamic product in products)
                {
                    if (product.Codigo == search || product.Nombre.ToUpper().Contains(search))
                    {
                        achieved.Add(product);
                    }
                }

                lvProducts.ItemsSource = achieved;
            }
        }

        private void btExitClick(object o, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btSaveClick(object o, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lvProductsKeyDown(object o, KeyEventArgs e)
        {
            if (lvProducts.SelectedItem != null && e.Key == Key.Enter)
                tbCant.Focus();
        }

        #endregion

        #region ProductComponentsGUI

        public object[] GetValues()
        {
            List<DObject> components = new List<DObject>();

            foreach (DObject component in lvProductsComponets.Items)
            {
                components.Add(component);
            }

            return components.ToArray();
        }

        public void SetValues(object[] data)
        {
            if (data != null && data.Length > 0)
            {
                foreach (object product in data)
                {
                    lvProductsComponets.Items.Add(product);
                }
            }
        }

        public void Show()
        {
            parent.NavigationService.Navigate(null);
            parent.NavigationService.Navigate(this);
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