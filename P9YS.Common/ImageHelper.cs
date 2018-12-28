using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.IO;
using System.Text;

namespace P9YS.Common
{
    public static class ImageHelper
    {
        public static string GetSuffix(Stream stream)
        {
            var image = Image.FromStream(stream);
            var suffix = string.Empty;
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
