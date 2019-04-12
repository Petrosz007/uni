package shop;

public class Book {
    /*
        C++
        private:
            const int publishYear;
            Book(int py) : publishYear(py) { ... }
    */
    private final Author author;
    private final String title;
    private final int publishYear;
        
    public Book(Author author, String title, int publishYear) {
        this.author = author;
        this.title = title;
        this.publishYear = publishYear;
    }
    /*
    nem jó, mert a title final
    public void setTitle(String s) {
        this.title = s;
    }*/
    
    public Author getAuthor() {
        return author;
    }
    
    public String getTitle() {
        return title;
    }
    
    public int getPublishYear() {
        return publishYear;
    }
    
    //ez nem override, hanem overload
    //@Override
    //public boolean equals(Book other)
    
    @Override
    public String toString() {
        return title + " by {" + author + "}, " + publishYear;
    }
    
    @Override
    public int hashCode() {
        return author.hashCode() + title.hashCode() + publishYear;
    }
    
    @Override
    public boolean equals(Object other) {
        //ha ugyan arra a memóriaterületre mutat
        if(this == other) return true;
        //ha nullptr, vagy nem Book típusú object
        if(null == other || !(other instanceof Book)) {
            return false;
        }
        Book that = (Book)other;
        return
        //referencia típus, equals kell
        title.equalsIgnoreCase(that.title)
        //referencia típus, equals kell
        && author.equals(that.author)
        //primitív típus, == kell
        && publishYear == that.publishYear;
    }
}