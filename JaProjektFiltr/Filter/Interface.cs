using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaProjektFiltr.Filter
{
    public abstract class Interface
    {

        //RGB 0.114f, 0.587f, 0.299f
        protected int _startIndex;
        protected int _endIndex;
        protected int _bytesPerPixel;

        public Interface(int bytesPerPixel, int startIndex, int endIndex)
        {
            _startIndex = startIndex;
            _endIndex = endIndex;
            _bytesPerPixel = bytesPerPixel;
        }

        public abstract void ExecuteResult(byte[] allPixels);

    }
}
