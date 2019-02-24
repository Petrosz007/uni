//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    abstract template class of sets

#pragma once

#include "enumerator.h"
#include "setrepr.hpp"
#include "array_set.hpp"
#include "sequence_set.hpp"

//General type of sets without its representation
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
template <typename Item>
class Set
{
public:
    Set(int n = 0){ _repr = createSetRep(n); }

    ~Set(){
        if(_repr->getEnumCount() != 0) throw UnderTraversalException();
        delete _repr;
    }
    void setEmpty()     {
        if(_repr->getEnumCount()!=0) throw UnderTraversalException();
        _repr->setEmpty();
    }
    void insert(const Item &e)  {
        if(_repr->getEnumCount()!=0) throw UnderTraversalException();
        _repr->insert(e);
    }
    void remove(const Item &e)  {
        if(_repr->getEnumCount()!=0) throw UnderTraversalException();
        _repr->remove(e);
    }
    Item select() const { return _repr->select(); }
    bool empty()  const { return _repr->empty(); }
    bool in(const Item &e)const { return _repr->in(e); }

    Enumerator<int>* createEnumerator() { return _repr->createEnumerator(); }
private:
    SetRepr<Item> *_repr;

    Set(const Set& other);
    Set& operator=(const Set& other) ;
    static SetRepr<Item>* createSetRep(int n){ return new SequenceSet<Item>; }
};

template<>
inline SetRepr<int>* Set<int>::createSetRep(int n) {
    if (n > 0) return new ArraySet(n);
    else       return new SequenceSet<int>;
}

/*
template<typename Item>
SetRepr<Item>* Set<Item>::createSetRep(int n) {
    return new SequenceSet<Item>;
}

template<>
SetRepr<int>* Set<int>::createSetRep(int n) {
    if (n > 0) return new ArraySet(n);
    else       return new SequenceSet<int>;
}
*/
