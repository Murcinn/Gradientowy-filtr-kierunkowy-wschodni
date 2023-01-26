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
       // private float[] array255 = { -1.0f, -1.0f, -1.0f, 255.0f };

        private sbyte[] array255 = new sbyte[] { -1, -1, -1, 127 };

        public AssemblyFilter(int startIndex, int endIndex, int imageWidth) 
            : base( startIndex, endIndex, imageWidth) { }

        [DllImport("F:\\GitHub\\Gradientowy-filtr-kierunkowy-wschodni\\x64\\Debug\\AsmProjekt.dll", EntryPoint = "AsmProc")]
        private static extern void AsmProc(byte[] orginalPixels, byte[] newPixels, sbyte[] array255, int startIndex, int endIndex, int imageWidth);

        public override void ExecuteResult(byte[] allOrginalPixels, byte[] allNewPixels)
        {

            AsmProc(allOrginalPixels, allNewPixels, array255, _startIndex, _endIndex, _imageWidth);

        }
    }





}

