using OgarCommon.Win32Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon
{
    public static class ExplorerHelper
    {
        /// <summary>
        /// Open file use exporer.
        /// </summary>
        /// <param name="filePath">File full path be opened</param>
        /// <param name="selected">Select file</param>
        /// <param name="useExisted">If existed explorer use it, not new one.</param>
        public static void BrowseFile(string filePath, bool selected = false, bool useExisted = true)
        {
            if (filePath == null)
                throw new ArgumentNullException("filePath");

            if (useExisted)
            {
                if (selected)
                {
                    IntPtr pidl = Shell32.ILCreateFromPathW(filePath);
                    Shell32.SHOpenFolderAndSelectItems(pidl, 0, IntPtr.Zero, 0);
                    Shell32.ILFree(pidl);
                }
                else
                {
                    var path = Path.GetDirectoryName(filePath);
                    Process.Start(path);
                }
            }
            else
            {
                if (selected)
                {
                    Process.Start("Explorer.exe", string.Format("/select,{0}", filePath));
                }
                else
                {
                    Process.Start("Explorer.exe", Path.GetFullPath(filePath));
                }
            }
        }
    }
}
