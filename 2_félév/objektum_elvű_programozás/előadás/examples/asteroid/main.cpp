//Author:   Gregorics Tibor
//Date:     2017.08.31.
//Title:    Dangerous asteroids

#include <iostream>
#include <sstream>
#include "../library/summation.hpp"
#include "../library/seqinfileenumerator.hpp"

using namespace std;

//type of observations
struct Observation{
    string id;
    string date;
    int    mass;
    int    distance;
};

//type of observations
struct Asteroid
{
    string id;
    int    mass;
    bool   near;
};

//enumerator of asteroids recorded in text file
//overrides the method first(), next(), current(), end()
class AsteroidEnumerator : public Enumerator<Asteroid> {
protected:
    SeqInFileEnumerator<Observation> *_f;
    Asteroid _current;
    bool _end;
public:
    AsteroidEnumerator(const string& str)
        { _f = new SeqInFileEnumerator< Observation>(str); }
    ~ AsteroidEnumerator(){ delete _f; }
    void first()         override { _f->first(); next(); }
    void next()          override;
    bool end()     const override { return _end; }
    Asteroid current() const override { return _current; }
};

//class of listing the selected asteroids
//overrides the method func(), cond()
class Assortment : public Summation<Asteroid, ostream>{
public:
    Assortment(ostream* o) : Summation<Asteroid, ostream>(o) {}
    string func(const Asteroid& e) const override {
        ostringstream os;
        os << e.id << " " << e.mass << endl;
        return os.str();
    }
    bool cond(const Asteroid &e) const override { return e.near; }
};

//Activity: writes the selected asteroids
int main()
{
    Assortment pr(&cout);
    AsteroidEnumerator enor("input.txt");
    pr.addEnumerator(&enor);
    pr.run();

    return 0;
}

//type of results
struct Result{
    int mass;
    bool near;
    Result(){}
    Result(int a, bool b) : mass(a), near(b) {}
};

//class of a conditional maximum search and an optimist linear search embedded into two summation
//overrides the method func(), neutral(), add(), first()
class DoubleSummation : public Summation<Observation, Result>
{
private:
    string id;
public:
    DoubleSummation(const string& str) : id(str) {}
protected:
    Result func(const Observation& e) const override {
        return Result( e.mass, e.distance < 10000 );
    }
    Result neutral() const override {
        return Result( 0, true );
    }
    Result add(const Result &a, const Result &b) const override {
        return Result(  max(a.mass,b.mass), //a.mass < b.mass ? b.mass : a.mass,
                        a.near && b.near );
    }
    void first() override {}
    bool whileCond(const Observation& e) const override {
        return e.id == id;
    }
};

//Task: 	reading the next asteroid
//Input:    SeqInFileEnumerator<Observation> *_f    - sequential input file of asteroids
//Output:   Asteroid _current                       - the current asteroid
//          bool _end                               - flag of the end of asteroids
//Activity: reading the next asteroid with processed data
void AsteroidEnumerator::next(){
    if((_end = _f->end())) return;

    _current.id = _f->current().id;
    DoubleSummation pr(_current.id);
    pr.addEnumerator(_f);
    pr.run();
    _current.mass = pr.result().mass;
    _current.near = pr.result().near;
}


//reading of Observation
istream& operator>>(istream& in, Observation &e)
{
    in >> e.id >> e.date >> e.mass >> e.distance;
    return in;
}
