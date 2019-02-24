//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    class of polygons

#pragma once
#include <vector>
#include <fstream>
#include "point.h"

//Datatype of polygons
class Polygon
{
public:
    enum Errors{FEW_VERTICES, INDEX_OVERLOADING};
    Polygon(int n) : _vertices(n)  {
        if (n < 3) throw FEW_VERTICES;
    }

    unsigned int sides() const { return _vertices.size(); }

    void  move(const Point &p);
    Point center() const;

    void write() const;

    static Polygon* create(std::ifstream &inp);

private:
    std::vector<Point> _vertices;
};

