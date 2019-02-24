//Author:   Gregorics Tibor
//Date:     2014.04.01.
//Title:    Selecting even numbers

#include <iostream>
#include <fstream>

using namespace std;

//Activity: Selecting even numbers from a sequence of integers in a text file
int main()
{
    ifstream x;
    bool error = true;
    do{
        string fname;
        cout << "file name: ";
        cin >> fname;
        x.open(fname.c_str());
        if( (error=x.fail()) ) {
            cout << "Wrong file name!\n";
            x.clear();
        }
    }while(error);

    cout << "Selected even numbers: ";
    int e;
    while(x >> e) {
        if(0==e%2) cout << e << " ";
    }
    return 0;
}

