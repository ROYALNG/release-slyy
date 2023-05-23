using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace GHIBMS.NHikPlayer
{
    /// <summary>
    /// A custom control derived from TextBox so that it accepts only numeric input.
    /// Author:Microsoft
    /// URL:http://msdn.microsoft.com/en-us/library/ms229644.aspx
    /// </summary>
    [ToolboxBitmap(typeof(TextBox))]
    [DefaultEvent("TextChanged")]
    public sealed class NumericTextBox : TextBox
    {
        bool allowSpace = false;

        // Restricts the entry of characters to digits (including hex), the negative sign,
        // the decimal point, and editing keystrokes (backspace).
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (keyInput.Equals(decimalSeparator) ||
                keyInput.Equals(groupSeparator) ||
                keyInput.Equals(negativeSign))
            {
                // Decimal separator is OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else if (this.allowSpace && e.KeyChar == ' ')
            {
                // Space key is OK
            }
            else
            {
                // Consume this invalid key and beep
                e.Handled = true;
                // MessageBeep();
            }
        }

        // 判断输入的字符串是否能正确转换成数字类型。
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            Int32 intValue = 0;
            if (this.Text.Length > 0 && !Int32.TryParse(this.Text, out intValue))
            {
                if (this.Text.Length == 1 && this.Text.StartsWith("-"))
                    return;
                this.Text = this.Text.Substring(0, this.Text.Length - 1);
                this.SelectionStart = this.Text.Length;
            }

            Decimal decimalValue = 0;
            if (this.Text.Length > 0 && !Decimal.TryParse(this.Text, out decimalValue))
            {
                if (this.Text.Length == 1 && this.Text.StartsWith("-"))
                    return;
                this.Text = this.Text.Substring(0, this.Text.Length - 1);
                this.SelectionStart = this.Text.Length;
            }
        }
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.All),
        System.ComponentModel.Category("Numeric settings"),
        System.ComponentModel.Description("获取TextBox显示的 32 位有符号的整数数值。")]
        public int IntValue
        {
            get
            {
                return Int32.Parse(this.Text);
            }
        }

        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.All),
        System.ComponentModel.Category("Numeric settings"),
        System.ComponentModel.Description("获取TextBox显示的十进制数数值。")]
        public decimal DecimalValue
        {
            get
            {
                return Decimal.Parse(this.Text);
            }
        }

        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.All),
        System.ComponentModel.Category("Numeric settings"),
        System.ComponentModel.Description("是否允许输入空格。")]
        public bool AllowSpace
        {
            set
            {
                this.allowSpace = value;
            }

            get
            {
                return this.allowSpace;
            }
        }
    }
}
