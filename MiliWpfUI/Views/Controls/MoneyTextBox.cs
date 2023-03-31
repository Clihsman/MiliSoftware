/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 25/02/2023          *
 * Assembly : MiliWpfUI                *
 * *************************************/

using System.Globalization;
using System.Windows.Controls;

namespace MiliSoftware.WpfUI
{
    public abstract class MoneyTextBox
    {
        public abstract event TextChangedEventHandler TextChanged;
        protected NumberFormatInfo NumberFormat;

        protected TextBox textBox;

        public MoneyTextBox(TextBox textBox) {
            this.textBox = textBox;
            NumberFormat = CultureInfo.CurrentCulture.NumberFormat;
        }

        protected abstract void SetMoneyTextBox(TextBox texbox);
        protected abstract string GetTotal(decimal total);
        protected abstract string GetNumberGroup(decimal total);
        public abstract decimal Value();
        public abstract string Text();

        public static MoneyTextBox GenerateMoneyTextBox(TextBox textBox) {
            var NumberFormat = CultureInfo.CurrentCulture.NumberFormat;
            if (NumberFormat.CurrencyDecimalDigits <= 0)
                return new IntTextBox(textBox);
            return new DecimalTextBox(textBox);
        }
    }
}
