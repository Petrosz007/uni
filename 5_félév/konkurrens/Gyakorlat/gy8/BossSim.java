import java.util.List;
import java.util.ArrayList;
import java.util.concurrent.ThreadLocalRandom;
import java.util.concurrent.atomic.*;
import java.util.function.IntBinaryOperator;
import java.util.*;

public class BossSim {

  private final static int dungeonsize = 4;

  private static void cooldown(int ms) {
    try {Thread.sleep(ms);} catch(Exception e) {e.printStackTrace();}
  }

  private static int roll(int k) {
    return ThreadLocalRandom.current().nextInt(0, k);
  }

  private static abstract class Character {
    private final String name;
    private final String classname;
    private AtomicInteger hp;
    private AtomicBoolean alive;
    private final int maxhp;
    private final int atk;
    private final int atkspeed;
    private final int heal;
    private final int healspeed;
    private final int preference;
    public Character(String name,
                     String classname,
                     int hp,
                     boolean alive,
                     int maxhp,
                     int atk,
                     int atkspeed,
                     int heal,
                     int healspeed,
                     int preference) {
      this.name       = name;
      this.classname  = classname;
      this.hp         = new AtomicInteger(hp);
      this.alive      = new AtomicBoolean(alive);
      this.maxhp      = maxhp;
      this.atk        = atk;
      this.atkspeed   = atkspeed;
      this.heal       = heal;
      this.healspeed  = healspeed;
      this.preference = preference;
    }
    public void attack(Character foe) {
      int hpleft = foe.hp.addAndGet(-1 * atk);
      System.out.format("%s (%s) attacked %s (%s) for %d damage\r\n",
        name, classname, foe.name, foe.classname, atk);
      System.out.format("%s (%s) has %d hp left\r\n",
        foe.name, foe.classname, hpleft);
      if (hpleft <= 0)
        foe.alive.set(false);
      cooldown(atkspeed);
    }
    public void heal(Character ally) {
      IntBinaryOperator addhp =
        (x, y) -> ((x + y <= ally.maxhp) ? (x + y) : ally.maxhp);
      int hpnow = ally.hp.accumulateAndGet(heal, addhp);
      System.out.format("%s (%s) healed %s (%s) for %d hp\r\n",
        name, classname, ally.name, ally.classname, heal);
      System.out.format("%s (%s) has %d hp now\r\n",
        ally.name, ally.classname, hpnow);
      cooldown(healspeed);
    }
    public void auto(Character foe, Character ally) {
      int dice = roll(20);
      if (dice < preference)
        attack(foe);
      else
        heal(ally);
    }
  }

  private static class Healer extends Character {
    public Healer(String name) {
      super(name, "Healer", 200, true, 200, 20, 3000, 40, 1000, 5);
    }
  }

  private static class Warrior extends Character {
    public Warrior(String name) {
      super(name, "Warrior", 350, true, 350, 50, 500, 0, 0, 20);
    }
  }

  private static class Battlemage extends Character {
    public Battlemage(String name) {
      super(name, "Battlemage", 300, true, 300, 60, 1200, 30, 1500, 10);
    }
  }

  private static class Tank extends Character {
    public Tank(String name) {
      super(name, "Tank", 800, true, 800, 80, 2500, 0, 0, 20);
    }
  }

  private static class Boss extends Character {
    public Boss(String name) {
      super(name, "Boss", 3000, true, 3000, 100, 1000, 300, 1500, 12);
    }
  }

  private static Character chargen(String name, boolean isBoss) {
    if (!isBoss) {
      int select = roll(4);
      switch(select) {
        case 0:
          return new Healer(name);
        case 1:
          return new Warrior(name);
        case 2:
          return new Battlemage(name);
        case 3:
          return new Tank(name);
      }
    }
    return new Boss(name);
  }

  private static class Autoplayer implements Runnable {
    private Character me;
    private List<Character> allies;
    private List<Character> foes;
    public Autoplayer(Character ch,
                      List<Character> allies,
                      List<Character> foes) {
      this.me     = ch;
      this.allies = allies;
      this.foes   = foes;
    }
    public void run() {
      while(me.alive.get() && !foes.isEmpty()) {
        Character ally = allies.stream()
          .min(Comparator.comparing(ch -> ch.hp.get()))
          .get();
        Character  foe = foes.get(roll(foes.size()));
        me.auto(foe, ally);
      }
      if (!me.alive.get())
        allies.remove(me);
    }
  }

  public static void main(String args[]) {
    List<Character> team = Collections.synchronizedList(new ArrayList<>());
    List<Character> boss = Collections.synchronizedList(new ArrayList<>());
    Thread[] teamplayers = new Thread[dungeonsize];
    Thread[] bossplayers = new Thread[1];
    for (int i = 0; i < dungeonsize; i++)
      team.add(chargen(String.format("player%d", i), false));
    boss.add(chargen("Big Scary Monster", true));
    for (int i = 0; i < dungeonsize; i++) {
      teamplayers[i] = new Thread(new Autoplayer(team.get(i), team, boss));
      teamplayers[i].start();
    }
    bossplayers[0] = new Thread(new Autoplayer(boss.get(0), boss, team));
    bossplayers[0].start();
    try {
      for (int i = 0; i < dungeonsize; i++)
        teamplayers[i].join();
      bossplayers[0].join();
    } catch (Exception e) {e.printStackTrace();}
  }
}
