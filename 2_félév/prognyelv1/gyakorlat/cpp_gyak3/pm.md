### 1. kérdés
Mit ír ki az alábbi kódrészlet?

```
#include <iostream>

struct Foo
{
    int x;
    Foo(int i=3)
    {
        x=i;
    }
    void modify(int x) const
    {
        x=x;
    }
    void print() const
    {
          std::cout <<"'x' value is: " << x << std::endl;
    }
};

int main()
{
    Foo p(1);
    p.modify(3);
    p.print();

    const Foo r;
    r.modify(7);
    r.print();

    return 0;
}
```

 - A: `'x' value is: 1 / 'x' value is: 3`
 - B: `'x' value is: 3 / 'x' value is: 7`
 - C: `'x' value is: 1 / 'x' value is: 7`
 - D: A program nem is fordul le, mert az `r` változót `const`-ként definiáltuk, így a `Foo::modify(int)` hívás nem értelmezhető rajta, mivel az megváltoztatná az `x` értékét, ami sérti a *const-correctness* elvét, így fordítási hibát kapunk.


### 2. kérdés

A fenti struktúra-definíció alapján minek felel meg a `Foo q = 5;` utasítás?

 - A: Copy konstruktor hívás
 - B: Implicit konstruktor hívás
 - C: Értékadó operátor hívás
 - D: Egyiknek sem, ez fordítási hibát okozna.



Megoldás egyszer tölthető fel, módosításra nincs lehetőség. Mindenhol egy, és pontosan egy jó válasz van.

Jó munkát!

<hr/>


