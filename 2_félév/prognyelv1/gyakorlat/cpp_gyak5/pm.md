### 1. kérdés

Mutasd be röviden (pár mondatban) az iterátorokat (mire jó, miért jó, hol használjuk)!

### 2. kérdés
Ahhoz, hogy az alábbi kódrészlet leforduljon, két operátort kell definiálni a User osztályon, egyet az `std::sort(..)`, egyet pedig az `std::find(..)` működéséhez. Módosítsd az osztályt, hogy (helyesen) működjön a kód!
```
#include <algorithm>
#include <vector>
struct User{
    int id;
    User(int i=0) : id(i){}
};

int main()
{
    std::vector<User> v;
    std::sort(v.begin(), v.end());
    User u(5);
    auto it = std::find(v.begin(), v.end(), u);
}

```
