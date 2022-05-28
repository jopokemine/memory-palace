using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace.Bluetooth
{
    public class BluetoothDevice : MonoBehaviour
    {
        string room;
        float x, y, rssi;

        public BluetoothDevice(string name, float x, float y, float rssi)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.rssi = rssi;
        }

        public BluetoothDevice(string name, float x, float y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.rssi = float.NaN;
        }

        public static string GetName()
        {
            return this.name;
        }

        public static string GetRoom()
        {
            return this.name.Split('-')[2];  // Based on name being 'mem-pal-{ROOM}-{IDENTIFIER}'
        }

        public static float[] GetCoords()
        {
            return new float[this.x, this.y];
        }

        public static float GetX()
        {
            return this.x;
        }

        public static float GetY()
        {
            return this.y;
        }

        public static float GetRssi()
        {
            return this.rssi;
        }

        public static void SetRssi(float rssi)
        {
            this.rssi = rssi;
        }

        public static float GetDist()
        {
            // TODO: Figure out how to turn rssi val to distance
            float measuredPower = -69; // TODO: Find out actual power
            int N = 2; // TODO: Find out actual value (environmental factor)
            return Math.Pow(10, (measuredPower - this.rssi) / (10 * N));
        }
    }
}
