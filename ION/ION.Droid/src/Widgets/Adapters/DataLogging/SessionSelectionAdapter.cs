namespace ION.Droid.Widgets.Adapters.DataLogging {

  using System;
  using System.Linq;
  using System.Collections.Generic;

  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Database;
  using ION.Core.Util;

  using ION.Droid.Views;
  using ION.Droid.Widgets.Adapters;

  /// <summary>
  /// The adapter that will list sessions.
  /// </summary>
  public class SessionSelectionAdapter : IONRecyclerViewAdapter {
    /// <summary>
    /// Gets the item count.
    /// </summary>
    /// <value>The item count.</value>
    public override int ItemCount {
      get {
        return records.Count;
      }
    }

    /// <summary>
    /// The records that the adapter will display.
    /// </summary>
    private List<IRecord> records = new List<IRecord>();

    public SessionSelectionAdapter() {
    }

    /// <summary>
    /// Gets the type of the item view.
    /// </summary>
    /// <returns>The item view type.</returns>
    /// <param name="position">Position.</param>
    public override int GetItemViewType(int position) {
      return (int)records[position].viewType;
    }

    /// <summary>
    /// Raises the create view holder event.
    /// </summary>
    /// <param name="parent">Parent.</param>
    /// <param name="viewType">View type.</param>
    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      var li = LayoutInflater.From(parent.Context);

      switch ((EViewType)viewType) {
        case EViewType.Job:
          return new JobViewHolder(this, li.Inflate(Resource.Layout.list_item_job, parent, false));
        case EViewType.Session:
          return new SessionViewHolder(this, li.Inflate(Resource.Layout.list_item_session, parent, false));
        default:
          throw new Exception("Cannot create view holer for: " + ((EViewType)viewType));  
      }
    }

    /// <summary>
    /// Raises the bind view holder event.
    /// </summary>
    /// <param name="holder">Holder.</param>
    /// <param name="position">Position.</param>
    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      var record = records[position];

      switch (record.viewType) {
        case EViewType.Job:
          ((JobViewHolder)holder).BindTo(record as JobRecord);
          return;
        case EViewType.Session:
          ((SessionViewHolder)holder).BindTo(record as SessionRecord);
          return;
      }
    }

    /// <summary>
    /// Sets the sessions that are to be added to the adapter.
    /// </summary>
    /// <param name="ion">Ion.</param>
    /// <param name="sessions">Sessions.</param>
    public void AddSessions(IION ion, IEnumerable<SessionRow> sessions) {
      foreach (var s in sessions) {
        records.Add(new SessionRecord(s));
      }
    }

    /// <summary>
    /// Sets the jobs that the adapter will display.
    /// </summary>
    /// <param name="jobs">Jobs.</param>
    public void AddJobs(IION ion, IEnumerable<JobRow> jobs) {
      foreach (var j in jobs) {
        var jr = new JobRecord(j);
        records.Add(jr);

        var sessions = new List<SessionRecord>();

        var sessionRows = ion.database.Table<SessionRow>()
          .Where(sr => sr.jobId == j.id)
          .AsEnumerable();

        foreach (var s in sessionRows) {
          var sr = new SessionRecord(s);
          records.Add(sr);
          sessions.Add(sr);
        }

        jr.sessions = sessions;
      }

      NotifyDataSetChanged();
    }

    /// <summary>
    /// Queries a list of the jobs that are checked.
    /// </summary>
    /// <returns>The checked jobs.</returns>
    public List<SessionRow> GetCheckedSessions() {
      var ret = new List<SessionRow>();

      foreach (var r in records) {
        var sr = r as SessionRecord;
        if (sr != null && sr.isChecked) {
          ret.Add(sr.sessionRow);
        }
      }

      return ret;
    }

    /// <summary>
    /// The abstract data object that will represent an entity displayed by the adapter.
    /// </summary>
    public interface IRecord {
      EViewType viewType { get; }
    }

    /// <summary>
    /// An enumeration that will enumerate the type of views that the adapter will show.
    /// </summary>
    public enum EViewType {
      Job,
      Session,
    }
  }

  public class JobRecord : SessionSelectionAdapter.IRecord {
    public SessionSelectionAdapter.EViewType viewType {
      get {
        return SessionSelectionAdapter.EViewType.Job;
      }
    }

    public JobRow jobRow { get; set; }
    public IEnumerable<SessionRecord> sessions { get; set; }
    public bool isChecked { get; set; }

    public JobRecord(JobRow row) {
      this.jobRow = row;
    }
  }

  public class SessionRecord : SessionSelectionAdapter.IRecord {
    public SessionSelectionAdapter.EViewType viewType {
      get {
        return SessionSelectionAdapter.EViewType.Session;
      }
    }

    public SessionRow sessionRow { get; set; }
    public bool isChecked { get; set; }

    public SessionRecord(SessionRow row) {
      this.sessionRow = row;
    }
  }

  public class JobViewHolder : RecyclerView.ViewHolder {
    private SessionSelectionAdapter adapter;
    private TextView name;
    private CheckBox checkBox;

    private JobRecord record;

    public JobViewHolder(SessionSelectionAdapter adapter, View view) : base(view) {
      this.adapter = adapter;
      name = view.FindViewById<TextView>(Resource.Id.name);
      checkBox = view.FindViewById<CheckBox>(Resource.Id.check);
      view.SetOnClickListener(new ViewClickAction((v) => {
        if (record != null) {
          checkBox.Checked = record.isChecked = !record.isChecked;

          foreach (var sr in record.sessions) {
            sr.isChecked = record.isChecked;
          }

          adapter.NotifyDataSetChanged();
        }
      }));
    }

    public void BindTo(JobRecord record) {
      this.record = record;

      name.Text = record.jobRow.jobName;

      record.isChecked = false;
      foreach (var sr in record.sessions) {
        if (sr.isChecked) {
          record.isChecked = true;
          break;
        }
      }
      checkBox.Checked = record.isChecked;
    }
  }

  public class SessionViewHolder : RecyclerView.ViewHolder {
    private SessionSelectionAdapter adapter;
    private TextView name;
    private CheckBox checkBox;

    private SessionRecord record;

    public SessionViewHolder(SessionSelectionAdapter adapter, View view) : base(view) {
      this.adapter = adapter;
      name = view.FindViewById<TextView>(Resource.Id.name);
      checkBox = view.FindViewById<CheckBox>(Resource.Id.check);
      view.SetOnClickListener(new ViewClickAction((v) => {
        if (record != null) {
          checkBox.Checked = record.isChecked = !record.isChecked;
          adapter.NotifyDataSetChanged();
        }
      }));
    }

    public void BindTo(SessionRecord record) {
      this.record = record;

      name.Text = record.sessionRow.sessionStart + " - " + record.sessionRow.sessionEnd;
      checkBox.Checked = record.isChecked;
    }
  }
}

