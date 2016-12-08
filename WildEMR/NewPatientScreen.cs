using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Widget;
using Java.IO;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

namespace WildEMR
{
    [Activity(Label = "New Patient Screen")]
    public class NewPatientScreen : Activity
    {

        public static class App
        {
            public static File _file;
            public static File _dir;
            public static Bitmap bitmap;
        }

        Button next_btn;
        Button cancel_btn;
        Spinner species_spinner;
        EditText id_text;
        EditText color_text;
        EditText sex_text;
        EditText age_text;
        EditText location_text;

        Patient new_patient = new Patient();

        protected override async void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewPatientScreen);

            //Get user input for patient ID and species
            var species_list = await DatabaseConnection.Instance.GetSpeciesAsync();
            var species_array = species_list.Select(species => species.Name).ToList();
            ArrayAdapter adapter = new ArrayAdapter(this, global::Android.Resource.Layout.SimpleSpinnerItem, species_array);
            species_spinner = FindViewById<Spinner>(Resource.Id.species_spinner);
            species_spinner.Adapter = adapter;
            id_text = (EditText)FindViewById(Resource.Id.edit_id);
            sex_text = (EditText)FindViewById(Resource.Id.edit_sex);
            age_text = (EditText)FindViewById(Resource.Id.edit_age);
            color_text = (EditText)FindViewById(Resource.Id.edit_color);
            location_text   = (EditText)FindViewById(Resource.Id.edit_location);

            //Next button handler
            next_btn = FindViewById<Button>(Resource.Id.next_button1);
            next_btn.Click += delegate
            {
                //Set user input to patient object
                new_patient.Identifier = id_text.Text;
                new_patient.Sex = sex_text.Text;
                int age;
                if (int.TryParse(age_text.Text, out age))
                    new_patient.Age = age; 
                new_patient.Color = color_text.Text;
                new_patient.LocationFound = location_text.Text;

                new_patient.species_id = species_list.Where(species => species.Name == species_spinner.SelectedItem.ToString()).SingleOrDefault().ID;


                //Pass information along to the next activity
                var new_patient_screen2 = new Intent(this, typeof(NewPatientScreen2));
                Bundle extras = new Bundle();
                extras.PutString("Patient_ID", new_patient.Identifier);
                extras.PutString("Patient_Sex", new_patient.Sex);
                extras.PutInt("Patient_Age", new_patient.Age);
                extras.PutString("Patient_Color", new_patient.Color);
                extras.PutString("Patient_Loc", new_patient.LocationFound);

                extras.PutInt("Patient_SPECIES", new_patient.species_id);
                if (App._file == null)
                {
                    extras.PutString("Patient_Photo", "");
                }
                else
                {
                    extras.PutString("Patient_Photo", App._file.Path);
                }
                new_patient_screen2.PutExtras(extras);
                StartActivity(new_patient_screen2);
            };

            //Cancel button handler, brings user back to Main screen
            cancel_btn = FindViewById<Button>(Resource.Id.cancel_button1);
            cancel_btn.Click += delegate
            {
                StartActivity(typeof(MainActivity));
            };


            if (PictureAppExists())
            {
                CreatePictureDirectory();
                ImageButton button = FindViewById<ImageButton>(Resource.Id.add_photo_button);
                button.Click += TakePicture;

            }
        }

        private bool PictureAppExists()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void CreatePictureDirectory()
        {
            App._dir = new File(
                Environment.GetExternalStoragePublicDirectory(
                    Environment.DirectoryPictures), "CameraAppDemo");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }

        private void TakePicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App._file = new File(App._dir, String.Format("temp_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            ImageButton button = FindViewById<ImageButton>(Resource.Id.add_photo_button);

            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume to much memory
            // and cause the application to crash.

            int height = Resources.DisplayMetrics.HeightPixels;
            int width = button.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            if (App.bitmap != null)
            {
                button.SetImageBitmap(App.bitmap);
                App.bitmap = null;
            }

            // Dispose of the Java side bitmap.
            GC.Collect();
        }
    }

    public static class BitmapHelpers
    {
        public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
        {
            // First we get the the dimensions of the file on disk
            BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
            BitmapFactory.DecodeFile(fileName, options);

            // Next we calculate the ratio that we need to resize the image by
            // in order to fit the requested dimensions.
            int outHeight = options.OutHeight;
            int outWidth = options.OutWidth;
            int inSampleSize = 1;

            if (outHeight > height || outWidth > width)
            {
                inSampleSize = outWidth > outHeight
                                   ? outHeight / height
                                   : outWidth / width;
            }

            // Now we will load the image and have BitmapFactory resize it for us.
            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;
            Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

            return resizedBitmap;
        }
    }
}