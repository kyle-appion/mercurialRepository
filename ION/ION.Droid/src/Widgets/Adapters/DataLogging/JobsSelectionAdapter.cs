namespace ION.Droid.Widgets.Adapters.DataLogging {

  using System;
  using System.Collections.Generic;

  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Database;
  using ION.Core.Util;

  using ION.Droid.Views;
  using ION.Droid.Widgets.Adapters;

  /// <summary>
  /// This class is used to select an arbitrary amount of jobs from a selectable list.
  /// </summary>
  public class JobsSelectionAdapter : IONRecyclerViewAdapter {
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
    
    public JobsSelectionAdapter() {
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
          return new JobViewHolder(li.Inflate(Resource.Layout.list_item_job, parent, false));
        default:
          throw new Exception("Cannot create view holder for: " + ((EViewType)viewType));
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
      }
    }

    /// <summary>
    /// Sets the jobs that the adapter will display.
    /// </summary>
    /// <param name="jobs">Jobs.</param>
    public void SetJobs(IEnumerable<JobRow> jobs) {
      records.Clear();
      foreach (var j in jobs) {
        records.Add(new JobRecord(j));
      }
      Log.D(this, "Jobs = " + records.Count);
      NotifyDataSetChanged();
    }

    /// <summary>
    /// Queries a list of the jobs that are checked.
    /// </summary>
    /// <returns>The checked jobs.</returns>
    public List<JobRow> GetCheckedJobs() {
      var ret = new List<JobRow>();

      foreach (var r in records) {
        var jr = r as JobRecord;
        if (jr != null && jr.isChecked) {
          ret.Add(jr.jobRow);
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
    }
  }

  public class JobRecord : JobsSelectionAdapter.IRecord {
    public JobsSelectionAdapter.EViewType viewType {
      get {
        return JobsSelectionAdapter.EViewType.Job;
      }
    }

    public JobRow jobRow { get; set; }
    public bool isChecked { get; set; }

    public JobRecord(JobRow row) {
      this.jobRow = row;
    }
  }

  public class JobViewHolder : RecyclerView.ViewHolder {
    private TextView name;
    private CheckBox checkBox;

    private JobRecord record;

    public JobViewHolder(View view) : base(view) {
      name = view.FindViewById<TextView>(Resource.Id.name);
      checkBox = view.FindViewById<CheckBox>(Resource.Id.check);
      view.SetOnClickListener(new ViewClickAction((v) => {
        if (record != null) {
          checkBox.Checked = record.isChecked = !record.isChecked;
        }
      }));
    }

    public void BindTo(JobRecord record) {
      this.record = record;

      name.Text = record.jobRow.jobName;
      checkBox.Checked = record.isChecked;
    }
  }
}

