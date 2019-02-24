// This program implements algorithm insertsort using class list2.
// Author: Ásványi, Tibor; Budapest, 2003.

#include <iostream>
#include "list2.h"

using namespace std;

struct node: public list2node
{
    char data;
    node(char c):data(c){};
};

typedef class list2node *list;

void build_list(list& L)
{ 
    char c;
    L = new list2node;
    while((c=cin.get())!='\n') (new node(c)) -> precede(L);
}

void sorted_insert( list L, node *p, node *b )
{
    while( p != L && b->data < p->data ) p = (node*)p->pre();
    b->follow(p);
}

void sort_list(list L) // InsertSort
{
    list2node *e = L->suc(); // pointer to the end of sorted part.
    list2node *b = e->suc(); // pointer to the beginning of unsorted part.
    while(b!=L)
    {
	if( ((node*)b)->data < ((node*)e)->data )
	    sorted_insert(L,(node*)e->pre(),(node*)b);
	else e = b;
	b = e->suc();
    }
}

void print_list(list L)
{
    for( list2node *p = L->suc(); p!=L; p = p->suc() ) 
	cout << ((node*)p)->data;
    putchar('\n'); 
}

int main()
{
  list L;
  printf("Type in the line to be sorted, please!\n");
  build_list(L);
  printf("Your input:\n");
  print_list(L);
  sort_list(L);
  printf("A crazy thing: Your characters sorted:\n");
  print_list(L);
  return 0;
}
