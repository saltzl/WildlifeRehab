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
using System.Threading.Tasks;

namespace WildEMR
{
    public class Patient
    {
        public string Identifier { get; set; }
        public int Species { get; set; }
        public List<Record> Records { get; set; }
        

        public async Task Create()
        {
           await DatabaseConnection.Instance.CreatePatientAsync(this);
        }

        public async Task AddRecord(Record newRecord)
        {
            await DatabaseConnection.Instance.CreateRecordAsync(this,newRecord);
            Records.Add(newRecord);
        }
    }
}