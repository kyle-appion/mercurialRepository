﻿namespace ION.Droid.Widgets.Adapters.Job {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Database;
  using ION.Core.Util;

  using ION.Droid.Widgets.RecyclerViews;

  public class JobAdapter : SwipableRecyclerViewAdapter {

    private IION ion;

    public JobAdapter(IION ion) {
      this.ion = ion;
    }

    public override void OnAttachedToRecyclerView(Android.Support.V7.Widget.RecyclerView recyclerView) {
      base.OnAttachedToRecyclerView(recyclerView);

      ion.database.onDatabaseEvent += OnDatabaseEvent;
    }

    public override void OnDetachedFromRecyclerView(Android.Support.V7.Widget.RecyclerView recyclerView) {
      base.OnDetachedFromRecyclerView(recyclerView);

      ion.database.onDatabaseEvent -= OnDatabaseEvent;
    }

    public override SwipableViewHolder OnCreateSwipableViewHolder(ViewGroup parent, int viewType) {
      switch ((EViewType)viewType) {
        case EViewType.Job:
          return new JobViewHolder(parent, Resource.Layout.list_item_job_row);
        default:
          throw new Exception("Cannot create view for " + (EViewType)viewType);
      }
    }

    public override bool IsViewHolderSwipable(SwipableRecyclerViewAdapter.IRecord record, SwipableViewHolder viewHolder, int index) {
      return true;
    }

    public override Action GetViewHolderSwipeAction(int index) {
      return () => {
        var job = ((JobRecord)records[index]).row;
        Log.D(this, "Deleted job: " + job._id + " = " + AppState.context.database.DeleteAsync<JobRow>(job));
      };
    }

    public int IndexOfJob(JobRow job) {
      for (int i = 0; i < this.ItemCount; i++) {
        var r = records[i] as JobRecord;
        if (r?.row._id == job?._id) {
          return i;
        }
      }

      return -1;
    }

    public JobRecord JobRecordWithId(long id) {
      for (int i = 0; i < records.Count; i++) {
        var job = records[i] as JobRecord;
        if (job != null && job.row._id == id) {
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

      var index = IndexOfJob(JobRecordWithId(de.id)?.row);

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
