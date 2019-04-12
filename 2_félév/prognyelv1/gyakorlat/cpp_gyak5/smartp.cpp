#include <iostream>

class Counter
{
private:
    int m_count;

public:
    void inc()
    {
        ++m_count;
    }
    int dec()
    {
        return --m_count;
    }
    int get() const
    {
        return m_count;
    }
};

template <typename T>
class my_smart_ptr
{
private:
    T* m_obj{ nullptr };
    Counter* m_counter{ nullptr };

public:
    my_smart_ptr()
    {
    }

    my_smart_ptr(T* object)
        : m_obj(object)
        , m_counter(new Counter())
    {
        m_counter->inc();
        std::cout << "Created smart_ptr! Ref count is " << m_counter->get() << std::endl;
    }

    // Destructor
    virtual ~my_smart_ptr()
    {
        if (m_counter)
        {
            int decrementedCount = m_counter->dec();
            std::cout << "Destroyed smart_ptr! Ref count is " << decrementedCount << std::endl;
            if (decrementedCount <= 0)
            {
                delete m_counter;
                delete m_obj;
                m_counter = nullptr;
                m_obj = nullptr;
            }
        }
    }

    // Copy Constructor
    my_smart_ptr(const my_smart_ptr<T>& other)
        : m_obj{ other.m_obj }
        , m_counter{ other.m_counter }
    {
        m_counter->inc();
        std::cout << "Copied smart_ptr! Ref count is " << m_counter->get() << std::endl;
    }

    // Overloaded Assignment Operator
    my_smart_ptr<T>& operator=(const my_smart_ptr<T>& other)
    {
        if (this != &other)
        {
            if (m_counter && m_counter->dec() == 0)
            {
                delete m_counter;
                delete m_obj;
            }
            m_obj = other.m_obj;
            m_counter = other.m_counter;
            if(m_counter)
            {
                m_counter->inc();
            }
        }
        std::cout << "Assigning smart_ptr! Ref count is " << (m_counter ? m_counter->get() : 0) << std::endl;
        return *this;
    }

    // Dereference operator
    T& operator*()
    {
        return *m_obj;
    }

    // Member Access operator
    T* operator->()
    {
        return m_obj;
    }

    // Reference count
    int get_count() const
    {
        return m_counter->get();
    }
};

struct Point
{
    int x, y;
    Point(int xx = 0, int yy = 0)
        : x(xx)
        , y(yy)
    {
    }
};

int main()
{
    my_smart_ptr<Point> p(new Point(5, 6));
    my_smart_ptr<Point> r(new Point(8, 9));

    my_smart_ptr<Point> s;
    my_smart_ptr<Point> w;
    s = w;
    {
        my_smart_ptr<Point> q(p);
    }
    p = r;
    s = r;

    return 0;
}
