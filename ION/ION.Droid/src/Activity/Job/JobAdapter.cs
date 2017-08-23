namespace ION.Droid.Activity.Job {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Runtime;
	using Android.Support.V7.Widget;
	using Android.Views;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Database;

	using ION.Droid.Dialog;
	using ION.Droid.Widgets.RecyclerViews;

	public class JobAdapter : RecordAdapter {

		public event Action<JobRow> onFavoriteClicked;

		private IION ion;

		public JobAdapter(IION ion) {
			this.ion = ion;
		}

		public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
			base.OnAttachedToRecyclerView(recyclerView);

			ion.database.onDatabaseEvent += OnDatabaseEvent;
		}

		public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
			base.OnDetachedFromRecyclerView(recyclerView);

			ion.database.onDatabaseEvent -= OnDatabaseEvent;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
			var rv = recyclerView as SwipeRecyclerView;
			var context = parent.Context;
			switch ((EViewType)viewType) {
				case EViewType.Job:
					var jobret = new JobViewHolder(rv, (job => {
						onFavoriteClicked(job);
					}));
				  return jobret;
				default:
					// TODO ahodder@appioninc.com: This needs to return a template empty object
					Log.E(this, "Failed to create view for: " + (EViewType)viewType);
				  return null;
			}
		}

		public void RequestDeleteJob(JobRecord record) {
			var context = recyclerView.Context;
			var adb = new IONAlertDialog(context);
			adb.SetTitle(Resource.String.job_delete_title);
			adb.SetMessage(Resource.String.job_delete_message);

			adb.SetNegativeButton(Resource.String.cancel, (sender, e) => {
				var d = sender as Dialog;
				if (d != null) {
					d.Cancel();
				}
			});
			adb.SetPositiveButton(Resource.String.delete, (sender, e) => {
				Log.V(this, "Deleted job: " + record.data._id + " = " + AppState.context.database.DeleteAsync<JobRow>(record.data));
			});

			adb.Show();
		}

		public int IndexOfJob(JobRow job) {
			for (int i = 0; i < this.ItemCount; i++) {
				var r = records[i] as JobRecord;
				if (r?.data._id == job?._id) {
					return i;
				}
			}

			return -1;
		}

		public JobRecord JobRecordWithId(long id) {
			for (int i = 0; i < records.Count; i++) {
				var job = records[i] as JobRecord;
				if (job != null && job.data._id == id) {
					return job;
				}
			}

			return null;
		}

		public void SetJobs(IEnumerable<JobRecord> jobs) {
			records.Clear();

			records.AddRange(jobs);

			NotifyDataSetChanged();
		}

		private async void OnDatabaseEvent(IONDatabase database, DatabaseEvent de) {
			if (!de.table.Equals(typeof(JobRow))) {
				return;
			}

			var index = IndexOfJob(JobRecordWithId(de.id)?.data);

			if (index < 0) {
				return;
			} 

			switch (de.action) {
				case DatabaseEvent.EAction.Deleted:
					records.RemoveAt(index);
					NotifyItemRemoved(index);
				break;
				case DatabaseEvent.EAction.Inserted:
					var job = await ion.database.QueryForAsync<JobRow>(de.id);
					ion.PostToMain(() => {
						records.Insert(index, new JobRecord(job));
						NotifyItemInserted(index);
					});
				break;
				case DatabaseEvent.EAction.Modified:
					NotifyItemChanged(index);
				break;
			}
		}
	}
}

