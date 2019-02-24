#pragma once

#include <queue>
#include <mutex>
#include <condition_variable>
#include <atomic>
//#include <atomic>

class PetrolStation;
class Car;

class Cash
{
public:
    Cash(PetrolStation* p, int m): _p(p), _engaged(0), _capacity(m) {}
    void goIn(Car* pa);
    int pay(unsigned int i, Car *pa);
private:
    PetrolStation * _p;
    std::atomic_int _engaged;
    int _capacity;
    std::queue<Car*> _cashQueue;

    std::mutex _mu;
	std::condition_variable _cond;

    int compute(int i) const;
	void writeQueue(Car* pa);
};
