package shop;

public enum Rating {
    G("General Audience"),
    PG("Parential Guidance Suggested"),
    PG_13("Parents Strongly Cautioned"),
    R("Restricted"),
    NC_17("Adults only"),
    NOT_YET_RATED("Not yet rated");
    
    private final String text;
    /* private */ Rating(String text) {
        this.text = text;
    }
    
    public String getText() {
        return text;
    }
    
    public static Rating get(String s) {
        for(Rating r: values()) {
            if(r.name().equalsIgnoreCase(s)) {
                return r;
            }
        }
        return NOT_YET_RATED;
    }
    
    public static Rating get(int id) {
        if( 0>= id && id < values().length) {
            return values()[id];
        }
        
        return NOT_YET_RATED;
    }
    
    @Override
    public String toString() {
        return name() + " - " + text;
    }
    
}
