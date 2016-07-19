namespace ION.Droid.Activity.Report {
	using System;

	using Android.App;
	using Android.Content;
	using Android.Content.PM;
	using Android.Graphics;
	using Android.Graphics.Drawables;
	using Android.OS;
	using Android.Runtime;
	using Android.Views;
	using Android.Widget;

	using ION.Droid.Activity;

	/// <summary>
	/// The activity that will walk a user through viewing and selecting reports for export.
	/// </summary>
	[Activity(Label="@string/reports", Theme="@style/AppTheme", LaunchMode=Android.Content.PM.LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
	public class ReportActivity : IONActivity {
		/// <summary>
		/// The fragment that is responsible for displaying and selecting new and saved reports.
		/// </summary>
//		private NewSavedReportFragment newSaved;
		/// <summary>
		/// The fragment that is responsible for exporting reports.
		/// </summary>
		private ExportReportFragment export;
		/// <summary>
		/// The current active fragment.
		/// </summary>
		private Fragment activeFragment;

		protected override void OnCreate(Bundle state) {
			base.OnCreate(state);

			SetContentView(Resource.Layout.activity_report);

			ShowNewSavedFragment();
		}

		/// <summary>
		/// Shows the new/saved report fragment.
		/// </summary>
		/// <returns>The new saved fragment.</returns>
		private void ShowNewSavedFragment() {
//			GotoFragment(new NewSavedReportFragment());
		}

		/// <summary>
		/// Shows the export fragment.
		/// </summary>
		/// <returns>The export fragment.</returns>
		private void ShowExportFragment() {
			GotoFragment(new ExportReportFragment());
		}

		/// <summary>
		/// Navigates to the given fragment.
		/// </summary>
		/// <returns>The fragment.</returns>
		/// <param name="fragment">Fragment.</param>
		private void GotoFragment(Fragment fragment) {
			var ft = FragmentManager.BeginTransaction();

			ft.SetCustomAnimations(Resource.Animation.enter, Resource.Animation.exit);

			if (activeFragment != null) {
				ft.Remove(activeFragment);
				activeFragment = null;
			}

			ft.Add(Resource.Id.content, activeFragment = fragment, null);
			ft.Commit();
		}
	}
}

