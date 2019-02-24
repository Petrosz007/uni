//Author:    Gregorics Tibor
//Date:     2015.02.22.
//Title:    The longest uphill part of a trip

#pragma once

#include <string>
#include <iostream>
#include <fstream>
#include <cstdlib>

// Class of the enumerators that show the uphill and downhill steps of the trip
class BitEnumerator{
public:
    enum Errors { FILEERROR };
    BitEnumerator(const std::string &fname){
    _f.open(fname);
    if(_f.fail()) throw FILEERROR;
    }
    void first() { _f >> _first >> _second; }
    void next()  { _first = _second; _f >> _second; }
    int current() const { return (_first < _second ? 1 : 0); }
    bool end()    const { return _f.fail(); }
private:
    std::ifstream _f;
    int _first, _second;
};


