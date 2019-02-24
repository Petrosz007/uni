//Author:    Gregorics Tibor
//Date:     2015.02.22.
//Title:    The longest uphill part of a trip

#include <iostream>
#include "lengthenumerator.h"

using namespace std;

//Task: 	Computing the longest uphill part of a trip
//Input:    ifstream f - sequence of altitudes of the trip
//Output:   int        - length of the longest uphill part of the trip
//Activity:	Selecting the longest uphill part of the trip (maximum selection)
//          based on the enumerator that presents the length of the uphill parts
int main()
{
    LengthEnumerator t("input.txt");
    t.first();
    int max = t.current();
    for( t.next(); !t.end(); t.next() ){
        if( max < t.current() ) max = t.current();
    }
    cout << "The length of the longest uphill part: " << max << endl;
    return 0;
}

