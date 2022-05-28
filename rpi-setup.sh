#!/bin/bash
set -e

if [[ $EUID -ne 0 ]]; then
    sudo "$0" "$@"
    exit $?
fi

read -p "What room will this device be in? " ROOM
read -n 3 -p "Unique identifier: " IDENTIFIER
echo ''

NEW_HOSTNAME="mem-pal-${ROOM}-${IDENTIFIER}"

if [[ $(cat /etc/hostname) !=  $NEW_HOSTNAME || $(cat /etc/hosts | grep 127.0.1.1 | cut -f 2) !=  $NEW_HOSTNAME ]]; then
    CONSENT=''
    until [[ $CONSENT == 'y' || $CONSENT == 'n' ]]
    do
        read -n 1 -p "This script will change the hostname of this device, is this ok? [y/n]: " CONSENT
        echo ''
        if [[ $CONSENT == 'y' ]]; then
            echo -e "$NEW_HOSTNAME" > /etc/hostname
            sed -i.bak -e "/127.0.1.1/c\127.0.1.1\t$NEW_HOSTNAME" /etc/hosts
        elif [[ $CONSENT == 'n' ]]; then
            echo "Hostname must be changed for server to work correctly, aborting" 1>&2 && exit 1
        else
            echo "Please enter y or n"
        fi
    done
fi

echo -e "Device is in room $ROOM with the identifier $IDENTIFIER"
echo -e "The device will be renamed to $NEW_HOSTNAME"

sed -i.bak -e "s/ROOM_HERE/$ROOM/" -e "s/IDENTIFIER_HERE/$IDENTIFIER/" 'raspberry-pi/server/app.py'

echo -ne 'Installing pip bluetooth libbluetooth-dev python-dev... '
apt-get install -y pip bluetooth libbluetooth-dev python-dev > /dev/null && echo 'done'

cd 'raspberry-pi'
echo -ne 'Installing PyBluez Flask... '
pip3 install PyBluez flask > /dev/null && echo 'done'
echo -ne 'Running setup.py install... '
python3 setup.py install > /dev/null && echo 'done'
cd ..

sed -i.bak -e "s|PATH_TO_SERVER_FILE|$(pwd)/raspberry-pi/server/app.py|" 'raspberry-pi/systemctl/rpi-bluetooth-server.service'

cp 'raspberry-pi/systemctl/rpi-bluetooth-server.service' '/etc/systemd/system'

systemctl enable rpi-bluetooth-server.service

read -n 1 -p 'Reboot required. Reboot now? [y/n]: ' REBOOT
echo ''

if [[ $REBOOT == 'y' ]]; then
    echo 'Device will now reboot. Server will be started when reboot is complete.'
    sleep 5
    reboot
else
    echo "Bluetooth server will not work as expected until reboot. Use 'sudo reboot' to reboot the device."
fi
