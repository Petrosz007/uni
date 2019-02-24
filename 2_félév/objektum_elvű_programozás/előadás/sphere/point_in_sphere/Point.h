#pragma once
#include <cmath>

class Point {
private:
    double _x, _y, _z;
public:
    Point():_x(0.0), _y(0.0), _z(0.0) {}
    Point(double a, double b, double c) : _x(a),_y(b),_z(c) {}
    void set(double x, double y, double z){_x = x; _y = y; _z = z;}

    double distance(const Point &p) const {
        return sqrt(pow(_x-p._x,2) + pow(_y-p._y,2) + pow(_z-p._z,2));
    }
};

