namespace ION.Droid.Activity.DataLogging {

  using Android.Widgets;

  using ION.Core.Database;

  using ION.Droid.Widgets.RecyclerViews;

  internal class SessionRecord : SwipableRecyclerViewAdapter.IRecord {
    /// <summary>
    /// Queries the view type of the record. This is used for identification and record switching.
    /// </summary>
    /// <value>The type of the view.</value>
    public int viewType {
      get {
        return (int)EViewType.Session;
      }
    }

    public SessionRow sessionRow;
    public bool isChecked;

    public SessionRecord(SessionRow sessionRow) {
      this.sessionRow = sessionRow;
    }
  }

  /// <summary>
  /// The view holder that will display a session to the user.
  /// </summary>
  internal class SessionViewHolder : SwipableViewHolder {
    private SessionRecord record;

    private TextView name;
    private CheckBox checkBox;

    public SessionViewHolder(ViewGroup parent) : base(parent, Resource.Layout.list_item_session) {
      this.name = this.view.FindViewById<TextView>(Resource.Id.name);
      this.checkBox = this.view.FindViewById<CheckBox>(Resource.Id.check);
    }

    /// <summary>
    /// Binds the session view holder to the given session record.
    /// </summary>
    public void BindTo(SessionRecord record) {
      this.record = record;

      var sd = record.sessionRow.sessionStart;
      var ed = record.sessionRow.sessionEnd;
      // TODO ahodder@appioninc.com: Localize the minutes in this string.
      name.Text = sd.ToShortDateString() + " " + sd.ToShortTimeString() + " " + (ed - sd).TotalMinutes + " min";
      checkBox.Checked = record.isChecked;
    }
  }
}

