#include "complex.h"
#include <iostream>
#include <cmath>

double Complex::get_real() const{
    return real;
}

double Complex::get_im() const{
    return im;
}

void Complex::print() const {
     std::cout << real;
    if(im>=0.0)
    {
        std::cout <<"+";
    }
    std::cout << im << "i" <<  std::endl;    
}

Complex::Complex()
{
    real = 0.0;
    im = 0.0;
}

Complex::Complex(double a, double b)
{
    real = a;
    im = b;
}

void Complex::conjugate()
{
    im *= -1;
}


void Complex::add(const Complex& other)
{
    real += other.real;
    im += other.im;
}

double Complex::abs() const
{
    return sqrt(real*real + im*im);
}


Complex Complex::plus(const Complex& other) const
{
    Complex result(
        real + other.real,
        im + other.im
    );

    return result;
}
















