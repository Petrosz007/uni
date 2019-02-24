//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    abstract class of sets

#pragma once

#include "enumerator.h"
#include "setrepr.h"
#include "array_set.h"
#include "sequence_set.h"

//General type of sets of integers without its representation
//Constructor selects a representation (dependency injection) based on its parameter
//All method checks whether there is an enumerator on the set
//Bridge design pattern
//Methods:
//setEmpty()        - all elements are removed from the set
//insert()          - a given integer is put in the set
//remove()          - a given integer is removed from the set
//select()          - selects an element from a non-empty set
//empty()           - checks whether the set is empty
//in()              - checks whether a given integer is in the set
//createEnumerator()- create a new enumerator on the set
class Set
{
public:
    Set(int n = 0) {
        if (n>0) _repr = new ArraySet(n);
        else     _repr = new SequenceSet;
    }
    ~Set()  {
        if(_repr->getEnumCount()!=0) throw UnderTraversalException();
        delete _repr;
    }

    void setEmpty()     {
        if(_repr->getEnumCount()!=0) throw UnderTraversalException();
        _repr->setEmpty();
    }
    void insert(const int &e)  {
        if(_repr->getEnumCount()!=0) throw UnderTraversalException();
        _repr->insert(e);
    }
    void remove(const int &e)  {
        if(_repr->getEnumCount()!=0) throw UnderTraversalException();
        _repr->remove(e);
    }
    int select() const { return _repr->select(); }
    bool empty()  const { return _repr->empty(); }
    bool in(const int &e)const { return _repr->in(e); }

    Enumerator* createEnumerator() { return _repr->createEnumerator(); }

private:
    SetRepr *_repr;

    Set(const Set& h);
    Set& operator=(const Set& h);
};

