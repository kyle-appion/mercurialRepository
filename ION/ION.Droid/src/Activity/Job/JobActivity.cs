using ION.Droid.Views;

namespace ION.Droid.Activity.Job {

  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.Graphics;
  using Android.OS;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Database;

  [Activity(Label = "@string/jobs_saved", Icon="@drawable/ic_job", Theme = "@style/AppTheme", LaunchMode=Android.Content.PM.LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
  public class JobActivity : IONActivity {

    private const int REQUEST_CREATE_JOB = 1;

    private View activeJobView;
    private View emptyJobView;

    private RecyclerView list;
    private JobAdapter adapter;

    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.activity_job);

      ActionBar.SetDisplayHomeAsUpEnabled(true);
      ActionBar.SetHomeButtonEnabled(true);
			ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_job, Resource.Color.gray));

      activeJobView = FindViewById(Resource.Id.active);
      emptyJobView = FindViewById(Resource.Id.text);
      list = FindViewById<RecyclerView>(Resource.Id.list);
      adapter = new JobAdapter(ion);
      adapter.onItemClicked += (position) => {
        var i = new Intent(this, typeof(EditJobActivity));
        i.PutExtra(EditJobActivity.EXTRA_JOB_ID, ((JobRecord)adapter[position]).data._id);
        StartActivity(i);
      };
      adapter.onFavoriteClicked += (job) => {
        ToggleActiveJob(job);
      };
      list.SetAdapter(adapter);
      adapter.emptyView = FindViewById(Resource.Id.empty);
      activeJobView.Click += (s, e) => {
        var i = new Intent(this, typeof(EditJobActivity));
        i.PutExtra(EditJobActivity.EXTRA_JOB_ID, ion.preferences.job.activeJob);
        StartActivity(i);
      };
      
      RemoveActiveJob();
    }

    protected override void OnResume() {
      base.OnResume();

      LoadJobsAsync();
      ion.database.onDatabaseEvent += OnDatabaseEvent;
    }
    
    protected override void OnPause() {
      base.OnPause();
      ion.database.onDatabaseEvent -= OnDatabaseEvent;
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

    /// <summary>
    /// If the given job is the current active job, we will request to remove it.
    /// </summary>
    /// <param name="???"></param>
    private void ToggleActiveJob(JobRow job) {
      if (ion.preferences.job.activeJob == job._id) {
        RemoveActiveJob();
      } else {
        MarkActiveJob(job);
      }
    }

    private void MarkActiveJob(JobRow job) {
      ion.preferences.job.activeJob = job._id;
      activeJobView.Visibility = ViewStates.Visible;
      emptyJobView.Visibility = ViewStates.Gone;

      var id = activeJobView.FindViewById<TextView>(Resource.Id.id);
      var name = activeJobView.FindViewById<TextView>(Resource.Id.name);
      var customer = activeJobView.FindViewById<TextView>(Resource.Id.customer_no);
      var dispatch = activeJobView.FindViewById<TextView>(Resource.Id.dispatch_no);
      var purchase = activeJobView.FindViewById<TextView>(Resource.Id.purchase_no);
      var favorite = activeJobView.FindViewById<ImageView>(Resource.Id.check);

      id.Text = job._id + "";
      name.Text = job.jobName;
      customer.Text = job.customerNumber;
      dispatch.Text = job.dispatchNumber;
      purchase.Text = job.poNumber;
      favorite.SetColorFilter(Resource.Color.gold.AsResourceColor(this), PorterDuff.Mode.SrcAtop);
      favorite.SetOnClickListener(new ViewClickAction((v) => {
        ToggleActiveJob(job);
      }));
      
      adapter.NotifyDataSetChanged();
    }

    private void RemoveActiveJob() {
      ion.preferences.job.activeJob = 0;
	    activeJobView.Visibility = ViewStates.Gone;
      emptyJobView.Visibility = ViewStates.Visible;
      adapter.NotifyDataSetChanged();
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
        
        if (ion.preferences.job.activeJob != 0) {
          var active = ion.database.QueryForAsync<JobRow>(ion.preferences.job.activeJob).Result;
          ion.PostToMain(() => { MarkActiveJob(active); });
        }

        ion.PostToMainDelayed(() => {
					adapter.SetJobs(records);
          progress.Dismiss();
        }, TimeSpan.FromSeconds(.5));
      });
    }
    
    private void OnDatabaseEvent(IONDatabase db, DatabaseEvent e) {
      switch (e.action) {
        case DatabaseEvent.EAction.Deleted:
          if (e.table != typeof(JobRow)) {
            return;
          }
          
          if ((int)e.id == ion.preferences.job.activeJob) {
            RemoveActiveJob();
          }
          break;
      }
    }
  }
}
