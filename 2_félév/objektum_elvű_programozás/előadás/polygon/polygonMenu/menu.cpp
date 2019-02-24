//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    implementation of menu class

#include "menu.h"
#include "read.hpp"
#include <iostream>
#include <fstream>
#include <cstdlib>

using namespace std;

bool check(int n) { return 0<=n && n<=4; }

//Task: 	running the menu
//Input:    Menu this
//Output:   Menu this
//Activity: selecting a menu item that manipulate one polygon
void Menu::run()
{
    int v = 0;
    do{
        menuWrite();
        v = read<int>("Choose a menu point: ", "An integer between 0 and 4 is needed.", check);
        switch(v){
            case 1: case1(); break;
            case 2: case2(); break;
            case 3: case3(); break;
            case 4: case3(); break;
        }
    }while(v != 0);
}

//Task: 	writing the menu items
//Input:    Menu this
//Output:   Menu this
//Activity: writing the menu items
void Menu::menuWrite()
{
    cout << "0 - exit\n";
    cout << "1 - create\n";
    cout << "2 - write\n";
    cout << "3 - move\n";
    cout << "4 - center\n";
}

//Task: 	creating a polygon
//Input:    Menu this
//Output:   Menu this
//Activity: creating the current polygon based on a textfile
void Menu::case1()
{
    if(s!=nullptr) delete s;
    cout << "file name: ";
    string fn; cin>> fn;
    ifstream inp(fn.c_str());
    if(inp.fail()) {
        cout << "Wrong file name!\n";
        exit(1);
    }
    s = Polygon::create(inp);
}

//Task: 	writing a polygon
//Input:    Menu this
//Output:   Menu this
//Activity: writing the current polygon
void Menu::case2(){
    if(s==nullptr) cout << "There is no polygon!\n";
    else s->write();
}

//Task: 	moving a polygon
//Input:    Menu this
//Output:   Menu this
//Activity: moving the current polygon
void Menu::case3(){
    if(s==nullptr) cout << "There is no polygon!\n";
    else s->move(Point(20,20));
}

//Task: 	computing the center of a polygon
//Input:    Menu this
//Output:   Menu this
//Activity: computing the center of the current polygon
void Menu::case4(){
    if(s==nullptr)
        cout << "There is no polygon!\n";
    else {
        Point sp = s->center();
        cout << "(" << sp._x << "," << sp._y << ")\n";
    }
}
