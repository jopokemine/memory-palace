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

        public List<string[]> getItemsInRoom ()
        {
            tbl_Item item = new tbl_Item();
            System.Data.IDataReader reader = item.getItemsInRoom();

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
    }
}
