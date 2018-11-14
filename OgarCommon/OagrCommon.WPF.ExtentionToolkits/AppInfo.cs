using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Windows;

namespace OgarCommon.WPF.ExtensionToolkits
{
    public class AppInfo
    {
        static AppInfo()
        {
            var asm = Assembly.GetEntryAssembly();

            ProductTitle = "";
            var titleAttrs = asm.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (titleAttrs.Length > 0)
            {
                var titleAttr = titleAttrs[0] as AssemblyTitleAttribute;
                ProductTitle = titleAttr.Title;
            }

            ProductVersion = asm.GetName().Version.ToString();
            var appAttrs = asm.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
            if (appAttrs.Length > 0)
            {
                // 如果存在产品版本，则使用文件版本替换程序集版本作为程序版本
                var appAttr = appAttrs[0] as AssemblyInformationalVersionAttribute;
                ProductVersion = appAttr.InformationalVersion;
            }
            else
            {
                var fileAttrs = asm.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if (fileAttrs.Length > 0)
                {
                    // 如果存在文件版本，则使用文件版本替换程序集版本作为程序版本
                    var fileAttr = fileAttrs[0] as AssemblyFileVersionAttribute;
                    ProductVersion = fileAttr.Version;
                }
            }
            
            var descAttrs = asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (descAttrs.Length > 0)
            {
                var descAttr = descAttrs[0] as AssemblyDescriptionAttribute;
                ProductDescription = descAttr.Description;
            }
        }

        public static readonly string ProductTitle;

        public static readonly string ProductVersion;

        public static readonly string ProductDescription;
    }
}
