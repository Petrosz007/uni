/*
import shop.Book;
import shop.Author;
import shop.Shop;
*/
import shop.*;

public class Main {
    
    public static void main(String[] args) {
        Shop bookshop = new Shop();
        
        Author a = new Author("Stuart Turton");
        Book book = new Book(
            a,
            "The Seven Deaths of Evelyn Hardcastle",
            2018);
        Book book2 = new Book(
            a,
            "ThE SeVeN DeAtHs Of EvElYn HaRdCaStLe",
            2018);
            
        System.out.println(book.equals(book2));
            
        bookshop.addBook(book);
        bookshop.addBook(book2);
        
        for(Book b : bookshop.getBooks()) {
            System.out.println(b);
        }
        
        System.out.println(
            bookshop.hasBook(book)
        );
    }   
}