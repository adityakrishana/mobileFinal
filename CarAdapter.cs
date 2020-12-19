using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cars
{
    public class CarAdapter : BaseAdapter<Model>
    {
        Activity context;
        List<Model> arrayList;

        public CarAdapter(Activity myContext, List<Model> myListItems)
        {
            this.context = myContext;
            this.arrayList = myListItems;
        }
        public override Model this[int position]
        {
            get { return arrayList[position]; }
        }

        public override int Count
        {

            get { return arrayList.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Model myModelObject = arrayList[position];

            View myview = convertView;

            if (myview == null)
            {
                myview = context.LayoutInflater.Inflate(Resource.Layout.carview, null);
            }

            ImageView myImage = myview.FindViewById<ImageView>(Resource.Id.imageView1);
            TextView myText = myview.FindViewById<TextView>(Resource.Id.textView1);


            myText.Text = myModelObject.name;
            myImage.SetImageResource(myModelObject.imageID);



            return myview;
        }
    }
}