//Author:   Gregorics Tibor
//Date:     2014.04.01.
//Title:    Selecting the books which number of copies is zero

#include <fstream>
#include <iostream>
#include <iomanip>
#include <string>
#include <cstdlib>

using namespace std;

struct Book{
    int id;
    string author;
    string title;
    string publisher;
    string year;
    int nc;
    string isbn;
};

enum Status{abnorm, norm};

bool read(ifstream &f, Book &dx, Status &sx);
void write(ofstream &f, const Book &dy);

//Activity: Selecting the books which number of copies is zero out form the register of a library
int main()
{
    ifstream x("inp.txt");
    if (x.fail() ) {
        cerr << "Nincs input fajl!\n";
        char ch; cin>>ch;
        exit(1);
    }

    ofstream y("out1.txt");
    if (y.fail() ) {
        cerr << "Nem lehet letrehozni az output fajlt!\n";
        char ch; cin>>ch;
        exit(1);
    }

    ofstream z("out2.txt");
    if (z.fail() ) {
        cerr << "Nem lehet letrehozni az output fajlt!\n";
        char ch; cin>>ch;
        exit(1);
    }

    Book dx;
    Status sx;
    while(read(x,dx,sx)) {
        if (0==dx.nc)    write(y,dx);
        if (dx.year<"2000") write(z,dx);
    }

    return 0;
}

//Task:     Getting the next book from the register (text file) of a library
//Input:    ifstream x  - text file of the books of the stock file
//Output:   ifstream x  - text file of the books of the stock file
//          Book dx     - the next book
//          Status sx   - state of the reading
//Activity: It reads a new line out from the text file. If the file is empty, the status of reading (sx) must be set to abnorm.
//          If it can read a line, then highlights the data of the book that is stored in this line,
//          and the status of reading (sx) must be set to norm.
bool read(ifstream &f, Book &dx, Status &sx){
    string line;
    getline(f,line);
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

//Task:     Writing a book into a sequential file
//Input:    ofstream f  - text file of the selected books
//Output:   ofstream f  - text file of the selected books
//          Book dy     - the next book
//Activity:	It writes some data of a book into a sequential file
void write(ofstream &f, const Book &dy){
    f << setw(4)  << dy.id     << ' '
      << setw(14) << dy.author << ' '
      << setw(19) << dy.title  << endl;
}
