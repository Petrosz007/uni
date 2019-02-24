// This program tests class list (list.h, list.cpp).
// Author: Ásványi, Tibor; Budapest, November 2001.

#include <stdio.h>
#include "list.h"

void skip_line()
{
  while(getchar()!='\n');
}

void help()
{
  printf("f  : L.first()        ;   F  : LL.first()     ;\n"); 
  printf("n  : L.next()         ;   N  : LL.next()      ;\n"); 
  printf("e  : L.end()          ;   E  : LL.end()       ;\n"); 
  printf("y  : L.empty()        ;   Y  : LL.empty()     ;\n"); 
  printf("ic : L.ins(c)         ;   Ic : LL.ins(c)      ;\n"); 
  printf("r  : L.rem()          ;   R  : LL.rem()       ;\n"); 
  printf("pc : L.put(c)         ;   Pc : LL.put(c)      ;\n"); 
  printf("g  : L.get()          ;   G  : LL.get()       ;\n"); 
  printf("d  : L.del_tail()     ;   D  : LL.del_tail()  ;\n"); 
  printf("m  : L.move(LL)       ;   M  : LL.move(L)     ;\n"); 
  printf("c  : L.change_tail(LL);   z  : exit this test ;\n"); 
}

int main()
{
  printf("Type 'h' for help.\n");
  char c;
  int actpos=1, ActPos=1;
  list L,LL;
  while((c=getchar())!='z')
    { 
      if( c=='\n' || c==' ' || c=='\t') continue;
      switch(c)
	{
	case 'f': 
	  L.first(); actpos=1;
	  printf("actpos=%i\n",actpos);
	  break;
	case 'F': 
	  LL.first(); ActPos=1;
	  printf("ActPos=%i\n",ActPos);
	  break;
	case 'n':
	  try
	    {
	      L.next(); actpos++;
	      printf("actpos=%i\n",actpos);
	    }
	  catch(list::end_error)
	    {
	      printf("end_error\n");
	    }
	  break;
	case 'N':
	  try
	    {
	      LL.next(); ActPos++;
	      printf("ActPos=%i\n",ActPos);
	    }
	  catch(list::end_error)
	    {
	      printf("end_error\n");
	    }
	  break;
	case 'e':
	  printf("%s\n",L.end()?"yes":"no");
	  break;
	case 'E':
	  printf("%s\n",LL.end()?"yes":"no");
	  break;
	case 'y':
	  printf("%s\n",L.empty()?"yes":"no");
	  break;
	case 'Y':
	  printf("%s\n",LL.empty()?"yes":"no");
	  break;
	case 'i': 
	  while((c=getchar())=='\n');
	  try
	    {
	      L.ins(c); 
	      printf("L="); L.print_list(); 
	    } 
	  catch(list::mem_full)
	    {
	      printf("mem_full\n");
	    }
	  break;
	case 'I': 
	  while((c=getchar())=='\n');
	  try
	    {
	      LL.ins(c); 
	      printf("LL="); LL.print_list(); 
	    } 
	  catch(list::mem_full)
	    {
	      printf("mem_full\n");
	    }
	  break;
	case 'r':
	  try
	    {
	      L.rem();
	      printf("L="); L.print_list(); 
	    }
	  catch(list::end_error)
	    {
	      printf("end_error\n");
	    }
	  break;
	case 'R':
	  try
	    {
	      LL.rem();
	      printf("LL="); LL.print_list(); 
	    }
	  catch(list::end_error)
	    {
	      printf("end_error\n");
	    }
	  break;
	case 'p':
	  while((c=getchar())=='\n');
	  try
	    {
	      L.put(c); 
	      printf("L="); L.print_list();  
	    }
	  catch(list::end_error)
	    {
	      printf("end_error\n");
	    }
	  break;
	case 'P':
	  while((c=getchar())=='\n');
	  try
	    {
	      LL.put(c); 
	      printf("LL="); LL.print_list();  
	    }
	  catch(list::end_error)
	    {
	      printf("end_error\n");
	    }
	  break;
	case 'g':
	  try
	    {
	      printf("L.get()=%c\n",L.get());
	    }
	  catch(list::end_error)
	    {
	      printf("end_error\n");
	    }
	  break;
	case 'G':
	  try
	    {
	      printf("LL.get()=%c\n",LL.get());
	    }
	  catch(list::end_error)
	    {
	      printf("end_error\n");
	    }
	  break;
	case 'd':
	  L.del_tail();
	  printf("L="); L.print_list();
	  break;
	case 'D':
	  LL.del_tail();
	  printf("LL="); LL.print_list();
	  break;
	case 'm':
	  try
	    {
	      L.move(LL);
	      printf("L="); L.print_list();
	      printf("LL="); LL.print_list();
	    }
	  catch(list::end_error)
	    {
	      printf("end_error\n");
	    }
	  break;
	case 'M':
	  try
	    {
	      LL.move(L);
	      printf("L="); L.print_list();
	      printf("LL="); LL.print_list();
	    }
	  catch(list::end_error)
	    {
	      printf("end_error\n");
	    }
	  break;
	case 'c':
	  L.change_tail(LL);
	  printf("L="); L.print_list();
	  printf("LL="); LL.print_list();
	  break;
	case 'h':
	  help();
	  break;
	default: 
	  skip_line();
	  printf( "Sorry : %c ?\n", c ); 
	  help();
	  break;
	}     
    }
  printf("--bye\n");
  return 0;
}
