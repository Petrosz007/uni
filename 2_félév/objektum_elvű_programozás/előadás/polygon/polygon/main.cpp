//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    Moving polygons and computing their center (type-oriented version)

#include <iostream>
#include <fstream>
#include <vector>
#include "polygon.h"

using namespace std;

//Activity:  Creating polygons based on a textfile,
//           moving them and then computing their center,
int main()
{
    string fn = "input.txt";
    ifstream inp(fn.c_str());
    if(inp.fail())
    {
        cout << "Wrong file name\n";
        return 1;
    }

    unsigned int n;
    inp >> n;
    vector<Polygon*> t(n);
    for(unsigned int i=0; i<n; ++i) t[i] = Polygon::create(inp);

    for ( Polygon* p : t ){
        p->write();
        p->move(Point(20,20));
        p->write();
        Point sp = p->center();
        cout << "(" << sp._x << "," << sp._y << ")\n";
    }

    for ( Polygon* p : t ) delete p;

    return 0;
}
