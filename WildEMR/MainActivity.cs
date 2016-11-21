using Android.App;
using Android.Widget;
using Android.OS;

namespace WildEMR
{
    [Activity(Label = "WildEMR", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button new_patient_btn;
        Button existing_patient_btn;
        protected override void OnCreate(Bundle bundle)
        {
            SetContentView(Resource.Layout.Main);

            base.OnCreate(bundle);
            new_patient_btn = FindViewById<Button>(Resource.Id.new_patient_btn);
            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            new_patient_btn.Click += delegate
            {
                StartActivity(typeof(NewPatientScreen));
            };


            existing_patient_btn = FindViewById<Button>(Resource.Id.existing_patient_btn);
            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            existing_patient_btn.Click += delegate
            {
                StartActivity(typeof(SpeciesScreen));
            };
        }
    }
}

