using System.Linq;
namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;
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
	using Widgets.RecyclerViews;

	public class ByJobFragment : IONFragment {
		public OnSessionChecked onSessionChecked;

		public List<int> sessions { private get; set; }

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
				onSessionChecked(record.row, record.isChecked);
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

		public class SessionRecord : SwipableRecyclerViewAdapter.IRecord {
			/// <summary>
			///  The view type for the record.
			/// </summary>
			/// <value>The type of the view.</value>
			public int viewType { get { return (int)EViewType.Session; } }

			public SessionRow row { get; private set; }
			public int devicesCount { get; private set; }
			public JobRow job { get; set; }

			public bool isChecked { get; set; }

			public SessionRecord(SessionRow row, int devicesCount) {
				this.row = row;
				this.devicesCount = devicesCount;
				this.isChecked = false;
			}
		}

		public class SessionViewHolder : SwipableViewHolder<SessionRecord> {
			private TextView date;
			private TextView duration;
			private TextView devicesUsed;
			private CheckBox check;

			public SessionViewHolder(ViewGroup parent, int viewResource, Action<SessionRecord> onChecked) : base(parent, viewResource) {
				date = view.FindViewById<TextView>(Resource.Id.report_date_created);
				duration = view.FindViewById<TextView>(Resource.Id.report_session_duration);
				devicesUsed = view.FindViewById<TextView>(Resource.Id.report_devices_used);
				check = view.FindViewById<CheckBox>(Resource.Id.check);
				check.CheckedChange += (sender, e) => {
					t.isChecked = check.Checked;
					if (onChecked != null) {
						onChecked(this.t);
					}
				};
				check.Checked = false;

				view.SetOnClickListener(new ViewClickAction((view) => {
					t.isChecked = !check.Checked;
					check.Checked = t.isChecked;
				}));;
			}

			public override void OnBindTo() {
				var g = ION.Core.App.AppState.context.dataLogManager.QuerySessionDataAsync(t.row._id).Result;
				var dateString = t.row.sessionStart.ToLocalTime().ToShortDateString() + " " + t.row.sessionStart.ToLocalTime().ToShortTimeString();

				if (t.job == null || t.row.frn_JID == 0 || t.job._id == t.row.frn_JID) {
					date.SetTextColor(date.Context.Resources.GetColor(Resource.Color.black));
				} else {
					date.SetTextColor(date.Context.Resources.GetColor(Resource.Color.red));
				}

				date.Text = dateString;
				duration.Text = ToFriendlyString(t.row.sessionEnd - t.row.sessionStart);
				devicesUsed.Text = "" + t.devicesCount;
				check.Checked = t.isChecked;
			}

			private string ToFriendlyString(TimeSpan timeSpan) {
				var c = view.Context;
				if (timeSpan.TotalHours > 24) {
					return timeSpan.TotalDays.ToString("#.#") + " " + c.GetString(Resource.String.time_days_abrv);
				} else if (timeSpan.TotalMinutes > 60) {
					return timeSpan.TotalHours.ToString("#.#") + " " + c.GetString(Resource.String.time_hours_abrv);
				} else if (timeSpan.TotalSeconds > 60) {
					return timeSpan.TotalMinutes.ToString("#.#") + " " + c.GetString(Resource.String.time_minutes_abrv);
				} else {
					return timeSpan.TotalSeconds.ToString("#.#") + " " + c.GetString(Resource.String.time_seconds_abrv);
				}
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

			public List<SessionRecord> sessions;

			public JobRecord(JobRow row) {
				this.row = row;
			}
		}

		private class JobViewHolder : SwipableViewHolder<JobRecord> {
			private TextView name;
			private View options;

			public JobViewHolder(ViewGroup parent, JobAdapter adapter) : base(parent, Resource.Layout.list_item_job) {
				this.name = view.FindViewById<TextView>(Resource.Id.name);  
				this.options = view.FindViewById(Resource.Id.icon);
				options.Click += (sender, e) => {
					adapter.NotifyJobHeaderClicked(t.row);
				};
			}

			public override void OnBindTo() {
				name.Text = t.row.jobName;
			}
		}

		private class JobAdapter : SwipableRecyclerViewAdapter {
			public OnJobHeaderClicked onJobHeaderClicked;
			public Action<SessionRecord> onSessionClicked;

			public override long GetItemId(int position) {
				return records[position].viewType;
			}

			public override SwipableViewHolder OnCreateSwipableViewHolder(ViewGroup parent, int viewType) {
				switch ((EViewType)viewType) {
					case EViewType.Job:
						return new JobViewHolder(parent, this);
					case EViewType.Session:
						var sessionret = new SessionViewHolder(parent, Resource.Layout.list_item_session, (sr) => {
							if (onSessionClicked != null) {
								onSessionClicked(sr);
							}
						});
						sessionret.button.SetText(Resource.String.delete);
						return sessionret;	
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

					if (jr != null && jr.row._id == id) {
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

