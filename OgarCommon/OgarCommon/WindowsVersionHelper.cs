using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgarCommon
{
    public class WindowsVersionHelper
    {

        private static Dictionary<int, Dictionary<int, string>> WindwosSystemDic = new Dictionary<int, Dictionary<int, string>>();

        static WindowsVersionHelper()
        {
            var dic3 = new Dictionary<int, string>();
            dic3.Add(1,"Windows 3.1");
            dic3.Add(51, "Windows NT 3.51");
            WindwosSystemDic.Add(3, dic3);

            var dic4 = new Dictionary<int, string>();
            dic4.Add(0, "Windows 95 or Windows NT 4.0");
            dic4.Add(10, "Windows 98");
            dic4.Add(90, "Windows Millennium Edition");
            WindwosSystemDic.Add(4, dic4);

            var dic5 = new Dictionary<int, string>();
            dic5.Add(0, "Windows 2000");
            dic5.Add(1, "Windows XP");
            dic5.Add(2, "Windows XP 64-Bit Edition or Windows Server 2003 or WindowsServer 2003 R2");
            WindwosSystemDic.Add(5, dic5);

            var dic6 = new Dictionary<int, string>();
            dic6.Add(0, "Windows Server 2008 or Windows Vista");
            dic6.Add(1, "Windows 7 or Windows Server 2008 R2");
            dic6.Add(2, "Windows 8");
            dic6.Add(3, "Windows 8.1");
            dic6.Add(4, "Windows 10 Build 9841/9860/9879");
            WindwosSystemDic.Add(6, dic6);

            var dic10 = new Dictionary<int, string>();
            dic10.Add(0, "Windows 10");
            WindwosSystemDic.Add(10, dic10);
        }

        public static string GetSystemName(int major, int minor)
        {
            Dictionary<int,string> dic;
            if (WindwosSystemDic.TryGetValue(major, out dic))
            {
                string systemName;
                if (dic.TryGetValue(minor, out systemName))
                {
                    return systemName;
                }
            }

            return "UnKnown";
        }

    }
}
