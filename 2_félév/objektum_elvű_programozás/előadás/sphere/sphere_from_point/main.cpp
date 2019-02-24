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
     Sphere s(x,y,z,r);

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

    int c = 0;
    for(int i=0; i<n; ++i){
        if(s.contains(v[i])) ++c;
    }

    cout << "The number of the points being in the sphere: " << c << endl;

    Sphere s1(0,0,0,5), s2(2,0,0,2); Point p1(1,0,0), p2(3,0,0);
    cout << s1.distance(s2) << endl;
    cout << s1.distance(p2) << endl;
    cout << p1.distance(s2) << endl;
    cout << p1.distance(p2) << endl;

    return 0;
}
