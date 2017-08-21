using Javax.Microedition.Khronos.Egl;

namespace ION.Droid.Activity.Job {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading;
  using System.Threading.Tasks;

  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.Graphics;
  using Android.OS;
  using Android.Support.V4.View;
  using Android.Support.V13.App;
  using Android.Runtime;
  using Android.Views;
  using Android.Widget;

	using Appion.Commons.Util;

  using ION.Core.Database;

	[Activity(Label = "@string/job_edit", Icon="@drawable/ic_job", Theme = "@style/AppTheme", LaunchMode=Android.Content.PM.LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
  public class EditJobActivity : IONActivity, ViewPager.IOnPageChangeListener {

    public const string EXTRA_JOB_ID = "ION.Droid.extra.job_id";

    private Button info;
    private Button data;

    private ViewPager pager;
    private JobFragmentAdapter adapter;

    private JobRow job;

    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.activity_job_edit);

			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetHomeButtonEnabled(true);
			ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_job, Resource.Color.gray));

      pager = FindViewById<ViewPager>(Resource.Id.content);

      info = FindViewById<Button>(Resource.Id.info);
      data = FindViewById<Button>(Resource.Id.data);
      
      info.Click += (s, e) => {
        pager.SetCurrentItem(0, true);
      };
      
      data.Click += (s, e) => {
        pager.SetCurrentItem(1, true);
      };


      adapter = new JobFragmentAdapter(FragmentManager);
      pager.Adapter = adapter;
      pager.AddOnPageChangeListener(this);

      pager.SetCurrentItem(0, false);
    }

    protected override void OnResume() {
      base.OnResume();
      LoadJobAsync();
			pager.SetCurrentItem(0, false);
    }

    public override bool OnCreateOptionsMenu(IMenu menu) {
      base.OnCreateOptionsMenu(menu);

      MenuInflater.Inflate(Resource.Menu.save, menu);

      return true;
    }

    public override bool OnPrepareOptionsMenu(IMenu menu) {
      base.OnPrepareOptionsMenu(menu);

      var save = menu.FindItem(Resource.Id.save);
      save.ActionView.Click += (sender, e) => {
        SaveJobAsync();
      };

      return true;
    }

    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Android.Resource.Id.Home:
          SetResult(Result.Canceled);
          Finish();
          return true;
        case Resource.Id.save:
          SaveJobAsync();
          return true;
        default: 
          return base.OnMenuItemSelected(featureId, item);
      }
    }

    public void OnPageScrollStateChanged(int state) {
      
    }

    public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixel) {
    }

    public void OnPageSelected(int position) {
      switch (position) {
        case 0:
          info.SetBackgroundResource(Resource.Drawable.xml_tab_white_light_blue);
          data.SetBackgroundResource(Resource.Drawable.xml_tab_gray_black);
          break;
        case 1:
          info.SetBackgroundResource(Resource.Drawable.xml_tab_gray_black);
          data.SetBackgroundResource(Resource.Drawable.xml_tab_white_light_blue);
					HideKeyboard();
          break;
      }
    }
    
    /// <summary>
    /// Attempts to pull the job from the activity's Intent. If a job is not
    /// present, we will simply create a new one.
    /// </summary>
    /// <returns>The job async.</returns>
    private Task LoadJobAsync() {
      var progress = new ProgressDialog(this);
      progress.SetTitle(Resource.String.please_wait);
      progress.SetMessage(GetString(Resource.String.job_loading));
      progress.Indeterminate = true;
      progress.SetCancelable(false);
      progress.Show();

      return Task.Factory.StartNew(() => {
        var delayTask = Task.Delay(TimeSpan.FromSeconds(0.5));
      
        try {
          if (Intent.HasExtra(EXTRA_JOB_ID)) {
            var task = ion.database.QueryForAsync<JobRow>(Intent.GetIntExtra(EXTRA_JOB_ID, -1));
            if (task.IsCompleted && !(task.IsCanceled || task.IsFaulted)) {
              job = task.Result;
            }
          }
        } catch (Exception e) {
          Log.E(this, "Failed to do something", e);
        }
          
        if (job == null) {
          job = new JobRow();
        }
        
        var _ = adapter.LoadAsync(job).Result;
      }).ContinueWith((result) => {
        progress.Dismiss();
        adapter.Present(job);
      }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private async Task SaveJobAsync() {
			var progress = new ProgressDialog(this);
			progress.SetTitle(Resource.String.saving);
			progress.SetMessage(GetString(Resource.String.please_wait));
			progress.Show();

			var success = await adapter.SaveAsync(job);

			if (success) {
				Alert(Resource.String.job_saved);
			} else {
				Error(GetString(Resource.String.job_error_failed_to_save));
			}

			progress.Dismiss();
    }

    private class JobFragmentAdapter : FragmentStatePagerAdapter {
      public override int Count {
        get {
          return fragments.Length;
        }
      }
      
      public EditJobFragment edit { get; private set; }
      public EditJobSessionsFragment sessions { get; private set; }

      private Fragment[] fragments;

      public JobFragmentAdapter(FragmentManager fm) : base(fm) {
        fragments = new Fragment[] { 
          edit = new EditJobFragment(),
          sessions = new EditJobSessionsFragment(),
        };
      }

      public override Fragment GetItem(int position) {
        return fragments[position];
      }

      public void Present(JobRow job) {
        foreach (var presenter in Presenters()) {
          try {
            presenter.Present(job);
          } catch (Exception e) {
            Log.E(this, "Failed to present display for job manager", e);
          }
        }
      }
      
      public async Task<bool> LoadAsync(JobRow job) {
        foreach (var presenter in Presenters()) {
          if (!await presenter.LoadAsync(job)) {
            return false;
          }
        }

        return true;
      }

      public async Task<bool> SaveAsync(JobRow job) {
        foreach (var presenter in Presenters()) {
          if (!await presenter.SaveAsync(job)) {
            return false;
          }
        }

        return true;
      }

      private List<IJobPresenter> Presenters() {
        var ret = new List<IJobPresenter>();

        foreach (var f in fragments) {
          var presenter = f as IJobPresenter;
          if (presenter != null) {
            ret.Add(presenter);
          }
        }

        return ret;
      }
    }
  }

  public interface IJobPresenter {
    Task<bool> LoadAsync(JobRow job);
    Task<bool> SaveAsync(JobRow job);
    void Present(JobRow job);
  }
}

