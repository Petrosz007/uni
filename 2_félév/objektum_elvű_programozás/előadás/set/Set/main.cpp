//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    Trying the class of sets

#include <iostream>
#include <exception>
#include "set.h"

using namespace std;

int main()
{
    Set h(15);
    try{
        cout << h.select() << endl;
    } catch(exception &er){
        cout << "empty -- " <<er.what() << endl;
    }

    try{
        h.insert(23);
    } catch(exception &er){
        cout << "illegal -- " << er.what() << endl;
    }

    h.insert(1);
    h.insert(2);
    h.insert(3);
    h.insert(3);

    return 0;
}
