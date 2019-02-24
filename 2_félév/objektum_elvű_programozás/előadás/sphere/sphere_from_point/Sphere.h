#pragma once
#include "Point.h"

#include <iostream>

class Sphere : public Point
{
    public:
        enum Errors { NEGATIVE_RADIUS };
        Sphere(double x, double y, double z, double r) : Point(x,y,z){
            if( r<0) throw NEGATIVE_RADIUS;
            _radius = r;
        }
        bool contains(const Point &p) const {
            return distance(p) + 2*p.radius() <= 0;
        }

        double radius() const override { return _radius;}

    protected:
        double _radius;
};

