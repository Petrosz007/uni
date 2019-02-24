//Author:    Gregorics Tibor
//Date:      2017.08.08.
//Title:     Diagonal matrix

#include <iostream>
#include "diag.h"

using namespace std;

//#define NORMAL_MODE
#ifdef NORMAL_MODE

//class of menu for diagonal matrix
class Menu{
  public:
    Menu(){a.reSize(3);}
    void run();
  private:
    Diag a;

    void menuWrite();
    void get() const;
    void set();
    void read();
    void write();
    void sum();
    void mul();
};

int main()
{
//    setlocale(LC_ALL,"Hun");
    Menu m;
    m.run();
}

void Menu::run()
{
    int n = 0;
    do{
        menuWrite();
        cout << endl << ">>>>" ; cin >> n;
        switch(n){
            case 1: get();
                    break;
            case 2: set();
                    break;
            case 3: read();
                    break;
            case 4: write();
                    break;
            case 5: sum();
                    break;
            case 6: mul();
                    break;
        }
    }while(n!=0);
}

void Menu::menuWrite()
{
     cout << endl << endl;
     cout << " 0. - Quit" << endl;
     cout << " 1. - Get an element of the matrix" << endl;
     cout << " 2. - Overwrite an element of the matrix" << endl;
     cout << " 3. - Read matrix" << endl;
     cout << " 4. - Write matrix" << endl;
     cout << " 5. - Add matrices" << endl;
     cout << " 6. - Multiply matrices" << endl;
}

void Menu::get() const
{
    int i,j;
    cout << "Give the index of the row: "; cin >> i;
    cout << "Give the index of the column: "; cin >> j;
    try{
        cout << "a[" << i << "," << j << "]= " << a(i,j) << endl;
    }catch(Diag::Exceptions ex){
        if(ex == Diag::OVERINDEXED)
            cout << "Overindexing!" << endl;
        else cout << "Unhandled ecxeption!" << endl;
    }
}

void Menu::set()
{
    int i,j,e;
    cout << "Give the index of the row: "; cin >> i;
    cout << "Give the index of the column: "; cin >> j;
    cout << "Give the value: "; cin >> e;
    try{
        a(i,j) = e;
    }catch(Diag::Exceptions ex){
        if(ex == Diag::OVERINDEXED)
   		      cout << "Overindexing!" << endl;
        if (ex == Diag::NULLPART)
   		      cout << "These indexes does not point to the diagonal!" << endl;
    }
}

void Menu::read()
{
    cin >> a;
}

void Menu::write()
{
    cout << a << endl;
}

void Menu::sum()
{
    Diag a(3), b(3), c(3);

    cout << "Give the diagonal of the first matrix: " << endl;
    cin >> a;
    cout << "Give the diagonal of the second  matrix: " << endl;
    cin >> b;
    cout << "Summation of matrices: " << endl;
    cout << a+b << endl;
}

void Menu::mul()
{
    Diag a(3), b(3);

    cout << "Give the diagonal of the first matrix: " << endl;
    cin >> a;
    cout << "Give the diagonal of the second  matrix: " << endl;
    cin >> b;
    cout << "Multiplication of matrices: " << endl;
    cout << a*b << endl;
}

#else
#define CATCH_CONFIG_MAIN
#include "catch.hpp"

TEST_CASE("create", "inp.txt") {
    const string fileName = "inp.txt";

    ifstream in(fileName.c_str());
    if(in.fail()){
        cout << "File name error!" << endl;
        exit(1);
    }

    Diag c(3);
    in >> c;
    CHECK(c(0,0)==3);
    CHECK(c(1,1)==2);
    CHECK(c(2,2)==1);

    Diag b(2);
    in >> b;
    CHECK(b(0,0)==2);
    CHECK(b(1,1)==1);

    Diag a(1);
    in >> a;
    CHECK(a(0,0)==1);
}

TEST_CASE("getting and changing an element of the matrix", "") {

    Diag a(3);
    a(0,0) = 0;
    CHECK(a(0,0)==0);
}

