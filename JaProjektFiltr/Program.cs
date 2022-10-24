using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Diagnostics;

namespace JaProjektFiltr
{
    
    //internal
    internal class Program
    {
        [DllImport(@"D:\JaProjektFiltr\x64\Debug\AsmProjekt.dll")]
        static extern int AsmProc(int a, int b);
        [DllImport(@"D:\JaProjektFiltr\x64\Debug\CppProjekt.ddl")]
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

        static void Main(string[] args)
        {
            int tempNumOFThreads = 5;
            Random rnd = new Random();

            //int x = 5, y = 3;
            //int fir = AsmProc(x, y);
            //// int sec = CppProc(x, y);

            //Console.Write("Suma Asm:");
            //Console.WriteLine(fir);
            ////Console.WriteLine("\n");

            //// Console.Write("Suma Cpp:");
            ////  Console.WriteLine(sec);

            Bitmap bitmap = (Bitmap)Image.FromFile(@"D:\JaProjektFiltr\bmpPath.bmp");
            Console.WriteLine("Number of threads: " + tempNumOFThreads);

            for (int i = 0; i < tempNumOFThreads; i++) { 
            
             _tasksCpp.Add(new Task(() => runCppProc(rnd.Next(10), rnd.Next(10))));
            }

            for (int i = 0; i < tempNumOFThreads; i++)
            {

                _tasksAsm.Add(new Task(() => runAsmProc(rnd.Next(10), rnd.Next(10))));
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Parallel.ForEach(_tasksCpp, (task) => task.Start());
            Task.WaitAll(_tasksCpp.ToArray());
            
            

            Parallel.ForEach(_tasksAsm, (task) => task.Start());
            Task.WaitAll(_tasksAsm.ToArray());

            sw.Stop();
            System.TimeSpan elTime = sw.Elapsed;
        }
    }
}
