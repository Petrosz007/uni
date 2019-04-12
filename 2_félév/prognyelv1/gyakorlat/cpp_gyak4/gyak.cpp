#include <iostream>
#include <vector>
#include <algorithm>
#include <numeric>
#include <string>

class User
{
    int id;
    std::string name;
public:
    User(int i=0, std::string n="") : id(i), name(n) {
        name = n;
     }
    
    int getId() const
    {
        return id;
    }
    std::string getName() const
    {
        return name;
    }

    User& operator++()
    {
        ++id;
        return *this;
    }

    bool operator<(const User& u)
    {
        return id < u.id;
    }

    bool operator==(const User& u)
    {
        return id == u.id;
    }
    
    bool operator!=(const User& u)
    {
        return !(*this==u);
    }
};

std::ostream& operator<<(std::ostream& out, const User& u)
{
    out << u.getId() << "-" << u.getName() << " ";
    return out;
}


struct MyFunctor
{
    bool operator()(User& i)
    {
        return i.getId() % 2 == 0;
    }
};



int main()
{
/*
    std::vector<int> v(100);

    std::vector<int>::iterator it = v.begin();
    std::advance(it, 5);
    std::iota(it, v.end(), 0);
*/
    std::vector<User> u(50);
    //User user(1, "asdfgh");
    User defaultUser(0, "asdfgh");
    std::iota(u.begin(), u.end(), defaultUser);
    
    std::reverse(u.begin(), u.end());

    std::sort(u.begin(), u.end());
//    std::shuffle(u.begin(), u.end());
/*
    int count = 0;
    for(auto it = u.begin(); it != u.end(); ++it)
    {
        if(*it == defaultUser)
        {
            ++count;
        }
    }
*/
//    int count = std::count(u.begin(), u.end(), defaultUser);
 int count = std::count_if(u.begin(), u.end(),
        //[](User& u){ return u.getId() % 2 == 0; }
        MyFunctor()
     );
    std::cout << "Count: " << count << std::endl;

    auto elemIt = std::find(u.begin(), u.end(), User(52));
    if(elemIt == u.end())
    {
        std::cout << "not found";
    }
    else
    {
        std::cout << *elemIt << std::endl;
    }
/*
    for(auto it = u.begin(); it != u.end(); ++it)
    {
        std::cout << *it << " ";
    }
*/
    std::endl(std::cout);


/*
    v.push_back(101);


    for(int i=0;i<v.size();++i)
    {
        std::cout << v[i] << " ";
    }

    for(std::vector<int>::iterator it = v.begin();
            it != v.end();
            ++it)
    {
           std::cout << *it << " ";
    }

*/
    return 0;
}
