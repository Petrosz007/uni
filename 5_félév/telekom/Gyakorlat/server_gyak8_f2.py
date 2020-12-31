import socket
import struct
import operator

def applyOp(in1, op, in2):
  ops = {b'+': operator.add, b'-': operator.sub, b'*': operator.mul, b'/': operator.floordiv}
  return ops[op](in1, in2)

def receive_n_send_data(unpacker, sock):
  data, client_addr = sock.recvfrom(16)
  unpacked_data = unpacker.unpack(data)
  print('Unpacked data: ' + str(unpacked_data))
  result = applyOp(*unpacked_data)
  print('Result: ' + str(result))
  sock.sendto(str(result).encode(), client_addr)
  
unpacker = struct.Struct('f c f')

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

server_address = ('localhost', 10000)
sock.bind(server_address)

receive_n_send_data(unpacker, sock)
receive_n_send_data(unpacker, sock)
receive_n_send_data(unpacker, sock)
receive_n_send_data(unpacker, sock)
receive_n_send_data(unpacker, sock)

sock.close()

sock.close()