//Athor:    Gregorics Tibor
//Date:     2017.12.15.
//Title:    classes of different shapes

#pragma once

#include "area.h"

// class of abstract shapes
class Shape
{
public:
    virtual ~Shape();
    virtual double volume() const = 0;
    static int piece() { return _piece; }
protected:
    Shape(double size);
    double _size;
private:
    static int _piece;
};

// class of abstract regular shapes
class Regular : public Shape{
public:
    ~Regular();
    double volume() const override;
    static int piece() { return _piece; }
protected:
    Regular(double size);
    virtual double multiplier() const = 0;
private:
    static int _piece;
};

// class of cubes
class Cube : public Regular
{
public:
    Cube(double size);
    ~Cube();
    static int piece() { return _piece; }
protected:
    double multiplier() const override { return 1.0; }
private:
    static int _piece;
};

// class of spheres
class Sphere : public Regular
{
public:
    Sphere(double size);
    ~Sphere();
    static int piece() { return _piece; }
protected:
    double multiplier() const override { return _multiplier; }
private:
    constexpr static double _multiplier = (4.0 * 3.14159) / 3.0;
    static int _piece;
};

// class of tetrahedrons
class Tetrahedron : public Regular
{
public:
    Tetrahedron(double size);
    ~Tetrahedron();
    static int piece() { return _piece; }
protected:
    double multiplier() const override { return _multiplier; }
private:
    constexpr static double _multiplier = 1.41421 / 12.0;
    static int _piece;
};

// class of octahedron
class Octahedron : public Regular
{
public:
    Octahedron(double size);
    ~Octahedron();
    static int piece() { return _piece; }
protected:
    double multiplier() const override { return _multiplier; }
private:
    constexpr static double _multiplier = 1.41421 / 3.0;
    static int _piece;
};

// class of abstract prismatic
class Prismatic : public Shape{
public:
    ~Prismatic ();
    double volume() const override;
    static int piece() { return _piece; }
protected:
    Prismatic(double size, double height);
    virtual double basis() const = 0;
    double _height;
private:
    static int _piece;
};

// class of cylinders
class Cylinder : public Prismatic
{
public:
    Cylinder(double size, double height);
    ~Cylinder();
    static int piece() { return _piece; }
protected:
    double basis() const override;
private:
    static int _piece;
};

// class of squarePrisms
class SquarePrism : public Prismatic
{
public:
    SquarePrism (double size, double height);
    ~SquarePrism ();
    static int piece() { return _piece; }
protected:
    double basis() const override;
private:
    static int _piece;
};

// class of triangularPrisms
class TriangularPrism : public Prismatic
{
public:
    TriangularPrism (double size, double height);
    ~TriangularPrism ();
    static int piece() { return _piece; }
protected:
    double basis() const override;
private:
    static int _piece;
};

// class of abstract conicals
class Conical : public Prismatic
{
public:
    ~Conical();
    double volume() const override;
    static int piece() { return _piece; }
protected:
    Conical(double size, double height);
private:
    static int _piece;
};

// class of cones
class Cone : public Conical
{
public:
    Cone(double size, double height);
    ~Cone();
    static int piece() { return _piece; }
protected:
    double basis() const override;
private:
    static int _piece;
};

// class of squarePyramids
class SquarePyramid : public Conical
{
public:
    SquarePyramid(double size, double height);
    ~SquarePyramid();
    static int piece() { return _piece; }
protected:
    double basis() const override;
private:
    static int _piece;
};
