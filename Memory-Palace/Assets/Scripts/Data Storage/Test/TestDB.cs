using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DataBank2 {
	public class TestDB : SqliteHelper {
		private const String CodistanTag = "Codistan: TestDB:\t";
        
        private const String TABLE_NAME = "Test";
        private const String KEY_ID = "id";
        private const String KEY_TYPE = "type";
        private const String KEY_LAT = "Lat";
        private const String KEY_LNG = "Lng";
		private const String KEY_DATE = "date";
        private const String TEST_F_KEY = "test_f_key";
        private String[] COLUMNS = new String[] {KEY_ID, KEY_TYPE, KEY_LAT, KEY_LNG, KEY_DATE, TEST_F_KEY};

        public TestDB() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER PRIMARY KEY, " +
                KEY_TYPE + " TEXT, " +
                KEY_LAT + " REAL, " +
                KEY_LNG + " REAL, " +
                KEY_DATE + " DATETIME DEFAULT CURRENT_TIMESTAMP, "+
                TEST_F_KEY + " INTEGER NOT NULL, " + 
                "FOREIGN KEY (" + TEST_F_KEY + ") REFERENCES Locations(id)"+
                ")";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(TestEntity location)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                // + KEY_ID + ", "
                + KEY_TYPE + ", "
                + KEY_LAT + ", "
                + KEY_LNG + ", "
                + TEST_F_KEY + " ) "

                + "VALUES ( '"
                // + location._id + "', '"
                + location._type + "', '"
                + location._Lat  + "', '"
                + location._Lng  + "', '"
                + location._test_f_key + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public override IDataReader getDataById(int id)
        {
            return base.getDataById(id);
        }

        public override IDataReader getDataByString(string str)
        {
            Debug.Log(CodistanTag + "Getting Location: " + str);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_TYPE + " = '" + str + "'";
            return dbcmd.ExecuteReader();
        }

        public override void deleteDataByString(string id)
        {
            Debug.Log(CodistanTag + "Deleting Location: " + id);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
            dbcmd.ExecuteNonQuery();
        }

		public override void deleteDataById(int id)
        {
            base.deleteDataById(id);
        }

        public override void deleteAllData()
        {
            Debug.Log(CodistanTag + "Deleting Table");

            base.deleteAllData(TABLE_NAME);
        }

        public override IDataReader getAllData()
        {
            return base.getAllData(TABLE_NAME);
        }

        public IDataReader getLatestTimeStamp()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " ORDER BY " + KEY_DATE + " DESC LIMIT 1";
            return dbcmd.ExecuteReader();
        }
	}
}