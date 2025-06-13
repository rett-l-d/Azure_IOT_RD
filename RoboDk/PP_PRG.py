import socket
import struct
import time

from robolink import *    # RoboDK API
from robodk import *      # Robot toolbox
RDK = Robolink()

# Any interaction with RoboDK must be done through
# Robolink()
RL = Robolink()

 
# get the robot item:
robot = RL.Item('ABB IRB 4600-40/2.55')
robottool = RL.Item('Tool')


# get frames
F_top_pick_table = RL.Item('Table_pick_top')
F_top_drop_table = RL.Item('Table_drop_top')

# get the targets:
P_home = RL.Item('Home')
P_pso = RL.Item('Pick_Stand_off')
P_pso_rotated = RL.Item('Pick_Stand_off_rotated')
P_pb1 = RL.Item('Pick_box1')
P_db1 = RL.Item('Drop_box1')





# move the robot to home, then to Stand off Position:
robot.MoveJ(P_home)
robot.MoveJ(P_pso)


poseref_pick = P_pb1.Pose()

poseref_drop = P_db1.Pose()

####Data to PLC
#[0] - In Cycle
#[1] - Completed Cycles count
#[2] - Cycle Complete
data_send = [0]*4



def robot_cycle(s: socket, counter: int):

    #while True:

    
    data_send[0] = 1
    data_send_raw =  struct.pack(str(len(data_send))+'i', *data_send) # '<' = little-endian, '4i' = four 32-bit signed integers
    s.sendall(data_send_raw)

    for i in range(0,365,105):

        robot.setSpeed(100,100)
        robot.setPoseFrame(F_top_pick_table)
        
        posei = poseref_pick.Offset(i,0,200)
        robot.MoveJ(posei)
        posei = poseref_pick.Offset(i,0,0)
        robot.MoveL(posei)
        robottool.AttachClosest()
        posei = poseref_pick.Offset(i,0,200)
        robot.MoveL(posei)

        #move the robot to stand off position
        #robot.MoveJ(P_pso)
        robot.MoveJ(P_pso_rotated)

        #move the robot to drop off position box 1
        robot.setPoseFrame(F_top_drop_table)
        posei = poseref_drop.Offset(i,0,200)
        robot.MoveJ(posei)
        posei = poseref_drop.Offset(i,0,0)
        robot.MoveL(posei)
        robottool.DetachAll(F_top_drop_table)
        posei = poseref_drop.Offset(i,0,200)
        robot.MoveL(posei)

        #move the robot to stand off position
        robot.MoveJ(P_pso)
        



    for i in range(315,-105,-105):

        #move the robot to drop off position box 1
        robot.setPoseFrame(F_top_drop_table)
        posei = poseref_drop.Offset(i,0,200)
        robot.MoveJ(posei)
        posei = poseref_drop.Offset(i,0,0)
        robot.MoveL(posei)
        robottool.AttachClosest()
        posei = poseref_drop.Offset(i,0,200)
        robot.MoveL(posei)

        #move the robot to stand off position
        robot.MoveJ(P_pso)


        robot.setPoseFrame(F_top_pick_table)
        posei = poseref_pick.Offset(i,0,200)
        robot.MoveJ(posei)
        posei = poseref_pick.Offset(i,0,0)
        robot.MoveL(posei)
        robottool.DetachAll(F_top_drop_table)
        posei = poseref_pick.Offset(i,0,200)
        robot.MoveL(posei)

        #move the robot to stand off position
        robot.MoveJ(P_pso)
        
    
    # # move back to the center, then home:

    robot.MoveJ(P_home)

    counter += 1
    data_send[1] = counter
    data_send[2] = 1 #Cycle completed
    # Send data
    # Pack the integer as 4-byte little-endian
    data_send_raw =  struct.pack(str(len(data_send))+'i', *data_send) # '<' = little-endian, '4i' = four 32-bit signed integers
    s.sendall(data_send_raw)
    print('Cycles Completed')
    return counter



def tcp_client():
    host = '25.38.171.74'  # Server IP or hostname
    port = 49151       # Server port

    #cycle count
    gCounter = 0

    while True:
        # Create a TCP socket
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            try:
                s.connect((host, port))
                print(f"Connected to {host}:{port}")
                print(f"Waiting Start")
                while True:
                    

                    # Receive response
                    buf = 4 * 4 # 4x DINTs
                    qty_str = str(4) + 'i'
                    data_rec_raw = s.recv(buf)
                    data_received = struct.unpack(qty_str, data_rec_raw)
                    #print(f"Received from server: {data_received}")
                    RDK.Render(False)
                    RDK.Update()
                   

                    #Run robot cycle if Start from PLC
                    if data_received[0] == 1:
                        print(f"Cycle Started")
                        
                        gCounter = robot_cycle(s, gCounter)

                        if data_send[2] == 1:
                            while data_received[1] != 1:
                                data_rec_raw = s.recv(buf)
                                data_received = struct.unpack(qty_str, data_rec_raw)
                                data_send[2] = 1
   
                        data_send[2] = 0 # Reset Cycle Complete
                        data_send_raw =  struct.pack(str(len(data_send))+'i', *data_send) # '<' = little-endian, '4i' = four 32-bit signed integers
                        s.sendall(data_send_raw)
                    else:
                        data_send[0] = 0 # Reset In Cycle
                        data_send_raw =  struct.pack(str(len(data_send))+'i', *data_send) # '<' = little-endian, '4i' = four 32-bit signed integers
                        s.sendall(data_send_raw)




            except ConnectionRefusedError:
                print("Could not connect to the server.")
                time.sleep(3)
            except Exception as e:
                print(f"An error occurred: {e}")

if __name__ == "__main__":
    #Move Robot Home
    robot.MoveJ(P_home)

    #Connect to PLC Server
    tcp_client()
