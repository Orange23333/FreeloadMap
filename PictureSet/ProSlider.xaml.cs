using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace PictureSet
{
    /// <summary>
    /// ProSlider.xaml 的交互逻辑
    /// </summary>
    public partial class ProSlider : UserControl
    {
        public string Title
        {
            get { return (string)this.Label_Title.Content; }
            set { this.Label_Title.Content = value; }
        }

        public double Value
        {
            get { return this.Slider_Value.Value; }
            set { this.Slider_Value.Value = value; }
        }
        
        public double MinValue
        {
            get { return this.Slider_Value.Minimum; }
            set {
                this.Slider_Value.Minimum = value;
                RefreshFromSlider(this.Slider_Value.Value);
                RefreshFromTextBox();
            }
        }
        
        public double MaxValue
        {
            get { return this.Slider_Value.Maximum; }
            set {
                this.Slider_Value.Maximum = value;
                RefreshFromSlider(this.Slider_Value.Value);
                RefreshFromTextBox();
            }
        }

        public ProSlider()
        {
            InitializeComponent();
        }

        private void Slider_Value_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RefreshFromSlider(e.NewValue);
        }
        private void RefreshFromSlider(double newValue)
        {
            this.TextBox_Value.Text = newValue.ToString();
        }

        private void TextBox_Value_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            RefreshFromTextBox();
        }
        private void RefreshFromTextBox()
        {
            double value = this.Slider_Value.Value;

            try
            {
                value = int.Parse(this.TextBox_Value.Text);
            }
            catch (FormatException)
            {
                ;
            }
            catch (OverflowException)
            {
                if (this.TextBox_Value.Text.SkipWhile((char ch) => { return Char.IsWhiteSpace(ch); }).First() == '-')
                {
                    value = this.Slider_Value.Minimum;
                }
                else
                {
                    value = this.Slider_Value.Maximum;
                }
            }

            if (value > this.Slider_Value.Maximum)
            {
                value = this.Slider_Value.Maximum;
            }
            else if (value < this.Slider_Value.Minimum)
            {
                value = this.Slider_Value.Minimum;
            }

            this.Slider_Value.Value = value;
        }
    }
}
