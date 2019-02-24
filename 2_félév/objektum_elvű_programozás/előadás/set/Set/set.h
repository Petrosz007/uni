//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    abstract class of sets

#pragma once

#include "setrepr.h"
#include "array_set.h"
#include "sequence_set.h"

//General type of sets of integers without its representation
//Constructor selects a representation (dependency injection) based on its parameter
//Bridge design pattern
//Methods:
//setEmpty()   - all elements are removed from the set
//insert()     - a given integer is put in the set
//remove()     - a given integer is removed from the set
//select()     - selects an element from a non-empty set
//empty()      - checks whether the set is empty
//in()         - checks whether a given integer is in the set
class Set {
public:
    Set(int n = 0) {
        if (n>0) _repr = new ArraySet(n);
        else     _repr = new SequenceSet;
    }
    ~Set() { delete _repr; }

    void setEmpty()     { _repr->setEmpty(); }
    void insert(int e)  { _repr->insert(e); }
    void remove(int e)  { _repr->remove(e); }
    int  select() const { return _repr->select(); }
    bool empty()  const { return _repr->empty(); }
    bool in(int e)const { return _repr->in(e); }
private:
    SetRepr *_repr;

    Set(const Set& h);
    Set& operator=(const Set& h);
};
