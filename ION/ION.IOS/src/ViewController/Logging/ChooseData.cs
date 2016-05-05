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
    public UIButton showGraphButton;
    public UILabel step2;
    public UIImageView backArrow;
    public UILabel middleBorder;
    public UILabel bottomBorder;
    public UILabel sessionHeader;
    public UIButton noJobLabel;
    public UITableView jobTable;
    public UIRefreshControl refreshJobs;
    public UITableView sessionTable;
    public UIRefreshControl refreshSessions;
    public UITapGestureRecognizer resize;
    public UIActivityIndicatorView activityLoadingTables;
    public List<JobData> allJobs;
    public List<SessionData> allSessions;
    public ObservableCollection<int> selectedSessions;
    public LoggingViewController parentVC;
    private IION ion;
    public nfloat cellHeight;

    public ChooseData(UIView mainView, ObservableCollection<int> selected, LoggingViewController vc) {
      parentVC = vc;
      DataType = new UIView(new CGRect(.01 * mainView.Bounds.Width, .15 * mainView.Bounds.Height + 20, .98 * mainView.Bounds.Width, .7 * mainView.Bounds.Height));
      DataType.BackgroundColor = UIColor.White;
      DataType.Layer.BorderColor = UIColor.Black.CGColor;
      DataType.Layer.BorderWidth = 1f; 

      selectedSessions = selected;
      selectedSessions.CollectionChanged += checkForSelected;
      cellHeight = .07f * mainView.Bounds.Height;

      step2 = new UILabel(new CGRect(0,20,DataType.Bounds.Width, .07 * mainView.Bounds.Height));
      step2.BackgroundColor = UIColor.FromRGB(95,212,48);
      step2.ClipsToBounds = true;
      step2.Layer.BorderWidth = 1f;
      step2.Layer.CornerRadius = 8;
      step2.Text = "Back To Session List"; 
      step2.TextColor = UIColor.Black;
      step2.TextAlignment = UITextAlignment.Center;
      step2.AdjustsFontSizeToFitWidth = true;
      step2.Hidden = true;

      backArrow = new UIImageView(new CGRect(.15 * DataType.Bounds.Width,.01 * mainView.Bounds.Height,.1 * step2.Bounds.Width, .05 * mainView.Bounds.Height));
      backArrow.Image = UIImage.FromBundle("img_bent_arrow");
      step2.AddSubview(backArrow);

      activityLoadingTables = new UIActivityIndicatorView(new CGRect(0,0, DataType.Bounds.Width, DataType.Bounds.Height));

      ion = AppState.context;  

      ///button to switch to job listing
      jobButton = new UIButton(new CGRect(0,.06 * mainView.Bounds.Height,.49 * mainView.Bounds.Width, .06 * mainView.Bounds.Height));
      jobButton.Layer.BorderColor = UIColor.Black.CGColor;
      jobButton.SetTitle("By Job", UIControlState.Normal);
      jobButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      jobButton.BackgroundColor = UIColor.White;

      ///user feedback for button press
      jobButton.TouchUpInside += switchJobTab;
      jobButton.TouchDown += (sender, e) => { jobButton.BackgroundColor = UIColor.White;};
      jobButton.TouchUpOutside += (sender, e) => {
        if(!jobTable.Hidden){
          jobButton.BackgroundColor = UIColor.White;
        } else {
          jobButton.BackgroundColor = UIColor.LightGray;
        }
      };
      ///button to switch to session listing
      sessionButton = new UIButton(new CGRect(.49 * mainView.Bounds.Width,.06 * mainView.Bounds.Height,.49 * mainView.Bounds.Width, .06 * mainView.Bounds.Height));
      sessionButton.Layer.BorderColor = UIColor.Black.CGColor;
      sessionButton.SetTitle("By Date", UIControlState.Normal);
      sessionButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      sessionButton.BackgroundColor = UIColor.White;

      ///user feedback for button press
      sessionButton.TouchUpInside += switchSessionTab;
      sessionButton.TouchDown += (sender, e) => { sessionButton.BackgroundColor = UIColor.White;};
      sessionButton.TouchUpOutside += (sender, e) => {
        if(!sessionTable.Hidden){
          sessionButton.BackgroundColor = UIColor.White;
        } else {
          sessionButton.BackgroundColor = UIColor.LightGray;
        }
      };

      sessionHeader = new UILabel(new CGRect(0,0,DataType.Bounds.Width, .061 * mainView.Bounds.Height));
      sessionHeader.Layer.CornerRadius = 8f;
      sessionHeader.Text = "Session Selection";
      sessionHeader.TextAlignment = UITextAlignment.Center;
      sessionHeader.Font = UIFont.BoldSystemFontOfSize(20);
      sessionHeader.BackgroundColor = UIColor.FromRGB(95,212,48);

      middleBorder = new UILabel(new CGRect(.5 * DataType.Bounds.Width,jobButton.Bounds.Height,2,jobButton.Bounds.Height));
      middleBorder.BackgroundColor = UIColor.Black;
      middleBorder.Layer.CornerRadius = 8f;

      bottomBorder = new UILabel(new CGRect(.45 * jobButton.Bounds.Width,1.76 * jobButton.Bounds.Height,.1 * jobButton.Bounds.Width,.08 * jobButton.Bounds.Height));
      bottomBorder.BackgroundColor = UIColor.Blue;
      bottomBorder.Layer.ShadowColor = UIColor.Black.CGColor;
      bottomBorder.Layer.ShadowOpacity = .1f;
      bottomBorder.Layer.ShadowRadius = .3f;
      bottomBorder.Layer.ShadowOffset = new CGSize(0f, 1f);
      bottomBorder.Layer.ShouldRasterize = true;
      bottomBorder.Layer.MasksToBounds = true;

      showGraphButton = new UIButton(new CGRect(.25 * mainView.Bounds.Width,.89 * mainView.Bounds.Height,.5 * mainView.Bounds.Width, cellHeight));
      showGraphButton.Layer.BorderColor = UIColor.Black.CGColor;
      showGraphButton.Layer.BorderWidth = 1f;
      showGraphButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      showGraphButton.SetTitle("Graph Selection", UIControlState.Normal);
      showGraphButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      showGraphButton.Enabled = false;
      showGraphButton.Alpha = .6f;

      ///user feedback for button press
      showGraphButton.TouchUpInside += (sender, e) => { showGraphButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
      showGraphButton.TouchDown += (sender, e) => { showGraphButton.BackgroundColor = UIColor.Blue;};
      showGraphButton.TouchUpOutside += (sender, e) => { showGraphButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};

      sessionTable = new UITableView(new CGRect(0, 2 * sessionButton.Bounds.Height, DataType.Bounds.Width, 8 * cellHeight));
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

      jobTable = new UITableView(new CGRect(0, 2 * jobButton.Bounds.Height, DataType.Bounds.Width, 8 * cellHeight));
      jobTable.RegisterClassForCellReuse(typeof(JobCell),"jobCell");
      jobTable.BackgroundColor = UIColor.Clear;
      jobTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;

      refreshJobs = new UIRefreshControl();
      refreshJobs.ValueChanged += (sender, e) => {
        ReloadAllJobs();
      };
      jobTable.InsertSubview(refreshJobs,0);
      jobTable.SendSubviewToBack(refreshJobs);

      noJobLabel = new UIButton(new CGRect(0,2 * jobButton.Bounds.Height,DataType.Bounds.Width,cellHeight));
      noJobLabel.SetTitle("No jobs exist. Click to create", UIControlState.Normal);
      noJobLabel.SetTitleColor(UIColor.Black, UIControlState.Normal);
      noJobLabel.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      noJobLabel.Layer.BorderWidth = 1f;
      noJobLabel.Hidden = true;
      noJobLabel.TouchUpInside += (sender, e) => {
        var jmvc = vc.InflateViewController<JobViewController>(BaseIONViewController.VC_JOB_MANAGER);
        jmvc.pushed = true;
        vc.NavigationController.PushViewController(jmvc,true);
      };
       
      DataType.AddSubview(sessionHeader);
      DataType.AddSubview(jobButton);
      DataType.BringSubviewToFront(jobButton);
      DataType.AddSubview(sessionButton);
      DataType.BringSubviewToFront(sessionButton);
      DataType.AddSubview(jobTable);
      DataType.BringSubviewToFront(jobTable);
      DataType.AddSubview(noJobLabel);
      DataType.AddSubview(sessionTable);
      DataType.BringSubviewToFront(sessionTable);
      DataType.AddSubview(activityLoadingTables);
      DataType.BringSubviewToFront(activityLoadingTables);
      DataType.AddSubview(step2);
      DataType.BringSubviewToFront(step2);
      DataType.AddSubview(bottomBorder);
      DataType.AddSubview(middleBorder);
      jobButton.SendActionForControlEvents(UIControlEvent.TouchUpInside);
    }
    /// <summary>
    /// Animate the session/job tab and then update the data
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void switchSessionTab(object sender, EventArgs e){
      jobButton.BackgroundColor = UIColor.LightGray;
      jobButton.Font = UIFont.SystemFontOfSize(18);
      sessionButton.BackgroundColor = UIColor.White;
      sessionButton.Font = UIFont.BoldSystemFontOfSize(22);

      sessionButton.Enabled = false;
      jobTable.Hidden = true;
      sessionTable.Hidden = false;

      UIView.Animate(.3, 0, UIViewAnimationOptions.CurveEaseInOut, () => {        
        var newCenter = bottomBorder.Frame;
        newCenter.X = .72f * DataType.Bounds.Width;
        bottomBorder.Frame = newCenter;
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

      sessionTable.Frame = new CGRect(0, 2 * sessionButton.Bounds.Height, DataType.Bounds.Width, 8 * cellHeight);
      sessionTable.Source = new LoggingSessionSource(allSessions,cellHeight, selectedSessions);
      sessionTable.ReloadData();
      sessionTable.LayoutIfNeeded();

      await Task.Delay(TimeSpan.FromSeconds(1));
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

      sessionTable.Frame = new CGRect(0, 2 * sessionButton.Bounds.Height, DataType.Bounds.Width, 8 * cellHeight);
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
      sessionButton.BackgroundColor = UIColor.LightGray;
      sessionButton.Font = UIFont.SystemFontOfSize(18);
      jobButton.BackgroundColor = UIColor.White;
      jobButton.Font = UIFont.BoldSystemFontOfSize(22);

      jobButton.Enabled = false;
      sessionTable.Hidden = true;
      jobTable.Hidden = false;

      UIView.Animate(.3, 0, UIViewAnimationOptions.CurveEaseInOut, () => {        
        var newCenter = bottomBorder.Frame;
        newCenter.X = .45f * jobButton.Bounds.Width;
        bottomBorder.Frame = newCenter;
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

      List<JobData> queriedJobs = new List<JobData>();
      for (int i = 0; i < result.Count; i++) {
        queriedJobs.Add(new JobData(result[i].JID, result[i].jobName,jobTable,cellHeight, selectedSessions,parentVC));
        var jSessions = ion.database.Query<ION.Core.Database.SessionRow>("SELECT * FROM SessionRow WHERE frn_JID = " + result[i].JID);
        foreach (var sess in jSessions) {
          queriedJobs[i].jobSessions.Add(new SessionData(sess.SID, sess.sessionStart, sess.sessionEnd));
        }
      }
      allJobs = queriedJobs;
      jobTable.Frame = new CGRect(0, 2 * jobButton.Bounds.Height, DataType.Bounds.Width, 8 * cellHeight);
      jobTable.Source = new LoggingJobSource(allJobs, cellHeight, selectedSessions);
      jobTable.ReloadData();
      jobTable.LayoutIfNeeded();

      if (result.Count.Equals(0)) {
        Console.WriteLine("There are no jobs");
        noJobLabel.Hidden = false;
      } else {
        Console.WriteLine("There are jobs available");
        noJobLabel.Hidden = true;
      }

      await Task.Delay(TimeSpan.FromSeconds(1));
      activityLoadingTables.StopAnimating();
      refreshJobs.EndRefreshing();
      jobButton.Enabled = true;
    }

    /// <summary>
    /// Queries for all the jobs a user has created and lists them in a tableview
    /// </summary>
    public void ReloadAllJobs (){
      var result = ion.database.Query<ION.Core.Database.JobRow>("SELECT JID, jobName FROM JobRow ORDER BY JID");

      List<JobData> queriedJobs = new List<JobData>();
      for (int i = 0; i < result.Count; i++) {
        queriedJobs.Add(new JobData(result[i].JID, result[i].jobName,jobTable,cellHeight, selectedSessions,parentVC));
        var jSessions = ion.database.Query<ION.Core.Database.SessionRow>("SELECT * FROM SessionRow WHERE frn_JID = " + result[i].JID);
        foreach (var sess in jSessions) {
          queriedJobs[i].jobSessions.Add(new SessionData(sess.SID, sess.sessionStart, sess.sessionEnd));
        }
      }
      allJobs = queriedJobs;
      jobTable.Frame = new CGRect(0, 2 * jobButton.Bounds.Height, DataType.Bounds.Width, 8 * cellHeight);
      jobTable.Source = new LoggingJobSource(allJobs, cellHeight, selectedSessions);
      jobTable.ReloadData();
      jobTable.LayoutIfNeeded();

      if (result.Count.Equals(0)) {
        Console.WriteLine("There are no jobs");
        noJobLabel.Hidden = false;
      } else {
        Console.WriteLine("There are jobs available");
        noJobLabel.Hidden = true;
      }

      refreshJobs.EndRefreshing();
      jobButton.Enabled = true;
    }

    public void checkForSelected(object sender, EventArgs e){
        if(selectedSessions.Count > 0){
          showGraphButton.Enabled = true;
          showGraphButton.Alpha = 1f;
        } else {
          showGraphButton.Enabled = false;
          showGraphButton.Alpha = .6f;
        }
    }
  }
}

