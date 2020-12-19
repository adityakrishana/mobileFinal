using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cars
{
    [Activity(Label = "Tab1")]
    public class Tab1 : Activity
    {
        EditText ednum, edage, edpass,edname;
        TextView edview;
        Button btname, btnum, btage, btpass;
        string title;
        Realm realmDB;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.Home);

            realmDB = Realm.GetInstance();

            edview = FindViewById<TextView>(Resource.Id.textView1);
            edpass = FindViewById<EditText>(Resource.Id.editText1);
            edname = FindViewById<EditText>(Resource.Id.editText2);
            ednum = FindViewById<EditText>(Resource.Id.editText3);
            edage = FindViewById<EditText>(Resource.Id.editText4);

            btpass = FindViewById<Button>(Resource.Id.button1);
            btname = FindViewById<Button>(Resource.Id.button2);
            btnum = FindViewById<Button>(Resource.Id.button3);
            btage = FindViewById<Button>(Resource.Id.button4);

            string exemail = Intent.GetStringExtra("email");
            edview.Text = exemail;
            var t = realmDB.All<Member>().Where(d => d.email == exemail);
            foreach (Member m in t)
            {
                title = m.name;
            }
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar1);
            toolbar.Title = "Welcome " + title;
            toolbar.InflateMenu(Resource.Menu.cartab);
            toolbar.MenuItemClick += (sender, e) => {
                if (e.Item.ItemId == Resource.Id.home)
                {
                    Intent hScreen = new Intent(this, typeof(Tab1));
                    hScreen.PutExtra("email", exemail);
                    StartActivity(hScreen);
                }
                else if (e.Item.ItemId == Resource.Id.favourite)
                {
                    Intent fScreen = new Intent(this, typeof(Tab2));
                    fScreen.PutExtra("email", exemail);
                    StartActivity(fScreen);
                }
                else
                {
                    Intent CScreen = new Intent(this, typeof(Tab3));
                    CScreen.PutExtra("email", exemail);
                    StartActivity(CScreen);
                }
            };



            realmDB = Realm.GetInstance();
            var mem = realmDB.All<Member>().Where(d => d.email == exemail);
            foreach (var s in mem)
            {
                edview.Text = "Name: " + s.email;
                ednum.Text = s.number;
                edage.Text = s.age;
                edname.Text = s.name;
                edpass.Text = s.password;
            }

            btname.Click += delegate
            {
                foreach (var a in mem)
                {
                    realmDB.Write(() =>
                    {
                        a.name = edname.Text.ToString();
                        Toast.MakeText(this, "Name Updated", ToastLength.Long).Show();
                    });
                }
            };

            btpass.Click += delegate
            {
                foreach (var d in mem)
                {
                    realmDB.Write(() =>
                    {
                        d.password = edpass.Text.ToString();
                        Toast.MakeText(this, "Password Updated", ToastLength.Long).Show();
                    });
                }
            };

            btnum.Click += delegate
            {
                foreach (var b in mem)
                {
                    realmDB.Write(() =>
                    {
                        b.number = ednum.Text.ToString();
                        Toast.MakeText(this, "Phone Number Updated", ToastLength.Long).Show();
                    });
                }
            };
            btage.Click += delegate
            {
                foreach (var c in mem)
                {
                    realmDB.Write(() =>
                    {
                        c.age = edage.Text.ToString();
                        Toast.MakeText(this, "Age Updated", ToastLength.Long).Show();
                    });
                }
            };

        }
    }
}