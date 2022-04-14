from setuptools import setup, find_packages

setup(name='bt_proximity',
      version='0.2.1',
      description="Allows for querying of the RSSI values for nearby devices using pybluez. Ported to Python 3, based on https://github.com/ewenchou/bluetooth-proximity",
      url='https://github.com/FrederikBolding/bluetooth-proximity',
      author='Frederik Bolding',
      author_email='frederik.bolding@gmail.com',
      license='Apache 2.0',
      packages=find_packages(),
      zip_safe=False)
