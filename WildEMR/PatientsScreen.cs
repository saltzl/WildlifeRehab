using Android.App;
using Android.Widget;
using Android.OS;
using System.Linq;
using Android.Content;

namespace WildEMR
{
    [Activity(Label = "PatientsScreen")]
    public class PatientsScreen : Activity
    {

        ListView patientListView;

        protected override async void OnCreate(Bundle bundle)
        {
            SetContentView(Resource.Layout.SpeciesScreen);

            base.OnCreate(bundle);

            //Get user input for patient ID and species

            var chosenSpecies = Intent.GetIntExtra("species", 0);

            var patientsList = await DatabaseConnection.Instance.GetPatientsAsync(chosenSpecies);
            var patientsArray = patientsList.Select(patient => patient.Identifier).ToList();
            ArrayAdapter adapter = new ArrayAdapter(this, global::Android.Resource.Layout.SimpleListItem1, patientsArray);
            patientListView = FindViewById<ListView>(Resource.Id.speciesListView);
            patientListView.Adapter = adapter;
            patientListView.ItemClick += (sender, args) =>
            {
                var lview = sender as ListView;
                var clicked = patientsList[args.Position];
                //Pass the new patient information to the Patient Profile Screen
                var existing_info_screen = new Intent(this, typeof(PatientProfileScreen));
                Bundle extras = new Bundle();
                extras.PutBoolean("NEW_PATIENT", false);
                extras.PutString("Patient_ID", clicked.Identifier);
                extras.PutInt("Patient_SPECIES", clicked.Species);

                existing_info_screen.PutExtras(extras);
                StartActivity(existing_info_screen);
            };
        }
    }
}

