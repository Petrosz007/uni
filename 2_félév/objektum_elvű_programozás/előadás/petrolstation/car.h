#pragma once

#include <string>
#include <thread>

class PetrolStation;

class Car
{
public:
    Car(){}
    Car(const std::string &str) : _name(str) {}
    ~Car() { _fuel.join(); }
    void setName(const std::string str) { _name = str; }
    std::string getName() const { return _name; }

    void refuel(PetrolStation* p, unsigned int i, int l) { _fuel = std::thread(process, this, p, i, l); }
private:
    std::string _name;
    std::thread _fuel;
    void process(PetrolStation* p, unsigned int i, int l);
};
