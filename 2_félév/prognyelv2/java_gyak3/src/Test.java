import utils.Point;

public class Test {
    
    public static void main(String[] args) {
        Point p = new Point();
        assertEq("(0,0)", p.toString());
        
        Point q = new Point(4,2);
        assertEq(4, q.getX());
        assertEq(2, q.getY());
        
        Object r = new Point(4,2);
        assertEq(true, r.equals(q));
        assertEq(true, q.equals(r));
        
        assertEq(false, p.equals(r));
        assertEq(false, q.equals(null));
        
        if(errorCount==0) {
            System.out.println("All test passed!");
        } 
        else {
            System.out.println("Gotta work on this!");
        }
    }
    
    private static int errorCount = 0;
    
    private static void assertEq(Object exc, Object act) {
        if(!java.util.Objects.equals(exc,act)) {
            ++errorCount;
            System.err.println(String.format(
                    "Values should be equal!\nExpected: '%1$s', Actual:'%2$s'", exc, act));
        }
    }
}
