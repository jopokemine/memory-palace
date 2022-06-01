using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBank
{
	public class StorageLocationEntity {

        public string _storage_id;
        public string _storage_name;
        public string _pos_x;
        public string _pos_y;
        public string _room_id;

        public StorageLocationEntity(string storage_id, string storage_name, string pos_x, string pos_y, string room_id)
        {
            _storage_id = storage_id;
            _storage_name = storage_name;
            _pos_x = pos_x;
            _pos_y = pos_y;
            _room_id = room_id;
        }
	}
}