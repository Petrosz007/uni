//Author:    Gregorics Tibor
//Date:     2015.02.22.
//Title:    Selecting in an infinit matrix

#include <iostream>
#include "read.hpp"

using namespace std;

double f(int i) { return i*i;}
double g(int j) { return j*j;}

bool all(double r){ return true; }

//Task: 	There is given f,g : N -> R and a real number e. Find indexes i, j where f(i)+g(j)==e.
//Input:    double e - the number that is searched
//Output:   int i, j - the indexes where f(i)+g(j)==e
//Activity:	Reading the real number that is searched,
//          enumerating the index pairs i, j ,
//		    selecting the first pair where f(i)+g(j)==e
int main()
{
    double e = read<double>("Give a real number: ", "This is not a real number!", all);

    int i, j;
    i = j = 0;
    while( f(i)+g(j)!=e ){
        if(i>0) { --i; ++j; }
        else    { i = j+1; j = 0; }
    }

    cout << "The given number is equal to the sum f(" << i << ")+g("
         << j << ")\n";

    return 0;
}


