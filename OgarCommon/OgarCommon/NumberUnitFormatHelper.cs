using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon
{
    public static class NumberUnitFormatHelper
    {
        public static string Format(double number, int maxAfterPoint, string unit)
        {
            var displayValue = 0d;
            var devimalDigit = 0;

            if (number < 1e3 && number > -1e3)
            {
                displayValue = number;
                devimalDigit = 0;
            }
            else if (number < 1e6 && number > -1e6)
            {
                displayValue = number / 1e3;
                devimalDigit = maxAfterPoint < 3 ? maxAfterPoint : 3;
                unit = "K" + unit;
            }
            else if (number < 1e9 && number > -1e9)
            {
                displayValue = number / 1e6;
                devimalDigit = maxAfterPoint < 6 ? maxAfterPoint : 6;
                unit = "M" + unit;
            }
            else if (number < 1e12 && number > -1e12)
            {
                displayValue = number / 1e9;
                devimalDigit = maxAfterPoint < 6 ? maxAfterPoint : 6;
                unit = "G" + unit;
            }
            else if (number < 1e15 && number > -1e15)
            {
                displayValue = number / 1e12;
                devimalDigit = maxAfterPoint < 6 ? maxAfterPoint : 6;
                unit = "T" + unit;
            }
            else if (number < 1e18 && number > -1e18)
            {
                displayValue = number / 1e15;
                devimalDigit = maxAfterPoint < 6 ? maxAfterPoint : 6;
                unit = "P" + unit;
            }
            else
            {
                displayValue = 1e18;// 用旧值重新覆盖
            }

            if (string.IsNullOrWhiteSpace(unit))
            {
                return string.Format("{0:F" + devimalDigit + "}", displayValue);
            }
            else
            {
                return string.Format("{0:F" + devimalDigit + "} {1}", displayValue, unit);
            }
        }
    }
}
