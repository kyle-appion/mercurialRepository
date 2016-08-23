using System;
using System.Collections.Generic;
using System.IO;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.Core.App;

namespace ION.IOS.ViewController.JobManager {
  public class ManageJobView {
    public UIView mjView;
    public UITableView currentJobs;
    public UIRefreshControl jobRefresh;
    IION ion;

    public ManageJobView(UIView parentView,List<int> jobID, JobViewController manageVC) {
      ion = AppState.context;

      mjView = new UIView(new CGRect(.01 * parentView.Bounds.Width, 60, .98 * parentView.Bounds.Width,  parentView.Bounds.Height - 100));
      mjView.BackgroundColor = UIColor.White;
      mjView.Layer.CornerRadius = 5;
      mjView.ClipsToBounds = true;

      currentJobs = new UITableView(new CGRect(0,0,mjView.Bounds.Width,mjView.Bounds.Height - 20));
      currentJobs.RegisterClassForCellReuse(typeof(CreatedJobCell), "createdJobCell");
      currentJobs.ClipsToBounds = true;
      currentJobs.ContentInset = UIEdgeInsets.Zero;
      currentJobs.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      currentJobs.Source = new CreatedJobSource(jobID,manageVC);
      currentJobs.ReloadData();

      jobRefresh = new UIRefreshControl();
      jobRefresh.ValueChanged += (sender, e) => {
        refreshJobList(manageVC);
      };

      currentJobs.InsertSubview(jobRefresh,0);
      currentJobs.SendSubviewToBack(jobRefresh);

      mjView.AddSubview(currentJobs);
    }

    public void refreshJobList(JobViewController manageVC){
      var jobs = ion.database.Query<ION.Core.Database.JobRow>("SELECT DISTINCT JID FROM JobRow ORDER BY JID DESC");

      var jobList = new List<int>();
      foreach (var job in jobs) {
        jobList.Add(job.JID);
      }

      currentJobs.Source = new CreatedJobSource(jobList, manageVC);
      currentJobs.ReloadData();
      jobRefresh.EndRefreshing();
    }
  }
}

