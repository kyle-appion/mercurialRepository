namespace ION.Droid.Activity.Job {

  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Android.App;
  using Android.OS;
	using Android.Support.V7.Widget;
  using Android.Views;

  // Using ION
  using Core.Database;

  // Using ION.Droid
  using Fragments;
	using Widgets.Adapters.Job;

  public class EditJobSessionsFragment : IONFragment, IJobPresenter {


		private View removeButton;
		private View addButton;

		private RecyclerView currentList;
		private RecyclerView availableList;

		private SessionAdapter currentAdapter;
		private SessionAdapter availableAdapter;

		private JobRow job;

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
      var ret = inflater.Inflate(Resource.Layout.fragment_edit_job_sessions, container, false);

			var remove = ret.FindViewById(Resource.Id.remove);
			currentList = remove.FindViewById<RecyclerView>(Resource.Id.list);
			removeButton = remove.FindViewById(Resource.Id.button);
			removeButton.Click += (sender, e) => {
				var progress = new ProgressDialog(Activity);
				progress.SetTitle(Resource.String.saving);
				progress.SetMessage(GetString(Resource.String.job_removing_sessions));
				progress.Show();

				Task.Factory.StartNew(() => {
					var success = RemoveSelected();

					ion.PostToMain(() => {
						if (success) {
							Alert(GetString(Resource.String.job_saved));
							LoadAsync();
						} else {
							Error(GetString(Resource.String.job_error_failed_to_save));
						}

						progress.Dismiss();
					});
				});
			};

			var add = ret.FindViewById(Resource.Id.add);
			availableList = add.FindViewById<RecyclerView>(Resource.Id.list);
			addButton = add.FindViewById(Resource.Id.button);
			addButton.Click += (sender, e) => {
				var progress = new ProgressDialog(Activity);
				progress.SetTitle(Resource.String.saving);
				progress.SetMessage(GetString(Resource.String.job_adding_sessions));
				progress.Show();

				Task.Factory.StartNew(() => {
					var success = AddSelected();

					ion.PostToMain(() => {
						if (success) {
							Alert(GetString(Resource.String.job_saved));
							LoadAsync();
						} else {
							Error(GetString(Resource.String.job_error_failed_to_save));
						}

						progress.Dismiss();
					});
				});
			};

      return ret;
    }

		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated(savedInstanceState);

			currentAdapter = new SessionAdapter(ion);
			currentAdapter.onItemClicked += (adapter, position) => {
				var sr = currentAdapter.GetRecordAt(position) as SessionRecord;

				if (sr != null) {
					sr.isChecked = !sr.isChecked;
				}

				currentAdapter.NotifyItemChanged(position);

				UpdateButtons();
			};

			availableAdapter = new SessionAdapter(ion);
			availableAdapter.onItemClicked += (adapter, position) => {
				var sr = availableAdapter.GetRecordAt(position) as SessionRecord;

				if (sr != null) {
					sr.isChecked = !sr.isChecked;
				}

				availableAdapter.NotifyItemChanged(position);

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
			this.job = job;
			LoadAsync();
    }

    public Task<bool> SaveAsync(JobRow job) {
      return Task.FromResult(true);
    }

		private async Task LoadAsync() {
			var progress = new ProgressDialog(Activity);
			progress.SetTitle(Resource.String.please_wait);
			progress.SetMessage(GetString(Resource.String.job_loading));
			progress.Show();

			var current = await LoadCurrentSessionsAsync();
			var available = await LoadAvailableSessionsAsync();

			ion.PostToMain(() => {
				progress.Dismiss();
				currentAdapter.SetSessions(current);
				currentAdapter.jobRow = job;

				availableAdapter.SetSessions(available);
				availableAdapter.jobRow = job;
				UpdateButtons();
			});

		}

		private Task<List<SessionRow>> LoadCurrentSessionsAsync() {
			var table = ion.database.Table<SessionRow>();
			var rows = table.Where(sr => sr.frn_JID == job._id && job._id != 0);
			var ret = new List<SessionRow>();

			foreach (var r in rows) {
				if (r._id == ion.dataLogManager.currentSessionId) {
					continue;
				}
				ret.Add(r);
			}

			return Task.FromResult(ret);
		}

		private Task<List<SessionRow>> LoadAvailableSessionsAsync() {
			var table = ion.database.Table<SessionRow>();
			var rows = table.Where(sr => sr.frn_JID != job._id || job._id == 0);
			var ret = new List<SessionRow>();

			foreach (var r in rows) {
				if (r._id == ion.dataLogManager.currentSessionId) {
					continue;
				}
				ret.Add(r);
			}

			return Task.FromResult(ret);
		}

		private bool RemoveSelected() {
			var sessions = new List<SessionRow>();

			foreach (var sr in currentAdapter.GetCheckedSessions()) {
				sr.row.frn_JID = 0;
				sessions.Add(sr.row);
			}

			try {
				var table = ion.database.Table<SessionRow>();
				ion.database.UpdateAll(sessions, false);

				ion.PostToMain(() => {
					UpdateButtons();
				});
				return true;
			} catch (Exception e) {
				ION.Core.Util.Log.E(this, "Failed to remove selected sessions from job", e);
				return false;
			}
		}

		private bool AddSelected() {
			var sessions = new List<SessionRow>();

			foreach (var sr in availableAdapter.GetCheckedSessions()) {
				sr.row.frn_JID = job._id;
				sessions.Add(sr.row);
			}

			try {
				var table = ion.database.Table<SessionRow>();
				ion.database.UpdateAll(sessions, true);

				ion.PostToMain(() => {
					UpdateButtons();
				});
				return true;
			} catch (Exception e) {
				ION.Core.Util.Log.E(this, "Failed to add selected sessions to job", e);
				return false;
			}
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

