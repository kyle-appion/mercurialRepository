namespace ION.Droid.Widgets.Adapters.Job {
  
  using System;

  using Android.Views;
  using Android.Widget;

  using ION.Core.Database;

  using ION.Droid.Widgets.RecyclerViews;

  public class JobRecord : SwipableRecyclerViewAdapter.IRecord {
    public int viewType { get { return (int)EViewType.Job; } } 

    public JobRow row { get; private set; }

    public JobRecord(JobRow row) {
      this.row = row;
    }
  }

  public class JobViewHolder : SwipableViewHolder<JobRecord> {
    private TextView name, customer, dispatch, purchase;
    private View check;

    public JobViewHolder(ViewGroup parent, int resource) : base(parent, resource) {
      this.name = view.FindViewById<TextView>(Resource.Id.name);
      this.customer = view.FindViewById<TextView>(Resource.Id.customer_no);
      this.dispatch = view.FindViewById<TextView>(Resource.Id.dispatch_no);
      this.purchase = view.FindViewById<TextView>(Resource.Id.purchase_no);
      this.check = view.FindViewById(Resource.Id.check);
      check.Visibility = ViewStates.Gone;
    }

    public override void OnBindTo() {
      name.Text = t.row.jobName;
      customer.Text = t.row.customerNumber;
      dispatch.Text = t.row.dispatchNumber;
      purchase.Text = t.row.poNumber;
    }
  }
}

