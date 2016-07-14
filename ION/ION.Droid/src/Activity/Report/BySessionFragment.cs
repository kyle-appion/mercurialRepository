namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Support.V7.Widget;
	using Android.Runtime;
	using Android.Views;
	using Android.Widget;

	using ION.Core.Database;

	// Using ION.Droid
	using Fragments;
	using Widgets.Adapters.Job;

	public class BySessionFragment : IONFragment {

		private RecyclerView list;
		private SessionAdapter adapter;

		private List<SessionRow> sessionsChecked = new List<SessionRow>();

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			var ret = inflater.Inflate(Resource.Layout.fragment_by_session, container, false);

			list = ret.FindViewById<RecyclerView>(Resource.Id.list);

			return ret;
		}

		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated(savedInstanceState);

			adapter = new SessionAdapter(ion);
			list.SetAdapter(adapter);
		}

		public override void OnResume() {
			base.OnResume();

			LoadAsync();
		}

		private async Task LoadAsync() {
			var progress = new ProgressDialog(Activity);
			progress.SetTitle(Resource.String.please_wait);
			progress.SetMessage(GetString(Resource.String.loading));
			progress.Show();

			var records = new List<SessionRecord>();
			foreach (var s in await ion.database.QueryForAllAsync<SessionRow>()) {
				records.Add(new SessionRecord(s));
			}

			adapter.SetSessions(records);

			progress.Dismiss();
		}
	}
}

