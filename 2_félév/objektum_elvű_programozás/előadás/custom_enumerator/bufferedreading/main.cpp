//Author:   Gregorics Tibor
//Date:     2015.02.22.
//Title:    Rate of the uphill part of a trip

#include <iostream>
#include <fstream>
#include <cstdlib>

using namespace std;

//Task: 	Computing the uphill part of a trip
//Input:    ifstream f - sequence of altitudes of the trip
//Output:   double     - rate of the uphill steps
//Activity:	Counting the uphill steps and all steps of the trip based on a textfile
//          and writing the quotient of them
int main()
{
    ifstream f("input.txt");
    if(f.fail()){
        cout << "Wrong file name!\n";
        exit(1);
    }

    int first, second;
    int c = 0; int d = 0;
    for( f >> first >> second; !f.fail(); first = second, f >> second){
        if( first < second ) ++c;
        ++d;
    }

    cout.setf(ios::fixed);
    cout.precision(2);
    cout << "Rate of the rising part of the trip: "
         << (100.0*c)/d << "%" << endl;

    return 0;
}
