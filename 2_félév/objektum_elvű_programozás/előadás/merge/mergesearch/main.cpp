#include <iostream>
#include <fstream>
#include <string>

using namespace std;

int main()
{
    ifstream x("founded.txt");
    ifstream y("virginia.txt");

    string dx, dy;

    string elem;
    bool l = false;
    x >> dx; y >> dy;
    while( !l && (!x.fail() || !y.fail() )){
        if(y.fail() || (!x.fail() &&dx<dy)){
            l = true; elem = dx;
            x >> dx;
        } else if( x.fail() || (!y.fail() && dx>dy)){
            l = true; elem = dy;
            y >> dy;
        }else if ( !x.fail() && !y.fail() && dx==dy){

            x >> dx; y >> dy;
        }
    }

    if(l) cout << elem << " is a ";
    else cout << "There is no !\n";
    cout << "president who is one of the founding fathers but not from Virginia.";
    return 0;
}
