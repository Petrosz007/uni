#include <iostream>
#include "stopper.h"

using namespace std;

int main()
{
    std::cout << "Choise option:" << std::endl;
	Stopper stopper;

    char o;

    do{

        std::cin >> o;
        if(o == 's')
        {
            stopper.send(click);
        }

    }while(o != 'q');
	stopper.send(quit);

    return 0;
}
