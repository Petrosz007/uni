#include <iostream>

class Base
{
    private:
        int secret_field;
    protected:
        int prot_field;
    public:
        int pub_field;
    Base(){
        std::cout << "Base::cons()" << std::endl;
    }

    virtual ~Base(){
        std::cout << "Base::dest()" << std::endl;
    }

    virtual void f() const
    {
        std::cout <<"Base::f()" << std::endl;
    }

    virtual Base* clone() = 0;
};

class InhPriv : private Base
{
    public:
    InhPriv(){
        std::cout << "InhPriv::InhPriv()" << std::endl;
    }

    ~InhPriv(){
        std::cout << "InhPriv::~InhPriv()" << std::endl;
    }

};

class InhProt : protected Base
{


};

class InhPub : public Base
{


};


class Inherited : public Base
{
    public:
    Inherited()
    {
        std::cout << "Inherited::cons()" << std::endl;
    }
    ~Inherited()
    {
        std::cout << "Inherited::dest()" << std::endl;
    }

    Inherited* clone() override
    {
        return new Inherited(*this);
    }

    void f() const override
    {
        Base::f();
        std::cout <<"Inherited::f()" << std::endl;
    }

    void g() const
    {
        std::cout <<"Inherited::g()" << std::endl;
    }


};


int main()
{
/*
    Base b;
    InhPriv ipriv;
    InhProt iprot;
    InhPub ipub;
*/
    Base* bp;
//    const Base* bp = new Inherited(); 
// ha lemarad a const() az f->ről akkor mi hívódik meg és miért?
    bp = new Inherited();
    
    bp->f();

    if(Inherited* i = dynamic_cast<Inherited*>(bp))
    {
        i->g();
    }    

    Base* copy = bp->clone();
//    Base b; // nem jó, mivel a Base-ben van pure virtual method, emiatt nem példányosítható belőle objektum - mi történne, ha a b.f(); hívást végeznénk? nincs ilyen függvény definiálva, ezért nem legális a hívás tehát nem fordul le
    delete bp;
    //bp = new InhProt();
    /*
        ->privát/protected öröklődés miatt ez illegális, mivel
        változna az egyes adattagok láthatósága.
        A Base osztályban ami public volt a bp típusa miatt
        továbbra is el kéne tudni érni, hiszen a Base típusban
        public, de a dinamikus típus miatt ez már nem teljesül,
        emiatt az ilyet nem engedi a C++
    */
    return 0;
}
