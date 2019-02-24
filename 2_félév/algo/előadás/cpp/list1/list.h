// Copyright: Ásványi, Tibor; Budapest, November 2001.
// Minden jog fenntartva.

typedef char T;

class list
{
  struct lnode *L, **act;
 public:
  list();        // Construct an empty list; end() becomes true.
  ~list();       // This destroys the whole list.
  void first();  // Let the first element of the list be actual;
                 // end() becomes true, iff the list is empty.
  void next();   // Let the actual element be the next element of the list. 
                 // (!end()) 
  bool end();    // Are we at the end of the list (after the last element)?
  bool empty();  // Is the list empty?
  void ins(T x); // Insert x at the actual position; 
                 // x will be at the actual position.
  void rem();    // Remove the element at the actual position. (!end())
                 // The next element of the list will be the actual one.
  void put(T x); // Update the content of the actual element.  (!end())
  T get();       // Return the content of the actual element.  (!end())
  void del_tail();    // Delete the tail of the list from the actual element; 
                      // end() becomes true.
  void move(list& LL); // Remove the actual element of this list (!end()), 
                       // and insert it into list LL at its actual position.
  void change_tail(list& LL); // Exchange the tails of the two lists from their
                              // actual positions.
  void print_list(); // Print the list.
  struct end_error{};
  struct mem_full{};
};
