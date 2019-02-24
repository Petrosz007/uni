//Author:   Gregorics Tibor
//Date:     2017.08.31.
//Title:    The least busy hour of the station

#include <iostream>
#include "../library/maxsearch.hpp"
#include "../library/seqinfileenumerator.hpp"

using namespace std;

// type of measurements
struct Pair{
    int time;
    int number;

    int day()  const { return time/100; }
    int hour() const { return time%100; }

friend ifstream& operator>>(ifstream&, Pair&);
};

//class that solves the problem
//overrides the method func(), cond()
class MyMinSearch: public MaxSearch<Pair, int, Less<int> > {
protected:
    int  func(const Pair &e) const override { return e.number; }
    bool cond(const Pair &e) const override { return e.day()%7==6 || e.day()%7==0; }
};

int main()
{
    try{
        MyMinSearch pr;
        SeqInFileEnumerator<Pair> enor("input.txt");
        pr.addEnumerator(&enor);

        pr.run();

        if (pr.found()){
            Pair p = pr.optElem();
            cout << "The least busy hour was the " << p.hour() << ". hour of the " << p.day() << ". day when "
                << pr.opt() << " people stepped into the station.\n";
        }else cout << "There is no weekend data.\n";
    } catch(SeqInFileEnumerator<int>::Exceptions &ex ){
        if(SeqInFileEnumerator<int>::OPEN_ERROR == ex){
            cout << "Wrong file name!\n";
            return 1;
        }
    }
    return 0;
}

istream& operator>>(istream& f, Pair& df)
{
    f >> df.time >> df.number;
    return f;
}

