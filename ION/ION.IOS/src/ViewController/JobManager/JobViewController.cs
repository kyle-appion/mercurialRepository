using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.IOS.ViewController;
using ION.Core.App;
using System.Globalization;

namespace ION.IOS.ViewController.JobManager {
  public partial class JobViewController : BaseIONViewController {

    public UIRefreshControl jobRefresh;
    IION ion;
    public bool pushed = false;

    public JobViewController(IntPtr handle): base(handle){
    }

    public override void ViewDidLoad() {
      base.ViewDidLoad();

      ion = AppState.context;
      // Perform any additional setup after loading the view, typically from a nib.
      View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));
      AutomaticallyAdjustsScrollViewInsets = false;

      if (!pushed) {
        InitNavigationBar("ic_job_settings", false);
        backAction = () => { 
          root.navigation.ToggleMenu();
        };
      }

      Title = "Job Settings";
      var addJobButton = new UIButton(new CGRect(0,0,31,30));
      addJobButton.TouchUpInside += (sender, e) => {
        var jevc = this.InflateViewController<JobEditViewController>(BaseIONViewController.VC_EDIT_JOB);
        jevc.frnJID = 0;
        this.NavigationController.PushViewController(jevc,true);
      };
      addJobButton.SetImage(UIImage.FromBundle("ic_plus"), UIControlState.Normal);
      addJobButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      addJobButton.Layer.BorderWidth = 1f;
      var barButton = new UIBarButtonItem(addJobButton);
      NavigationItem.SetRightBarButtonItem(barButton,true);

      ion = AppState.context;
      var jobQuery = ion.database.Query<ION.Core.Database.JobRow>("SELECT DISTINCT JID FROM JobRow ORDER BY JID DESC");

      var jobList = new List<int>();
      foreach (var id in jobQuery) {
        jobList.Add(id.JID);
      }
      jobTable.Layer.CornerRadius = 5f;
      jobTable.Layer.BorderWidth = 1f;
      jobTable.RegisterClassForCellReuse(typeof(CreatedJobCell), "createdJobCell");
      jobTable.ClipsToBounds = true;
      jobTable.ContentInset = UIEdgeInsets.Zero;
      jobTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      jobTable.Source = new CreatedJobSource(jobList,this);
      jobTable.ReloadData();

      jobRefresh = new UIRefreshControl();
      jobRefresh.ValueChanged += (sender, e) => {
        refreshJobList(this);
      };

      jobTable.InsertSubview(jobRefresh,0);
      jobTable.SendSubviewToBack(jobRefresh);

    }
    
    public void refreshJobList(JobViewController manageVC){
      var jobs = ion.database.Query<ION.Core.Database.JobRow>("SELECT DISTINCT JID FROM JobRow ORDER BY JID DESC");

      var jobList = new List<int>();
      foreach (var job in jobs) {
        jobList.Add(job.JID);
      }

      jobTable.Source = new CreatedJobSource(jobList, this);
      jobTable.ReloadData();
      jobRefresh.EndRefreshing();
    }
    
    public override void ViewDidAppear(bool animated) {
      var jobQuery = ion.database.Query<ION.Core.Database.JobRow>("SELECT DISTINCT JID FROM JobRow ORDER BY JID DESC");
      var jobList = new List<int>();
      foreach (var id in jobQuery) {
        jobList.Add(id.JID);
      }
      jobTable.Source = new CreatedJobSource(jobList,this);
      jobTable.ReloadData();
    }
    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


