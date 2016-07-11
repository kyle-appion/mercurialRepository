namespace ION.Droid.Activity.Report {
	
	using System;
  using System.Collections.Generic;

	using Android.App;
	using Android.OS;
  using Android.Support.V7.Widget;
	using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Database;

  using ION.Droid.Fragments;
  using ION.Droid.Widgets.RecyclerViews;

	/// <summary>
	/// The fragment that is responsible for displaying the views related to selecting reports to graph and export or view.
	/// </summary>
	public class NewSavedReportFragment : IONFragment {

    private NewReports newReports;
    private SavedReports savedReports;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			var ret = inflater.Inflate(Resource.Layout.fragment_report_new_saved, container, false);

      ret.FindViewById(Resource.Id.report_new).Click += (sender, e) => {
        ShowNewReports();
      };

      ret.FindViewById(Resource.Id.report_saved).Click += (sender, e) => {
        ShowSavedReports();
      };

      newReports = new NewReports(AppState.context, ret.FindViewById(Resource.Id.report_session_selection));
      savedReports = new SavedReports(AppState.context, ret.FindViewById(Resource.Id.report_saved));

			return ret;
		}

		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated(savedInstanceState);

      ShowNewReports();
		}

    private void ShowNewReports() {
      newReports.content.Visibility = ViewStates.Gone;
      savedReports.content.Visibility = ViewStates.Visible;
    }

    private void ShowSavedReports() {
      newReports.content.Visibility = ViewStates.Visible;
      savedReports.content.Visibility = ViewStates.Gone;
    }

    class NewReports {
      private IION ion;
      public View content;
      private Button byJob;
      private Button byDate;

      private RecyclerView list;

      private View newJob;
      private View emptySessions;

      private JobAdapter jobAdapter;
      private SessionAdapter sessionAdapter;

      public NewReports(IION ion, View content) {
        this.ion = ion;
        this.content = content;

        list = content.FindViewById<RecyclerView>(Resource.Id.list);
        content.FindViewById(Resource.Id.report_by_job).Click += (sender, e) => {
          SwitchToByJob();
        };
        content.FindViewById(Resource.Id.report_by_date).Click += (sender, e) => {
          SwitchToByDate();
        };

        newJob = content.FindViewById(Resource.Id.button);
        emptySessions = content.FindViewById(Resource.Id.empty);

        jobAdapter = new JobAdapter();
        sessionAdapter = new SessionAdapter();

        SwitchToByJob();
      }

      public void SwitchToByJob() {
        emptySessions.Visibility = ViewStates.Gone;

        // TODO ahodder@appioninc.com: Make this more generic such that future changes to the database don't shatter reporting.
        var jobs = ion.database.Query<JobRow>("SELECT * FROM JobRow");

        var jl = new List<JobRecord>();

        foreach (var job in jobs) {
          var jr = new JobRecord(job);
          jl.Add(jr);
          var l = new List<SessionRecord>();
          foreach (var session in ion.database.Query<SessionRow>("SELECT * FROM SessionRow WHERE frn_JID = " + job._id)) {
            l.Add(new SessionRecord(session));
          }
          jr.sessions = l;
        }

        jobAdapter.SetJobs(jl);
        list.SetAdapter(jobAdapter);

        if (jobAdapter.ItemCount > 0) {
          newJob.Visibility = ViewStates.Gone;
          list.Visibility = ViewStates.Visible;
        } else {
          newJob.Visibility = ViewStates.Visible;
          list.Visibility = ViewStates.Gone;
        }
      }

      public void SwitchToByDate() {
        newJob.Visibility = ViewStates.Gone;

        var sl = new List<SessionRecord>();

        // TODO ahodder@appioninc.com: Make this more generic such that future changes to the database don't shatter reporting.
        foreach (var sr in ion.database.Query<SessionRow>("SELECT * FROM SessionRow")) {
          sl.Add(new SessionRecord(sr));
        }

        sessionAdapter.SetSessions(sl);
        list.SetAdapter(sessionAdapter);

        if (jobAdapter.ItemCount > 0) {
          emptySessions.Visibility = ViewStates.Gone;
          list.Visibility = ViewStates.Visible;
        } else {
          emptySessions.Visibility = ViewStates.Visible;
          list.Visibility = ViewStates.Gone;
        }
      }
    }

    class SavedReports {
      private IION ion;
      public View content;
      private Button byJob;
      private Button byDate;

      private RecyclerView list;

      private View emptySpreadsheets;
      private View emptyPdfs;

      public SavedReports(IION ion, View content) {
        this.ion = ion;
        this.content = content;

        list = content.FindViewById<RecyclerView>(Resource.Id.list);
/*
        content.FindViewById(Resource.Id.NULL).Click += (sender, e) => {
        };
        content.FindViewById(Resource.Id.report_by_date).Click += (sender, e) => {
        };
*/

        emptySpreadsheets = content.FindViewById(Resource.Id.button);
        emptyPdfs = content.FindViewById(Resource.Id.empty);
      }
    }
	}
}

