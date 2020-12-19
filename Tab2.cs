using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cars
{
    [Activity(Label = "Tab2")]
    class Tab2 : Activity, SearchView.IOnQueryTextListener
    {
        ListView myListview;
        string title;
        SearchView mySearchView;
        Realm realmDB;
        CarAdapter Adapter;

        int[] arrayInt = {Resource.Drawable.sa1,
            Resource.Drawable.sa2,
            Resource.Drawable.sa3,
            Resource.Drawable.sa4,
            Resource.Drawable.sa5,
            Resource.Drawable.sa6,
            Resource.Drawable.sa7
        };


        List<Model> resultList = new List<Model>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.carlist);
            mySearchView = FindViewById<SearchView>(Resource.Id.searchView1);


            realmDB = Realm.GetInstance();
            string exemail = Intent.GetStringExtra("email");
            var toolbarBottom = FindViewById<Toolbar>(Resource.Id.toolbar1);
            var cs = realmDB.All<Member>().Where(d => d.email == exemail);
            foreach (Member a in cs)
            {
                title = a.name;
            }
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar1);
            toolbar.Title = "Welcome " + title;
            toolbar.InflateMenu(Resource.Menu.cartab);
            toolbar.MenuItemClick += (sender, e) =>
            {
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



            myListview = FindViewById<ListView>(Resource.Id.listView1);
            resultList = getDataFromRealmDB();
            Adapter = new CarAdapter(this, resultList);
            myListview.Adapter = Adapter;
            myListview.TextFilterEnabled = false;
            setupSearchView();

        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.cartab, menu);
            return base.OnPrepareOptionsMenu(menu);

        }

        private void setupSearchView()
        {

            mySearchView.SetIconifiedByDefault(false);
            mySearchView.SetOnQueryTextListener(this);
            mySearchView.SubmitButtonEnabled = true;
            mySearchView.SetQueryHint("Search");
        }
        public bool OnQueryTextChange(string newText)
        {
            try
            {
                if (TextUtils.IsEmpty(newText))
                {
                    myListview.ClearTextFilter();
                    Adapter = new CarAdapter(this, resultList);
                    myListview.Adapter = Adapter;
                }
                else
                {

                    myListview.ClearTextFilter();
                    List<Model> yourListViewItems2 = (from i in resultList
                                                      where i.name.ToLower().Contains(newText.ToLower())
                                                      select i).ToList();
                    Adapter = new CarAdapter(this, yourListViewItems2);
                    myListview.Adapter = Adapter;
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
            }
            return true;
        }

        public bool OnQueryTextSubmit(string query)
        {
            return false;
        }


        public List<Model> getDataFromRealmDB()
        {

            string exemail = Intent.GetStringExtra("email");
            List<Model> dbRecordList = new List<Model>();
            var resultCollection = realmDB.All<Favcars>().Where(d => d.mname == exemail);


            foreach (Favcars carsObj in resultCollection)
            {

                String nameFromDB = carsObj.name;

                Random r = new Random();
                int rint = r.Next(1, 7);
                int valueFromArray = arrayInt[rint];

                Model temp = new Model(nameFromDB, valueFromArray);
                dbRecordList.Add(temp);
            }

            return dbRecordList;
        }
    }
    }