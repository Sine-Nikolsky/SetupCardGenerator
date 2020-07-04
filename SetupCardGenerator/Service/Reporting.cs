using System;
using System.Drawing;
using System.IO;

namespace SetupCardGenerator.Service
{
    public static class Reporting
    {
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            if (imageIn == null)
                return new byte[1];
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        //public static string GetPaddingLeft(Image image) => (int)((113.0f - 113.0f * ((float)image.Width / (float)image.Height)) / (float)2) * 0.75 + "pt";

        public static string GetPaddingLeft(Image img, int maxWidth, int maxHeight)
        {
            if (img == null)
                return "0pt";
            Size newSize = CalculateDimensions(img.Size, maxWidth, maxHeight);
            int res = Convert.ToInt32((maxWidth - newSize.Width) / 2 * 0.75);
            string padding = res + "pt";
            System.Diagnostics.Debug.WriteLine(padding);
            return padding;
        }

        //public static string GetPaddingTop(Image image) => (int)((151.0f - 151.0f * ((float)image.Height / (float)image.Width)) / (float)2) * 0.75 + "pt";
        public static string GetPaddingTop(Image img, int maxWidth, int maxHeight)
        {
            if (img == null)
                return "0pt";
            Size newSize = CalculateDimensions(img.Size, maxWidth, maxHeight);
            int res = Convert.ToInt32(((maxHeight - newSize.Height) / 2) * 0.75);
            string padding = res + "pt";
            System.Diagnostics.Debug.WriteLine(padding);
            return padding;
        }

        private static Size CalculateDimensions(Size currentSize, double maxWidth, double maxHeight)
        {
            var sourceWidth = (double)currentSize.Width;
            var sourceHeight = (double)currentSize.Height;

            var widthPercent = maxWidth / sourceWidth;
            var heightPercent = maxHeight / sourceHeight;

            var percent = heightPercent < widthPercent
                ? heightPercent
                : widthPercent;

            var destWidth = (int)(sourceWidth * percent);
            var destHeight = (int)(sourceHeight * percent);

            return new Size(destWidth, destHeight);
        }

    }
}
