public class Server {
	public static void main( String[] args ) {
		try( Connection connection = Connection.accept() ){
			while( true ){
				connection.send( connection.receive().toUpperCase() );
			}
		} catch( java.io.IOException  e ){
			System.err.println("No further incoming messages from the client.");
		}
	}
}
