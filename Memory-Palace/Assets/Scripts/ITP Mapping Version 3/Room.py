import math

class Room:
    def __init__(self, name, corners):
        #Should be given option to fill in if two corners intersect
        self.name = name
        self.corners = corners