import java.util.List;
import java.util.ArrayList;
import java.io.*;

class PrimesPipe {
  /* Single-threaded prime finder */
  private static List<Integer> finderST(long bound) {
    if (bound >= Integer.MAX_VALUE / 2)
      throw new IllegalArgumentException("Boundary is too large.");
    long start = System.currentTimeMillis();
    int n = (int)bound;
    List<Integer> primes = new ArrayList<>((int)(2 * n / Math.log(n - 1)));
    primes.add(2);
    boolean[] nums = new boolean[n+1];
    for (int i=3; i<=(int)Math.sqrt(n)+1; i+=2) {
      if (nums[i])
        continue;
      for (int j=i*i; j<=n; j+=i)
        nums[j] = true;
    }
    for (int i=3; i<=n; i+=2) {
      if (nums[i])
        continue;
      primes.add(i);
    }
    long end = System.currentTimeMillis();
		System.out.format("Found %d primes in %d ms\n", primes.size(), end - start);
    return primes;
  }

  /* Multi-threaded prime finder */
  public static class Filter implements Runnable {
    private int myprime;
    private List<Integer> primes;
    private DataInputStream in;
    Filter(List<Integer> primes, InputStream in) {
      this.primes = primes;
      this.in = new DataInputStream(in);
    }
    public void run() {
      try {
        myprime = in.readInt();
        if(myprime == -1)
          return;
        primes.add(myprime);
        int next = 0;
        while(next != -1 && next % myprime == 0)
          next = in.readInt();
        if(myprime == -1)
          return;
        PipedInputStream oin = new PipedInputStream();
        DataOutputStream oout = new DataOutputStream(new PipedOutputStream(oin));
        Thread other = new Thread(new Filter(primes, oin));
        other.start();
        while(next != -1) {
          if(next % myprime != 0) {
            oout.writeInt(next);
            //System.out.println("im sending: " + next);
            oout.flush();
          }
          next = in.readInt();
        }
        in.close();
        oout.writeInt(-1);
        oout.flush();
        other.join();
      } catch (Exception e) {e.printStackTrace();}
    }
  }

  private static List<Integer> finderMT(long bound) throws Exception {
    if (bound >= Integer.MAX_VALUE / 2)
      throw new IllegalArgumentException("Boundary is too large.");
    long start = System.currentTimeMillis();
    int n = (int)bound;
    List<Integer> primes = new ArrayList<>((int)(2 * n / Math.log(n - 1)));
    primes.add(2);
    try {
      PipedInputStream in = new PipedInputStream();
      DataOutputStream out = new DataOutputStream(new PipedOutputStream(in));
      Thread filter = new Thread(new Filter(primes, in));
      filter.start();
      for (int p = 3; p <= n; p += 2) {
        out.writeInt(p);
        out.flush();
      }
      out.writeInt(-1);
      out.flush();
      filter.join();
    } catch (Exception e) {e.printStackTrace();}
    long end = System.currentTimeMillis();
		System.out.format("Found %d primes in %d ms\n", primes.size(), end - start);
    return primes;
  }

  public static void main(String[] args) throws Exception {
    List<Integer> primes1 = finderST(10_000);
    List<Integer> primes2 = finderMT(10_000);

    boolean correctness = true;
    if (primes1.size() == primes2.size()) {
      for (int i = 0; i < primes1.size(); i++) {
        if (!primes1.get(i).equals(primes2.get(i))) {
          correctness = false;
        }
      }
    }
    else {
      correctness = false;
    }
    System.out.println("Correct: " + correctness);
  }
}
