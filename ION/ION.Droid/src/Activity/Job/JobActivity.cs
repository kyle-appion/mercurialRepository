namespace ION.Droid.Activity.Job {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Runtime;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Database;

	// Using ION.Droid
	using Widgets.Adapters.Job;
  using Widgets.RecyclerViews;

  [Activity(Label = "JobActivity", Theme = "@style/AppTheme", LaunchMode=Android.Content.PM.LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
  public class JobActivity : IONActivity {

    private const int REQUEST_CREATE_JOB = 1;

    private RecyclerView list;
    private JobAdapter adapter;

    /// <summary>
    /// Whether or not the activity is currently loading a job.
    /// </summary>
    private bool loadingJobs;

    protected override void OnCreate(Bundle savedInstanceState) {
      RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.activity_job);

      ActionBar.SetDisplayHomeAsUpEnabled(true);
      ActionBar.SetHomeButtonEnabled(true);

      list = FindViewById<RecyclerView>(Resource.Id.list);
      adapter = new JobAdapter(ion);
      adapter.onItemClicked += (adapter, position) => {
        var i = new Intent(this, typeof(EditJobActivity));
        i.PutExtra(EditJobActivity.EXTRA_JOB_ID, ((JobRecord)adapter.GetRecordAt(position)).row._id);
        StartActivity(i);
      };
      list.SetAdapter(adapter);
    }

    protected override void OnResume() {
      base.OnResume();

      LoadJobsAsync();
    }

    public override bool OnCreateOptionsMenu(IMenu menu) {
      base.OnCreateOptionsMenu(menu);

      MenuInflater.Inflate(Resource.Menu.plus, menu);

      return true;
    }

    public override bool OnPrepareOptionsMenu(IMenu menu) {
      base.OnPrepareOptionsMenu(menu);

      var newJob = menu.FindItem(Resource.Id.plus);
      var view = newJob.ActionView;
      view.Click += (sender, e) => {
        StartActivityForResult(typeof(EditJobActivity), REQUEST_CREATE_JOB);
      };

      if (loadingJobs) {
        SetProgressBarIndeterminateVisibility(true);
      } else {
        SetProgressBarIndeterminateVisibility(false);
      }

      return true;
    }

    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Android.Resource.Id.Home:
          SetResult(Result.Canceled);
          Finish();
          return true;
        case Resource.Id.plus:
          StartActivityForResult(typeof(EditJobActivity), REQUEST_CREATE_JOB);
          return true;
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
    }

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
      switch (requestCode) {
        case REQUEST_CREATE_JOB:
          if (resultCode == Result.Canceled) {
            return;
          }

        break;
      }
    }

    private Task LoadJobsAsync() {
      var progress = new ProgressDialog(this);
      progress.SetTitle(Resource.String.please_wait);
      progress.SetMessage(GetString(Resource.String.jobs_loading));
      progress.Show();

      return Task.Factory.StartNew(() => {
        var jobs = ion.database.QueryForAllAsync<JobRow>().Result;
        var records = new List<JobRecord>();

        foreach (var job in jobs) {
          records.Add(new JobRecord(job));
        }

        ion.PostToMainDelayed(() => {
					adapter.SetJobs(records);
          progress.Dismiss();
        }, TimeSpan.FromSeconds(.5));
      });
    }
  }
}

