using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WildEMR
{
    class DatabaseConnection
    {
        private static DatabaseConnection instance;


        private Uri DatabaseURI;
        static HttpClient client = new HttpClient();
        private DatabaseConnection()
        {
            DatabaseURI = new Uri("http://ec2-52-15-97-1.us-east-2.compute.amazonaws.com");
            client = new HttpClient();
        }

        public static DatabaseConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseConnection();
                }
                return instance;
            }
        }

        public async Task<IList<Species>> GetSpeciesAsync()
        {
            var requestURI = new Uri(DatabaseURI.ToString() + "species");
            var response = await client.GetAsync(requestURI);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var species  = JsonConvert.DeserializeObject<List<Species>>(content);
                return species;
            }
            return new List<Species>();
        }

        public async Task<IList<Patient>> GetPatientsAsync(int speciesID)
        {
            var requestURI = new Uri(String.Format(DatabaseURI.ToString() + "species/patients/{0}", speciesID));
            var response = await client.GetAsync(requestURI);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var species = JsonConvert.DeserializeObject<List<Patient>>(content);
                return species;
            }
            return new List<Patient>();
        }

        public async Task<Patient> GetPatientAsync(int speciesID, string identifier)
        {
            var requestURI = new Uri(String.Format(DatabaseURI.ToString() + "patient/{0}/{1}", speciesID, identifier));
            var response = await client.GetAsync(requestURI);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var patient = JsonConvert.DeserializeObject<Patient>(content);
                return patient;
            }
            return null;
        }

        public async Task<List<Record>> GetRecordAsync(int speciesID, string identifier)
        {
            var requestURI = new Uri(String.Format(DatabaseURI.ToString() + "records/{0}/{1}", speciesID, identifier));
            var response = await client.GetAsync(requestURI);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var record = JsonConvert.DeserializeObject<List<Record>>(content);
                return record;
            }
            return null;
        }
        public async Task CreatePatientAsync(Patient newPatient)
        {

            var requestURI = new Uri(String.Format(DatabaseURI.ToString() + "patient/{0}", newPatient.species_id));

            var json = JsonConvert.SerializeObject(newPatient);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PostAsync(requestURI, content);
        }

        public async Task CreateRecordAsync(Patient patient, Record newRecord)
        {

            var requestURI = new Uri(String.Format(DatabaseURI.ToString() + "record/{0}/{1}", patient.species_id, patient.Identifier));

            var json = JsonConvert.SerializeObject(newRecord);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PostAsync(requestURI, content);
        }
    }
}