#include "list.h"

int list::counter = 0;

list::list(int val, list* n)
{
    value = val;
    next = n;
    list::counter++;
}

list::list(const list& other)
{
    value = other.value;
    if(other.next!=0)
    {
        next = new list(*other.next);
    }
}

list& list::operator=(const list& other)
{
    if(this == &other)
    {
        return *this;
    }
    //if(this != &other)
    else
    {
        delete next;
        value = other.value;
        if(other.next!=0)
        {
            next = new list(*other.next);
        }
        return *this;
    }
}

list::~list()
{
    delete next;
    list::counter--;
}

int list::get_counter()
{
    return counter;
}

void list::set_value(int val)
{
    value = val;
}

int list::get_value() const
{
    return value;
}

list* list::get_next() const
{
    return next;
}
