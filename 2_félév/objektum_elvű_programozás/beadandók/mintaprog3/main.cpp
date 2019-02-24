#include <iostream>
#include <vector>
#include <fstream>
#include <sstream>
#include "jarmu.h"
#include "terep.h"
#include <typeinfo>

using namespace std;

void beolvas(ifstream &f, vector<Jarmu*> &versenyzok, vector<Terep*> &palya);
bool versenyez(vector<Jarmu*> &versenyzok, const vector<Terep*> &palya, int &ind);

#define NORMAL_MODE
#ifdef NORMAL_MODE

int main()
{
    string fajlNev;
    //cout << "A versenyadatok melyik fajlban vannak? ";
    //cin >> fajlNev;
    fajlNev = "input.txt";
    ifstream f(fajlNev.c_str());
    if (f.fail()) {
        cerr << "Hibas vagy nem letezo fajl!\n";
        return 1;
    }
    vector<Terep*> palya;
    vector<Jarmu*> versenyzok;
    beolvas(f, versenyzok, palya);

    int ind;
    if( versenyez(versenyzok, palya, ind) ) {
        cout<<"Nem esett ki senki a verseny folyaman!\n";
    }else {
        cout << "Az elso kiesett adatai:\n\n";
        cout << "Neve: " << versenyzok[ind]->getNev() << endl;
        cout << "Jarmuvenek tipusa: " << versenyzok[ind]->getTipus() << endl;
    }

    return 0;
}

bool versenyez(vector<Jarmu*> &versenyzok, const vector<Terep*> &palya, int &ind)
{
    bool l = true;
    for( unsigned int i = 0; i < palya.size() && l; ++i){
        for( unsigned int j = 0; j < versenyzok.size() && l; ++j)
        {
            ind = j;
            versenyzok[j]->halad(palya[i]);
            l = versenyzok[j]->el();
        }
    }
    return l;
}

void beolvas(ifstream &f, vector<Jarmu*> &versenyzok, vector<Terep*> &palya)
{
    int db;
    f >> db;
    versenyzok.clear();
    for (int i=0; i<db; i++)
    {
        string nev;
        int liter;
        string tipus;
        f >> nev >> liter >> tipus;
        if (tipus == "Kamion")
        {
            versenyzok.push_back(new Kamion(nev, liter, 25, 30, 25));
        }
        else if (tipus == "autó")
        {
            versenyzok.push_back(new SzemelyAuto(nev, liter, 7, 9, 10));
        }
        else
        {
            versenyzok.push_back(new Motor(nev, liter, 10, 12, 13));
        }
    }
    string sor;
    getline(f, sor, '\n');
    stringstream ss(sor);
    char ch;
    palya.clear();
    while(ss >> ch){
        switch(ch){
        case 'h' : palya.push_back(Homok::peldany()); break;
        case 'd' : palya.push_back(Domb::peldany()); break;
        case 'v' : palya.push_back(Viz::peldany()); break;
        }
    }
}

#else

bool versenyez(vector<Jarmu*> &versenyzok, const vector<Terep*> &palya, int &ind)
{
    bool l = true;
    for( unsigned int i = 0; l && i < palya.size() && l; ++i){
        for( unsigned int j = 0; l && j < versenyzok.size(); ++j)
        {
            ind = j;
            versenyzok[j]->halad(palya[i]);
            l = versenyzok[j]->el();
        }
    }
    return l;
}

#define CATCH_CONFIG_MAIN
#include "catch.hpp"

