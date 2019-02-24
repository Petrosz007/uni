#include "petrol.h"

#include <iostream>
#include <sstream>

PetrolStation::PetrolStation(int n, int m) : _cash(this, m)
{
    _pumps.resize(n);
    for(int i = 0; i<n; ++i){
        _pumps[i] = new Pump();
    }
}

PetrolStation::~PetrolStation(){
    for( Pump *p : _pumps ) delete p;
}

bool PetrolStation::driveIn(Car* pa, unsigned int i)
{
    if( i>=_pumps.size() ) return false;
    _pumps[i]->standNextTo(pa);
    return true;
}

bool PetrolStation::tank(Car* pa, int l)
{
    unsigned int i;
    if( !search(pa, i) ) return false;
    _pumps[i]->fill(pa, l);
    return true;
}

void PetrolStation::goToCash(Car* pa){
    _cash.goIn(pa);
}

int PetrolStation::pay(Car* pa){
    unsigned int i;
    if( !search(pa, i) ) return 0;
    return _cash.pay(i, pa);
}

bool PetrolStation::driveOff(Car* pa)
{
    unsigned int i;
    if( !search(pa, i) ) return false;
    _pumps[i]->leave();
    return true;
}

bool PetrolStation::search(Car* pa, unsigned int &ind) const
{
    bool l = false;
    for(unsigned int i = 0; !l && i<_pumps.size(); ++i)
    {
        ind = i;
        l = _pumps[i]->getCurrent() == pa;
    }
    return l;
}
