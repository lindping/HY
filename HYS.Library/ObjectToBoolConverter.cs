using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HYS.Library
{

    public class ObjectToBoolConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null && parameter != null && parameter.ToString().Equals(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isChecked = (bool)value;
            if (!isChecked)
            {
                if (targetType == typeof(int) || targetType == typeof(float))
                {
                    return 0;
                }
                else
                {
                    return null;
                }
            }
            return parameter;
        }
    }
}
