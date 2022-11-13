using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace JaProjektFiltr.Extension
{
    public static class BitmapExtension
    {
        public static float[] ConvertToBmpArrayBGR(this BitmapSource bitmapSource)
        {
            int step = bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel / 8);
            byte[] bytePixels = new byte[bitmapSource.PixelHeight * step];

            bitmapSource.CopyPixels(bytePixels, step, 0);

            float[] floatPixels = bytePixels.ConvertToFloatArray();

            return floatPixels;
        }
        //====
        public static float[] ConvertToFloatArray(this byte[] byteArray)
        {
            float[] newFloatArray = new float[byteArray.Length];
            for (int i = 0; i < newFloatArray.Length; i++)
            {
                newFloatArray[i] = (float)byteArray[i];
            }
            return newFloatArray;
        }

        public static BitmapSource ConvertBmpArrayBGRToImageFloat(this byte[] pixels, int width, int height, PixelFormat pixelFormat)
        {
            const int bitsInByte = 8;
            const int dpi = 300;
            WriteableBitmap bitmap = new WriteableBitmap(width, height, dpi, dpi, pixelFormat, null);

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * (bitmap.Format.BitsPerPixel / bitsInByte), 0);
            
            return bitmap;
        }
        //===
        public static byte[] ConvertToByteArray(this float[] floatArray)
        {
            byte[] newByteArray = new byte[floatArray.Length];
            for (int i = 0; i < newByteArray.Length; i++)
            {
                newByteArray[i] = (byte)floatArray[i];
            }
            return newByteArray;
        }


        public static BitmapSource ConvertBmpArrayBGRToImageByte(this float[] pixels, int width, int height, PixelFormat pixelFormat)
        {
            byte[] byteArray = pixels.ConvertToByteArray();
            return byteArray.ConvertBmpArrayBGRToImageFloat(width, height, pixelFormat);
        }


    }
}
