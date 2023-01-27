using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaProjektFiltr.Filter
{
    internal abstract class Interface
    {

        ////RGB 0.114f, 0.587f, 0.299f
        //public int _startIndex;
        //public int _endIndex;
        ////na protected

        //public int _imageHeight;
        //public int _imageWidth;

        //public byte[] _input;
        //public byte[] _output;

        //public Interface(byte[] input, byte[] output, int startIndex, int endIndex, int _imageWidth)
        //{
        //    _startIndex = startIndex;
        //    _endIndex = endIndex;
        //    _imageWidth = _imageWidth;
        //    _input = input;
        //    _output = output;

        //}

        public abstract void ExecuteResult(byte[] allOrginalPixels, byte[] allNewPixels, int startIndex, int endIndex, int imageWidth);

    }
}
