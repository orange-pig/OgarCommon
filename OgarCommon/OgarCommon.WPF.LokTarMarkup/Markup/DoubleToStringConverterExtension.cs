using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace OgarCommon.WPF.LokTarMarkup
{
    public class DoubleToStringConverterExtension : MarkupExtension
    {
        private bool _isReversed;

        [ConstructorArgument("isReversed")]
        public bool IsReversed
        {
            get { return _isReversed; }o
            set { _isReversed = value; }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new DoubleToStringConverter(_isReversed);
        }
    }
}
