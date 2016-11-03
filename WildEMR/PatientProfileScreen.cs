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
    [Activity(Label = "PatientProfileScreen")]
    public class PatientProfileScreen : Activity
    {
        TextView id_text;
        TextView species_text;
        TextView record_weight;
        TextView record_height;
        Patient current_patient = new Patient();
        Record current_record = new Record();
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PatientProfileScreen);

            id_text = (TextView)FindViewById(Resource.Id.patient_id_text);
            species_text = (TextView)FindViewById(Resource.Id.patient_species_text);
            record_weight = (TextView)FindViewById(Resource.Id.patient_weight);
            record_height = (TextView)FindViewById(Resource.Id.patient_height);

            //Check if a new patient is being passed in from a previous activity
            String is_new_patient = Intent.GetStringExtra("NEW_PATIENT");
            if (is_new_patient == "true")
            {
                //Get all of the patient information from the previous activity
                current_patient.Patient_id = Intent.GetStringExtra("Patient_ID");
                current_patient.Patient_species = Intent.GetStringExtra("Patient_SPECIES");
                current_record.Patient_height = Intent.GetStringExtra("Patient_HEIGHT");
                current_record.Patient_weight = Intent.GetStringExtra("Patient_WEIGHT");
                current_record.Patient_note = Intent.GetStringExtra("Patient_NOTE");

                //Update the text fields
                species_text.Text = current_patient.Patient_species;
                id_text.Text = current_patient.Patient_id;
                record_weight.Text = current_record.Patient_weight;
                record_height.Text = current_record.Patient_height;
            }
        }
    }
}