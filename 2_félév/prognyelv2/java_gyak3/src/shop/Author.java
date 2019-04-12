package shop;

public class Author {
    private final String name;
    
    public Author(String name) {
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
        
        //if(other = null){
        if(null == other){
            return false;
        }
        
        if(other instanceof Author) {
            Author that = (Author)other;
            //return (this.name == other.name);
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