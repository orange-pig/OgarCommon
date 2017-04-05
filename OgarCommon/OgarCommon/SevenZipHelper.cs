using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace OgarCommon
{
    public class SevenZipHelper
    {
        private const string sevenZipInstallPath = "./tools/7zip/7za.exe";

        public static void CompressDirectory(string inDirPath, string outFilePath)
        {
            Process process = new Process();
            process.StartInfo.FileName = Path.Combine(Directory.GetCurrentDirectory(), sevenZipInstallPath);
            process.StartInfo.Arguments = " a -tzip " + outFilePath + " " + inDirPath + " -r";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            process.Close();
        }

        public static void CompressFile(string InFilePath, string OutFilePath)
        {
            Process process = new Process();
            process.StartInfo.FileName = Path.Combine(Directory.GetCurrentDirectory() ,sevenZipInstallPath);
            process.StartInfo.Arguments = " a -tzip " + OutFilePath + " " + InFilePath + "";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            process.Close();
        }

        public static void DecompressFileToDestdDirectory(string InFilePath, string OutDirectoryPath)
        {
            Process process = new Process();
            process.StartInfo.FileName = Path.Combine(Directory.GetCurrentDirectory(), sevenZipInstallPath);
            process.StartInfo.Arguments = " x " + InFilePath + " -o" + OutDirectoryPath + " -r";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            process.Close();
        }  
    }
}
