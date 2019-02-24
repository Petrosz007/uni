//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    class of bag

#pragma once
#include <vector>

//Datatype of bags including integers between 0 and m
class Bag{
public:
    enum Errors{EmptyBag, WrongInput};

    Bag(int m);
    void erase();
    void putIn(int e);
    int  mostFrequented() const;
protected:
    std::vector<int> _vec;
    int _elem;
//Type invariant: _vec[_elem] = max(_vec)
};


