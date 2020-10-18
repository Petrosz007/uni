import java.io.*;
import java.util.concurrent.atomic.AtomicBoolean;

public class Client {
	private static class ClientReciever implements Runnable {
		private Connection connection;
		private AtomicBoolean run;
		public ClientReciever(Connection connection, AtomicBoolean run) {
			this.connection = connection;
			this.run = run;
		}
		public void run() {
			while (run.get())
				try {
					System.out.println(connection.receive());
				} catch (Exception e) {
					run.set(false);
					e.printStackTrace();
				}
		}
	}
	public static void main( String[] args ) {
		String name = args.length > 0 ? args[0] : "anon";
		String msg  = new String();
		AtomicBoolean run = new AtomicBoolean(true);
		try( Connection connection = Connection.connect() ){
			new Thread(new ClientReciever(connection, run)).start();
			connection.send(name);
			InputStreamReader reader = new InputStreamReader(System.in);
			BufferedReader bufReader = new BufferedReader(reader);
			while(!msg.equals("\\q") && run.get()) {
				msg = bufReader.readLine();
				connection.send(msg);
			}
			run.set(false);
		} catch (Exception e) {run.set(false);}
	}
}
