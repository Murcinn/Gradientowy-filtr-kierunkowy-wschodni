using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.IO;
//using static System.Net.Mime.MediaTypeNames;


namespace JaProjektFiltr
{
    
    //internal
    internal class Program
    {


        [DllImport(@"F:\GitHub\Gradientowy-filtr-kierunkowy-wschodni\x64\Debug\CppProjekt.dll")]
        public static extern void ChangeBitmapCpp(byte[] _data, int _dataLen, ref ImageInfo imTemplate);

        [DllImport(@"F:\GitHub\Gradientowy-filtr-kierunkowy-wschodni\x64\Debug\CppProjekt.dll")]
        public static extern void ReleaseMemoryCpp(IntPtr buf);
       
        //public struct ImageInfo
        //{
        //    public IntPtr _data;
        //    public int _size;
        //}

        //public static Image ConvertImage(Image image)
        //{
        //    MemoryStream convertedImageMemoryStream;
        //    using (MemoryStream sourceImageStream = new MemoryStream())
        //    {
        //        image.Save(sourceImageStream, System.Drawing.Imaging.ImageFormat.Bmp);
        //        byte[] sourceImagePixels = sourceImageStream.ToArray();
        //        ImageInfo imInfo = new ImageInfo();
        //        ChangeBitmapCpp(sourceImagePixels, sourceImagePixels.Count(), ref imInfo);

        //        byte[] imagePixels = new byte[imInfo._size];
        //        Marshal.Copy(imInfo._data, imagePixels, 0, imInfo._size);
        //        if (imInfo._data != IntPtr.Zero)
        //            ReleaseMemoryCpp(imInfo._data);
        //        convertedImageMemoryStream = new MemoryStream(imagePixels);
        //        image.Save(convertedImageMemoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                
                
        //    }
            
        //    Image processed = new Bitmap(convertedImageMemoryStream);
        //    //processed.Save("C:\\Users\\Marcin\\Desktop\\aaa.bmp");
        //    return processed;
        //}

        [DllImport(@"F:\GitHub\Gradientowy-filtr-kierunkowy-wschodni\x64\Debug\AsmProjekt.dll")]
        static extern int AsmProc(int a, int b);
        [DllImport(@"F:\GitHub\Gradientowy-filtr-kierunkowy-wschodni\x64\Debug\CppProjekt.dll")]
        static extern int CppProc(int a, int b);

        private static List<Task> _tasksCpp = new List<Task>();
        private static List<Task> _tasksAsm = new List<Task>();

        private static void runCppProc(int x, int y) {
            int res = CppProc(x, y);
            Console.Write("Suma Cpp: ");
            Console.WriteLine(res+"\n");

        }
        private static void runAsmProc(int x, int y)
        {
            int res = AsmProc(x, y);
            Console.Write("Suma Asm: ");
            Console.WriteLine(res + "\n");

        }

        public static void changeBitmap(Bitmap bmp)
        {

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {

                    Color clr = bmp.GetPixel(x, y);
                    Color newClr = Color.FromArgb(clr.G, clr.B, 0);
                    bmp.SetPixel(x, y, newClr);

                }
            }

        }
        //==============================================================


        [DllImport(@"F:\GitHub\Gradientowy-filtr-kierunkowy-wschodni\x64\Debug\CppProjekt.dll")]
        public static extern void ChangeBitmapCpp1(out IntPtr ptr, int width, int height);
        
        public IntPtr Scan0
        {
            get;
            set;
        }
        
        public static void ConvertImage1(Bitmap finalBmp)
        {
            Rectangle rect = new Rectangle(0, 0, finalBmp.Width, finalBmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                finalBmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                finalBmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;



            ChangeBitmapCpp1(out ptr, bmpData.Stride, finalBmp.Height);
            finalBmp.UnlockBits(bmpData);
            finalBmp.Save("C:\\Users\\Marcin\\Desktop\\aaa.bmp");

        }


        static void Main(string[] args)
        {
            int tempNumOFThreads = 5;
            Random rnd = new Random();

            //string curDir = Directory.GetDirectories();
            //string myPath = Environment.GetFolderPath(Environment.);
            //Console.WriteLine(curDir);

            Bitmap bitmap = (Bitmap)Image.FromFile(@"F:\GitHub\Gradientowy-filtr-kierunkowy-wschodni\Temp\bmpPath.bmp");
            Console.WriteLine("Number of threads: " + tempNumOFThreads);
            ConvertImage1(bitmap);
            //for (int i = 0; i < tempNumOFThreads; i++) {             
            // _tasksCpp.Add(new Task(() => runCppProc(rnd.Next(10), rnd.Next(10))));
            //}

            //for (int i = 0; i < tempNumOFThreads; i++)
            //{
            // _tasksAsm.Add(new Task(() => runAsmProc(rnd.Next(10), rnd.Next(10))));
            //}

            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            //Parallel.ForEach(_tasksCpp, (task) => task.Start());
            //Task.WaitAll(_tasksCpp.ToArray());



            //Parallel.ForEach(_tasksAsm, (task) => task.Start());
            //Task.WaitAll(_tasksAsm.ToArray());

            //sw.Stop();
            //System.TimeSpan elTime = sw.Elapsed;
            //changeBitmap(bitmap);



            //ConvertImage(bitmap).Save(@"F:\GitHub\Gradientowy-filtr-kierunkowy-wschodni\Temp\output.png");



        }
    }
}
