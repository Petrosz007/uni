// Building AVL trees - illustration.
// Ásványi Tibor, ELTE IK, Budapest; January 2007; October, November 2008.

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
    if(t!=NULL) inorder_(t);
    else printf("NULL");
    printf( "\n" );
}

void balanceMM(Tree *pt,Tree left){
    Tree t = *pt;
    t->left=left->right;
    left->right=t;
    t->balance=0;
    left->balance=0;
    *pt=left;
}

void balanceMZ(Tree *pt,Tree left){
    Tree t = *pt;
    t->left=left->right;
    left->right=t;
    t->balance=-1;
    left->balance=1;
    t=left;
}

void balanceMP(Tree *pt,Tree left){
    Tree t = *pt;
    Tree p=left->right;
    t->left=p->right;
    left->right=p->left;
    p->right=t;
    p->left=left;
    if (p->balance==-1){
	t->balance=1; 
	left->balance=0;
    }else if (p->balance==0){
	t->balance=0;
	left->balance=0;
    }else if (p->balance==1){
	t->balance=0;
	left->balance=-1;
    }
    p->balance=0;
    *pt=p;
}

void balancePM(Tree *pt,Tree right){
    Tree t = *pt;
    Tree p=right->left;
    t->right=p->left;
    right->left=p->right;
    p->left=t;
    p->right=right;
    if (p->balance==-1){
	t->balance=0;
	right->balance=1;
    }else if (p->balance==0){
	t->balance=0;
	right->balance=0;
    }else if (p->balance==1){
	t->balance=-1;
	right->balance=0;
    }
    p->balance=0;
    *pt=p;
}

void balancePZ(Tree *pt,Tree right){
    Tree t = *pt;
    t->right=right->left;
    right->left=t;
    t->balance=1;
    right->balance=-1;
    *pt=right;
}

void balancePP(Tree *pt,Tree right){
    Tree t = *pt;
    t->right=right->left;
    right->left=t;
    t->balance=right->balance=0;
    *pt=right;
}

// insert_(&t,x,&d): Beszúrja t-be x-et, ha még t-ben nem volt x.
// Ha t kiegyensúlyozott volt, az is marad.
// d =  1, ha sikeres volt a beszúrás, és t magasabb lett,
// d =  0, ha sikeres volt a beszúrás, de t nem lett magasabb,
// d = -1, ha t már tartalmazta x-et (és így t nem változott).

// insert_(&t,x,&d): Insert x into t, if t does not contains x.
// A balanced tree remains balanced.
// d =  1, if the insert was successful and t has become higher.
// d =  0, if the insert was successful and t has not become higher.
// d = -1, ha t has already contained x (and so the tree has not changed).
void insert_(Tree *pt, int x, int *pd){
    if (*pt==NULL){
	*pt=new_Node(x);
	*pd=1;
    }else if ( x < (*pt)->data ) {
	insert_(&(*pt)->left,x,pd);
	if (*pd>0) {
	    if ((*pt)->balance==-1){
		Tree left=(*pt)->left;
		if (left->balance==-1) balanceMM(pt,left);
		else balanceMP(pt,left); // case ( left->balance == +1 )
		*pd=0;
	    }else *pd = -(--(*pt)->balance);
	}
    }else if( (*pt)->data < x ) {
	insert_(&(*pt)->right,x,pd);
	if (*pd>0) {
	    if ((*pt)->balance==1){
		Tree right=(*pt)->right;
		if (right->balance==-1) balancePM(pt,right);
		else balancePP(pt,right); // case ( right->balance == +1 )
		*pd=0;
	    }else *pd = ++(*pt)->balance; 
	}
    }else *pd = -1; // case ( x == (*pt)->data )
}

// Beszúrja a t, kiegyensúlyozott keresõfába x-et, ha még nem tartalmazta,
// és igaz logikai értékkel tér vissza, különben hamissal.
// Ha t kiegyensúlyozott volt, az is marad.

