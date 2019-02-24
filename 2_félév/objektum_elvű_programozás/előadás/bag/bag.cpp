//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    class of bag

#include "bag.h"

//Task: 	creating of empty bag
//Input:    int m      -  the upper limit of the natural numbers of the bag
//Output:   Bag this   -  the bag
//Activity: memory allocation of the array representing the bag and erasing the bag
Bag::Bag(int m) : _vec(m+1,0){
   _elem = 0;
}

//Task: 	erasing the bag
//Input:    Bag this   -  the bag
//Output:   Bag this   -  the bag
//Activity: sets to zero the elements of the array representing the bag
//          and maintains the type invariant
void Bag::erase() {
    for(unsigned int i=0; i<_vec.size(); ++i) _vec[i] = 0;
    _elem = 0;
}

//Task: 	putting an element into the bag
//Input:    Bag this   -  the bag
//          int e      -  the element being put in
//Output:   Bag this   -  the bag
//Activity: increases the occurrence frequency of e in the array representing the bag
//          and maintains the type invariant
void Bag::putIn(int e) {
    if( e<0 || e>= int(_vec.size()) ) throw WrongInput;
    if( ++_vec[e] > _vec[_elem] ) _elem = e;
}

//Task: 	asking the most frequented element of the bag
//Input:    Bag this       -  the bag
//Output:   int _maxelem   -  most frequented element of the bag
//Activity: giving _elem if _vec[_elem]>0 (bag is not empty)
int Bag::mostFrequented() const {
    if( 0 ==_vec[_elem] ) throw EmptyBag;
    return _elem;
}

