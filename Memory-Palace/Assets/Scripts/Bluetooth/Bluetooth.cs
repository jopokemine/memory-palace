using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemoryPalace.Util;
using BluetoothDevice;
using DM = MemoryPalace.Util.DataManipulation;

namespace MemoryPalace.Bluetooth
{
    public class Bluetooth : MonoBehaviour
    {
        Request req;
        // Start is called before the first frame update
        void Start()
        {
            req = GameObject.Find("Utilities").GetComponent<Request>();
        }

        public (string, float, float) GetUserPos()
        {
            BluetoothDevice[] devices = GetRssiValues(GetBluetoothDevices());
            string room = GetCurrentRoomByBTStrength(devices);
            float[] userCoords = TriangulateUserPos(GetBluetoothDevicesFromRoom(room));
            Debug.Log($"room: {room}, x: {userCoords[0]}, y: {userCoords[1]}");
            return (room, userCoords[0], userCoords[1]);
        }

        BluetoothDevice[] GetBluetoothDevices()
        {
            // TODO Implement this once data storage is completed
            return new BluetoothDevice[] {
                new BluetoothDevice("mem-pal-kitchen-cok", 2.0, 5.0, 5.0),
                new BluetoothDevice("mem-pal-kitchen-cok", 5.0, 5.0, 2.0),
                new BluetoothDevice("mem-pal-kitchen-cok", 1.0, 7.0, 4.0)
            };
        }

        BluetoothDevice[] GetBluetoothDevicesFromRoom(string room)
        {
            // TODO: Implement this once data storage is completed
            return new BluetoothDevice[] {
                new BluetoothDevice("mem-pal-kitchen-cok", 2.0, 5.0, 5.0),
                new BluetoothDevice("mem-pal-kitchen-cok", 5.0, 5.0, 2.0),
                new BluetoothDevice("mem-pal-kitchen-cok", 1.0, 7.0, 4.0)
            };
        }

        void GetRssiValues(ref BluetoothDevice[] bluetoothDevices)
        {
            foreach (BluetoothDevice device in bluetoothDevices)
            {
                req.GetRequest($"http://{device.GetName()}.local:5000/api/v1/bluetooth/rssi?addr={GetBluetoothMACAddress()}", ResCallback);
                void ResCallback(string data)
                {
                    Debug.Log($"data: {data}");
                    // TODO get device and rssi from data
                    device.SetRssi(5.0);
                }
            }
        }

        string GetCurrentRoomByBTStrength(BluetoothDevice bluetoothDevices)
        {
            Dictionary<string, List<int32>> roomStrengths = new Dictionary<string, List<int32>>();
            foreach (BluetoothDevice device in bluetoothDevices)
            {
                string room = device.GetRoom();
                if (roomStrengths.ContainsKey(room))
                {
                    roomStrengths[room].Add(device.GetRssi());
                }
                else
                {
                    roomStrengths.Add(room, new List<int32>(device.GetRssi()));
                }
            }

            KeyValuePair<string, float> strongestRoom = new KeyValuePair<string, float>("null", float.MaxValue);
            foreach (var room in roomStrengths)
            {
                float avgVal = room.Value.Average();
                if (avgVal < strongestRoom.Value)
                {
                    strongestRoom = (room.Key, avgVal);
                }
            }

            return strongestRoom.Key;
        }

        string GetBluetoothMACAddress()
        {
            // TODO Imlpement function to get BT MAC address for device
            return "74:65:0C:DC:9F:94";
        }

        BluetoothDevice[] GetStrongestThreeDevices(BluetoothDevice[] bluetoothDevices)
        {
            return (from i in bluetoothDevices
                    orderby i.GetRssi() descending
                    select i).Take(3);
        }

        float[] TriangulateUserPos(BluetoothDevice[] bluetoothDevices)
        {
            if (bluetoothDevices.Length == 1)
            {
                return new float[0.0, 0.0];  // TODO: Figure out what this should return if only one BT device
            }
            else if (bluetoothDevices.Length == 2)
            {
                return new float[0, 0];  // TODO: Figure out how to find the two points the user could be in, and return
                // Or just return same as above
            }
            else
            {
                BluetoothDevice[] devices = GetStrongestThreeDevices(bluetoothDevices);
                BluetoothDevice blue1 = devices[0];
                BluetoothDevice blue2 = devices[1];
                BluetoothDevice blue3 = devices[2];

                float x1 = -2 * blue1.GetX();
                float y1 = -2 * blue1.GetY();
                float ans1 = (Math.Pow(blue1.GetDist(), 2)) - (Math.Pow(blue1.GetX() * -1, 2)) - (Math.Pow(blue1.GetY() * -1, 2));

                float x2 = -2 * blue2.GetX();
                float y2 = -2 * blue2.GetY();
                float ans2 = (Math.Pow(blue2.GetDist(), 2)) - (Math.Pow(blue2.GetX() * -1, 2)) - (Math.Pow(blue2.GetY() * -1, 2));

                float x3 = -2 * blue3.GetX();
                float y3 = -2 * blue3.GetY();
                float ans3 = (Math.Pow(blue3.GetDist(), 2)) - (Math.Pow(blue3.GetX() * -1, 2)) - (Math.Pow(blue3.GetY() * -1, 2));

                x2 = x1 - x2;
                y2 = y1 - y2;
                ans2 = ans1 - ans2;

                x3 = x1 - x3;
                y3 = y1 - y3;
                ans3 = ans1 - ans3;

                float deter = (x2 * y3) - (y2 * x3);
                float invFrac = 1 / deter;

                DM.Swap<float>(x2, y3);

                x3 *= -1;
                y2 *= -1;

                float userX = (x2 * ans2) + (x3 * ans3);
                float userY = (y2 * ans2) + (y3 * ans3);
                userX *= invFrac;
                userY *= invFrac;

                return new float[userX, userY];

                // I assume this maps to a grid where 0,1 is 1m away from 0,0
                // If this is the case, userX and userY need to be doubled to match the 0.5m scale
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
