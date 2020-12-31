import socket
import struct
import time

def send_n_receive_values(server_address, values, packer, connection):
  packed_data = packer.pack(*values)
  print('%f %s %f' % values)
  connection.sendto(packed_data, server_address)
  result, server_addr = connection.recvfrom(16)
  print(result)

packer = struct.Struct('f c f')
connection = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

server_address = ('localhost', 10000)

values = (7.0, b'-', 2.0)

send_n_receive_values(server_address, values, packer, connection)

time.sleep(2)

values = (9.0, b'+', 3.0)

send_n_receive_values(server_address, values, packer, connection)

time.sleep(2)

values = (2.0, b'*', 3.0)

send_n_receive_values(server_address, values, packer, connection)

time.sleep(2)

values = (9.0, b'/', 3.0)

send_n_receive_values(server_address, values, packer, connection)

time.sleep(2)

values = (1.0, b'+', 3.0)

send_n_receive_values(server_address, values, packer, connection)

connection.close()