TEST_CASE("copy constructor", "inp.txt") {
    const string fileName = "inp.txt";

    ifstream in(fileName.c_str());
    if(in.fail()){
        cout << "File name error!" << endl;
        exit(1);
    }

    Diag a(3);
    in >> a;

    Diag b = a;

    CHECK(a(0,0)==b(0,0));
    CHECK(a(1,1)==b(1,1));
    CHECK(a(2,2)==b(2,2));
}

TEST_CASE("assignment operator", "inp.txt") {
    const string fileName = "inp.txt";

    ifstream in(fileName.c_str());
    if(in.fail()){
        cout << "File name error!" << endl;
        exit(1);
    }

    Diag a(3), b(3);
    in >> a;

    b = a;
    CHECK(a(0,0)==b(0,0));
    CHECK(a(1,1)==b(1,1));
    CHECK(a(2,2)==b(2,2));

    Diag c(3);
    c = b = a;
    CHECK(a(0,0)==c(0,0));
    CHECK(a(1,1)==c(1,1));
    CHECK(a(2,2)==c(2,2));

    a = a;
    CHECK(a(0,0)==3);
    CHECK(a(1,1)==2);
    CHECK(a(2,2)==1);
}

TEST_CASE("add", "inp2.txt") {
    const string fileName = "inp2.txt";

    ifstream in(fileName.c_str());
    if(in.fail()){
        cout << "File name error!" << endl;
        exit(1);
    }

    Diag a(3), b(3), c(3), d(3), f(3), e(3), z(3);
    in >> a >> b >> z >> e;

    c = a + b;
    CHECK(c(0,0)==4);
    CHECK(c(1,1)==5);
    CHECK(c(2,2)==6);

    d = b + a;
    CHECK(c(0,0)==d(0,0));
    CHECK(c(1,1)==d(1,1));
    CHECK(c(2,2)==d(2,2));

    d = (a + b) + c;
    f = a + (b + c);
    CHECK(d(0,0)==f(0,0));
    CHECK(d(1,1)==f(1,1));
    CHECK(d(2,2)==f(2,2));

    c = a + z;
    CHECK(c(0,0)==a(0,0));
    CHECK(c(1,1)==a(1,1));
    CHECK(c(2,2)==a(2,2));
}

TEST_CASE("multiply", "inp2.txt") {
    const string fileName = "inp2.txt";

    ifstream in(fileName.c_str());
    if(in.fail()){
        cout << "File name error!" << endl;
        exit(1);
    }

    Diag a(3), b(3), c(3), d(3), f(3), e(3), z(3);
    in >> a >> b >> z >> e;

    c = a * b;
    CHECK(c(0,0)==3);
    CHECK(c(1,1)==6);
    CHECK(c(2,2)==9);

    d = b * a;
    CHECK(c(0,0)==d(0,0));
    CHECK(c(1,1)==d(1,1));
    CHECK(c(2,2)==d(2,2));

    d = (a * b) * c;
    f = a * (b * c);
    CHECK(d(0,0)==f(0,0));
    CHECK(d(1,1)==f(1,1));
    CHECK(d(2,2)==f(2,2));

    c = a * e;
    CHECK(c(0,0)==a(0,0));
    CHECK(c(1,1)==a(1,1));
    CHECK(c(2,2)==a(2,2));
}

TEST_CASE("ecxeptions", "") {

    Diag a(3);

    try{
        a(3,3) = 4;
    } catch(Diag::Exceptions ex){
        if(Diag::OVERINDEXED) ;
    }

    try{
        a(-1,4) = 4;
    } catch(Diag::Exceptions ex){
        if(Diag::OVERINDEXED) ;
    }

    Diag b(2); Diag c(3);

    try{
        a = b;
    } catch(Diag::Exceptions ex){
        if(Diag::DIFFERENT) ;
    }

    try{
        c = a + b;
    } catch(Diag::Exceptions ex){
        if(Diag::DIFFERENT) ;
    }

    try{
        c = a * b;
    } catch(Diag::Exceptions ex){
        if(Diag::DIFFERENT) ;
    }


    try{
        a(1,0) = 4;
    } catch(Diag::Exceptions ex){
        if(Diag::NULLPART) ;
    }

    try{
        int  k = a(1,0);
    } catch(Diag::Exceptions ex){
        if(Diag::NULLPART) ;
    }
}

#endif
