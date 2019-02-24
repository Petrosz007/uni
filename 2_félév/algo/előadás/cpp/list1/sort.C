// This program implements mergesort eliminating duplications.
// We use class list.
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

int length(list& L)
{
  int len=0;
  L.first();
  while( !L.end() ) 
    {
      L.next();
      len++;
    } 
  return len;
}

void mergesort(list& L, int n)
{
  if(n>1)
    {
      // Divide the list.
      int m=n/2;
      list LL;
      L.first();
      for(int i=0;i<m;i++) L.next();
      L.change_tail(LL);
      // Sort its parts.
      mergesort(L,m);
      mergesort(LL,n-m);
      // Sorted union of LL into L.
      L.first();
      LL.first();
      while( !L.end() && !LL.empty() )
	{
	  if(L.get()==LL.get()) LL.rem();
	  else if(L.get()>LL.get()) LL.move(L);
	  L.next();
	}
      if(!LL.empty()) L.change_tail(LL);
    }
}

void sort_list(list& L) // MergeSort
{
  int n=length(L);
  mergesort(L,n);
}

int main()
{
  list L;
  printf("Type in the line to be sorted, please!\n");
  build_list(L);
  printf("Your input line:\n");
  L.print_list();
  sort_list(L);
  printf("Your input characters:\n");
  L.print_list();
  return 0;
}
