//Author:    Gregorics Tibor
//Date:      2017.08.08.
//Title:     Diagonal matrix

#include "diag.h"
#include <iostream>
#include <iomanip>

using namespace std;

//Task: 	copy constructor
//Input:    Diag a    - matrix
//Output:   Diag this - default matrix
//Activity: creates the array of the diagonal and fills in its elements based on matrix a
Diag::Diag(const Diag& a)
{
    _n = a._n;
    _v = new int[_n];
    for(int i=0; i<_n; ++i) _v[i] = a._v[i];
}

//Task: 	assignment operator
//Input:    Diag a    - matrix
//Output:   Diag this - default matrix
//Activity: fills in the elements of the diagonal based on matrix a
Diag& Diag::operator=(const Diag& a)
{
    if(_n!=a._n) throw DIFFERENT;

    if(this==&a) return *this;
    for(int i=0; i<_n; ++i) _v[i] = a._v[i];
    return *this;
}

//Task: 	adding
//Input:    Diag a    - matrix
//          Diag b    - matrix
//Output:   Diag      - result matrix
//Activity: adds the elements of the diagonals of the matrices
Diag operator+(const Diag& a ,const Diag& b)
{
    if(a._n!=b._n) throw Diag::DIFFERENT;

    Diag c(a._n);

    for(int i=0; i<c._n; ++i)  c._v[i] = a._v[i] + b._v[i];
    return c;
}

//Task: 	multiplying
//Input:    Diag a    - matrix
//          Diag b    - matrix
//Output:   Diag      - result matrix
//Activity: multiplies the elements of the diagonals of the matrices
Diag operator*(const Diag& a ,const Diag& b)
{
    if(a._n!=b._n) throw Diag::DIFFERENT;

    Diag c(a._n);

    for(int i=0; i<c._n; ++i) c._v[i] = a._v[i] * b._v[i];
    return c;
}

//Task: 	writing
//Input:    ostream s - target of writing
//          Diag a    - matrix
//Output:   ostream s - target of writing
//Activity: writes the elements of the matrix
ostream& operator<<(ostream& s, const Diag& a)
{
    for(int i=0; i<a._n; ++i){
        for(int j=0; j<a._n; ++j)
            s << setw(5) << a(i,j);
        s << endl;
    }
    return s;
}

//Task: 	reading
//Input:    istream s - source of writing
//          Diag a    - matrix
//Output:   istream s - source of writing
//Activity: reads the elements of the diagonal of the matrix
istream& operator>>(istream& s, Diag& a)
{
    for(int i=0; i<a._n; ++i) {
 //       cout << "[" << i << "," << i << "]=";
        s >> a(i,i);
    }
    return s;
}

//Task: 	getting
//Input:    int i,j - indexes of element
//Output:   int     - the element of the matrix in ith row and jth column
//Activity: gets the given elements of the diagonal matrix
int Diag::operator()(int i, int j) const
{
    if ((i>=_n || i<0 ) || (j>=_n || j<0 )) throw OVERINDEXED;
    if (i!=j) return 0;
    return _v[i];
}

//Task: 	setting
//Input:    int i,j - indexes of element
//Output:   int     - the element of the matrix in ith row and jth column
//Activity: gives a reference to the given elements of the diagonal matrix
int& Diag::operator()(int i, int j)
{
    if ((i>=_n || i<0 ) || (j>=_n || j<0 )) throw OVERINDEXED;
    if (i!=j) throw NULLPART;
    return _v[i];
}
