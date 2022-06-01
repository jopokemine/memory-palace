using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataBank
{
    public class Items : MonoBehaviour
    {
        public Items()
        {
            
        }

        public List<string[]> getItemsInRoom (string room_name)
        {
            tbl_Item item = new tbl_Item();
            System.Data.IDataReader reader = item.getItemsInRoom(room_name);

            int fieldCount = reader.FieldCount;
		    List<string[]> myList = new List<string[]>();
		    while (reader.Read())
		    {
                string[] entity = {
                    reader[0].ToString(), 
                    reader[1].ToString()
                };
                myList.Add(entity);
		    }
            item.close();
            return myList;
        }

        public List<string> getItems()
        {
            tbl_Item item = new tbl_Item();
            System.Data.IDataReader reader = item.getItems();

            int fieldCount = reader.FieldCount;
		    List<string> myList = new List<string>();
		    while (reader.Read())
		    {
                string entity = reader[0].ToString();
                myList.Add(entity);
		    }
            item.close();
            return myList;
        }
    }
}
