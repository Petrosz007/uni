//Author:   Gregorics Tibor
//Date:     2018.09.23.
//Title:    Anglers

#pragma once

#include "contest.h"
#include <string>

//Datatype of anglers
struct Angler {
    std::string id;
    bool skillful;
};

//Datatype of enumerator of anglers
class AnglerEnor{
    private:
        ContestEnor _tt;
        Angler _cur;
        bool _end;
    public:
        AnglerEnor(const std::string &str): _tt(str) { };
        void first() {_tt.first(); next();}
        void next();
        Angler current() const { return _cur;}
        bool end() const { return _end;}
};

