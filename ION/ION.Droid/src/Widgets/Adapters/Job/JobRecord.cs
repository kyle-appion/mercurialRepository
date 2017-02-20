
namespace ION.Droid.Widgets.Adapters.Job {
  
  using Android.Views;
  using Android.Widget;

	using ION.Core.App;
  using ION.Core.Database;

	using ION.Droid.Dialog;
  using ION.Droid.Widgets.RecyclerViews;
	using ION.Droid.Views;

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
			var b = background as TextView;
			b.SetText(Resource.String.delete);
			b.SetOnClickListener(new ViewClickAction((view) => {
				if (record == null) {
					return;
				}
				var adb = new IONAlertDialog(view.Context);
				adb.SetTitle(Resource.String.job_delete);
				adb.SetMessage(Resource.String.job_delete_message);
				adb.SetNegativeButton(Resource.String.cancel, (sender, e) => {});
				adb.SetPositiveButton(Resource.String.delete, (sender, e) => {;
					var ion = AppState.context;
					ion.database.DeleteAsync(record.data);
				});
				adb.Show();
			}));
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

