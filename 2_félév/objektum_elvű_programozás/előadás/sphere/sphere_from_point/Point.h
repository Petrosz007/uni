#pragma once
#include <cmath>

class Point
{
    public:
        Point():_x(0.0),_y(0.0),_z(0.0){}
        Point(double x, double y, double z):_x(x),_y(y),_z(z){}
        void set(double x, double y, double z){
            _x = x; _y = y; _z = z;
        }

        double distance(const Point &p) const {
            return sqrt(pow(_x-p._x, 2) + pow(_y-p._y,2) + pow(_z-p._z,2)) - radius() - p.radius() ;
        }

        virtual double radius() const { return 0.0;}

    protected:
        double _x;
        double _y;
        double _z;
};
