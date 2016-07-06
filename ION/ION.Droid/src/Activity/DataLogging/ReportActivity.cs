namespace ION.Droid {

	using System;

	using Android.OS;

	using ION.Droid.Activity;

	public class ReportActivity : IONActivity {

		protected override void OnCreate(Bundle state) {
			base.OnCreate(state);

			SetContentView(Resource.Layout.activity_report);
		}
	}
}

