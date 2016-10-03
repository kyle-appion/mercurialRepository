using System.Linq;
namespace ION.Droid.Widgets.Adapters.Job {
  
  using System;

  using Android.Views;
  using Android.Widget;

  using ION.Core.Database;

  using ION.Droid.Widgets.RecyclerViews;

  public class SessionRecord : SwipableRecyclerViewAdapter.IRecord {
    /// <summary>
    ///  The view type for the record.
    /// </summary>
    /// <value>The type of the view.</value>
    public int viewType { get { return (int)EViewType.Session; } }

    public SessionRow row { get; private set; }
		public JobRow job { get; set; }

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
			check.CheckedChange += (sender, e) => {
				t.isChecked = check.Checked;
			};
    }

    public override void OnBindTo() {
      var ellapsed = t.row.sessionEnd - t.row.sessionStart;
			var g = ION.Core.App.AppState.context.dataLogManager.QuerySessionDataAsync(t.row._id).Result;
			var dateString = t.row.sessionStart.ToLocalTime().ToShortDateString() + " " + t.row.sessionStart.ToLocalTime().ToShortTimeString();
			dateString = dateString + " " + ellapsed.TotalMinutes.ToString("0.0") + " # Records: " + g.deviceSensorLogs.Length;

			ION.Core.Util.Log.D(this, "frn_jid: " + t.row.frn_JID + " job.id: " + t.job?._id);
			if (t.job == null || t.row.frn_JID == 0 || t.job._id == t.row.frn_JID) {
				text.SetTextColor(text.Context.Resources.GetColor(Resource.Color.black));
			} else {
				text.SetTextColor(text.Context.Resources.GetColor(Resource.Color.red));
			}

			text.Text = dateString;
			check.Checked = t.isChecked;
    }
  }
}

