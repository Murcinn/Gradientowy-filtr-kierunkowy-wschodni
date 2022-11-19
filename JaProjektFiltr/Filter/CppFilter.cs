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
        public CppFilter(int bytesPerPixel, int startIndex, int endIndex) : base(bytesPerPixel, startIndex, endIndex) { }

         
        
        //"C:\\Users\\Marcin\\Desktop\\Gradientowy-filtr-kierunkowy-wschodni\\x64\\Debug\\CppProjekt.dll"
        [DllImport("C:\\Users\\Marcin\\Desktop\\Gradientowy-filtr-kierunkowy-wschodni\\x64\\Debug\\CppProjekt.dll", EntryPoint = "CppProc")]
        private static extern void CppProc(byte[] pixels, int size, int bytesPerPixel, int startIndex, int endIndex);

        public override void ExecuteResult(byte[] allPixels)
        {
            CppProc(allPixels, allPixels.Length, _bytesPerPixel, _startIndex, _endIndex);
        }

    }
}
