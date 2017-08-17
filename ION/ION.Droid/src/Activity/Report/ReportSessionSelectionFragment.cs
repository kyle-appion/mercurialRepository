﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

using Appion.Commons.Util;

using ION.Core.Database;

using ION.Droid.Dialog;
using ION.Droid.Fragments;
using ION.Droid.Widgets.RecyclerViews;

namespace ION.Droid.Activity.Report {
  public class ReportSessionSelectionFragment : IONFragment {

    private SwipeRecyclerView list;
    private View delete;

    private ReportSessionAdapter adapter;
    private Action reloadAction;

    private HashSet<int> checkedSessions = new HashSet<int>();

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
      var ret = inflater.Inflate(Resource.Layout.fragment_report_session_selection, container, false);

      list = ret.FindViewById<SwipeRecyclerView>(Resource.Id.list);
      delete = ret.FindViewById(Resource.Id.delete);
      var build = ret.FindViewById(Resource.Id.export);

      delete.Visibility = ViewStates.Invisible;
      delete.Click += (sender, e) => {
        RequestDeleteSessions(adapter.GetCheckedSessions());
      };

      ret.FindViewById(Resource.Id.report_by_job).Click += (sender, e) => {
        reloadAction = ReloadSessionsByJob;
        reloadAction();
      };

      ret.FindViewById(Resource.Id.report_by_date).Click += (sender, e) => {
        reloadAction = ReloadSessionsByDate;
        reloadAction();
      };

      ret.FindViewById(Resource.Id.export).Click += (sender, e) => {
        StartExportActivity();
      };


			adapter = new ReportSessionAdapter(list);
			adapter.emptyView = ret.FindViewById(Resource.Id.empty);
      adapter.onJobRowClicked = ShowJobContextDialog;
			adapter.onSessionCheckChanged = (sr) => {
        if (sr.isChecked) {
          checkedSessions.Add(sr.data._id);
        } else {
          checkedSessions.Remove(sr.data._id);
        }
        InvalidateDelete();
			};
			list.SetAdapter(adapter);

			return ret;
    }

    public override void OnResume() {
      base.OnResume();
      if (reloadAction == null) {
        reloadAction = ReloadSessionsByJob;
      }

      reloadAction();
    }

    private void SyncCheckedSessions() {
      foreach (var id in checkedSessions) {
        adapter.SetCheckStatusForSessionRow(id, true);
      }
    }

    private void InvalidateDelete() {
      if (checkedSessions.Count > 0) {
        delete.Visibility = ViewStates.Visible;
      } else {
				delete.Visibility = ViewStates.Invisible;
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

        if (reloadAction != null) {
          reloadAction();
        }

        pd.Dismiss();
      }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void ReloadSessionsByJob() {
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

    private void ReloadSessionsByDate() {
      var progress = new ProgressDialog(Activity);
      progress.SetTitle(Resource.String.please_wait);
      progress.SetMessage(GetString(Resource.String.loading));
      progress.Indeterminate = true;
      progress.Show();

      LoadAllSessionsAsync().ContinueWith((arg) => {
        if (arg.IsFaulted) {
          Error(GetString(Resource.String.report_error_failed_to_load_sessions), arg.Exception);
        } else {
          adapter.Set(arg.Result);
        }
        progress.Dismiss();
        InvalidateDelete();
        SyncCheckedSessions();
      }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private async Task<List<ReportSessionRecord>> LoadAllSessionsAsync() {
			var ret = new List<ReportSessionRecord>();

			var rows = ion.database.Table<SessionRow>().AsEnumerable();
			foreach (var row in rows) {
				try {
					var record = await ReportSessionRecord.NewAsync(ion, row);
					ret.Add(record);
				} catch (Exception e) {
					Log.E(this, "Failed to load row: " + row, e);
				}
			}

			return ret;
		}

    private void StartExportActivity() {
      var i = new Intent(Activity, typeof(ReportGraphActivity));
      i.PutExtra(ReportGraphActivity.EXTRA_SESSIONS, new List<int>(checkedSessions).ToArray());
      StartActivity(i);
    }
  }
}
