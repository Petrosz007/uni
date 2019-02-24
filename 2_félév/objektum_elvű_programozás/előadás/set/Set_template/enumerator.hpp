//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    abstract class of enumerators

#pragma once

//General type of the enumerators
template <typename Item>
class Enumerator {
public:
    virtual void first() = 0;
    virtual void next() = 0;
    virtual bool end() const = 0;
    virtual Item current() const = 0;
    virtual ~Enumerator(){}
};

