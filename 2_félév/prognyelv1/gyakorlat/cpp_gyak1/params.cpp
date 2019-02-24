#include <iostream>

void swapBad(int a, int b){
    int c = a;
    a = b;
    b = c;
}

void swapPtr(int* a, int* b){
    int c = *a;
    *a = *b;
    *b = c;   
}


void swapPtr2(int* const a, int* const b){
    int c = *a;
    *a = *b;
    *b = c;   
}

void swapGood(int& a, int& b)
{
    int c = a;
    a = b;
    b = c;
}


int main()
{
int x=5, y=6;
std::cout << x << ", " << y << std::endl;

int * const p = &x;
int *q = &y;
    q = &x;

    //p = &y;
    
std::cout << p << ", " << q << std::endl;

 *p = 7;

std::cout << x << ", " << y << std::endl;
   
    const int answer = 42;
    //int * r = &answer; //nem const!
    //megengedné, hogy átírjuk r-en keresztül az értéket.
    int const * r = &answer;
    int const * const s = &answer;


/*
    std::cout << x << ", " << y << std::endl;
    swapBad(x,y);
    std::cout << x << ", " << y << std::endl;
    swapPtr(&x,&y);
    std::cout << x << ", " << y << std::endl;
    swapGood(x,y);
    std::cout << x << ", " << y << std::endl;
    */
    return 0;
}
