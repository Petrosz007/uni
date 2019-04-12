#include "list.h"
#include <iostream>
/*
void free_list(list* l)
{
    if(l!=0)
    {
        free_list(l->get_next());
        delete l;
    }
}*/

void print_list(list* l)
{
    if(l!=0)
    {
        std::cout << l->get_value() << " ";
        print_list(l->get_next());
    }
}

int main()
{
    list* k = new list(3);
    list l = list(42, k);
//    print_list(&l);
    std::cout<<std::endl;

    l = l;

    list m(l);
    m.get_next()->set_value(4);
    print_list(&l);
    std::endl(std::cout);
    print_list(&m);
    std::endl(std::cout);
/*
    std::cout << l->get_value() << std::endl;
    std::cout << l->get_next()->get_value() << std::endl;
    std::cout << "list created: " << list::get_counter() << std::endl;

    delete l;

    std::cout << "list created: " << list::get_counter() << std::endl;
*/

    return 0;
}
