#pragma once

#ifdef DLL_EXPORTS
#define DLL_API __declspec(dllexport)
#else
#define DLL_API __declspec(dllimport)
#endif

class DLL_API ExportInterface
{
public:
	virtual void foo() = 0;
};

extern "C" DLL_API ExportInterface*  getInstance();

#ifdef DLL_EXPORTS  //我们并不需要向外导出该类的定义，在外部代码编译时，也不需要包含此类的定义。
class ExportClass : public ExportInterface
{
pirvate:
	std::string x; //由于外部代码对此不可见，此处的std::string是安全的。
public:
	void foo(); //函数体在dllExample.cpp中实现
};
#endif