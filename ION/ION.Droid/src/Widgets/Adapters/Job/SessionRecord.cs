namespace ION.Droid.Widgets.Adapters.Job {
  
  using System;

  using Android.Views;
  using Android.Widget;

  using ION.Core.Database;

	using ION.Droid.Views;
  using ION.Droid.Widgets.RecyclerViews;


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
				if (onChecked != null) {
					onChecked(this.t);
				}
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
}

