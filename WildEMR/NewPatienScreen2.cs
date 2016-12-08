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
using System.Net;
using System.Collections.Specialized;
using Android.Graphics;

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
        string photo_string;
        Patient new_patient = new Patient();
        Record new_record = new Record();

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewPatientScreen2);

            //Get patient id and species from previous activity
            new_patient.Identifier = Intent.GetStringExtra("Patient_ID");
            new_patient.species_id = Intent.GetIntExtra("Patient_SPECIES",0);
            photo_string = Intent.GetStringExtra("Patient_Photo");

            var isnew = Intent.GetBooleanExtra("newPatient", true);

            new_patient.Records = new List<Record>();

            height_text = (EditText)FindViewById(Resource.Id.edit_height);
            weight_text = (EditText)FindViewById(Resource.Id.edit_weight);
            notes_text = (EditText)FindViewById(Resource.Id.edit_additional_notes);



            //Save button event handler,
            save_btn = FindViewById<Button>(Resource.Id.save_button2);
            save_btn.Click += async delegate
            {
                if (isnew)
                {
                    if (photo_string != "")
                    {
                        BitmapFactory.Options options = new BitmapFactory.Options();
                        var bitmap = BitmapFactory.DecodeFile(photo_string, options);
                        new_patient.SetImage(bitmap);
                    }
                    await new_patient.Create();
                }
                //Get all of the user input data for the new record
                new_record.Height = height_text.Text;
                new_record.Weight = weight_text.Text;
                new_record.Note = notes_text.Text;
                //Add the record data to the new patient
                await new_patient.AddRecord(new_record);
                
                //Pass the new patient information to the Patient Profile Screen
                var existing_info_screen = new Intent(this, typeof(PatientProfileScreen));
                Bundle extras = new Bundle();
                extras.PutString("Patient_ID", new_patient.Identifier);
                extras.PutInt("Patient_SPECIES", new_patient.species_id);
                extras.PutString("Patient_Photo", photo_string);
                extras.PutString("Patient_HEIGHT", new_record.Height);
                extras.PutString("Patient_WEIGHT", new_record.Weight);
                extras.PutString("Patient_NOTES", new_record.Note);
                extras.PutBoolean("NEW_PATIENT", isnew);
                existing_info_screen.PutExtras(extras);
                StartActivity(existing_info_screen);
            };

            //Cancel button handler, brings user back to Main Page
            cancel_btn = FindViewById<Button>(Resource.Id.cancel_button2);
            cancel_btn.Click += delegate
            {
                StartActivity(typeof(MainActivity));
            };

        }
    }
}