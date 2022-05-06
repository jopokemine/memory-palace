import math

class BluetoothDevice:
    def __init__(self, distance): #remove distance parameter when using RSSI
        self.name = None
        self.dist = distance #gathered from converting RSSI when integrated
        self.x = None
        self.y = None

