namespace ION.Droid.Activity.Report {

  using System;
  using System.Collections.Generic;

  using Android.Views;
  using Android.Widget;

  using ION.Core.Database;

  using ION.Droid.Dialog;
  using ION.Droid.Widgets.RecyclerViews;

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

    public IEnumerable<SessionRecord> sessions;

    public JobRecord(JobRow row) {
      this.row = row;
    }
  }

  public class JobViewHolder : SwipableViewHolder<JobRecord> {
    private TextView name;
    private View options;

    public JobViewHolder(ViewGroup parent, int resource) : base(parent, resource) {
      this.name = view.FindViewById<TextView>(Resource.Id.name);  
      this.options = view.FindViewById(Resource.Id.icon);
      options.Click += (sender, e) => {
//        var adb = new IONAlertDialog(parent.Context);
//        adb.SetTitle(Resource.String.report_job_options);
        Toast.MakeText(parent.Context, "Complete me", ToastLength.Short).Show();
      };
    }

    public override void OnBindTo() {
      name.Text = t.row.jobName;
    }
  }
}

