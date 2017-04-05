using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon
{
    public class VersionHelper
    {

        public static DateTime VersionToDateTime(string versionString)
        {
            if (versionString.Length != 10)
            {
                return new DateTime(0, 0, 0, 0, 0, 0);
            }

            var year = int.Parse(versionString.Substring(0, 4));
            var mouth = int.Parse(versionString.Substring(4, 2));
            var day = int.Parse(versionString.Substring(6, 2));
            var hour = int.Parse(versionString.Substring(8, 2));

            return new DateTime(year, mouth, day, hour, 0, 0);
        }

        public static string DateTimeToVersion(DateTime version)
        {
            return string.Format(version.ToString("yyyyMMddHH"));
        }
    }
}
