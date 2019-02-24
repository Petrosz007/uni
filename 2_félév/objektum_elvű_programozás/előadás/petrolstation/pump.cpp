#include "pump.h"

#include "car.h"
#include <sstream>

void Pump::standNextTo(Car* pa)
{
    std::unique_lock<std::mutex> lock(_mu);
    _queue.push(pa);
    // várakozik:
    while( pa !=_queue.front() ){
        _cond.wait(lock);
    }
    std::ostringstream os;
    os << pa->getName() << " is ready to fuel. " << std::endl;
    std::cout << os.str();
}

void Pump::fill(Car* pa, int l)
{
    std::unique_lock<std::mutex> lock(_mu);
    _quantity = l;
}

void Pump::leave()
{
    _queue.pop();
    _cond.notify_all();
}
