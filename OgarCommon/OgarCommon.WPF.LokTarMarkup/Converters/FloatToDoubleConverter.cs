using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OgarCommon.WPF.LokTarMarkup
{
    /// <summary>
    /// Float converter to double easy to loses precision
    /// </summary>
    public class FloatToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null || !(value is Single) ? 0d : (double)(decimal)(float)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (float)(double)value;
        }
    }
}
