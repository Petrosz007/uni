#include <iostream>
#include "complex.h"
#include <vector>

/*
void g(const int x)
{
    if(x==5){
    std::cout << x << " is 5" << std::endl;
    }
}
*/

void calc_abs(const std::vector<Complex>& numbers)
{
    double abs = 0.0;    
    for(int i=0;i<numbers.size();++i)
    {
        abs += numbers[i].abs();
    }
    std::cout << "abs: " << abs << std::endl;
}


int main()
{
    std::vector<Complex> numbers;
    const Complex i(0,1);
    numbers.push_back(i);
//    g(5);    
    Complex c(5.4, 3.6);
    numbers.push_back(c);
    c.print();

    Complex d(3, -5);
    numbers.push_back(d);
    //d.print();

    //d.conjugate();
    //d.print();

    //Complex e;
    //e.print();

    c.add(d);
    numbers.push_back(c);
    c.print();
    Complex f = i.plus(c);
    numbers.push_back(f);
    f.print();

//    const Complex pi(3.14);
    const Complex pi = 3.14;
    pi.print();
    Complex m = pi.plus(1.4);
    m.print();

    std::cout << f.abs() << std::endl;

    calc_abs(numbers);
    //i.conjugate();
    //i.print();
/*
   
*/
    return 0;
}
