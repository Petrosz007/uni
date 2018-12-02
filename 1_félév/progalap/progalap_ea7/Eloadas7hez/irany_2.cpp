//Gipsz Jakab
//GIJAAJT.ELTE
//gipsugynok@elte.hu
//
//Feladat:
// Adjuk meg, hogy az orig�b�l n�zve az 1. s�k-negyedbe eso P ponthoz k�pest a Q
// balra, jobbra, vagy pedig egy ir�nyban l�tszik-e!
// Irany(P,Q):=-1, ha balra, +1, ha jobbra; 1, ha egy ir�nyba esik.
//
//Specifik�ci�:
// Be: P,Q ELEME Pont, Pont=XxY, X,Y=R
// Ki: Ir ELEME Z
// Ef: �
// Uf: Ir=Ir�ny(P,Q)
// Def: Ir�ny:PontxPont->Z
//      Ir�ny(p,q):=sgn(p.y*q.x-q.y*p.x)
//
//ALgoritmus:
// T�pus
//   TPont=Rekord(x,y:Val�s)
// V�ltoz�
//   P,Q:TPont
//   Ir:Eg�sz
// ...
//TODO az algoritmust befejezni, legal�bb a l�nyegi r�sz�t
//

#include <iostream>
#include <stdlib.h> //Code::Blocks 10.05-h�z m�r kell a system kedv��rt

using namespace std;

//t�pusdefin�ci�(k):
struct TPont{double x,y;};

//finom�t�s(ok) fejsora(i):
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
    //beolvas�s:
    P=BePont("P");
    Q=BePont("Q");
    //a l�nyeg:
    Ir=Irany(P,Q);
    //ki�r�s:
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
    //beolvas�s ellenorz�s�hez:
    bool hiba;
    string tmp;
    do{
      cerr << kerd; cin >> adat;
      hiba=cin.fail();
      if (hiba)
      {
        cerr << "Hibas adat!" << endl;
        cin.clear(); getline(cin,tmp,'\n');//a sorv�gjel megev�se
      }
    }while (hiba);
    return adat;
}

TPont BePont(string pontNev)
{
    TPont pont;//seg�d pont
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
