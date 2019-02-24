#pragma once

#include "Sphere.h"

class Point : public Sphere{
    public:
        Point():Sphere(0.0,0.0,0.0,0.0){}
        Point(double x, double y, double z):Sphere(x,y,z,0.0){}
        void set(double x, double y, double z){_x = x; _y = y; _z = z;}
};
