//#define DllExport extern "C" __declspec(dllexport)
#include "pch.h"
#include <vector>
#include <iostream>




extern "C" __declspec(dllexport) int CppProc(int x, int y) {

	return x + y;
    
}



//extern "C" __declspec(dllexport) int changeBitmap(Bitmap bmp) {
//
//    for (int x = 0; x < bmp.Width; x++) {
//        for (int y = 0; y < bmp.Height; y++) {
//
//            Color clr = bmp.GetPixel(x, y);
//            Color newClr = Color.FromArgb(0, clr.B, 0);
//            bmp.SetPixel(x, y, newClr);
//
//        }
//    }
//
//}

struct ImageInfo
{
	unsigned char* _data;
	int _size;
};

extern "C" __declspec(dllexport) void ChangeBitmapCpp(unsigned char* data, int dataLen, ImageInfo & imInfo) {
   
    std::vector<unsigned char> inputImageBytes(data, data + dataLen);
    //dataLen = dataLen = 54;
    std::vector<unsigned char> tempVec(dataLen);

   // for (int i = 0; i < dataLen; i++) {
        //inputImageBytes.at(i) == (255 - inputImageBytes.at(i))/2;
   //     inputImageBytes[i] = 100;

        //::cout << &inputImageBytes[i] << std::endl;
        
   // }
    //std::cout << inputImageBytes.size() << " noooo";
    //std::cout << dataLen << " noooo";
    
    //std::cout << inputImageBytes.at(500) << "xdd";

   // inputImageBytes[1] = 100;
   // unsigned char* output = new unsigned char[dataLen];

    //for (int i = 0; i < dataLen; i++) {
    //    output[i] = 255 - data[i];
    //
    //}
   
   // std::vector<unsigned char> tempVec(output,output+dataLen);
   

    for (size_t i = 0; i < dataLen; i += 3) {
        inputImageBytes.at(i) = 0;
        inputImageBytes.at(i+1) = 0;
        inputImageBytes.at(i+2) = 0;
    
    
    }
    tempVec = inputImageBytes;

	//imInfo._size = tempVec.size();
	//imInfo._data = (unsigned char*)calloc(imInfo._size, sizeof(unsigned char));
	//std::copy(tempVec.begin(), tempVec.end(), imInfo._data);
    
    


}


extern "C" __declspec(dllexport) bool ReleaseMemoryCpp(unsigned char* buf)
{
    if (buf == NULL)
        return false;

    free(buf);
    return true;
}


extern "C" __declspec(dllexport) void ChangeBitmapCpp1(BYTE * *pixelValues, int width, int height) {

for (size_t i = 0; i < width * height; i++) {

    (*pixelValues)[i] = 255 - (*pixelValues)[i];

}


}