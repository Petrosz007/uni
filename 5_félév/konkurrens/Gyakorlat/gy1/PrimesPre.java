import java.util.List;
import java.util.ArrayList;

class PrimesPre {
  private static List<Integer> finder1(int n) {
    long start = System.currentTimeMillis();
    List<Integer> primes = new ArrayList<>();

    // TODO check all numbers from 2 to n, add to 'primes' if a prime number

    long end = System.currentTimeMillis();
		System.out.format("Found %d primes in %d ms\n", primes.size(), end - start);
    return primes;
  }

  private static List<Integer> finder2(int n) {
    long start = System.currentTimeMillis();
    List<Integer> primes = new ArrayList<>();

    // TODO represent sieve with boolean array (all values are false by default)
    // Run loop from 2 to n:
    //   - if corresponding value in the bool array is false (default value),
    //     then we have a prime. Change all multiplicatives to 'true' and add
    //     number to 'primes'.
    //   - if corresponding value in the bool array is true -> skip (continue)

    long end = System.currentTimeMillis();
		System.out.format("Found %d primes in %d ms\n", primes.size(), end - start);
    return primes;
  }

  private static List<Integer> finder3(int n) {
    long start = System.currentTimeMillis();
    List<Integer> primes = new ArrayList<>();

    // TODO similar to 'finder1', use already found primes to check each number

    long end = System.currentTimeMillis();
		System.out.format("Found %d primes in %d ms\n", primes.size(), end - start);
    return primes;
  }

  public static void main(String[] args) {
    finder1(10_000_000);
    finder2(10_000_000);
    finder3(10_000_000);
  }
}
