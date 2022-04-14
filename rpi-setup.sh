#!/bin/bash

if [[ $EUID -ne 0 ]]; then
    sudo "$0" "$@"
    exit $?
fi

read -p "What room will this device be in? " ROOM
read -n 3 -p "Give this device a unique identifier (max. 3 characters): " IDENTIFIER

echo -e "\nDevice is in room $ROOM with the identifier $IDENTIFIER"

sed -i.bak -e "s/ROOM_HERE/$ROOM/" -e "s/IDENTIFIER_HERE/$IDENTIFIER/" 'raspberry-pi/server/app.py'

apt-get install -y bluetooth libbluetooth-dev python-dev
cd 'raspberry-pi'
pip install PyBluez
python3 setup.py install
cd ..

sed -i.bak -e "s|PATH_TO_SERVER_FILE|$(pwd)/raspberry-pi/server/app.py|" 'raspberry-pi/systemctl/rpi-bluetooth-server.service'
cp 'raspberry-pi/systemctl/rpi-bluetooth-server.service' '/etc/systemd/service'

systemctl enable rpi-bluetooth-server.service
systemctl start rpi-bluetooth-server.service
