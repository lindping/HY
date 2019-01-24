using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace YH.Virtual_ECG_Monitor
{
    public partial class NumericUpDown : INotifyPropertyChanged
    {
     
        private int numericValue = 0;

        public NumericUpDown()
        {
            this.InitializeComponent();
        }

        public int Value
        {
            get { return numericValue; }
            set
            {
                numericValue = value;
                NotifyPropertyChanged("Value");
            }
        }
        public int Increment { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            int newValue = (Value + Increment);
            if (newValue > MaxValue)
            {
                Value = MaxValue;
            }
            else
            {
                Value = newValue;
            }
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            int newValue = (Value - Increment);
            if (newValue < MinValue)
            {
                Value = MinValue;
            }
            else
            {
                Value = newValue;
            }
        }

        private void ValueText_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Value = int.Parse(ValueText.Text);
                if (Value > MaxValue)
                {
                    ValueText.Text = MaxValue.ToString();
                    Value = MaxValue;
                }
                else if(Value<MinValue)
                {
                    ValueText.Text = MinValue.ToString();
                    Value = MinValue;
                }
            }
            catch (Exception)
            {
                Value = 0;
            }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
             
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }


}
