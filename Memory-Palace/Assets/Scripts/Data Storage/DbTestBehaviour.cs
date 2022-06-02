using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;

public class DbTestBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		tbl_Room_Door room_door  = new tbl_Room_Door();
		tbl_Item item            = new tbl_Item();
		tbl_Storage_Location storage_location = new tbl_Storage_Location();
		tbl_Connected_Rooms connected_rooms = new tbl_Connected_Rooms();

		//Add Data
		room.addData(new RoomEntity("NULL","0.001", "0.007", "2.0", "3.0", "Test Room", "Room description"));
		btd.addData(new BluetoothDeviceEntity("NULL","BTD1", "5.0", "0.05", "1"));
		room_door.addData(new RoomDoorEntity("NULL","5", "2"));
		storage_location.addData(new StorageLocationEntity("NULL","Test drawer", "0.05", "0.004", "1"));
		connected_rooms.addData(new ConnectedRoomEntity("NULL","3","5"));
		item.addData(new ItemEntity("NULL","Test item", "0.006", "0.002", "1"));
		
		room.close();
		btd.close();
		room_door.close();
		storage_location.close();
		connected_rooms.close();
		item.close();



		// //Fetch All Data
		// LocationDb mLocationDb2 = new LocationDb();
		// System.Data.IDataReader reader = mLocationDb2.getAllData();

		// int fieldCount = reader.FieldCount;
		// List<TestEntity> myList = new List<TestEntity>();
		// while (reader.Read())
		// {
		// 	TestEntity entity = new TestEntity(	reader[0].ToString(), 
		// 												reader[1].ToString(), 
		// 												reader[2].ToString(),
		// 												reader[3].ToString(), 
		// 												reader[4].ToString());

		// 	Debug.Log("id: " + entity._id + " Lat: " + entity._Lat + " Type: " + entity._type);
		// 	myList.Add(entity);
		// }
	
	// Update is called once per frame
	void Update () {
		
	}
}
}