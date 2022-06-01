using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataBank
{
    public class BluetoothDevices : MonoBehaviour
    {
        public BluetoothDevices()
        {
            
        }

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
    }
}
