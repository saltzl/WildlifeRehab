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
using Android.Graphics;
using System.IO;

namespace WildEMR
{
    public class Patient
    {
        public string Identifier { get; set; }
        public int species_id { get; set; }
        public Species Species { get; set; }
        public List<Record> Records { get; set; }
        public byte[] Image { get; set;  }

        public async Task Create()
        {
           await DatabaseConnection.Instance.CreatePatientAsync(this);
        }

        public async Task AddRecord(Record newRecord)
        {
            await DatabaseConnection.Instance.CreateRecordAsync(this,newRecord);
            Records.Add(newRecord);
        }

        public void SetImage(Bitmap img)
        {
            using (var ms = new MemoryStream())
            {
                img.Compress(Bitmap.CompressFormat.Png, 0, ms);
                Image = ms.ToArray();

            }
        }

        public Bitmap GetImage()
        {
            BitmapFactory.Options options = new BitmapFactory.Options();
            
                using (var ms = new MemoryStream(Image))
                {
                    return BitmapFactory.DecodeByteArray(ms.ToArray(), 0, (int)ms.Length, options);
                }
            
            
        }
    }
}