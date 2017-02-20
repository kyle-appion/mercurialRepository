namespace ION.Droid.Activity.Report {

	using System.Linq;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Android.App;
	using Android.OS;
	using Android.Support.V7.Widget;
	using Android.Views;

	using ION.Core.Database;

	// Using ION.Droid
	using Fragments;
	using Widgets.Adapters.Job;

	public class BySessionFragment : IONFragment {
		public OnSessionChecked onSessionChecked;

		public List<int> sessions { private get; set; }

		private RecyclerView list;
		private SessionAdapter adapter;
		private View empty;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			var ret = inflater.Inflate(Resource.Layout.fragment_by_session, container, false);

			list = ret.FindViewById<RecyclerView>(Resource.Id.list);
			empty = ret.FindViewById(Resource.Id.empty);

			return ret;
		}

		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated(savedInstanceState);

			adapter = new SessionAdapter(ion);
			adapter.onSessionRowChecked += (sender, e) => {
				onSessionChecked(e.data, e.isChecked);
			};
			adapter.onItemClicked += (position) => {
				var record = (SessionRecord)adapter[position];
				record.isChecked = !record.isChecked;
				adapter.NotifyItemChanged(position);
				NotifySessionChecked(record);
			};
			list.SetAdapter(adapter);
		}

		public override void OnResume() {
			base.OnResume();

			LoadAsync();
		}

		private void NotifySessionChecked(SessionRecord record) {
			if (onSessionChecked != null) {
				onSessionChecked(record.data, record.isChecked);
			}
		}

		private async Task LoadAsync() {
			var progress = new ProgressDialog(Activity);
			progress.SetTitle(Resource.String.please_wait);
			progress.SetMessage(GetString(Resource.String.loading));
			progress.Show();

			var records = new List<SessionRecord>();
			foreach (var s in await ion.database.QueryForAllAsync<SessionRow>()) {
				if (s._id == ion.dataLogManager.currentSessionId) {
					continue;
				}

				var table = ion.database.Table<SensorMeasurementRow>();
				var query = table.Where(smr => smr.frn_SID == s._id)
				                 .GroupBy(smr => smr.serialNumber);

				var count = query.Count();

				var sessionRecord = new SessionRecord(s, count);
				if (sessions != null) {
					sessionRecord.isChecked = sessions.Contains(s._id);
				}
				records.Add(sessionRecord);
			}

			adapter.SetSessions(records);
			if (adapter.ItemCount == 0) {
				list.Visibility = ViewStates.Gone;
				empty.Visibility = ViewStates.Visible;
			} else {
				list.Visibility = ViewStates.Visible;
				empty.Visibility = ViewStates.Gone;				
			}

			progress.Dismiss();
		}
	}
}

