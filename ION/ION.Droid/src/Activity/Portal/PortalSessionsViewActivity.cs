using ION.Droid.Dialog;
namespace ION.Droid.Activity.Portal {

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
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using ION.Core.Content;
	using ION.Core.Database;

	using ION.Droid.Widgets.Adapters.Job;

	[Activity(Label = "@string/portal_upload_sessions", Theme = "@style/TerminalActivityTheme", ScreenOrientation=ScreenOrientation.Portrait)]      
	public class PortalSessionsViewActivity : IONActivity {

		private RecyclerView list;
		private Button upload;

		private SessionAdapter adapter;

		private Handler handler;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_portal_sessions_view);

			ActionBar.SetDisplayHomeAsUpEnabled(true);

			list = FindViewById<RecyclerView>(Resource.Id.list);
			upload = FindViewById<Button>(Resource.Id.button);

			list.SetAdapter(adapter = new SessionAdapter(ion));
			upload.Click += (sender, args) => {
				UploadSessions();
			};

			handler = new Handler();
		}

		protected override void OnResume() {
			base.OnResume();
			LoadSessionsAsync();
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

		private async Task LoadSessionsAsync() {
			var pd = new ProgressDialog(this);
			pd.SetTitle(Resource.String.report_sessions_loading);
			pd.SetMessage(GetString(Resource.String.please_wait));
			pd.SetCancelable(false);
			pd.Show();

			var table = await ion.database.QueryForAllAsync<SessionRow>();
			adapter.SetSessions(table);

			pd.Dismiss();
		}

		private async Task UploadSessions() {
			var pd = new ProgressDialog(this);
			pd.SetTitle(Resource.String.portal_uploading_sessions);
			pd.SetMessage(GetString(Resource.String.please_wait));
			pd.SetCancelable(false);
			pd.Show();

			var sessions = new List<SessionRow>();
			foreach (var sr in adapter.GetCheckedSessions()) {
				sessions.Add(sr.row);
			}
			var result = await ion.portal.UploadSessionsAsync(ion, sessions);

			pd.Dismiss();

			if (result.success) {
				Toast.MakeText(this, Resource.String.portal_upload_sessions_success, ToastLength.Long).Show();
			} else {
				var adb = new IONAlertDialog(this);
				adb.SetTitle(Resource.String.portal_error);
				adb.SetMessage(Resource.String.portal_error_upload_sessions_failed);
				adb.SetNegativeButton(Resource.String.cancel, (sender, args) => {
				});
				adb.Show();
			}
		}
	}
}
