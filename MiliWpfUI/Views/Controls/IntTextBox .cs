/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 17/12/2022          *
 * Assembly : MiliWpfUI                *
 * *************************************/

using System.Windows.Controls;
using System.Windows.Input;

namespace MiliSoftware.WpfUI
{
    public class IntTextBox : MoneyTextBox
    {
        public override event TextChangedEventHandler TextChanged;

        public IntTextBox(TextBox textBox) : base(textBox)
        {
            SetMoneyTextBox(textBox);
        }

        protected override void SetMoneyTextBox(TextBox texbox)
        {
            textBox.Text = "0";
            texbox.Select(1, 0);
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
                if (texbox.Text.StartsWith("0"))
                    texbox.Select(1, 0);

                if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Tab)
                    e.Handled = false;
                else
                    e.Handled = true;
            };

            texbox.TextChanged += delegate {
                if (!string.IsNullOrWhiteSpace(texbox.Text))
                {
                    ulong value = ulong.Parse(texbox.Text.Replace(".", "").Replace(",", ""));
                    texbox.Text = GetTotal(value);
                    texbox.Select(texbox.Text.Length, 0);
                }
                else
                {
                    texbox.Text = "0";
                    texbox.Select(texbox.Text.Length, 0);
                }
            };
        }

        protected override string GetTotal(decimal total)
        {
            string tlString = total.ToString();
            string result = "";

            string currentTotal = "";
            int id = -1;
            for (int i = tlString.Length - 1; i > -1; i--)
            {
                if (id < 2)
                    id++;
                else
                {
                    currentTotal += ".";
                    id = 0;
                }

                currentTotal += tlString[i];
            }

            for (int i = currentTotal.Length - 1; i > -1; i--)
            {
                result += currentTotal[i];
            }

            return result;
        }

        protected void SetIntTextBox2(TextBox texbox)
        {   
            texbox.KeyDown += delegate (object o, KeyEventArgs e)
            {
                if (texbox.Text.StartsWith("0"))
                    texbox.Select(1, 0);

                if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Tab)
                    e.Handled = false;
                else
                    e.Handled = true;
            };

            texbox.TextChanged += delegate (object o, TextChangedEventArgs e)
            {
                if (string.IsNullOrWhiteSpace(texbox.Text))
                {
                    texbox.Text = "0";
                    texbox.Select(texbox.Text.Length, 0);
                }
                else if (texbox.Text.StartsWith("0"))
                {
                    try
                    {
                        texbox.Text = int.Parse(texbox.Text).ToString();
                        texbox.Select(texbox.Text.Length, 0);
                    }
                    catch {
                        texbox.Text = "0";
                    }
                }
                int value = 0;
                if (!int.TryParse(texbox.Text.Replace(".",""), out value))
                    texbox.Text = value.ToString();

                TextChanged?.Invoke(o, e);
            };
        }

        public override string Text()
        {
            return textBox.Text.Replace(".", "");
        }

        protected override string GetNumberGroup(decimal total)
        {
            throw new System.NotImplementedException();
        }

        public override decimal Value()
        {
            return int.Parse(Text());
        }
    }
}
