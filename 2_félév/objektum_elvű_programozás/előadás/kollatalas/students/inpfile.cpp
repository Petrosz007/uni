//Author:   Gregorics Tibor
//Date:     2017.08.08.
//Title:    implementation of the type of sequential input file including results of students

#include "inpfile.h"
#include <iostream>
#include <cstdlib>
#include <sstream>

using namespace std;

//Task:     Getting the results of the next student out from the input (text) file
//Input:    ifstream f  - input (text) file
//Output:   ifstream x  - input (text) file
//          Student dx  - the next student
//          Status sx   - state of the reading
//Activity: It reads a new line out from the text file. If the file is empty, the status of reading (sx) must be set to abnorm.
//          If it can read a line, then highlights the data of the students that is stored in this line,
//          and the status of reading (sx) must be set to norm.
bool InpFile::read(Student &dx, Status &sx)
{
    string line;
    getline(f, line);
    if (!f.fail() && line!="") {
        sx=norm;

        istringstream in(line);

        in >> dx.name;
        in >> dx.neptun;
        in >> dx.pm;
        while( !('+'== dx.pm[0] || '-'== dx.pm[0]) ){
            dx.name += " " + dx.neptun;
            dx.neptun = dx.pm;
            in >> dx.pm;
        }
        dx.result.clear();
        int mark;
        while( in >> mark ) dx.result.push_back(mark);
    } else sx=abnorm;
    return norm==sx;
}


