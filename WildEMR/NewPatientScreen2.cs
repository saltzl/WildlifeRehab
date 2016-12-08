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
        EditText subjectiveText;
        EditText objectiveText;
        EditText assessmentText;
        EditText planText;
        string photo_string;
        Patient new_patient = new Patient();
        Record new_record = new Record();

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewPatientScreen2);

            //Get patient id and species from previous activity
            new_patient.Identifier = Intent.GetStringExtra("Patient_ID");
            new_patient.Sex = Intent.GetStringExtra("Patient_Sex");
            new_patient.Color = Intent.GetStringExtra("Patient_Color");
            new_patient.LocationFound = Intent.GetStringExtra("Patient_Loc");
            new_patient.Age = Intent.GetIntExtra("Patient_Age", 0);

            new_patient.species_id = Intent.GetIntExtra("Patient_SPECIES",0);
            photo_string = Intent.GetStringExtra("Patient_Photo");

            var isnew = Intent.GetBooleanExtra("newPatient", true);
            if (isnew)
            {
                new_patient.Records = new List<Record>();
            }
            subjectiveText = (EditText)FindViewById(Resource.Id.edit_subjective);
            objectiveText = (EditText)FindViewById(Resource.Id.edit_objective);
            assessmentText = (EditText)FindViewById(Resource.Id.edit_assessment);
            planText = (EditText)FindViewById(Resource.Id.edit_plan);



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
                new_record.Subjective = subjectiveText.Text;
                new_record.Objective  = objectiveText.Text;
                new_record.Assesment = assessmentText.Text;
                new_record.Plan = planText.Text;
                //Add the record data to the new patient
                await new_patient.AddRecord(new_record);
                
                //Pass the new patient information to the Patient Profile Screen
                var existing_info_screen = new Intent(this, typeof(PatientProfileScreen));
                Bundle extras = new Bundle();
                extras.PutString("Patient_ID", new_patient.Identifier);
                extras.PutInt("Patient_SPECIES", new_patient.species_id);
                extras.PutString("Patient_Photo", photo_string);
                extras.PutString("Patient_Subjective", new_record.Subjective);
                extras.PutString("Patient_Objective", new_record.Objective);
                extras.PutString("Patient_Assesment", new_record.Assesment);
                extras.PutString("Patient_Plan", new_record.Plan);
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