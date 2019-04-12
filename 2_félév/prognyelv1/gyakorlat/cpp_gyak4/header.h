#ifndef HEADER_H
#define HEADER_H

#include <string>

class User{
    std::string _name;
    static int count;
    public:
    
    User(std::string name="") : _name(name){ ++count;}
    User(const User& other) : _name(other._name){ ++count;}    
    ~User(){--count;}
    std::string getName() const { return _name; }
    void setName(const std::string& name) { _name = name; }
    static int getObjectCounter(){return count;}
};
int User::count = 0;

#endif
