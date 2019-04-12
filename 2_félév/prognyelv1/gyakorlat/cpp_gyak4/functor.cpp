#include <iostream>
#include <vector>
#include <numeric>
#include <algorithm>

bool f(const int i)
{
	return i%2 == 0;
}
struct MyFunc
{
	bool operator()(int i)
	{
		return i%2 == 0;
	}
};

struct MyCounter
{
	int val;
	MyCounter(int v) : val(v){}
	bool operator()(int i)
	{
		return (i==val);
	}
};

struct User
{
	int age;
	
	User(int a=0) : age(a) {}
	User operator++()
	{
		return User(age+1);
	}
	bool operator<(const User& other) const
	{
		return age < other.age;
	}
	
	bool operator ==(const User& other) const
	{
		return age == other.age;
	}
};

struct Printer
{	
	void operator()(int i)
	{
		std::cout << i << " ";
	}
	void operator()(User& u)
	{
		std::cout << u.age << " ";
	}
};


int main()
{
	std::vector<User> u;
	u.push_back(User(42));
	u.push_back(User(32));
	u.push_back(User(57));
	u.push_back(User());
	u.push_back(User(36));
	u.push_back(User(36));
	
	std::for_each(u.begin(), u.end(), Printer());
	std::endl(std::cout);
	std::for_each(u.begin(), u.end(), [](const User& u){std::cout << u.age <<" ";});
	std::cout << std::endl;
	std::sort(u.begin(), u.end());
	std::for_each(u.begin(), u.end(), Printer());
	std::cout << std::endl;
	
	std::cout << "36: " << std::count(u.begin(), u.end(), User(36)) << " times" << std::endl; 
	
	
	std::vector<int> v(50);
	std::iota(v.begin(), v.end(), 0);
		
	int count = 0;
	for(int i=0;i<v.size();++i)
	{
		if(v[i] % 2 == 0)
		{
			++count;
		}
	}
	
	std::cout << "Even elements: " << count << std::endl;
	
	MyFunc mf;
	count = std::count_if(v.begin(), v.end(), mf);
	std::cout << "Even elements: " << count << std::endl;

	MyCounter mc(5);
	count = std::count_if(v.begin(), v.end(), mc);
	std::cout << "5 found: " << count << std::endl;
	
	auto it = std::find(v.begin(), v.end(), 5);
	if(it != v.end())
	{
		std::cout << *it << std::endl;
	}
	
	it = std::find_if(v.begin(), v.end(), mf);
	if(it != v.end())
	{
		std::cout << *it << std::endl;
	}
	
	std::for_each(v.begin(), v.end(), Printer());
	std::endl(std::cout);
	return 0;
}