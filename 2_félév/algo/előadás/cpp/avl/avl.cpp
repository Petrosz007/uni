// Building AVL trees - illustration.
// Ásványi Tibor, ELTE IK, Budapest; 
// January 2007; October, November 2008; November 2011.

#include <iostream>

using namespace std;

typedef struct Node *Tree;

struct Node
{
   int data;
   int balance;  // the balance of the Node
   Tree left, right;
  
   Node(int d){
      data=d; balance=0; left=right=0;
   }
};

// Inorder print of the tree.
// The nonempty (sub)trees are bracketed.
// The format of a Node is "data:balance".
// The empty (sub)trees are not printed.
void inorder_(Tree t){
    if (t!=0){
      cout << " (";
      inorder_(t->left);
      cout << t->data <<":" << t->balance ;
      inorder_(t->right);
      cout <<  ") ";
    }
}

// Inorder print of the tree. Printed into a single line, 
// with a "newline" at the end.
void Inorder(Tree t){
    if(t!=0) inorder_(t);
    else cout << "Empty tree.";
    cout << "\n";
}

// Balance the (sub)tree t when
// t->balance == -2 && left == t->left && left->balance == -1
void balanceMM(Tree &t,Tree left){
    t->left=left->right;
    left->right=t;
    t->balance=0;
    left->balance=0;
    t=left;
}

// Balance the (sub)tree t when
// t->balance == -2 && left == t->left && left->balance == 0
void balanceMZ(Tree &t,Tree left){
    t->left=left->right;
    left->right=t;
    t->balance=-1;
    left->balance=1;
    t=left;
}

// Balance the (sub)tree t when
// t->balance == -2 && left == t->left && left->balance == 1
void balanceMP(Tree &t,Tree left){
    Tree p=left->right;
    t->left=p->right;
    left->right=p->left;
    p->right=t;
    p->left=left;
    // if (p->balance==-1){ t->balance=1; left->balance=0; }
    // else if (p->balance==0){ t->balance=0; left->balance=0; }
    // else if (p->balance==1){ t->balance=0; left->balance=-1; }
    t->balance = (-p->balance+1)>>1;
    left->balance = -((p->balance+1)>>1);  
    p->balance=0;
    t=p;
}

// Balance the (sub)tree t when
// t->balance == 2 && right == t->right && right->balance == -1
void balancePM(Tree &t,Tree right){
    Tree p=right->left;
    t->right=p->left;
    right->left=p->right;
    p->left=t;
    p->right=right;
    // if (p->balance==-1){ t->balance=0; right->balance=1; }
    // else if (p->balance==0){ t->balance=0; right->balance=0; }
    // else if (p->balance==1){ t->balance=-1; right->balance=0; }
    t->balance = -((p->balance+1)>>1);
    right->balance = (-p->balance+1)>>1;
    p->balance=0;
    t=p;
}

// Balance the (sub)tree t when
// t->balance == 2 && right == t->right && right->balance == 0
void balancePZ(Tree &t,Tree right){
    t->right=right->left;
    right->left=t;
    t->balance=1;
    right->balance=-1;
    t=right;
}

// Balance the (sub)tree t when
// t->balance == 2 && right == t->right && right->balance == 1
void balancePP(Tree &t,Tree right){
    t->right=right->left;
    right->left=t;
    t->balance=right->balance=0;
    t=right;
}

// insert_(t,x,d): Insert x into t, if t does not contains x.
// A balanced tree remains balanced.
// d =  1, if the insert was successful and t has become higher.
// d =  0, if the insert was successful and t has not become higher.
// d = -1, if t has already contained x (and so the tree has not changed).
void insert_(Tree &t, int x, int &d){
    if (t==0){
	t=new Node(x);
	d=1;
    }else if ( x < t->data ) {
	insert_(t->left,x,d);
	if (d>0) {
	    if (t->balance==-1){
		Tree left=t->left;
		if (left->balance==-1) balanceMM(t,left);
		else balanceMP(t,left); // ( left->balance == +1 )
		d=0;
	    }else d = -(--t->balance);
	}
    }else if( t->data < x ) {
	insert_(t->right,x,d);
	if (d>0) {
	    if (t->balance==1){
		Tree right=t->right;
		if (right->balance==-1) balancePM(t,right);
		else balancePP(t,right); // ( right->balance == +1 )
		d=0;
	    }else d = ++t->balance; 
	}
    }else d = -1; // ( x == t->data )
}

