using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon
{
    public class IPAddressHelper
    {
        /// <summary>
        /// 将uint32四字节的ip地址转换成字符串
        /// </summary>
        /// <param name="ipCode"></param>
        /// <returns></returns>
        public static string Int2IP(UInt32 ipCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((ipCode >> 24) & 0xFF).Append(".");
            sb.Append((ipCode >> 16) & 0xFF).Append(".");
            sb.Append((ipCode >> 8) & 0xFF).Append(".");
            sb.Append(ipCode & 0xFF);
            return sb.ToString();
        }

        /// <summary>
        /// 将ip地址转换成4字节的uint32
        /// </summary>
        /// <param name="ipStr"></param>
        /// <returns></returns>
        public static long IP2Int(string ipStr)
        {
            char[] separator = new char[] { '.' };
            string[] items = ipStr.Split(separator);
            return long.Parse(items[0]) << 24
                    | long.Parse(items[1]) << 16
                    | long.Parse(items[2]) << 8
                    | long.Parse(items[3]);
        }
    }
}
