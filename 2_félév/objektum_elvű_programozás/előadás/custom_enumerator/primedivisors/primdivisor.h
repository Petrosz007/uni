//Author:    Gregorics Tibor
//Date:     2015.02.22.
//Title:    The sum of the positive prime divisors of a natural number

#pragma once

// Class of the enumerators that presents the positiv prime divisors of a given natural number
class PrimDivisor{
public:
    PrimDivisor(int i);
    void first();
    void next();
    int current() const { return d;}
    bool end()    const { return n==1;}
private:
    int n, d;
};

