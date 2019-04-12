package shop;

public abstract class Video {
    protected final Creator creator;
    protected final String title;
    protected final int publishYear;
    protected final Genre genre;
    protected final Rating rating;
    
    public Video(Creator creator, String title, int publishYear, Genre genre, Rating rating) {
        this.creator = creator;
        this.title = title;
        this.publishYear = publishYear;
        this.genre = genre;
        this.rating = rating;
    }
    
    //virtual std::string getInfo() = 0;
    public abstract String getInfo();
    
    public Creator getCreator() {
        return creator;
    }
    
    public String getTitle() {
        return title;
    }
    
    public int getPublishYear() {
        return publishYear;
    }
    
    public Genre getGenre() {
        return genre;
    }
    
    public Rating getRating() {
        return rating;
    }
    
    
    @Override
    public String toString() {
        return title + " by {" + creator + "}, " + publishYear;
    }
    
    @Override
    public int hashCode() {
        return creator.hashCode() + title.hashCode() + publishYear + genre.hashCode() + rating.hashCode();
    }
    
    @Override
    public boolean equals(Object other) {
        if(this == other) return true;
        if(!(other instanceof Video)) {
            return false;
        }
        Video that = (Video)other;
        return title.equalsIgnoreCase(that.title) 
        && creator.equals(that.creator) && publishYear == that.publishYear &&
        this.genre == that.genre &&
        this.rating == that.rating;
    }
}