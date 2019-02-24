//Author:   Gregorics Tibor
//Date:     2017.12.15.
//Title:    The most frequented number - type-oriented version

#include <iostream>
#include <fstream>
#include "bag.h"

using namespace std;

//#define NORMAL_MODE
#ifdef NORMAL_MODE

//Activity:  Fills in a bag from a textfile
//           with integers between 0 and m
//           and gives the most frequented number of this bag.
int main()
{
    ifstream f( "input.txt" );
    if(f.fail()){ cout << "Wrong file name!\n"; return 1;}
    int m; f >> m;
    if(m<0){ cout << "Upper limit of the integers cannot be negative!\n"; return 1;}

    try{
        Bag b(m);
        int e;
        while(f >> e) { b.putIn(e); }
        cout << "The most frequented number: " << b.mostFrequented() << endl;
    }catch(Bag::Errors ex){
        if     (ex==Bag::WrongInput){ cout << "Illegal integer!\n"; }
        else if(ex==Bag::EmptyBag)  { cout << "No input no maxima!\n";    }
    }
    return 0;
}

#else
#define CATCH_CONFIG_MAIN
#include "catch.hpp"
#include "bag_test.h"

TEST_CASE("empty sequence", "[sum]")
{
    int m = 2;
    Bag b(m);
    CHECK_THROWS(b.mostFrequented());
}

TEST_CASE("one element in the file", "[sum]")
{
    int m = 10;
    Bag b(m); b.putIn(2);
    CHECK(2 == b.mostFrequented());
}

TEST_CASE("more elements in the file", "[sum]")
{
    int m = 10;
    Bag b(m); b.putIn(3); b.putIn(5); b.putIn(5); b.putIn(1);
    CHECK(5==b.mostFrequented());
}

TEST_CASE("beginning of the file", "[sum]")
{
    int m = 10;
    Bag b(m); b.putIn(1); b.putIn(1); b.putIn(2); b.putIn(2);
    CHECK(1==b.mostFrequented());
}

TEST_CASE("end of the file", "[sum]")
{
    int m = 10;
    Bag b(m); b.putIn(2); b.putIn(2); b.putIn(1); b.putIn(1); b.putIn(1);
    CHECK(1==b.mostFrequented());
}

TEST_CASE("scaling", "[sum]")
{
    int m = 10;
    Bag b(m);
    for(int i=0; i<10000; ++i) b.putIn(2);
    CHECK(2==b.mostFrequented());
}

/* ------------------------------------ */

TEST_CASE("creation of an empty bag", "[bag]")
{
    SECTION("bag can contain one kind of element")
    {
        int m = 0;
        Bag_Test b(m);
        vector<int> v = { 0 };
        CHECK(v == b.getArray());
        CHECK_THROWS(b.mostFrequented());
    }

    SECTION("bag can contain two kinds of elements")
    {
        int m = 1;
        Bag_Test b(m);
        vector<int> v = { 0 , 0 };
        CHECK(v == b.getArray());
        CHECK_THROWS(b.mostFrequented());
    }

    SECTION("bag can contain five kinds of elements")
    {
        int m = 4;
        Bag_Test b(m);
        vector<int> v = { 0, 0, 0, 0, 0 };
        CHECK(v == b.getArray());
        CHECK_THROWS(b.mostFrequented());
    }
}

TEST_CASE("first element into empty bag", "[putIn]")
{
    int m = 1;
    Bag_Test b(m);
    b.putIn(0);
    vector<int> v = { 1, 0 };
    CHECK(v == b.getArray());
    CHECK(0 == b.mostFrequented());
}

TEST_CASE("new element into a non-empty bag", "[putIn]")
{
    int m = 1;
    Bag_Test b(m); b.putIn(0);
    vector<int> v1 = { 1, 0 };
    CHECK(v1 == b.getArray());
    b.putIn(1);
    vector<int> v2 = { 1, 1 };
    CHECK(v2 == b.getArray());
    CHECK(0 == b.mostFrequented());
}

TEST_CASE("putting the same element twice in", "[putIn]")
{
    int m = 1;
    Bag_Test b(m); b.putIn(0); b.putIn(1);
    vector<int> v1 = { 1, 1 };
    CHECK(v1 == b.getArray());
    b.putIn(1);
    vector<int> v2 = { 1, 2 };
    CHECK(v2 == b.getArray());
    CHECK(1 == b.mostFrequented());
}

TEST_CASE("putting 0 in", "[putIn]")
{
    int m = 1;
    Bag_Test b(m); b.putIn(0); b.putIn(1);
    vector<int> v1 = { 1, 1 };
    CHECK(v1 == b.getArray());
    b.putIn(0);
    vector<int> v2 = { 2, 1 };
    CHECK(v2 == b.getArray());
    CHECK(0 == b.mostFrequented());
}

TEST_CASE("putting m in", "[putIn]")
{
    int m = 1;
    Bag_Test b(m); b.putIn(0); b.putIn(1);
    vector<int> v1 = { 1, 1 };
    CHECK(v1 == b.getArray());
    b.putIn(m);
    vector<int> v2 = { 1, 2 };
    CHECK(v2 == b.getArray());
    CHECK(1 == b.mostFrequented());
}

TEST_CASE("putting an illegal element", "[putIn]")
{
    int m = 1;
    Bag_Test b(m); b.putIn(0); b.putIn(1); b.putIn(1);
    vector<int> v = { 1, 2 };
    int e = b.mostFrequented();
    CHECK(v == b.getArray());
    CHECK_THROWS(b.putIn(2));
    CHECK(v == b.getArray());
    CHECK(e == b.mostFrequented());
}

TEST_CASE("empty bag", "[mostFrequented]")
{
    int m = 1;
    Bag_Test b(m);
    CHECK_THROWS(b.mostFrequented());
}

TEST_CASE("one element in the bag", "[mostFrequented]")
{
    int m = 1;
    Bag_Test b(m); b.putIn(0);
    vector<int> v = { 1, 0 };
    CHECK(v == b.getArray());
    CHECK(0 == b.mostFrequented());
}

TEST_CASE("one most frequented element", "[mostFrequented]")
{
    int m = 1;
    Bag_Test b(m); b.putIn(0); b.putIn(0); b.putIn(1);
    vector<int> v = { 2, 1 };
    CHECK(v == b.getArray());
    CHECK(0 == b.mostFrequented());
}

TEST_CASE("more most frequented elements", "[mostFrequented]")
{
    int m = 1;
    Bag_Test b(m); b.putIn(0); b.putIn(0); b.putIn(1); b.putIn(1);
    vector<int> v = { 2, 2 };
    CHECK(v == b.getArray());
    bool l = (0 == b.mostFrequented()) || (1 == b.mostFrequented());
    CHECK(l);
}

TEST_CASE("most frequented element : 0", "[integration]")
{
    int m = 1;
    Bag_Test b(m); b.putIn(0);
    vector<int> v = { 1, 0 };
    CHECK(0 == b.mostFrequented());
}

TEST_CASE("most frequented element : m", "[integration]")
{
    int m = 1;
    Bag_Test b(m); b.putIn(1);
    vector<int> v = { 0, 1 };
    CHECK(1 == b.mostFrequented());
}

#endif

