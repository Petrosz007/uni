#include "terep.h"
#include "jarmu.h"

Homok* Homok::_peldany = nullptr;
Homok* Homok::peldany()
{
    if(_peldany == nullptr) {
        _peldany = new Homok();
    }
    return _peldany;
}

void Homok::halad(Jarmu* p)
{
    p->elFogyaszt((p->getHomokFogyasztas() + p->getTankSzazalek() * p->getHomokFogyasztas() / 10) / 10);
}

Domb* Domb::_peldany = nullptr;
Domb* Domb::peldany()
{
    if(_peldany == nullptr) {
        _peldany = new Domb();
    }
    return _peldany;
}

void Domb::halad(Jarmu* p)
{
    p->elFogyaszt((p->getDombFogyasztas() + p->getTankSzazalek() * p->getDombFogyasztas() / 10 ) / 10);
}

Viz* Viz::_peldany = nullptr;
Viz* Viz::peldany()
{
    if(_peldany == nullptr) {
        _peldany = new Viz();
    }
    return _peldany;
}

void Viz::halad(Jarmu* p)
{
    p->elFogyaszt((p->getVizFogyasztas() + p->getTankSzazalek() * p->getVizFogyasztas() / 10) / 10);
}
