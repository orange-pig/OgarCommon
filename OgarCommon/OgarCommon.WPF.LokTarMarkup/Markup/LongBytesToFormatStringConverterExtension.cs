﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace OgarCommon.WPF.LokTarMarkup
{
    public class LongBytesToFormatStringConverterExtension : MarkupExtension
    {
        private bool isUseSpeed;

        [ConstructorArgument("isUseSpeed")]
        public bool IsUseSpeed
        {
            get
            {
                return isUseSpeed;
            }

            set
            {
                isUseSpeed = value;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new LongBytesToFormatStringConverter(isUseSpeed);
        }
    }
}
