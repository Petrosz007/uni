package shop;


public class Videoclip extends Video {
    protected final int length;
    protected final int viewCount;
    
    public Videoclip(Creator creator, String title, int publishYear, Genre genre, Rating rating, int length, int viewCount) {
        super(creator, title, publishYear, genre, rating);
        
        this.length = length;
        this.viewCount = viewCount;
        
    }
    
    public int getLength() {
        return length;
    }
    
    public int getViewCount() {
        return viewCount;
    }
    
    public static int price() {
        return 200;
    }
    
    
    @Override
    public int hashCode() {
        return super.hashCode() + length + viewCount;
    }
    
    @Override
    public boolean equals(Object other) {
        if(super.equals(other)) {
            if(other instanceof Videoclip) {
                 Videoclip that = (Videoclip)other;
                
                return length == that.length && viewCount == that.viewCount;
            }
            else {
                return false;
            }
        }else {
            return false;
        }
    }
    
}