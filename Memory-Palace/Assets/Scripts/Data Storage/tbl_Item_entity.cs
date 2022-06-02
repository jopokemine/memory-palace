using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBank
{
	public class ItemEntity {

        public string _item_id;
        public string _item_name;
        public string _pos_x;
        public string _pos_y;
        public string _storage_id;

        public ItemEntity(string item_id, string item_name, string pos_x, string pos_y, string storage_id)
        {
            _item_id = item_id;
            _item_name = item_name;
            _pos_x = pos_x;
            _pos_y = pos_y;
            _storage_id = storage_id;
        }
	}
}