//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    application class

#pragma once

#include "polygon.h"
#include <vector>
#include <fstream>

// Application class: creating and moving polygons and computing their center
class Application
{
public:
    Application();
    ~Application();
    void run();
private:
    std::vector<Polygon*> t;
};
