using Android.App;
using Android.Widget;
using Android.OS;
using System.Linq;
using Android.Content;
using Android.Views;

namespace WildEMR
{
    [Activity(Label = "SpeciesScreen")]
    public class SpeciesScreen : Activity
    {

        ListView speciesListView;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SpeciesScreen);

            //Get user input for patient ID and species
            var species_list = await DatabaseConnection.Instance.GetSpeciesAsync();
            var species_array = species_list.Select(species => species.Name).ToList();
            ArrayAdapter adapter = new ArrayAdapter(this, global::Android.Resource.Layout.SimpleListItem1, species_array);
            speciesListView = FindViewById<ListView>(Resource.Id.speciesListView);
            speciesListView.Adapter = adapter;
            speciesListView.ItemClick += (sender, args) =>
            {
                var lview = sender as ListView;
                var clicked = species_array[args.Position];
                //Pass the new patient information to the Patient Profile Screen
                var existing_info_screen = new Intent(this, typeof(PatientsScreen));
                Bundle extras = new Bundle();
                int speciesID = species_list.Where(species => species.Name == clicked).SingleOrDefault().ID;
                extras.PutInt("species", speciesID);
                existing_info_screen.PutExtras(extras);
                StartActivity(existing_info_screen);
            };

        }
    }
}

