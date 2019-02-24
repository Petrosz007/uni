// Copyright: Ásványi, Tibor; Budapest, November 2001.
// Minden jog fenntartva.

#include <new>
#include <stdio.h>
#include "list.h"

void print_T(char C)
{
  putchar(C);
}

struct lnode
{
  T data;
  lnode *link;
};

list::list()
{
  L=NULL;
  act=&L;
}

list::~list()
{
  lnode *p;
  while(L)
    { 
      p=L;
      L=L->link;
      delete []p;
    }
}

void list::first()
{
  act=&L;
}

void list::next()
{
  if(*act!=NULL) act=&(*act)->link;
  else throw end_error();
}

bool list::end()
{
  return *act==NULL;
}

bool list::empty()
{
  return L==NULL;
}

void list::ins(T x)
{
  lnode *p = new(std::nothrow) lnode;
  if(p==NULL) throw mem_full();
  p->data=x;
  p->link=*act;
  *act=p;
}

void list::rem()
{
  lnode *p=*act;
  if(p==NULL) throw end_error();
  *act=p->link;
  delete []p;
}

void list::put(T x)
{
  if(*act!=NULL) (*act)->data=x;
  else throw end_error();
}

T list::get()
{
  if(*act!=NULL) return (*act)->data;
  else throw end_error();
}

void list::del_tail()
{
  lnode *p,*q=*act;
  *act=NULL;
  while(q!=NULL)
    { 
      p=q;
      q=q->link;
      delete []p;
    }
}

void list::move(list& LL)
{
  lnode *p=*act;
  if(p==NULL) throw end_error();
  *act=p->link;
  p->link=*LL.act;
  *LL.act=p;
}

void list::change_tail(list& LL)
{
  lnode *p=*LL.act;
  *LL.act=*act;
  *act=p;
}

void list::print_list()
{
  lnode *p=L;
  while(p!=NULL)
    {
      print_T(p->data);
      p=p->link;
    }
  putchar('\n');
}
