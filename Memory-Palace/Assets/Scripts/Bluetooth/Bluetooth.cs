using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemoryPalace.Util;

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

        string[] GetBluetoothDevices()
        {
            // TODO Implement this once data storage is completed
            return new string[] { "mem_pal_kitchen_cok", "mem_pal_kitchen_sok", "mem_pal_bedroom_nob", "mem_pal_bedroom_sob" };
        }

        public Dictionary<string, float> GetBluetoothStrengths()
        {
            string[] bluetoothDevices = GetBluetoothDevices();
            Dictionary<string, float> bluetoothStrengths = new Dictionary<string, float>();
            foreach (var device in bluetoothDevices)
            {
                req.GetRequest($"http://{device}.local:5000/api/v1/bluetooth/rssi?addr={GetBluetoothMACAddress()}", ResCallback);
            }

            void ResCallback(string data)
            {
                Debug.Log($"data: {data}");
                // TODO get device and rssi from data
                // bluetoothStrengths.Add(device, rssi);
                // make sure all rssi vals are absolute values here! i.e. >= 0
            }

            return bluetoothStrengths;
        }

        public string GetCurrentRoomByBTStrength(Dictionary<string, float> bluetoothStrengths)
        {
            Dictionary<string, List<int32>> roomStrengths = new Dictionary<string, List<int32>>();
            foreach (var strength in bluetoothStrengths)
            {
                string room = strength.Key.Split('_')[2];  // From mem_pal_{room}_{identifier}
                if (roomStrengths.ContainsKey(room))
                {
                    roomStrengths[room].Add(strength.Value);
                }
                else
                {
                    roomStrengths.Add(room, new List<int32>(strength.Value));
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

        // Update is called once per frame
        void Update()
        {

        }
    }
}
