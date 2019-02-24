//Author:   Gregorics Tibor
//Date:     2014.04.01.
//Title:    Eliminating accented vowel

#include <fstream>
#include <iostream>
#include <cstdlib>
#include <sstream>

using namespace std;

char transform(char ch);

//Activity: Transforming an accented text to one without accents
int main()
{
    ifstream x("inp.txt");
    if ( x.fail() ){
        cout<<"Wrong file name!"<<endl;
        char ch; cin>>ch;
        exit(1);
    }

    ofstream y("out.txt");
    if ( y.fail() ){
        cout<<"Output file cannot be created!"<<endl;
        char ch; cin>>ch;
        exit(1);
    }

    char ch;
    while(x.get(ch)){
        y << transform(ch);
    }

  return 0;
}

//Task: 	Transforming an accented vowel to one without accent
//Input:    ch   - character
//Output:   char - transformed character
//Activity: if a character is not accented, it preserves it; if it is accented, it transforms it to one without accent
char transform(char ch)
{
    char new_ch;
    switch (ch) {
        case 'á' :                          new_ch = 'a'; break;
        case 'é' :                          new_ch = 'e'; break;
        case 'í' :                          new_ch = 'i'; break;
        case 'ó' : case 'ö' : case 'õ' :    new_ch = 'o'; break;
        case 'ú' : case 'ü' : case 'û' :    new_ch = 'u'; break;
        case 'Á' :                          new_ch = 'A'; break;
        case 'É' :                          new_ch = 'E'; break;
        case 'Í' :                          new_ch = 'I'; break;
        case 'Ó' : case 'Ö' :case 'Õ' :     new_ch = 'O'; break;
        case 'Ú' : case 'Ü' :case 'Û' :     new_ch = 'U'; break;
        default  :                          new_ch = ch;
    }
    return new_ch;
}
