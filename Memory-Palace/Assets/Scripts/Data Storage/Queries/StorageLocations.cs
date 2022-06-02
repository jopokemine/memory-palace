using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;

namespace DataBank
{
    public class StorageLocations : MonoBehaviour
    {
        public void addStorageLocation (string storage_name, string x, string y, string room_id)
        {
            tbl_Storage_Location storage_location = new tbl_Storage_Location();
            storage_location.addData(new StorageLocationEntity("NULL", storage_name, x, y, room_id));
            storage_location.close();
        }

        public void updateStorageLocation(string id, string new_name)
        {
            tbl_Storage_Location storage_location = new tbl_Storage_Location();
            storage_location.updateStorageLocation(id,new_name);
            storage_location.close();
        }
    }
}
