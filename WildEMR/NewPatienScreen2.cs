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
    [Activity(Label = "New Patient Screen")]
    public class NewPatientScreen2 : Activity
    {

        Button save_btn;
        Button cancel_btn;
        EditText height_text;
        EditText weight_text;
        EditText notes_text;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewPatientScreen2);

            String id_string = Intent.GetStringExtra("ANIMAL_ID");
            String species_string = Intent.GetStringExtra("ANIMAL_SPECIES");
            height_text = (EditText)FindViewById(Resource.Id.edit_height);
            weight_text = (EditText)FindViewById(Resource.Id.edit_weight);
            notes_text = (EditText)FindViewById(Resource.Id.edit_additional_notes);

            save_btn = FindViewById<Button>(Resource.Id.save_button2);
            save_btn.Click += delegate
            {
                String height_string = height_text.Text;
                String weight_string = weight_text.Text;
                String notes_string = notes_text.Text;
                var existing_info_screen = new Intent(this, typeof(MainActivity));
                Bundle extras = new Bundle();
                extras.PutString("ANIMAL_ID", id_string);
                extras.PutString("ANIMAL_SPECIES", species_string);
                extras.PutString("ANIMAL_HEIGHT", height_string);
                extras.PutString("ANIMAL_WEIGHT", weight_string);
                extras.PutString("ANIMAL_NOTES", notes_string);
                existing_info_screen.PutExtras(extras);
                StartActivity(existing_info_screen);
            };

            cancel_btn = FindViewById<Button>(Resource.Id.cancel_button2);
            cancel_btn.Click += delegate
            {
                StartActivity(typeof(MainActivity));
            };
        }
    }
}