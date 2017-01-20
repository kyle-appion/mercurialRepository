namespace ION.Droid.Activity.Portal {

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

	[Activity(Label = "activity_portal")]
	public class PortalActivity : Activity {



		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_portal);

			var ft = FragmentManager.BeginTransaction();
			ft.Add(Resource.Id.content, new LoginFragment());
		}
	}
}
