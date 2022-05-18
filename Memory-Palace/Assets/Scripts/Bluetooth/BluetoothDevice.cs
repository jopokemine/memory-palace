using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace.Bluetooth
{
    public class BluetoothDevice : MonoBehaviour
    {
        string room;
        float x, y, rssi;
        public BluetoothDevice(string room, float x, float y, float rssi)
        {
            this.room = room;
            this.x = x;
            this.y = y;
            this.rssi = rssi;
        }

        public static string GetRoom()
        {
            return this.room;
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

        public static float GetDist()
        {
            // TODO: Figure out how to turn rssi val to distance
            return this.rssi;
        }
    }
}
