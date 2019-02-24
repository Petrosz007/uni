//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    abstr
#pragma once

#include <exception>
#include <sstream>

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


// enum Errors{EMPTY_SET, ILLEGAL_ELEMENT};

//General type of the representation of sets of integers
//Bridge design pattern
//Methods:
//setEmpty()    - all elements are removed from the set
//insert()      - a given integer is put in the set
//remove()      - a given integer is removed from the set
//select()      - selects an element from a non-empty set
//empty()       - checks whether the set is empty
//in()          - checks whether a given integer is in the set
class SetRepr
{
public:
    virtual void setEmpty() 	= 0;
    virtual void insert(int e)  = 0;
    virtual void remove(int e)  = 0;
    virtual int  select() const = 0;
    virtual bool empty()  const = 0;
    virtual bool in(int e)const = 0;
    virtual ~SetRepr(){}
};
