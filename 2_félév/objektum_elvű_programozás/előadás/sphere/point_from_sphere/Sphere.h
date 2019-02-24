#pragma once
#include <cmath>

class Sphere
{
    public:
        enum Errors { NEGATIVE_RADIUS };
        Sphere(){}
        Sphere(double x, double y, double z, double r):_x(x),_y(y),_z(z){
            if( r<0) throw NEGATIVE_RADIUS;
            _radius = r;
        }

        double distance(const Sphere &s)const {
            return sqrt(pow(_x-s._x,2) + pow(_y-s._y,2) + pow(_z-s._z,2)) - _radius - s._radius;
        }

        bool contains(const Sphere &s) const {
            return distance(s) + 2*s._radius <= 0;
        }

    protected:
        double _x;
        double _y;
        double _z;
    private:
        double _radius;
};
