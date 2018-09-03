// Dlltest.cpp: 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "Dlltest.h"
#include<iostream>

void CoolingTower::Hi() {
	std::cout << "Hello " << std::endl;
}

int CoolingTower::Test() {
	return data;
}

void CoolingTower::Release() {
	delete this;
}

CoolingTower::~CoolingTower() {
	std::cout << "Release OK" << std::endl;
}
