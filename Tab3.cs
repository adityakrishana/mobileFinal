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
    [Activity(Label = "Tab3")]
    class Tab3 : Activity, SearchView.IOnQueryTextListener
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
/*
            Add cars explicitly to database
            realmDB.Write(() =>
            {
                realmDB.Add(new Cars { name = "Honda"});
                realmDB.Add(new Cars { name = "Toyota" });
                realmDB.Add(new Cars { name = "Hyundai" });
                realmDB.Add(new Cars { name = "Mercedes" });
                realmDB.Add(new Cars { name = "Jaguar" });
                realmDB.Add(new Cars { name = "Ford" });
                realmDB.Add(new Cars { name = "Mazda" });
                realmDB.Add(new Cars { name = "Subaru" });
                realmDB.Add(new Cars { name = "Kia" });
                realmDB.Add(new Cars { name = "Lamborghini" });
            });

    */


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



            myListview = FindViewById<ListView>(Resource.Id.listView1);
            resultList = getDataFromRealmDB();
            Adapter = new CarAdapter(this, resultList);
            myListview.Adapter = Adapter;
            myListview.TextFilterEnabled = false;
            setupSearchView();
            myListview.ItemClick += listViewClickIteam;

        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.cartab, menu);
            return base.OnPrepareOptionsMenu(menu);

        }
        public void listViewClickIteam(object sender, AdapterView.ItemClickEventArgs args)
        {

            int index = args.Position;

            Model aPerson = resultList[index];
            string favname = aPerson.name;

            displayAlertDialog("Add Car", "Do you want to add this car to favourite list?", favname);

        }

        private void setupSearchView()
        {

            mySearchView.SetIconifiedByDefault(false);
            mySearchView.SetOnQueryTextListener(this);
            mySearchView.SubmitButtonEnabled = true;
            mySearchView.SetQueryHint("Search");
        }

        public void displayAlertDialog(String title, String message, String favour)
        {
            string uemail = Intent.GetStringExtra("email");
            AlertDialog.Builder alert = new AlertDialog.Builder(this); ;

            alert.SetTitle(title);
            alert.SetMessage(message);


            alert.SetPositiveButton("Yes", (senderAlert, args) =>
            {
                Favcars favname = new Favcars();
                favname.name = favour;
                favname.mname = uemail;
                realmDB.Write(() =>
                {
                    realmDB.Add(favname);
                });
                Toast.MakeText(this, "Added to Favourite List", ToastLength.Long).Show();
            });

            alert.SetNegativeButton("NO", (senderAlert, args) =>
            {
                System.Console.WriteLine("No clciked");
            });

            Dialog dialog = alert.Create();

            dialog.Show();

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

            List<Model> dbRecordList = new List<Model>();
            var resultCollection = realmDB.All<Cars>();


            foreach (Cars carsObj in resultCollection)
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