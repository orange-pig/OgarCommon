using OgarCommon.Win32Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon
{
    public class ProcessController
    {
        /// <summary>
        /// 获取当前程序已经运行的实例
        /// </summary>
        /// <param name="instanceName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Process GetSingleRunningInstance()
        {
            Process current = Process.GetCurrentProcess();

            Process[] process = Process.GetProcessesByName(current.ProcessName);
            foreach (var item in process)
            {
                if (item.Id != current.Id)
                {
                    if (item.MainModule.FileName == current.MainModule.FileName)
                    {
                        return item;
                    }

                    // Next code do work on you include this class in you application project.
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\")
                        == current.MainModule.FileName)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 显示正在运行的程序实例
        /// </summary>
        /// <param name="instance"></param>
        public static void HandleInstance(Process instance)
        {
            int showCmd = User32.SW_NORMAL;

            User32.WINDOWPLACEMENT palcement = GetPlacement(instance.MainWindowHandle);
            switch (palcement.showCmd)
            {
                case User32.ShowWindowCommands.Hide:
                case User32.ShowWindowCommands.Minimized:
                    showCmd = User32.SW_RESTORE;
                    break;
                case User32.ShowWindowCommands.Maximized:
                    showCmd = User32.SW_MAXIMIZE;
                    break;
                case User32.ShowWindowCommands.Normal:
                    showCmd = User32.SW_NORMAL;
                    break;
            }
            User32.ShowWindowAsync(instance.MainWindowHandle, showCmd);

            User32.SetForegroundWindow(instance.MainWindowHandle);
        }


        private static User32.WINDOWPLACEMENT GetPlacement(IntPtr hwnd)
        {
            User32.WINDOWPLACEMENT placement = new User32.WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            User32.GetWindowPlacement(hwnd, ref placement);
            return placement;
        }
    }
}
