//#define DllExport extern "C" __declspec(dllexport)
#include "pch.h"
#include <vector>
#include <iostream>

float kernel[9] = {
				-1,  1,  1,
				-1, -2,  1,
				-1,  1,  1
};

constexpr int xd = 3;

inline BYTE CheckRange(int value)
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

		newPixels[i] = CheckRange((orginalPixels[i - imageWidth - xd] * kernel[0]) +
										(orginalPixels[i - imageWidth] * kernel[1]) +
										(orginalPixels[i - imageWidth + xd] * kernel[2]) +
										(orginalPixels[i - xd] * kernel[3]) +
										(orginalPixels[i] * kernel[4]) +
										(orginalPixels[i + xd] * kernel[5]) +
										(orginalPixels[i + imageWidth - xd] * kernel[6]) +
										(orginalPixels[i + imageWidth] * kernel[7]) +
										(orginalPixels[i + imageWidth + xd] * kernel[8]));
	 
	}
}




extern "C" __declspec(dllexport) void CppProc(
	BYTE * orginalPixels, BYTE * newPixels, int startIndex, int endIndex, int imageWidth)
{



	RndFilter(orginalPixels, newPixels, startIndex, endIndex, imageWidth);



}

