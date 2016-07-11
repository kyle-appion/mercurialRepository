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
      text.Text = t.row.sessionStart.ToLongDateString() + " " + ellapsed.TotalMinutes.ToString("#.0");
			check.Checked = t.isChecked;
    }
  }
}

