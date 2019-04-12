package shop;

import java.util.Map;
import java.util.HashMap;

public class Shop { 
    //private final Map<Book, int> books;
    
    private final Map<Book, Integer> books;
    /*
    int i = 42;
    Integer j = new Integer(i);
    Integer j = i;
    */
    
    /*
    foo<int> != foo<char>
    foo<Object>
    foo<Object>
    */
    /*
    List<String> l = new LinkedList<>();
    //List<Object> l = new LinkedList<>();
    ...
    String s = l.get(0);
    
    String s = (String)l.get(0);
    */
    
    public Shop() {
        this.books = new HashMap<>();
    }
    
    public void addBook(Book b) {
        Integer count = books.get(b) == null ? 0 : books.get(b);
        books.put(b, count+1);
        
    }
    
    public void sellBook(Book b) throws ShopTransactionException {
        if(books.containsKey(b)) {
            if(books.get(b) > 0 ) {
                books.put(b, books.get(b)-1);
            }
            else{
                throw new ShopTransactionException("The shop is out of stock!");
            }
        }
        else {
            throw new ShopTransactionException("The shop doesn't have this book!");
        }
    }
    
    public Map<Book, Integer> getBooks() {
        return books;
    }
    
    public boolean hasBook(Book b) {
        return books.containsKey(b);
    }
}

class ShopTransactionException extends Exception {
    ShopTransactionException(String message) {
        super(message);
    }
    
    ShopTransactionException() {
    
    }
}

