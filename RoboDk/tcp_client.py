import socket
import struct
import time

def tcp_client():
    host = '127.0.0.1'  # Server IP or hostname
    port = 49151        # Server port

    while True:
        # Create a TCP socket
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            try:
                s.connect((host, port))
                print(f"Connected to {host}:{port}")
                while True:
                    
                    # Send data
                    # Integer to send
                    number = 42
                    # Pack the integer as 4-byte little-endian
                    data_send = struct.pack('<i', number) # < = little-endian, i = 4-byte signed int
                    s.sendall(data_send)
                    print(f"Sent integer: {number}")

                    # Receive response
                    buf = 1 * 4
                    data_raw = s.recv(buf)
                    qty_str = str(1) + 'i'
                    data_received = struct.unpack(qty_str, data_raw)
                    print(f"Received from server: {data_received[0]}")


            except ConnectionRefusedError:
                print("Could not connect to the server.")
                time.sleep(3)
            except Exception as e:
                print(f"An error occurred: {e}")

if __name__ == "__main__":
    tcp_client()
