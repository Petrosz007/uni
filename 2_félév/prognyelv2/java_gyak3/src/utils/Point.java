package utils;

public class Point{
    public final int x;
    public final int y;

    public Point(){
        this.x=0;
        this.y=0;
    }

    public Point(int x, int y){
        this.x=x;
        this.y=y;
    }

    public getX(){
        return x;
    }
    
    public getY(){
        return y;
    }
    
    @Override
    public boolean equals(Object other){
        if (other == this) {
            return true;
        }
        if (other == null) {
            return false;
        }
        if(other instanceof Point){
            Point that = (Point)other;
            return x == that.x && y==that.y;
        }
        else return false;
    }

    @Override
    public String toString() {
        String s ='(' + x + ',' + y + ')';
    }

}