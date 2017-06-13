namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using Appion.Commons.Util;

	using ION.Core.Database;

	// Using ION.Droid
	using Dialog;
	using Job;
	using Fragments;
	using Views;
  using Util;
	using Widgets.RecyclerViews;

	public class ByJobFragment : IONFragment {
		public OnSessionChecked onSessionChecked;

		public HashSet<int> sessions { private get; set; }

		private RecyclerView list;
		private View empty;

		private JobAdapter adapter;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			var ret = inflater.Inflate(Resource.Layout.fragment_by_job, container, false);

			list = ret.FindViewById<RecyclerView>(Resource.Id.list);
			empty = ret.FindViewById(Resource.Id.empty);
			empty.Click += (sender, e) => {
				StartActivity(new Intent(container.Context, typeof(JobActivity)));
			};

			return ret;
		}

		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated(savedInstanceState);

			adapter = new JobAdapter();
			adapter.onJobHeaderClicked = OnJobClicked;
			adapter.onSessionClicked = (sr) => {
				NotifySessionChecked(sr);
			};
			list.SetAdapter(adapter);
		}

		public override void OnResume() {
			base.OnResume();

			LoadAsync();
		}

		private void OnJobClicked(JobRow row) {
			var ldb = new ListDialogBuilder(Activity);
			ldb.SetTitle(Resource.String.job_options);

			ldb.AddItem(Resource.String.job_info, () => {
				var i = new Intent(Activity, typeof(EditJobActivity));
				i.PutExtra(EditJobActivity.EXTRA_JOB_ID, row._id);
				StartActivity(i);
			});

			ldb.AddItem(Resource.String.select_all, () => {
				var record = adapter.FindJobRecord(row._id);

				foreach (var r in record.sessions) {
					r.isChecked = true;
					NotifySessionChecked(r);
				}

				adapter.RefreshJob(record);
			});

			ldb.AddItem(Resource.String.deselect_all, () => {
				var record = adapter.FindJobRecord(row._id);

				foreach (var r in record.sessions) {
					r.isChecked = false;
					NotifySessionChecked(r);
				}

				adapter.RefreshJob(record);
			});

			ldb.SetNegativeButton(Resource.String.cancel, (sender, e) => {
				var d = sender as Dialog;
				d.Dismiss();
			});

			ldb.Show();
		}

		private void NotifySessionChecked(SessionRecord record) {
			if (onSessionChecked != null) {
				onSessionChecked(record.data, record.isChecked);
				Log.D(this, "The record check state is: " + record.isChecked);
			}
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
				var sr = new List<SessionRecord>();

				foreach (var s in table.Where(s => s.frn_JID == j._id)) {
					var t = ion.database.Table<SensorMeasurementRow>();
					var query = t.Where(smr => smr.frn_SID == s._id)
					               .GroupBy(smr => smr.serialNumber);

					var count = query.Count();

					var sessionRecord = new SessionRecord(s, count);
					if (sessions != null) {
						sessionRecord.isChecked = sessions.Contains(s._id);
					}
					sr.Add(sessionRecord);
				}

				r.sessions = sr;
				var record = new JobRecord(j);
				record.sessions = r.sessions;
				records.Add(record);
			}

			adapter.SetJobs(records);

			if (adapter.ItemCount == 0) {
				list.Visibility = ViewStates.Gone;
				empty.Visibility = ViewStates.Visible;
			} else {
				list.Visibility = ViewStates.Visible;
				empty.Visibility = ViewStates.Gone;				
			}

			progress.Dismiss();
		}

		private enum EViewType {
			Job,
			Session,
		}

		public class SessionRecord : RecordAdapter.Record<SessionRow> {
			/// <summary>
			///  The view type for the record.
			/// </summary>
			/// <value>The type of the view.</value>
			public override int viewType { get { return (int)EViewType.Session; } }

			public int devicesCount { get; private set; }
			public JobRow job { get; set; }

			public bool isChecked { get; set; }

			public SessionRecord(SessionRow row, int devicesCount) : base(row) {
				this.devicesCount = devicesCount;
				this.isChecked = false;
			}
		}

		public class SessionViewHolder : RecordAdapter.SwipeRecordViewHolder<SessionRecord> {
			private TextView date;
			private TextView duration;
			private TextView devicesUsed;
			private CheckBox check;

			public SessionViewHolder(SwipeRecyclerView rv, int viewResource, Action<SessionRecord> onChecked) : base(rv, viewResource, Resource.Layout.list_item_button) {
				date = foreground.FindViewById<TextView>(Resource.Id.report_date_created);
				duration = foreground.FindViewById<TextView>(Resource.Id.report_session_duration);
				devicesUsed = foreground.FindViewById<TextView>(Resource.Id.report_devices_used);
				check = foreground.FindViewById<CheckBox>(Resource.Id.check);
				check.CheckedChange += (sender, e) => {
					record.isChecked = check.Checked;
					if (onChecked != null) {
						onChecked(record);
					}
				};
				check.Checked = false;

				foreground.SetOnClickListener(new ViewClickAction((view) => {
					record.isChecked = !check.Checked;
					check.Checked = record.isChecked;
				}));

				var button = background as TextView;
				button.SetText(Resource.String.remove);
				button.SetOnClickListener(new ViewClickAction((view) => {
				}));
			}

			public override void Invalidate() {
				var g = ION.Core.App.AppState.context.dataLogManager.QuerySessionDataAsync(record.data._id).Result;
				var dateString = record.data.sessionStart.ToLocalTime().ToShortDateString() + " " + record.data.sessionStart.ToLocalTime().ToShortTimeString();

				if (record.job == null || record.data.frn_JID == 0 || record.job._id == record.data.frn_JID) {
					date.SetTextColor(date.Context.Resources.GetColor(Resource.Color.black));
				} else {
					date.SetTextColor(date.Context.Resources.GetColor(Resource.Color.red));
				}

				date.Text = dateString;
        duration.Text = (record.data.sessionEnd - record.data.sessionStart).ToFriendlyString(date.Context);
				devicesUsed.Text = "" + record.devicesCount;
				check.Checked = record.isChecked;
			}
		}

		public class JobRecord : RecordAdapter.Record<JobRow> {
			/// <summary>
			/// The view type of the record.
			/// </summary>
			/// <value>The type of the view.</value>
			public override int viewType { get { return (int)EViewType.Job; } }

			public List<SessionRecord> sessions;

			public JobRecord(JobRow row) : base(row) { }
		}

		private class JobViewHolder : RecordAdapter.SwipeRecordViewHolder<JobRecord> {
			private TextView name;
			private View options;

			public JobViewHolder(SwipeRecyclerView rv, JobAdapter adapter) : base(rv, Resource.Layout.list_item_job, Resource.Layout.list_item_button) {
				this.name = foreground.FindViewById<TextView>(Resource.Id.name);  
				this.options = foreground.FindViewById(Resource.Id.icon);
				options.Click += (sender, e) => {
					adapter.NotifyJobHeaderClicked(record.data);
				};
			}

			public override void Invalidate() {
				name.Text = record.data.jobName;
			}
		}

		private class JobAdapter : RecordAdapter {
			public OnJobHeaderClicked onJobHeaderClicked;
			public Action<SessionRecord> onSessionClicked;

			public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
				var rv = recyclerView as SwipeRecyclerView;
				switch ((EViewType)viewType) {
					case EViewType.Job:
						return new JobViewHolder(rv, this);
					case EViewType.Session:
						var sessionret = new SessionViewHolder(rv, Resource.Layout.list_item_session, (sr) => {
							if (onSessionClicked != null) {
								onSessionClicked(sr);
							}
						});
						return sessionret;	
					default:
						throw new Exception("Cannot create view holder for " + (EViewType)viewType);
				}
			}

			public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
				if (holder is SwipeRecordViewHolder) {
					var vh = holder as SwipeRecordViewHolder;
					vh.data = records[position] as Record;
				} else if (holder is RecordViewHolder) {
					var vh = holder as RecordViewHolder;
					vh.data = records[position] as Record;
				}
			}


			public void NotifyJobHeaderClicked(JobRow job) {
				if (onJobHeaderClicked != null) {
					onJobHeaderClicked(job);
				}
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

			public JobRecord FindJobRecord(int id) {
				foreach (var r in records) {
					var jr = r as JobRecord;

					if (jr != null && jr.data._id == id) {
						return jr;
					}
				}

				return null;
			}

			public void RefreshJob(JobRecord record) {
				NotifyItemRangeChanged(records.IndexOf(record), 1 + record.sessions.Count);
			}
		}
	}
}

