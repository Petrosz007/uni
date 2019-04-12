package utils;
//import utils.Date;

public class Sample {
    public static void main(String[] args) {
        
        for(int i=0;i<args.length;++i)
        {
            System.out.println(args[i]);
        }
        
        for(String s : args)
        {
            System.out.println(s);
        }
        
        //System.out.println("Goodbye World");
        /*
        Date d = new Date();
        d.year = 2019;
        d.month = 3;
        d.day = 20;*/
        Date d = new Date(2019,3,20);
        /*System.out.println(d.year);
        System.out.println(d.month);
        System.out.println(d.day);
        */
        //System.out.println(d);
        int d_year = d.getYear();
        d_year = 2018;
        
        String s = d.toString();
        System.out.println(s);
        
        Date e = new Date(2019,4,25);
        f(e);
        System.out.println(e);
        e = null;
        System.out.println(e);
        
        Date date = new Date();
        //utils.Date d = new utils.Date();
    }
    
    static void f(Date d){
        d = null;
    }
}
