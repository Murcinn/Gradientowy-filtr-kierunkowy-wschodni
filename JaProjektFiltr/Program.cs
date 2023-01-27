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
       

        static void Main(string[] args)
        {


        BitmapSource _newBitmap = new BitmapImage(new System.Uri("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\Temp\\bmpPath.bmp"));
        BitmapSource _newBitmap1 = new BitmapImage(new System.Uri("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\Temp\\bmpPath.bmp"));

            Interface myInter = new CppFilter();

            TasksManager progCpp = new TasksManager(_newBitmap,2, myInter);

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
