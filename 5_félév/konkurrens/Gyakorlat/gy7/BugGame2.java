import java.io.*;

public class BugGame2 {
  private static enum Dir {
    up, right, down, left
  }

  public static class Pos {
    int x, y;
    public Pos(int x, int y) {
      this.x = x;
      this.y = y;
    }
    public Pos adj(Dir dir) {
      Pos adjPos = new Pos(x, y);
      switch(dir) {
        case up:
          adjPos.x--;
          break;
        case right:
          adjPos.y++;
          break;
        case down:
          adjPos.x++;
          break;
        case left:
          adjPos.y--;
          break;
      }
      return adjPos;
    }
    public boolean outside() {
      boolean isOutside;
      if(x < 0 || y < 0 || 16 <= x || 16 <= y)
        isOutside = true;
      else
        isOutside = false;
      return isOutside;
    }
  }

  public static class Bug {
    public Pos pos;
    public Dir dir;
    public Bug(Pos pos, Dir dir) {
      this.pos = pos;
      this.dir = dir;
    }
    public boolean move() {
      Pos next = pos.adj(dir);
      if(next.outside())
        return false;
      else
        pos = next;
        return true;
    }
  }

  public static class Logic implements Runnable {
    private Bug bug;
    private boolean alive;
    public Logic(Bug bug) {
      this.bug = bug;
      this.alive = true;
    }
    public boolean isAlive() {
      return alive;
    }
    private void cls() {
      try {
          final String os = System.getProperty("os.name");
          if (os.contains("Windows"))
            new ProcessBuilder("cmd", "/c", "cls").inheritIO().start().waitFor();
          else
            Runtime.getRuntime().exec("clear");
          }
      catch(Exception e) {e.printStackTrace();}
    }
    private void render() {
      cls();
      int map[][] = new int[16][16];
      map[bug.pos.x][bug.pos.y] = 1;
      System.out.println("----------------");
      for (int i = 0; i < 16; i++) {
        for (int j = 0; j < 16; j++) {
          switch(map[i][j]) {
            case 0: {
              System.out.print(" ");
              break;
            }
            case 1:
              System.out.print("O");
              break;
          }
        }
        System.out.print("|\n");
      }
      System.out.println("----------------");
    }
    public void run() {
      while(alive) {
        render();
        try {Thread.sleep(1000);} catch(Exception e) {e.printStackTrace();}
        synchronized(bug) {
          alive = bug.move();
        }
      }
      System.out.println("Game Over :(");
    }
  }

  public static void main(String args[]) {
    Bug bug = new Bug(new Pos(0,0), Dir.right);
    Logic logic = new Logic(bug);
    new Thread(logic).start();
    InputStreamReader reader = new InputStreamReader(System.in);
    BufferedReader bufReader = new BufferedReader(reader);
    while(logic.isAlive()) {
      String order = new String();
      try {order = bufReader.readLine();}
      catch (Exception e) {e.printStackTrace();}
      synchronized(bug) {
        switch(order) {
          case "u":
            bug.dir = Dir.up;
            break;
          case "r":
            bug.dir = Dir.right;
            break;
          case "d":
            bug.dir = Dir.down;
            break;
          case "l":
            bug.dir = Dir.left;
            break;
        }
      }
    }
  }
}
