//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    implementation of application class

#include "application.h"
#include <iostream>
#include <cstdlib>

using namespace std;

//Task: 	creating application with polygons
//Input:    textfile with data of polygons
//Output:   Application this   -  application
//Activity: creating an instance of Application
Application::Application(){
    cout << "file name: "; string fn; cin>> fn;
    ifstream inp(fn.c_str());
    if(inp.fail()) {
        cout << "Wrong file name!\n"; exit(1);
    }
    unsigned int n; inp >> n;
    t.resize(n);
    for(unsigned int i=0; i<n; ++i)
        t[i] = Polygon::create(inp);
}

//Task: 	destroying the application
//Input:    Application this   -  application
//Output:   -
//Activity: destroying the instance of Application
Application::~Application()
{
    for ( Polygon* p : t ) delete p;
}

//Task: 	running the application
//Input:    Application this   -  application
//Output:   Application this   -  application
//Activity: moving the polygons of the application and computing their center
//          and writing the results
void Application::run(){
    for ( Polygon* p : t ){
        p->move(Point(20,20)); p->write();
        Point sp = p->center();
        cout << "(" << sp._x << ","
                    << sp._y << ")\n";
    }
}

