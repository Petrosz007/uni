//Author:   Gregorics Tibor
//Date:     2018.09.23.
//Title:    Anglers

#pragma once

#include <fstream>
#include <sstream>
#include <string>

//Datatype of contests
struct Contest {
    std::string angler;
    std::string contest;
    int counter;
};

//Datatype of enumerator of contests
class ContestEnor{
    private:
        std::ifstream _f;
        Contest _cur;
        bool _end;
    public:
        ContestEnor(const std::string &str) { _f.open(str); };
        void first() {next();}
        void next();
        Contest current() const { return _cur;}
        bool end() const { return _end;}
};
