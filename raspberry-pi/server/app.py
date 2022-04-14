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
    for _ in range(5):
        rssi_tuple = btrssi.request_rssi()
        rssi_vals.append(
            rssi_tuple[0] if rssi_tuple is not None else "not in range")
        time.sleep(0.2)

    rssi = "not in range" if rssi_vals.count(
        "not in range") >= 3 else average(rssi_vals)

    return jsonify(name=f"mem_pal_{ROOM}_{UNIQUE_IDENTIFIER}", rssi=rssi)


if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')
