using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DataBank {
	public class tbl_Item : SqliteHelper {
		private const String Tag = "Team1: tbl_Item:\t";
	
        	private const String TABLE_NAME = "tbl_Item";
        	
			private const String ITEM_ID = "item_id";
			private const String ITEM_NAME = "item_name";
        	private const String POS_X = "pos_x";
            private const String POS_Y = "pos_y";
            private const String STORAGE_ID = "storage_id";
        	private String[] COLUMNS = new String[] {ITEM_ID, ITEM_NAME, POS_X, POS_Y, STORAGE_ID};

        	public tbl_Item() : base()
        	{
            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                		ITEM_ID + " INTEGER PRIMARY KEY, " +
                		ITEM_NAME + " TEXT NOT NULL, " +
                        POS_X + " REAL NOT NULL, " +
                        POS_Y + " REAL NOT NULL, " +
                        STORAGE_ID + " INTEGER NOT NULL, " +
                        "FOREIGN KEY (" + STORAGE_ID + ") REFERENCES tbl_Storage_Location(storage_id)"+
                        ")";
            		dbcmd.ExecuteNonQuery();
        	}

        	public void addData(ItemEntity item_entity)
        	{
            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText =
                		"INSERT INTO " + TABLE_NAME
                		+ " ( "
                		+ ITEM_NAME + ", "
                		+ POS_X + ", "
                        + POS_Y + ", "
                        + STORAGE_ID +" ) "

                		+ "VALUES ( '"
                		+ item_entity._item_name + "', '"
                		+ item_entity._pos_x + "', '"
                		+ item_entity._pos_y + "', '"
                        + item_entity._storage_id + "' )";
            		dbcmd.ExecuteNonQuery();
        	}

        	public override IDataReader getDataById(int id)
        	{
            		return base.getDataById(id);
        	}

        	public override IDataReader getDataByString(string str)
        	{
            		Debug.Log(Tag + "Getting Location: " + str);

            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText =
                		"SELECT * FROM " + TABLE_NAME + " WHERE " + ITEM_ID + " = '" + str + "'";
            		return dbcmd.ExecuteReader();
        	}

        	public override void deleteDataByString(string id)
        	{
            		Debug.Log(Tag + "Deleting Location: " + id);

            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText =
                		"DELETE FROM " + TABLE_NAME + " WHERE " + ITEM_ID + " = '" + id + "'";
            		dbcmd.ExecuteNonQuery();
        	}

        	public override void deleteDataById(int id)
        	{
            		base.deleteDataById(id);
        	}

        	public override void deleteAllData()
        	{
            		Debug.Log(Tag + "Deleting Table");

            		base.deleteAllData(TABLE_NAME);
        	}

        	public override IDataReader getAllData()
        	{
            		return base.getAllData(TABLE_NAME);
        	}

			public IDataReader getItemsInRoom(string room_name)
			{
				IDbCommand dbcmd = getDbCommand();
				string query = 
				"SELECT item_name, storage_name, item_id, storage_id FROM "
				+ TABLE_NAME
				+" INNER JOIN tbl_Storage_Location on tbl_Storage_Location.storage_id = "
				+ TABLE_NAME
				+".storage_id "
				+"INNER JOIN tbl_Room on tbl_Room.room_id = "
				+"tbl_Storage_Location.room_id "
				+"WHERE tbl_Room.room_name = " + room_name + ";";

				dbcmd.CommandText = query;
				return dbcmd.ExecuteReader();
			}

			public IDataReader getItems()
			{
				IDbCommand dbcmd = getDbCommand();
				string query = 
				"SELECT item_name FROM "
				+ TABLE_NAME
				+";";

				dbcmd.CommandText = query;
				return dbcmd.ExecuteReader();
			}

			public IDataReader getItemRoom(string itemName)
			{
				IDbCommand dbcmd = getDbCommand();
				string query = 
				"SELECT room_name FROM " +
				TABLE_NAME +
				" INNER JOIN tbl_Storage_Location on tbl_Storage_Location.storage_id = " +
				TABLE_NAME +
				".storage_id " +
				"INNER JOIN tbl_Room on tbl_Room.room_id = " +
				"tbl_Storage_Location.room_id " +
				"WHERE item_name = '" + itemName + "';";

				dbcmd.CommandText = query;
				return dbcmd.ExecuteReader();
			}

			public IDataReader getItemPos(string itemName)
			{
				IDbCommand dbcmd = getDbCommand();
				string query = 
				"SELECT tbl_Storage_Location.pos_x, tbl_Storage_Location.pos_y FROM " +
				TABLE_NAME +
				" INNER JOIN tbl_Storage_Location on tbl_Storage_Location.storage_id = " +
				TABLE_NAME +
				".storage_id " +
				"WHERE item_name = '" + itemName + "';";

				dbcmd.CommandText = query;
				return dbcmd.ExecuteReader();
			}
    
			public void updateItemName (string id, string new_name)
			{
				IDbCommand dbcmd = getDbCommand();
				string query = 
				"UPDATE "
				+ TABLE_NAME
				+" SET item_name = "
				+ new_name
				+" WHERE item_id = "
				+ id
				+";";
				
				dbcmd.CommandText = query;
			}

        	// public IDataReader getNearestLocation(LocationInfo loc)
        	// {
            // 		Debug.Log(Tag + "Getting nearest centoid from: "
            //     		+ loc.latitude + ", " + loc.longitude);
            // 		IDbCommand dbcmd = getDbCommand();

            // 		string query =
            //     		"SELECT * FROM "
            //     		+ TABLE_NAME
            //     		+ " ORDER BY ABS(" + KEY_LAT + " - " + loc.latitude 
            //     		+ ") + ABS(" + KEY_LNG + " - " + loc.longitude + ") ASC LIMIT 1";

            // 		dbcmd.CommandText = query;
            // 		return dbcmd.ExecuteReader();
        	// }

        	// public IDataReader getLatestTimeStamp()
        	// {
            // 		IDbCommand dbcmd = getDbCommand();
            // 		dbcmd.CommandText =
            //     		"SELECT * FROM " + TABLE_NAME + " ORDER BY " + KEY_DATE + " DESC LIMIT 1";
            // 		return dbcmd.ExecuteReader();
        	// }
	}
}
