import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.TimeUnit;

public class EXample {
  public static class Whatever implements Runnable {
    public void run() {
      long start = System.currentTimeMillis();
      while(System.currentTimeMillis() < start + 1);
    }
  }

  public static void do1000st() {
    long start = System.currentTimeMillis();
    for(int i = 0; i < 1000; i++) {
      Whatever runnable = new Whatever();
      runnable.run();
    }
    long end = System.currentTimeMillis();
    long time = end - start;
    System.out.format("%d\r\n", end - start);
  }

  public static void do1000mt() {
    long start = System.currentTimeMillis();
    Thread[] threads = new Thread[1000];
    for(int i = 0; i < 1000; i++) {
      threads[i] = new Thread(new Whatever());
      threads[i].start();
    }
    for(int i = 0; i < 1000; i++) {
      try {threads[i].join();}
      catch (Exception e) {e.printStackTrace();}
    }
    long end = System.currentTimeMillis();
    long time = end - start;
    System.out.format("%d\r\n", end - start);
  }

  public static void do1000ex() {
    long start = System.currentTimeMillis();
    ExecutorService ex = Executors.newFixedThreadPool(32);
    for(int i = 0; i < 1000; i++)
      ex.execute(new Whatever());
    ex.shutdown();
    try {ex.awaitTermination(Long.MAX_VALUE, TimeUnit.SECONDS);}
    catch (InterruptedException ie) {ie.printStackTrace();}
    long end = System.currentTimeMillis();
    long time = end - start;
    System.out.format("%d\r\n", end - start);
  }

  public static void main(String[] args) {
    do1000st();
    do1000mt();
    do1000ex();
  }
}
