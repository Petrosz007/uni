#pragma once

#include <iostream>
#include <queue>
#include <mutex>
#include <condition_variable>

class Car;

class Pump
{
public:
    Pump() : _quantity(0) { }

    void standNextTo(Car *pa) ;
    void fill(Car *pa, int l);
    void leave() ;

    Car* getCurrent() const { return _queue.front(); }
    int getQuantity() const { return _quantity; }
    void resetQuantity() { _quantity = 0; }

private:
    int _quantity;
    std::queue<Car*> _queue;

    std::mutex _mu;
	std::condition_variable _cond;
};
