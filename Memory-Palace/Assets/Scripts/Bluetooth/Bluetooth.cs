using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MemoryPalace.Util;
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
            BluetoothDevice[] devices = GetBluetoothDevices();
            GetRssiValues(ref devices);
            string room = GetCurrentRoomByBTStrength(devices);
            float[] userCoords = TriangulateUserPos(GetBluetoothDevicesFromRoom(room));
            Debug.Log($"room: {room}, x: {userCoords[0]}, y: {userCoords[1]}");
            return (room, userCoords[0], userCoords[1]);
        }

        BluetoothDevice[] GetBluetoothDevices()
        {
            // TODO Implement this once data storage is completed
            return new BluetoothDevice[] {
                new BluetoothDevice("mem-pal-kitchen-cok", 2.0f, 5.0f, 5.0f),
                new BluetoothDevice("mem-pal-kitchen-cok", 5.0f, 5.0f, 2.0f),
                new BluetoothDevice("mem-pal-kitchen-cok", 1.0f, 7.0f, 4.0f)
            };
        }

        BluetoothDevice[] GetBluetoothDevicesFromRoom(string room)
        {
            // TODO: Implement this once data storage is completed
            return new BluetoothDevice[] {
                new BluetoothDevice("mem-pal-kitchen-cok", 2.0f, 5.0f, 5.0f),
                new BluetoothDevice("mem-pal-kitchen-cok", 5.0f, 5.0f, 2.0f),
                new BluetoothDevice("mem-pal-kitchen-cok", 1.0f, 7.0f, 4.0f)
            };
        }

        void GetRssiValues(ref BluetoothDevice[] bluetoothDevices)
        {
            foreach (BluetoothDevice device in bluetoothDevices)
            {
                req.GetRequest($"http://{device.Name}.local:5000/api/v1/bluetooth/rssi?addr={GetBluetoothMACAddress()}", ResCallback);
                void ResCallback(string data)
                {
                    Debug.Log($"data: {data}");
                    // TODO get device and rssi from data
                    device.Rssi = 5.0f;
                }
            }
        }

        string GetCurrentRoomByBTStrength(BluetoothDevice[] bluetoothDevices)
        {
            Dictionary<string, List<float>> roomStrengths = new Dictionary<string, List<float>>();
            foreach (BluetoothDevice device in bluetoothDevices)
            {
                string room = device.Room;
                if (roomStrengths.ContainsKey(room))
                {
                    roomStrengths[room].Add(device.Rssi);
                }
                else
                {
                    roomStrengths.Add(room, new List<float>{device.Rssi});
                }
            }

            string strongestRoom = "null";
            float avg = float.MaxValue;
            foreach (var room in roomStrengths)
            {
                float avgVal = room.Value.Average();
                if (avgVal < avg)
                {
                    strongestRoom = room.Key;
                    avg = avgVal;
                }
            }

            return strongestRoom;
        }

        string GetBluetoothMACAddress()
        {
            // TODO Imlpement function to get BT MAC address for device
            return "74:65:0C:DC:9F:94";
        }

        BluetoothDevice[] GetStrongestThreeDevices(BluetoothDevice[] bluetoothDevices)
        {
            return (BluetoothDevice[])(from i in bluetoothDevices
                    orderby i.Rssi descending
                    select i).Take(3);
        }

        float[] TriangulateUserPos(BluetoothDevice[] bluetoothDevices)
        {
            if (bluetoothDevices.Length == 1)
            {
                return new float[2]{0f, 0f};  // TODO: Figure out what this should return if only one BT device
            }
            else if (bluetoothDevices.Length == 2)
            {
                return new float[2]{0f, 0f};  // TODO: Figure out how to find the two points the user could be in, and return
                // Or just return same as above
            }
            else
            {
                BluetoothDevice[] devices = GetStrongestThreeDevices(bluetoothDevices);
                BluetoothDevice blue1 = devices[0];
                BluetoothDevice blue2 = devices[1];
                BluetoothDevice blue3 = devices[2];

                float x1 = -2 * blue1.X;
                float y1 = -2 * blue1.Y;
                float ans1 = (Mathf.Pow(blue1.Distance, 2)) - (Mathf.Pow(blue1.X * -1, 2)) - (Mathf.Pow(blue1.Y * -1, 2));

                float x2 = -2 * blue2.X;
                float y2 = -2 * blue2.Y;
                float ans2 = (Mathf.Pow(blue2.Distance, 2)) - (Mathf.Pow(blue2.X * -1, 2)) - (Mathf.Pow(blue2.Y * -1, 2));

                float x3 = -2 * blue3.X;
                float y3 = -2 * blue3.Y;
                float ans3 = (Mathf.Pow(blue3.Distance, 2)) - (Mathf.Pow(blue3.X * -1, 2)) - (Mathf.Pow(blue3.Y * -1, 2));

                x2 = x1 - x2;
                y2 = y1 - y2;
                ans2 = ans1 - ans2;

                x3 = x1 - x3;
                y3 = y1 - y3;
                ans3 = ans1 - ans3;

                float deter = (x2 * y3) - (y2 * x3);
                float invFrac = 1 / deter;

                DM.Swap<float>(ref x2, ref y3);

                x3 *= -1;
                y2 *= -1;

                float userX = (x2 * ans2) + (x3 * ans3);
                float userY = (y2 * ans2) + (y3 * ans3);
                userX *= invFrac;
                userY *= invFrac;

                return new float[2]{userX, userY};

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
