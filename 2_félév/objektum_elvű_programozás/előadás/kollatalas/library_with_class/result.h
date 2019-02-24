//Author:   Gregorics Tibor
//Date:     2014.04.01.
//Title:    Selecting the books which number of copies is zero and the books which publishing year is earlier than 2000

#pragma once

#include "common.h"
#include <fstream>
#include <string>

// Class of sequences of selected books
class Result{
public:
    enum Errors{FILEERROR};
    Result(std::string fname);
    void write(const Book &dx);
private:
    std::ofstream f;
};

