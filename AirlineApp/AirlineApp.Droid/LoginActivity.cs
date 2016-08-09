using System;
using System.Text;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Json;
using System.Net;
using System.Collections.Specialized;
using Android.Content;

namespace AirlineApp.Droid
{
    [Activity(Label = "Airline", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        Button login;
        TextView textSignup;
        EditText UserName;
        EditText Password;
        user objUser;
        string result;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);

            objUser = new user();
            login = FindViewById<Button>(Resource.Id.btnlogin);
            textSignup = FindViewById<TextView>(Resource.Id.txtsignup);
            UserName = FindViewById<EditText>(Resource.Id.txtUser);
            Password = FindViewById<EditText>(Resource.Id.txtPass);

            login.Click += delegate
            {
                if (String.IsNullOrEmpty(UserName.Text))
                {
                    UserName.Error = "Please Enter Your Email";
                    UserName.RequestFocus();
                }
                else
                {
                    if (String.IsNullOrEmpty(Password.Text))
                    {
                        Password.Error = "Please Enter Your Password";
                        Password.RequestFocus();
                    }
                    else
                    {
                        objUser.user_name = UserName.Text.ToString();
                        objUser.user_pass = Password.Text.ToString();
                        LoginUser(objUser);
                    }
                }                
            };
            textSignup.Click += delegate
            {
                StartActivity(typeof(RegistrationActivity));
            };
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
                Toast.MakeText(this, MSG, ToastLength.Long).Show();
                StartActivity(typeof(MenuBarActivity));
            }
            else
            {
                Toast.MakeText(this, "Try Again", ToastLength.Long).Show();
                Intent refresh = new Intent(this, typeof(LoginActivity));
                Finish();
                StartActivity(refresh);
            }
        }       
    }
}