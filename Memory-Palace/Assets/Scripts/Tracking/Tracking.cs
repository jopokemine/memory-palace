using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemoryPalace.BluetoothFunctions;
using CG = MemoryPalace.Util.CoordinateGeometry;

namespace MemoryPalace.Tracking
{
    public class Tracking : MonoBehaviour {

        Bluetooth BT;
        // Start is called before the first frame update
        void Start() {
            BT = GameObject.Find("Bluetooth").GetComponent<Bluetooth>();
        }

        public string GetItemRoom(string itemName)
        {
            // TODO: Implement this when data storage is complete
            return "kitchen";
        }

        // public bool ItemUserInSameRoom(string itemName)
        // {
        //     return GetItemRoom(itemName) == BT.GetUserPos()[0];
        // }

        public Vector2 GetItemLocation(string itemName)
        {
            // TODO: Implement this when data storage is complete
            return new Vector2(5, 5);
        }

        public float GetAngleDegsToItem(string itemName)
        {
            Vector2 itemPos = GetItemLocation(itemName);
            Vector2 userPos = BT.GetUserPos().Value;
            Vector2 diff = CG.DistanceVector2(itemPos, userPos);
            float adj = diff.x;
            float hyp = Mathf.Sqrt(Mathf.Pow(diff.x, 2) + Mathf.Pow(diff.y, 2));
            float angleInRads = Mathf.Acos(adj / hyp);
            return 90 - angleInRads * (180 / Mathf.PI);
        }

        float GetRoomMagneticNorthAngle()
        {
            // TODO: Get this from the initial setup
            return 0f;
        }

        public void DirectUserToItem(string itemName)
        {
            KeyValuePair<string, Vector2> userInfo = BT.GetUserPos();
            string itemRoom = GetItemRoom(itemName);
            Debug.Log($"itemRoom: {itemRoom}");
            // if (userInfo.Key != itemRoom) {
            //     // User in the wrong roon, tell them to go to correct one!
            //     // return itemRoom; // TODO: What should this return?
            //     return 361;
            // }
            // Assumes the user is in the correct room!
            Vector2 userPos = userInfo.Value;
            Vector2 itemPos = GetItemLocation(itemName);
            Debug.Log($"Item location, x: {itemPos.x}, y: {itemPos.y}");
            float angleToItem = GetAngleDegsToItem(itemName);
            Debug.Log($"angleToItem: {angleToItem}");
            // float roomAngleFromMagneticNorth = GetRoomMagneticNorthAngle();
            // Debug.Log($"roomAngleFromMagneticNorth: {roomAngleFromMagneticNorth}");
            // float userAngleFromMagneticNorth = Quaternion.Euler(0, -Input.compass.magneticHeading, 0);
            // Debug.Log($"userAngleFromMagneticNorth: {userAngleFromMagneticNorth}");
            // Debug.Log($"final angle: {angleToItem - (userAngleFromMagneticNorth + roomAngleFromMagneticNorth)}");
            // Debug.Log($"final angle: {angleToItem - roomAngleFromMagneticNorth}");
            // return angleToItem - (userAngleFromMagneticNorth + roomAngleFromMagneticNorth);
            // return angleToItem - (userAngleFromMagneticNorth + roomAngleFromMagneticNorth);
            // return angleToItem;
        }

        // Update is called once per frame
        void Update() {
        
        }
    }
}
