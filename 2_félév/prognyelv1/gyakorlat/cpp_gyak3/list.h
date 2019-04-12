#ifndef LIST_H
#define LIST_H

//RAII
class list
{
    int value;
    list* next;
    static int counter;
    
    public:
    static int get_counter();

    list(int val, list* n=0);
    list(const list& other);
    list& operator=(const list& other);
    ~list();

    void set_value(int val);
    void print() const;

    int get_value() const;
    list* get_next() const;       
};

#endif
    
