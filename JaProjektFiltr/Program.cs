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

            //User interaction

            String algorithmType;
            String filePath;
            String numOfThreads;

            Console.WriteLine("Choose algorithm you want to use (1/2):");
            Console.WriteLine("1.Assembler Algorithm");
            Console.WriteLine("2.Cpp Algorithm");
            algorithmType=Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter file path:");
            filePath= Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Give number of threads:");
            numOfThreads = Console.ReadLine();
            Console.Clear();

            BitmapSource newBitmap = new BitmapImage(new System.Uri(filePath));


            if (int.Parse(algorithmType) == 1) {
                double[] arr = new double[100];
                for (int i = 0; i < arr.Length; i++)
                {
                    Interface myInter1 = new CppFilter();

                    TasksManager progAsm = new TasksManager(newBitmap, int.Parse(numOfThreads), myInter1);

                    progAsm.SaveToFile("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\Temp\\bmpAsmOut.bmp", progAsm.RunProgram());
                    //progAsm.RunProgram();
                    arr[i] = progAsm.getTime();
                    //Console.WriteLine(arr[i]);

                    Array.Sort(arr);
                    Console.WriteLine("Algorithm parameters:");
                    Console.WriteLine();
                    Console.WriteLine("Number of threads: " + int.Parse(numOfThreads));
                    Console.WriteLine("Algorithm language: Cpp");
                    Console.WriteLine("algorithm duration: " + arr[50]+"ms");


                }
            } else if (int.Parse(algorithmType) == 2) {

                double[] arr = new double[100];
                for (int i = 0; i < arr.Length; i++)
                {
                    Interface myInter1 = new AssemblyFilter();

                    TasksManager progAsm = new TasksManager(newBitmap, int.Parse(numOfThreads), myInter1);

                    progAsm.SaveToFile("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\Temp\\bmpAsmOut.bmp", progAsm.RunProgram());
                    //progAsm.RunProgram();
                    arr[i] = progAsm.getTime();
                    //Console.WriteLine(arr[i]);


                }
                Array.Sort(arr);
                Console.WriteLine("Algorithm parameters:");
                Console.WriteLine();
                Console.WriteLine("Number of threads: "+ int.Parse(numOfThreads));
                Console.WriteLine("Algorithm language: Assembler");
                Console.WriteLine("algorithm duration: "+arr[50] + "ms");


            }



            Console.ReadLine();

        }
    }
}
