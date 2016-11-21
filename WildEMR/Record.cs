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

namespace WildEMR
{
    public class Record
    {
        public int ID { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Note { get; set; }
    }
}