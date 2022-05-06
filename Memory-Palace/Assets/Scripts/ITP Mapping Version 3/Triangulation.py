from math import *
from graphics import *
from Corner import *
from Room import *
from BluetoothDevice import *
from User import *

def main():
    win = GraphWin("User's Home", 310, 210)
    user = User()

    #define distance between user and devices
    #OPTIONAL PRE-MADE MAP(S)
    blue1 = BluetoothDevice(math.sqrt(125 ** 2 + 25 ** 2)) #replace with commented code below when using RSSI
    blue2 = BluetoothDevice(math.sqrt(25 ** 2 + 25 ** 2))  #the arguments are the dist from user to each device, using Pythagorus
    blue3 = BluetoothDevice(math.sqrt(125 ** 2 + 25 ** 2)) #look at comments in BluetoothDevice Class when using RSSI

    #blue1 = getRSSI and convert to distance ; write in the code to allow the script to use RSSI
    #blue2 = getRSSI and convert to distance ; the code for the dist here and in BluetoothDevice Class
    #blue3 = getRSSI and convert to distance ; are a rough guide, currently works for pre-defined maps

    blue1.x = 25  #will be pre set when running setup script; remove when setup script is finished
    blue1.y = 25  #will be pre set when running setup script; remove when setup script is finished
    blue2.x = 175 #will be pre set when running setup script; remove when setup script is finished
    blue2.y = 25  #will be pre set when running setup script; remove when setup script is finished
    blue3.x = 25  #will be pre set when running setup script; remove when setup script is finished
    blue3.y = 75  #will be pre set when running setup script; remove when setup script is finished

    #SEE DOCUMENTATION FOR THE CODE BELOW AS IT SHOWS WHAT THE CODE REPRESENTS
    #define the x & y parts for the simultaneous equations
    x1 = (blue1.x * (-1)) * 2 #calulates the total Xs in the matrix multiplication. eg. the '-50x' in 'x^2 -50x + 625'
    y1 = (blue1.y * (-1)) * 2 #as above but with y
    ans1 = (blue1.dist ** 2) - ((blue1.x * (-1)) ** 2) - ((blue1.y * (-1)) ** 2)
    #takes constants from matrix multiplications and moves them to the ans

    x2 = (blue2.x * (-1)) * 2
    y2 = (blue2.y * (-1)) * 2
    ans2 = (blue2.dist ** 2) - ((blue2.x * (-1)) ** 2) - ((blue2.y * (-1)) ** 2) #5625 -30625 -625

    x3 = (blue3.x * (-1)) * 2
    y3 = (blue3.y * (-1)) * 2
    ans3 = (blue3.dist ** 2) - ((blue3.x * (-1)) ** 2) - ((blue3.y * (-1)) ** 2) #8125 -625 -5625

    #solving the simultaneous equations
    x2 = x1 - x2
    y2 = y1 - y2
    ans2 = ans1 - ans2

    x3 = x1 - x3
    y3 = y1 - y3
    ans3 = ans1 - ans3

    #create part of the inverse of the matrix
    deter = (x2 * y3) - (y2 * x3)
    invFrac = (1 / deter)

    #swap the diagonal (top left to bottom right) of the matrix
    temp = x2
    x2 = y3
    y3 = temp

    #change the signs of the other parts of the matrix
    x3 = x3 * (-1)
    y2 = y2 * (-1)

    #matrix multiplication
    x1 = (x2 * ans2) + (x3 * ans3)
    y1 = (y2 * ans2) + (y3 * ans3)
    x1 = x1 * invFrac
    y1 = y1 * invFrac

    #test that the values are correct
    print(x1)
    print(y1)

    #draws a line from the user to each bluetooth device to test location
    Point(x1,y1).draw(win)
    line1 = Line(Point(x1,y1),Point(blue1.x,blue1.y)).draw(win)
    line2 = Line(Point(x1,y1),Point(blue2.x,blue2.y)).draw(win)
    line3 = Line(Point(x1,y1),Point(blue3.x,blue3.y)).draw(win)
    line1.setOutline("green")
    line2.setOutline("green")
    line3.setOutline("green")

    #Test if devices are drawn in the right place
    point1 = Circle(Point(0,0),1).draw(win)
    point2 = Circle(Point(200,0),1).draw(win)
    point3 = Circle(Point(200,100),1).draw(win)
    point4 = Circle(Point(0,100),1).draw(win)
    point5 = Circle(Point(300,0),1).draw(win)
    point6 = Circle(Point(300,100),1).draw(win)
    point7 = Circle(Point(100,100),1).draw(win)
    point8 = Circle(Point(100,200),1).draw(win)
    point9 = Circle(Point(200,200),1).draw(win)
    point10 = Circle(Point(300,200),1).draw(win)

    blue1 = Circle(Point(25,25),1).draw(win)
    blue2 = Circle(Point(175,25),1).draw(win)
    blue3 = Circle(Point(25,75),1).draw(win)
    blueI = Circle(Point(225,25),1).draw(win)
    blueII = Circle(Point(275,25),1).draw(win)
    blueIII = Circle(Point(275,75),1).draw(win)
    blueA = Circle(Point(275,125),1).draw(win)
    blueB = Circle(Point(275,175),1).draw(win)
    blueC = Circle(Point(225,175),1).draw(win)
    blueX = Circle(Point(125,125),1).draw(win)
    blueY = Circle(Point(175,125),1).draw(win)
    blueZ = Circle(Point(125,175),1).draw(win)

    points = [point1,point2,point3,point4,point5,point6,point7,point8,point9,point10]
    blues = [blue1,blue2,blue3,blueI,blueII,blueIII,blueA,blueB,blueC,blueX,blueY,blueZ]

    for point in points:
        point.setOutline("red")

    for blue in blues:
        blue.setOutline("blue")