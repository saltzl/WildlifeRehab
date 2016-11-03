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
    public class Patient
    {
        public string Patient_id { get; set; }
        public string Patient_species { get; set; }
        public List<Record> Record_list { get; set; }
    }
}