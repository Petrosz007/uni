//Author:    Gregorics Tibor
//Date:     2017.12.15.
//Title:    Trying the class of sets

#include "set.h"
#include <iostream>

using namespace std;

int main()
{
    Set h;

    // Beolvasás
    h.insert(1);
    h.insert(2);
    h.insert(3);
    h.insert(3);

    bool l = false;
    int e;
    Enumerator* enor1 = h.createEnumerator();
    for(enor1->first(); !l && !enor1->end(); enor1->next()) {
        e = enor1->current();
        int c = 0;
        Enumerator* enor2 = h.createEnumerator();
        for(enor2->first(); !enor2->end(); enor2->next()) {
            if(e > enor2->current()) ++c;
        }
        delete enor2;
        l = c>=3;
    }
    delete enor1;
    if (l) cout << "A keresett szam: " << e << endl;
    else cout << "Nincs keresett szam.\n";

    Set h1(15);
    try{
        h1.insert(23);
    } catch(exception &er){
        cout << "illegal -- " << er.what() << endl;
    }

    try{
        cout << h1.select() << endl;
    } catch(exception &er){
        cout << "empty -- " <<er.what() << endl;
    }

    Enumerator* enor = h1.createEnumerator();
    enor->first();
    try{
        h1.remove(5);
        delete enor;
    } catch(exception &er){
        cout << "under -- " <<er.what() << endl;
        delete enor;
    }

    return 0;
}
