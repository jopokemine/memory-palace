using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DataBank {
	public class tbl_Room : SqliteHelper {
		private const String Tag = "Team1: tbl_Room:\t";
	
        	private const String TABLE_NAME = "tbl_Room";
        	
			private const String ROOM_ID = "room_id";
			private const String POS_X = "pos_x";
        	private const String POS_Y = "pos_y";
        	private const String ROOM_WIDTH = "room_width";
        	private const String ROOM_HEIGHT = "room_height";
            private const String ROOM_NAME = "room_name";
            private const String ROOM_DESCRIPTION = "room_description";
        	private String[] COLUMNS = new String[] {ROOM_ID, ROOM_WIDTH, ROOM_HEIGHT, POS_X, POS_Y, ROOM_NAME, ROOM_DESCRIPTION};

        	public tbl_Room() : base()
        	{
            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                		ROOM_ID + " INTEGER PRIMARY KEY, " +
                		ROOM_WIDTH + " REAL NOT NULL, " +
                		ROOM_HEIGHT + " REAL NOT NULL, " +
                		POS_X + " REAL NOT NULL, " +
                		POS_Y + " REAL NOT NULL, " +
                        ROOM_NAME + " TEXT NOT NULL, " +
                        ROOM_DESCRIPTION + " TEXT )";
            		dbcmd.ExecuteNonQuery();
        	}

        	public void addData(RoomEntity room)
        	{
            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText =
                		"INSERT INTO " + TABLE_NAME
                		+ " ( "
                		+ ROOM_WIDTH + ", "
                		+ ROOM_HEIGHT + ", "
                        + POS_X + ", " 
                        + POS_Y + ", " 
                        + ROOM_NAME + ", "
                        + ROOM_DESCRIPTION 
                        +" ) "

                		+ "VALUES ( '"
                		+ room._room_width + "', '"
                		+ room._room_height + "', '"
                		+ room._pos_x + "', '" 
                        + room._pos_y + "', '"
                        + room._room_name + "', '"
                        + room._room_description 
                        + "' )";
            		dbcmd.ExecuteNonQuery();
        	}

        	public override IDataReader getDataById(int room_id)
        	{
            		return base.getDataById(room_id);
        	}

        	public override IDataReader getDataByString(string str)
        	{
            		Debug.Log(Tag + "Getting Location: " + str);

            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText =
                		"SELECT * FROM " + TABLE_NAME + " WHERE " + ROOM_ID + " = '" + str + "'";
            		return dbcmd.ExecuteReader();
        	}

        	public override void deleteDataByString(string id)
        	{
            		Debug.Log(Tag + "Deleting Location: " + id);

            		IDbCommand dbcmd = getDbCommand();
            		dbcmd.CommandText =
                		"DELETE FROM " + TABLE_NAME + " WHERE " + ROOM_ID + " = '" + id + "'";
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