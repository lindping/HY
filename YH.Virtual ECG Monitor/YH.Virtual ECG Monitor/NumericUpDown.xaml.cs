using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Timers;
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
        Timer timer;
        bool isAdd;
        public NumericUpDown()
        {
            this.InitializeComponent();
            Increment = Increment <= 0 ? 1 : Increment;
            MaxValue = (MinValue == MaxValue && MinValue == 0) ? 100 : MaxValue;
            isAdd = false;
            timer = new Timer(100);
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ChangeValue();
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
            isAdd = true;
            ChangeValue();
        }
        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            isAdd = false;
            ChangeValue();
        }

        private void ChangeValue()
        {
            int increment = isAdd ? Increment : -Increment;

            int newValue = Value + increment;
            if (newValue > MaxValue)
            {
                newValue = MaxValue;
            }
            if (newValue < MinValue)
            {
                newValue = MinValue;
            }
            Value = newValue;
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
                else if (Value < MinValue)
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

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            isAdd = button == UpButton;
            timer.Enabled = true;
        }

        private void Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            timer.Enabled = false;
        }
    }


}
