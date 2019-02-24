#include <iostream>
#include <vector>
#include "Sphere.h"
#include "Point.h"

using namespace std;

int main()
{
    cout << "Give me the coordinates of the central point and the radius of the sphere!" << endl;
    double x,y,z,r;
    cout << "x= "; cin >> x;
    cout << "y= "; cin >> y;
    cout << "z= "; cin >> z;
    cout << "r= "; cin >> r;

    Point c(x,y,z);
    Sphere s(&c,r);

    int n;
    cout << "How many points are there? "; cin >> n;
    vector<Point> v(n);

    cout << "Give me the coordinates of the points!" << endl;
    for(int i=0; i<n; ++i){
        cout << i+1 << ". points:" << endl;
        cout << "x= "; cin >> x;
        cout << "y= "; cin >> y;
        cout << "z= "; cin >> z;
        v[i].set(x,y,z);
    }

    int db = 0;
    for(int i=0; i<n; ++i){
        if(s.contains(v[i])) ++db;
    }

    cout << "The number of the points being in the sphere: " << db << endl;

    return 0;
}
