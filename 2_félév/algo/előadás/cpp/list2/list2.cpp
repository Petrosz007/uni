// list2.cpp
// Author: Ásványi Tibor, 2003. Budapest

#include "list2.h"

list2node::list2node(){
  prev = next = this;
}

list2node::~list2node(){
  prev->next = next;
  next->prev = prev;
}

void list2node::precede(list2node *p){
    if( p==this ) 
	throw PrecedeItselfErr;
    else{
	prev->next = next;
	next->prev = prev;
	next = p;
	prev = p->prev;
	p->prev->next = this;
	p->prev = this;
    } 
}

void list2node::follow(list2node *p){
    if( p==this ) 
	throw FollowItselfErr; 
    else{
	prev->next = next;
	next->prev = prev;
	prev = p;
	next = p->next;
	p->next->prev = this;
	p->next = this;
    } 
}
    

list2node *list2node::pre() const{
  return prev;
}

list2node *list2node::suc() const{
  return next;
}

void list2node::out(){
  prev->next = next;
  next->prev = prev;
  prev = next = this;
}
