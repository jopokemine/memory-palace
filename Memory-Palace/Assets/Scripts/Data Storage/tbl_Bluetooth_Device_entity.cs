using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBank
{
	public class BluetoothDeviceEntity {

        public string _device_id;
        public string _pos_x;
        public string _pos_y;
        public string _device_name;
        public string _room_id;

        public BluetoothDeviceEntity(string device_id, string pos_x, string pos_y, string device_name, string room_id)
        {
            _device_id = device_id;
            _pos_x = pos_x;
            _pos_y = pos_y;
            _device_name = device_name;
            _room_id = room_id;
        }
	}
}