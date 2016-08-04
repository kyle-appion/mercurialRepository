﻿using ION.Droid.Dialog;
namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;

	using Android.App;
	using Android.Content;
	using Android.Content.PM;
	using Android.OS;
	using Android.Views;
	using Android.Widget;

	using ION.Core.Database;

	using ION.Droid.Activity;
	using ION.Droid.Fragments;
	using ION.Droid.Views;

	/// <summary>
	/// The activity that will walk a user through viewing and selecting reports for export.
	/// </summary>
	[Activity(Label="@string/reports", Theme="@style/AppTheme", LaunchMode=LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
	public class ReportActivity : IONActivity {


		private const string MIME_EXCEL = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
		private const string MIME_PDF = "application/pdf";

		/// <summary>
		/// The current active fragment.
		/// </summary>
		private Fragment activeFragment;

		/// <summary>
		/// The header that show the 
		/// </summary>
		private TextView header;

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
			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

			header = FindViewById<TextView>(Resource.Id.text);

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

			var newReportTab = ActionBar.NewTab();
			newReportTab.SetText(Resource.String.report_new);
			newReportTab.TabSelected += (sender, e) => {
				PrepareNewReportFragments();
			};

			var savedReportsTab = ActionBar.NewTab();
			savedReportsTab.SetText(Resource.String.report_saved);
			savedReportsTab.TabSelected += (sender, e) => {
				PrepareShowReportsFragments();
			};

			ActionBar.AddTab(newReportTab);
			ActionBar.AddTab(savedReportsTab);

			PrepareNewReportFragments();
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

		private void PrepareNewReportFragments() {
			tab1Button.SetOnClickListener(new ViewClickAction((view) => {
				ShowByJobFragment();
			}));

			tab2Button.SetOnClickListener(new ViewClickAction((view) => {
				ShowBySessionFragment();
			}));

			tab1Button.Text = GetString(Resource.String.report_by_job);
			tab2Button.Text = GetString(Resource.String.report_by_date);
			graphButtonButton.Visibility = ViewStates.Visible;

			ShowByJobFragment();

			header.SetText(Resource.String.report_session_selection);
		}

		private void PrepareShowReportsFragments() {
			tab1Button.SetOnClickListener(new ViewClickAction((view) => {
				ShowSpreadsheetFragment();
			}));

			tab2Button.SetOnClickListener(new ViewClickAction((view) => {
				ShowPDFFragment();
			}));

			tab1Button.Text = GetString(Resource.String.spreadsheet);
			tab2Button.Text = GetString(Resource.String.pdf);
			graphButtonButton.Visibility = ViewStates.Gone;

			ShowSpreadsheetFragment();
			header.SetText(Resource.String.report_selection);
		}

		private void ShowByJobFragment() {
			var frag = new ByJobFragment();
			frag.sessions = checkedSessions;
			frag.onSessionChecked += OnSessionChecked;
			GotoFragment(frag, Resource.Animation.enter_left, Resource.Animation.exit_right);

			tab1Button.SetBackgroundResource(Resource.Drawable.tab_selected_ion_action_bar);
			tab2Button.SetBackgroundResource(Resource.Drawable.tab_unselected_ion_action_bar);
		}

		private void ShowBySessionFragment() {
			var frag = new BySessionFragment();
			frag.sessions = checkedSessions;
			frag.onSessionChecked += OnSessionChecked;
			GotoFragment(frag, Resource.Animation.enter_right, Resource.Animation.exit_left);

			tab1Button.SetBackgroundResource(Resource.Drawable.tab_unselected_ion_action_bar);
			tab2Button.SetBackgroundResource(Resource.Drawable.tab_selected_ion_action_bar);
		}

		/// <summary>
		/// Shows the new/saved report fragment.
		/// </summary>
		/// <returns>The new saved fragment.</returns>
		private void ShowSpreadsheetFragment() {
			var frag = new FileViewerFragment();
			frag.folder = ion.dataLogReportFolder;
			frag.filter = new FileExtensionFilter(true, new string[] { ".xlsx" });
			frag.onFileClicked = (file) => {
				try {
					var i = new Intent(Intent.ActionView);
					i.SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File(file.fullPath)), MIME_EXCEL);
					i.SetFlags(ActivityFlags.NoHistory);
					StartActivity(Intent.CreateChooser(i, GetString(Resource.String.open_with)));
				} catch (Exception e) {
					ION.Core.Util.Log.E(this, "Failed to start Excel activity chooser", e);
					var adb = new IONAlertDialog(this);
					adb.SetTitle(Resource.String.error_failed_to_open_file);
					adb.SetMessage(Resource.String.error_excel_viewer_missing);
					adb.SetNegativeButton(Resource.String.cancel, (sender, ex) => {
						var dialog = sender as Dialog;
						if (dialog != null) {
							dialog.Dismiss();
						}
					});
					adb.Show();
				}
			};
			GotoFragment(frag, Resource.Animation.enter_left, Resource.Animation.exit_right);

			tab1Button.SetBackgroundResource(Resource.Drawable.tab_selected_ion_action_bar);
			tab2Button.SetBackgroundResource(Resource.Drawable.tab_unselected_ion_action_bar);
		}

		/// <summary>
		/// Shows the export fragment.
		/// </summary>
		/// <returns>The export fragment.</returns>
		private void ShowPDFFragment() {
			var frag = new FileViewerFragment();
			frag.folder = ion.dataLogReportFolder;
			frag.filter = new FileExtensionFilter(true, new string[] { ".pdf" });
			frag.onFileClicked = (file) => {
				try {
					var i = new Intent(Intent.ActionView);
					i.SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File(file.fullPath)), MIME_PDF);
					i.SetFlags(ActivityFlags.NoHistory);
					StartActivity(Intent.CreateChooser(i, GetString(Resource.String.open_with)));
				} catch (Exception e) {
					ION.Core.Util.Log.E(this, "Failed to start Excel activity chooser", e);
					var adb = new IONAlertDialog(this);
					adb.SetTitle(Resource.String.error_failed_to_open_file);
					adb.SetMessage(Resource.String.error_pdf_viewer_missing);
					adb.SetNegativeButton(Resource.String.cancel, (sender, ex) => {
						var dialog = sender as Dialog;
						if (dialog != null) {
							dialog.Dismiss();
						}
					});
					adb.Show();
				}
			};
			GotoFragment(frag, Resource.Animation.enter_left, Resource.Animation.exit_left);

			tab1Button.SetBackgroundResource(Resource.Drawable.tab_unselected_ion_action_bar);
			tab2Button.SetBackgroundResource(Resource.Drawable.tab_selected_ion_action_bar);
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

