#ifndef LCDNUMBER_H
#define LCDNUMBER_H

#include <iostream>
#include <string>
#include <sstream>

class LcdNumber
{
public:
    void display(int seconds);
private:
	std::string extend(int n) const;
};

#endif // LCDNUMBER_H
