using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemoryPalace.BluetoothFunctions;
using DataBank;
using CG = MemoryPalace.Util.CoordinateGeometry;

namespace MemoryPalace.Tracking
{
    public class Tracking : MonoBehaviour
    {

        public Bluetooth BT;
        GameObject cursor;
        Items itemDb;
        // Start is called before the first frame update
        void Start() {
            itemDb = new Items();
            BT = GameObject.Find("Bluetooth").GetComponent<Bluetooth>();
            cursor = GameObject.Find("cursor");
            cursor.GetComponent<Renderer>().enabled = false;
            // float angle = DirectUserToItem("item");
            // transform.Rotate(angle, 0f, 0f, Space.Self);
        }

        public string GetItemRoom(string itemName)
        {
            // return itemDb.getItemRoom(itemName);
            return "kitchen";
        }

        // public bool ItemUserInSameRoom(string itemName)
        // {
        //     return GetItemRoom(itemName) == BT.GetUserPos()[0];
        // }

        public Vector2 GetItemLocation(string itemName)
        {
            // return itemDb.getItemPos(itemName);
            return new Vector2(5, 5);
        }

        public float GetAngleDegsToItem(string itemName, Vector2 userPos)
        {
            Vector2 itemPos = GetItemLocation(itemName);
            Vector2 diff = CG.DistanceVector2(itemPos, userPos);
            float adj = diff.x;
            float hyp = Mathf.Sqrt(Mathf.Pow(diff.x, 2) + Mathf.Pow(diff.y, 2));
            float angleInRads = Mathf.Asin(adj / hyp);
            return angleInRads * (180 / Mathf.PI);
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
            if (userInfo.Key != itemRoom) {
                // User in the wrong room, tell them to go to correct one!
                Debug.Log("Wrong room!");
            }
            // Assumes the user is in the correct room!
            Vector2 userPos = userInfo.Value;
            Vector2 itemPos = GetItemLocation(itemName);
            Debug.Log($"Item location, x: {itemPos.x}, y: {itemPos.y}");
            float angleToItem = GetAngleDegsToItem(itemName, userPos);
            Debug.Log($"angleToItem: {angleToItem}");
            cursor.GetComponent<Renderer>().enabled = true;
            cursor.transform.Rotate(angleToItem, 0f, 0f, Space.Self);
            // return angleToItem;

            // TODO: Adjust angle of arrow here

            // TODO: Implement way to find users orientation against magnetic north
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
            // float rotationTime = 180f;
		    // transform.Rotate(Vector3.right * (rotationTime * Time.deltaTime));
        }
    }
}
