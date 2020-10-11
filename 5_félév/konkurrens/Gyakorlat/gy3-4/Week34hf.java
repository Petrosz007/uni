import java.util.*;

public class Week34hf {

  public static class SubTask implements Runnable {
    // TODO: implement class
    public void run(){
      // TODO: override run()
    }
  }

  public static void main(String args[]) {

    // init: a, b are random, c initialized to all zeros
    final int m = 1000;
    final int n = 500;
    final int p = 2000;
    int[][] a = new int[m][n];
    int[][] b = new int[n][p];
    int[][] c1 = new int[m][p];
    int[][] c2 = new int[m][p];
    for(int ni = 0; ni < n; ni++) {
      for(int mi = 0; mi < m; mi++)
        a[mi][ni] = (int)(Math.random() * 10);
      for(int pi = 0; pi < p; pi++)
        b[ni][pi] = (int)(Math.random() * 10);
    }

    // c1 := a * b (single threaded)
    long start1 = System.currentTimeMillis();
    for(int mi = 0; mi < m; mi++)
      for(int ni = 0; ni < n; ni++)
        for(int pi = 0; pi < p; pi++)
          c1[mi][pi] += a[mi][ni] * b[ni][pi];
    long end1 = System.currentTimeMillis();
    System.out.format("m = %d, n = %d, p = %d, time: %d ms\n", m, n, p, end1 - start1);

    // c2 := a * b (multi threaded)
    long start2 = System.currentTimeMillis();
      // TODO: some code goes here
    long end2 = System.currentTimeMillis();
    System.out.format("m = %d, n = %d, p = %d, time: %d ms\n", m, n, p, end2 - start2);

    // Check correctness
    Boolean isValid = true;
    for (int i = 0; i < m; i++)
      for (int j = 0; j < p; j++)
        if (c1[i][j] != c2[i][j])
          isValid = false;
    System.out.format("Solution is correct: %b\n", isValid);

  }
}
