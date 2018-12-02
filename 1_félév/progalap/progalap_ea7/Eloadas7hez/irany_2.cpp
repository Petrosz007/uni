//Gipsz Jakab
//GIJAAJT.ELTE
//gipsugynok@elte.hu
//
//Feladat:
// Adjuk meg, hogy az origóból nézve az 1. sík-negyedbe eso P ponthoz képest a Q
// balra, jobbra, vagy pedig egy irányban látszik-e!
// Irany(P,Q):=-1, ha balra, +1, ha jobbra; 1, ha egy irányba esik.
//
//Specifikáció:
// Be: P,Q ELEME Pont, Pont=XxY, X,Y=R
// Ki: Ir ELEME Z
// Ef: –
// Uf: Ir=Irány(P,Q)
// Def: Irány:PontxPont->Z
//      Irány(p,q):=sgn(p.y*q.x-q.y*p.x)
//
//ALgoritmus:
// Típus
//   TPont=Rekord(x,y:Valós)
// Változó
//   P,Q:TPont
//   Ir:Egész
// ...
//TODO az algoritmust befejezni, legalább a lényegi részét
//

#include <iostream>
#include <stdlib.h> //Code::Blocks 10.05-höz már kell a system kedvéért

using namespace std;

//típusdefiníció(k):
struct TPont{double x,y;};

//finomítás(ok) fejsora(i):
int Irany(TPont p, TPont q);
double BeKoordinata(string kerd);
TPont BePont(string pontNev);
void KiEredmeny(TPont p, TPont q, int ir);
void BillentyureVar();

int main()
{
    //bemenet:
    TPont P,Q;
    //kimenet:
    int Ir;

    cout << "Q iranya a P-tol\n" << endl;
    //beolvasás:
    P=BePont("P");
    Q=BePont("Q");
    //a lényeg:
    Ir=Irany(P,Q);
    //kiírás:
    KiEredmeny(P,Q,Ir);
    BillentyureVar();
    return 0;
}

int Irany(TPont p, TPont q)
{
    int F; double S;
    S=p.y*q.x-q.y*p.x;
    if (S<0) F=-1;
    else if (S==0) F=0;
    else if (S>0) F=1;
    return F;
}

double BeKoordinata(string kerd)
{
    double adat;
    //beolvasás ellenorzéséhez:
    bool hiba;
    string tmp;
    do{
      cerr << kerd; cin >> adat;
      hiba=cin.fail();
      if (hiba)
      {
        cerr << "Hibas adat!" << endl;
        cin.clear(); getline(cin,tmp,'\n');//a sorvégjel megevése
      }
    }while (hiba);
    return adat;
}

TPont BePont(string pontNev)
{
    TPont pont;//segéd pont
    pont.x=BeKoordinata("Add meg "+pontNev+" x-koordinatajat!");
    pont.y=BeKoordinata("Add meg "+pontNev+" y-koordinatajat!");
    return pont;
}

void KiEredmeny(TPont p, TPont q, int ir)
{
    cout<<"("<<q.x<<","<<q.y<<") ";
    switch (ir){
      case -1: cout<<"("<<p.x<<","<<p.y<<")-tol balra\n"; break;
      case +1: cout<<"("<<p.x<<","<<p.y<<")-tol jobbra\n"; break;
      case  0: cout<<"("<<p.x<<","<<p.y<<")-vel egy iranyba\n"; break;
    }
}

void BillentyureVar()
{
    cout << endl;
    system("pause");
}
