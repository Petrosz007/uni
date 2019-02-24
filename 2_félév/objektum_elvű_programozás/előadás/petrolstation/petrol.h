#pragma once

#include "pump.h"
#include "cash.h"
#include "car.h"

#include <vector>

class PetrolStation
{
public:
    PetrolStation(int n, int m);
    ~PetrolStation();
    bool driveIn(Car* pa, unsigned int i);
    bool tank(Car* pa, int l);
    void goToCash(Car* pa);
    int  pay(Car* pa);
    bool driveOff(Car* pa);

    void resetQuantity(unsigned int i) { _pumps[i]->resetQuantity(); }
    int getQuantity(unsigned int i) const { return _pumps[i]->getQuantity(); }
    int getUnit() const { return _unit; }
private:
    std::vector<Pump*> _pumps;
    Cash _cash;
    const int _unit = 400;

    bool search(Car* pa, unsigned int &ind) const;
};
