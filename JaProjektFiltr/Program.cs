using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;


using System.Windows.Media.Imaging;

using JaProjektFiltr.Filter;
using JaProjektFiltr.Extension;

namespace JaProjektFiltr
{
    enum ChooseDLL
    {
        Assembly,
        Cpp
    }

    internal class Program
    {
        private BitmapSource _oldBitmapSource;

        Interface           _interface;
        private List<Task>  _tasks = new List<Task>();
        private byte[]      _allOrginalPixels;
        private byte[]      _allNewPixels;

        private int         _numberOfThreads;
        private int         _rowsPerThread;


        int _imageWidth;
        int _imageHeight;

        int _additionalLastThreadRows;

        private const PixelFormat Rgb24Format = PixelFormat.Format24bppRgb;



        public Program(BitmapSource bitmapImage, int numberOfThreads, Interface inter)
        {
            _oldBitmapSource = bitmapImage;

            _interface = inter;
            _allOrginalPixels = BitmapToByteArray(BitmapFromSource(bitmapImage));
            _allNewPixels = BitmapToByteArray(BitmapFromSource(bitmapImage), false);

            _numberOfThreads = numberOfThreads;
            _rowsPerThread = (bitmapImage.PixelHeight - 2) / _numberOfThreads;

            _imageWidth = _oldBitmapSource.PixelWidth;
            _imageHeight = _oldBitmapSource.PixelHeight;


            MakeAdditionalThreadRows();
            SetTasks();

        }


        private void SetTasks() {

            for (int partNumber = 0; partNumber < _numberOfThreads-1; partNumber++)
            {
                _tasks.Add(CreateTask(partNumber,0));
            }
            Console.WriteLine("xddddd");
            _tasks.Add(CreateTask(_numberOfThreads-1,_additionalLastThreadRows));


        }



            private Task CreateTask(int taskID, int additionalRows){
            
            return new Task(() =>{
                
                for (int i = 0; i < _rowsPerThread+ additionalRows; i++){

                    int startId = SetStartIndex(taskID, i);
                    int endId = startId + (_imageWidth*3 - 6);
                    _interface.ExecuteResult(_allOrginalPixels, _allNewPixels, startId, endId, _imageWidth*3);


                }

            });
        }

        private int SetStartIndex(int taskID, int threadRow) {

            int temp = _imageWidth * 3;
            int threadPoolFirstObject = temp + (taskID * _rowsPerThread * temp);
            int currentIndex = threadPoolFirstObject + (threadRow * temp) + 3;
            return currentIndex;


        }

        private void MakeAdditionalThreadRows() {

            //_rowsPerThread = _imageHeight / _numberOfThreads;

            if (_numberOfThreads * _rowsPerThread != _imageHeight)
            {
                _additionalLastThreadRows = _imageHeight % _numberOfThreads;
            }

        }

        byte[] BitmapToByteArray(Bitmap bmp, bool fillWithData = true)
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

        public static Bitmap BitmapFromSource(BitmapSource bitmapsource)
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



        public  byte[] RunProgram(out System.TimeSpan elapsedTime)
        {
            //BitmapSource

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Parallel.ForEach(_tasks, (task) => task.Start());
            Task.WaitAll(_tasks.ToArray());

            stopwatch.Stop();
            elapsedTime = stopwatch.Elapsed;

            //return _allOrginalPixels.ConvertBmpArrayBGRToImageFloat(_oldBitmapSource.PixelWidth, _oldBitmapSource.PixelHeight, _oldBitmapSource.Format);
            return _allNewPixels;


        }

        private void SaveImageToDisk(BitmapSource image, string filePath)
        {

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }

        }

        public void SaveToFile(String path,byte[] output)
        {
            Bitmap newBitmap = NiezlaFunkcja(output, _oldBitmapSource.PixelWidth, _oldBitmapSource.PixelHeight);
            newBitmap.Save(path);
        }


        public Bitmap NiezlaFunkcja(byte[] byteIn,int w, int h)
        {
            Bitmap outputBitmap = new Bitmap(w,h, Rgb24Format);
            BitmapData bmpData = outputBitmap.LockBits(new Rectangle(0, 0, w, h ), ImageLockMode.WriteOnly, Rgb24Format);
            Marshal.Copy(byteIn, 0, bmpData.Scan0, (Math.Abs(bmpData.Stride) * _oldBitmapSource.PixelHeight));
            outputBitmap.UnlockBits(bmpData);
            return outputBitmap;
        }

        static void Main(string[] args)
        {




        BitmapSource _newBitmap = new BitmapImage(new System.Uri("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\Temp\\bmpPath.bmp"));
        BitmapSource _newBitmap1 = new BitmapImage(new System.Uri("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\Temp\\bmpPath.bmp"));

            Interface myInter = new CppFilter();

            Program progCpp = new Program(_newBitmap,2, myInter);

            //BitmapSource resCpp = progCpp.RunProgram(out System.TimeSpan elapsedTimeCpp);
            byte[] resCpp = progCpp.RunProgram(out System.TimeSpan elapsedTimeCpp);

            //progCpp.SaveImageToDisk(resCpp, "F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\Temp\\bmpCppOut.bmp");

            progCpp.SaveToFile("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\Temp\\bmpCppOut.bmp",resCpp);
            Console.Write("Czas wykonywania programu: " + elapsedTimeCpp + "\n\n");



            //Interface myInter1 = new AssemblyFilter();

            //Program progAsm = new Program(_newBitmap1, 2, myInter1);

            //byte[] resAsm = progAsm.RunProgram(out System.TimeSpan elapsedTimeAsm);

            //progAsm.SaveToFile( "F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\Temp\\bmpAsmOut.bmp",resAsm);

            //Console.Write("Czas wykonywania programu: " + elapsedTimeAsm + "\n\n");

            //Console.ReadLine();
            Environment.Exit(0);



        }
    }
}
