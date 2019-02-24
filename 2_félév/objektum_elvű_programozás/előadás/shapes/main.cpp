//Athor:    Gregorics Tibor
//Date:     2017.12.15.
//Title:    volume of different shapes (with design patterns)

#include <iostream>
#include <fstream>
#include <vector>
#include "shapes.h"

using namespace std;

Shape* create(ifstream &inp);
void statistic();

//Activity:  Creating shapes and computing their volume
int main()
{
    ifstream inp("shapes.txt");
    if(inp.fail()) { cout << "Wrong file name!\n";  return 1; }

    int shape_number;
    inp >> shape_number;
    vector<Shape*> shapes(shape_number);

    for ( int i = 0; i < shape_number; ++i ){
        string type;
        shapes[i] = create(inp);
    }
    inp.close();

    for ( Shape *p : shapes ){
        cout << p->volume() << endl;
    }

    statistic();

    for ( Shape *p : shapes ) delete p;

    statistic();
}

//Task: 	creating a shape based on a textfile
//Input:    ifstream inp   -  textfile
//Output:   Shape*         -  shape
//Activity: constructing a shape and setting its properties
Shape* create(ifstream &inp)
{
    Shape *p;
    string type;
    inp >> type;
    int size, height;
    inp >> size;
    if ( type == "Cube" ){
        p = new Cube(size);
    }
    else if ( type == "Sphere" ){
        p = new Sphere(size);
    }
    else if ( type == "Tetrahedron" ){
        p = new Tetrahedron(size);
    }
    else if ( type == "Octahedron" ){
        p = new Octahedron(size);
    }
    else if ( type == "Cylinder" ){
        inp >> height;
        p = new Cylinder(size, height);
    }
    else if ( type == "SquarePrism" ){
        inp >> height;
        p = new SquarePrism(size, height);
    }
    else if ( type == "TriangularPrism" ){
        inp >> height;
        p = new TriangularPrism(size, height);
    }
    else if ( type == "Cone" ){
        inp >> height;
        p = new Cone(size, height);
    }
    else if ( type == "SquarePyramid" ){
        inp >> height;
        p = new SquarePyramid(size, height);
    }
    else{
        cout << "Unknown shape" << endl;
    }
    return p;
}

//Activity: constructing a shape and setting its properties
void statistic(){
    cout << Shape::piece()       << " " << Regular::piece()      << " "
         << Prismatic::piece()   << " " << Conical::piece()      << " "
         << Sphere::piece()      << " " << Cube::piece()         << " "
         << Tetrahedron::piece() << " " << Octahedron::piece()   << " "
         << Cylinder::piece()    << " " << SquarePrism::piece()  << " "
         << TriangularPrism::piece() << " "
         << Cone::piece()        << " " << SquarePyramid::piece()<< endl;
}
