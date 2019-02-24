//Author:   Gregorics Tibor
//Date:     2014.04.01.
//Title:    Selecting the books which number of copies is zero and the books which publishing year is earlier than 2000

#include <fstream>
#include <string>
#include "stock.h"
#include "result.h"
#include "common.h"

//Activity: Selecting the books which number of copies is zero and the books which publishing year is earlier than 2000
//          out form the register of a library
int main()
{
    Stock  x("input.txt");
    Result y("output1.txt");
    Result z("output2.txt");

    Book dx;
    Status sx;
    while(x.read(dx,sx)){
        if (0==dx.nc)       y.write(dx);
        if (dx.year<"2000") z.write(dx);}
    return 0;
}




