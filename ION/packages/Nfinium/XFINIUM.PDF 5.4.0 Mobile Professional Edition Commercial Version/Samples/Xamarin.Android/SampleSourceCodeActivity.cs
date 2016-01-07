
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
using Android.Webkit;

namespace Xfinium.Pdf.SamplesExplorer.Xamarin.Android
{
	[Activity (Label = "Xfinium.Pdf Samples Explorer")]			
	public class SampleSourceCodeActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.SampleSourceCode);

			string sampleName = Intent.GetStringExtra ("SampleName");
			if (sampleName != null) 
			{
				TextView tvSampleName = FindViewById<TextView>(Resource.Id.tvSampleName);
				tvSampleName.Text = sampleName + " - C# Source code";

				string sourceCode = Intent.GetStringExtra("SampleCSharpSourceCodeFile");
				WebView wvSourceCode = FindViewById<WebView>(Resource.Id.wvSourceCode);
				wvSourceCode.LoadUrl("file:///android_asset/"  + sourceCode); 
			}
		}
	}
}

