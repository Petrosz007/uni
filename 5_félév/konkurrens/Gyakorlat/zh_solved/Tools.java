import java.util.*;
import java.util.concurrent.*;
import java.util.concurrent.ThreadLocalRandom;

/* *** WARNING ***
   While highly unlikely, there is a chance for generating the same number
   twice using a pseudo-random generator. Since we are not actually doing
   any processing this is not an issue, however in real software this could
   lead to problems such as stiching together image parts belonging to
   different images. This will go unnoticed if a generated ID is shared
   between different images.
*/

public class Tools {
  public static void countSheep(int ms) {
    long start = System.currentTimeMillis();
    if (3 < ms) {
      try {Thread.sleep(ms - 3);}
      catch (Exception e) {e.printStackTrace();}
    }
    while(System.currentTimeMillis() < start + ms);
  }

  public static class IMG {
    /* Data fields */
    private int id = ThreadLocalRandom.current().nextInt();
    private boolean isPreProcessed   = false;
    private boolean isMotionDetected = false;
    private boolean isObjectDetected = false;
    private static Semaphore sp = new Semaphore(4);   // max 4 preprocess
    private static Semaphore sm = new Semaphore(256); // max 1 motion detection
    private static Semaphore so = new Semaphore(1);   // max 1 object detection
    private static Set<Long> regp =
      Collections.synchronizedSet(new HashSet<Long>());
    private static Set<Long> regm =
      Collections.synchronizedSet(new HashSet<Long>());
    private static Set<Long> rego =
      Collections.synchronizedSet(new HashSet<Long>());

    /* Constructors for both internal and external usage */
    public  IMG() {}
    public IMG(IMG other) {
      this.id = other.id;
      this.isPreProcessed   = other.isPreProcessed;
      this.isMotionDetected = other.isMotionDetected;
      this.isObjectDetected = other.isObjectDetected;
    }
    private IMG(int id,
                boolean isPreProcessed,
                boolean isMotionDetected,
                boolean isObjectDetected) {
      this.id = id;
      this.isPreProcessed   = isPreProcessed;
      this.isMotionDetected = isMotionDetected;
      this.isObjectDetected = isObjectDetected;
   }

   /* Artifically induce time penalty after each new thread */
   private void regThread(Set<Long> reg) {
     long id = Thread.currentThread().getId();
     if (!reg.contains(id)) {
       try {sp.acquire();} catch (Exception e) {e.printStackTrace();}
       countSheep(200);
       reg.add(id);
       sp.release();
     }
   }

    /* Getter methods */
    public int getId() {return id;}
    public boolean getIsPreProcessed()   {return isPreProcessed;}
    public boolean getIsMotionDetected() {return isMotionDetected;}
    public boolean getIsObjectDetected() {return isObjectDetected;}

    /* Filters */
    public void preProcess() {
      regThread(regp);
      try {sp.acquire();} catch (Exception e) {e.printStackTrace();}
      if (!isPreProcessed) {
        countSheep(8);
        isPreProcessed = true;
      }
      sp.release();
    }

    public void motionDetect() {
      regThread(regm);
      if (!isPreProcessed)
        preProcess();
      try {sm.acquire();} catch (Exception e) {e.printStackTrace();}
      if (!isMotionDetected) {
        countSheep(16);
        isMotionDetected = true;
      }
      sm.release();
    }

    public void objectDetect() throws Exception {
      regThread(rego);
      if (!isPreProcessed)
        preProcess();
      if (!isMotionDetected)
        motionDetect();
      try {so.acquire();} catch (Exception e) {e.printStackTrace();}
      if (!isObjectDetected) {
        countSheep(4);
        isObjectDetected = true;
      }
      so.release();
    }
  }
}
