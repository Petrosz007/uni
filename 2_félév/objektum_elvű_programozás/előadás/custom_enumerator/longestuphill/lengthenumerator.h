//Author:    Gregorics Tibor
//Date:     2015.02.22.
//Title:    The longest uphill part of a trip

#pragma once

#include "bitenumerator.h"

//Class of the enumerators that show the length of the uphill parts of the trip
class LengthEnumerator{
public:
    LengthEnumerator(const std::string &fname) : _b(fname){}
    void first() { _b.first(); next(); }
    int current()  const { return _length; }
    bool end()     const { return _end; }
    void next();
private:
    BitEnumerator _b;
    int _length;
    bool _end;
};


