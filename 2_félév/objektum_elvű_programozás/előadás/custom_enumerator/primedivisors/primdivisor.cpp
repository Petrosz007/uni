//Author:    Gregorics Tibor
//Date:     2015.02.22.
//Title:    The sum of the positive prime divisors of a natural number

#include "primdivisor.h"

//Task: 	Constructing the enumerator
//Input:    int i   - the number those prime divisor should be enumerated
//Output:   int n   - the natural number those prime divisor should be enumerated
//Activity:	n:=i; if i<=0 then n:=1 (instead if error)
PrimDivisor::PrimDivisor(int i)
{
    n = i>0 ? i : 1;
}

//Task: 	Computing the smallest prime divisor of the number n
//Input:    int n   - natural number
//Output:   int d   - the smallest prime divisor of the number n
//Activity:	If n>1, it finds  the first divisor of n (selection) that is greater than 1.
//          This number must be prime.
void PrimDivisor::first()
{
    if(n>1){
        d = 2;
        while(n%d!=0 && d<=n/2) ++d;
        if (d>n/2) d = n;
    }
}

//Task: 	Computing the next prime divisor of the original number
//Input:    int n   - natural number
//          int d   - the smallest prime divisor of the number n
//Output:   int n   - the reduced number of the number n
//          int d   - the smallest prime divisor of the reduced number n
//Activity:	It divides the number n by d
//          and than if n>1, it finds  the first divisor of n (selection) that is greater than 1.
void PrimDivisor::next()
{
    while(n%d==0) n=n/d;
    first();
}

