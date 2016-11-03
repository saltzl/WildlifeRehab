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
        public int Record_id { get; set; }
        public string Patient_height { get; set; }
        public string Patient_weight { get; set; }
        public string Patient_note { get; set; }
    }
}