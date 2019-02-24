#pragma once

#include "terep.h"
#include <string>

class Jarmu
{
    public:
        Jarmu(std::string nev,double tank,double tankMax,double fogyasztHomok,double fogyasztDomb,double fogyasztViz)
            : _nev(nev),_tank(tank),_tankMax(tankMax),_fogyasztHomok(fogyasztHomok),_fogyasztDomb(fogyasztDomb),_fogyasztViz(fogyasztViz)
        {
            if (tank > tankMax) _tank = tank > tankMax ? tankMax : tank;
        }

        void halad(Terep* talajTipus) { talajTipus->halad(this); }

        bool el() const { return _tank > 0; }
        void elFogyaszt( double l) { _tank -= l; if (_tank < 0) _tank = 0; }

        virtual std::string getTipus() const = 0;
        std::string getNev() const { return _nev; }
        double getTank() const { return _tank; }
        double getHomokFogyasztas() const { return  _fogyasztHomok; }
        double getDombFogyasztas() const { return  _fogyasztDomb; }
        double getVizFogyasztas() const { return  _fogyasztViz; }
        double getTankSzazalek () { return _tank / _tankMax; }

    protected:
        std::string _nev;
        double _tank;
        const double _tankMax;
        double _fogyasztHomok;
        double _fogyasztDomb;
        double _fogyasztViz;
};

class Kamion : public Jarmu
{
    public:
        Kamion(std::string nev,double tank, int fogyasztHomok = 25, int fogyasztDomb = 30, int fogyasztViz = 35)
            :Jarmu(nev, tank, 1500, fogyasztHomok, fogyasztDomb, fogyasztViz) { }
        std::string getTipus() const override { return "kamion"; }
};

class SzemelyAuto : public Jarmu
{
    public:
        SzemelyAuto(std::string nev,double tank, int fogyasztHomok = 10, int fogyasztDomb = 12, int fogyasztViz = 13)
            :Jarmu(nev, tank, 70, fogyasztHomok, fogyasztDomb, fogyasztViz) { }
        std::string getTipus() const override { return "szemelyauto"; }
};

class Motor : public Jarmu
{
    public:
        Motor(std::string nev,double tank, int fogyasztHomok = 7, int fogyasztDomb = 9, int fogyasztViz = 10)
            :Jarmu(nev, tank, 50, fogyasztHomok, fogyasztDomb, fogyasztViz) { }
        std::string getTipus() const override { return "motor"; }
};
