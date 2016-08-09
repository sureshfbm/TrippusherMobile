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
using System.Json;
using System.Net;
using System.Collections.Specialized;

namespace AirlineApp.Droid
{
    class AddTrip : Fragment
    {
        Button login;
        EditText UserName;
        EditText Password;
        user objUser;
        string result;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var view = inflater.Inflate(Resource.Layout.AddTrip, container, false);
            objUser = new user();
            login = view.FindViewById<Button>(Resource.Id.button1);
            UserName = view.FindViewById<EditText>(Resource.Id.editText1);
            Password = view.FindViewById<EditText>(Resource.Id.editText2);

            login.Click += delegate
            {
                objUser.user_name = UserName.Text.ToString();
                objUser.user_pass = Password.Text.ToString();
                LoginUser(objUser);
            };

            return view;
        }
        private void LoginUser(user objUser)
        {
            WebClient client = new WebClient();
            Uri uri = new Uri("http://trippusher.funnelboostmedia.net/restAPIs/api/user_login");
            NameValueCollection parametar = new NameValueCollection();
            parametar.Add("user_name", objUser.user_name.ToString());
            parametar.Add("password", objUser.user_pass.ToString());
            client.UploadValuesCompleted += Client_LoginValuesCompleted;
            client.UploadValuesAsync(uri, parametar);
        }
        private void Client_LoginValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            result = Encoding.UTF8.GetString(e.Result);
            var content = JsonObject.Parse(result);
            int UID = content["status_id"];
            string MSG = content["status_msg"];
            if (UID == 1)
            {
                //Toast.MakeText(this, MSG, ToastLength.Long).Show();
                //StartActivity(typeof(MenuBarActivity));
                UserName.Text = MSG.ToString();
            }
            else
            {
                //Toast.MakeText(this, "Try Again", ToastLength.Long).Show();
                //Intent refresh = new Intent(this, typeof(LoginActivity));
                //Finish();
                //StartActivity(refresh);
                Password.Text = MSG.ToString();
            }

        }
    }
}