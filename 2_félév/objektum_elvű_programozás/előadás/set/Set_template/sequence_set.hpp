//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    class of the representation of sets with a sequence

#pragma once

#include "setrepr.hpp"
#include <vector>

//Set is represented with a sequence
//Constructor allocates an an empty sequence
//There is an embedded class of Enumerator
template <typename Item>
class SequenceSetEnum;

template <typename Item>
class SequenceSet : public SetRepr<Item> {
public:
    SequenceSet(): SetRepr<Item>() {}
    void setEmpty() override;
    void insert(const Item &e) override;
    void remove(const Item &e) override;
    Item  select() const override;
    bool empty()  const override;
    bool in(const Item &e)const override;

    class SequenceSetEnor : public Enumerator<Item> {
    public:
        SequenceSetEnor(SequenceSet<Item> *h): _s(h) { ++(_s->_enumeratorCount); }

        ~SequenceSetEnor(){ --(_s->_enumeratorCount); }

        void first() override { _ind = 0; }
        void next() override  { ++_ind;   }
        bool end() const override { return _ind ==_s->_seq.size(); }
        Item current() const override { return _s->_seq[_ind]; }
    private:
        SequenceSet<Item>  *_s;
        unsigned int  _ind;
    };

    Enumerator<Item>* createEnumerator() override {
        return new SequenceSetEnor(this);
    }
private:
    std::vector<Item> _seq;
    bool search(const Item &e, unsigned int &ind) const;
};

//Task: 	implementation of removing all elements from the set
//Input:    SequenceSet this    -  representation of the set
//Output:   SequenceSet this    -  representation of the set
//Activity: It clears the sequence that represents the set.
template <typename Item>
void SequenceSet<Item>::setEmpty() {
    _seq.clear();
}

//Task: 	implementation of inserting an element into the set
//Input:    SequenceSet this   -  representation of the set
//          Item e             -  element that must be inserted
//Output:   SequenceSet this   -  representation of the set
//Activity: If the element e is not in the this->_seq, this->_seq must be concatenated with the element e.
template <typename Item>
void SequenceSet<Item>::insert(const Item &e) {
    unsigned int ind;
    if(!search(e,ind)) _seq.push_back(e);
}

//Task: 	implementation of inserting an element into the set
//Input:    SequenceSet this    -  representation of the set
//          Item e              -  element that must be removed
//Output:   SequenceSet this    -  representation of the set
//Activity: If the element e is in the this->_seq, this element must be deleted out from this->_seq.
template <typename Item>
void SequenceSet<Item>::remove(const Item &e) {
    unsigned int ind;
    if(search(e,ind)) {
        _seq[ind] = _seq[_seq.size()-1];
        _seq.pop_back();
    }
}

//Task: 	implementation of checking empty state of the set
//Input:    SequenceSet this    -  representation of the set
//Output:   SequenceSet this    -  representation of the set
//          bool                -  answer
//Activity: It gives 'true' if this->_seq.size() is zero
template <typename Item>
bool SequenceSet<Item>::empty() const {
    return _seq.size()==0;
}

//Task: 	implementation of inserting an element into the set
//Input:    SequenceSet this    -  representation of the set
//Output:   SequenceSet this    -  representation of the set
//          Item e              -  element that is selected
//Activity: It checks whether the size of the set is not zero.
//          It looks for the index e of this->_vec where the value is 'true'
template <typename Item>
Item SequenceSet<Item>::select() const {
    if(_seq.size()==0) throw new EmptySetException;
    return _seq[0];
}

//Task: 	implementation of checking that an element is in the set
//Input:    SequenceSet this    -  representation of the set
//Output:   SequenceSet this    -  representation of the set
//          bool                -  answer
//Activity: it looks for the index of the element e in this->_seq
template <typename Item>
bool SequenceSet<Item>::in(const Item &e) const {
    unsigned int ind;
    return search(e,ind);
}

//Task: 	implementation of searching for an element in the sequence
//Input:    SequenceSet this    -  representation of the set
//          Item e              -  the element that is searched for
//Output:   SequenceSet this    -  representation of the set
//          bool l              -  answer
//          unsigned int ind    -  index of the searched element in the sequence
//Activity: linear searching
template <typename Item>
bool SequenceSet<Item>::search(const Item &e, unsigned int &ind) const {
    bool l = false;
    for(unsigned int i=0; !l && i<_seq.size(); ++i ) {
        l = _seq[i]==e;
        ind = i;
    }
    return l;
}
