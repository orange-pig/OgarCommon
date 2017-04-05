using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OgarCommon
{
    /// <summary>
    /// 二进制图片帮助类
    /// </summary>
    public static class BitmapHelper
    {
        #region bitmap

        /// <summary>
        /// 纯净的图片数据转换到bitmap，没有头部信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap Byte2Bitmap(Byte[] data, int width, int height)
        {
            Bitmap image = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            BitmapData bmData = image.LockBits(rect, ImageLockMode.ReadWrite, image.PixelFormat);
            IntPtr ptr = bmData.Scan0;

            for (int i = 0; i < image.Height; i++)
            {
                Marshal.Copy(data, i * image.Width * 3, ptr, image.Width * 3);
                ptr = (IntPtr)(ptr.ToInt32() + bmData.Stride);
            }

            //unsafe
            //{
            //    byte* p = (byte*)(void*)ptr;
            //    int nOffset = bmData.Stride - image.Width * 3;
            //    int nWidth = image.Width * 3;
            //    for (int r = 0; r < image.Height; r++)
            //    {
            //        for (int c = 0; c < nWidth; c++)
            //        {
            //            *p = data[r * nWidth + c];
            //            p++;
            //        }
            //        p += nOffset;
            //    }
            //}

            image.UnlockBits(bmData);

            return image;
        }

        /// <summary>
        /// 从源图片中人脸位置方框截取头像图片
        /// </summary>
        /// <param name="srcBitmap">源图片</param>
        /// <param name="FaceRectangle">人脸位置方框</param>
        /// <returns>头像图片</returns>
        public static Bitmap GetHeadImage(Bitmap srcBitmap, Rectangle FaceRectangle)
        {
            //制造头像范围的矩形
            var HeadRectangle = MakeHeadRectangle(FaceRectangle);

            Bitmap HeadImage = new Bitmap(HeadRectangle.Width, HeadRectangle.Height);
            using (Graphics g = Graphics.FromImage(HeadImage))
            {
                g.DrawImage(srcBitmap, new Rectangle(0, 0, HeadRectangle.Width, HeadRectangle.Height), HeadRectangle, GraphicsUnit.Pixel);
                g.Dispose();
            }

            return HeadImage;
        }

        /// <summary>
        /// 包含头部信息的图片数据转换成bitmap
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap TotalByte2Bitmap(byte[] image)
        {
            try
            {
                Bitmap bmp = null;

                System.IO.MemoryStream ms = new System.IO.MemoryStream(image);

                bmp = new Bitmap(ms);

                return bmp;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将bitmap格式的图片转换成二进制数组
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] BitmapToByteArray(Bitmap img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            byte[] bytes = ms.ToArray();
            ms.Close();

            return bytes;
        }

        /// <summary>
        /// 将二进制数组转换成bitmap图片格式
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Bitmap ByteArrayToBitmap(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            Bitmap img = Image.FromStream(ms) as Bitmap;
            ms.Close();

            return img;
        }

        /// <summary>
        /// 以逆时针为方向对图像进行旋转，性能不行
        /// </summary>
        /// <param name="b">位图流</param>
        /// <param name="angle">旋转角度[0,360](前台给的)</param>
        /// <returns></returns>
        public static Bitmap Rotate(Bitmap b, int angle)
        {
            angle = angle % 360;
            //弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            //原图的宽和高
            int w = b.Width;
            int h = b.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));
            //目标位图
            Bitmap dsImage = new Bitmap(W, H);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //计算偏移量
            Point Offset = new Point((W - w) / 2, (H - h) / 2);
            //构造图像显示区域：让图像的中心与窗口的中心点一致
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(360 - angle);
            //恢复图像在水平和垂直方向的平移
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(b, rect);
            //重至绘图的所有变换
            g.ResetTransform();
            g.Save();
            g.Dispose();
            //dsImage.Save("yuancd.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return dsImage;
        }

        /// <summary>
        /// 旋转图片，性能不行
        /// </summary>
        /// <param name="b"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Bitmap rotateImage(Bitmap b, float angle)
        {
            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(returnBitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //move rotation point to center of image
            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
            //rotate
            g.RotateTransform(angle);
            //move image back
            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
            //draw passed in image onto graphics object
            g.DrawImage(b, new Point(0, 0));
            return returnBitmap;
        }

        /// <summary>
        /// 旋转图片，性能不行
        /// </summary>
        /// <param name="image"></param>
        /// <param name="rotateAtX"></param>
        /// <param name="rotateAtY"></param>
        /// <param name="angle"></param>
        /// <param name="bNoClip"></param>
        /// <returns></returns>
        public static Bitmap RotateImage(Bitmap image, float rotateAtX, float rotateAtY, float angle, bool bNoClip)
        {
            int W, H, X, Y;
            if (bNoClip)
            {
                double dW = (double)image.Width;
                double dH = (double)image.Height;

                double degrees = Math.Abs(angle);
                if (degrees <= 90)
                {
                    double radians = 0.0174532925 * degrees;
                    double dSin = Math.Sin(radians);
                    double dCos = Math.Cos(radians);
                    W = (int)(dH * dSin + dW * dCos);
                    H = (int)(dW * dSin + dH * dCos);
                    X = (W - image.Width) / 2;
                    Y = (H - image.Height) / 2;
                }
                else
                {
                    degrees -= 90;
                    double radians = 0.0174532925 * degrees;
                    double dSin = Math.Sin(radians);
                    double dCos = Math.Cos(radians);
                    W = (int)(dW * dSin + dH * dCos);
                    H = (int)(dH * dSin + dW * dCos);
                    X = (W - image.Width) / 2;
                    Y = (H - image.Height) / 2;
                }
            }
            else
            {
                W = image.Width;
                H = image.Height;
                X = 0;
                Y = 0;
            }

            //create a new empty bitmap to hold rotated image
            Bitmap bmpRet = new Bitmap(W, H);
            bmpRet.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(bmpRet);

            //Put the rotation point in the "center" of the image
            g.TranslateTransform(rotateAtX + X, rotateAtY + Y);

            //rotate the image
            g.RotateTransform(angle);

            //move the image back
            g.TranslateTransform(-rotateAtX - X, -rotateAtY - Y);

            //draw passed in image onto graphics object
            g.DrawImage(image, new PointF(0 + X, 0 + Y));

            return bmpRet;
        }

        #endregion

        #region BitmapImage

        /// <summary>
        /// 将bitmap转换成bitmapImage类型
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns>绑定给控件显示的类型</returns>
        public static BitmapImage GetBmpFromBitmap(Bitmap bitmap)
        {
            BitmapImage bitImg = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);

                try
                {
                    bitImg.BeginInit();

                    bitImg.CacheOption = BitmapCacheOption.OnLoad;

                    bitImg.StreamSource = ms;

                    bitImg.EndInit();
                    bitImg.Freeze();
                    return bitImg;
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 二进制图片转换成bitmapimage
        /// </summary>
        /// <param name="imageData"></param>
        /// <returns></returns>
        public static BitmapImage ToBitmapImage(byte[] imageData)
        {
            BitmapImage bmp = null;

            try
            {
                if (imageData.Length > 0)
                {
                    bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.StreamSource = new MemoryStream(imageData.ToArray(), 0, imageData.Length);
                    bmp.EndInit();
                    bmp.Freeze();
                }
            }
            catch (Exception ex)
            {
                bmp = null;
            }

            return bmp;
        }

        /// <summary>
        /// 将图像数据byte[]转换为BitmapImage，用于界面显示
        /// </summary>
        /// <param name="data"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static BitmapImage Byte2BitmapImage(Byte[] data, int width, int height)
        {
            Bitmap image = Byte2Bitmap(data, width, height);
            BitmapImage bmp = GetBmpFromBitmap(image);
            return bmp;
        }

        /// <summary>
        /// 包含头部信息的图片数据转换成bitmapImage
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static BitmapImage TotalByte2BitmapImage(Byte[] image)
        {
            if (image == null)
            {
                return null;
            }

            System.IO.MemoryStream ms = new System.IO.MemoryStream(image.ToArray());

            BitmapImage bitImg = new BitmapImage();

            try
            {
                bitImg.BeginInit();

                bitImg.CacheOption = BitmapCacheOption.OnLoad;

                bitImg.StreamSource = ms;

                bitImg.EndInit();
                bitImg.Freeze();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
                ms.Dispose();
            }

            return bitImg;
        }

        public static BitmapSource ByteArrayToBitmapSource(byte[] imageData, int width, int height)
        {
            System.Windows.Media.PixelFormat pf = PixelFormats.Bgr24;
            int rawStride = width * 3;
            BitmapSource bitmap = BitmapSource.Create(width, height,
                96, 96, pf, null,
                imageData, rawStride);

            return bitmap;
        }

        /// <summary>
        /// BitmapImage 转换成二进制图片
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static byte[] BitmapImageToByteArray(BitmapImage bmp)
        {
            byte[] bytearray = null;

            try
            {
                Stream smarket = bmp.StreamSource;

                if (smarket != null && smarket.Length > 0)
                {
                    //很重要，因为position经常位于stream的末尾，导致下面读取到的长度为0。
                    smarket.Position = 0;

                    using (BinaryReader br = new BinaryReader(smarket))
                    {
                        bytearray = br.ReadBytes((int)smarket.Length);
                    }
                }
            }
            catch
            {
                //other exception handling
            }

            return bytearray;
        }


        #endregion

        #region rectangle

        /// <summary>
        /// 根据人脸矩形，制造一个头像矩形（包含位置信息）
        /// </summary>
        /// <param name="faceRectangle">人脸的矩形框</param>
        /// <returns></returns>
        public static Rectangle MakeHeadRectangle(Rectangle faceRectangle)
        {
            double xScale = 0.7;
            double yScale = 0.7;

            int xAddPixel = (int)(xScale * (double)faceRectangle.Height);
            int yAddPixel = (int)(yScale * (double)faceRectangle.Width);

            //将矩形范围以中心点宽高各自扩大相应的倍数。
            faceRectangle.Inflate(yAddPixel, xAddPixel);

            return faceRectangle;
        }

        #endregion
    }
}
