//Author:    Gregorics Tibor
//Date:     2015.02.22.
//Title:    The longest uphill part of a trip

#include "lengthenumerator.h"

//Task: 	Computing the next uphill part of the trip
//Input:    ifstream f - sequence of altitudes of the trip
//Output:   int        - length of the longest uphill part of the trip
//Activity:	Overstepping the next downhill part (selcetion)
//          and counting the length of the next uphill part
void LengthEnumerator::next()
{
    for( ; !_b.end() && !_b.current(); _b.next() );
    if ( (_end = _b.end()) ) return;
    for( _length = 0 ; !_b.end() && _b.current(); _b.next() ) ++_length;
}
