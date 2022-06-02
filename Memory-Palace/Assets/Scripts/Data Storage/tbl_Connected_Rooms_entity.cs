using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBank
{
	public class ConnectedRoomEntity {

        public string _connected_id;
        public string _room_door_id1;
        public string _room_door_id2;

        public ConnectedRoomEntity(string connected_id, string room_door_id1, string room_door_id2)
        {
            _connected_id = connected_id;
            _room_door_id1 = room_door_id1;
            _room_door_id2 = room_door_id2;

        }
	}
}