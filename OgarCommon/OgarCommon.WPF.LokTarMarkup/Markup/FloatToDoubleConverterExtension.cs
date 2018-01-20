﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace OgarCommon.WPF.LokTarMarkup
{
    public class FloatToDoubleConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new FloatToDoubleConverter();
        }
    }
}
