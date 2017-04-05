#ifndef IMAGEREADER_H
#define IMAGEREADER_H


/// DLL导出定义
////////////////////////////////////////////////////////////////////////
#ifdef IMAGEREADER_EXPORTS
#define IMAGEREADER_C __declspec(dllexport)
#else
#define IMAGEREADER_C 
#endif

#ifdef __cplusplus
extern "C" {
#endif

	IMAGEREADER_C int ImageReaderBgrFromByte(char* imgIn, int iImgInLen, char* imageData, int* imageDataLength, int* width, int* height, int* NumberOfChannels);
	IMAGEREADER_C int ImageReaderBgrFromFile(char* imagePath, char* imageData, int* imageDataLength, int* width, int* height, int* NumberOfChannels);

#ifdef __cplusplus
}
#endif

#endif
 