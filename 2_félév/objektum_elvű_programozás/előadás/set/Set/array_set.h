//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    class of the representation of sets with an array

#pragma once

#include "setrepr.h"
#include <vector>

//Set including natural numbers from a given interval is represented with an array
//Constructor allocates an array that is indexed by 0..n
class ArraySet : public SetRepr{
public:
    ArraySet (int n): _vect(n+1), _size(0){
        setEmpty();
    }
    void setEmpty()     override;
    void insert(int e)  override;
    void remove(int e)  override;
    int  select() const override;
    bool empty()  const override;
    bool in(int e)const override;
private:
    std::vector<bool> _vect;
    int _size;
};
