using OgarCommon.Win32Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
            foreach(var item in process)
            {
                if(item.Id != current.Id)
                {
                    if(Assembly.GetExecutingAssembly().Location.Replace("/","\\")
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
            User32.ShowWindowAsync(instance.MainWindowHandle, User32.WS_SHOWNORMAL);
            User32.SetForegroundWindow(instance.MainWindowHandle);
        }
    }
}
