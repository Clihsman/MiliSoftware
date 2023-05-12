using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MiliControls.Controls
{
    public partial class TextBoxM : UserControl
    {
        //Fields
        private Color borderColor = Color.White;
        private int borderSize = 2;
        private bool underLineStyle = false;
        private int borderRadius;

        //Contructor
        public TextBoxM()
        {
            InitializeComponent();
        }

        //Properties
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }

            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        public int BorderSize
        {
            get
            {
                return borderSize;
            }

            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        public bool UnderLineStyle
        {
            get
            {
                return underLineStyle;
            }

            set
            {
                underLineStyle = value;
                this.Invalidate();
            }
        }

        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }

            set
            {
                if (value >= 0)
                {
                    borderRadius = value;
                    this.Validate();
                }
            }
        }
    
        public bool PasswordChar
        {
            get {
                return textBox.UseSystemPasswordChar;
            }
            set {
                textBox.UseSystemPasswordChar = value;
            }
        }

        //Overridden methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            if (borderRadius > 0)
            {
                var rectBoderSmooth = this.ClientRectangle;
                var rectBorder = Rectangle.Inflate(rectBoderSmooth,-borderSize, -borderSize);
                int smoothSize = borderSize > 0 ? borderSize : 1;

                using (GraphicsPath pathBoderSmooth = GetFigurePath(rectBoderSmooth, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penBoderSmooth = new Pen(this.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(pathBoderSmooth);
                    if (borderRadius > 15) SetTextBoxRoundedRegion();
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                    if (underLineStyle) //Lines Styles
                    {
                        graph.DrawPath(penBoderSmooth, pathBoderSmooth);
                        graph.SmoothingMode = SmoothingMode.None ;
                        graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    }
                    else // Normal Styles
                    {
                        graph.DrawPath(penBoderSmooth, pathBoderSmooth);
                        graph.DrawPath(penBorder, pathBorder);
                    }
                }
            }
            else
            {
                //Draw boder
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(this.ClientRectangle);
                    penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

                    if (underLineStyle) //Lines Styles
                        graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    else // Normal Styles
                        graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlHeight();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if(this.DesignMode)
            UpdateControlHeight();
        }

        // Private methods
        private void UpdateControlHeight()
        {
            if (!textBox.Multiline)
            {
                int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height + 1;
                textBox.Multiline = true;
                textBox.MinimumSize = new Size(0,txtHeight);
                textBox.Multiline = false;

                this.Height = textBox.Height + this.Padding.Top + this.Padding.Bottom;
            }
        }

        private void SetTextBoxRoundedRegion()
        {
            GraphicsPath pathTxt;
            if (textBox.Multiline)
            {
                pathTxt = GetFigurePath(textBox.ClientRectangle, borderRadius - borderSize);
                textBox.Region = new Region(pathTxt);
            }
            else
            {
                pathTxt = GetFigurePath(textBox.ClientRectangle, borderSize * 2);
                textBox.Region = new Region(pathTxt);
            }
        }

        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
