// Dlltest2.cpp: 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "Dlltest2.h"

#define DLL_EXPORTS

extern "C" DLL_API ExportInterface* getInstance()
{
	ExportInterface* pInstance = new ExportClass();
	return pInstance;
}

void ExportClass::foo()
{
	//do something...
	return;
}

