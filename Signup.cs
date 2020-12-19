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
    [Activity(Label = "Signup")]
    public class Signup : Activity
    {

        EditText mEmail, mName, mPass, mNum, mAge;
        Button rgsBtn;

        Realm realmDB;
        string memail, mname, mpass, mnum, mage;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.Register);

            realmDB = Realm.GetInstance();
            mEmail = FindViewById<EditText>(Resource.Id.editText1);
            mName = FindViewById<EditText>(Resource.Id.editText2);
            mPass = FindViewById<EditText>(Resource.Id.editText3);
            mNum = FindViewById<EditText>(Resource.Id.editText4);
            mAge = FindViewById<EditText>(Resource.Id.editText5);

            rgsBtn = FindViewById<Button>(Resource.Id.button1);



            rgsBtn.Click += delegate
            {
                memail = mEmail.Text;
                mname = mName.Text;
                mpass = mPass.Text;
                mnum = mNum.Text;
                mage = mAge.Text;



                if (memail.Trim() == "" && mname.Trim() == "" && mpass.Trim() == "" && mnum.Trim() == "" && mage.Trim() == "")
                {

                    displayAlertDialog("InValid Info", "Please Enter Valid Name & Password");
                }
                else
                {

                    Member memObj = new Member();


                    memObj.name = mname;
                    memObj.password = mpass;
                    memObj.age = mage;
                    memObj.number = mnum;
                    memObj.email = memail;


                    realmDB.Write(() =>
                    {
                        realmDB.Add(memObj);
                    });

                    displayAlertDialog("Member registered", "Member Added Succesfully to Database");

                }
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
    }
}