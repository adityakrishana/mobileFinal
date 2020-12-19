using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AlertDialog = Android.App.AlertDialog;
using Realms;
using Android.Content;
using System.Linq;

namespace Cars
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText email;
        EditText password;
        Button loginBtn;
        Button signupBtn;
        string pass;
        string emailValue;
        Realm realmDB;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);


            email = FindViewById<EditText>(Resource.Id.editText1);
            password = FindViewById<EditText>(Resource.Id.editText2);
            loginBtn = FindViewById<Button>(Resource.Id.button1);
            signupBtn = FindViewById<Button>(Resource.Id.button2);

            realmDB = Realm.GetInstance();

            loginBtn.Click += delegate {


                emailValue = email.Text.Trim().ToLower();
                pass = password.Text;

                if ((emailValue == "" || emailValue == " ") || (pass == "" || pass == " "))
                {
                    displayAlertDialog("Incompatible Data", "Email Id or Password not Valid");

                }
                else
                {

                    var User = realmDB.All<Member>().Where(d => d.email == emailValue);
                    if (User.Count() > 0)
                    {
                        Intent welcomeScreen = new Intent(this, typeof(Tab1));
                        string exemail = email.Text.ToString();
                        welcomeScreen.PutExtra("email", exemail);
                        StartActivity(welcomeScreen);
                    }
                    else
                    {
                        displayAlertDialog("User not Found", "Member not registered on Database");
                    }
                }


            };

            signupBtn.Click += delegate {

                Intent newRegisterScreen = new Intent(this, typeof(Signup));
                StartActivity(newRegisterScreen);
            };


        }

        public void displayAlertDialog(String title, String message)
        {

            AlertDialog.Builder alert = new AlertDialog.Builder(this);


            alert.SetTitle(title);
            alert.SetMessage(message);


            alert.SetPositiveButton("Yes", (senderAlert, args) =>
            {
                System.Console.WriteLine("Yes Button Clicked");

            });

            alert.SetNegativeButton("NO", (senderAlert, args) =>
            {
                System.Console.WriteLine("NO Button Clicked");

            });

            Dialog dialog = alert.Create();

            dialog.Show();

        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
