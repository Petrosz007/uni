//Athor:    Gregorics Tibor
//Date:     2017.12.15.
//Title:    implementation of classes of different shapes

#include "shapes.h"

// implementation of class of abstract shapes
int Shape::_piece = 0;

Shape::Shape(double size)
{
    _size = size;
    ++_piece;
}

Shape::~Shape()
{
    --_piece;
}

// implementation of class of abstract regular shapes
int Regular::_piece = 0;

Regular::Regular(double size) : Shape(size)
{
    ++_piece;
}

Regular::~Regular()
{
    --_piece;
}

double Regular::volume() const
{
    return _size * _size * _size * multiplier();
}

// implementation of class of cubes
int Cube::_piece = 0;

Cube::Cube(double size) : Regular(size)
{
    ++_piece;
}

Cube::~Cube()
{
    --_piece;
}

// implementation of class of spheres
int Sphere::_piece = 0;

Sphere::Sphere(double size) : Regular(size)
{
    ++_piece;
}

Sphere::~Sphere()
{
    --_piece;
}

// implementation of class of tetrahedrons
int Tetrahedron::_piece = 0;

Tetrahedron::Tetrahedron(double size) : Regular(size)
{
    ++_piece;
}

Tetrahedron::~Tetrahedron()
{
    --_piece;
}

// implementation of class of octahedron
int Octahedron::_piece = 0;

Octahedron::Octahedron(double size) : Regular(size)
{
    ++_piece;
}

Octahedron::~Octahedron()
{
    --_piece;
}

// implementation of class of abstract prismatic
int Prismatic::_piece = 0;

Prismatic::Prismatic(double size, double height): Shape(size)
{
    _height = height; ++_piece;
}

Prismatic::~Prismatic ()
{
    --_piece;
}

double Prismatic::volume() const
{
    return basis() * _height;
}

// implementation of class of cylinders
int Cylinder::_piece = 0;

Cylinder::Cylinder(double size, double height) : Prismatic(size, height)
{
    ++_piece;
}

Cylinder::~Cylinder()
{
    --_piece;
}

double Cylinder::basis() const
{
    return 3.14159 * _size * _size;
}

// implementation of class of squarePrisms
int SquarePrism::_piece = 0;

SquarePrism::SquarePrism (double size, double height) : Prismatic(size, height)
{
    ++_piece;
}

SquarePrism::~SquarePrism ()
{
    --_piece;
}

double SquarePrism::basis() const
{
    return _size * _size;
}

// implementation of class of triangularPrisms
int TriangularPrism::_piece = 0;

TriangularPrism::TriangularPrism (double size, double height) : Prismatic(size, height)
{
    ++_piece;
}

TriangularPrism::~TriangularPrism ()
{
    --_piece;
}

double TriangularPrism::basis() const
{
    return 1.73205 * _size * _size / 4.0;
}

// implementation of class of abstract conicals
int Conical::_piece = 0;

Conical::Conical(double size, double height) : Prismatic(size, height)
{
    ++_piece;
}

Conical::~Conical()
{
    --_piece;
}

double Conical::volume() const
{
    return (basis() * _height) / 3.0;
}

// implementation of class of cones
int Cone::_piece = 0;

Cone::Cone(double size, double height) : Conical(size, height)
{
    ++_piece;
}

Cone::~Cone()
{
    --_piece;
}

double Cone::basis() const
{
    return 3.14159 * _size * _size;
}

// implementation of class of squarePyramids
int SquarePyramid::_piece = 0;

SquarePyramid::SquarePyramid(double size, double height) : Conical(size, height)
{
    ++_piece;
}

SquarePyramid::~SquarePyramid()
{
    --_piece;
}

double SquarePyramid::basis() const
{
    return _size * _size;
}
