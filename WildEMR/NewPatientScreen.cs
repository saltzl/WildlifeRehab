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
    public class NewPatientScreen : Activity
    {

        Button next_btn;
        Button cancel_btn;
        Spinner species_spinner;
        EditText id_text;
        string[] species_array = { " ", "Raccoon", "Fox", "Cat" };
        Patient new_patient = new Patient();

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewPatientScreen);

            //Get user input for patient ID and species
            ArrayAdapter adapter = new ArrayAdapter(this, global::Android.Resource.Layout.SimpleSpinnerItem, species_array);
            species_spinner = FindViewById<Spinner>(Resource.Id.species_spinner);
            species_spinner.Adapter = adapter;
            id_text = (EditText)FindViewById(Resource.Id.edit_id);

            //Next button handler
            next_btn = FindViewById<Button>(Resource.Id.next_button1);
            next_btn.Click += delegate
            {
                //Set user input to patient object
                new_patient.Patient_id = id_text.Text;
                new_patient.Patient_species = species_spinner.SelectedItem.ToString();

                //Pass information along to the next activity
                var new_patient_screen2 = new Intent(this, typeof(NewPatientScreen2));
                Bundle extras = new Bundle();
                extras.PutString("Patient_ID", new_patient.Patient_id);
                extras.PutString("Patient_SPECIES", new_patient.Patient_species);
                new_patient_screen2.PutExtras(extras);
                StartActivity(new_patient_screen2);
            };

            //Cancel button handler, brings user back to Main screen
            cancel_btn = FindViewById<Button>(Resource.Id.cancel_button1);
            cancel_btn.Click += delegate
            {
                StartActivity(typeof(MainActivity));
            };
        }
    }
}