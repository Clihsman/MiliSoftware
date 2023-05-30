using GrapInterCom.Interfaces.Export;
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

namespace MiliSoftware.Views.Export
{
    /// <summary>
    /// Lógica de interacción para PageExport.xaml
    /// </summary>
    public partial class PageExport : Page, IExportGUI
    {
        public DialogResult DialogResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICRUD<object, object, object, object> controller { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public PageExport()
        {
            InitializeComponent();
            LoadEvents();
            LoadLanguaje();
        }

        #region Variables

        public event EventHandler OnOpen;
        public event EventHandler OnClosed;

        #endregion

        #region Functions

        private void LoadLanguaje()
        {
            btExportExcel.ToolTip = languaje.PageExport.toolTipBtnExportExcel;
            btExportWord.ToolTip = languaje.PageExport.toolTipBtnExportWord;
            btExportPdf.ToolTip = languaje.PageExport.toolTipBtnExportPdf;
            btExportPng.ToolTip = languaje.PageExport.toolTipBtnExportPng;
        }

        private void LoadEvents()
        {
            // button export excel
            btExportExcel.MouseEnter += IconMouseEnter;
            btExportExcel.MouseLeave += IconMouseLeave;
            btExportExcel.MouseDown += (o, e) => {
                progBar.Visibility = Visibility.Visible;
            };
            // button export word
            btExportWord.MouseEnter += IconMouseEnter;
            btExportWord.MouseLeave += IconMouseLeave;
            btExportWord.MouseDown += (o, e) => {
                progBar.Visibility = Visibility.Visible;
            };

            // button export pdf
            btExportPdf.MouseEnter += IconMouseEnter;
            btExportPdf.MouseLeave += IconMouseLeave;
            btExportPdf.MouseDown += (o, e) => {
                progBar.Visibility = Visibility.Visible;
            };
            // button export png
            btExportPng.MouseEnter += IconMouseEnter;
            btExportPng.MouseLeave += IconMouseLeave;
            btExportPng.MouseDown += (o, e) => {
                progBar.Visibility = Visibility.Visible;
            };
        }

        #endregion

        #region Events

        private void IconMouseEnter(object o, EventArgs e)
        {
            ((Image)o).Width = ((Image)o).Width + 5;
            ((Image)o).Height = ((Image)o).Height + 5;
        }

        private void IconMouseLeave(object o, EventArgs e)
        {
            ((Image)o).Width = ((Image)o).Width - 5;
            ((Image)o).Height = ((Image)o).Height - 5;
        }

        #endregion

        #region IExportGUI

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
            return Title;
        }

        public string GetName()
        {
            return string.Format("{0}:{1}", "MiliSoftware.Views.Export", nameof(PageExport));
        }

        public GUIState GetGUIState()
        {
            throw new NotImplementedException();
        }

        public object GetGUI()
        {
            return this;
        }

        public IntPtr GetHandle()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {

            OnOpen?.Invoke(this, new EventArgs());
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            OnClosed?.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
