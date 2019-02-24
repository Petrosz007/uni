//Author:   Gregorics Tibor
//Date:     2017.08.31.
//Title:    How many foods need sugar

#include <iostream>
#include <vector>
#include <sstream>
#include "../library/counting.hpp"
#include "../library/seqinfileenumerator.hpp"
#include "../library/stringstreamenumerator.hpp"
#include "../library/linsearch.hpp"
#include "../library/arrayenumerator.hpp"

using namespace std;

//type of ingredients
struct Ingredient{
    string substance;
    int quantity;
    string unit;
};

//type of recipes
struct Recipe{
    string name;
    bool has_sugar;
};

//class of counting of ingredients including sugar
//overrides the method cond()
class MyCounting : public Counting<Recipe>{
protected:
    bool cond(const Recipe &e) const { return e.has_sugar; }
};

//Activity: writes the selected asteroids
int main()
{
    MyCounting pr;
    SeqInFileEnumerator<Recipe> enor("input.txt");
    pr.addEnumerator(&enor);
    pr.run();
    cout << "Number of the recipes that need sugar: " << pr.result() << endl;

    return 0;
}

//reading of ingredient
istream& operator>>(istream& in, Ingredient &e)
{
    in >> e.substance >> e.quantity >> e.unit;
    return in;
}

//class of searching sugar among the ingredients
//overrides the method cond()
class MyLinSearch : public LinSearch<Ingredient>
{
protected:
    bool cond(const Ingredient &e) const override { return e.substance == "sugar"; }
};

//reading of recipe
istream& operator>>(istream& in, Recipe &e)
{
    string line;
    getline(in, line);
    stringstream is(line);

    is >> e.name;

    StringStreamEnumerator<Ingredient> enor(is);
    MyLinSearch pr;
    pr.addEnumerator(&enor);
    pr.run();
    e.has_sugar = pr.found();

    return in;
}
