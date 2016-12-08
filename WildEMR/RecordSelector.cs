using Android.App;
using Android.Widget;
using Android.OS;
using System.Linq;
using Android.Content;

namespace WildEMR
{
    [Activity(Label = "RecordSelector")]
    public class RecordSelector : Activity
    {

        ListView recordListView;

        protected override async void OnCreate(Bundle bundle)
        {
            SetContentView(Resource.Layout.SpeciesScreen);

            base.OnCreate(bundle);

            //Get user input for patient ID and species
            var identifier = Intent.GetStringExtra("Patient_ID");
            var species = Intent.GetIntExtra("Patient_SPECIES", 0);

            var records = await DatabaseConnection.Instance.GetRecordAsync(species,identifier);
            var recordsArray = records.Select(record => record.ID).ToList();
            ArrayAdapter adapter = new ArrayAdapter(this, global::Android.Resource.Layout.SimpleListItem1, recordsArray);
            recordListView = FindViewById<ListView>(Resource.Id.recordsListView);
            recordListView.Adapter = adapter;
            recordListView.ItemClick += (sender, args) =>
            {
                var lview = sender as ListView;
                var clicked = records[args.Position];
                //Pass the new patient information to the Patient Profile Screen
                var existing_info_screen = new Intent(this, typeof(PatientProfileScreen));
                Bundle extras = new Bundle();
                extras.PutString("Patient_ID", identifier);
                extras.PutInt("Patient_SPECIES", species);
                extras.PutString("Patient_Subjective", clicked.Subjective);
                extras.PutString("Patient_Objective", clicked.Objective);
                extras.PutString("Patient_Assesment", clicked.Assesment);
                extras.PutBoolean("NEW_PATIENT", false);
                extras.PutBoolean("newRecord", false);
                existing_info_screen.PutExtras(extras);
                StartActivity(existing_info_screen);

            };
        }
    }
}

