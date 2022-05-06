import math

class Corner:
    def __init__(self, distance, angle):
        #Determined from LiDAR? and Compass APIs
        self.distToUser = distance
        self.angleToUser = angle

        #Calculated using trigonometry with distToUser & angleToUser
        #self.x = (
        #            xMultiplier = 1
        #            yMultiplier = -1

        #            if angle > (360 - angle):
        #                angle = 360 - angle
        #                xMultiplier = -1

                    #if angle is obtuse, make it acute by using other angle
        #            if angle > 90:
        #                angle = 180 - angle
        #                yMultiplier = 1

        #            adjacent = dist * math.cos(angle)
        #            opposite = dist * math.sin(angle)
        #        )
        #self.y = (
        #            xMultiplier = 1
        #            yMultiplier = -1

        #            if angle > (360 - angle):
        #                angle = 360 - angle
        #                xMultiplier = -1

                    #if angle is obtuse, make it acute by using other angle
        #            if angle > 90:
        #                angle = 180 - angle
        #                yMultiplier = 1

        #            adjacent = dist * math.cos(angle)
        #            opposite = dist * math.sin(angle)
        #        )