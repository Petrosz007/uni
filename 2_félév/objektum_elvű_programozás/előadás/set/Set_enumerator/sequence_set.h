//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    class of the representation of sets with a sequence

#pragma once

#include "setrepr.h"
#include <vector>

//Set including integers is represented with a sequence
//Constructor allocates an an empty sequence
//There is an embedded class of Enumerator
class SequenceSet : public SetRepr {
public:
    SequenceSet (){ _seq.clear(); }
    void setEmpty()     override;
    void insert(int e)  override;
    void remove(int e)  override;
    int  select() const override;
    bool empty()  const override;
    bool in(int e)const override;

    class SequenceSetEnor : public Enumerator{
    public:
        SequenceSetEnor(SequenceSet *h): _s(h) { ++(_s->_enumeratorCount); }
        ~SequenceSetEnor(){ --(_s->_enumeratorCount); }
        void first()       override { _ind = 0; }
        void next()        override { ++_ind;}
        bool end()   const override { return _ind ==_s->_seq.size();}
        int current()const override { return _s->_seq[_ind]; }
    private:
        SequenceSet  *_s;
        unsigned int  _ind;
    };
    Enumerator* createEnumerator() override{
        return new SequenceSetEnor(this);
    }
private:
    std::vector<int> _seq;
    bool search(int e, unsigned int &ind) const;
};
