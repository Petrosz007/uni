#include <iostream>
#include <cstdio>

//using namespace std;

namespace A{
    int x = 3;
}

struct B{
    int y;
};

int main()
{
    int x = 6;
    const char* text = "Hello World!";
/*
    printf("%d\n", x);
//    printf("%S\n", x); //segfault
    std::cout << x << std::endl;

    printf("%s\n", text);
    std::cout << text << std::endl;
*/
    B b;
    b.y = 42;
    //printf("%s\n", b); //segfault
    //std::cout << b << std::endl; //comp.error

/*
    int x = 5;
    {
        int x = 4;
        std::cout << x << std::endl; //4
        std::cout << A::x << std::endl; //3
    }
    //std::cout << x << std::endl; //5
    std::operator<<(std::cout, "Hello World");
*/
    
}
