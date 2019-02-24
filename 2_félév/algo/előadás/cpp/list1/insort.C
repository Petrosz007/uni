// This program implements algorithm insertsort using class list.
// Author: Ásványi, Tibor; Budapest, November 2001.

#include <stdio.h>
#include "list.h"

void build_list(list& L)
{ 
  char c;
  while((c=getchar())!='\n')
    {
      L.ins(c);
      L.next();
    }
}

void overwrite(list& L)
{
  char c;
  L.first();
  while( !L.end() && (c=getchar())!='\n' )
    {
      L.put(c);
      L.next();
    }
  if(L.end()) build_list(L);
  else L.del_tail();
}

void sorted_move(list& L,list& LL)
{
  T x=L.get();
  LL.first();
  while( !LL.end() && x>LL.get() ) LL.next(); 
  L.move(LL);
}

void sort_list(list& L) // InsertSort
{
  list LL;
  L.first(); 
  while(!L.empty()) sorted_move(L,LL);
  LL.first();
  L.change_tail(LL);
}

int main()
{
  list L;
  printf("Type in the line to be sorted, please!\n");
  build_list(L);
  printf("Your input:\n");
  L.print_list();
  sort_list(L);
  printf("A crazy thing: Your characters sorted:\n");
  L.print_list();
  printf("Please, write over this crazy output!\n");
  overwrite(L);
  printf("You are great! The result:\n");
  L.print_list();
  return 0;
}
