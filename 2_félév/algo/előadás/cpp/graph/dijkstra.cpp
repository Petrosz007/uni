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
    int* costs();
    ~PriorQ();
  private:
    int *d;  // d[i] == The cost of the path found to node i
    bool *b; // b[i] == node i has not been selected for expansion
    int n;   // The elements of the priority queue come from [0..n-1]
};

// G[0..n-1] represents the graph, s is the start node.
void Dijkstra( edge G[], int n, int s, int* &d, int* &P ) {
    PriorQ Q(n,s);
    P = new int[n];
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
    d = Q.costs();
}

PriorQ::PriorQ( int size, int s ) {
    n = size;
    d = new int[n];
    b = new bool[n];
    for( int i=0 ; i<n ; ++i ) {
        d[i] = INF;
        b[i] = true;
    }
    d[s] = 0;
    b[s] = false;
}

int PriorQ::cost( int i ) {
    return d[i];
}

int PriorQ::out_min() {
    int i, mini = -1;

    for( i=0 ; i<n && mini==-1 ; ++i ) {
        if( b[i] ) mini = i;
    }
    if( mini>=0 ) {
        for( ; i<n ; ++i ) {
            if( b[i] && d[i] < d[mini] ) mini = i;
        }
        b[mini] = false;
    }
    return mini;
}

void PriorQ::adjust( int j, int c ) {
    d[j] = c;
}

int* PriorQ::costs() {
    return d;  // unsafe but effective
    // (safe solution: a copy of d[0..n-1] should be returned)
}

PriorQ::~PriorQ() {
    //delete[] d; // safe solution
    delete[] b;
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

	int s, *P, *d;

	cout << "Start node [0.." << n-1 << "] = ";
	cin >> s;

	Dijkstra(G,n,s,d,P);

    display_result(n,s,d,P);
	return 0;
}
