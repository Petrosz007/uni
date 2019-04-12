package shop;

public enum Genre {
    ACTION,
    ANIMATION,
    COMEDY,
    CRIME,
    DRAMA,
    DOCUMENTARY,
    FANTASY,
    HORROR,
    MISTERY,
    MUSIC,
    SCI_FI,
    THRILLER,
    WESTERN,
    INVALID;
    
    public static Genre get(int id) {
        if(id >= 0 && id < values().length ) {
            return values()[id];
        }
        else {
            return INVALID;
        }
    }
}
