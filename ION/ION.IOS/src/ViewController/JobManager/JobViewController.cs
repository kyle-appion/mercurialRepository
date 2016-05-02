using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.IOS.ViewController;
using ION.Core.App;

namespace ION.IOS.ViewController.JobManager {
  public partial class JobViewController : BaseIONViewController {

    ManageJobView manageJobs;
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
        Console.WriteLine("Was not pushed");
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
      manageJobs = new ManageJobView(View,jobList,this);

      View.AddSubview(manageJobs.mjView);
    }

    public override void ViewDidAppear(bool animated) {
      var jobQuery = ion.database.Query<ION.Core.Database.JobRow>("SELECT DISTINCT JID FROM JobRow ORDER BY JID DESC");
      var jobList = new List<int>();
      foreach (var id in jobQuery) {
        jobList.Add(id.JID);
      }
      manageJobs.currentJobs.Source = new CreatedJobSource(jobList,this);
      manageJobs.currentJobs.ReloadData();
    }
    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


