using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBank2
{
	public class TestEntity {

		public string _id;
        public String _type;
        public string _Lat;
        public string _Lng;
        public String _dateCreated; // Auto generated timestamp
        public String _test_f_key;

        // public TestEntity(String type, string Lat, string Lng)
        // {
        //     _id = null;
        //     _type = type;
        //     _Lat = Lat;
        //     _Lng = Lng;
		// 	_dateCreated = "";
        // }

        public TestEntity(string id, String type, string Lat, string Lng, string test_f_key)
        {
            _id = id;
            _type = type;
            _Lat = Lat;
            _Lng = Lng;
			_dateCreated = "";
            _test_f_key = test_f_key;
        }

        public TestEntity(string id, String type, string Lat, string Lng, string dateCreated, string test_f_key)
        {
            _id = id;
            _type = type;
            _Lat = Lat;
            _Lng = Lng;
			_dateCreated = dateCreated;
            _test_f_key = test_f_key;
        }
	}
}