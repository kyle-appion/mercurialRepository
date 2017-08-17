
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Appion.Commons.Util;

using ION.Core.Database;

using ION.Droid.Activity;
using ION.Droid.Widgets.RecyclerViews;

namespace ION.Droid.Activity.Report {

  [Activity(Label = "@string/reporting", Icon="@drawable/ic_nav_reporting", Theme="@style/AppTheme", LaunchMode=LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
  public class ReportActivity : IONActivity {

		public const string EXTRA_SHOW_SAVED_SPREADSHEETS = "ION.Droid.Activity.Report.extra.SHOW_SAVED_SPREADSHEETS";
		public const string EXTRA_SHOW_SAVED_PDF = "ION.Droid.Activity.Report.extra.SHOW_SAVED_PDF";


		private const string MIME_EXCEL = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
		private const string MIME_PDF = "application/pdf";

    private Fragment activeFragment;

    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.activity_report_new);

      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_reporting, Resource.Color.gray));
      ActionBar.SetDisplayHomeAsUpEnabled(true);
      ActionBar.SetHomeButtonEnabled(true);

      NavigateToFragment(new ReportSessionSelectionFragment());
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

    private void NavigateToFragment(Fragment frag) {
      var ft = FragmentManager.BeginTransaction();

      ft.Replace(Resource.Id.content, frag);
      ft.SetCustomAnimations(Resource.Animation.card_flip_left_in, Resource.Animation.card_flip_left_out);
      activeFragment = frag;

      ft.Commit();
    }
  }
}
