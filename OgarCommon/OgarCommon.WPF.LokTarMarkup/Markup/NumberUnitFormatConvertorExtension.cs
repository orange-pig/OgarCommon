using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace OgarCommon.WPF.LokTarMarkup
{
    public class NumberUnitFormatConvertorExtension : MarkupExtension
    {
        private string _unit;

        [ConstructorArgument("unit")]
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new NumberUnitFormatConvertor(_unit);
        }
    }
}
