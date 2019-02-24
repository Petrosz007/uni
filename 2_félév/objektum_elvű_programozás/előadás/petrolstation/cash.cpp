#include "cash.h"
#include "petrol.h"
#include "pump.h"

#include <iostream>
#include <sstream>

typedef std::chrono::milliseconds milliseconds;

void Cash::goIn(Car* pa)
{
    std::unique_lock<std::mutex> lock(_mu);
    if( _engaged<_capacity ) ++_engaged;
    else {
        _cashQueue.push(pa);
        // várakozik:
        std::ostringstream os;
        os << pa->getName() << " is waiting for cash. " << std::endl;
        std::cout << os.str();
        // writeQueue(pa);
        while( _cashQueue.front()!=pa ) {
            _cond.wait(lock);
        }
    }
}

int Cash::pay(unsigned int i, Car* pa)
{
    int amount = compute(i);
    if(_cashQueue.empty()) --_engaged;
    else _cashQueue.pop();
    _cond.notify_all();

    /*
        //várakozás: mintha a fizetés 1 ms lenne
        std::mutex _mu1;
        std::condition_variable _cond1;
        std::unique_lock<std::mutex> lock1(_mu1);
        _cond1.wait_for(lock1, milliseconds(2000));
    */
    return amount;
}

int Cash::compute(int i) const
{
    int amount = _p->getQuantity(i) * _p->getUnit();
    _p->resetQuantity(i);
    return amount;
}

void Cash::writeQueue(Car* pa)
{
    std::ostringstream os;
    os << "free desks: " << _capacity << ". Queue of the cash: ( ";
    Car* pb = _cashQueue.front();
    os << pb->getName() << " "; _cashQueue.pop(); _cashQueue.push(pb);
    Car* p  = _cashQueue.front();
    while( !_cashQueue.empty() && p != pb ) {
        _cashQueue.pop(); _cashQueue.push(p); os << p->getName() << " ";
        p  = _cashQueue.front();
    }
    os << ")" << std::endl;
    std::cout << os.str();
}
