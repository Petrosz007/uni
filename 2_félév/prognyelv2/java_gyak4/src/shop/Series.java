package shop;


public class Series extends Video {
    protected final int seasons;
    protected final int episodeCount;
    
    public Series(Creator creator, String title, int publishYear, Genre genre, Rating rating, int seasons, int episodeCount) {
        super(creator, title, publishYear, genre, rating);
        
        this.seasons = seasons;
        this.episodeCount = episodeCount;
        
    }
    
    public int getSeasons() {
        return seasons;
    }
    
    public int getEpisodeCount() {
        return episodeCount;
    }
    
    
    public static int price() {
        return 1500;
    }
    
    
    @Override
    public int hashCode() {
        return super.hashCode() + seasons + episodeCount;
    }
    
    @Override
    public boolean equals(Object other) {
        if(super.equals(other)) {
            if(other instanceof Series) {
                 Series that = (Series)other;
                
                return seasons == that.seasons && episodeCount == that.episodeCount;
            }
            else {
                return false;
            }
        }else {
            return false;
        }
    }
    
}