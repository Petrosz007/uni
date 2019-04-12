package utils;

public class Date {
    private int year;
    private int month;
    private int day;
    
    public Date() {
        this.year = 2019;
        this.month = 3;
        this.day = 20;
    }
    
    
    public Date(int year, int month, int day) {
        this.year = year;
        this.month = month;
        this.day = day;
    }
    
    public int getYear() {
        return year;
    }
    
    public int getMonth() {
        return month;
    }
    
    public int getDay() {
        return day;
    }
    
    @Override
    public String toString() {
        String s = year + ".";
        if(month<10) {
            s += "0";
        }
        s += month + ".";
        if(day<10) {
            s += "0";
        }
        s += day + ".";
        return s;
    }
}