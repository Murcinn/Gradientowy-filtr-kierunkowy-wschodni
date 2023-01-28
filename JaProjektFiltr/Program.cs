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
        BitmapSource _newBitmap1 = new BitmapImage(new System.Uri("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\Temp\\bmpPath1.bmp"));





            double[] arr = new double[20];
            for (int i = 0; i < arr.Length; i++)
            {
                Interface myInter1 = new AssemblyFilter();

                TasksManager progAsm = new TasksManager(_newBitmap1, 5, myInter1);

                progAsm.SaveToFile("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\Temp\\bmpAsmOut.bmp", progAsm.RunProgram());
                //progAsm.RunProgram();
                arr[i] = progAsm.getTime();
                Console.WriteLine(arr[i]);

            }

            Console.WriteLine();
            Console.WriteLine();



            //double[] arr1 = new double[20];
            //for (int i = 0; i < arr1.Length; i++)
            //{
            //    Interface myInter = new CppFilter();

            //    TasksManager progCpp = new TasksManager(_newBitmap1, 5, myInter);
            //    progCpp.SaveToFile("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\Temp\\bmpCppOut.bmp", progCpp.RunProgram());
            //    //progCpp.RunProgram();


            //    //if (i == arr.Length - 1)
            //    //{

            //    //    progCpp.SaveToFile("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\Temp\\bmpCppOut.bmp", progCpp.RunProgram());
            //    //    //progCpp.RunProgram();


            //    //}
            //    //else
            //    //{

            //    //    //progCpp.SaveToFile("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\Temp\\bmpCppOut.bmp", progCpp.RunProgram());
            //    //    progCpp.RunProgram();

            //    //}
            //    arr1[i] = progCpp.getTime();
            //    Console.WriteLine(arr1[i]);

            //}

            //Array.Sort(arr);
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine(arr[10]);
            //Array.Sort(arr1);
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine(arr1[500]);

            Console.ReadLine();

        }
    }
}
