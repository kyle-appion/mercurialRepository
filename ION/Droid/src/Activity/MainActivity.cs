using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using ION.Core.Devices;
using ION.Core.Util;

using ION.Droid.Devices;

namespace ION.Droid.Activities
{
	[Activity (Label = "ION.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity {

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

      StartActivity(new Intent(this, typeof(DeviceManagerActivity)));
      Finish();
		}
	}
}


