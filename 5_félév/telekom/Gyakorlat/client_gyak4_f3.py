import socket
import sys

class SimpleTCPSelectClient:
  def __init__(self, serverAddr='localhost', serverPort=10001):
    self.setupClient(serverAddr, serverPort)

  def setupClient(self, serverAddr, serverPort):
    server_address = (serverAddr, serverPort)

    # Create a TCP/IP socket
    self.client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    
    # Connect the socket to the port where the server is listening
    self.client.connect(server_address)
  
  def handleIncomingMessageFromRemoteServer(self):
        data = self.client.recv(4096).decode('utf-8')
        if not data :
          print('\nDisconnected from server')
          sys.exit()
        else:
          print(str(data))
  
  def handleConnection(self):
    while True:
        msg = input('Message: ')
        if msg != '':
          msg = msg.strip()
          self.client.sendall(msg.encode())
        self.handleIncomingMessageFromRemoteServer()

simpleTCPSelectClient = SimpleTCPSelectClient()
simpleTCPSelectClient.handleConnection()