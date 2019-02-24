//Author:   Gregorics Tibor
//Date:     2014.04.01.
//Title:    Selecting the books which number of copies is zero and the books which publishing year is earlier than 2000

#include "stock.h"
#include <iostream>
#include <cstdlib>

using namespace std;

//Task: 	Opening a stock file
//Input:    string fname - name of text file of the books
//Output:   ifstream f   - text file of the books
//Activity: It opens a stock file as an sequential input file
Stock::Stock(std::string fname)
{
    f.open(fname.c_str());
    if(f.fail()) throw FILEERROR;
}

//Task:     Getting the next book from the stock file (text file) of a library
//Input:    ifstream x  - text file of the books of the stock file
//Output:   ifstream x  - text file of the books of the stock file
//          Book dx     - the next book
//          Status sx   - state of the reading
//Activity: It reads a new line out from the text file. If the file is empty, the status of reading (sx) must be set to abnorm.
//          If it can read a line, then highlights the data of the book that is stored in this line,
//          and the status of reading (sx) must be set to norm.
bool Stock::read(Book &dx, Status &sx)
{
    string line;
    getline(f, line);
    if (!f.fail()) {
        sx = norm;
        dx.id         = atoi(line.substr( 0, 4).c_str());
        dx.author     =      line.substr( 5,14);
        dx.title      =      line.substr(21,19);
        dx.publisher  =      line.substr(42,14);
        dx.year       =      line.substr(58, 4);
        dx.nc         = atoi(line.substr(63, 3).c_str());
        dx.isbn       =      line.substr(67,14);
    }
    else sx=abnorm;

    return norm==sx;
}



