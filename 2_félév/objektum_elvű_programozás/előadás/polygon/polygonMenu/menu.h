//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    menu class

#pragma once

#include "polygon.h"
#include <fstream>

// Menu class: creating and moving one polygon and computing its center
class Menu{
public:
    Menu() { s = nullptr;}
    void run();
    ~Menu(){ if(s!=nullptr) delete s;}
private:
    Polygon* s;

    void menuWrite();
    void case1();
    void case2();
    void case3();
    void case4();
};
