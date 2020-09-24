import java.util.List;
import java.util.ArrayList;

class PrimesPost {
  private static List<Integer> finder1(int n) {
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

  private static List<Integer> finder2(int n) {
    long start = System.currentTimeMillis();
    List<Integer> primes = new ArrayList<>();
    boolean[] nums = new boolean[n+1];
    for (int i=2; i<=n; i++) {
      if (nums[i])
        continue;
      primes.add(i);
      for (int j=i; j<=n; j+=i)
        nums[j] = true;
    }
    long end = System.currentTimeMillis();
		System.out.format("Found %d primes in %d ms\n", primes.size(), end - start);
    return primes;
  }

  private static List<Integer> finder2opt(long bound) {
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

  public static void main(String[] args) {
    finder1(10_000_000);
    finder2(10_000_000);
    finder2opt(10_000_000L);
  }
}
