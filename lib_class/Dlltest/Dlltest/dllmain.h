#pragma once
#ifdef DLLTEST_EXPORTS
#define DLLTEST_API __declspec(dllexport)
#else
#define DLLTEST_API __declspec(dllimport)
#endif

class Tower
{
public:
	virtual void Hi() = 0;
	virtual int Test() = 0;
	virtual void Release() = 0;
};

extern "C" DLLTEST_API Tower* _stdcall CreateExportObj();
extern "C" DLLTEST_API void _stdcall DestoryExportObj(Tower* cExport);