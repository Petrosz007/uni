/*
import shop.Book;
import shop.Author;
import shop.Shop;
*/
import shop.*;
import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.Map;

public class Main {
    
    static boolean fillShop() {    
        try(
            BufferedReader input = new BufferedReader(new FileReader("books.csv"));
        )
        {
            String line;
            while(null != (line = input.readLine()) ) {
                String[] data = line.split(";");
                shop.addBook(new Book(new Author(data[0]), data[1], Integer.parseInt(data[2])));
            }
        }
        catch(IOException ioe){
            System.err.println("Some error happened while reading! " + ioe);
            return false;
        }
        
        return true;
    }
    
    static Shop shop;
    public static void main(String[] args) {
        shop = new Shop();
        if(fillShop()) {
            
            for(Map.Entry<Book,Integer> entry : shop.getBooks().entrySet()) {
                System.out.println(entry);
            }
            
        } else{
            System.err.println("Could not fill the shop with books.");
        }
    }
}


/*
class Main2 {
    
    public static void main(String[] args) {
        BufferedReader input = null;
        try{
            input = new BufferedReader(new FileReader("books.csv"));
            String line;
            while(null != (line = input.readLine()) ) {
                //line == G.R.R. Martin;Game of Thrones;2019
                String[] data = line.split(";");
                //data[0] == G.R.R. Martin
                //data[1] == Game of Thrones
                //data[2] == 2019
                
                //int.parseInt()
                //int.equals()
                //int x=0;
                //x.equals(25);
                
                System.out.println(new Book(new Author(data[0]), data[1], Integer.parseInt(data[2])));
                
            }
        }
        catch(IOException ioe){
            System.err.println("Some error happened while reading! " + ioe);
        }
        finally{
            if(null != input){
                try{
                    input.close();
                }catch(IOException ioe){
                    System.err.println("Some error happened while closing! " + ioe);
                }
            }
        }   
    }   
}
*/