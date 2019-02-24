#include "lcdnumber.h"

void LcdNumber::display(int seconds)
{
	std::cout << extend((seconds % 3600) / 60) + ":" + extend((seconds % 3600) % 60) << std::endl;
}

std::string LcdNumber::extend(int n) const
{
    std::ostringstream os;
    os << n;
    return (n < 10 ? "0" : "") + os.str();
}
