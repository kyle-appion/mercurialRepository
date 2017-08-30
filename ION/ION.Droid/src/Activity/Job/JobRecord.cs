using System;

using Android.Graphics;
using Android.Views;
using Android.Widget;

using ION.Core.App;
using ION.Core.Database;

using ION.Droid.Dialog;
using ION.Droid.Widgets.RecyclerViews;
using ION.Droid.Views;

namespace ION.Droid.Activity.Job {
	
	public class JobRecord : RecordAdapter.Record<JobRow> {
		public override int viewType { get { return (int)EViewType.Job; } } 

		public JobRecord(JobRow row) : base(row) {
		}
	}

	public class JobViewHolder : RecordAdapter.SwipeRecordViewHolder<JobRecord> { 
		private TextView id, name, customer, dispatch, purchase;
		private ImageView favorite;

		public JobViewHolder(SwipeRecyclerView rv, Action<JobRow> onFavoriteClicked) : base(rv, Resource.Layout.view_holder_job, Resource.Layout.view_delete) {
      this.id = foreground.FindViewById<TextView>(Resource.Id.id);
			this.name = foreground.FindViewById<TextView>(Resource.Id.name);
			this.customer = foreground.FindViewById<TextView>(Resource.Id.customer_no);
			this.dispatch = foreground.FindViewById<TextView>(Resource.Id.dispatch_no);
			this.purchase = foreground.FindViewById<TextView>(Resource.Id.purchase_no);
			this.favorite = foreground.FindViewById<ImageView>(Resource.Id.check);
			favorite.Click += (s, e) => {
				onFavoriteClicked(record.data);
			};
			var b = background as TextView;
			b.SetText(Resource.String.delete);
			b.SetOnClickListener(new ViewClickAction((view) => {
				if (record == null) {
					return;
				}
				var adb = new IONAlertDialog(view.Context);
				adb.SetTitle(Resource.String.job_delete_title);
				adb.SetMessage(Resource.String.job_delete_message);
				adb.SetNegativeButton(Resource.String.cancel, (sender, e) => {});
				adb.SetPositiveButton(Resource.String.delete, (sender, e) => {
					var ion = AppState.context;
					ion.database.DeleteAsync(record.data);
				});
				adb.Show();
			}));
		}

		public override void Invalidate() {
      var ion = AppState.context;
			var c = ItemView.Context;
    
      id.Text = record.data._id + "";
			name.Text = record.data.jobName;
			customer.Text = record.data.customerNumber;
			dispatch.Text = record.data.dispatchNumber;
			purchase.Text = record.data.poNumber;

			if (ion.preferences.job.activeJob == record.data._id) {
				favorite.SetColorFilter(Resource.Color.gold.AsResourceColor(c), PorterDuff.Mode.SrcAtop);
			} else {
				favorite.SetColorFilter(Resource.Color.black.AsResourceColor(c), PorterDuff.Mode.SrcAtop);
			}
		}
	}
}

