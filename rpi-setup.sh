#!/bin/bash

read -p "What room will this device be in? " ROOM
read -n 3 -p "Give this device a unique identifier (max. 3 characters): " IDENTIFIER

echo -e "\nDevice is in room $ROOM with the identifier $IDENTIFIER"

sed -i '' -e "s/ROOM_HERE/$ROOM/" -e "s/IDENTIFIER_HERE/$IDENTIFIER/" 'raspberry-pi/examples/app.py'

apt-get install -y bluetooth python-bluez

python setup.py install
