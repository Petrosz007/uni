//Author:   Gregorics Tibor
//Date:     2017.08.08.
//Title:    Final mark of students

#include <iostream>
#include <string>
#include "inpfile.h"
#include "outfile.h"

using namespace std;

bool cond(const Student dx);
int  avr(const vector<int> x);

//Activity: Computing the final mark of the students based on their results recorded in a text file
//          and writing them in t another text file
int  main()
{
    try {
        InpFile x("input.txt");
        OutFile y("output.txt");
        Student dx;
        Status sx;
        while(x.read(dx,sx)) {
            if (cond(dx)) {
                Evaluation dy(dx.name, dx.neptun, avr(dx.result));
                y.write(dy);
            }
        }
    }catch(InpFile::Errors er) {
        if(er==InpFile::FILE_OPEN_ERROR) cout << "input file open error!\n";
    }catch(OutFile::Errors er) {
        if(er==OutFile::FILE_OPEN_ERROR) cout << "output file open error!\n";
    }
    return 0;
}

//Task:     Deciding whether a student can get final mark
//Input:    Student dx - student
//Output:   bool l     - result of the checking
//Activity: It checks whether all marks of the student are at least 2 (optimist linear searching)
//          and the number of plus greater than or equal to the number of minus, (two counting)
bool cond(const Student dx)
{
    bool l = true;
    for(unsigned int i=0; l && i<dx.result.size(); ++i){
        l=dx.result[i]>1;
    }
    int p, m; p = m = 0;
    for(unsigned int i = 0; i<dx.pm.size(); ++i){
        if(dx.pm[i]=='+') ++p;
        if(dx.pm[i]=='-') ++m;
    }
    return l && m<=p;
}

//Task:     Giving the final mark of a student
//Input:    vector<int> v - marks of the student
//Output:   int           - final mark of the student
//Activity: It computes the average of the marks of the student and rounds down it.
//          If the number of the marks is zero, the result is also zero.
int avr(const vector<int> v)
{
    double s = 0.0;
    for(unsigned int i = 0; i<v.size(); ++i){
        s += v[i];
    }
    return (0==v.size() ? 0 : s/v.size());
}

