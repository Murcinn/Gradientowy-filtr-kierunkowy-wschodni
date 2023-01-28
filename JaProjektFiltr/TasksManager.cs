using JaProjektFiltr.Extension;
using JaProjektFiltr.Filter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace JaProjektFiltr
{
    internal class TasksManager
    {
        private BitmapSource _oldBitmapSource;

        Interface _interface;
        private List<Task> _tasks = new List<Task>();
        private byte[] _allOrginalPixels;
        private byte[] _allNewPixels;

        private int _numberOfThreads;
        private int _rowsPerThread;


        int _imageWidth;
        int _imageHeight;

        int _additionalLastThreadRows;

        double timeResult;



        public TasksManager(BitmapSource bitmapImage, int numberOfThreads, Interface inter)
        {

            _oldBitmapSource = bitmapImage;

            _interface = inter;
            _allOrginalPixels = bitmapImage.BitmapFromBitmapSource().ConvertBitmapToByteArray();
            _allNewPixels = bitmapImage.BitmapFromBitmapSource().ConvertBitmapToByteArray(false);

            _numberOfThreads = numberOfThreads;
            _rowsPerThread = (bitmapImage.PixelHeight - 2) / _numberOfThreads;

            _imageWidth = _oldBitmapSource.PixelWidth;
            _imageHeight = _oldBitmapSource.PixelHeight;

            CalculateThreadsValues();
            //MakeAdditionalThreadRows();
            SetTasks();

        }


        private void SetTasks()
        {

            for (int partNumber = 0; partNumber < _numberOfThreads - 1; partNumber++)
            {
                _tasks.Add(CreateTask(partNumber));
            }

            _tasks.Add(CreateTask(_numberOfThreads - 1, _additionalLastThreadRows));


        }


        private Task CreateTask(int taskID, int additionalRows=0)
        {

            return new Task(() => {

                for (int i = 0; i < _rowsPerThread + additionalRows; i++)
                {

                    int startId = SetStartIndex(taskID, i);
                    int endId = startId + (_imageWidth * 3 - 6);
                    _interface.ExecuteResult(_allOrginalPixels, _allNewPixels, startId, endId, _imageWidth * 3);

                }

            });
        }

        private int SetStartIndex(int taskID, int threadRow)
        {

            int temp = _imageWidth * 3;
            int threadPoolFirstObject = temp + (taskID * _rowsPerThread * temp);
            int currentIndex = threadPoolFirstObject + (threadRow * temp) + 3;
            return currentIndex;

        }

        //private void MakeAdditionalThreadRows()
        //{

        //    _rowsPerThread = _imageHeight / _numberOfThreads;

        //    if (_numberOfThreads * _rowsPerThread != _imageHeight)
        //    {
        //        _additionalLastThreadRows = _imageHeight % _numberOfThreads;
        //    }

        //}

        private void CalculateThreadsValues()
        {
            //int width = _bmp.Width;
            //int height = _bmp.Height;

           // realWidth = _bmp.Stride - 2 * _pixelStride;
            //realHeight = height - 2;

            if (_numberOfThreads <= 0)
            {
                _numberOfThreads = 1;
            }
            else if (_numberOfThreads >= 64)
            {
                _numberOfThreads = 64;
            }

            _rowsPerThread = (_oldBitmapSource.PixelHeight-2) / _numberOfThreads;

            if (_numberOfThreads * _rowsPerThread != (_oldBitmapSource.PixelHeight - 2))
            {
                _additionalLastThreadRows = (_oldBitmapSource.PixelHeight - 2) % _numberOfThreads;
            }
        }


        public double getTime() {

            return timeResult;
        }

        public byte[] RunProgram()
        {

            System.TimeSpan elapsedTime;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Parallel.ForEach(_tasks, (task) => task.Start());
            Task.WaitAll(_tasks.ToArray());
            
            stopwatch.Stop();
            elapsedTime = stopwatch.Elapsed;
            timeResult= stopwatch.Elapsed.TotalMilliseconds;


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

        public void SaveToFile(String path, byte[] output)
        {
            Bitmap newBitmap = output.ConvertByteArrayToBitmap(_oldBitmapSource.PixelWidth, _oldBitmapSource.PixelHeight);
            newBitmap.Save(path);
        }





    }
}
