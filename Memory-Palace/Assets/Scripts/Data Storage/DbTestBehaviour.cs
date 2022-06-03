using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;

public class DbTestBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		tbl_Room room = new tbl_Room();
		tbl_Bluetooth_Device btd = new tbl_Bluetooth_Device();
		tbl_Room_Door room_door  = new tbl_Room_Door();
		tbl_Storage_Location storage_location = new tbl_Storage_Location();
		tbl_Item item            = new tbl_Item();
		tbl_Connected_Rooms connected_rooms = new tbl_Connected_Rooms();
		
		//Add Data
		room.addData(new RoomEntity("NULL","0.001", "0.007", "2.0", "3.0", "Kitchen", "Place with the food in"));
		room.addData(new RoomEntity("NULL","0.001", "0.007", "2.0", "3.0", "Hallway", "The hall"));
		btd.addData(new BluetoothDeviceEntity("NULL","BTD1", "5.0", "0.05", "1"));
		room_door.addData(new RoomDoorEntity("NULL","1", "2"));
		storage_location.addData(new StorageLocationEntity("NULL","Shelf", "0.05", "0.004", "1"));
		connected_rooms.addData(new ConnectedRoomEntity("NULL","1","2"));
		item.addData(new ItemEntity("NULL","Keys", "0.006", "0.002", "1"));
		item.addData(new ItemEntity("NULL","Medicine", "0.006", "0.002", "1"));
		item.addData(new ItemEntity("NULL","Wallet", "0.006", "0.002", "1"));
		
		room.close();
		btd.close();
		room_door.close();
		storage_location.close();
		connected_rooms.close();
		item.close();
}
}