// list2.h
// Author: Ásványi Tibor, 2003. Budapest
// Handling of cyclic two-way lists.

// If the pointers (this and/or p) have undefined or 0 value, 
// the effect of the following operations is undefined.
// For example, if we call q->follow(p), q and p must refer to list2nodes.

// if q==p in the call above, exception FollowItselfErr is raised.
// if q==p in the call q->precede(p), exception PrecedeItselfErr is raised.

#ifndef __LIST2_H
#define __LIST2_H

class list2node{

 public:
    enum Exceptions{ PrecedeItselfErr, FollowItselfErr };
    
    list2node(); // *this becomes a cyclic list of a single element
    virtual ~list2node(); // *this will be deleted (first it calls out())

    void precede(list2node *p); // *this will precede *p (first it calls out())
    // if this==p, the appropriate exception is raised
    void follow(list2node *p); // *this will follow *p (first it calls out())

    list2node *pre() const; // return the address of the previous element
    list2node *suc() const; // return the address of the successor element

    void out(); // *this is detached, it becomes a cyclic list itself

 protected:
    list2node(const list2node &item);
    list2node &operator=(const list2node &item);

 private:
    list2node *prev, *next;
};

#endif

//////////////////////////////////////////////////////////////////////////

