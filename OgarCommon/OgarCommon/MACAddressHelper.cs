using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgarCommon
{
    public class MacAddressHelper
    {
        /// <summary>
        /// [未实现]将ulong八字节的MAC地址转换成字符串
        /// </summary>
        /// <param name="ipCode"></param>
        /// <returns></returns>
        public static string Int2MAC(ulong macCode)
        {
            byte a = (byte)((macCode & 0xFF000000) >> 0x18);
            byte b = (byte)((macCode & 0x00FF0000) >> 0xF);
            byte c = (byte)((macCode & 0x0000FF00) >> 0x8);
            byte d = (byte)(macCode & 0x000000FF);
            string ipStr = String.Format("{0}.{1}.{2}.{3}", a, b, c, d);
            return ipStr;
        }

        /// <summary>
        /// 将ip地址转换成4字节的uint32
        /// </summary>
        /// <param name="ipStr"></param>
        /// <returns></returns>
        public static ulong Mac2ULong(string macStr)
        {
            string[] ip = macStr.Split('-');
            ulong result = 0;
            
            for(int i = 0; i < ip.Length; i ++)
            {
                byte b = byte.Parse(ip[i], System.Globalization.NumberStyles.HexNumber);
                result |= ((ulong)(b) << (int)((ip.Length - i - 1) << 3));
            }

            return result;
        }
    }
}
