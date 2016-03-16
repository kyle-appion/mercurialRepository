using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

using UIKit;
using Foundation;
using CoreGraphics;

using SQLite;

using ION.Core.App;

namespace ION.IOS.ViewController.Logging {
  
  public class ChooseData {
    public UIView DataType;
    public UIButton jobButton;
    public UIButton sessionButton;
    public UIButton showGraphButton;
    public UILabel step2;
    public UITableView jobTable;
    public UITableView sessionTable;
    public UITapGestureRecognizer resize;
    public UIActivityIndicatorView activityLoadingTables;
    public List<JobData> allJobs;
    public List<SessionData> allSessions;
    public ObservableCollection<int> selectedSessions;
    private IION ion;
    public bool jobSelected = true;
    public bool sessionSelected = false;
    private nfloat cellHeight;

    public ChooseData(UIView mainView) {
      DataType = new UIView(new CGRect(.01 * mainView.Bounds.Width,.15 * mainView.Bounds.Height,.98 * mainView.Bounds.Width, .08 * mainView.Bounds.Height));
      DataType.BackgroundColor = UIColor.White;
      DataType.Layer.BorderColor = UIColor.Black.CGColor;
      DataType.Layer.BorderWidth = 1f;
      DataType.Hidden = true;
      DataType.Layer.CornerRadius = 8;

      selectedSessions = new ObservableCollection<int>();
      selectedSessions.CollectionChanged += checkForSelected;
      cellHeight = .07f * mainView.Bounds.Height;

      step2 = new UILabel(new CGRect(0,0,DataType.Bounds.Width, cellHeight));
      step2.Text = "Step 2";
      step2.TextColor = UIColor.Black;
      step2.TextAlignment = UITextAlignment.Center;
      step2.AdjustsFontSizeToFitWidth = true;
      step2.Hidden = true;

      activityLoadingTables = new UIActivityIndicatorView(new CGRect(0,0, DataType.Bounds.Width, DataType.Bounds.Height));

      ion = AppState.context;
      ///button to switch to job listing
      jobButton = new UIButton(new CGRect(0,0,.49 * mainView.Bounds.Width, .12 * mainView.Bounds.Height));
      jobButton.Layer.CornerRadius = 8;
      jobButton.Layer.BorderColor = UIColor.Black.CGColor;
      jobButton.Layer.BorderWidth = 1f;
      jobButton.SetTitle("Show Jobs", UIControlState.Normal);
      jobButton.BackgroundColor = UIColor.LightGray;
      jobButton.Hidden = true;
      ///user feedback for button press
      jobButton.TouchUpInside += GetAllJobs;
      jobButton.TouchDown += (sender, e) => { jobButton.BackgroundColor = UIColor.Blue;};
      jobButton.TouchUpOutside += (sender, e) => { };
      ///button to switch to session listing
      sessionButton = new UIButton(new CGRect(.49 * mainView.Bounds.Width,0,.49 * mainView.Bounds.Width, .12 * mainView.Bounds.Height));
      sessionButton.Layer.CornerRadius = 8;
      sessionButton.Layer.BorderColor = UIColor.Black.CGColor;
      sessionButton.Layer.BorderWidth = 1f;
      sessionButton.SetTitle("Show Sessions", UIControlState.Normal);
      sessionButton.BackgroundColor = UIColor.LightGray;
      sessionButton.Hidden = true;
      ///user feedback for button press
      sessionButton.TouchUpInside += GetAllSessions;
      sessionButton.TouchDown += (sender, e) => { sessionButton.BackgroundColor = UIColor.Blue;};
      sessionButton.TouchUpOutside += (sender, e) => { sessionButton.BackgroundColor = UIColor.LightGray;};

      showGraphButton = new UIButton(new CGRect(.25 * DataType.Bounds.Width,10 * cellHeight,.5 * DataType.Bounds.Width, cellHeight));
      showGraphButton.Layer.BorderColor = UIColor.Black.CGColor;
      showGraphButton.Layer.BorderWidth = 1f;
      showGraphButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      showGraphButton.SetTitle("Graph Selection", UIControlState.Normal);
      showGraphButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      showGraphButton.Hidden = true;
      showGraphButton.Enabled = false;
      showGraphButton.Alpha = .6f;
      ///user feedback for button press
      showGraphButton.TouchUpInside += (sender, e) => { showGraphButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
      showGraphButton.TouchDown += (sender, e) => { showGraphButton.BackgroundColor = UIColor.Blue;};
      showGraphButton.TouchUpOutside += (sender, e) => { showGraphButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};

      sessionTable = new UITableView(new CGRect(0, .12 * mainView.Bounds.Height, DataType.Bounds.Width, 8 * cellHeight));
      sessionTable.RegisterClassForCellReuse(typeof(SessionCell),"sessionCell");
      sessionTable.BackgroundColor = UIColor.Clear;
      sessionTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      sessionTable.Hidden = true;

      jobTable = new UITableView(new CGRect(0, .12 * mainView.Bounds.Height, DataType.Bounds.Width, 8 * cellHeight));
      jobTable.RegisterClassForCellReuse(typeof(JobCell),"jobCell");
      jobTable.BackgroundColor = UIColor.Clear;
      jobTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      jobTable.Hidden = true;

      DataType.AddSubview(jobButton);
      DataType.BringSubviewToFront(jobButton);
      DataType.AddSubview(sessionButton);
      DataType.BringSubviewToFront(sessionButton);
      DataType.AddSubview(jobTable);
      DataType.BringSubviewToFront(jobTable);
      DataType.AddSubview(sessionTable);
      DataType.BringSubviewToFront(sessionTable);
      DataType.AddSubview(activityLoadingTables);
      DataType.BringSubviewToFront(activityLoadingTables);
      DataType.AddSubview(showGraphButton);
      DataType.BringSubviewToFront(showGraphButton);
      DataType.AddSubview(step2);
      DataType.BringSubviewToFront(step2);
    }

    /// <summary>
    /// Queries for all the sessions a user has recorded and lists them in a tableview
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public async void GetAllSessions(object sender, EventArgs e){      
      jobSelected = false;
      jobButton.BackgroundColor = UIColor.LightGray;
      sessionSelected = true;
      sessionButton.BackgroundColor = UIColor.Blue;
      sessionButton.Enabled = false;
      jobTable.Hidden = true;
      sessionTable.Hidden = false;

      if (activityLoadingTables != null)
        activityLoadingTables = null;

      activityLoadingTables = new UIActivityIndicatorView(new CGRect(0, 0, DataType.Bounds.Width, DataType.Bounds.Height));
      activityLoadingTables.Alpha = .4f;
      activityLoadingTables.Layer.CornerRadius = 8;
      activityLoadingTables.BackgroundColor = UIColor.DarkGray;

      DataType.AddSubview(activityLoadingTables);
      DataType.BringSubviewToFront(activityLoadingTables);

      activityLoadingTables.StartAnimating();

      var result = ion.database.Query<ION.Core.Database.Session>("SELECT SID, sessionStart, sessionEnd FROM Session ORDER BY SID DESC");
      List<SessionData> queriedSessions = new List<SessionData>();

      for (int i = 0; i < result.Count; i++) {
        queriedSessions.Add(new SessionData(result[i].id, result[i].sessionStart, result[i].sessionEnd));

        var measurements = ion.database.Query<ION.Core.Database.SessionMeasurement>("SELECT MID, frnSID, deviceSN, deviceMeasurement FROM SessionMeasurement WHERE frnSID = " + queriedSessions[i].SID);

        foreach(var value in measurements){
          queriedSessions[i].sessionMeasurements.Add(new MeasurementData(value.MID,value.frnSID,value.deviceSN, value.deviceMeasurement));
        }
      }
      allSessions = queriedSessions;

      sessionTable.Frame = new CGRect(0, jobButton.Bounds.Height, DataType.Bounds.Width, 8 * cellHeight);
      sessionTable.Source = new LoggingSessionSource(allSessions,cellHeight, selectedSessions);
      sessionTable.ReloadData();

      await Task.Delay(TimeSpan.FromSeconds(2));
      activityLoadingTables.StopAnimating();
      sessionButton.Enabled = true;
    }

    /// <summary>
    /// Queries for all the jobs a user has created and lists them in a tableview
    /// </summary>
    public async void GetAllJobs (object sender, EventArgs e){
      sessionSelected = false;
      sessionButton.BackgroundColor = UIColor.LightGray;
      jobSelected = true;
      jobButton.BackgroundColor = UIColor.Blue;
      jobButton.Enabled = false;
      sessionTable.Hidden = true;
      jobTable.Hidden = false;

      if (activityLoadingTables != null)
        activityLoadingTables = null;

      activityLoadingTables = new UIActivityIndicatorView(new CGRect(0, 0, DataType.Bounds.Width, DataType.Bounds.Height));
      activityLoadingTables.Alpha = .4f;
      activityLoadingTables.Layer.CornerRadius = 8;
      activityLoadingTables.BackgroundColor = UIColor.DarkGray;

      DataType.AddSubview(activityLoadingTables);
      DataType.BringSubviewToFront(activityLoadingTables);

      activityLoadingTables.StartAnimating();

      var result = ion.database.Query<ION.Core.Database.Job>("SELECT JID, jobName FROM Job ORDER BY JID");

      List<JobData> queriedJobs = new List<JobData>();
      for (int i = 0; i < result.Count; i++) {
        queriedJobs.Add(new JobData(result[i].id, result[i].jobName,jobTable,cellHeight, selectedSessions));
        var jSessions = ion.database.Query<ION.Core.Database.Session>("SELECT * FROM Session WHERE frnJID = " + result[i].id);
        foreach (var sess in jSessions) {
          queriedJobs[i].jobSessions.Add(new SessionData(sess.id, sess.sessionStart, sess.sessionEnd));
        }

      }
      allJobs = queriedJobs;
      jobTable.Frame = new CGRect(0, jobButton.Bounds.Height, DataType.Bounds.Width, 8 * cellHeight);
      jobTable.Source = new LoggingJobSource(allJobs, cellHeight, selectedSessions);
      jobTable.ReloadData();

      await Task.Delay(TimeSpan.FromSeconds(2));
      activityLoadingTables.StopAnimating();
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

