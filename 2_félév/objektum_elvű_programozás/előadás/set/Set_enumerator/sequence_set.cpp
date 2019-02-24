//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    implementation of the class of the representation of sets with a sequence

#include "sequence_set.h"

//Task: 	implementation of removing all elements from the set
//Input:    SequenceSet this    -  representation of the set
//Output:   SequenceSet this    -  representation of the set
//Activity: It clears the sequence that represents the set.
void SequenceSet::setEmpty()
{
    _seq.clear();
}

//Task: 	implementation of inserting an element into the set
//Input:    SequenceSet this   -  representation of the set
//          int e           -  element that must be inserted
//Output:   SequenceSet this   -  representation of the set
//Activity: If the element e is not in the this->_seq, this->_seq must be concatenated with the element e.
void SequenceSet::insert(int e)
{
    unsigned int ind;
    if(!search(e,ind)) _seq.push_back(e);
}

//Task: 	implementation of inserting an element into the set
//Input:    SequenceSet this    -  representation of the set
//          int e               -  element that must be removed
//Output:   SequenceSet this    -  representation of the set
//Activity: If the element e is in the this->_seq, this element must be deleted out from this->_seq.
void SequenceSet::remove(int e)
{
    unsigned int ind;
    if(search(e,ind)){
        _seq[ind] = _seq[_seq.size()-1];
        _seq.pop_back();
    }
}

//Task: 	implementation of checking empty state of the set
//Input:    SequenceSet this    -  representation of the set
//Output:   SequenceSet this    -  representation of the set
//          bool                -  answer
//Activity: It gives 'true' if this->_seq.size() is zero
bool SequenceSet::empty() const
{
    return _seq.size()==0;
}

//Task: 	implementation of inserting an element into the set
//Input:    SequenceSet this    -  representation of the set
//Output:   SequenceSet this    -  representation of the set
//          int e               -  element that is selected
//Activity: It checks whether the size of the set is not zero.
//          It looks for the index e of this->_vec where the value is 'true'
int SequenceSet::select() const
{
    if(_seq.size()==0) throw EmptySetException();
    return _seq[0];
}

//Task: 	implementation of checking that an element is in the set
//Input:    SequenceSet this    -  representation of the set
//Output:   SequenceSet this    -  representation of the set
//          bool                -  answer
//Activity: it looks for the index of the element e in this->_seq
bool SequenceSet::in(int e) const
{
    unsigned int ind;
    return search(e,ind);
}

//Task: 	implementation of searching for an integer in the sequence
//Input:    SequenceSet this    -  representation of the set
//          int e               -  the element that is searched for
//Output:   SequenceSet this    -  representation of the set
//          bool l              -  answer
//          unsigned int ind    -  index of the searched element in the sequence
//Activity: linear searching
bool SequenceSet::search(int e, unsigned int &ind) const
{
    bool l = false;
    for(unsigned int i=0; !l && i<_seq.size(); ++i ){
        l = _seq[i]==e; ind = i;
    }
    return l;
}
