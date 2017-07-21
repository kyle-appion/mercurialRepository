namespace ION.Droid.Activity.Report {
  
  using System;

  using Android.Views;
  using Android.Widget;

  using ION.Core.Database;

	using ION.Droid.Views;
  using ION.Droid.Widgets.RecyclerViews;


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
				if (onChecked != null) {
					onChecked(record);
				}
			}));
    }

    public override void Invalidate() {
			var g = ION.Core.App.AppState.context.dataLogManager.QuerySessionDataAsync(record.data._id).Result;
			var dateString = record.data.sessionStart.ToLocalTime().ToShortDateString() + " " + record.data.sessionStart.ToLocalTime().ToShortTimeString();

			if (record.job == null || record.data.frn_JID == 0 || record.job._id == record.data.frn_JID) {
				date.SetTextColor(Resource.Color.black.AsResourceColor(date.Context));
			} else {
				date.SetTextColor(Resource.Color.red.AsResourceColor(date.Context));
			}

			date.Text = dateString;
			duration.Text = ToFriendlyString(record.data.sessionEnd - record.data.sessionStart);
			devicesUsed.Text = "" + record.devicesCount;
			check.Checked = record.isChecked;
    }

		private string ToFriendlyString(TimeSpan timeSpan) {
			var c = foreground.Context;
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

