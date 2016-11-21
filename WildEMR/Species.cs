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
    class Species
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
        public bool Rabies { get; set; }
    }
}