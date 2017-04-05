using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon.Win32Api
{
    public class AdvApi32
    {
        public const int MAX_SIZE_DEVICE_DESCRIPTION = 1000;

        /// <summary>
        /// 取得指定项或子项的默认（未命名）值 
        /// </summary>
        /// <param name="KeyClass"></param>
        /// <param name="SubKey"></param>
        /// <param name="ClassDescription"></param>
        /// <param name="sizeB"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll")]
        public static extern UInt32 RegQueryValueA(IntPtr KeyClass, UInt32 SubKey, StringBuilder ClassDescription, ref UInt32 sizeB);
    }
}
