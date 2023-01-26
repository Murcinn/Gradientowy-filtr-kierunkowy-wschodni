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


        public int _imageHeight;
        public int _imageWidth;

        public Interface( int startIndex, int endIndex, int imageWidth)
        {
            _startIndex = startIndex;
            _endIndex = endIndex;
            _imageWidth = imageWidth;

        }

        public abstract void ExecuteResult(byte[] allOrginalPixels, byte[] allNewPixels);

    }
}
