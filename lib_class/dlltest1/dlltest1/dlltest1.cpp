// dlltest1.cpp: 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "dlltest1.h"

void TestClass::Hi() {
	std::cout << "hello" << std::endl;
}

double TestClass::Num(double num) {
	return num;
}

 
