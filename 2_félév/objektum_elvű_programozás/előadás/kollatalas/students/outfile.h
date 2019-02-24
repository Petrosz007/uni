//Author:   Gregorics Tibor
//Date:     2017.08.08.
//Title:    The type of sequential output file including final mark of students

#pragma once

#include <fstream>
#include <string>
#include <iomanip>

struct Evaluation {
    std::string name;
    std::string neptun;
    int mark;
    Evaluation(std::string str1, std::string str2, int j) :  name(str1), neptun(str2), mark(j) {}
};

// Class of sequential output files
class OutFile{
public:
    enum Errors{FILE_OPEN_ERROR};
    OutFile(std::string fname){
        f.open(fname.c_str());
        if(f.fail()) throw FILE_OPEN_ERROR;
    }
    void write(const Evaluation &dy) {
        f << dy.name << " " << dy.neptun << std::setw(3) << dy.mark << std::endl;
    }
private:
    std::ofstream f;
};

