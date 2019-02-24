//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    implementation of the class of the representation of sets with an array

#include "array_set.h"

//Task: 	implementation of removing all elements from the set
//Input:    ArraySet this   -  representation of the set
//Output:   ArraySet this   -  representation of the set
//Activity: It fills the array, that represents the set, with 'false' and set the _size to zero.
void ArraySet::setEmpty()
{
    for(int e=0; e<int(_vect.size()); ++e) _vect[e] = false;
    _size = 0;
}

//Task: 	implementation of inserting an element into the set
//Input:    ArraySet this   -  representation of the set
//          int e           -  element that must be inserted
//Output:   ArraySet this   -  representation of the set
//Activity: It checks the element e being valid and than it set this->_vect[e] to 'true'.
//          If this->_vect[e] was 'true', this->_size must be increased.
void ArraySet::insert(int e)
{
    if(e<0 || e>int(_vect.size())-1) throw IllegalElementException(e);
    if(!_vect[e]){
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
void ArraySet::remove(int e)
{
    if(e<0 || e>int(_vect.size())-1) throw IllegalElementException(e);
    if(_vect[e]){
        _vect[e] = false;
        --_size;
    }
}

//Task: 	implementation of checking empty state of the set
//Input:    ArraySet this   -  representation of the set
//Output:   ArraySet this   -  representation of the set
//          bool            -  answer
//Activity: It gives 'true' if this->_size is zero
bool ArraySet::empty() const
{
    return _size==0;
}

//Task: 	implementation of selecting an element from the set
//Input:    ArraySet this   -  representation of the set
//Output:   ArraySet this   -  representation of the set
//          int e           -  element that is selected
//Activity: It checks whether the size of the set is not zero.
//          It looks for the index e of this->_vec where the value is 'true'
int ArraySet::select() const
{
    if(_size==0) throw EmptySetException();
    int e;
    for(e=0; !_vect[e]; ++e);
    return e;
}

//Task: 	implementation of checking that an element is in the set
//Input:    ArraySet this   -  representation of the set
//Output:   ArraySet this   -  representation of the set
//          bool            -  answer
//Activity: It checks the element e being valid and than gives back this->_vect[e]
bool ArraySet::in(int e) const
{
    if(e<0 || e>int(_vect.size())-1) throw IllegalElementException(e);
    return _vect[e];
}
