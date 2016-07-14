namespace ION.Droid {

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using ION.Core.Database;

	// Using ION.Droid
	using Fragments;
	using Widgets.RecyclerViews;

	public class ByJobFragment : IONFragment {
		private RecyclerView list;

		private JobAdapter adapter;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			var ret = inflater.Inflate(Resource.Layout.fragment_by_job, container, false);

			list = ret.FindViewById<RecyclerView>(Resource.Id.list);

			return ret;
		}

		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated(savedInstanceState);

			adapter = new JobAdapter();
		}

		public override void OnResume() {
			base.OnResume();

			LoadAsync();
		}

		private async Task LoadAsync() {
			var progress = new ProgressDialog(Activity);
			progress.SetTitle(Resource.String.please_wait);
			progress.SetTitle(Resource.String.loading);
			progress.Show();

			var records = new List<JobRecord>();
			var jobs = await ion.database.QueryForAllAsync<JobRow>();
			foreach (var j in jobs) {
				var r = new JobRecord(j);

				var table = ion.database.Table<SessionRow>();
				var sessions = new List<SessionRecord>();

				foreach (var s in table.Where(sr => sr.frn_JID == j._id)) {
					sessions.Add(new SessionRecord(s));
				}

				r.sessions = sessions;
			}

			adapter.SetJobs(records);
			progress.Dismiss();
		}

		private enum EViewType {
			Job,
			Session,
		}

		public class SessionRecord : SwipableRecyclerViewAdapter.IRecord {
			/// <summary>
			///  The view type for the record.
			/// </summary>
			/// <value>The type of the view.</value>
			public int viewType { get { return (int)EViewType.Session; } }

			public SessionRow row { get; private set; }

			public bool isChecked { get; set; }

			public SessionRecord(SessionRow row) {
				this.row = row;
			}
		}

		public class SessionViewHolder : SwipableViewHolder<SessionRecord> {
			private TextView text;
			private CheckBox check;

			public SessionViewHolder(ViewGroup parent, int viewResource) : base(parent, viewResource) {
				text = view.FindViewById<TextView>(Resource.Id.name);
				check = view.FindViewById<CheckBox>(Resource.Id.check);
			}

			public override void OnBindTo() {
				var ellapsed = t.row.sessionEnd - t.row.sessionStart;
				text.Text = t.row.sessionStart.ToLongDateString() + " " + ellapsed.TotalMinutes.ToString("#.0");
			}
		}

		public class JobRecord : SwipableRecyclerViewAdapter.IRecord {
			/// <summary>
			/// The view type of the record.
			/// </summary>
			/// <value>The type of the view.</value>
			public int viewType { get { return (int)EViewType.Job; } }
			/// <summary>
			/// The job row that we are representing.
			/// </summary>
			public JobRow row { get; private set; }

			public IEnumerable<SessionRecord> sessions;

			public JobRecord(JobRow row) {
				this.row = row;
			}
		}

		public class JobViewHolder : SwipableViewHolder<JobRecord> {
			private TextView name;
			private View options;

			public JobViewHolder(ViewGroup parent, int resource) : base(parent, resource) {
				this.name = view.FindViewById<TextView>(Resource.Id.name);  
				this.options = view.FindViewById(Resource.Id.icon);
				options.Click += (sender, e) => {
					//        var adb = new IONAlertDialog(parent.Context);
					//        adb.SetTitle(Resource.String.report_job_options);
					Toast.MakeText(parent.Context, "Complete me", ToastLength.Short).Show();
				};
			}

			public override void OnBindTo() {
				name.Text = t.row.jobName;
			}
		}

		private class JobAdapter : SwipableRecyclerViewAdapter {
			public override long GetItemId(int position) {
				return records[position].viewType;
			}

			public override SwipableViewHolder OnCreateSwipableViewHolder(Android.Views.ViewGroup parent, int viewType) {
				var li = LayoutInflater.From(parent.Context);

				switch ((EViewType)viewType) {
					case EViewType.Job:
					return new JobViewHolder(parent, Resource.Layout.list_item_job);
					case EViewType.Session:
					return new SessionViewHolder(parent, Resource.Layout.list_item_session);
					default:
						throw new Exception("Cannot create view holder for " + (EViewType)viewType);
				}
			}

			public override void OnBindViewHolder(SwipableRecyclerViewAdapter.IRecord record, SwipableViewHolder vh, int position) {
				vh.record = record;
			}

			public override bool IsViewHolderSwipable(SwipableRecyclerViewAdapter.IRecord record, SwipableViewHolder viewHolder, int index) {
				return false;
			}

			public override Action GetViewHolderSwipeAction(int index) {
				return null;
			}

			/// <summary>
			/// Sets the jobs content for the adapter.
			/// </summary>
			/// <returns>The jobs.</returns>
			/// <param name="jobs">Jobs.</param>
			public void SetJobs(IEnumerable<JobRecord> jobs) {
				records.Clear();

				foreach (var j in jobs) {
					records.Add(j);
					records.AddRange(j.sessions);
				}

				NotifyDataSetChanged();
			}
		}
	}
}

