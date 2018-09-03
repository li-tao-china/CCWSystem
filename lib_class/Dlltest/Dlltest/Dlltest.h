#pragma once
#include "dllmain.h"
class CoolingTower : public Tower
{
public:
	virtual void Hi();
	virtual int Test();
	virtual void Release();
	~CoolingTower();
private:
	int data;
};
