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

namespace WildEMR
{
    [Activity(Label = "PatientProfileScreen")]
    public class PatientProfileScreen : Activity
    {
        TextView id_text;
        TextView species_text;
        TextView record_weight;
        TextView record_height;
        Patient current_patient = new Patient();
        Record current_record = new Record();
        
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PatientProfileScreen);

            id_text = (TextView)FindViewById(Resource.Id.patient_id_text);
            species_text = (TextView)FindViewById(Resource.Id.patient_species_text);
            record_weight = (TextView)FindViewById(Resource.Id.patient_weight);
            record_height = (TextView)FindViewById(Resource.Id.patient_height);

            //Check if a new patient is being passed in from a previous activity
            var is_new_patient = Intent.GetBooleanExtra("NEW_PATIENT", false);
            if (is_new_patient)
            {
                //Get all of the patient information from the previous activity
                current_patient.Identifier = Intent.GetStringExtra("Patient_ID");
                current_patient.Species = Intent.GetIntExtra("Patient_SPECIES",0);
                current_record.Height = Intent.GetStringExtra("Patient_HEIGHT");
                current_record.Weight = Intent.GetStringExtra("Patient_WEIGHT");
                current_record.Note = Intent.GetStringExtra("Patient_NOTE");

                //Update the text fields
                species_text.Text = current_patient.Species.ToString();
                id_text.Text = current_patient.Identifier;
                record_weight.Text = current_record.Weight;
                record_height.Text = current_record.Height;
            } else {
                var ident = Intent.GetStringExtra("Patient_ID");
                var species = Intent.GetIntExtra("Patient_SPECIES", 0);

                current_patient = await DatabaseConnection.Instance.GetPatientAsync(species, ident);
            }
        }


    }
}