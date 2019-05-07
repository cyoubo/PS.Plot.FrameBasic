using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Utils
{
    public class GraphicUtils
    {
        public string ErrorMessage { get; protected set; }

        public string SourceImagePath { get; protected  set; }

        public Image SourceImage { get; protected set; }

        public GraphicUtils(string SourceImagePath)
        {
            this.SourceImagePath = SourceImagePath;
        }

        /// <summary>
        /// 加载影像
        /// </summary>
        /// <returns></returns>
        public bool LoadImage()
        {
            bool result = true;
            try
            {
                Release();

                SourceImage = Image.FromFile(SourceImagePath);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 加载影像
        /// </summary>
        /// <param name="SourceImagePath"></param>
        /// <returns></returns>
        public bool LoadImage(string SourceImagePath)
        {
            bool result = true;
            try
            {
                Release();

                this.SourceImagePath = SourceImagePath; 
                SourceImage = Image.FromFile(SourceImagePath);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                result = false;
            }
            return result;

        }

        /// <summary>
        /// 释放影像
        /// </summary>
        public void Release()
        {
            if (SourceImage != null)
                SourceImage.Dispose();
        }

        /// <summary>
        /// 输出缩略图，改变原图长宽比，以满足输出的长度和宽度值
        /// </summary>
        /// <param name="DstImagePath"></param>
        /// <param name="OutW"></param>
        /// <param name="OutH"></param>
        /// <returns></returns>
        public bool ExportThumbnail(string DstImagePath,int OutW,int OutH)
        {
            bool result = false;
            onProcessDstPath(DstImagePath);

            int towidth = OutW;
            int toheight = OutH;
            int ow = SourceImage.Width;
            int oh = SourceImage.Height;

            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            g.DrawImage(SourceImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(0, 0, ow, oh), GraphicsUnit.Pixel);
            try
            {
                ImageFormat format = onExtractFormatFromPath(DstImagePath);
                bitmap.Save(DstImagePath, format);
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                bitmap.Dispose();
                g.Dispose();
            }

            return result;
        }

        /// <summary>
        /// 输出缩率图，以参考长度为准，缩放短边
        /// </summary>
        /// <param name="DstImagePath"></param>
        /// <param name="refenceLength"></param>
        /// <returns></returns>
        public bool ExportThumbnail(string DstImagePath, int refenceLength)
        {
            bool result = false;
            int towidth = refenceLength;
            int toheight = refenceLength;
            int ow = SourceImage.Width;
            int oh = SourceImage.Height;
            int dw = 0;
            int dh = 0;

            if ((double)SourceImage.Width / (double)SourceImage.Height > (double)towidth / (double)toheight)
            {
                dw = SourceImage.Width * towidth / SourceImage.Width;
                dh = SourceImage.Height * toheight / SourceImage.Width;
            }
            else
            {
                dw = SourceImage.Width * towidth / SourceImage.Height;
                dh = SourceImage.Height * toheight / SourceImage.Height;
            }

            System.Drawing.Image bitmap = new System.Drawing.Bitmap(dw, dh);
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            g.DrawImage(SourceImage, new Rectangle(0, 0, dw, dh), new Rectangle(0, 0, ow, oh), GraphicsUnit.Pixel);

            try
            {
                ImageFormat format = onExtractFormatFromPath(DstImagePath);
                bitmap.Save(DstImagePath, System.Drawing.Imaging.ImageFormat.Png);
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                bitmap.Dispose();
                g.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 输出缩略图，指定目标图片长度与参考长度，缩放后可能会出现白边
        /// </summary>
        /// <param name="DstImagePath"></param>
        /// <param name="OutW"></param>
        /// <param name="OutH"></param>
        /// <param name="refenceLength"></param>
        /// <returns></returns>
        public bool ExportThumbnail(string DstImagePath, int OutW, int OutH, int refenceLength)
        {
            bool result = false;

            int towidth = OutW;
            int toheight = OutH;

            int x = 0; //缩略图在画布上的X放向起始点
            int y = 0; //缩略图在画布上的Y放向起始点
            int ow = SourceImage.Width;
            int oh = SourceImage.Height;
            int dw = 0;
            int dh = 0;

            if ((double)SourceImage.Width / (double)SourceImage.Height > (double)towidth / (double)toheight)
            {
                dw = SourceImage.Width * towidth / SourceImage.Width;
                dh = SourceImage.Height * toheight / SourceImage.Width;
                x = 0;
                y = (toheight - dh) / 2;
            }
            else
            {
                dw = SourceImage.Width * towidth / SourceImage.Height;
                dh = SourceImage.Height * toheight / SourceImage.Height;
                x = (towidth - dw) / 2;
                y = 0;
            }

            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            g.DrawImage(SourceImage, new Rectangle(x, y, dw, dh),new Rectangle(0, 0, ow, oh), GraphicsUnit.Pixel);

            try
            {
                ImageFormat format = onExtractFormatFromPath(DstImagePath);
                bitmap.Save(DstImagePath, System.Drawing.Imaging.ImageFormat.Png);
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                bitmap.Dispose();
                g.Dispose();
            }

            return result;
        }


        private  ImageFormat onExtractFormatFromPath(string path)
        {
            string extent = Path.GetExtension(path).ToLower();
            switch (extent)
            {
                case "bmp": return ImageFormat.Bmp;
                case "emf": return ImageFormat.Emf;
                case "gif": return ImageFormat.Gif;
                case "icon": return ImageFormat.Icon;
                case "jpeg":
                case "jpg":
                    return ImageFormat.Jpeg;
                case "memoryBmp": return ImageFormat.MemoryBmp;
                case "png": return ImageFormat.Png;
                case "tiff":
                case "tif":
                    return ImageFormat.Tiff;
                case "wmf": return ImageFormat.Wmf;
                default: return ImageFormat.Jpeg;
            }
        }

        private void onProcessDstPath(string dstPath)
        {
            DirectoryInfo targetFold = new DirectoryInfo(Path.GetDirectoryName(dstPath));
            if (targetFold.Exists == false)
                targetFold.Create();
        }
    }
}
