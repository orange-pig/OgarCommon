using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon
{
    /// <summary>
    /// 图片帮助类
    /// </summary>
    public class ImageHelper
    {
        public static System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
                return null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArrayIn))
            {
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                ms.Flush();
                return returnImage;
            }
        }


        /// <summary>
        /// Paddeds the width of the row.
        /// </summary>
        /// <param name="bitsPerPixel">The bits per pixel.</param>
        /// <param name="w">The w.</param>
        /// <param name="padToNBytes">The pad to n bytes.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">padToNBytes;pad value must be greater than 0.</exception>
        public static int PaddedRowWidth(int bitsPerPixel, int w, int padToNBytes)
        {
            if (padToNBytes == 0)
                throw new ArgumentOutOfRangeException("padToNBytes", "pad value must be greater than 0.");
            int padBits = 8 * padToNBytes;
            return ((w * bitsPerPixel + (padBits - 1)) / padBits) * padToNBytes;
        }

        /// <summary>
        /// 获取 Bitmap 对象的跨距宽度
        /// 跨距是单行像素（一个扫描行）的宽度，舍入为一个 4 字节的边界。 如果跨距为正，则位图自顶向下。 如果跨距为负，则位图颠倒。
        /// </summary>
        /// <param name="bitsPerPixel">每像素位数,如RGB24,则为24位。由RGB三色表示，每色8位</param>
        /// <param name="width">Bitmap宽度</param>
        /// <returns>System.Int32.</returns>
        public static int GetStride(int bitsPerPixel, int width)
        {
            return PaddedRowWidth(bitsPerPixel, width, 4);
        }
    }
}
