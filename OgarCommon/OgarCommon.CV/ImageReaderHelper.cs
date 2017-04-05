using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon.CV
{
    /// <summary>
    /// 图片读取类，使用OpenCV2411封装，解决读取图片一致性问题。
    /// </summary>
    public class ImageReaderHelper
    {
        private const string ImageReaderDLL = @"ImageReader.dll";

        /// <summary>
        /// 读取图像
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        /// <param name="imageData">传出的图像数据（bgr）</param>
        /// <param name="imageDataLength">图像数据长度，width * height * channel</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="NumberOfChannels">数据类型</param>
        /// <returns></returns>
        [DllImportAttribute(ImageReaderDLL, EntryPoint = "ImageReaderBgrFromFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImageReaderBgr(string imagePath, byte[] imageData, ref int imageDataLength, ref int width, ref int height, ref int NumberOfChannels);

        [DllImportAttribute(ImageReaderDLL, EntryPoint = "ImageReaderBgrFromByte", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImageReaderBgrFromByte(byte[] imageInData, int iImgInLen, byte[] imageData, ref int imageDataLength, ref int width, ref int height, ref int NumberOfChannels);
    }
}
