package shop;

import java.util.Map;
import java.util.HashMap;

public class Shop { 
    private final Map<Video, Integer> videos;
    
    public Shop() {
        this.videos = new HashMap<>();
    }
    
    public void addVideo(Video v, int price) {
        videos.put(v, price);
    }
    
    public void sellVideo(Video v, Customer c) throws ShopTransactionException {
        Integer price = videos.get(v);
        if(null != price) {
            if(c.getBalance() >= price) {
                c.buySomething(price);
            }
            else{
                throw new ShopTransactionException("The Customer can not buy this video!");
            }
        }
        else {
            throw new ShopTransactionException("The shop does not have this video!");
        }
    }
    
    public Map<Video, Integer> getVideos() {
        return videos;
    }
    
    public boolean hasVideo(Video b) {
        return videos.containsKey(b);
    }
}
