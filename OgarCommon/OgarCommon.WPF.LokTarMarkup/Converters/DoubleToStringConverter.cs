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
    /// Double to String , or String to Double
    /// </summary>
    public class DoubleToStringConverter : IValueConverter
    {
        private bool _isReversed;

        public bool IsReversed { get { return _isReversed; } set { _isReversed = value; } }

        public DoubleToStringConverter()
            : this(false)
        {

        }

        public DoubleToStringConverter(bool isReversed)
        {
            _isReversed = isReversed;
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !IsReversed ? ConvertToString(value) : ConvertToDouble(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !IsReversed ? ConvertToDouble(value) : ConvertToString(value);
        }


        private object ConvertToDouble(object value)
        {
            return double.TryParse(value.ToString(), out double number) ? number : Binding.DoNothing;
        }

        private object ConvertToString(object value)
        {
            return value == null ? null : value.ToString();
        }
    }
}
