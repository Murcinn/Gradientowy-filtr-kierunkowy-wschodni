//#define DllExport extern "C" __declspec(dllexport)
#include "pch.h"
#include <vector>
#include <iostream>



constexpr int xd = 3;

inline int RndFilter(BYTE* orginalPixels, BYTE* newPixels, int startIndex, int endIndex, int imageWidth,int index, float* kernel)
{	
	return			(orginalPixels[index - imageWidth - xd]				* kernel[0]) +
					(orginalPixels[index - imageWidth]					* kernel[1]) +
					(orginalPixels[index - imageWidth + xd]				* kernel[2]) +
					(orginalPixels[index - xd]							* kernel[3]) +
					(orginalPixels[index]								* kernel[4]) +
					(orginalPixels[index + xd]							* kernel[5]) +
					(orginalPixels[index + imageWidth - xd]				* kernel[6]) +
					(orginalPixels[index + imageWidth]					* kernel[7]) +
					(orginalPixels[index + imageWidth	+ xd]			* kernel[8]);

	//int k = newPixels[i];
		//newPixels[i] = orginalPixels[endIndex - i];
		

		//if (newPixels[i] > 255)
		//{
		//	newPixels[i] = 255;
		//}
		//else if (newPixels[i] < 0)
		//{
		//	newPixels[i] = 0;
		//}


	
	//}

	//for (int i = startIndex; i < endIndex; i += 3) {

	//	newPixels[i] = 0;
	//	newPixels[i + 1] = orginalPixels[i + 1];
	//	newPixels[i + 2] = 0;
	//}

	//std::cout << "xdd";




	//for (int i = startIndex; i < endIndex; i += 3) {

	//	newPixels[i] = 0;
	//	newPixels[i + 1] = 0;
	//	newPixels[i + 2] = orginalPixels[i + 2];;
	//}

	//BYTE* tempPixels = pixels;

	//for (int i = startIndex; i < endIndex && i < size; i ++) {
	//	
	//	int finalValue = 0;
	//	

	//	int xKernel=0;
	//	for (int j = 0; j < 3*imageWidth; j+=imageWidth) {
	//		
	//			//int a = tempPixels[j];
	//			//int b = tempPixels[j + bytesPerPixel];
	//			//int c = tempPixels[j + bytesPerPixel * 2];


	//			finalValue += tempPixels[i+j				  ] * kernel[xKernel][j];
	//			finalValue += tempPixels[i+j + bytesPerPixel] * kernel[xKernel][j];
	//			finalValue += tempPixels[i+j + bytesPerPixel*2] * kernel[xKernel][j];
	//			finalValue += tempPixels[i+j + bytesPerPixel*3] * kernel[xKernel][j];
	//			xKernel++;
	//			//xd = tempPixels[j] + tempPixels[j + bytesPerPixel] + tempPixels[j + bytesPerPixel*2] + tempPixels[j + bytesPerPixel*3];
	//	}
	//	pixels[i+imageWidth] = finalValue;

	//}




}




extern "C" __declspec(dllexport) void CppProc(
	BYTE * orginalPixels, BYTE * newPixels, int startIndex, int endIndex, int imageWidth)
{
	float kernel[9] = {
					-1,  1,  1,
					-1, -2,  1,
					-1,  1,  1
	};

	for (int i = startIndex; i < endIndex; i ++) {
		int temp =RndFilter(orginalPixels, newPixels, startIndex, endIndex, imageWidth,i,kernel);

		if (temp > 255)
		{
			temp = 255;
		}
		else if (temp < 0)
		{
			temp = 0;
		}

		newPixels[i] = temp;
	}
}

