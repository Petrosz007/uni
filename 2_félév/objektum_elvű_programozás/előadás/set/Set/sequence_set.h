//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    class of the representation of sets with a sequence

#pragma once

#include "setrepr.h"
#include <vector>

//Set including integers is represented with a sequence
//Constructor allocates an an empty sequence
class SequenceSet : public SetRepr{
public:
    SequenceSet (){ _seq.clear(); }
    void setEmpty()     override;
    void insert(int e)  override;
    void remove(int e)  override;
    int  select() const override;
    bool empty()  const override;
    bool in(int e)const override;
private:
    std::vector<int> _seq;
    bool search(int e, unsigned int &ind) const;
};
