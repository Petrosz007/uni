//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    class of the representation of sets with an array

#pragma once

#include "setrepr.h"
#include <vector>

//Set including natural numbers from a given interval is represented with an array
//Constructor allocates an array that is indexed by 0..n
//There is an embedded class of Enumerator
class ArraySet : public SetRepr {
public:
    ArraySet (int n): SetRepr(), _vect(n+1), _size(0) {
        setEmpty();
    }
    void setEmpty() override;
    void insert(int e) override;
    void remove(int e) override;
    int  select() const override;
    bool empty()  const override;
    bool in(int e)const override;

    class ArraySetEnor : public Enumerator{
    public:
        ArraySetEnor(ArraySet *h): _s(h) { ++(_s->_enumeratorCount); }
        ~ArraySetEnor(){ --(_s->_enumeratorCount); }
        void first() override { _ind = -1; next(); }
        void next()  override { for(++_ind;_ind<_s->_vect.size() && !_s->_vect[_ind]; ++_ind);}
        bool end()     const override {return _ind==_s->_vect.size();}
        int  current() const override {return _ind; }
    private:
        ArraySet  *_s;
        unsigned int _ind;
    };
    Enumerator* createEnumerator() override{
        return new ArraySetEnor(this);
    }
private:
    std::vector<int> _vect;
    int _size;
};
