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
    public class Model
    {
        public String name;
        public int imageID;


        public Model(String nameInfo, int imageInfo)
        {
            this.name = nameInfo;
            this.imageID = imageInfo;
        }

    }
}