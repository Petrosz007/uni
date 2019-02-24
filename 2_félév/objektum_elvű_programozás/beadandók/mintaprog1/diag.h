//Author:    Gregorics Tibor
//Date:      2017.08.08.
//Title:     Diagonal matrix

#pragma once
#include <iostream>

//Class of diagonal matrices
//Methods: add, multiply, write, read, refer to an element
//Representation: only the elements of the diagonal
class Diag {
public:
    enum Exceptions{OVERINDEXED, NULLPART, DIFFERENT};

    Diag(){ _n = 0; _v = NULL; }
    Diag(int k) { _v = NULL; reSize(k); }
    ~Diag(){if (_n!=0) delete[] _v; }
    Diag(const Diag& a);
    Diag& operator=(const Diag& a);

    void reSize(int k) { _n = k; if(_v!=NULL) delete[] _v; _v = new int[_n]; }

    int operator()(int i, int j) const;
    int& operator()(int i, int j);

    friend Diag operator+ (const Diag& a, const Diag& b);
    friend Diag operator* (const Diag& a, const Diag& b);
    friend std::istream& operator>> (std::istream& s, Diag& a);
    friend std::ostream& operator<< (std::ostream& s, const Diag& a);
private:
    int* _v;
    int  _n;
};
