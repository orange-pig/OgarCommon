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
        /// 匹配特殊字符，检查是否存在!@#$%^
        /// </summary>
        /// <param name="s">匹配目标字符串</param>
        /// <returns>有则真，无则假</returns>
        public static bool MatchSpecialChars(this string s)
        {
            var x = "!@#$%^".ToArray();

            return MatchSpecialChars(s, x);
        }

        /// <summary>
        /// 匹配特殊字符
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool MatchSpecialChars(this string s, params char[] c)
        {
            int index = s.IndexOfAny(c);

            return index >= 0;
        }

        /// <summary>
        /// 匹配参数字符串中的任意字符
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool MatchSpecialChars(this string s, string c)
        {
            var x = c.ToArray();
            int index = s.IndexOfAny(x);

            return index >= 0;
        }
    }
}
