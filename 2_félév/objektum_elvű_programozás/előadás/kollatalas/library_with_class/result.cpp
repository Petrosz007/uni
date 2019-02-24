//Author:   Gregorics Tibor
//Date:     2014.04.01.
//Title:    Selecting the books which number of copies is zero and the books which publishing year is earlier than 2000

#include "result.h"
#include <iostream>
#include <iomanip>
#include <cstdlib>

using namespace std;

//Task: 	Opening a stock file
//Input:    string fname - name of text file of the selected books
//Output:   ofstream f   - text file of the selected books
//Activity: It opens a textk file as a sequential output file
Result::Result(std::string fname)
{
    f.open(fname.c_str());
    if(f.fail()) throw FILEERROR;
}

//Task:     Writing a book into a sequential file
//Input:    ofstream y  - text file of the selected books
//Output:   ofstream y  - text file of the selected books
//          Book dy     - the next book
//Activity:	It writes some data of a book into a sequential file
void Result::write(const Book &dy)
{
    f << setw(4)  << dy.id     << ' '
      << setw(14) << dy.author << ' '
      << setw(19) << dy.title  << endl;
}

