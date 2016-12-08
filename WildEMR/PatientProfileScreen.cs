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
using Android.Provider;
using Android.Content.PM;
using Android.Graphics;

namespace WildEMR
{
    [Activity(Label = "PatientProfileScreen")]
    public class PatientProfileScreen : Activity
    {
        TextView id_text;
        TextView species_text;
        TextView record_weight;
        TextView record_height;
        TextView record_assessment;
        TextView record_plan;
        TextView patient_color;
        TextView patient_loc;
        TextView patient_sex;
        TextView patient_age;


        Patient current_patient = new Patient();
        Record current_record = new Record();
        ImageView imageView;
        string photo_string;
        
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PatientProfileScreen);

            FragmentManager.ExecutePendingTransactions();
            id_text = (TextView)FindViewById(Resource.Id.patient_id_text);
            species_text = (TextView)FindViewById(Resource.Id.patient_species_text);
            record_weight = (TextView)FindViewById(Resource.Id.patient_weight);
            record_height = (TextView)FindViewById(Resource.Id.patient_height);
            patient_age = (TextView)FindViewById(Resource.Id.patient_age_text);
            patient_sex = (TextView)FindViewById(Resource.Id.patient_sex_text);
            patient_color = (TextView)FindViewById(Resource.Id.patient_color_text);
            patient_loc = (TextView)FindViewById(Resource.Id.patient_loc_text);


            imageView = (ImageView)FindViewById(Resource.Id.patient_image_view);

            //Check if a new patient is being passed in from a previous activity
            var is_new_patient = Intent.GetBooleanExtra("NEW_PATIENT", false);
            var isNewRecord = Intent.GetBooleanExtra("newRecord", true);

            var ident = Intent.GetStringExtra("Patient_ID");
            var species = Intent.GetIntExtra("Patient_SPECIES", 0);
            var other = Intent.GetIntExtra("species", 0);
            var recordNum = Intent.GetIntExtra("Record_num", 0);
            current_patient = await DatabaseConnection.Instance.GetPatientAsync(species, ident);
            if (isNewRecord)
            {
                current_record = (await DatabaseConnection.Instance.GetRecordAsync(species, ident)).Last();
            }
            else
            {
                current_record = (await DatabaseConnection.Instance.GetRecordAsync(species, ident))[recordNum];
            }
            species_text.Text = current_patient.Species.Name;
            id_text.Text = current_patient.Identifier;
            patient_age.Text = current_patient.Age.ToString();
            patient_sex.Text = current_patient.Sex;
            patient_color.Text = current_patient.Color;
            patient_loc.Text = current_patient.LocationFound;


            record_weight.Text = current_record.Objective;
            record_height.Text = current_record.Subjective;

            var newButton = (Button)FindViewById(Resource.Id.new_button);

            newButton.Click += delegate
            {
                //Pass information along to the next activity
                var newRecordScreen = new Intent(this, typeof(NewPatientScreen2));
                Bundle extras = new Bundle();
                extras.PutString("Patient_ID", current_patient.Identifier);
                extras.PutInt("Patient_SPECIES", current_patient.Species.ID);
                extras.PutBoolean("newPatient", false);
                newRecordScreen.PutExtras(extras);
                StartActivity(newRecordScreen);
            };


            var oldButton = (Button)FindViewById(Resource.Id.old_button);

            oldButton.Click += delegate
            {
                //Pass information along to the next activity
                var newRecordScreen = new Intent(this, typeof(RecordSelector));
                Bundle extras = new Bundle();
                extras.PutString("Patient_ID", current_patient.Identifier);
                extras.PutInt("Patient_SPECIES", current_patient.species_id);
                newRecordScreen.PutExtras(extras);
                StartActivity(newRecordScreen);
            };

        }


    }
}