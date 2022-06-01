using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DataBank {
	public class tbl_Storage_Location : SqliteHelper {
		private const String Tag = "Team1: tbl_Storage_Location:\t";
	
        	private const String TABLE_NAME = "tbl_Storage_Location";
        	
			private const String STORAGE_ID = "storage_id";
			private const String STORAGE_NAME = "storage_name";
        	private const String POS_X = "pos_x";
            private const String POS_Y = "pos_y";
            private const String ROOM_ID = "room_id";
        	private String[] COLUMNS = new String[] {STORAGE_ID, STORAGE_NAME, POS_X, POS_Y, ROOM_ID};

        	public tbl_Storage_Location() : base()
        	{
            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                		STORAGE_ID + " INTEGER PRIMARY KEY, " +
                		STORAGE_NAME + " TEXT NOT NULL, " +
                        POS_X + " REAL NOT NULL, " +
                        POS_Y + " REAL NOT NULL, " +
                        ROOM_ID + " INTEGER NOT NULL, " +
                        "FOREIGN KEY (" + ROOM_ID + ") REFERENCES tbl_Room(room_id)"+
                        ")";
            		dbcmd.ExecuteNonQuery();
        	}

        	public void addData(StorageLocationEntity storage_location)
        	{
            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText =
                		"INSERT INTO " + TABLE_NAME
                		+ " ( "
                		+ STORAGE_NAME + ", "
                		+ POS_X + ", "
                        + POS_Y + ", "
                        + ROOM_ID +" ) "

                		+ "VALUES ( '"
                		+ storage_location._storage_name + "', '"
                		+ storage_location._pos_x + "', '"
                		+ storage_location._pos_y + "', '"
                        + storage_location._room_id + "' )";
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
                		"SELECT * FROM " + TABLE_NAME + " WHERE " + STORAGE_ID + " = '" + str + "'";
            		return dbcmd.ExecuteReader();
        	}

        	public override void deleteDataByString(string id)
        	{
            		Debug.Log(Tag + "Deleting Location: " + id);

            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText =
                		"DELETE FROM " + TABLE_NAME + " WHERE " + STORAGE_ID + " = '" + id + "'";
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