import java.util.concurrent.*;

class Bank_sem {
  private static class Account {
    private String name;
    private String email;
    private String blablabla;

    private int balance;

    public void setBalance(int value) {
      this.balance = value;
    }
    public int getBalance() {
      return this.balance;
    }
  }

  private static class Manager implements Runnable {
    public enum Mode {
      add,
      sub
    }
    private Semaphore sem;
    private Account account;
    private Mode mode;
    private int value;
    Manager(Semaphore sem, Account account, Mode mode, int value) {
      this.sem = sem;
      this.account = account;
      this.mode = mode;
      this.value = value;
    }
    private void doStuff() {
      try {
        Thread.sleep((int)(Math.random() * 1000));
      } catch (Exception e) {System.out.println(e);}
    }
    public void run(){
      try {sem.acquire();} catch (Exception e) {System.out.println(e);}
      int currentBalance = account.getBalance();
      doStuff();
      if (mode == Mode.add)
        account.setBalance(currentBalance + value);
      else if (mode == Mode.sub)
        account.setBalance(currentBalance - value);
      sem.release();
    }
  }

  public static void main(String[] args) {
    Account account = new Account();
    account.setBalance(5000);
    Semaphore sem = new Semaphore(1);

    Thread[] threads = new Thread[5];

    threads[0] = new Thread(new Manager(sem, account, Manager.Mode.add, 200));
    threads[1] = new Thread(new Manager(sem, account, Manager.Mode.sub, 300));
    threads[2] = new Thread(new Manager(sem, account, Manager.Mode.add, 200));
    threads[3] = new Thread(new Manager(sem, account, Manager.Mode.sub, 300));
    threads[4] = new Thread(new Manager(sem, account, Manager.Mode.add, 200));

    for (int i = 0; i < 5; i++)
      threads[i].start();
    for (int i = 0; i < 5; i++)
      try {threads[i].join();} catch (Exception e) {System.out.println(e);}

    System.out.println(account.getBalance());
  }
}
