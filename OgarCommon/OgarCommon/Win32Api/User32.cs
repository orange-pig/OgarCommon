using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon.Win32Api
{
    public class User32
    {
        public const int WS_SHOWNORMAL = 1;

        [DllImport("User32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 载入图片
        /// </summary>
        /// <param name="hInstance"></param>
        /// <param name="Reserved"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int LoadBitmapW(int hInstance, ulong Reserved);

    }
}
