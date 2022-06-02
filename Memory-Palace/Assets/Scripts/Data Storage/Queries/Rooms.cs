using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataBank
{
    public class Rooms : MonoBehaviour
    {
        public void addRoom (string width, string height, string x, string y, string room_name, string description)
        {
            tbl_Room room = new tbl_Room();
            room.addData(new RoomEntity("NULL", width, height, x, y, room_name, description));
            room.close();
        }

        public void addRoom (string width, string height, string x, string y, string room_name)
        {
            tbl_Room room = new tbl_Room();
            room.addData(new RoomEntity("NULL", width, height, x, y, room_name));
            room.close();
        }
    }
}
