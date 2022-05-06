from math import *
from graphics import *
from Corner import *
from Room import *
from BluetoothDevice import *
from User import *

def main():
    #define user's position as origin (middle of screen)
    win = GraphWin("User's Home", 310, 210)
    user = User()
    user.x = 250
    user.y = 250
    Point(user.x, user.y).draw(win)

    #define minXY & maxXY
    minX = user.x
    maxX = user.x
    minY = user.y
    maxY = user.y

    #Take in distance & angle from user to desired corner location, then figure out relative XY
    #setup = True
    #corners = []
    #index = 0
    #while setup:
        #if user decides to place corner
            #if user presses add button to place point
                #dist = getAPIdist
                #angle = getAPIangle #must be in degrees

                #corners[index] = Corner(dist, angle)
                #if corners[index].x < minX:
                #   minX = corners[index].x

                #if corners[index].x > maxX:
                #   maxX = corners[index].x

                #if corners[index].y < minY:
                #   minY = corners[index].y

                #if corners[index].y > maxY:
                #   maxY = corners[index].y

                #index = index + 1

            #if user presses finish button
                #Room(input("Name of room: "),corners) #use a polygon shape to connect the corners and allows boundary condition
                #setup = False

        #if user decides to place bluetooth
            #if user presses add button to place point
                #dist = getAPIdist
                #angle = getAPIangle #must be in degrees

                #blueDevice = BluetoothDevice(dist, angle)
                #if blueDevice.x < minX:
                #   minX = blueDevice.x

                #if blueDevice.x > maxX:
                #   maxX = blueDevice.x

                #if blueDevice.y < minY:
                #   minY = blueDevice.y

                #if blueDevice.y > maxY:
                #   maxY = blueDevice.y

            #if user presses finish button
                #setup = False



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