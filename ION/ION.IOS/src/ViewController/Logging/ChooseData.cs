using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

using UIKit;
using Foundation;
using CoreGraphics;

using SQLite;

using ION.Core.App;
using ION.Core.Database;
using ION.IOS.ViewController.JobManager;

namespace ION.IOS.ViewController.Logging {
  
  public class ChooseData {
    public UIView DataType;
    public UIButton jobButton;
    public UIButton sessionButton;
		public UIButton noJobLabel;

		public UITableView jobTable;
		public UITableView sessionTable;
		public UIRefreshControl refreshJobs;
		public UIRefreshControl refreshSessions;

    public UILabel sessionHeader;
		public UILabel jobHighlight;
		public UILabel sessionHighlight;

    public UITapGestureRecognizer resize;
    public UIActivityIndicatorView activityLoadingTables;
    public List<JobData> allJobs;
    public List<SessionData> allSessions;
    public ObservableCollection<int> selectedSessions;
    public LoggingViewController parentVC;
    private IION ion;
    public nfloat cellHeight;

    public ChooseData(UIView containerView, ObservableCollection<int> selected, LoggingViewController vc) {
      parentVC = vc;
      DataType = new UIView(new CGRect(0, 55, containerView.Bounds.Width, .8 * containerView.Bounds.Height));
      DataType.BackgroundColor = UIColor.White;
      DataType.Layer.BorderColor = UIColor.Black.CGColor;
      DataType.Layer.BorderWidth = 1f;
      DataType.Layer.CornerRadius = 5;
      DataType.ClipsToBounds = true;

      selectedSessions = selected;
      cellHeight = .1f * containerView.Bounds.Height;

      activityLoadingTables = new UIActivityIndicatorView(new CGRect(0,0, DataType.Bounds.Width, DataType.Bounds.Height));

      ion = AppState.context;

			sessionHeader = new UILabel(new CGRect(0, 0, DataType.Bounds.Width, 40));
			sessionHeader.Text = Util.Strings.Report.SESSIONSELECTION;
			sessionHeader.TextAlignment = UITextAlignment.Center;
			sessionHeader.Font = UIFont.BoldSystemFontOfSize(20);
			sessionHeader.BackgroundColor = UIColor.FromRGB(0, 166, 81);

      jobButton = new UIButton(new CGRect(0,40,.5 * DataType.Bounds.Width, 40));
      jobButton.Layer.BorderColor = UIColor.Black.CGColor;
      jobButton.SetTitle(Util.Strings.Report.BYJOB, UIControlState.Normal);
      jobButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      jobButton.BackgroundColor = UIColor.White;

      jobButton.TouchUpInside += switchJobTab;

      jobHighlight = new UILabel(new CGRect(0,80,.5 * DataType.Bounds.Width,5));
      jobHighlight.BackgroundColor = UIColor.FromRGB(0, 174, 239);

      sessionButton = new UIButton(new CGRect(.5 * DataType.Bounds.Width,40,.5 * containerView.Bounds.Width, 40));
      sessionButton.Layer.BorderColor = UIColor.Black.CGColor;
      sessionButton.SetTitle(Util.Strings.Report.BYDATE, UIControlState.Normal);
      sessionButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      sessionButton.BackgroundColor = UIColor.White;

      sessionButton.TouchUpInside += switchSessionTab;

      sessionHighlight = new UILabel(new CGRect(.5 * DataType.Bounds.Width, 80, .5 * DataType.Bounds.Width, 5));
      sessionHighlight.BackgroundColor = UIColor.Black;

      sessionTable = new UITableView(new CGRect(5, 85, DataType.Bounds.Width - 10, 6.1 * cellHeight));
      sessionTable.RegisterClassForCellReuse(typeof(SessionCell),"sessionCell");
      sessionTable.BackgroundColor = UIColor.Clear;
      sessionTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      sessionTable.Hidden = true;
      sessionTable.EstimatedRowHeight = 0;
      refreshSessions = new UIRefreshControl();
      refreshSessions.ValueChanged += (sender, e) => {
        if(!ion.dataLogManager.isRecording){
          ReloadAllSessions();
        }
      };

      sessionTable.InsertSubview(refreshSessions,0);
      sessionTable.SendSubviewToBack(refreshSessions);

      jobTable = new UITableView(new CGRect(5, 85, DataType.Bounds.Width - 10, 6.1 * cellHeight));
      jobTable.RegisterClassForCellReuse(typeof(JobCell),"jobCell");
      jobTable.BackgroundColor = UIColor.Clear;
      jobTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;

      refreshJobs = new UIRefreshControl();
      refreshJobs.ValueChanged += (sender, e) => {
        ReloadAllJobs();
      };
      jobTable.InsertSubview(refreshJobs,0);  
      jobTable.SendSubviewToBack(refreshJobs);

      noJobLabel = new UIButton(new CGRect(0,90,DataType.Bounds.Width,cellHeight));
      noJobLabel.SetTitle(Util.Strings.Report.NOJOBS, UIControlState.Normal);
      noJobLabel.SetTitleColor(UIColor.Black, UIControlState.Normal);
      noJobLabel.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      noJobLabel.Layer.BorderWidth = 1f;
      noJobLabel.Hidden = true;
      noJobLabel.TouchDown += (sender, e) => {noJobLabel.BackgroundColor = UIColor.Blue;};
      noJobLabel.TouchUpOutside += (sender, e) => {noJobLabel.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
      noJobLabel.TouchUpInside += (sender, e) => {
        noJobLabel.BackgroundColor = UIColor.FromRGB(255, 215, 101);
        var jmvc = vc.InflateViewController<JobViewController>(BaseIONViewController.VC_JOB_MANAGER);
        jmvc.pushed = true;
        parentVC.NavigationController.PushViewController(jmvc,true);
      }; 
       
      DataType.AddSubview(sessionHeader);
      DataType.AddSubview(jobButton);
			DataType.BringSubviewToFront(jobButton);
			DataType.AddSubview(jobHighlight);
      DataType.AddSubview(sessionButton);
      DataType.BringSubviewToFront(sessionButton);
			DataType.AddSubview(sessionHighlight);
			DataType.AddSubview(jobTable);
      DataType.BringSubviewToFront(jobTable);
      DataType.AddSubview(noJobLabel);
      DataType.AddSubview(sessionTable);
      DataType.BringSubviewToFront(sessionTable);
      DataType.AddSubview(activityLoadingTables);
      DataType.BringSubviewToFront(activityLoadingTables);
      jobButton.SendActionForControlEvents(UIControlEvent.TouchUpInside);
    }
    /// <summary>
    /// Animate the session/job tab and then update the data
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void switchSessionTab(object sender, EventArgs e){
      noJobLabel.Hidden = true;
      jobButton.Enabled = true;
      jobButton.BackgroundColor = UIColor.LightGray;
      jobHighlight.BackgroundColor = UIColor.Black;
      jobButton.Font = UIFont.SystemFontOfSize(18);
      sessionButton.BackgroundColor = UIColor.White;
      sessionButton.Font = UIFont.BoldSystemFontOfSize(22);
      sessionHighlight.BackgroundColor = UIColor.FromRGB(0, 174, 239);
      sessionButton.Enabled = false;
      jobTable.Hidden = true;
      sessionTable.Hidden = false;

      UIView.Animate(.3, 0, UIViewAnimationOptions.CurveEaseInOut, () => {        

      },() => {
        if(!ion.dataLogManager.isRecording){
          GetAllSessions();
        }
      });
    }

    /// <summary>
    /// Queries for all the sessions a user has recorded and lists them in a tableview
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public async void GetAllSessions(){
      
      if (activityLoadingTables != null) {
        activityLoadingTables.StopAnimating();
        activityLoadingTables = null;
      }
      activityLoadingTables = new UIActivityIndicatorView(new CGRect(0, 2 * sessionButton.Bounds.Height, DataType.Bounds.Width, DataType.Bounds.Height - (2 * sessionButton.Bounds.Height)));
      activityLoadingTables.Alpha = .4f;
      activityLoadingTables.BackgroundColor = UIColor.DarkGray;

      DataType.AddSubview(activityLoadingTables);
      DataType.BringSubviewToFront(activityLoadingTables);

      activityLoadingTables.StartAnimating();

      var result = ion.database.Query<ION.Core.Database.SessionRow>("SELECT SID, sessionStart, sessionEnd FROM SessionRow ORDER BY SID DESC");
      List<SessionData> queriedSessions = new List<SessionData>();

      for (int i = 0; i < result.Count; i++) {
        queriedSessions.Add(new SessionData(result[i].SID, result[i].sessionStart, result[i].sessionEnd));

        var measurements = ion.database.Query<ION.Core.Database.SensorMeasurementRow>("SELECT * FROM SensorMeasurementRow WHERE frn_SID = ? ORDER BY MID ASC", result[i].SID);

        foreach (var meas in measurements) {          
          queriedSessions[i].sessionMeasurements.Add(new MeasurementData(meas.MID, meas.frn_SID, meas.serialNumber, meas.measurement.ToString()));
        }
      }
      allSessions = queriedSessions;

      sessionTable.Source = new LoggingSessionSource(allSessions,cellHeight, selectedSessions);
      sessionTable.ReloadData();
      sessionTable.LayoutIfNeeded();

      await Task.Delay(TimeSpan.FromSeconds(.5));
      activityLoadingTables.StopAnimating();
      sessionButton.Enabled = true;
      refreshSessions.EndRefreshing();
    }

    /// <summary>
    /// Queries for all the sessions a user has recorded and lists them in a tableview
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void ReloadAllSessions(){
      var result = ion.database.Query<ION.Core.Database.SessionRow>("SELECT SID, sessionStart, sessionEnd FROM SessionRow ORDER BY SID DESC");
      List<SessionData> queriedSessions = new List<SessionData>();

      for (int i = 0; i < result.Count; i++) {
        queriedSessions.Add(new SessionData(result[i].SID, result[i].sessionStart, result[i].sessionEnd));

        var measurements = ion.database.Query<ION.Core.Database.SensorMeasurementRow>("SELECT * FROM SensorMeasurementRow WHERE frn_SID = ? ORDER BY MID ASC", result[i].SID);

        foreach (var meas in measurements) {          
          queriedSessions[i].sessionMeasurements.Add(new MeasurementData(meas.MID, meas.frn_SID, meas.serialNumber, meas.measurement.ToString()));
        }
      }
      allSessions = queriedSessions;

      sessionTable.Source = new LoggingSessionSource(allSessions,cellHeight, selectedSessions);
      sessionTable.ReloadData();
      sessionTable.LayoutIfNeeded();

      sessionButton.Enabled = true;
      refreshSessions.EndRefreshing();
    }

    /// <summary>
    /// Animate the switch to the job tab and then load job data
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void switchJobTab(object sender, EventArgs e){
    	sessionButton.Enabled = true;
      sessionButton.BackgroundColor = UIColor.LightGray;
      sessionHighlight.BackgroundColor = UIColor.Black;
      sessionButton.Font = UIFont.SystemFontOfSize(18);
      jobButton.BackgroundColor = UIColor.White;
      jobButton.Font = UIFont.BoldSystemFontOfSize(22);
			jobHighlight.BackgroundColor = UIColor.FromRGB(0, 174, 239);

			jobButton.Enabled = false;
      sessionTable.Hidden = true;
      jobTable.Hidden = false;

      UIView.Animate(.3, 0, UIViewAnimationOptions.CurveEaseInOut, () => {        

      },() => {
        GetAllJobs();
      });
    }
    /// <summary>
    /// Queries for all the jobs a user has created and lists them in a tableview
    /// </summary>
    public async void GetAllJobs (){

      if (activityLoadingTables != null) {
        activityLoadingTables.StopAnimating();
        activityLoadingTables = null;
      }

      activityLoadingTables = new UIActivityIndicatorView(new CGRect(0, 2 * jobButton.Bounds.Height, DataType.Bounds.Width, DataType.Bounds.Height - (2 * jobButton.Bounds.Height)));
      activityLoadingTables.Alpha = .4f;
      activityLoadingTables.BackgroundColor = UIColor.DarkGray;

      DataType.AddSubview(activityLoadingTables);
      DataType.BringSubviewToFront(activityLoadingTables);

      activityLoadingTables.StartAnimating();

      var result = ion.database.Query<ION.Core.Database.JobRow>("SELECT JID, jobName FROM JobRow ORDER BY JID");
      var unassigned = ion.database.Query<ION.Core.Database.SessionRow>("SELECT * FROM SessionRow WHERE frn_JID is null OR frn_JID = 0");

      List<JobData> queriedJobs = new List<JobData>();
      for (int i = 0; i < result.Count; i++) {
        queriedJobs.Add(new JobData(result[i].JID, result[i].jobName,jobTable,cellHeight, selectedSessions,parentVC));
        var jSessions = ion.database.Query<ION.Core.Database.SessionRow>("SELECT * FROM SessionRow WHERE frn_JID = " + result[i].JID);
        foreach (var sess in jSessions) {
          queriedJobs[i].jobSessions.Add(new SessionData(sess.SID, sess.sessionStart, sess.sessionEnd));
        }
      }
      queriedJobs.Add(new JobData(0,Util.Strings.Report.UNASSIGNED,jobTable, cellHeight, selectedSessions, parentVC));
      foreach(var unsesh in unassigned){
      	queriedJobs[queriedJobs.Count - 1].jobSessions.Add(new SessionData(unsesh.SID, unsesh.sessionStart, unsesh.sessionEnd));
      }
      
      allJobs = queriedJobs;
      jobTable.Source = new LoggingJobSource(allJobs, cellHeight, selectedSessions);
      jobTable.ReloadData();
      //jobTable.LayoutIfNeeded();

      if (result.Count.Equals(0)) {
        noJobLabel.Hidden = false;
      } else {
        noJobLabel.Hidden = true;
      }

      await Task.Delay(TimeSpan.FromSeconds(.5));
      activityLoadingTables.StopAnimating();
      refreshJobs.EndRefreshing();
      jobButton.Enabled = true;
    }

    /// <summary>
    /// Queries for all the jobs a user has created and lists them in a tableview
    /// </summary>
    public void ReloadAllJobs (){
      var result = ion.database.Query<ION.Core.Database.JobRow>("SELECT JID, jobName FROM JobRow ORDER BY JID");
      var unassigned = ion.database.Query<ION.Core.Database.SessionRow>("SELECT * FROM SessionRow WHERE frn_JID is null OR frn_JID = 0");

      List<JobData> queriedJobs = new List<JobData>();
      for (int i = 0; i < result.Count; i++) {
        queriedJobs.Add(new JobData(result[i].JID, result[i].jobName,jobTable,cellHeight, selectedSessions,parentVC));
        var jSessions = ion.database.Query<ION.Core.Database.SessionRow>("SELECT * FROM SessionRow WHERE frn_JID = " + result[i].JID);
        foreach (var sess in jSessions) {
          queriedJobs[i].jobSessions.Add(new SessionData(sess.SID, sess.sessionStart, sess.sessionEnd));
        }
      }

      queriedJobs.Add(new JobData(0,Util.Strings.Report.UNASSIGNED,jobTable, cellHeight, selectedSessions, parentVC));
      foreach(var unsesh in unassigned){
      	queriedJobs[queriedJobs.Count - 1].jobSessions.Add(new SessionData(unsesh.SID, unsesh.sessionStart, unsesh.sessionEnd));
      }      
      
      allJobs = queriedJobs;
      jobTable.Source = new LoggingJobSource(allJobs, cellHeight, selectedSessions);
      jobTable.ReloadData();
      jobTable.LayoutIfNeeded();

      if (result.Count.Equals(0)) {
        noJobLabel.Hidden = false;
      } else {
        noJobLabel.Hidden = true;
      }

      refreshJobs.EndRefreshing();
      jobButton.Enabled = true;
    }

  }
}

