import java.util.Set;
import java.util.List;
import java.util.ArrayList;
import java.util.Collections;

public class Week34Post {

/* Task1 */

  /* Writes message to console using Thread */
  public static class Printer1 extends Thread {
    public String msg;
    Printer1(String msg) {
      this.msg = msg;
    }
    public void run(){
      for (int i = 0; i < 10000; i++) {
        System.out.print(msg);
      }
    }
  }

  /* Writes message to console using Runnable */
  public static class Printer2 implements Runnable {
    public String msg;
    Printer2(String msg) {
      this.msg = msg;
    }
    public void run(){
      for (int i = 0; i < 10000; i++) {
        System.out.print(msg);
      }
    }
  }

/* Task2 */

  /* Naive solution (single threaded) */
  private static List<Integer> finderST(int n) {
    long start = System.currentTimeMillis();
    List<Integer> primes = new ArrayList<>();
    for (int i=2; i<=n; i++) {
      boolean isPrime = true;
      for (int j=2; (j<(int)Math.sqrt(i)+1) && isPrime; j++)
        if (i % j == 0)
          isPrime = false;
      if (isPrime)
        primes.add(i);
    }
    long end = System.currentTimeMillis();
		System.out.format("Found %d primes in %d ms\n", primes.size(), end - start);
    return primes;
  }

  /* Naive solution (multi threaded) */
  private static class SafeList {
    private List<Integer> list = new ArrayList<>();
    public synchronized void add(int i) {
      list.add(i);
    }
    public int size() {
      return list.size();
    }
    public List<Integer> getList() {
      return list;
    }
  }

  private static List<Integer> finderMT(int n, int t_max) {
      long start = System.currentTimeMillis();
      if (t_max <= 0) t_max = 1;
      SafeList primes = new SafeList();
      Thread[] threads = new Thread[t_max];
      for (int i = 0; i < t_max; i++) {
        SubTask st = new SubTask(primes, n, i, t_max);
        threads[i] = new Thread(st);
        threads[i].start();
      }
      for (int i = 0; i < t_max; i++)
        try {threads[i].join();} catch(Exception e) {System.out.println(e);}
      long end = System.currentTimeMillis();
  		System.out.format("Found %d primes in %d ms\n", primes.size(), end - start);
      return primes.getList();
    }

  public static class SubTask implements Runnable {
    private SafeList primes;
    private int n;
    private int t_id;
    private int t_max;
    SubTask(SafeList primes, int n, int t_id, int t_max) {
      this.primes = primes;
      this.n = n;
      this.t_id = t_id;
      this.t_max = t_max;
    }
    public void run(){
      for (int i = 2 + t_id; i <= n; i += t_max) {
        boolean isPrime = true;
        for (int j = 2; (j < (int)Math.sqrt(i) + 1) && isPrime; j++)
          if (i % j == 0)
            isPrime = false;
        if (isPrime)
          primes.add(i);
      }
    }
  }



/* Run one of the tasks in main */
  public static void main(String args[]) {
  /* Task1 */
    //int reps = 1; // amount of letters to write to console at a time

    /* Writes message to console using Printer1 */
    //Thread t1 = new Printer1("A".repeat(reps));
    //t1.start();

    /* Writes message to console using Printer2 */
    //Thread t2 = new Thread(new Printer2("B".repeat(reps)));
    //t2.start();

    /* Writes message to console using lambda */
    //Thread t3 = new Thread(() -> {
    //  for (int i = 0; i < 10000; i++) {
    //    System.out.print("C".repeat(reps));
    //  }
    //});
    //t3.start();

    /* Wait for threads to finish before continuing main */
    //try {
    //  t1.join();
    //  t2.join();
    //  t3.join();
    //} catch (Exception e) {System.out.println("Exception occurred:" + e);}

  /* Task2 */
    int t_opt = Runtime.getRuntime().availableProcessors();

    List<Integer> primes1 = finderST(5_000_000);
    List<Integer> primes2 = finderMT(5_000_000, t_opt);

    assert primes1.equals(primes2);
    assert Set.copyOf(primes1).equals(Set.copyOf(primes2));
  }
}
