using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OgarCommon
{
    /// <summary>
    /// 匹配方法类
    /// </summary>
    public static class MatchHelper
    {
        /// <summary>
        /// 匹配特殊字符
        /// </summary>
        /// <param name="s">匹配目标字符串</param>
        /// <returns>有则真，无则假</returns>
        public static bool MatchSpecialChars(string s)
        {
            var x = "!@#$%^".ToArray();
            int index = s.IndexOfAny(x);

            return index >= 0;
        }
    }
}
