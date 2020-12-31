import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.Arrays;
import java.util.concurrent.TimeUnit;

/* *** Story time ***
  Let me introduce you to John. He's a smart security camera.
  John can not only see, but he can also analyze images. Analysing an image
  takes the following 3 steps:
    1. John preprocesses the image (contrast enhancment and other fancy
       stuff). Johnny uses his 4 core ARM CPU for this, so he can't
       preprocess more than 4 images simultaniously.
    2. John has a dedicated motion detection chip with a whopping 256 cores.
       If only we could take advantage of that somehow... Unfortunately
       images must be preprocessed before the motion detector can work with
       them.
    3. Finally, once motion is detected John can send the processed images
       to the datacenter where a supercomputer classifies the object in motion.
       The main bottleneck here is the latency induced by the network
       communication, so the speed of this operation is independant of the
       characteristics of the actual image. Every classification task takes the
       same amount of time.
  John might be cool, but he can't write his own software, so help him out!

  Some advice:
    1. John manages his resources automatically. For example if you call the
       function preProcess() 5 times at the same time the 5th call will simply
       block until one of John's CPU cores is freed up.
    2. You can call motionDetect() without calling preProcess() or call
       objectDetect() without calling motionDetect() first. In these cases
       all preliminary methods will be called automatically.
    3. There are 4 resources:
      3.1. time (ms) - each action freezes the calling thread for some time
      3.2. CPU cores - some action requires a CPU core from John
      3.3. MD (motion detector) cores - other actions can require MD cores
      3.4. Network - John likes high-res images... Unfortunately for us, that
           means we can only send one image at a time to our beloved datacenter
    4. The following filters use up the following amount of resources:
      4.1. preProcess()   - 8 ms, 1 CPU core
      4.2. motionDetect() - 16 ms, 1 MD core
      4.3. objectDetect() - 4 ms, Network
    5. You cannot simply hand fresh threads over to Johnny. Each of the 3 steps
       require some stuff to be set up. Filters have to be loaded for
       preprocessing. Network connection has to be established for object
       classification and only God knows what crap that proprietary MD chip
       needs. Tl;Dr - every time you run an action on a thread where that
       action has never set foot before you will need an additional 200 ms of
       time and 1 CPU core for the duration to complete the action.

*/

public class Pipe {

  /* *** No mark ***
    Input:  array of IMG objects
    Output: exact same array
    tip:    Life is too short to bother with exams ¯\_(ツ)_/¯
  */

  public static Tools.IMG[] mark0(Tools.IMG[] imgs) {
    return imgs;
  }

  /* *** mark 1 (fail) ***
    Input:  array of IMG objects
    Output: same objects with all 3 filters applied
    Speed:  less than 4500 ms / 100 images
    tip:    apply all 3 filters for all images
  */

  public static Tools.IMG[] mark1(Tools.IMG[] imgs) {
    for (Tools.IMG img : imgs) {
      try {
        img.objectDetect();
      } catch (Exception e) {e.printStackTrace();}
    }
    return imgs;
  }

  /* *** mark 2 (enough) ***
    Input:  array of IMG objects
    Output: same objects with all 3 filters applied
    Speed:  less than 3000 ms / 100 images
    tip:    process half the frames on a new thread
  */

  public static class Helper2 implements Runnable {
    private Tools.IMG[] imgs;
    public Helper2(Tools.IMG[] imgs) {
      this.imgs = imgs;
    }
    public void run() {
      for (int i = 1; i < imgs.length; i += 2) {
        try {imgs[i].objectDetect();}
        catch (Exception e) {e.printStackTrace();}
      }
    }
  }

  public static Tools.IMG[] mark2(Tools.IMG[] imgs) {
    Thread helper = new Thread(new Helper2(imgs));
    helper.start();
    for (int i = 0; i < imgs.length; i += 2) {
      try {imgs[i].objectDetect();}
      catch (Exception e) {e.printStackTrace();}
    }
    try {helper.join();} catch (Exception e) {e.printStackTrace();}
    return imgs;
  }

  /* *** mark 3 (fair) ***
    Input:  array of IMG objects
    Output: same objects with all 3 filters applied
    Speed:  less than 2000 ms / 100 images
    tip1:   lunch multiple threads from a for loop
    tip2:   Don't go overboard, sometimes less is more
  */

  public static class Helper3 implements Runnable {
    private Tools.IMG[] imgs;
    private int id;
    public Helper3(Tools.IMG[] imgs, int id) {
      this.imgs = imgs;
      this.id  = id;
    }
    public void run() {
      for (int i = id; i < imgs.length; i += 7) {
        try {imgs[i].objectDetect();}
        catch (Exception e) {e.printStackTrace();}
      }
    }
  }

  public static Tools.IMG[] mark3(Tools.IMG[] imgs) {
    Thread[] threads = new Thread[7];
    for (int i = 0; i < threads.length; i++) {
      threads[i] = new Thread(new Helper3(imgs, i));
      threads[i].start();
    }
    for (int i = 0; i < threads.length; i++)
      try {threads[i].join();} catch (Exception e) {e.printStackTrace();}
    return imgs;
  }

  /* *** mark 4 (good) ***
    Input:  array of IMG objects
    Output: same objects with all 3 filters applied
    Speed:  less than 1500 ms / 100 images
    tip1:   connection to the cloud cannot be parallelized, so try
            object detection in main, use Thread for everything else
    tip2:   queues can come in handy if you need to connect different
            parts of your code
  */

