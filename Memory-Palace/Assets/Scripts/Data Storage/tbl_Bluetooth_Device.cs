using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DataBank{
	public class tbl_Bluetooth_Device : SqliteHelper {
		private const String Tag = "Team1: tbl_Bluetooth_Device:\t";
        
        	private const String TABLE_NAME = "tbl_Bluetooth_Device";
        	private const String DEVICE_ID = "device_id";
        	private const String POS_X = "pos_x";
        	private const String POS_Y = "pos_y";
            private const String DEVICE_NAME = "device_name";
            private const String ROOM_ID = "room_id";
        	private String[] COLUMNS = new String[] {DEVICE_ID, POS_X, POS_Y, DEVICE_NAME, ROOM_ID};

        	public tbl_Bluetooth_Device() : base()
        	{
            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                		DEVICE_ID + " INTEGER PRIMARY KEY, " +
                		POS_X + " REAL NOT NULL, " +
                		POS_Y + " REAL NOT NULL, " +
                        DEVICE_NAME + " TEXT NOT NULL, " +
                        ROOM_ID + " INTEGER NOT NULL, " +
                        "FOREIGN KEY (" + ROOM_ID + ") REFERENCES tbl_Room(room_id)"
                        + ")";
            		dbcmd.ExecuteNonQuery();
        	}

        	public void addData(BluetoothDeviceEntity btd)
        	{
            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText =
                		"INSERT INTO " + TABLE_NAME
                		+ " ( "
                        + POS_X + ", " 
                        + POS_Y + ", " 
                        + DEVICE_NAME + ", "
                        + ROOM_ID
                        +" ) "

                		+ "VALUES ( '"
                		+ btd._pos_x + "', '" 
                        + btd._pos_y + "', '"
                        + btd._device_name + "', '"
                        + btd._room_id + "' )";
            		dbcmd.ExecuteNonQuery();
        	}

        	public override IDataReader getDataById(int device_id)
        	{
            		return base.getDataById(device_id);
        	}

        	public override IDataReader getDataByString(string str)
        	{
            		Debug.Log(Tag + "Getting Location: " + str);

            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText =
                		"SELECT * FROM " + TABLE_NAME + " WHERE " + DEVICE_ID + " = '" + str + "'";
            		return dbcmd.ExecuteReader();
        	}

        	public override void deleteDataByString(string id)
        	{
            		Debug.Log(Tag + "Deleting Location: " + id);

            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText =
                		"DELETE FROM " + TABLE_NAME + " WHERE " + DEVICE_ID + " = '" + id + "'";
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

			public IDataReader getBluetoothDevices()
			{
				IDbCommand dbcmd = getDbCommand();
				string query = 
				"SELECT device_id, " + TABLE_NAME +".pos_x, " + TABLE_NAME + ".pos_y, device_name, room_name FROM "
				+ TABLE_NAME
				+" INNER JOIN tbl_Room on tbl_Room.room_id = "
				+ TABLE_NAME
				+".room_id;";

				dbcmd.CommandText = query;
				return dbcmd.ExecuteReader();
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
