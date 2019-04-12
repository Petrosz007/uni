package utils;

public class Point {
    private final int x;
    private final int y;
    
    public Point() {
        this(0,0);
    }
    
    public Point(int x, int y) {
        this.x = x;
        this.y = y;
    }
    
    public int getX() {
        return x;
    }
    
    public int getY() {
        return y;
    }
    
    @Override
    public String toString() {
        return "(" + x + "," + y + ")";
    }
    
    @Override
    public boolean equals(Object other) {
        if(this == other) {
            return true;
        }
        if(!(other instanceof Point)){
            return false;
        }
        Point that = (Point)other;
        
        return (this.x==that.x && this.y==that.y);
    }
}