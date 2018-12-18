using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace YH.Virtual_ECG_Monitor
{
    public class SelectToColorConverter : IValueConverter
    {

        public static SolidColorBrush Selected = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0089e1"));
        public static SolidColorBrush UnSelected = Brushes.Transparent;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? Selected : UnSelected;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (SolidColorBrush)value == Selected ? true : false;
        }
    }
}
