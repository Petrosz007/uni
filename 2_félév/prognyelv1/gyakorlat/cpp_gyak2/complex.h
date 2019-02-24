#ifndef COMPLEX_H
#define COMPLEX_H


class Complex
{
    double real, im;

public:
    Complex();
    Complex(double a, double b=0.0);

    double get_real() const;
    double get_im() const;
    double abs() const;

    void print() const;

    void add(const Complex& other);
    Complex plus(const Complex& other) const;

    void conjugate();
};

#endif

