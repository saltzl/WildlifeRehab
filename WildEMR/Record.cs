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
        public string Subjective { get; set; }
        public string Objective { get; set; }
        public string Assesment { get; set; }
        public string Plan { get; set; }
    }
}