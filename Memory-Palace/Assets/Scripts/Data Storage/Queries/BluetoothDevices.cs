using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataBank
{
    public class BluetoothDevices : MonoBehaviour
    {
        public List<string[]> getBluetoothDevices ()
        {
            tbl_Bluetooth_Device btd = new tbl_Bluetooth_Device();
            System.Data.IDataReader reader = btd.getBluetoothDevices();

            int fieldCount = reader.FieldCount;
		    List<string[]> myList = new List<string[]>();
		    while (reader.Read())
		    {
                string[] entity = {
                    reader[0].ToString(), 
                    reader[1].ToString(), 
                    reader[2].ToString(),
                    reader[3].ToString(),
                    reader[4].ToString()
                };
                myList.Add(entity);
		    }
            btd.close();
            return myList;
        }

        public void addBluetoothDevice (string x, string y, string device_name, string room_id)
        {
            tbl_Bluetooth_Device btd = new tbl_Bluetooth_Device();
            btd.addData(new BluetoothDeviceEntity("NULL", x, y, device_name, room_id));
            btd.close();
        }
    }
}
