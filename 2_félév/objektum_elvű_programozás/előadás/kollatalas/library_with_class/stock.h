//Author:   Gregorics Tibor
//Date:     2014.04.01.
//Title:    Selecting the books which number of copies is zero and the books which publishing year is earlier than 2000

#pragma once

#include "common.h"
#include <fstream>
#include <string>

enum Status {abnorm,norm};

// Class of the registers of books of a library
class Stock{
public:
    enum Errors{FILEERROR};
    Stock(std::string fname);
    bool read(Book &dx, Status &sx);
private:
    std::ifstream f;
};

