import java.util.Set;
import java.util.HashSet;

public class Server {

	private static class ClientHandler implements Runnable {
		private Set<Connection> connections;
		private Connection myClient;
		public ClientHandler(Set<Connection> connections, Connection myClient) {
			this.connections = connections;
			this.myClient  = myClient;
		}
		public void run() {
			String name = "anon";
			try {
				name = myClient.receive();
				System.out.format("'%s' has connected\n", name);
				while (true){
					String msg = myClient.receive();
					System.out.format("<%s>: %s\n", name, msg);
					for (Connection connection : connections)
						if (connection != myClient)
							connection.send(String.format("<%s>: %s", name, msg));
				}
			} catch (Exception e){
				connections.remove(myClient);
				System.out.format("%s has disconnected\n", name);
				System.out.format("Connected users: %s\n", connections.size());
			}
		}
	}

	public static void main( String[] args ) {
		System.out.println("Server is running...");
		Set<Connection> connections = new HashSet<>();
		while (true) {
			System.out.format("Connected users: %s\n", connections.size());
			try {
				Connection newClient = Connection.accept();
				connections.add(newClient);
				new Thread(new ClientHandler(connections, newClient)).start();
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
	}
}
