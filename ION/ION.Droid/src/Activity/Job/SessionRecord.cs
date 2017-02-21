namespace ION.Droid.Activity.Job {

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

	public class SessionViewHolder : RecordAdapter.RecordViewHolder<SessionRecord> {
		public TextView date;
		public TextView duration;
		public TextView devicesUsed;
		public CheckBox check;

		public SessionViewHolder(ViewGroup parent, int viewResource, Action<SessionRecord> onChecked) : base(parent, viewResource) {
			date = ItemView.FindViewById<TextView>(Resource.Id.report_date_created);
			duration = ItemView.FindViewById<TextView>(Resource.Id.report_session_duration);
			devicesUsed = ItemView.FindViewById<TextView>(Resource.Id.report_devices_used);
			check = ItemView.FindViewById<CheckBox>(Resource.Id.check);
			check.Enabled = false;
			check.Clickable = false;
			check.Checked = false;
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
			duration.Text = ToFriendlyString(record.data.sessionEnd - record.data.sessionStart);
			devicesUsed.Text = "" + record.devicesCount;
			check.Checked = record.isChecked;
		}

		private string ToFriendlyString(TimeSpan timeSpan) {
			var c = ItemView.Context;
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

