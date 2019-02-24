//Author:   Gregorics Tibor
//Date:     2017.08.31.
//Title:    The greatest odd integer

#include <iostream>
#include "../library/maxsearch.hpp"
#include "../library/seqinfileenumerator.hpp"

using namespace std;

//class that solves the problem
//overrides the method func(), cond()
class MyMaxSearch : public MaxSearch<int>{
protected:
    int  func(const int& e) const override { return e;}
    bool cond(const int& e) const override { return e%2!=0;}
};

int main(){
    try{
        MyMaxSearch pr;
        SeqInFileEnumerator<int> enor("input.txt");
        pr.addEnumerator(&enor);

        pr.run();

        if (pr.found())
        cout << "The greatest odd integer:" << pr.optElem();
        else
        cout << "There is no odd integer!";
    } catch(SeqInFileEnumerator<int>::Exceptions ex){
        if(SeqInFileEnumerator<int>::OPEN_ERROR == ex){
            cout << "Wrong file name!\n";
            return 1;
        }
    }
    return 0;
}
