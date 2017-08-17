using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon
{
    public static class NumberUnitFormatHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number">The number need format </param>
        /// <param name="maxAfterPoint">The precision digits</param>
        /// <param name="unit">Need custome unit string</param>
        /// <param name="HasIntervalBeforeUnit">Has whitespace before unit, other means the interval between in number and Unit</param>
        /// <returns></returns>
        public static string Format(double number, int maxAfterPoint, string unit, bool HasIntervalBeforeUnit = true)
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
                if (HasIntervalBeforeUnit)
                {
                    return string.Format("{0:F" + devimalDigit + "} {1}", displayValue, unit);
                }
                else
                {
                    return string.Format("{0:F" + devimalDigit + "}{1}", displayValue, unit);
                }
            }
        }
    }
}
