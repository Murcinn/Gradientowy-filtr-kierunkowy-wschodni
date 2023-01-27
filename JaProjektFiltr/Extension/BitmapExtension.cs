using System.Windows.Media.Imaging;
using System.Windows;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System;

namespace JaProjektFiltr.Extension
{
    public static class BitmapExtension
    {
        private const PixelFormat Rgb24Format = PixelFormat.Format24bppRgb;
        public static byte[] ConvertBitmapToByteArray(this Bitmap bmp, bool fillWithData = true)
        {
            System.Drawing.Imaging.PixelFormat Rgb24Format = System.Drawing.Imaging.PixelFormat.Format24bppRgb;

            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, Rgb24Format);

            byte[] outputBytes = new byte[(Math.Abs(bmpData.Stride) * bmp.Height)];

            if (fillWithData)
            {
                Marshal.Copy(bmpData.Scan0, outputBytes, 0, (Math.Abs(bmpData.Stride) * bmp.Height));
            }

            bmp.UnlockBits(bmpData);
            return outputBytes;
        }

        public static Bitmap BitmapFromBitmapSource(this BitmapSource bitmapsource)
        {
            Bitmap bitmap;
            using (var outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);
            }
            return bitmap;
        }

        public static Bitmap ConvertByteArrayToBitmap(this byte[] byteIn, int w, int h)
        {
            Bitmap outputBitmap = new Bitmap(w, h, Rgb24Format);
            BitmapData bmpData = outputBitmap.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, Rgb24Format);
            Marshal.Copy(byteIn, 0, bmpData.Scan0, (Math.Abs(bmpData.Stride) * h));
            outputBitmap.UnlockBits(bmpData);
            return outputBitmap;
        }

    }
}
