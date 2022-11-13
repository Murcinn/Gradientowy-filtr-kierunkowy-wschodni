using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JaProjektFiltr.Filter
{
    internal class AssemblyFilter : Interface
    {
        private float[] array255 = { -1.0f, -1.0f, -1.0f, 255.0f };

        public AssemblyFilter(int bytesPerPixel,int startIndex, int endIndex): base( bytesPerPixel, startIndex, endIndex) { }

        [DllImport("C:\\Users\\Marcin\\Desktop\\Gradientowy-filtr-kierunkowy-wschodni\\x64\\Debug\\AsmProjekt.dll", EntryPoint = "AsmProc")]
        private static extern void AsmProc(float[] pixels, float[] array255, int startIndex, int endIndex);

        public override void ExecuteResult(float[] allPixels)
        {

            AsmProc(allPixels, array255, _startIndex, _endIndex);

        }
    }





}

