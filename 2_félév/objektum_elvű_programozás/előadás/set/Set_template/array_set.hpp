//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    class of the representation of sets with an array

#pragma once

#include "setrepr.hpp"
#include <vector>

//Set including natural numbers from a given interval is represented with an array
//Constructor allocates an array that is indexed by 0..n
//There is an embedded class of Enumerator
class ArraySet : public SetRepr<int> {
public:
    ArraySet (int n): SetRepr<int>(), _vect(n+1), _size(0) {
        setEmpty();
    }
    void setEmpty() override;
    void insert(const int &e) override;
    void remove(const int &e) override;
    int  select() const override;
    bool empty()  const override;
    bool in(const int &e)const override;

    int maxInt() const override { return _vect.size()-1; }

    class ArraySetEnor : public Enumerator<int> {
    public:
        ArraySetEnor(ArraySet *h): _s(h) { ++(_s->_enumeratorCount); }
        ~ArraySetEnor(){ --(_s->_enumeratorCount); }

        void first() override {
            _cur = -1;
            next();
        }
        void next() override {
            for(++_cur; _cur<(int)_s->_vect.size() && !_s->_vect[_cur]; ++_cur);
        }
        bool end()const override      {
            return _cur==(int)_s->_vect.size();
        }
        int  current() const override {
            return _cur;
        }
    private:
        ArraySet  *_s;
        int         _cur;
    };
    Enumerator<int>* createEnumerator() override {
        return new ArraySetEnor(this);
    }
private:
    std::vector<int> _vect;
    int _size;
};

//Task: 	implementation of removing all elements from the set
//Input:    ArraySet this   -  representation of the set
//Output:   ArraySet this   -  representation of the set
//Activity: It fills the array, that represents the set, with 'false' and set the _size to zero.
void ArraySet::setEmpty() {
    for(int e=0; e<int(_vect.size()); ++e) _vect[e] = false;
    _size = 0;
}

//Task: 	implementation of inserting an element into the set
//Input:    ArraySet this   -  representation of the set
//          int e           -  element that must be inserted
//Output:   ArraySet this   -  representation of the set
//Activity: It checks the element e being valid and than it set this->_vect[e] to 'true'.
//          If this->_vect[e] was 'true', this->_size must be increased.
void ArraySet::insert(const int &e) {
    if(e<0 || e>int(_vect.size())-1) throw IllegalElementException(e);
    if(!_vect[e]) {
        _vect[e] = true;
        ++_size;
    }
}

//Task: 	implementation of removing an element from the set
//Input:    ArraySet this   -  representation of the set
//          int e           -  element that must be removed
//Output:   ArraySet this   -  representation of the set
//Activity: It checks the element e being valid and than it set this->_vect[e] to 'false'.
//          If this->_vect[e] was 'false', this->_size must be decreased.
void ArraySet::remove(const int &e) {
    if(e<0 || e>int(_vect.size())-1) throw IllegalElementException(e);
    if(_vect[e]) {
        _vect[e] = false;
        --_size;
    }
}

//Task: 	implementation of checking empty state of the set
//Input:    ArraySet this   -  representation of the set
//Output:   ArraySet this   -  representation of the set
//          bool            -  answer
//Activity: It gives 'true' if this->_size is zero
bool ArraySet::empty() const {
    return _size==0;
}

//Task: 	implementation of selecting an element from the set
//Input:    ArraySet this   -  representation of the set
//Output:   ArraySet this   -  representation of the set
//          int e           -  element that is selected
//Activity: It checks whether the size of the set is not zero.
//          It looks for the index e of this->_vec where the value is 'true'
int ArraySet::select() const {
    if(_size==0) throw  EmptySetException();
    int e;
    for(e=0; !_vect[e]; ++e);
    return e;
}

//Task: 	implementation of checking that an element is in the set
//Input:    ArraySet this   -  representation of the set
//Output:   ArraySet this   -  representation of the set
//          bool            -  answer
//Activity: It checks the element e being valid and than gives back this->_vect[e]
bool ArraySet::in(const int &e)    const {
    if(e<0 || e>int(_vect.size())-1) throw IllegalElementException(e);
    return _vect[e];
}
