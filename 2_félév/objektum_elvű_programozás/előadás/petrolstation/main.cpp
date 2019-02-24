#include <iostream>
#include <sstream>
#include "petrol.h"
#include "car.h"

using namespace std;

int main()
{
    PetrolStation p(4, 2) ;

    vector<Car> cars(8);
    for( unsigned int i=0; i<cars.size(); ++i ){
        ostringstream os; os << i+1 << ". car";
        cars[i].setName(os.str());
        cars[i].refuel(&p, i%6, (i+1)*5);
    }
    return 0;
}
