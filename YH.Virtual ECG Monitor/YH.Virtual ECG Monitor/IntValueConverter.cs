using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace YH.Virtual_ECG_Monitor
{
    [ValueConversion(typeof(int), typeof(string))]
    public class IntValueConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return value.ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return int.Parse((string)value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion
    }
}
