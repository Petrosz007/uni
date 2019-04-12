package shop;


public class Movie extends Video {
    protected final int length;
    protected final double imdbRate;
    
    public Movie(Creator creator, String title, int publishYear, Genre genre, Rating rating, int length, double imdbRate) {
        super(creator, title, publishYear, genre, rating);
        
        this.length = length;
        this.imdbRate = imdbRate;
        
    }
    
    public int getLength() {
        return length;
    }
    
    public double getImdbRate() {
        return imdbRate;
    }
    
    
    public static int price() {
        return 2000;
    }
    
    
    @Override
    public int hashCode() {
        return super.hashCode() + length + (int)(100*imdbRate);
    }
    
    @Override
    public String toString() {
        return super.toString() + ", rated " + imdbRate + " on IMDB.";
    }

    @Override
    public boolean equals(Object other) {
        if(super.equals(other)) {
            if(other instanceof Movie) {
                 Movie that = (Movie)other;
                
                return length == that.length && 
                Math.abs(imdbRate-that.imdbRate) < 0.00001;
            }
            else {
                return false;
            }
        }else {
            return false;
        }
    }
    
}