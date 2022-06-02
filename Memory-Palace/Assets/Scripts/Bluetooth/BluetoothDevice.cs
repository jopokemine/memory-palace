using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace.BluetoothFunctions
{
    public class BluetoothDevice
    {
        private string deviceName, room;
        private float x, y, rssi;

        public BluetoothDevice(string deviceName, float x, float y, float rssi)
        {
            this.deviceName = deviceName;
            this.room = this.deviceName.Split('-')[2];
            this.x = x;
            this.y = y;
            this.rssi = rssi;
        }

        public BluetoothDevice(string deviceName, string roomName, float x, float y, float rssi)
        {
            this.deviceName = deviceName;
            this.room = roomName;
            this.x = x;
            this.y = y;
            this.rssi = rssi;
        }

        public BluetoothDevice(string deviceName, string roomName, float x, float y)
        {
            this.deviceName = deviceName;
            this.room = roomName;
            this.x = x;
            this.y = y;
        }

        public BluetoothDevice(string deviceName, float x, float y)
        {
            this.deviceName = deviceName;
            this.room = this.deviceName.Split('-')[2];
            this.x = x;
            this.y = y;
            this.rssi = float.NaN;
        }

        public string Name
        {
            get
            {
                return deviceName;
            }
        }

        public string Room
        {
            get
            {
                return room;
            }
            set
            {
                room = value;
            }
        }

        public Vector2 Coords
        {
            get
            {
                return new Vector2(x, y);
            }
        }

        public float X
        {
            get
            {
                return x;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
        }

        public float Rssi
        {
            get
            {
                return rssi;
            }
            set
            {
                rssi = value;
            }
        }

        public float Distance
        {
            get
            {
                float measuredPower = -1; // TODO: Find out actual power
                int N = 2;
                Debug.Log($"Distance for {deviceName} with rssi = {rssi}: {Mathf.Pow(10, (measuredPower - rssi) / (10 * N))}");
                return Mathf.Pow(10, (measuredPower - rssi) / (10 * N));
            }
        }
    }
}
