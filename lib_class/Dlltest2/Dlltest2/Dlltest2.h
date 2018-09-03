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

#ifdef DLL_EXPORTS  //���ǲ�����Ҫ���⵼������Ķ��壬���ⲿ�������ʱ��Ҳ����Ҫ��������Ķ��塣
class ExportClass : public ExportInterface
{
pirvate:
	std::string x; //�����ⲿ����Դ˲��ɼ����˴���std::string�ǰ�ȫ�ġ�
public:
	void foo(); //��������dllExample.cpp��ʵ��
};
#endif