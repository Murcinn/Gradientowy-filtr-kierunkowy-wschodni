using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaProjektFiltr.Filter
{
    internal abstract class Interface
    {

        public abstract void ExecuteResult(byte[] allOrginalPixels, byte[] allNewPixels, int startIndex, int endIndex, int imageWidth);

    }
}
