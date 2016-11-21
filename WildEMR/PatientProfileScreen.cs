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
        Patient current_patient = new Patient();
        Record current_record = new Record();
        ImageView imageView;
        string photo_string;
        
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PatientProfileScreen);

            id_text = (TextView)FindViewById(Resource.Id.patient_id_text);
            species_text = (TextView)FindViewById(Resource.Id.patient_species_text);
            record_weight = (TextView)FindViewById(Resource.Id.patient_weight);
            record_height = (TextView)FindViewById(Resource.Id.patient_height);
            imageView = (ImageView)FindViewById(Resource.Id.patient_image_view);

            //Check if a new patient is being passed in from a previous activity
            var is_new_patient = Intent.GetBooleanExtra("NEW_PATIENT", false);
            var isNewRecord = Intent.GetBooleanExtra("newRecord", true);

            if (is_new_patient)
            {
                //Get all of the patient information from the previous activity
                current_patient.Identifier = Intent.GetStringExtra("Patient_ID");
                current_patient.Species = Intent.GetIntExtra("Patient_SPECIES",0);
                photo_string = Intent.GetStringExtra("Patient_Photo");

                BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = false };
                var bitmap = BitmapFactory.DecodeFile(photo_string, options);

                current_patient.SetImage(bitmap);
                current_record.Height = Intent.GetStringExtra("Patient_HEIGHT");
                current_record.Weight = Intent.GetStringExtra("Patient_WEIGHT");
                current_record.Note = Intent.GetStringExtra("Patient_NOTE");

            } else {
                var ident = Intent.GetStringExtra("Patient_ID");
                var species = Intent.GetIntExtra("Patient_SPECIES", 0);

                current_patient = await DatabaseConnection.Instance.GetPatientAsync(species, ident);
                if (isNewRecord)
                {
                    current_record = current_patient.Records.Last();
                }
                else
                {
                    current_record.Height = Intent.GetStringExtra("Patient_HEIGHT");
                    current_record.Weight = Intent.GetStringExtra("Patient_WEIGHT");
                    current_record.Note = Intent.GetStringExtra("Patient_NOTE");
                }
            }
            species_text.Text = current_patient.Species.ToString();
            id_text.Text = current_patient.Identifier;
            record_weight.Text = current_record.Weight;
            record_height.Text = current_record.Height;
            imageView.SetImageBitmap(current_patient.GetImage());

            var newButton = (Button)FindViewById(Resource.Id.new_button);

            newButton.Click += delegate
            {
                //Pass information along to the next activity
                var newRecordScreen = new Intent(this, typeof(NewPatientScreen2));
                Bundle extras = new Bundle();
                extras.PutString("Patient_ID", current_patient.Identifier);
                extras.PutInt("Patient_SPECIES", current_patient.Species);
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
                extras.PutInt("Patient_SPECIES", current_patient.Species);
                newRecordScreen.PutExtras(extras);
                StartActivity(newRecordScreen);
            };

        }


    }
}