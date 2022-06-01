using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBank
{
	public class RoomEntity {

        public string _room_id;
        public string _room_width;
        public string _room_height;
        public string _pos_x;
        public string _pos_y;
        public string _room_name;
        public string _room_description;

        public RoomEntity(string room_id, string room_width, string room_height, string pos_x, string pos_y, string room_name)
        {
            _room_id = room_id;
            _room_width = room_width;
            _room_height = room_height;
            _pos_x = pos_x;
            _pos_y = pos_y;
            _room_name = room_name;
            _room_description = "";
        }

        public RoomEntity(string room_id, string room_width, string room_height, string pos_x, string pos_y, string room_name, string room_description)
        {
            _room_id = room_id;
            _room_width = room_width;
            _room_height = room_height;
            _pos_x = pos_x;
            _pos_y = pos_y;
            _room_name = room_name;
            _room_description = room_description;

        }
	}
}