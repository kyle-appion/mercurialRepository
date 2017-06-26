namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using Android.App;
	using Android.Content;
	using Android.Content.PM;
	using Android.OS;
	using Android.Runtime;
	using Android.Views;
	using Android.Widget;

  [Activity(Label = "@string/report_build", Icon="@drawable/ic_nav_reporting", Theme="@style/AppTheme", ScreenOrientation=ScreenOrientation.Portrait)]
  public class BuildReportActivity : IONActivity {

    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.activity_build_report);

			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetHomeButtonEnabled(true);
			ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_reporting, Resource.Color.gray));
    }

		public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
			switch (item.ItemId) {
				case Android.Resource.Id.Home:
					SetResult(Result.Canceled);
					Finish();
					return true;
				default:
					return base.OnMenuItemSelected(featureId, item);
			}
		}
  }
}
