#include "car.h"
#include "petrol.h"
#include <iostream>
#include <sstream>
#include <thread>

void Car::process(PetrolStation* p, unsigned int i, int l)
{
    std::ostringstream os1; os1 << _name << " starts to fuel\n"; std::cout << os1.str();

    if( !p->driveIn(this, i) ) return;
    if( !p->tank(this, l) ) return;
    std::ostringstream os2; os2 << _name << " is fueling\n"; std::cout << os2.str();

    p->goToCash(this);
    std::ostringstream os3; os3 << _name << " has paid: " << p->pay(this) << " Ft" << std::endl; std::cout << os3.str();

    p->driveOff(this);
    std::ostringstream os4; os4 << _name << " has left\n"; std::cout << os4.str();
}
