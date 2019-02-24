//Author:    Gregorics Tibor
//Date:     2015.02.22.
//Title:    The sum of the positive prime divisors of a natural number

#include <iostream>
#include "primdivisor.h"
#include "read.hpp"

using namespace std;

bool pos(int n){ return n>0; }

//Task: 	Computing the longest uphill part of a trip
//Input:    int n   - natural number
//Output:   int s   - the sum of the prime divisors of the natural number
//Activity:	Reading the natural number
//          and computing the sum of its positive prime divisors
//          and writing the result
int main()
{
    int n = read<int>("Give a positive integer: ",
				 "This is not a positive integer!", pos);
    PrimDivisor t(n);

    int s = 0;
    for(t.first(); !t.end(); t.next()){
        s = s + t.current();
    }

    cout << "The sum of the prime divisors: " << s << endl;
}

