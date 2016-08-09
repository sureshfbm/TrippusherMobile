using System;
using System.Collections.Generic;
using System.Text;

using Android.App;
using Android.OS;
using Android.Widget;
using System.Json;
using System.Net;
using System.Collections.Specialized;
using AirlineApp.Droid;
using Android.Content;

namespace AirlineApp.Droid
{
    [Activity(Label = "Airline")]
    public class RegistrationActivity : Activity
    {
        List<user> AirlineList;
        Spinner spinner;
        EditText user_name;
        EditText user_mobile;
        EditText user_pass;
        Switch allow_calls;
        ArrayAdapter adapter;
        Button registration;
        string result;
        //string resultCode;
        RadioButton radio_5;
        RadioButton radio_50;
        PayPalManager MainManager;
        user objUser;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Registration);
            MainManager = new PayPalManager(this);
            objUser = new user();
            registration = FindViewById<Button>(Resource.Id.btnRsignup);
            spinner = FindViewById<Spinner>(Resource.Id.spiAir);
            user_name = FindViewById<EditText>(Resource.Id.txtName);
            user_mobile = FindViewById<EditText>(Resource.Id.txtMobile);
            user_pass = FindViewById<EditText>(Resource.Id.txtPassword);
            allow_calls = FindViewById<Switch>(Resource.Id.switch1);
            radio_5 = FindViewById<RadioButton>(Resource.Id.radio5);
            radio_50 = FindViewById<RadioButton>(Resource.Id.radio50);
            AirlineList = new List<user>();

            WebClient client = new WebClient();
            Uri uri = new Uri("http://trippusher.funnelboostmedia.net/restAPIs/api/get_airlines");
            NameValueCollection parametar = new NameValueCollection();
            parametar.Add("status_id", "1");
            client.UploadValuesCompleted += Client_UploadValuesCompleted1;
            client.UploadValuesAsync(uri, parametar);


            registration.Click += delegate
            {
                //validateFormInput();
                if (String.IsNullOrEmpty(user_name.Text))
                {
                    user_name.Error = "Please Enter Your Name";
                    user_name.RequestFocus();
                }
                else
                {
                    if (String.IsNullOrEmpty(user_pass.Text))
                    {
                        user_pass.Error = "Please Enter Your Password";
                        user_pass.RequestFocus();
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(user_mobile.Text))
                        {
                            user_mobile.Error = "Please Enter Your Mobile";
                            user_mobile.RequestFocus();
                        }
                        else
                        {
                            if (radio_5.Checked == true && radio_50.Checked == false)
                            {
                                objUser.radio_id = 5;
                            }
                            else 
                                if (radio_5.Checked == false && radio_50.Checked == true)
                                {
                                    objUser.radio_id = 50;
                                }
                                objUser.pk_airline_id = spinner.SelectedItemId;
                                objUser.user_name = user_name.Text.ToString();
                                objUser.user_mobile = user_mobile.Text.ToString();
                                objUser.user_pass = user_pass.Text.ToString();
                                if (allow_calls.Checked == true)
                                {
                                    objUser.allow_calls = 1;
                                }
                                else
                                {
                                    objUser.allow_calls = 0;
                                }
                                if (objUser.radio_id != 0)
                                {
                                    
                                    MainManager.BuySomething(objUser);
                                    //SaveItem(objUser);
                                }
                                else
                                {
                                    Toast.MakeText(this, "Choose Your Subscription First", ToastLength.Long).Show();
                                }
                        }
                    }
                }             
                
            };
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            MainManager.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                SaveItem(objUser);
            }
            else if (resultCode ==Result.Canceled)
            {
                Toast.MakeText(this, "Payment Canceled ", ToastLength.Long).Show();
                Intent refresh = new Intent(this, typeof(RegistrationActivity));
                refresh.AddFlags(ActivityFlags.NoAnimation);
                Finish();
                StartActivity(refresh);
            }
        }

        protected override void OnDestroy()
        {
            if (MainManager != null)
            { 
            MainManager.Destroy();
            base.OnDestroy();
            }
        }

        private void SaveItem(user objUser)
        {
            WebClient client = new WebClient();
            Uri uri = new Uri("http://trippusher.funnelboostmedia.net/restAPIs/api/create_user/");
            NameValueCollection parametar = new NameValueCollection();
            parametar.Add("airline_id", objUser.pk_airline_id.ToString());
            parametar.Add("user_name", objUser.user_name.ToString());
            parametar.Add("phone_no", objUser.user_mobile.ToString());
            parametar.Add("password", objUser.user_pass.ToString());
            parametar.Add("allow_calls", objUser.allow_calls.ToString());
            client.UploadValuesCompleted += Client_UploadValuesCompleted;
            client.UploadValuesAsync(uri, parametar);
        }

        private void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            result = Encoding.UTF8.GetString(e.Result);
            var content = JsonObject.Parse(result);
            int UID = content["status_id"];
            string MSG = content["status_msg"];
            if (UID == 1)
            {
                Toast.MakeText(this, MSG, ToastLength.Long).Show();
            }
        }

        private void Client_UploadValuesCompleted1(object sender, UploadValuesCompletedEventArgs e)
        {
            result = Encoding.UTF8.GetString(e.Result);
            var content = JsonObject.Parse(result);
            int UID = content["status_id"];
            string MSG = content["status_msg"];
            if (UID == 1)
            {
                var json = JsonValue.Parse(result);
                var data = json["data"];
                AirlineList.Add(new user() { airline_title = "select airline", pk_airline_id = 0 });
                foreach (object dataItem in data)
                {
                    string dataItemStr = dataItem.ToString();
                    var Item = JsonObject.Parse(dataItemStr);
                    AirlineList.Add(new user() { airline_title = Item["airline_title"], pk_airline_id = Item["pk_airline_id"] });
                    adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, AirlineList);
                }
                spinner.Adapter = adapter;
            }
        }

        void validateFormInput()
        {
            if (String.IsNullOrEmpty(user_name.Text))
            {
                user_name.Error = "Please Enter Your Name";
                user_name.RequestFocus();
            }
            else
            {
                if (String.IsNullOrEmpty(user_pass.Text))
                {
                    user_pass.Error = "Please Enter Your Password";
                    user_pass.RequestFocus();
                }
                else
                {
                    if (String.IsNullOrEmpty(user_mobile.Text))
                    {
                        user_mobile.Error = "Please Enter Your Mobile";
                        user_mobile.RequestFocus();
                    }
                }
            }

        }
    };
}