﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace OgarCommon.WPF.LokTarMarkup
{
    public class FrequencyUnitFormatConverterExtension : MarkupExtension
    {
        public FrequencyUnitFormatConverterExtension()
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new FrequencyUnitFormatConverter();
        }
    }
}
