using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

namespace JaProjektFiltr.Filter
{
    internal class CppFilter : Interface
    {
        //public CppFilter(byte[] input, byte[] output, int startIndex, int endIndex, int _imageWidth) 
        //    : base( input, output, startIndex, endIndex, _imageWidth) { }



        //"C:\\Users\\Marcin\\Desktop\\Gradientowy-filtr-kierunkowy-wschodni\\x64\\Debug\\CppProjekt.dll"
        [DllImport("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\x64\\Debug\\CppProjekt.dll", EntryPoint = "CppProc")]
        private static extern void CppProc(byte[] input, byte[] output, int startIndex, int endIndex, int imageWidth);

        public override void ExecuteResult(byte[] input, byte[] output, int startIndex, int endIndex, int imageWidth)
        {
            CppProc(input, output, startIndex, endIndex, imageWidth);
        }

    }
}
