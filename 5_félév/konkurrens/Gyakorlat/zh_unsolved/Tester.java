import java.util.stream.*;
import java.util.function.*;

public class Tester {

  public static Tools.IMG[] deepCopy(Tools.IMG[] imgs) {
    Tools.IMG[] other = new Tools.IMG[imgs.length];
    for (int i = 0; i < imgs.length; i++)
      other[i] = new Tools.IMG(imgs[i]);
    return other;
  }

  public static boolean check(String name,
                              Tools.IMG[] orig,
                              Function<Tools.IMG[], Tools.IMG[]> mark,
                              long tlimit) {
    boolean correct = true;
    long start = System.currentTimeMillis();
    Tools.IMG[] res = mark.apply(deepCopy(orig));
    long end   = System.currentTimeMillis();
    for (int i = 0; i < orig.length; i++) {
      if (orig[i].getId() != res[i].getId() ||
          !res[i].getIsPreProcessed()   ||
          !res[i].getIsMotionDetected() ||
          !res[i].getIsObjectDetected())
        correct = false;
    }
    long time = end - start;
    System.out.format("%s %d %d %b \r\n", name, time, tlimit, correct);
    return correct && time <= tlimit;
  }

  public static void main(String args[]) {
    /* Generate raw images */
    Tools.IMG[] imgs = Stream.generate(() ->
      new Tools.IMG()).limit(100).toArray(Tools.IMG[]::new);

    /* Push images through pipe */
    int mark = 0;
    if (check("mark0", imgs, Pipe::mark0,    0)) mark = 0;
    if (check("mark1", imgs, Pipe::mark1, 4500)) mark = 1;
    if (check("mark2", imgs, Pipe::mark2, 3000)) mark = 2;
    if (check("mark3", imgs, Pipe::mark3, 2000)) mark = 3;
    if (check("mark4", imgs, Pipe::mark4, 1500)) mark = 4;
    if (check("mark5", imgs, Pipe::mark5, 1000)) mark = 5;

    /* Print mark */
    System.out.println("Your mark is: " + mark);
  }
}
