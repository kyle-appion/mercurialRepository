namespace ION.Droid.Activity.Job {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  using Android.App;
  using Android.OS;
	using Android.Support.V7.Widget;
  using Android.Views;

	using Appion.Commons.Util;

  // Using ION
  using Core.Database;

  // Using ION.Droid
  using Fragments;
  using Dialog;

  public class EditJobSessionsFragment : IONFragment, IJobPresenter {


    private View current;
    private View available;
    
		private View removeButton;
		private View addButton;

		private RecyclerView currentList;
    private View currentEmptyView;
		private RecyclerView availableList;
    private View availableEmptyView;

		private SessionAdapter currentAdapter;
		private SessionAdapter availableAdapter;
    
    private List<SessionRow> currentSessions;
    private List<SessionRow> availableSessions;

		private JobRow job;

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
      var ret = inflater.Inflate(Resource.Layout.fragment_job_edit_sessions, container, false);

		  current = ret.FindViewById(Resource.Id.remove);
			currentList = current.FindViewById<RecyclerView>(Resource.Id.list);
			removeButton = current.FindViewById(Resource.Id.button);
			removeButton.Click += (sender, e) => {
				var success = RemoveSelected();

				if (success) {
					Alert(GetString(Resource.String.job_saved));
          InternalRefreshSessions();
				} else {
					Error(GetString(Resource.String.job_error_failed_to_save));
				}
			};

			available = ret.FindViewById(Resource.Id.add);
			availableList = available.FindViewById<RecyclerView>(Resource.Id.list);
			addButton = available.FindViewById(Resource.Id.button);
			addButton.Click += (sender, e) => {
        var checkedSessions = availableAdapter.GetCheckedSessions();
        if (checkedSessions.Where(x => x.data.frn_JID != 0).Count() > 0) {
          var dialog = new IONAlertDialog(Activity);
          // TODO ahodder@appioninc.com: Check this this verbage is ok
          dialog.SetTitle(Resource.String.job_link_break_title);
          dialog.SetMessage(GetString(Resource.String.job_link_ensure_break_request));
          dialog.SetNegativeButton(Resource.String.cancel, (s2, e2) => {});
          dialog.SetPositiveButton(Resource.String.job_link, (s2, e2) => {
            AddSelected();
          });
          dialog.Show();
        } else {
          AddSelected();
        }
			};

      return ret;
    }

		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated(savedInstanceState);

			currentAdapter = new SessionAdapter(ion);
      currentAdapter.emptyView = current.FindViewById(Resource.Id.empty);
			currentAdapter.onSessionRowChecked += (sender, e) => {
				UpdateButtons();
			};

			availableAdapter = new SessionAdapter(ion);
      availableAdapter.emptyView = available.FindViewById(Resource.Id.empty);
			availableAdapter.onSessionRowChecked += (sender, e) => {
					UpdateButtons();
			};

			currentList.SetAdapter(currentAdapter);
			availableList.SetAdapter(availableAdapter);
		}

		public override void OnResume() {
			base.OnResume();
			UpdateButtons();
		}

    public void Present(JobRow job) {
      currentAdapter.jobRow = job;
      currentAdapter.SetSessions(currentSessions);

      availableAdapter.jobRow = job;
      availableAdapter.SetSessions(availableSessions);
      
      UpdateButtons();
    }
    
    public async Task<bool> LoadAsync(JobRow job) {
      try {
        this.job = job;
        currentSessions = await LoadCurrentSessionsAsync();
        availableSessions = await LoadAvailableSessionsAsync();
      } catch (Exception e) {
        Log.E(this, "ERROR", e);
      }
      
      return true;
    }

    public Task<bool> SaveAsync(JobRow job) {
      return Task.FromResult(true);
    }
    
    private Task InternalRefreshSessions() {
      var progress = new ProgressDialog(Activity);
      progress.SetTitle(Resource.String.saving);
      progress.SetMessage(GetString(Resource.String.job_adding_sessions));
      progress.Show();
      
      return Task.Factory.StartNew(() => LoadAsync(job))
                          .ContinueWith((result) => {
                            Present(job);
                            progress.Dismiss();
                          }, TaskScheduler.FromCurrentSynchronizationContext());
    }

		private Task<List<SessionRow>> LoadCurrentSessionsAsync() {
      return Task.Factory.StartNew(() => {
        var table = ion.database.Table<SessionRow>();
        var rows = table.Where(sr => sr.frn_JID == job._id && job._id != 0);
        var ret = new List<SessionRow>();
  
        foreach (var r in rows) {
          if (r._id == ion.dataLogManager.currentSessionId) {
            continue;
          }
          ret.Add(r);
        }
        return ret;
      });
		}

		private Task<List<SessionRow>> LoadAvailableSessionsAsync() {
      return Task.Factory.StartNew(() => {
  			var table = ion.database.Table<SessionRow>();
  			var rows = table.Where(sr => job._id == 0 || sr.frn_JID != job._id);
  			var ret = new List<SessionRow>();
  
  			foreach (var r in rows) {
  				if (r._id == ion.dataLogManager.currentSessionId) {
  					continue;
  				}
  				ret.Add(r);
  			}
        
        return ret;
      });
		}

		private bool RemoveSelected() {
			var sessions = new List<SessionRow>();

			foreach (var sr in currentAdapter.GetCheckedSessions()) {
				sr.data.frn_JID = 0;
				sessions.Add(sr.data);
			}

			try {
				var table = ion.database.Table<SessionRow>();
				ion.database.UpdateAll(sessions, false);

				ion.PostToMain(() => {
					UpdateButtons();
				});
				return true;
			} catch (Exception e) {
				Log.E(this, "Failed to remove selected sessions from job", e);
				return false;
			}
		}

		private Task AddSelected() {
      return Task.Factory.StartNew(() => {
        var sessions = new List<SessionRow>();
        
        foreach (var sr in availableAdapter.GetCheckedSessions()) {
          sr.data.frn_JID = job._id;
          sessions.Add(sr.data);
        }
  
        var success = false;
        try {
          var table = ion.database.Table<SessionRow>();
          ion.database.UpdateAll(sessions, true);
  
          success = true;
        } catch (Exception e) {
          Log.E(this, "Failed to add selected sessions to job", e);
          success = false;
        }
        
        if (success) {
          Alert(GetString(Resource.String.job_saved));
        } else {
          Error(GetString(Resource.String.job_error_failed_to_save));
        }
      }).ContinueWith((arg) => {
        ion.PostToMainDelayed(() => {
          InternalRefreshSessions();
        }, TimeSpan.FromSeconds(0.3));
      }, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private void UpdateButtons() {
			if (currentAdapter.GetCheckedSessions().Count <= 0) {
				removeButton.Visibility = ViewStates.Invisible;
			} else {
				removeButton.Visibility = ViewStates.Visible;
			}

			if (availableAdapter.GetCheckedSessions().Count <= 0) {
				addButton.Visibility = ViewStates.Invisible;
			} else {
				addButton.Visibility = ViewStates.Visible;
			}
		}
  }
}

