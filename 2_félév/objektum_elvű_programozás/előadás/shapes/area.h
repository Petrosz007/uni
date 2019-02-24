//Athor:    Gregorics Tibor
//Date:     2017.12.15.
//Title:    classes of different areas

#pragma once

// class of abstract Area
class Area
{
public:
    virtual double area(double m) const = 0;
};

// class of SquareArea
class SquareArea : public Area
{
public:
    double area(double m) const override
    {
        return m * m;
    }
    static SquareArea *instance();
private:
    static SquareArea *_instance;
    SquareArea () {}
};

// class of CircleArea
class CircleArea : public Area
{
public:
    double area(double m) const override
    {
        return 3.14159 * m * m;
    }
    static CircleArea *instance();
private:
    static CircleArea *_instance;
    CircleArea () {}
};

// class of TriangularArea
class TriangularArea : public Area
{
public:
    double area(double m) const override
    {
        return 1.73205 * m * m / 4.0;
    }
    static TriangularArea *instance();
private:
    static TriangularArea *_instance;
    TriangularArea() {}
};
