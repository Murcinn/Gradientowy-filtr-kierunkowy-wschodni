//#define DllExport extern "C" __declspec(dllexport)
#include "pch.h"
#include <vector>
#include <iostream>

int kernel[9] = {
				-1,  1,  1,
				-1, -2,  1,
				-1,  1,  1
};

constexpr int step = 3;

BYTE CheckRange(int value)
{
	int newValue = value;

	if (value > 255)
	{
		newValue = 255;
	}
	else if (value < 0)
	{
		newValue = 0;
	}
	return newValue;
}

void RndFilter(BYTE* orginalPixels, BYTE* newPixels, int startIndex, int endIndex, int imageWidth)
{
	for (int i = startIndex; i < endIndex; i++) {

		newPixels[i] = CheckRange((orginalPixels[i - imageWidth - step] * kernel[0]) +
										(orginalPixels[i - imageWidth] * kernel[1]) +
										(orginalPixels[i - imageWidth + step] * kernel[2]) +
										(orginalPixels[i - step] * kernel[3]) +
										(orginalPixels[i] * kernel[4]) +
										(orginalPixels[i + step] * kernel[5]) +
										(orginalPixels[i + imageWidth - step] * kernel[6]) +
										(orginalPixels[i + imageWidth] * kernel[7]) +
										(orginalPixels[i + imageWidth + step] * kernel[8]));	 
	}
}




extern "C" __declspec(dllexport) void CppProc(
	BYTE * orginalPixels, BYTE * newPixels, int startIndex, int endIndex, int imageWidth)
{



	RndFilter(orginalPixels, newPixels, startIndex, endIndex, imageWidth);



}

