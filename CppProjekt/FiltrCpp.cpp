//#define DllExport extern "C" __declspec(dllexport)
#include "pch.h"
#include <vector>
#include <iostream>



constexpr int xd = 3;

inline int RndFilter(BYTE* orginalPixels, BYTE* newPixels, int startIndex, int endIndex, int imageWidth, int index, float* kernel)
{
	return			(orginalPixels[index - imageWidth - xd] * kernel[0]) +
		(orginalPixels[index - imageWidth] * kernel[1]) +
		(orginalPixels[index - imageWidth + xd] * kernel[2]) +
		(orginalPixels[index - xd] * kernel[3]) +
		(orginalPixels[index] * kernel[4]) +
		(orginalPixels[index + xd] * kernel[5]) +
		(orginalPixels[index + imageWidth - xd] * kernel[6]) +
		(orginalPixels[index + imageWidth] * kernel[7]) +
		(orginalPixels[index + imageWidth + xd] * kernel[8]);

}

inline BYTE CutRange(int value)
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

	return static_cast<BYTE>(newValue);;
}


extern "C" __declspec(dllexport) void CppProc(
	BYTE * orginalPixels, BYTE * newPixels, int startIndex, int endIndex, int imageWidth)
{
	float kernel[9] = {
					-1,  1,  1,
					-1, -2,  1,
					-1,  1,  1
	};

	for (int i = startIndex; i < endIndex; i++) {
		int temp = CutRange(RndFilter(orginalPixels, newPixels, startIndex, endIndex, imageWidth, i, kernel));

		//if (temp > 255)
		//{
		//	temp = 255;
		//}
		//else if (temp < 0)
		//{
		//	temp = 0;
		//}

		newPixels[i] = temp;
	}
}

