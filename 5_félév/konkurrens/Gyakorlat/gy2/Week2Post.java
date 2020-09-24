public class Week2Post {

//task0
public static class MyThread extends Thread {
  public String msg;
  MyThread(String msg) {
    this.msg = msg;
  }
  public void run(){
    while(true)
      System.out.println(msg);
  }
}

/* Task1 */
private static void task1() throws Exception {
  throw new Exception("i am an exception, duh");
}

/* Task2 */
public static class MyThread2 extends Thread {
  public void run() {
    try {
      int[] numbers = {0, 1};
      numbers[1000] = 777;
    } catch(Exception e) {
        e.printStackTrace();
    }

  }
}

/* Task3 */
public static void task3(int n) {
  for (int i = 0; i < n; i++) {
    (new Thread()).start();
  }
}

/* Task4 */
public static class MyThread4 extends Thread {
  public void run() {
    while(true);
  }
}

public static void task4(int n) {
  for (int i = 0; i < n; i++) {
    (new MyThread4()).start();
  }
}

/* Task5 */
public static class MyThread5 extends Thread {
  private int id;
  MyThread5(int id) {
    this.id = id;
  }
  public void run() {
    (new MyThread5(id + 1)).start();
    System.out.println("I am number:" + id);
  }
}

/* Task6 */
public static class MyThread6 extends Thread {
  private int id;
  private int max;
  MyThread6(int id, int max) {
    this.id  = id;
    this.max = max;
  }
  public void run() {
    if (id < max)
      (new MyThread6(id + 1, max)).start();
    System.out.println("I am number:" + id);
  }
}

/* Task7 */
public static class MyThread7 extends Thread {
  private int level;
  private int id;
  MyThread7(int level, int id) {
    this.level = level;
    this.id    = id;
  }
  public void run() {
    (new MyThread7(level + 1, 2 * id)).start();
    (new MyThread7(level + 1, 2 * id + 1)).start();
    System.out.println("level:" + level + " id:" + id);
  }
}

/* Run one of the tasks in main */
  public static void main(String args[]) {
    /* Task0 */
    //MyThread mt = new MyThread("A");
    //mt.msg = "B";
    //mt.start();
    //mt.msg = "C";

    /* Task1 */
    //task1();

    /* Task2 */
    //(new MyThread2()).start();

    /* Task3 */
    //task3(100000);

    /* Task4 */
    //task4(4);

    /* Task5 */
    //(new MyThread5(0)).start();

    /* Task6 */
    //(new MyThread6(0, 10000)).start();

    /* Task7 */
    (new MyThread7(0, 0)).start();
  }
}
