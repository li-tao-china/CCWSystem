#pragma once
#include <iostream>

#ifdef DLLTEST1_EXPORTS
#define DLLTEST1_API __declspec(dllexport)
#else
#define DLLTEST1_API __declspec(dllimport)
#endif

class DLLTEST1_API TestClass {
public:
	TestClass(void);
	void Hi(void);
	double Num(double num);
private:
	double data;
};

extern DLLTEST1_API int ntestdll;
DLLTEST1_API int fntestdll(void);