using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBank
{
	public class RoomDoorEntity {

        public string _room_door_id;
        public string _room_id;
        public string _wall_pos;

        public RoomDoorEntity(string room_door_id, string room_id, string wall_pos)
        {
            _room_door_id = room_door_id;
            _room_id = room_id;
            _wall_pos = wall_pos;
        }
	}
}