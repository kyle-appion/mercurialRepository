namespace ION.Droid.Widgets.Adapters.Job {
  
  using Android.Views;
  using Android.Widget;

  using ION.Core.Database;

  using ION.Droid.Widgets.RecyclerViews;

	public class JobRecord : RecordAdapter.Record<JobRow> {
    public override int viewType { get { return (int)EViewType.Job; } } 

		public JobRecord(JobRow row) : base(row) {
    }
  }

	public class JobViewHolder : RecordAdapter.SwipeRecordViewHolder<JobRecord> { 
    private TextView name, customer, dispatch, purchase;
    private View check;

		public JobViewHolder(SwipeRecyclerView rv, int resource) : base(rv, resource, Resource.Layout.list_item_button) {
      this.name = foreground.FindViewById<TextView>(Resource.Id.name);
			this.customer = foreground.FindViewById<TextView>(Resource.Id.customer_no);
			this.dispatch = foreground.FindViewById<TextView>(Resource.Id.dispatch_no);
			this.purchase = foreground.FindViewById<TextView>(Resource.Id.purchase_no);
			this.check = foreground.FindViewById(Resource.Id.check);
      check.Visibility = ViewStates.Gone;
    }

    public override void Invalidate() {
			name.Text = record.data.jobName;
			customer.Text = record.data.customerNumber;
			dispatch.Text = record.data.dispatchNumber;
			purchase.Text = record.data.poNumber;
    }
  }
}

