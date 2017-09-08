﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Appion.Commons.Util;

using ION.Core.Database;

using ION.Droid.Dialog;
using ION.Droid.Fragments;
using ION.Droid.Widgets.RecyclerViews;

namespace ION.Droid.Activity.Report {
  public class ReportByJobFragment : IONFragment, ReportSessionAdapter.ISessionActions {
  
    private SwipeRecyclerView list;
    private View delete;
    private View export;
    
    private ReportSessionAdapter adapter;
    
    private ReportActivity activity;
    
    public ReportByJobFragment(ReportActivity activity) {
      this.activity = activity;
    }
    
    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
      var ret = inflater.Inflate(Resource.Layout.fragment_report_sessions, container, false);
      
      list = ret.FindViewById<SwipeRecyclerView>(Resource.Id.list);
      delete = ret.FindViewById(Resource.Id.delete);
      delete.Click += (sender, e) => {
        RequestDeleteSessions(adapter.GetCheckedSessions());
      };
      
      export = ret.FindViewById(Resource.Id.export);
      export.Click += (sender, e) => {
        StartExportActivity();
      };
      
      adapter = new ReportSessionAdapter(list);
      adapter.emptyView = ret.FindViewById(Resource.Id.empty);
      adapter.sessionActions = this;
      list.SetAdapter(adapter);

      return ret;
    }
  
    public override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);
    }
    
    public override void OnResume() {
      base.OnResume();
      Reload();
    }
    
    public void OnJobClicked(ReportJobHeaderRecord record) {
      ShowJobContextDialog(record);
    }
    
    public void OnCheckChanged(ReportSessionRecord record) {
      if (record.isChecked) {
        activity.checkedSessions.Add(record.data._id);
      } else {
        activity.checkedSessions.Remove(record.data._id);
      }
      InvalidateDelete();
    }
    
    public void OnDeleteClicked(ReportSessionRecord record) {
      RequestDeleteSessions(new SessionRow[] { record.data } );
    }
    
    public void Reload() {
      var progress = new ProgressDialog(Activity);
      progress.SetTitle(Resource.String.please_wait);
      progress.SetMessage(GetString(Resource.String.loading));
      progress.Indeterminate = true;
      progress.Show();

      Task.Factory.StartNew(async () => {
        var db = ion.database;
        var jobs = new Dictionary<JobRow, List<SessionRow>>();

        // Add the "Default" job that sessions are assigned to
        jobs.Add(new JobRow() {
          _id = 0,
          jobName = GetString(Resource.String.report_session_unassigned),
        }, new List<SessionRow>());

        foreach (var job in await db.QueryForAllAsync<JobRow>()) {
          jobs[job] = new List<SessionRow>();
        }

        foreach (var job in jobs.Keys) {
          var sessions = jobs[job];
          sessions.AddRange(db.Table<SessionRow>().Where(x => x.frn_JID == job._id).AsEnumerable());
        }

        var records = new List<RecordAdapter.IRecord>();

        foreach (var job in jobs.Keys) {
          records.Add(new ReportJobHeaderRecord(job, jobs[job]));
          foreach (var session in jobs[job]) {
            records.Add(await ReportSessionRecord.NewAsync(ion, session));
          }
        }

        return records;
      }).ContinueWith((arg) => {
        progress.Dismiss();

        if (arg.Result.IsFaulted) {
          Error(GetString(Resource.String.report_error_unknown), arg.Exception);
        } else {
          adapter.Set(arg.Result.Result);
        }
        InvalidateDelete();
        SyncCheckedSessions();
      }, TaskScheduler.FromCurrentSynchronizationContext());
    }
    
    private void SyncCheckedSessions() {
      foreach (var id in activity.checkedSessions) {
        adapter.SetCheckStatusForSessionRow(id, true);
      }
    }
    
    private void ShowJobContextDialog(ReportJobHeaderRecord job) {
      var dialog = new ListDialogBuilder(Activity);
      dialog.SetTitle(Resource.String.report_job_details);
      dialog.AddItem(Resource.String.report_delete_sessions, () => {
        RequestDeleteSessions(job.sessions);
      });
      dialog.AddItem(Resource.String.report_check_all, () => {
        adapter.SetCheckStatus(job.sessions, true);
      });
      dialog.AddItem(Resource.String.report_uncheck_all, () => {
        adapter.SetCheckStatus(job.sessions, false);
      });
      dialog.Show();
    }
    
    private void InvalidateDelete() {
      if (activity.checkedSessions.Count > 0) {
        delete.Visibility = ViewStates.Visible;
        export.SetBackgroundResource(Resource.Drawable.selector_button_gold_round);
      } else {
        delete.Visibility = ViewStates.Invisible;
        export.SetBackgroundResource(Resource.Drawable.xml_rect_light_gray_black_bordered_round);
      }
    }
    
    private void RequestDeleteSessions(IEnumerable<SessionRow> sessions) {
      var adb = new IONAlertDialog(Activity);
      adb.SetTitle(Resource.String.report_sessions_delete_request);
      adb.SetMessage(Resource.String.report_sessions_delete_warning);
      adb.SetNegativeButton(Resource.String.cancel, (sender, e) => { });
      adb.SetPositiveButton(Resource.String.delete, (sender, e) => {
        DeleteCheckedSessions(sessions);
      });
      adb.Show();
    }

    private void DeleteCheckedSessions(IEnumerable<SessionRow> sessions) {
      var pd = new ProgressDialog(Activity);
      pd.SetTitle(Resource.String.please_wait);
      pd.SetMessage(GetString(Resource.String.report_sessions_deleting));
      pd.Show();
      Task.Factory.StartNew(() => {
        var db = ion.database;
        try {
          db.BeginTransaction();

          foreach (var sr in adapter.GetCheckedSessions()) {
            db.Delete<SessionRow>(sr._id);
          }

          db.Commit();
        } catch (Exception e) {
          db.Rollback();
          throw e;
        }
      }).ContinueWith((arg) => {
        if (arg.IsFaulted) {
          Error(GetString(Resource.String.report_error_unknown), arg.Exception);
        }

        activity.checkedSessions.Clear();
        Reload();
        adapter.NotifyDataSetChanged();
        foreach (var sr in adapter.GetCheckedSessions()) {
          activity.checkedSessions.Add(sr._id);
        }

        pd.Dismiss();
      }, TaskScheduler.FromCurrentSynchronizationContext());
    }
    
    private void StartExportActivity() {
      if (activity.checkedSessions.Count <= 0) {
        Alert(Resource.String.report_error_graph_no_sessions_provided);
        return;
      }
      var i = new Intent(Activity, typeof(ReportGraphActivity));
      i.PutExtra(ReportGraphActivity.EXTRA_SESSIONS, new List<int>(activity.checkedSessions).ToArray());
      Activity.StartActivityForResult(i, ReportActivity.REQUEST_REPORT);
    }
  }
}
