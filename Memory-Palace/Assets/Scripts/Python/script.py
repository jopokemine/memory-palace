import random
import sys

class Greeter:
    def __init__(self, _name):
        self.name = _name
    
    def greet(self):
        return self.name
    
    def RandomNumber(self, min, max):
        return random.randint(min, max) + min
    
    def PythonVersion(self):
        return sys.version_info