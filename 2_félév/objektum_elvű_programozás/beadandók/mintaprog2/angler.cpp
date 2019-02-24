//Author:   Gregorics Tibor
//Date:     2018.09.23.
//Title:    Anglers

#include "angler.h"

using namespace std;

//Task: 	Reading the next angler with his ability
//Input:    ContestEnor tt  - the enumerator of contests
//Output:   Angler _cur     - the next angler
//          bool _end       - the end of the enumeration
//Activity:
void AnglerEnor::next()
{
    if( !(_end = _tt.end()) ){
        _cur.id = _tt.current().angler;
        _cur.skillful = _tt.current().counter >= 2;
        for( ; !_tt.end() && _tt.current().angler == _cur.id; _tt.next() ){
            _cur.skillful = _cur.skillful && (_tt.current().counter >=2);
        }
    }
}
