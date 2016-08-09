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
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;

namespace AirlineApp.Droid
{
    [Activity(Label = "MenuBarActivity", Theme = "@style/Theme.DesignDemo")]
    public class MenuBarActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.menu);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            ListView search = FindViewById<ListView>(Resource.Id.nav_Search);


            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
        }

       
        
        void NavigationView_NavigationItemSelected (object sender, NavigationView.NavigationItemSelectedEventArgs e)
            {
                e.MenuItem.SetChecked(true);
                drawerLayout.CloseDrawers();
            var ft = FragmentManager.BeginTransaction();
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_Search):
                    // React on 'nav_home' selection
                    Toast.MakeText(this, "search", ToastLength.Long).Show();
                    var Search = new Search();
                    ft.Replace(Resource.Id.fragment_container, Search);
                    ft.Commit();
                    //SetContentView(Resource.Layout.AddTrip);
                    //Button msg = FindViewById<Button>(Resource.Id.button1);
                    //msg.Click += delegate { Toast.MakeText(this, "addtrip", ToastLength.Long).Show();  };
                    break;
                case (Resource.Id.nav_AddTrip):
                    Toast.MakeText(this, "addtrip", ToastLength.Long).Show();
                    var AddTrip = new AddTrip();                    
                    ft.Replace(Resource.Id.fragment_container, AddTrip);
                    ft.Commit();
                    break;
                case (Resource.Id.nav_BrowseAll):
                    // React on 'Friends' selection
                    Toast.MakeText(this, "browseall", ToastLength.Long).Show();
                    break;
                case (Resource.Id.nav_ChatRooms):
                    // React on 'nav_home' selection
                    Toast.MakeText(this, "chatroom", ToastLength.Long).Show();
                    break;
                case (Resource.Id.nav_Messages):
                    Toast.MakeText(this, "message", ToastLength.Long).Show();
                    break;
                case (Resource.Id.nav_Notifications):
                    // React on 'Friends' selection
                    Toast.MakeText(this, "Notifications", ToastLength.Long).Show();
                    break;
                case (Resource.Id.nav_Setting):
                    // React on 'Friends' selection
                    Toast.MakeText(this, "setting", ToastLength.Long).Show();
                    break;
            }

            // Close drawer
            drawerLayout.CloseDrawers();
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);

        }
    }
}