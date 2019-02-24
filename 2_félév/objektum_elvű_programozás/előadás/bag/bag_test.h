//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    test class of bag

#pragma once
#include <vector>
#include "bag.h"
class Bag_Test : public Bag{
public:
    Bag_Test(int m) : Bag(m) {}
    const std::vector<int>& getArray() const  {return _vec;}
};

