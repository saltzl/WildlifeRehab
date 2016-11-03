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
        Patient new_patient = new Patient();
        Record new_record = new Record();

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewPatientScreen2);

            //Database connection variables
            WebClient client = new WebClient();
            Uri uri = new Uri("http://ec2-52-15-97-1.us-east-2.compute.amazonaws.com/");
            NameValueCollection parameters = new NameValueCollection();

            //Get patient id and species from previous activity
            new_patient.Patient_id = Intent.GetStringExtra("Patient_ID");
            new_patient.Patient_species = Intent.GetStringExtra("Patient_SPECIES");
            new_patient.Record_list = new List<Record>();

            height_text = (EditText)FindViewById(Resource.Id.edit_height);
            weight_text = (EditText)FindViewById(Resource.Id.edit_weight);
            notes_text = (EditText)FindViewById(Resource.Id.edit_additional_notes);

            //Save button event handler,
            save_btn = FindViewById<Button>(Resource.Id.save_button2);
            save_btn.Click += delegate
            {
                //Get all of the user input data for the new record
                new_record.Record_id = 1;
                new_record.Patient_height = height_text.Text;
                new_record.Patient_weight = weight_text.Text;
                new_record.Patient_note = notes_text.Text;
                //Add the record data to the new patient
                new_patient.Record_list.Add(new_record);

                //Pass the new patient along to be pushed to the database
                //parameters.Add("NEW_PATIENT", new_patient);
                //client.UploadValuesCompleted += client_UploadValuesCompleted;
                //client.UploadValuesAsync(uri, parameters);

                //Pass the new patient information to the Patient Profile Screen
                var existing_info_screen = new Intent(this, typeof(PatientProfileScreen));
                Bundle extras = new Bundle();
                extras.PutString("Patient_ID", new_patient.Patient_id);
                extras.PutString("Patient_SPECIES", new_patient.Patient_species);
                extras.PutString("Patient_HEIGHT", new_record.Patient_height);
                extras.PutString("Patient_WEIGHT", new_record.Patient_weight);
                extras.PutString("Patient_NOTES", new_record.Patient_note);
                extras.PutString("NEW_PATIENT", "true");
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

        //Check to see if values were uploaded succesfully (Work in Progress)
        void client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            //string id = Encoding.UTF8.GetString(e.Result);
            //int newID = 0;

            //int.TryParse(id, out newID);
            return;
        }
    }
}