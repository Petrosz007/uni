//Author:   Gregorics Tibor
//Date:     2017.08.08.
//Title:    The type of sequential input file including results of students

#include <fstream>
#include <string>
#include <vector>

struct Student {
    std::string name;
    std::string neptun;
    std::string pm;
    std::vector<int> result;
};

enum Status {abnorm, norm};

// Class of sequential input files
class InpFile{
public:
    enum Errors{FILE_OPEN_ERROR};
    InpFile(std::string fname){
        f.open(fname.c_str());
        if(f.fail()) throw FILE_OPEN_ERROR;
    }
    bool read( Student &dx, Status &sx);
private:
    std::ifstream f;
};

