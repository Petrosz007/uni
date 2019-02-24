// Building AVL trees - illustration.
// Ásványi Tibor, ELTE IK; January 2007; October, November 2008.

#include <stdio.h>
#include <stdlib.h>

typedef struct Node *Tree;

struct Node
{
  int data;
  int balance;  // the balance of the Node
  Tree left, right;
};

Tree new_Node( int data ) {
  Tree t = (Tree)malloc(sizeof(struct Node)); 
  t->data = data; t->balance = 0; t->left = t->right = NULL;
  return t;
}

// A fa Inorder kiiratása, a nemüres részfákat bezárójelezve, 
// a csúcsok "adat:egyensúly" formátumban. Az üres részfákat elhagyja.

// Inorder print of the tree.
// The nonempty (sub)trees are bracketed.
// The format of a Node is "data:balance".
// The empty (sub)trees are not printed.
void inorder_(Tree t){
  if (t!=NULL){
    printf( " (" );
    inorder_(t->left);
    printf( "%d:%d", t->data, t->balance );
    inorder_(t->right);
    printf( ") " );
  }
}

// A fa Inorder kiiratása, egy sorba, a végén újsor.

// Inorder print of the tree. Printed into a single line, 
// with a "newline" at the end.
void Inorder(Tree t){
  inorder_(t);
  printf( "\n" );
}

void balanceMM(Tree *t,Tree b){
  (*t)->left=b->right;
  b->right=*t;
  (*t)->balance=0;
  b->balance=0;
  *t=b;
}

void balanceMP(Tree *t,Tree b){
  Tree p=b->right;
  (*t)->left=p->right;
  b->right=p->left;
  p->right=*t;
  p->left=b;
  if (p->balance==-1){
    (*t)->balance=1; 
    b->balance=0;
  }else if (p->balance==0){
    (*t)->balance=0;
    b->balance=0;
  }else if (p->balance==1){
    (*t)->balance=0;
    b->balance=-1;
  }
  p->balance=0;
  *t=p;
}

void balancePM(Tree *t,Tree j){
  Tree p=j->left;
  (*t)->right=p->left;
  j->left=p->right;
  p->left=*t;
  p->right=j;
  if (p->balance==-1){
    (*t)->balance=0;
    j->balance=1;
  }else if (p->balance==0){
    (*t)->balance=0;
    j->balance=0;
  }else if (p->balance==1){
    (*t)->balance=-1;
    j->balance=0;
  }
  p->balance=0;
  *t=p;
}

void balancePP(Tree *t,Tree j){
  (*t)->right=j->left;
  j->left=*t;
  (*t)->balance=j->balance=0;
  *t=j;
}

// insert_(&t,x,&h): Beszúrja t-be x-et, ha még t-ben nem volt x.
// Ha t kiegyensúlyozott volt, az is marad.
// h =  1, ha sikeres volt a beszúrás, és t magasabb lett,
// h =  0, ha sikeres volt a beszúrás, de t nem lett magasabb,
// h = -1, ha t már tartalmazta x-et (és így t nem változott).

// insert_(&t,x,&h): Insert x into t, if t does not contains x.
// A balanced tree remains balanced.
// h =  1, if the insert was successful and t has become higher.
// h =  0, if the insert was successful and t has not become higher.
// h = -1, ha t has already contained x (and so the tree has not changed).
void insert_(Tree *t, int x, int *h){
  if (*t==NULL){
      *t=new_Node(x);
      *h=1;
  }else{
    if ( x == (*t)->data ) *h = -1;
    else if ( x < (*t)->data ) {
      insert_(&(*t)->left,x,h);
      if (*h>0) {
	if ((*t)->balance==-1){					
	  Tree b=(*t)->left;
	  if (b->balance==-1) balanceMM(t,b);
	  else balanceMP(t,b); // case ( b->balance == +1 )
	  *h=0;
	}else *h = -(--(*t)->balance);
      }
    }else{ // case ( (*t)->data < x )
      insert_(&(*t)->right,x,h);
      if (*h>0) {
	if ((*t)->balance==1){
	  Tree j=(*t)->right;
	  if (j->balance==-1) balancePM(t,j);
	  else balancePP(t,j); // case ( j->balance == +1 )
	  *h=0;
	}else *h = ++(*t)->balance; 
      }
    }
  }
}

// Beszúrja a t, kiegyensúlyozott keresõfába x-et, ha még nem tartalmazta,
// és igaz logikai értékkel tér vissza, különben hamissal.
// Ha t kiegyensúlyozott volt, az is marad.

// Insert(&t,x): Insert x into t, if t does not contains x.
// It returns "true", iff t did not contain x.
// A balanced tree remains balanced.
int Insert(Tree *t, int x) {
  int h;
  insert_(t,x,&h); 
  return h>=0;
}

/////////////////////////////////////

int main()
{
  Tree root=NULL;
  int n;
  printf("The integers to be inserted? (an alphabetic char at the end)\n");
  while(scanf("%d",&n)){       
    if( Insert(&root,n) ) Inorder(root);
    else printf("The number %d was already in the tree.\n",n);
  }
  return 0;
}
