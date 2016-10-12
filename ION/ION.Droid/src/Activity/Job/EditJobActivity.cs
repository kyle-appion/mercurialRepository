namespace ION.Droid.Activity.Job {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
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

  using ION.Core.Util;
  using ION.Core.Database;

  [Activity(Label = "EditJobActivityTheme", Theme = "@style/TerminalActivityTheme", LaunchMode=Android.Content.PM.LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
  public class EditJobActivity : IONActivity, ViewPager.IOnPageChangeListener {

    public const string EXTRA_JOB_ID = "ION.Droid.extra.job_id";

    private NavButton info;
    private NavButton sessions;

    private ViewPager pager;
    private JobFragmentAdapter adapter;

    private JobRow job;

    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.activity_edit_job);

			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetHomeButtonEnabled(true);

      pager = FindViewById<ViewPager>(Resource.Id.content);

      info = new NavButton(FindViewById(Resource.Id.edit), () => {
        pager.SetCurrentItem(0, true);

      });

      sessions = new NavButton(FindViewById(Resource.Id.report_sessions), () => {
        pager.SetCurrentItem(1, true);
      });


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
          info.SetColor(new Color(GetColor(Resource.Color.black)));
          sessions.SetColor(new Color(GetColor(Resource.Color.gray)));
           break;
        case 1:
          info.SetColor(new Color(GetColor(Resource.Color.gray)));
          sessions.SetColor(new Color(GetColor(Resource.Color.black)));
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
      progress.Show();

      return Task.Factory.StartNew(() => {
        try {
          if (Intent.HasExtra(EXTRA_JOB_ID)) {
            var task = ion.database.QueryForAsync<JobRow>(Intent.GetIntExtra(EXTRA_JOB_ID, -1));
            task.Wait();
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

        ion.PostToMainDelayed(() => {
          progress.Dismiss();
          adapter.Present(job);
        }, TimeSpan.FromSeconds(1));
      });
    }

    private async Task SaveJobAsync() {
			var progress = new ProgressDialog(this);
			progress.SetTitle(Resource.String.saving);
			progress.SetMessage(GetString(Resource.String.please_wait));
			progress.Show();

			var success = await adapter.SaveAsync(job);

			ion.PostToMain(() => {
				if (success) {
					Alert(Resource.String.job_saved);
				} else {
					Error(GetString(Resource.String.job_error_failed_to_save));
				}

				progress.Dismiss();
			});
    }

    private class JobFragmentAdapter : FragmentStatePagerAdapter {
      public override int Count {
        get {
          return fragments.Length;
        }
      }

      private Fragment[] fragments;

      public JobFragmentAdapter(FragmentManager fm) : base(fm) {
        fragments = new Fragment[] { 
          new EditJobFragment(),
          new EditJobSessionsFragment(),
        };
      }

      public override Fragment GetItem(int position) {
        return fragments[position];
      }

      public void Present(JobRow job) {
        foreach (var presenter in Presenters()) {
          presenter.Present(job);
        }
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


    private class NavButton {
      ImageView icon;
      TextView text;

      public NavButton(View contentView, Action clickAction) {
        icon = contentView.FindViewById<ImageView>(Resource.Id.icon);
        text = contentView.FindViewById<TextView>(Resource.Id.text);
        contentView.Click += (sender, e) => {
          clickAction();
        };
      }

      public void SetColor(Color color) {
        icon.SetColorFilter(color);
        text.SetTextColor(color);
      }
    }
  }

  public interface IJobPresenter {
    void Present(JobRow job);
    Task<bool> SaveAsync(JobRow job);
    
  }
}

