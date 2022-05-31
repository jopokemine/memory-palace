using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace.Bluetooth
{
    public class BluetoothDevice : MonoBehaviour
    {
        private string deviceName, room;
        private float x, y, rssi;

        public BluetoothDevice(string deviceName, float x, float y, float rssi)
        {
            this.deviceName = deviceName;
            this.x = x;
            this.y = y;
            this.rssi = rssi;
        }

        public BluetoothDevice(string deviceName, float x, float y)
        {
            this.deviceName = deviceName;
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
                return deviceName.Split('-')[2];  // Based on deviceName being 'mem-pal-{ROOM}-{IDENTIFIER}'
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
                // TODO: Figure out how to turn rssi val to distance
                float measuredPower = -69; // TODO: Find out actual power
                int N = 2; // TODO: Find out actual value (environmental factor)
                return Mathf.Pow(10, (measuredPower - rssi) / (10 * N));
            }
        }
    }
}
