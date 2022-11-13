//#define DllExport extern "C" __declspec(dllexport)
#include "pch.h"
#include <vector>
#include <iostream>




void RndFilter(float* pixels, int size,int bytesPerPixel, int startIndex, int endIndex)
{

	for (int i = startIndex; i < endIndex && i < size; i += bytesPerPixel) {
	
		pixels[i] = 0;
		pixels[i+1] = pixels[i+1];
		pixels[i+2] = 0;
		
	
	}

}

extern "C" __declspec(dllexport) void CppProc(
	float* pixels, int size,int bytesPerPixel, int startIndex, int endIndex)
{
	RndFilter(pixels, size,bytesPerPixel, startIndex, endIndex);
    
}

