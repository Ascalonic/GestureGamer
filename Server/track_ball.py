from collections import deque
import numpy as np
import argparse
import imutils
import cv2

import socket
import sys
from _thread import *

####################################################

def object_track(conn):
    ap = argparse.ArgumentParser()
    ap.add_argument("-v", "--video",
        	help="path to the (optional) video file")
    ap.add_argument("-b", "--buffer", type=int, default=32,
            help="max buffer size")
    args = vars(ap.parse_args())

    # we are going to track a yellow ball
    yellowLower = (0, 86, 116)
    yellowUpper = (32, 205, 255)

    pts = deque(maxlen=args["buffer"])
    counter = 0
    (dX, dY) = (0, 0)
    counter = 0
    (dX, dY) = (0, 0)
    direction = ""

    numpts = 10

    # use webcam input
    camera = cv2.VideoCapture(0)

    while True:
            # grab the current frame
            (grabbed, frame) = camera.read()

            # resize the frame, blur it, and convert it to the HSV
            frame = imutils.resize(frame, width=600)
            blurred = cv2.GaussianBlur(frame, (11, 11), 0)
            hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)

            # construct a mask for the color "green", then perform
            # a series of dilations and erosions to remove any small
            # blobs left in the mask
            mask = cv2.inRange(hsv, greenLower, greenUpper)
            mask = cv2.erode(mask, None, iterations=2)
            mask = cv2.dilate(mask, None, iterations=2)

            # find contours in the mask and initialize the current
            # (x, y) center of the ball
            cnts = cv2.findContours(mask.copy(), cv2.RETR_EXTERNAL,
                    cv2.CHAIN_APPROX_SIMPLE)[-2]
            center = None

            # only proceed if at least one contour was found
            if len(cnts) > 0:
                    # find the largest contour in the mask, then use
                    # it to compute the minimum enclosing circle and
                    # centroid
                    c = max(cnts, key=cv2.contourArea)
                    ((x, y), radius) = cv2.minEnclosingCircle(c)
                    M = cv2.moments(c)
                    center = (int(M["m10"] / M["m00"]), int(M["m01"] / M["m00"]))

                    # only proceed if the radius meets a minimum size
                    if radius > 10:
                            # draw the circle and centroid on the frame,
                            # then update the list of tracked points
                            cv2.circle(frame, (int(x), int(y)), int(radius),
                                    (0, 255, 255), 2)
                            cv2.circle(frame, center, 5, (0, 0, 255), -1)
                            pts.appendleft(center)

            # loop over the set of tracked points
            for i in np.arange(1, len(pts)):
                    # if either of the tracked points are None, ignore
                    # them
                    if pts[i - 1] is None or pts[i] is None:
                            continue

                    # check to see if enough points have been accumulated in
                    # the buffer
                    if counter >= numpts and i == numpts and pts[i-numpts] is not None:
                            # compute the difference between the x and y
                            # coordinates and re-initialize the direction
                            # text variables
                            dX = pts[-numpts][0] - pts[i][0]
                            dY = pts[-numpts][1] - pts[i][1]
                            (dirX, dirY) = ("", "")

                            # ensure there is significant movement in the
                            # x-direction
                            if np.abs(dX) > 20:
                                    dirX = "East" if np.sign(dX) == 1 else "West"

                            # ensure there is significant movement in the
                            # y-direction
                            if np.abs(dY) > 20:
                                    dirY = "North" if np.sign(dY) == 1 else "South"

                            ########################

                            if dirY=="North":   
                                conn.sendall(b'North')
                            elif dirY=="South":
                                conn.sendall(b'South')
                            elif dirX=="East":
                                conn.sendall(b'East')
                            elif dirX=="West":
                                conn.sendall(b'West')

                            # handle when both directions are non-empty
                            if dirX != "" and dirY != "":
                                    direction = "{}-{}".format(dirY, dirX)

                            # otherwise, only one direction is non-empty
                            else:
                                    direction = dirX if dirX != "" else dirY

                    # otherwise, compute the thickness of the line and
                    # draw the connecting lines
                    thickness = int(np.sqrt(args["buffer"] / float(i + 1)) * 2.5)
                    cv2.line(frame, pts[i - 1], pts[i], (0, 0, 255), thickness)

            # show the movement deltas and the direction of movement on
            # the frame
            cv2.putText(frame, direction, (10, 30), cv2.FONT_HERSHEY_SIMPLEX,
                    0.65, (0, 0, 255), 3)
            cv2.putText(frame, "dx: {}, dy: {}".format(dX, dY),
                    (10, frame.shape[0] - 10), cv2.FONT_HERSHEY_SIMPLEX,
                    0.35, (0, 0, 255), 1)

            # show the frame to our screen and increment the frame counter
            cv2.imshow("Frame", frame)
            key = cv2.waitKey(1) & 0xFF
            counter += 1

            # if the 'q' key is pressed, stop the loop
            if key == ord("q"):
                    break

    # cleanup the camera and close any open windows
    camera.release()
    cv2.destroyAllWindows()

    return

##################################################

HOST = ''   
PORT = 8888 

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
print('Socket created')
 
try:
    s.bind((HOST, PORT))
except socket.error as msg:
    print('Bind failed. Error Code : ' + str(msg[0]) + ' Message ' + msg[1])
    sys.exit()
     
print ('Socket bind complete')
 
#Start listening on socket
s.listen(10)
print ('Socket now listening')
 
#Function for handling connections. This will be used to create threads
def clientthread(conn):
    #Sending message to connected client
    conn.send(b'Welcome to the server. Type something and hit enter\n') #send only takes string
     
    #infinite loop so that function do not terminate and thread do not end.
    while True:

        object_track(conn)
         
        #Receiving from client
        #data = conn.recv(1024)
        #reply = b'OK...' + data
        #if not data: 
            #break
     
        #conn.sendall(reply)
     
    #came out of loop
    conn.close()
 
#now keep talking with the client
while 1:
    #wait to accept a connection - blocking call
    conn, addr = s.accept()
    print ('Connected with ' + addr[0] + ':' + str(addr[1]))
     
    #start new thread takes 1st argument as a function name to be run, second is the tuple of arguments to the function.
    start_new_thread(clientthread ,(conn,))
 
s.close()


