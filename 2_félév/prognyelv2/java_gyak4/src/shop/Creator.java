package shop;

public class Creator {
    private final String name;
    
    public Creator(String name) {
        this.name = name;
    }
    
    public String getName() {
        return name;
    }
    
    @Override
    public int hashCode() {
        return name.hashCode();
    }
    
    @Override
    public boolean equals(Object other) {
        if(this == other) {
            return true;
        }
        if(other instanceof Creator) {
            Creator that = (Creator)other;
            return this.name.equalsIgnoreCase(that.name);
        }
        else {
            return false;
        }
    }
    
    @Override
    public String toString() {
        return name;
    }
}