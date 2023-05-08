using System.Windows.Controls;
using System.Windows.Input;

namespace MiliSoftware.WpfUI
{
    public class SymbolTextBox : MoneyTextBox
    {
        public override event TextChangedEventHandler TextChanged;
        private string symbol;
        private TextBox texbox;

        public string Symbol {
            get
            {
                return symbol;
            }
            set {
                symbol = value;
                texbox.Text = GetTotal(Value()) + Symbol;
            }
        }

        public SymbolTextBox(TextBox texbox, string symbol) : base(texbox)
        {
            this.texbox = texbox;
            SetMoneyTextBox(texbox);
            Symbol = symbol;
        }

        protected override void SetMoneyTextBox(TextBox texbox)
        {
            texbox.Text = "0" + Symbol;
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
                if (texbox.Text.StartsWith("0" + Symbol))
                    texbox.Select(1, 0);

                if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Tab)
                    e.Handled = false;
                else
                    e.Handled = true;
            };

            texbox.TextChanged += delegate (object o, TextChangedEventArgs e) {
                string text = RemoveLetters(texbox.Text);
                if (!string.IsNullOrWhiteSpace(text))
                {
                    ulong value = ulong.Parse(text);
                    texbox.Text = GetTotal(value) + Symbol;
                    texbox.Select(texbox.Text.Length - Symbol.Length, 0);
                }
                else
                {
                    texbox.Text = "0" + Symbol;
                    texbox.Select(texbox.Text.Length - Symbol.Length, 0);
                }
                TextChanged?.Invoke(o,e);
            };
        }

        private static string RemoveLetters(string input) {
            string neValue = input;
            for (int i = 0;i< input.Length;i++)
            {
                if (!char.IsNumber(input[i]))
                    neValue = neValue.Replace(input[i].ToString(), "");
            }
            return neValue;
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

        public override string Text()
        {
            return RemoveLetters(textBox.Text);
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
