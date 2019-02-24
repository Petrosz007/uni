// Calculate the minimum weight spanning tree of a connected graph; 
// or the set of minimum weight spanning trees, if the graph is not connected.

// Algorithm Prim on graphs represented by edge-list.
// The input graph is considered symmetric (not directed) 
//   (if edge i->j is contained, edge j->i is supposed).
// If there are parallel edges, only one of them: 
//   the cheapest one is considered.
// We use minimum heap to represent the priority queue.

#include <iostream>
#include <fstream>

using namespace std;

const int INF = 1000000000;

typedef struct linkNode* edge;

struct linkNode{
  int next, cost;
  edge suc;
  linkNode(int edgeTo, int edgeCost, edge nextEdge) {
    next = edgeTo; cost = edgeCost; suc = nextEdge;
  }
};

void sorted_insert( edge &L, int edgeTo, int edgeCost ) {
  edge *p;  
  for( p=&L ; *p!=0 && (*p)->next < edgeTo ; p=&(*p)->suc );
  if( *p==0 || (*p)->next > edgeTo ) *p = new linkNode(edgeTo,edgeCost,*p);
  else // if ( *p!=0 && (*p)->next == edgeTo )
    if( edgeCost < (*p)->cost ) (*p)->cost = edgeCost;
}

void build_graph(string GraphFile, edge* &G, int &n) {
    ifstream GF;
    GF.open(GraphFile.c_str());
    GF >> n;
    G = new edge[n];
    for( int i=0 ; i<n ; ++i ) G[i] = 0;
    int node, edgeTo, edgeCost;
    for( GF >> node ; node >= 0 && node < n ; GF >> node ) {
        for( GF >> edgeTo ; edgeTo >= 0 && edgeTo < n ; GF >> edgeTo ){
	    GF >> edgeCost;
	    sorted_insert(G[node],edgeTo,edgeCost);
	    sorted_insert(G[edgeTo],node,edgeCost);
        }
    }
    GF.close();
}

void display_graph(edge G[], int n) {
    cout << "\nThe graph:\n";
    for( int i=0 ; i<n ; ++i ) {
        cout << i;
        for( edge p = G[i] ; p != 0 ; p = p->suc ) {
	  cout << " -> " << p->next << ':' << p->cost;
        }
        cout << " ;\n";
    }
}

class PriorQ {
public:
  PriorQ( int size );
  void init_a_spanning_tree( int s );  
  int cost( int i );
  int out_min();
  bool has( int j );
  void adjust( int j, int c );
  void costs(int dd[]);
  ~PriorQ();
private:
  int *d;  // d[i] == The cost of the path found to node i
  int *H; // The heap: d[H[i]] =< min( d[H[2*i+1]], d[H[2*i+2]] )
  int *I; // The inverted heap: I[H[i]] = i ( iff i in 0..m-1 ) 
          //                 or I[j]=-1 ( iff j notin H[0..m-1] )
  int n; // The elements of the priority queue come from [0..n-1]
  int m; // The size of the heap ( H[0..m-1] contains the heap ).
};

// G[0..n-1] represents the graph.
void Prim( edge G[], int n, int d[], int P[] ) {
    PriorQ Q(n);
    for( int i=0 ; i<n ; ++i ) {
        P[i] = -2;              // node i of G has not been found
    }
    for( int s=0 ; s<n ; ++s ) {
      if( Q.has(s) ) { 
	Q.init_a_spanning_tree(s); 
        // s will be the root of the minimum weight spanning tree 
        // of a connected component of the graph
	P[s] = -1;                  // node s does not have parent node
	int i=s;                    // i is the actual node
	while( i>=0 ) {
	  for( edge p=G[i] ; p!=0 ; p=p->suc ) { // consider the neighbors of i
            int j = p->next; // j = the actual neighbor
            if( Q.has(j) && p->cost < Q.cost(j) ) {
	      Q.adjust(j,p->cost);
	      P[j] = i;
            }
	  }
	  i = Q.out_min();
	}

      }
    }
    Q.costs(d);
}

PriorQ::PriorQ( int size ) {
    m = n = size;
    d = new int[n];
    H = new int[n];
    I = new int[n];
    for( int i=0 ; i<n ; ++i ) {
        d[i] = INF;
        H[i] = I[i] = i;
    }
}

void PriorQ::init_a_spanning_tree( int s ) { 
  // Now d[H[i]] == INF (forall i in 0..m-1), (forall elements of the heap)
  int i = I[s],   // the place of s in the heap
      k = H[--m]; // the last item of the heap
  d[s] = 0; 
  // Let s be the root of the minimum weight spanning tree 
  // of the connected component containing s.
  H[i] = k; I[k] = i; // Let the last item of the heap move into the place of s.
  I[s] = -1; // Remove s from the heap.
}

int PriorQ::cost( int i ) {
    return d[i];
}

// Remove and return the node with the minimal cost (d value), 
//   except if the that minimal cost is INF.
// If the priority queue is empty or the minimal cost is INF, return -1.
int PriorQ::out_min() {
  int i, j, k, dk, Hj, mini;
  if( m > 0 ) {
    mini = H[0];  // mini = the node of minimal cost
    if( d[mini] < INF ) { 
      k = H[--m]; dk = d[k]; // k = the last node
      i = 0; j = 1;          // i refers to the hole, j to its left child
      while( j < m ) {       // Sink the hole into the heap.
	if( j+1 < m && d[H[j+1]] < d[H[j]] ) ++j; // Select the smaller child.
	Hj = H[j];
	if( dk > d[Hj] ) {   // k still does not fit into the hole
	  H[i] = Hj; I[Hj] = i; // exchange the hole with its smaller child
	  i = j; j = 2*j+1;    // i refers to the hole, j to its left child
	}
	else break;   // k fits into the hole
      }
      H[i] = k; I[k] = i; // Put k into the hole (spec. for mini==k)
      I[mini] = -1; // Remove the node of minimal cost.
    }
    else mini = -1;
  }
  else mini = -1; 

  return mini;
}

bool PriorQ::has( int j ) { return I[j] >= 0; }

void PriorQ::adjust( int k, int c ) {
  int i, j;
    d[k] = c;
    j = I[k];
    while( j > 0 && d[H[i=(j-1)/2]] > c ) {
      H[j] = H[i]; I[H[i]] = j;
      j = i;
    }
    H[j] = k; I[k] = j;
}

void PriorQ::costs(int dd[]) {
  for( int i = 0 ; i<n ; ++i ) dd[i] = d[i]; 
}

PriorQ::~PriorQ() {
    delete[] d; // safe solution
    delete[] H;
    delete[] I;
}

void display_result( int n, int *d, int *P ) {
    cout << "\nThe result ( node / cost <- parent ) : \n";
    for( int i=0 ; i<n ; ++i ) {
        cout << i << " / " << d[i] << " <- " << P[i] << endl;
    }
}

int main()
{
  string GraphFile;
  edge* G;    // G represents the graph
  int n; // the nodes of the graph are [0..n-1]

  cout << "\n          Algorithm Prim\n" << endl;
  cout << "File of input graph: ";
  cin >> GraphFile;

  build_graph(GraphFile, G, n);
  display_graph(G, n);

  int *P = new int[n], *d = new int[n];

  Prim(G,n,d,P);

  display_result(n,d,P);
  return 0;
}