  public static class Helper4 implements Runnable {
    private Tools.IMG[] imgs;
    private int id;
    private LinkedBlockingQueue<Tools.IMG> que;
    public Helper4(Tools.IMG[] imgs,
                   int id,
                   LinkedBlockingQueue<Tools.IMG> que) {
      this.imgs = imgs;
      this.id   = id;
      this.que  = que;
    }
    public void run() {
      for (int i = id; i < imgs.length; i += 6) {
        try {imgs[i].motionDetect();}
        catch (Exception e) {e.printStackTrace();}
        que.add(imgs[i]);
      }
    }
  }

  public static Tools.IMG[] mark4(Tools.IMG[] imgs) {
    Thread[] threads = new Thread[6];
    LinkedBlockingQueue<Tools.IMG> que = new LinkedBlockingQueue<>();
    for (int i = 0; i < threads.length; i++) {
      threads[i] = new Thread(new Helper4(imgs, i, que));
      threads[i].start();
    }
    for (int i = 0; i < imgs.length; i++) {
      try {que.take().objectDetect();}
      catch (Exception e) {e.printStackTrace();}
    }
    return imgs;
  }

  /* *** mark 5 (excelent) ***
    Input:  array of IMG objects
    Output: same objects with all 3 filters applied
    Speed:  less than 1000 ms / 100 images
    tip1:   build pipeline, connect elements with queues
    tip2:   2 threads for preprocessing, 4 threads for motion detection
  */

  public static class Helper5P implements Runnable {
    private int id;
    private Tools.IMG[] imgs;
    private LinkedBlockingQueue<Tools.IMG> que;
    public Helper5P(int id,
                    Tools.IMG[] imgs,
                    LinkedBlockingQueue<Tools.IMG> que) {
      this.id   = id;
      this.imgs = imgs;
      this.que  = que;
    }
    public void run() {
      for (int i = id; i < imgs.length; i += 2) {
        try {imgs[i].preProcess();}
        catch (Exception e) {e.printStackTrace();}
        que.add(imgs[i]);
      }
    }
  }

  public static class Helper5M implements Runnable {
    private int countdown;
    private LinkedBlockingQueue<Tools.IMG> from;
    private LinkedBlockingQueue<Tools.IMG> to;
    public Helper5M(int countdown,
                   LinkedBlockingQueue<Tools.IMG> from,
                   LinkedBlockingQueue<Tools.IMG> to) {
      this.countdown = countdown;
      this.from      = from;
      this.to        = to;
    }
    public void run() {
      while (0 < countdown--) {
        try {
          Tools.IMG img = from.take();
          img.motionDetect();
          to.add(img);
        } catch (Exception e) {e.printStackTrace();}
      }
    }
  }

  public static Tools.IMG[] mark5(Tools.IMG[] imgs) {
    LinkedBlockingQueue<Tools.IMG> quep = new LinkedBlockingQueue<>();
    LinkedBlockingQueue<Tools.IMG> quem = new LinkedBlockingQueue<>();
    Thread[] tps = new Thread[2];
    for (int i = 0; i < tps.length; i++) {
      tps[i] = new Thread(new Helper5P(i, imgs, quep));
      tps[i].start();
    }
    Thread[] tpm = new Thread[4];
    for (int i = 0; i < tpm.length; i++) {
      int countdown = imgs.length / 4 + (i < imgs.length % 4 ? 1 : 0);
      tpm[i] = new Thread(new Helper5M(countdown, quep, quem));
      tpm[i].start();
    }
    for (int countdown = imgs.length; 0 < countdown; countdown--) {
      try {quem.take().objectDetect();}
      catch (Exception e) {e.printStackTrace();}
    }
    return imgs;
  }

  /* *** alternative solution ***
    Notes:
      - based on solution 'mark5'
      - similar performance, significantly less lines of code
      - implemented using executor
  */
  public static class Helper5b implements Runnable {
    private LinkedBlockingQueue<Tools.IMG> from;
    private LinkedBlockingQueue<Tools.IMG> to;
    public Helper5b(LinkedBlockingQueue<Tools.IMG> from,
                     LinkedBlockingQueue<Tools.IMG> to) {
      this.from = from;
      this.to   = to;
    }
    public void run() {
      try {
        Tools.IMG img = from.take();
        if (!img.getIsPreProcessed())
          img.preProcess();
        else
          img.motionDetect();
        to.add(img);
      } catch (Exception e) {e.printStackTrace();}
    }
  }

  public static Tools.IMG[] mark5b(Tools.IMG[] imgs) {
    LinkedBlockingQueue<Tools.IMG> quep = new LinkedBlockingQueue<>(Arrays.asList(imgs));
    LinkedBlockingQueue<Tools.IMG> quem = new LinkedBlockingQueue<>();
    LinkedBlockingQueue<Tools.IMG> queo = new LinkedBlockingQueue<>();
    final ExecutorService exp = Executors.newFixedThreadPool(2);
    final ExecutorService exm = Executors.newFixedThreadPool(4);
    for (Tools.IMG img : imgs) {
      exp.execute(new Helper5b(quep, quem));
      exm.execute(new Helper5b(quem, queo));
    }
    exp.shutdown();
    exm.shutdown();
    for (Tools.IMG img : imgs) {
      try {queo.take().objectDetect();}
      catch (Exception e) {e.printStackTrace();}
    }
    return imgs;
  }
}