TEST_CASE("konstruktorok")
{
    setlocale(LC_ALL, "hun");
    SECTION("Kamion paraméteres konstruktor")
    {
        Kamion k("Dani",120);
        CHECK(k.getNev()=="Dani");
        CHECK(k.getTank()==120);
        SzemelyAuto a("Dani",60);
        CHECK(a.getNev()=="Dani");
        CHECK(a.getTank()==60);
        Motor m("Dani",20);
        CHECK(m.getNev()=="Dani");
        CHECK(m.getTank()==20);
    }
    SECTION("paraméteres konstruktor, túl sok benzin")
    {
        Kamion k("Dani",1600);
        CHECK(k.getTank()==1500);
        SzemelyAuto a("Dani",80);
        CHECK(a.getTank()==70);
        Motor m("Dani",120);
        CHECK(m.getTank()==50);
    }
}
TEST_CASE("pályaszakaszok száma szerint")
{
    SECTION("nulla pályaszakasz")
    {
        vector<Terep*> palya;
        Kamion* k=new Kamion("Dani",120);
        Kamion* k2=new Kamion("Dani",120);
        vector<Jarmu*> v;
        v.push_back(k);
        int ind;
        versenyez(v,palya,ind);
        CHECK(v[0]->getNev()==k2->getNev());
        CHECK(v[0]->getTank()==k2->getTank());
    }
    SECTION("egy pályaszakasz, homokos")
    {
        Kamion k("Dani",750);
        SzemelyAuto a("Dani",35);
        Motor m("Dani",25);

        k.halad(Homok::peldany());
        a.halad(Homok::peldany());
        m.halad(Homok::peldany());

        CHECK(k.getTank()==747.375);
        CHECK(a.getTank()==33.95);
        CHECK(m.getTank()==24.265);
    }
    SECTION("egy pályaszakasz, dombos")
    {
        Kamion k("Dani",750);
        SzemelyAuto a("Dani",35);
        Motor m("Dani",25);

        k.halad(Domb::peldany());
        a.halad(Domb::peldany());
        m.halad(Domb::peldany());

        CHECK(k.getTank()==746.85);
        CHECK(a.getTank()==33.74);
        CHECK(m.getTank()==24.055);
    }
    SECTION("egy pályaszakasz, vizes")
    {
        Kamion k("Dani",750);
        SzemelyAuto a("Dani",35);
        Motor m("Dani",25);

        k.halad(Viz::peldany());
        a.halad(Viz::peldany());
        m.halad(Viz::peldany());

        CHECK(k.getTank()==746.325);
        CHECK(a.getTank()==33.635);
        CHECK(m.getTank()==23.95);
    }
    SECTION("két pályaszakasz, homokos és dombos")
    {
        Kamion k("Dani",900);
        SzemelyAuto a("Dani",35);
        Motor m("Dani",25);

        k.halad(Homok::peldany());
        k.halad(Domb::peldany());
        a.halad(Homok::peldany());
        a.halad(Domb::peldany());
        m.halad(Homok::peldany());
        m.halad(Domb::peldany());

        CHECK(k.getTank()==894.17053);
        CHECK(a.getTank()==32.6918);
        CHECK(m.getTank()==23.321323);
    }
}

TEST_CASE("versenyzők száma szerint")
{
    SECTION("nulla versenyző")
    {
        vector<Terep*> palya;
        palya.push_back(Homok::peldany());
        vector<Jarmu*> v;
        int ind;
        versenyez(v, palya, ind);
        CHECK(v.size()==0);
    }
    SECTION("egy versenyző, egy pályaszakasz")
    {
        vector<Terep*> palya;
        palya.push_back(Homok::peldany());
        vector<Jarmu*> v;

        Kamion* k=new Kamion("Dani",750);
        v.push_back(k);
        int ind;
        versenyez(v, palya, ind);
        CHECK(v[0]->getTank()==747.375);
        v.clear();

        SzemelyAuto* a=new SzemelyAuto("Dani",35);
        v.push_back(a);
        versenyez(v, palya, ind);
        CHECK(v[0]->getTank()==33.95);
        v.clear();

        Motor* m=new Motor("Dani",25);
        v.push_back(m);
        versenyez(v, palya, ind);
        CHECK(v[0]->getTank()==24.265);
        v.clear();
    }
    SECTION("több versenyző, több pályaszakasz, nincs kieső")
    {
        vector<Terep*> palya;
        palya.push_back(Homok::peldany());
        palya.push_back(Domb::peldany());
        vector<Jarmu*> v;

        Kamion* k=new Kamion("Dani",900);
        v.push_back(k);
        SzemelyAuto* a=new SzemelyAuto("Dani",35);
        v.push_back(a);
        Motor* m=new Motor("Dani",25);
        v.push_back(m);
        int ind;
        versenyez(v, palya, ind);
        CHECK(v[0]->getTank()==894.17053);
        CHECK(v[1]->getTank()==32.6918);
        CHECK(v[2]->getTank()==23.321323);
    }
    SECTION("több versenyző, több pályaszakasz, van kieső")
    {
        vector<Terep*> palya;
        palya.push_back(Homok::peldany());
        palya.push_back(Domb::peldany());
        vector<Jarmu*> v;

        Kamion* k=new Kamion("Dani",900);
        v.push_back(k);
        SzemelyAuto* a=new SzemelyAuto("Dani",35);
        v.push_back(a);
        Motor* m=new Motor("Dani",25);
        v.push_back(m);
        Kamion* k2=new Kamion("Dani",5);
        v.push_back(k2);
        int ind;
        versenyez(v, palya, ind);
        CHECK(v[0]->getTank()==894.17053);
        CHECK(v[1]->getTank()==32.6918);
        CHECK(v[2]->getTank()==23.321323);
        CHECK(v[3]->getTank()==0);
        CHECK(!v[3]->el());
    }
}

#endif
