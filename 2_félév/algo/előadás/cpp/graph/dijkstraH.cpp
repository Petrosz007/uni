#include <iostream>
#include <fstream>

using namespace std;

const int INF = 1000000000;

typedef struct linkNode* edge;

struct linkNode{
    int next, cost;
    edge suc;
    linkNode(int edgeTo, int edgeCost) {
        next = edgeTo; cost = edgeCost; suc = 0;
    }
};

struct Negative_edge{};

void build_graph(string GraphFile, edge* &G, int &n) {
    ifstream GF;
    GF.open(GraphFile.c_str());
    GF >> n;
    G = new edge[n];
    for( int i=0 ; i<n ; ++i ) G[i] = 0;
    int node, edgeTo, edgeCost;
    for( GF >> node ; node >= 0 && node < n ; GF >> node ) {
	edge *p = &G[node];
        for( GF >> edgeTo ; edgeTo >= 0 && edgeTo < n ; GF >> edgeTo ){
	    GF >> edgeCost;
            if( edgeCost < 0 ) {
                cout << "Illegal cost: " << node << " -> "
                     << edgeTo << ":" << edgeCost << endl;
                throw Negative_edge(); 
            }
            *p = new linkNode(edgeTo,edgeCost);
            p = &(*p)->suc;
        }
    }
    GF.close();
}

void display_graph(edge G[], int n) {
    cout << "\nThe graph:\n";
    //for( int i=0 ; i<n ; ++i ) G[i] = new linkNode(1,1);
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
  PriorQ( int size, int s );
  int cost( int i );
  int out_min();
  void adjust( int j, int c );
  void costs(int dd[]);
  ~PriorQ();
private:
  int *d;  // d[i] == The cost of the path found to node i
  int *H; // The heap: d[H[i]] =< min( d[H[2*i+1]], d[H[2*i+2]] )
  int *I; // The inverted heap: I[H[i]] = i ( iff i in 0..m-1 ) 
          //                 or I[j]=-1 ( iff j notin H[0..m-1] (for alg Prim) )
  int n; // The elements of the priority queue come from [0..n-1]
  int m; // The size of the heap ( H[0..m-1] contains the heap ).
};

// G[0..n-1] represents the graph, s is the start node.
void Dijkstra( edge G[], int n, int s, int d[], int P[] ) {
    PriorQ Q(n,s);
    for( int i=0 ; i<n ; ++i ) {
        P[i] = -2;              // node i of G has not been found
    }
    P[s] = -1;                  // node s does not have parent node
    int i=s;                    // i is the actual node
    while( i>=0 ) {
        int costi = Q.cost(i);
        for( edge p=G[i] ; p!=0 ; p=p->suc ) {
            int j = p->next;
            int c = costi + p->cost;
            if( c < Q.cost(j) ) {
                P[j] = i;
                Q.adjust(j,c);
            }
        }
        i = Q.out_min();
    }
    Q.costs(d);
}

PriorQ::PriorQ( int size, int s ) {
    n = size;
    d = new int[n];
    H = new int[n];
    I = new int[n];
    for( int i=0 ; i<n ; ++i ) {
        d[i] = INF;
        H[i] = I[i] = i;
    }
    d[s] = 0;
    m = n-1;
    H[s] = m; I[m] = s;
    // I[s] = -1; //  alg Prim
}

int PriorQ::cost( int i ) {
    return d[i];
}

int PriorQ::out_min() {
  int i, j, k, dk, Hj, mini;
    if( m > 0 ) {
      mini = H[0]; // I[mini] = -1; //  alg Prim
      k = H[--m]; dk = d[k];
      i = 0; j = 1;
      while( j < m ) {
	if( j+1 < m && d[H[j+1]] < d[H[j]] ) ++j;
	Hj = H[j];
	if( dk > d[Hj] ) { 
	  H[i] = Hj; I[Hj] = i;
	  i = j; j = 2*j+1;
	}
	else break;
      }
      H[i] = k; I[k] = i;
    }
    else mini = -1;

    return mini;
}

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

void display_result( int n, int s, int *d, int *P ) {
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

  cout << "\n          Dijkstra algorithm\n" << endl;
  cout << "File of input graph: ";
  cin >> GraphFile;

  build_graph(GraphFile, G, n);
  display_graph(G, n);

  int s, *P = new int[n], *d = new int[n];

  cout << "Start node [0.." << n-1 << "] = ";
  cin >> s;

  Dijkstra(G,n,s,d,P);

  display_result(n,s,d,P);
  return 0;
}
