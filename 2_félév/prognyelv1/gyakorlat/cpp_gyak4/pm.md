### Feladat:

Az itt található `main.cpp`fájlhoz a megfelelő header állomány elkészítése úgy, hogy a program a futtatás végén az `"All tests successfully passed!"` üzenetet írja a konzolra.

```
#include "header.h"
#include <iostream>
#include "header.h"

int err_no = 0;

template <class T>
void assert(const T& a, const T& b, std::string error="")
{
    if(a!=b)
    {
        std::endl(std::cout);
        ++err_no;
        std::cerr <<"\t" << err_no <<". error: " << error << std::endl;
        std::cerr << "\t    Expected value: " << a <<", actual value: " << b << std::endl;
    }
}

int main()
{
    User u;
    assert(std::string(), u.getName() );
    assert(1, User::getObjectCounter());
    
    const User b("Bela");
    assert(std::string("Bela"), b.getName() );
    assert(2, User::getObjectCounter());
    
    {
        User h;
    }
    
    assert(2, User::getObjectCounter());
    
    User* g;
    {
        g = new User(User("Joco"));
    }
    
    assert(3, User::getObjectCounter());
    assert(std::string("Joco"), g->getName() );
    g->setName("whatever");
    assert(std::string("whatever"), g->getName() );
    
    if(g){
        delete g;
    }
    assert(2, User::getObjectCounter());
    
    if(err_no)
    {
        std::cout << "You gotta work in this, mate!" << std::endl;
    }
    else
    {
        std::cout << "All tests successfully passed!" << std::endl;
    }
    return 0;
}
```
