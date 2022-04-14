import sys
import time
from bt_proximity import BluetoothRSSI
from flask import Flask, jsonify, request

app = Flask(__name__)

ROOM: str = "ROOM_HERE"  # Put room name here

# Give the pi a unique identifier
UNIQUE_IDENTIFIER: int = "IDENTIFIER_HERE"


def average(lst: list) -> float:
    return sum(lst) / len(lst)


@app.route('/bluetooth/rssi', methods=['GET'])
def get_bluetooth_rssi() -> object:
    """Receive a HTTP get request, find the rssi for the mac address provided, and return the rssi value

    Returns:
        object: A JSON object containing the name of the pi, and the rssi value
    """
    rssi_vals = []
    btrssi = BluetoothRSSI(addr=request.json['addr'])
    if btrssi is not None:
        for _ in range(5):
            rssi_vals.append(btrssi.request_rssi()[0])
            time.sleep(0.2)
        return jsonify(name=f"mem_pal_{ROOM}_{UNIQUE_IDENTIFIER}", rssi=average(rssi_vals))
    else:
        return jsonify(name=f"mem_pal_{ROOM}_{UNIQUE_IDENTIFIER}", rssi="out of range")


if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')
