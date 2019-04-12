package shop;

import java.util.List;
import java.util.LinkedList;

//C++: class Shop : public Object {
public class Shop { //extends Object {
    
    //std::vector<Book> books;
    private final List<Book> books;
    public Shop() {
        this.books = new LinkedList<>();
    }
    
    public void addBook(Book b) {
        books.add(b);
    }
    
    public List<Book> getBooks() {
        return books;
    }
    
    public boolean hasBook(Book b) {
        return books.contains(b);
        /*
        for(int i=0;i<books.size();++i) {
            if(books.get(i).equals(b)) {
                return true;
            }
        }
        return false;
        */
    }
}