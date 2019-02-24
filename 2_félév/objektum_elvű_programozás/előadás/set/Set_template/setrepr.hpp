//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    abstract class of representation of sets

#pragma once

#include <exception>
#include <sstream>
#include "enumerator.h"

class EmptySetException : public std::exception {
public:
    const char* what() const noexcept override { return "Empty Set";}
};

class IllegalElementException : public std::exception{
private:
    int _e;
public:
    IllegalElementException(int e): _e(e) {}
    const char* what() const noexcept override {
        std::ostringstream os;
        os <<  "Illegal element: " << _e;
        std::string str = os.str();
        char * msg = new char[str.size() + 1];
        std::copy(str.begin(), str.end(), msg);
        msg[str.size()] = '\0';
        return msg;
    }
};

class UnderTraversalException : public std::exception
{
public:
    const char* what() const noexcept override { return "Under traversal";}
};


//General type of the representation of sets
//Bridge design pattern
//Methods:
//setEmpty()        - all elements are removed from the set
//insert()          - a given integer is put in the set
//remove()          - a given integer is removed from the set
//select()          - selects an element from a non-empty set
//empty()           - checks whether the set is empty
//in()              - checks whether a given integer is in the set
//createEnumerator()- create a new enumerator on the set
//getEnumCount()    - gives back the number of the enumerators on the set
template <typename Item>
class SetRepr
{
protected:
    int _enumeratorCount;
public:

    SetRepr() : _enumeratorCount(0) { }
    virtual ~SetRepr(){};

    virtual void setEmpty() 	= 0;
    virtual void insert(const Item &e)  = 0;
    virtual void remove(const Item &e)  = 0;
    virtual Item  select() const = 0;
    virtual bool empty()  const = 0;
    virtual bool in(const Item &e)const = 0;

    virtual int maxInt() const { return 0; }

    int getEnumCount() const { return _enumeratorCount; }

    virtual Enumerator<Item>* createEnumerator() = 0;

};

