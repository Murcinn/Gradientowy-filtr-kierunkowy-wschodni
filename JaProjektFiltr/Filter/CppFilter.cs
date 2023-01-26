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
        public CppFilter( int startIndex, int endIndex, int imageWidth) 
            : base( startIndex, endIndex, imageWidth) { }



        //"C:\\Users\\Marcin\\Desktop\\Gradientowy-filtr-kierunkowy-wschodni\\x64\\Debug\\CppProjekt.dll"
        [DllImport("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\x64\\Debug\\CppProjekt.dll", EntryPoint = "CppProc")]
        private static extern void CppProc(byte[] orginalPixels, byte[] newPixels, int startIndex, int endIndex, int imageWidth);

        public override void ExecuteResult(byte[] allOrginalPixels, byte[] allNewPixels)
        {
            CppProc(allOrginalPixels, allNewPixels , _startIndex, _endIndex, _imageWidth);
        }

    }
}
