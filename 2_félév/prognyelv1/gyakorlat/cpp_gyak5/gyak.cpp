#include <iostream>
#include <vector>

class Animal
{
    public:
    Animal(){
        std::cout << "Animal ctr" << std::endl;
    }
    virtual ~Animal(){
        std::cout << "Animal dtr" << std::endl;
    }

    virtual Animal* clone() = 0;

    int id;
    virtual void say_something()
    {
        std::cout << "I'm an animal." << std::endl;
    }    

    virtual void f() const
    {
        std::cout << "Animal::f()" << std::endl;
    }
};

class Dog : public Animal
{
    
    int id2;
    public:
    Dog(){
            std::cout << "Dog ctr" << std::endl;
    }
    ~Dog(){
            std::cout << "Dog dtr" << std::endl;
    }
    void say_something()
    {
        std::cout << "I'm a dog." << std::endl;
    }

    
    //Animal*
    Dog* clone() override
    {
        return new Dog(*this);
    }

    void bark()
    {
        std::cout << "woahf" << std::endl;
    }
    void f() const override
    {
        std::cout << "Dog::f()" << std::endl;
    }
};

class Goat : public Animal
{
    public:
    void say_something()
    {
        std::cout << "I'm a goat." << std::endl;
    }  
};



int main()
{
/*
    Animal a;
    a.say_something();

    Dog d;
    d.say_something();


    std::vector<Animal*> v;
    v.push_back(&a);
    v.push_back(&d);
*/

    Animal* aptr = new Dog();

    if(Goat *d = dynamic_cast<Goat*>(aptr))
    {
        //d->bark();
    }
    
    if(auto d = dynamic_cast<Dog*>(aptr))
    {
        d->bark();
    }

    Animal* otherptr = new Dog();
    otherptr->f();


    Animal* other_dog = aptr->clone();
    other_dog->say_something();
    std::vector<Animal*> vec;
    vec.push_back(aptr);
    vec.push_back(otherptr);
    vec.push_back(other_dog);

    for(auto ptr : vec)
    {
        ptr->say_something();
    }

    delete aptr;
    //aptr-> say_something();
/*    
    int type;
    std::cin >> type;
    if(type==1)
    {
        aptr = new Dog();   
    }
    else
    {
        aptr = new Goat();
    }

    aptr->say_something();
*/
    //Dog* d1 = &v[1];//.say_something();
 
    return 0;
}


