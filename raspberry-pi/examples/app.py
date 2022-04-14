import sys
import time
from bluetooth import BluetoothRSSI
from flask import Flask, jsonify, request

app = Flask(__name__)

ROOM: str = "ROOM_HERE"  # Put room name here

# Give the pi a unique identifier
UNIQUE_IDENTIFIER: int = "IDENTIFIER_HERE"


def average(lst: list) -> float:
    """Find the average value of a list of numbers

    Args:
        lst (list): The list of values to average

    Returns:
        float: The average of each item in the list
    """
    return sum(lst) / len(lst)


@app.route('/bluetooth/rssi', methods=['GET'])
def get_bluetooth_rssi() -> object:
    """Receive a HTTP get request, find the rssi for the mac address provided, and return the rssi value

    Returns:
        object: A JSON object containing the name of the pi, and the rssi value
    """
    rssi_vals = []
    btrssi = BluetoothRSSI(addr=request.json['addr'])
    for _ in range(5):
        rssi_vals.append(btrssi.request_rssi()[0])
        time.sleep(0.2)
    return jsonify(name=f"mem_pal_{ROOM}_{UNIQUE_IDENTIFIER}", rssi=average(rssi_vals))


if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')
