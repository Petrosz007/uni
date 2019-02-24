//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    class of points

#pragma once

//Datatype of points on the plan
class Point
{
public:
    Point(int x = 0, int y = 0): _x(x), _y(y) { }

    void setPoint(int x, int y) { _x = x; _y = y; }

    Point move(const Point &p) const
    { return Point(_x + p._x, _y + p._y ); }
    Point operator+(const Point &p) const
    { return Point(_x + p._x, _y + p._y);}
    Point operator/(int f) const
    { return Point(_x / f, _y / f ); }
public:
    int _x, _y;
};
