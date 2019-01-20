using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.IO;
using System.Text;

namespace P9YS.Common
{
    public static class ImageHelper
    {
        /// <summary>
        /// 获取图片扩展名  .jpg
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string GetSuffix(Stream stream)
        {
            var suffix = string.Empty;
            var image = Image.FromStream(stream);
            if (image.RawFormat.Equals(System.DrawingCore.Imaging.ImageFormat.Jpeg))
                suffix = ".jpg";
            else if (image.RawFormat.Equals(System.DrawingCore.Imaging.ImageFormat.Png))
                suffix = ".png";
            else if (image.RawFormat.Equals(System.DrawingCore.Imaging.ImageFormat.Gif))
                suffix = ".gif";
            else if (image.RawFormat.Equals(System.DrawingCore.Imaging.ImageFormat.Icon))
                suffix = ".ico";
            else if (image.RawFormat.Equals(System.DrawingCore.Imaging.ImageFormat.Bmp))
                suffix = ".bmp";
            return suffix;
        }
    }
}
