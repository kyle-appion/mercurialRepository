using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace Xfinium.Pdf.SamplesExplorer.Xamarin.Android
{
	[Activity (Label = "Xfinium.Pdf Samples Explorer", MainLauncher = true)]
	public class MainActivity : ListActivity
	{
        SampleInfo[] samples;

		protected override void OnCreate (Bundle bundle)
		{
            base.OnCreate(bundle);

			Stream input = Assets.Open ("samples.xml");
			XDocument samplesDoc = XDocument.Load(input);
			IEnumerable<SampleInfo> q = from file in samplesDoc.Descendants("sample")
								        select new SampleInfo()
								        {
								            Name = file.Element("name").Value,
								            Description = file.Element("description").Value,
								            CSharpSourceCodeFile = file.Element("csharpsourcecode").Value,
								            VbNetSourceCodeFile = file.Element("vbnetsourcecode").Value,
								            ID = file.Element("id").Value,
								        };
			samples = q.ToArray();
			input.Close();

            ListAdapter = new ArrayAdapter<SampleInfo>(this, global::Android.Resource.Layout.SimpleListItem1, samples);
		}

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            //Android.Widget.Toast.MakeText(this, t.Name, Android.Widget.ToastLength.Short).Show();
            Console.WriteLine("Clicked on " + samples[position]);

			Intent sampleDetailsActivity = new Intent(this, typeof(SampleDetailsActivity));
			sampleDetailsActivity.PutExtra("SampleName", samples[position].Name);
 			sampleDetailsActivity.PutExtra("SampleID", samples[position].ID);
			sampleDetailsActivity.PutExtra("SampleDescription", samples[position].Description);
			sampleDetailsActivity.PutExtra("SampleCSharpSourceCodeFile", samples[position].CSharpSourceCodeFile);
			sampleDetailsActivity.PutExtra("SampleVbNetSourceCodeFile", samples[position].VbNetSourceCodeFile);
            StartActivity(sampleDetailsActivity);
        }
	}
}


