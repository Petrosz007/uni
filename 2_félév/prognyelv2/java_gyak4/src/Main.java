import shop.*;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

import java.util.Map;
import java.util.Set;

public class Main {
    
    private static Shop shop;
    
    static Movie createMovie(String[] data) {
        Creator creator = new Creator(data[1]);
        String title = data[2];
        int publishYear = Integer.parseInt(data[3]);
        Genre g = Genre.get(Integer.parseInt(data[4]));
        Rating r = Rating.get(data[5]);
        
        int length = Integer.parseInt(data[6]);
        double imdbRate = Double.parseDouble(data[7]);
        
        return new Movie(creator, title,publishYear,g,r,length,imdbRate);
    }
    
    static Series createSeries(String[] data) {
        Creator creator = new Creator(data[1]);
        String title = data[2];
        int publishYear = Integer.parseInt(data[3]);
        Genre g = Genre.get(Integer.parseInt(data[4]));
        Rating r = Rating.get(data[5]);
        
        int seasons = Integer.parseInt(data[6]);
        int episodes = Integer.parseInt(data[7]);
        
        return new Series(creator, title,publishYear,g,r,seasons,episodes);
    }
      static Videoclip createVideoclip(String[] data) {
        Creator creator = new Creator(data[1]);
        String title = data[2];
        int publishYear = Integer.parseInt(data[3]);
        Genre g = Genre.get(Integer.parseInt(data[4]));
        Rating r = Rating.get(data[5]);
        
        int length = Integer.parseInt(data[6]);
        int viewCount = Integer.parseInt(data[7]);
        
        return new Videoclip(creator, title,publishYear,g,r,length,viewCount);
    }
    
    static boolean fillShop() {    
        try(
            BufferedReader input = new BufferedReader(new FileReader("videos.csv"));
        )
        {
            String line;
            while(null != (line = input.readLine()) ) {
                String[] data = line.split(";");
                /*
                base:
                    id;creator;title;publish_year;main_genre;rating
                1: series
                    seasons;episode_count
                2: movie
                    length;imdb_rate
                3: videoclip
                    length;view_count
                */
                
                int id = Integer.parseInt(data[0]);
                
                Video v = null;
                int price = 0;
                
                switch(id)
                {
                    case 1: v = createSeries(data); price = Series.price(); break;
                    case 2: v = createMovie(data); price = Movie.price(); break;
                    case 3: v = createVideoclip(data); price = Videoclip.price(); break;
                    default: throw new IllegalArgumentException("Invalid ID found.");
                }
                
                shop.addVideo(v, price);
            }
        }
        catch(IOException ioe){
            System.err.println("Some error happened while reading! " + ioe);
            return false;
        }
        
        return true;
    }
    
    public static void main(String[] args) {
        shop = new Shop();
        if(fillShop()) {
            
            for(Map.Entry<Video, Integer> e : shop.getVideos().entrySet()) {
                if(e.getKey() instanceof Movie) {
                    Movie m = (Movie)e.getKey();
                    
                    m.getImdbRate();
                }
                System.out.println(e);
            }
            
        } else{
            System.err.println("Could not fill the shop with the required videos!");
            System.exit(1);
        }
    }
}