#pragma once
#include "Point.h"

class Sphere{
private:
    Point  _centre;
    double _radius;
public:
    enum Errors{ILLEGAL_RADIUS};
    Sphere(const Point &c, double r): _centre(c), _radius(r) {
        if (_radius<0) throw ILLEGAL_RADIUS;
    }
    double contains(const Point &p) const {
        return _centre.distance(p) <= _radius;
    }
};

