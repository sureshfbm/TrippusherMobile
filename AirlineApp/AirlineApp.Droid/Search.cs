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
    class Search : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var view = inflater.Inflate(Resource.Layout.Search, container, false);
            Button login = view.FindViewById<Button>(Resource.Id.button1);
            CheckBox UserName = view.FindViewById<CheckBox>(Resource.Id.checkBox1);

            login.Click += delegate
            {
                if (UserName.Checked == true)
                {
                    UserName.Toggle();
                }
                else
                {
                    var AddTrip = new AddTrip();
                    var ft = FragmentManager.BeginTransaction();
                    ft.Replace(Resource.Id.fragment_container, AddTrip);
                    ft.Commit();
                }
            };
            return view;
        }
    }
}