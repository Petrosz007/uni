//Athor:    Gregorics Tibor
//Date:     2017.12.15.
//Title:    implementation of classes of different areas

#include "area.h"

// implementation of class of squareArea
SquareArea* SquareArea::_instance = nullptr;

SquareArea* SquareArea::instance()
{
    if ( _instance == nullptr ) _instance = new SquareArea();
    return _instance;
}

// implementation of class of circleArea
CircleArea* CircleArea::_instance = nullptr;

CircleArea* CircleArea::instance()
{
    if ( _instance == nullptr ) _instance = new CircleArea();
    return _instance;
}

// implementation of class of triangularAre
TriangularArea* TriangularArea::_instance = nullptr;

TriangularArea* TriangularArea::instance()
{
    if ( _instance == nullptr ) _instance = new TriangularArea();
    return _instance;
}
