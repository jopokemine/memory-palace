using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DataBank {
	public class tbl_Connected_Rooms : SqliteHelper {
		private const String Tag = "Team1: tbl_Connected_Rooms:\t";
	
        	private const String TABLE_NAME = "tbl_Connected_Rooms";
        	
			private const String CONNECTED_ID = "connected_id";
			private const String ROOM_DOOR_ID1 = "room_door_id1";
        	private const String ROOM_DOOR_ID2 = "room_door_id2";
        	private String[] COLUMNS = new String[] {CONNECTED_ID, ROOM_DOOR_ID1, ROOM_DOOR_ID2};

        	public tbl_Connected_Rooms() : base()
        	{
            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                		CONNECTED_ID + " INTEGER PRIMARY KEY, " +
                		ROOM_DOOR_ID1 + " INTEGER NOT NULL, " +
                        ROOM_DOOR_ID2 + " INTEGER NOT NULL, " +
                        "FOREIGN KEY (" + ROOM_DOOR_ID1 + ") REFERENCES tbl_Room_Door(room_door_id)"+
                        "FOREIGN KEY (" + ROOM_DOOR_ID2 + ") REFERENCES tbl_Room_Door(room_door_id)"+
                        ")";
            		dbcmd.ExecuteNonQuery();
        	}

        	public void addData(ConnectedRoomEntity connected_room)
        	{
            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText =
                		"INSERT INTO " + TABLE_NAME
                		+ " ( "
                		+ ROOM_DOOR_ID1 + ", "
                		+ ROOM_DOOR_ID2 + " ) "

                		+ "VALUES ( '"
                		+ connected_room._room_door_id1 + "', '"
                		+ connected_room._room_door_id2 + "' )";
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
                		"SELECT * FROM " + TABLE_NAME + " WHERE " + CONNECTED_ID + " = '" + str + "'";
            		return dbcmd.ExecuteReader();
        	}

        	public override void deleteDataByString(string id)
        	{
            		Debug.Log(Tag + "Deleting Location: " + id);

            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText =
                		"DELETE FROM " + TABLE_NAME + " WHERE " + CONNECTED_ID + " = '" + id + "'";
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