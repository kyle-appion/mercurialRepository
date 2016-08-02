namespace ION.Droid.Activity.Report {
	using System;
	using System.Collections.Generic;

	using Android.App;
	using Android.Content;
	using Android.Content.PM;
	using Android.Graphics;
	using Android.Graphics.Drawables;
	using Android.OS;
	using Android.Runtime;
	using Android.Views;
	using Android.Widget;

	using ION.Core.Database;

	using ION.Droid.Activity;

	/// <summary>
	/// The activity that will walk a user through viewing and selecting reports for export.
	/// </summary>
	[Activity(Label="@string/reports", Theme="@style/AppTheme", LaunchMode=LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
	public class ReportActivity : IONActivity {
		/// <summary>
		/// The fragment that is responsible for displaying and selecting new and saved reports.
		/// </summary>
//		private NewSavedReportFragment newSaved;
		/// <summary>
		/// The fragment that is responsible for exporting reports.
		/// </summary>
//		private ExportReportFragment export;
		/// <summary>
		/// The current active fragment.
		/// </summary>
		private Fragment activeFragment;

		/// <summary>
		/// The button that will show the new report fragments.
		/// </summary>
		private Button newReportButton;
		/// <summary>
		/// The button that will show the saved report fragments.
		/// </summary>
		private Button savedReportButton;

		/// <summary>
		/// The button that will show the jobs fragment.
		/// </summary>
		private Button tab1Button;
		/// <summary>
		/// The button that will show the sessions fragment.
		/// </summary>
		/// <returns>The create.</returns>
		/// <param name="state">State.</param>
		private Button tab2Button;
		/// <summary>
		/// The button that will graph the selected sessions.
		/// </summary>
		private Button graphButtonButton;

		private List<int> checkedSessions = new List<int>();

		protected override void OnCreate(Bundle state) {
			base.OnCreate(state);

			SetContentView(Resource.Layout.activity_report);

			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetHomeButtonEnabled(true);

			newReportButton = FindViewById<Button>(Resource.Id.report_new);
			savedReportButton = FindViewById<Button>(Resource.Id.report_saved);

			tab1Button = FindViewById<Button>(Resource.Id.tab_1);
			tab2Button = FindViewById<Button>(Resource.Id.tab_2);

			graphButtonButton = FindViewById<Button>(Resource.Id.report_graph);

			tab1Button.Click += (sender, e) => {
				ShowByJobFragment();
			};

			tab2Button.Click += (sender, e) => {
				ShowBySessionFragment();
			};

			graphButtonButton.Click += (sender, e) => {
				var i = new Intent(this, typeof(GraphReportSessionsActivity));
				i.PutExtra(GraphReportSessionsActivity.EXTRA_SESSIONS, checkedSessions.ToArray());
				StartActivity(i);
			};

			ShowByJobFragment();
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

		private void ShowByJobFragment() {
			var frag = new ByJobFragment();
			frag.sessions = checkedSessions;
			frag.onSessionChecked += OnSessionChecked;
			GotoFragment(frag, Resource.Animation.enter_left, Resource.Animation.exit_right);
		}

		private void ShowBySessionFragment() {
			var frag = new BySessionFragment();
			frag.sessions = checkedSessions;
			frag.onSessionChecked += OnSessionChecked;
			GotoFragment(frag, Resource.Animation.enter_right, Resource.Animation.exit_left);
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
//			GotoFragment(new ExportReportFragment());
		}

		/// <summary>
		/// Navigates to the given fragment.
		/// </summary>
		/// <returns>The fragment.</returns>
		/// <param name="fragment">Fragment.</param>
		private void GotoFragment(Fragment fragment, int enter, int exit) {
			if (activeFragment == fragment) {
				return;
			}

			var ft = FragmentManager.BeginTransaction();

			if (activeFragment != null) {
//				ft.SetCustomAnimations(enter, exit);
				ft.Remove(activeFragment);
				activeFragment = null;
			}

			ft.Add(Resource.Id.content, fragment, null);
			ft.Commit();
			activeFragment = fragment;
		}

		private void OnSessionChecked(SessionRow session, bool isChecked) {
			if (isChecked) {
				checkedSessions.Add(session._id);
			} else {
				checkedSessions.Remove(session._id);
			}

			if (checkedSessions.Count == 0) {
				graphButtonButton.Visibility = ViewStates.Invisible;
			} else {
				graphButtonButton.Visibility = ViewStates.Visible;
			}
		}
	}
}

