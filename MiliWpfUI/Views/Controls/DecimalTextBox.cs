using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using System;

namespace MiliSoftware.WpfUI
{
    public class DecimalTextBox : MoneyTextBox
    {
        public override event TextChangedEventHandler TextChanged;
        private bool setDecimal = false;
        private bool user = false;

        public DecimalTextBox(TextBox textBox) : base(textBox)
        {
            textBox.Text = GetTotal(0.0M);
            SetMoneyTextBox(textBox);
        }

        protected override void SetMoneyTextBox(TextBox texbox)
        {
            texbox.PreviewKeyDown += delegate (object o, KeyEventArgs e)
            {
                bool keys = (e.Key == Key.Up && e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right);

                if (keys)
                    e.Handled = true;
                else
                    e.Handled = false;
            };

            texbox.KeyDown += delegate (object o, KeyEventArgs e)
            {
                if (!setDecimal && e.Key == Key.Decimal)
                {
                    setDecimal = true;
                    texbox.Text = GetNumberGroup(int.Parse(texbox.Text.Split(',')[0].Replace(NumberFormat.NumberGroupSeparator, ""))) + ",";
                    int index = texbox.Text.IndexOf(",");
                    texbox.Select(index + 1, 0);
                }

                if (!setDecimal)
                {
                    int index = texbox.Text.IndexOf(",");
                    if(index != -1) texbox.Select(index, 0);
                }


                bool keys = (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab);

                if (!keys)
                    e.Handled = true;
                else
                    e.Handled = false;

                user = true;
            };

            texbox.GotFocus += delegate {

                if (setDecimal)
                {
                    if (!(texbox.Text.Length > 2))
                    {
                        texbox.Text = "0,00";
                        texbox.Select(texbox.Text.Length - 1, 0);
                    }
                    else
                    {
                        texbox.Select(texbox.Text.Length , 0);
                    }
                }
                else
                {
                    if (!(texbox.Text.Length > 3))
                    {
                        texbox.Text = "0,00";
                        texbox.Select(texbox.Text.Length - 3, 0);
                    }
                    else
                    {
                        texbox.Select(texbox.Text.Length - 3, 0);
                    }
                }
            };

            texbox.LostFocus += delegate {
                setDecimal = false;
                decimal value = 0.0M;
                decimal.TryParse(texbox.Text.Replace(NumberFormat.NumberGroupSeparator, "").Replace(NumberFormat.NumberDecimalSeparator, ","), out value);
                texbox.Text = GetTotal(value);
            };

            texbox.TextChanged += delegate(object o, TextChangedEventArgs e)
            {
                if (!user)
                    return;
                try
                {
                    string[] @in = textBox.Text.Split(new string[] { NumberFormat.NumberDecimalSeparator }, System.StringSplitOptions.RemoveEmptyEntries);
                    string m_real = @in[0];
                    string m_decimal = "0";
                    if (@in.Length > 1) m_decimal = @in[1];

                    ulong v_real = ulong.Parse(m_real.Replace(NumberFormat.NumberGroupSeparator, ""));
                    ulong v_decimal = ulong.Parse(m_decimal);

                    if (setDecimal)
                    {
                        texbox.Select(texbox.Text.Length, 0);

                        if (m_decimal.Length > 1 && m_decimal.StartsWith("0"))
                            texbox.Text = string.Format("{0}{1}{2}", GetNumberGroup(v_real), NumberFormat.NumberDecimalSeparator, m_decimal);
                        else
                            texbox.Text = string.Format("{0}{1}{2}", GetNumberGroup(v_real), NumberFormat.NumberDecimalSeparator, v_decimal);
                    }
                    else
                    {
                        if (texbox.Text.Length > 3)
                        {
                            decimal value = decimal.Parse(texbox.Text.Replace(NumberFormat.NumberGroupSeparator, "").Replace(NumberFormat.NumberDecimalSeparator, ","));
                            texbox.Text = GetTotal(value);
                            texbox.Select(texbox.Text.Length - 3, 0);
                        }
                        else
                        {
                            texbox.Text = "0,00";
                            texbox.Select(texbox.Text.Length - 3, 0);
                        }
                    }
                    TextChanged?.Invoke(o, e);
                }
                catch {
                    textBox.Text = GetTotal(0.0M);
                }
                user = false;
            };
        }

        protected override string GetTotal(decimal total)
        {
            string m_real = "";
            string m_decimal = "";

            string @in = total.ToString("N" + NumberFormat.CurrencyDecimalDigits);

            if(@in.Contains(","))
            {
                string[] @in_split = @in.Split(',');
                m_real = @in_split[0];
                m_decimal = @in_split[1];
            }

            m_real = m_real.Replace(".", NumberFormat.NumberGroupSeparator);
            m_decimal = m_decimal.Replace(",", NumberFormat.NumberDecimalSeparator);

            Console.WriteLine(@in);

            return m_real + NumberFormat.NumberDecimalSeparator + m_decimal;
        }

        protected override string GetNumberGroup(decimal total)
        {
            string m_real = "";
            string m_decimal = "";

            string @in = total.ToString("N" + NumberFormat.CurrencyDecimalDigits);

            if (@in.Contains(","))
            {
                string[] @in_split = @in.Split(',');
                m_real = @in_split[0];
                m_decimal = @in_split[1];
            }

            m_real = m_real.Replace(".", NumberFormat.NumberGroupSeparator);

            return m_real;
        }

        public override decimal Value()
        {
            if (string.IsNullOrWhiteSpace(textBox.Text)) return 0;
            return decimal.Parse(textBox.Text);
        }

        public override string Text()
        {
            return textBox.Text.Replace(NumberFormat.NumberGroupSeparator, "");
        }
    }
}
