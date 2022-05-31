using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CG = MemoryPalace.Util.CoordinateGeometry;
using BT = MemoryPalace.Bluetooth.Bluetooth;

namespace MemoryPalace.Tracking
{
    public class Tracking : MonoBehaviour {
        // Start is called before the first frame update
        void Start() {
        
        }

        public string GetItemRoom(string itemName)
        {
            // TODO: Implement this when data storage is complete
            return "bedroom";
        }

        public bool ItemUserInSameRoom(string itemName)
        {
            return GetItemRoom(itenName) == BT.GetUserPos()[0];
        }

        

        public Vector2 GetItemLocation(string itemName)
        {
            // TODO: Implement this when data storage is complete
            return new Vector2(5, 5);
        }

        public float GetAngleDegsToItem(string itemName)
        {
            Vector2 itemPos = GetItemLocation(itemName);
            Vector2 userPos = new Vector2(2, 2);  // TODO: Replace this with function to get UserPos
            Vector2 diff = CG.DistanceVector2(itemPos, userPos);
            float adj = diff.x;
            float hyp = Mathf.Sqrt(Mathf.Pow(diff.x, 2) + Mathf.Pow(diff.y, 2));
            float angleInRads = Mathf.Acos(adj / hyp);
            return 90 - angleInRads * (180 / Mathf.PI);
            // TODO: Think about user angle
        }

        public void DirectUserToItem(string itenName)
        {
            var userInfo = BT.GetUserPos();
            string itemRoom = GetItemRoom();
            if (userInfo[0] != itemRoom) {
                // User in the wrong roon, tell them to go to correct one!
                return itemRoom; // TODO: What should this return?
            }
            // Assumes the user is in the correct room!
            Vector2 userPos = new Vector2(userInfo[1], userInfo[2]);
            Vector2 itemPos = GetItemLocation(itemName);
            

            // if yes, then get user location
            // find orientation of user vs. due north
            // find angle of user loc to location from 
        }

        // Update is called once per frame
        void Update() {
        
        }
    }
}
