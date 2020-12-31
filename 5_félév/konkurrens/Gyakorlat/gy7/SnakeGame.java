import java.util.List;
import java.util.ArrayList;
import java.util.Random;
import java.io.*;

public class SnakeGame {

  private static enum Dir {
    up,
    right,
    down,
    left
  }

  private static enum Block {
    free,
    wall,
    food
  }

  private static class Pos {
    public int x, y;
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
      if (x < 0 || 16 <= x || y < 0 || 16 <= y)
        isOutside = true;
      else
        isOutside = false;
      return isOutside;
    }
  }

  private static class Snake {
    public Pos pos;
    public Dir dir;
    public List<Pos> tail;
    public Snake(Pos pos, Dir dir) {
      this.pos = pos;
      this.dir = dir;
      this.tail = new ArrayList<>();
    }
    public void changeDir(Dir dir) {
      this.dir = dir;
    }
    public boolean move(Pos food) {
      Pos next = pos.adj(dir);
      Block block;
      if (next.outside() || tail.contains(next))
        block = Block.wall;
      else if (food.x == next.x && food.y == next.y)
        block = Block.food;
      else
        block = Block.free;
      moveTail(block);
      moveHead(block);
      return block == Block.food;
    }

    public void moveHead(Block block) {
      if (block != Block.wall) {
        pos = pos.adj(dir);
      }
    }

    public void moveTail(Block block) {
      if(block != Block.wall)
        tail.add(pos);
      if (block != Block.food && !tail.isEmpty())
        tail.remove(0);
    }
  }

  private static class Logic implements Runnable {
    Snake snake;
    Pos food;
    public Logic(Snake snake) {
      this.snake = snake;
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
    private Pos genFood() {
      Random rand = new Random();
      return new Pos(rand.nextInt(16), rand.nextInt(16));
    }
    private void render() {
      cls();
      int map[][] = new int[16][16];
      for (Pos pos : snake.tail)
        map[pos.x][pos.y] = 1;
      map[snake.pos.x][snake.pos.y] = 2;
      map[food.x][food.y] = 3;
      System.out.print("----------------\n");
      for (int i = 0; i < 16; i++) {
        for (int j = 0; j < 16; j++) {
          switch (map[i][j]) {
            case 0:
              System.out.print(' ');
              break;
            case 1:
              System.out.print('X');
              break;
            case 2:
              System.out.print('O');
              break;
            case 3:
              System.out.print('$');
              break;
          }
        }
        System.out.print("|\n");
      }
      System.out.print("----------------\n");
    }
    public void run() {
      this.food = genFood();
      while(true) {
        render();
        try {Thread.sleep(1000);}
        catch(Exception e) {e.printStackTrace();}
        synchronized(snake) {
          boolean eaten = snake.move(food);
          if (eaten)
            food = genFood();
        }
      }
    }
  }

  public static void main(String args[]) {
    Snake snake = new Snake(new Pos(0, 0), Dir.right);
    new Thread(new Logic(snake)).start();
    InputStreamReader reader = new InputStreamReader(System.in);
    BufferedReader bufReader = new BufferedReader(reader);
    while(true) {
      String order = new String();
      try {order = bufReader.readLine();}
      catch (Exception e) {e.printStackTrace();}
      synchronized(snake) {
        switch(order) {
          case "u":
            snake.dir = Dir.up;
            break;
          case "r":
            snake.dir = Dir.right;
            break;
          case "d":
            snake.dir = Dir.down;
            break;
          case "l":
            snake.dir = Dir.left;
            break;
        }
      }
    }
  }
}
