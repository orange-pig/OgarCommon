using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgarCommon
{
    /// <summary>
    /// 获取各个时间段开始的时刻
    /// </summary>
    public static class TimeHelper
    {
        /// <summary>
        /// 昨天开始的时刻
        /// </summary>
        /// <returns>昨天开始的时刻的DateTime类型</returns>
        public static DateTime GetYesterdayFirstTime
        {
            get { return DateTime.Now.Date.AddDays(0 - 1); }
        }

        /// <summary>
        /// 今天开始的时刻
        /// </summary>
        /// <returns>今天开始的时刻的DataTime类型</returns>
        public static DateTime GetTodayFirstTime
        {
            get { return DateTime.Now.Date; }
        }

        /// <summary>
        /// 这周第一天开始时刻
        /// </summary>
        /// <returns>这周第一天开始的时刻的DataTime类型</returns>
        public static DateTime GetThisWeekFirstTime
        {
            get { return DateTime.Now.AddDays(0 - Convert.ToInt16(DateTime.Now.DayOfWeek)).Date; }
        }

        /// <summary>
        /// 上周第一天开始的时刻
        /// </summary>
        /// <returns>上周第一天开始的时刻的DataTime类型</returns>
        public static DateTime GetLastWeekFirstTime
        {
            get { return GetThisWeekFirstTime.AddDays(0 - 7).Date; }
        }

        /// <summary>
        /// 这个月第一天开始的时刻
        /// </summary>
        /// <returns>这个月第一天开始的时刻的DataTime类型</returns>
        public static DateTime GetThisMonthFirstTime
        {
            get { return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); }
        }

        /// <summary>
        /// 上个月第一天开始的时刻
        /// </summary>
        /// <returns>上个月第一天开始的时刻的DataTime类型</returns>
        public static DateTime GetLastMonthFirstTime
        {
            get { return GetThisMonthFirstTime.AddMonths(0 - 1); }
        }

        /// <summary>
        /// 今年第一天开始的时刻
        /// </summary>
        /// <returns>今年第一天开始的时刻的DataTime类型</returns>
        public static DateTime GetThisYearFirstTime
        {
            get { return new DateTime(DateTime.Now.Year, 1, 1); }
        }

        /// <summary>
        /// 昨年第一天开始的时刻
        /// </summary>
        /// <returns>昨年第一天开始的时刻的DataTime类型</returns>
        public static DateTime GetLastYearFirstTime
        {
            get { return GetThisYearFirstTime.AddYears(0 - 1); }
        }


        /// <summary>
        /// 判断超时
        /// </summary>
        /// <param name="trackTime">要检测的时刻</param>
        /// <param name="timeOutThreshold">超时时间，毫秒</param>
        /// <returns></returns>
        public static bool IsTimeOut(DateTime trackTime, int timeOutThreshold)
        {
            DateTime now = DateTime.Now.ToUniversalTime();
            TimeSpan ts = now - trackTime.ToUniversalTime();

            int alpha = timeOutThreshold - (int)Math.Round(ts.TotalMilliseconds);

            return alpha <= 0;
        }

        /// <summary>
        /// 判断是否超时
        /// </summary>
        /// <param name="trackTime">要检测的时刻</param>
        /// <param name="itemsCount">要检测的总的条目数</param>
        /// <param name="timeOutThreshold">超时时间，毫秒</param>
        /// <returns></returns>
        public static bool IsTimeOut(DateTime trackTime, int itemsCount, int timeOutThreshold)
        {
            DateTime now = DateTime.Now.ToUniversalTime();
            TimeSpan ts = now - trackTime.ToUniversalTime();

            //每条最大超时的总和 与 现在时间与时间戳的之差值（单挑超时量），除以条目数量，得到最大超时限制与每条的超时量的差值
            int alpha = (timeOutThreshold * itemsCount - (int)Math.Round(ts.TotalMilliseconds)) / itemsCount;

            return alpha <= 0;
        }

        /// <summary>
        /// 判断是否超时二
        /// </summary>
        /// <param name="trackTime">要检测超时的时刻</param>
        /// <param name="index">被检测的条目处于全部要检测的索引号，从一开始</param>
        /// <param name="timeOutThreshold">超时时间，毫秒</param>
        /// <returns></returns>
        public static bool IsTimeOutNew(DateTime trackTime, int index, int timeOutThreshold)
        {
            DateTime now = DateTime.Now.ToUniversalTime();
            TimeSpan ts = now - trackTime.ToUniversalTime();

            //每条最大超时的总和 与 现在时间与时间戳的之差值（单挑超时量），除以条目数量，得到最大超时限制与每条的超时量的差值
            int alpha = timeOutThreshold - (int)Math.Round(ts.TotalMilliseconds) / index;

            return alpha <= 0;
        }

        /// <summary>
        /// 转换成带3位毫秒数的字符串，格式为"yyyy/MM/dd HH:mm:ss fff"
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToStringWithMillisecond(DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss fff");
        }

        /// <summary>
        /// 将带有毫秒数的时间字符串转换成时间
        /// </summary>
        /// <param name="dateTimeStringWithMillsecond">带毫秒数的时间字符串，格式为"yyyy/MM/dd HH:mm:ss fff"</param>
        /// <returns></returns>
        public static DateTime ToDateTimeFromStringWithMillisecond(string dateTimeStringWithMillsecond)
        {
            DateTime retDateTime;

            string[] temp = dateTimeStringWithMillsecond.Split(' ');
            if (temp.Length != 3)
            {
                throw new ArgumentException("时间字符串不符合格式要求");
            }

            string dateTimeString = temp[0] + " " + temp[1];
            if (DateTime.TryParse(dateTimeString, out retDateTime))
            {
                int millsecond;
                if (int.TryParse(temp[2], out millsecond))
                {
                    retDateTime.AddMilliseconds(millsecond);
                    return retDateTime;
                }
                else
                {
                    throw new ArgumentException("时间字符串不符合格式要求");
                }
            }
            else
            {
                throw new ArgumentException("时间字符串不符合格式要求");
            }
        }

        /// <summary>
        /// 将秒级Unix时间戳转换为DateTime类型时间
        /// 默认使用本地时间
        /// </summary>
        /// <param name="d">毫秒级Unix时间戳</param>
        /// <returns>DateTime</returns>
        public static DateTime ConvertUnixTimeStampToDateTime(this long d)
        {
            DateTime time = DateTime.MinValue;
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0);
            time = startTime.AddSeconds(d);
            return time;
        }

        /// <summary>
        /// 将c# DateTime时间格式转换为秒级Unix时间戳格式
        /// 默认使用本地时间
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>long</returns>
        public static long ConvertDateTimeToUnixTimeStamp(this DateTime time)
        {
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0);
            long t = (long)(time - startTime).TotalSeconds;
            return t;
        }

        /// <summary>
        /// 将毫秒级Unix时间戳转换为DateTime类型时间
        /// 默认使用世界协调时
        /// </summary>
        /// <param name="d">毫秒级Unix时间戳</param>
        /// <returns>DateTime</returns>
        public static DateTime ConvertMillisecondsUnixTimeStampToDateTime(this long d)
        {
            DateTime time = DateTime.MinValue;
            DateTime startTime = new DateTime(1970, 1, 1);
            time = TimeZone.CurrentTimeZone.ToLocalTime(startTime.AddMilliseconds(d));
            return time;
        }

        /// <summary>
        /// 将c# DateTime时间格式转换为毫秒级Unix时间戳格式
        /// 默认使用世界协调时
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>long</returns>
        public static long ConvertDateTimeToMillisecondsUnixTimeStamp(this DateTime time)
        {
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime endTime = TimeZone.CurrentTimeZone.ToUniversalTime(time);

            //intResult = (time- startTime).TotalMilliseconds;
            long t = (endTime.Ticks - startTime.Ticks) / 10000; //除10000调整为13位
            return t;
        }
    }
}