// Insert(t,x): Insert x into t, if t does not contain x.
// It returns "true", iff t did not contain x.
// A balanced tree remains balanced.
bool Insert(Tree &t, int x) {
    int d;
    insert_(t,x,d); 
    return d>=0;
}

// A node was deleted from the left subtree of t.
// The left subtree became lower.
// Balance t if required. Actualize the balances.
// d = -1, if t has become lower.
// d =  0, if t has not become lower.
void after_del_from_left_balance(Tree &t, int &d){    
    if(t->balance==1) {
        Tree right = t->right;
        if( right->balance==1 ) balancePP(t,right);
        else if( right->balance==-1 ) balancePM(t,right);
        else{ // right->balance==0
            balancePZ(t,right);
            d=0;
        }
    }else d = (++t->balance) - 1;
}

// A node was deleted from the right subtree of t.
// The right subtree became lower.
// Balance t if required. Actualize the balances.
// d = -1, if t has become lower.
// d =  0, if t has not become lower.
void after_del_from_right_balance(Tree &t, int &d){
    if(t->balance==-1) {
        Tree left = t->left;
        if( left->balance==1 ) balanceMP(t,left);
        else if( left->balance==-1 ) balanceMM(t,left);
        else{ // case left->balance==0
            balanceMZ(t,left);
            d=0;
        }
    }else d = -( (--t->balance) + 1);   
}

// out_min(t,min,d) removes its minimum from the nonempty t.
// d = -1, if t has become lower.
// d =  0, if t has not become lower.
void out_min(Tree &t, int &min, int &d) {
    if( t->left == 0 ) {
        Tree p = t;
        min = p->data;
        t = p->right;
        delete p;
        d = -1;
    }else{
        out_min( t->left, min, d );
        if( d==-1 ) after_del_from_left_balance(t,d);
    }
}

// delete_(t,x,d): Delete x from t, if t contains x.
// A balanced tree remains balanced.
// d = -1, if the delete was successful and t has become lower.
// d =  0, if the delete was successful and t has not become lower.
// d = +1, if t has not contained x (and so the tree has not changed).
void delete_(Tree &t, int x, int &d){
    if( t == 0 ) d = 1;
    else if( x < t->data ) {
	delete_( t->left, x, d );
	if( d==-1 ) after_del_from_left_balance(t,d);
    }else if( x > t->data ) {
	delete_( t->right, x, d );
	if( d==-1 ) after_del_from_right_balance(t,d);
    }else{ // x == t->data
	if( t->left == 0 ) {
	   Tree p = t;
	   t = p->right;
	   delete p;
	   d = -1;
	}else if( t->right == 0 ) {
	   Tree p = t;
	   t = p->left;
	   delete p;
	   d = -1;
	}else{ // t->left != 0 && t->right != 0
	   out_min( t->right, t->data, d );
	   if( d==-1 ) after_del_from_right_balance(t,d);
	}
    }
}

// Delete(&t,x): Delete x from t, if t contains x.
// It returns "true", iff t contained x.
// A balanced tree remains balanced. (later)
bool Delete(Tree &t, int x) {
    int d;
    delete_(t,x,d); 
    return d<=0;
}


/////////////////////////////////////

int main() {
  Tree root=0;
  int n; char c;
  cout << "\n               Building AVL trees - illustration.\n\n";
  for(;;) {
    cout << "i:insert ; d:delete ; q:quit\n";
    cin >> c;
    switch( c ) {
    case 'i': case 'I': {
      cout << "The integers to be inserted? ";
      cout << "(an alphabetic char at the end)\n";
      while( cin >> n ){       
	if( Insert(root,n) ) Inorder(root);
	else cout << "The number " << n << " was already in the tree.\n";
      }
      cin.clear(); while(cin.get()!='\n');
      continue;
    }
    case 'd': case 'D': {
      cout << "The integers to be deleted? ";
      cout << "(an alphabetic char at the end)\n";
      while( cin >> n ){       
	if( Delete(root,n) ) Inorder(root);
	else cout << "The number " << n << " was not in the tree.\n";
      }
      cin.clear(); while(cin.get()!='\n');
      continue;
    }
    case 'q': case 'Q': break;
    default: continue;
    }
    break;
  }
  return 0;
}
