using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OgarCommon
{
    /// <summary>
    /// 二进制文件操作类
    /// </summary>
    public static class BinaryFileHelper
    {
        /// <summary>
        /// 保存一个二进制数组到指定文件中
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="buffer">二进制数组</param>
        public static void BinarySave(string filename,byte[] buffer)
        {
            Stream flStr = new FileStream(filename, FileMode.Create);
            BinaryWriter binWrt = new BinaryWriter(flStr, Encoding.Unicode);

            binWrt.Write(buffer);

            flStr.Close();
        }

        /// <summary>
        /// 加载二进制文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>二进制数组</returns>
        public static byte[] BinaryLoad(string filename)
        {

            Stream flStr = new FileStream(filename, FileMode.Open);

            BinaryReader binRdr = new BinaryReader(flStr);

            var ret =  binRdr.ReadBytes((int)flStr.Length);

            flStr.Close();

            return ret;
        }
    }
}
