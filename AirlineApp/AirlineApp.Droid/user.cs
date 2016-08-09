using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AirlineApp.Droid
{
    public class user
    {
        private int _radio_id;
        public int radio_id
        {   get { return _radio_id; }
            set { _radio_id = value; }
        }

        private string _user_name;
        public string user_name
        {
            get { return _user_name; }
            set { _user_name = value; }
        }
        public string user_id { get; set; }
        //public string user_name { get; set; }
        public string user_pass { get; set; }
        public string user_mobile { get; set; }
        public Int64 pk_airline_id { get; set; }
        public String airline_title { get; set; }
        public int allow_calls { get; set; }
        //public int radio_id { get; set; }
        public override string ToString()
        {
            return airline_title;
        }

    }
}