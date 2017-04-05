using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon.Win32Api
{
    public class CfgMgr32
    {
        public const int CR_SUCCESS = 0x00000000;
        public const int CR_NO_SUCH_VALUE = 0x00000025;
        public const int CR_INVALID_DATA = 0x0000001F;

        /// <summary>
        /// 提供每个类的GUID枚举本地机器安装的设备类
        /// </summary>
        /// <param name="ClassIndex"></param>
        /// <param name="ClassGuid"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        [DllImport("cfgmgr32.dll")]
        public static extern UInt32 CM_Enumerate_Classes(UInt32 ClassIndex, ref Guid ClassGuid, UInt32 Params);
    }
}