// Insert(&t,x): Insert x into t, if t does not contain x.
// It returns "true", iff t did not contain x.
// A balanced tree remains balanced.
int Insert(Tree *pt, int x) {
    int d;
    insert_(pt,x,&d); 
    return d>=0;
}

void possible_del_left_balance(Tree *pt, int *pd){
    Tree t = *pt;
    if(*pd==-1) {
	if(t->balance==1) {
	    Tree right = t->right;
	    if( right->balance==1 ) balancePP(pt,right);
	    else if( right->balance==-1 ) balancePM(pt,right);
	    else{ // case right->balance==0
		balancePZ(pt,right);
		*pd=0;
	    }
	}else *pd = (++t->balance) - 1;
    }
}

void possible_del_right_balance(Tree *pt, int *pd){
    Tree t = *pt;
    if(*pd==-1) {
	if(t->balance==-1) {
	    Tree left = t->left;
	    if( left->balance==1 ) balanceMP(pt,left);
	    else if( left->balance==-1 ) balanceMM(pt,left);
	    else{ // case left->balance==0
		balanceMZ(pt,left);
		*pd=0;
	    }
	}else *pd = -( (--t->balance) + 1);
    }
}

void out_min(Tree *pt, int *pmin, int *pd) {
    Tree t = *pt;
    if( t->left == NULL ) {
	*pmin = t->data;
	*pt = t->right;
	free(t);
	*pd = -1;
    }else{
	out_min( &t->left, pmin, pd );
	possible_del_left_balance(pt,pd);
    }
}

// delete_(&t,x,&d): Delete x from t, if t contains x.
// A balanced tree remains balanced. (later)
// d = -1, if the delete was successful and t has become less high.
// d =  0, if the delete was successful and t has not become less high.
// d = +1, ha t has not contained x (and so the tree has not changed).
void delete_(Tree *pt, int x, int *pd){
    Tree t = *pt;
    if( t == NULL ) *pd = 1;
    else if( x < t->data ) {
	delete_( &t->left, x, pd );
	possible_del_left_balance(pt,pd);
    }else if( x > t->data ) {
	delete_( &t->right, x, pd );
	possible_del_right_balance(pt,pd);
    }else{ // x == t->data
	if( t->left == NULL ) {
	    *pt = t->right;
	    free(t);
	    *pd = -1;
	}else if( t->right == NULL ) {
	    *pt = t->left;
	    free(t);
	    *pd = -1;
	}else{ // t->left != NULL && t->right != NULL
	    out_min( &t->right, &t->data, pd );
	    possible_del_right_balance(pt,pd);
	}
    }
}

// Delete(&t,x): Delete x from t, if t contains x.
// It returns "true", iff t contained x.
// A balanced tree remains balanced. (later)
int Delete(Tree *pt, int x) {
    int d;
    delete_(pt,x,&d); 
    return d<=0;
}



/////////////////////////////////////

int main() {
    Tree root=NULL;
    int n, c;
    printf("\n               Building AVL trees - illustration.\n\n");
    for(;;) {
	printf("i:insert ; d:delete ; q:quit\n");
	c=getchar();    while(getchar()!='\n');
	switch( c ) {
	    case 'i': case 'I': {
		printf("The integers to be inserted? ");
		printf("(an alphabetic char at the end)\n");
		while(scanf("%d",&n)){       
		    if( Insert(&root,n) ) Inorder(root);
		    else printf("The number %d was already in the tree.\n",n);
		}
		while(getchar()!='\n');
		continue;
	    }
	    case 'd': case 'D': {
		printf("The integers to be deleted? ");
		printf("(an alphabetic char at the end)\n");
		while(scanf("%d",&n)){       
		    if( Delete(&root,n) ) Inorder(root);
		    else printf("The number %d was not in the tree.\n",n);
		}
		while(getchar()!='\n');
		continue;
	    }
	    case 'q': case 'Q': break;
	    default: continue;
	}
	break;
    }
    return 0;
}
