using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;
using System.IO;
using SQLite;
using System.Threading.Tasks;

using ION.IOS.ViewController;
using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Util;
using ION.IOS.Util;
using ION.IOS.UI;

namespace ION.IOS.ViewController.Logging {
  public partial class LoggingViewController : BaseIONViewController {
    private SessionView listSessions;
    private JobView listJobs;
    private UIButton jobButton;
    private UIButton sessionButton;
    private string _pathToDatabase;
    private bool jobSelected = true;
    private bool sessionSelected = false;
    public UITableView jobTable;
    public UITableView sessionTable;
    public AssociateJob jobScroll;

    public LoggingViewController(IntPtr handle) : base(handle) {
    
    }

    public override void ViewDidLoad() {
      base.ViewDidLoad();
      // Perform any additional setup after loading the view, typically from a nib.
      View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));

      InitNavigationBar("ic_nav_workbench", false);

      backAction = () => {
        root.navigation.ToggleMenu();
      };
      //**************************************************************
      UIBarButtonItem dataRecord = new UIBarButtonItem(
        //UIImage.FromBundle("ic_record"),
        "R",
        UIBarButtonItemStyle.Plain, 
        (sender, e) => {
          Console.WriteLine("Clicked record");
        }
      );

      UIBarButtonItem dataStop= new UIBarButtonItem(
        "S", 
        UIBarButtonItemStyle.Plain, 
        (sender, e) => {
          Console.WriteLine("Clicked stop");
        }
      );
      //dataStop.Image = UIImage.FromBundle("ic_stop");

      UIBarButtonItem showRecords= new UIBarButtonItem(
        //UIImage.FromBundle("ic_menu"), 
        "P",
        UIBarButtonItemStyle.Plain, 
        (sender, e) => {
          Console.WriteLine("Clicked pause");
        }
      );
      //showRecords.Image = UIImage.FromBundle("ic_pause");

      UIBarButtonItem [] recordButtons = new UIBarButtonItem[]{dataStop,showRecords,dataRecord,};
      NavigationItem.SetRightBarButtonItems(recordButtons, true);
      //**************************************************************

      var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      _pathToDatabase = Path.Combine(documents, "AppionJSO.db");

      SetupLoggingUI();
    }
    /// <summary>
    /// Creates the job and session views and adds their views/subviews to the mainview
    /// </summary>
    public void SetupLoggingUI(){
      listJobs = new JobView(View);
      listJobs.createJob.TouchUpInside += showNewJobAlert;
      listSessions = new SessionView(View);

      ///button to switch to job listing
      jobButton = new UIButton(new CGRect(.01 * View.Bounds.Width,.08 * View.Bounds.Height,.49 * View.Bounds.Width, .12 * View.Bounds.Height));
      jobButton.Layer.CornerRadius = 8;
      jobButton.Layer.BorderColor = UIColor.Black.CGColor;
      jobButton.Layer.BorderWidth = 1f;
      jobButton.SetTitle("Show Jobs", UIControlState.Normal);
      jobButton.BackgroundColor = UIColor.LightGray;
      ///user feedback for button press
      jobButton.TouchUpInside += GetAllJobs;
      jobButton.TouchDown += (sender, e) => { jobButton.BackgroundColor = UIColor.Blue;};
      jobButton.TouchUpOutside += (sender, e) => { if(jobSelected == false) jobButton.BackgroundColor = UIColor.LightGray;};
      ///button to switch to session listing
      sessionButton = new UIButton(new CGRect(.5 * View.Bounds.Width,.08 * View.Bounds.Height,.49 * View.Bounds.Width, .12 * View.Bounds.Height));
      sessionButton.Layer.CornerRadius = 8;
      sessionButton.Layer.BorderColor = UIColor.Black.CGColor;
      sessionButton.Layer.BorderWidth = 1f;
      sessionButton.SetTitle("Show Sessions", UIControlState.Normal);
      sessionButton.BackgroundColor = UIColor.LightGray;
      ///user feedback for button press
      sessionButton.TouchUpInside += GetAllSessions;
      sessionButton.TouchDown += (sender, e) => { sessionButton.BackgroundColor = UIColor.Blue;};
      sessionButton.TouchUpOutside += (sender, e) => { if(sessionSelected == false) sessionButton.BackgroundColor = UIColor.LightGray;};

      sessionTable = new UITableView(new CGRect(.01 * View.Bounds.Width, .2 * View.Bounds.Height, listSessions.sView.Bounds.Width, .07 * View.Bounds.Height));
      sessionTable.RegisterClassForCellReuse(typeof(SessionCell),"sessionCell");
      sessionTable.BackgroundColor = UIColor.Clear;
      sessionTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      ///collapses and expands uiview when user taps the view
      listSessions.sView.AddGestureRecognizer(new UITapGestureRecognizer (() => {
        if(listSessions.expanded == true){
          listSessions.expanded = false;
          sessionTable.Hidden = true;
          UIView.Animate(.5,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              listSessions.sView.Frame = new CGRect(.01 * View.Bounds.Width, .2 * View.Bounds.Height, .98 * View.Bounds.Width, .075 * View.Bounds.Height);
            }, () => {});          
        } else {
          listSessions.expanded = true;
          UIView.Animate(.5,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              listSessions.sView.Frame = new CGRect(.01 * View.Bounds.Width, .2 * View.Bounds.Height, .98 * View.Bounds.Width, .75 * View.Bounds.Height);
            }, () => {sessionTable.Hidden = false;});
        }
      }));

      jobTable = new UITableView(new CGRect(0, .2 * View.Bounds.Height + listJobs.createJob.Bounds.Height, listJobs.jView.Bounds.Width, .07 * View.Bounds.Height));
      jobTable.RegisterClassForCellReuse(typeof(JobCell),"jobCell");
      jobTable.BackgroundColor = UIColor.Clear;
      jobTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      ///collapses and expands uiview when user taps the view
      listJobs.jView.AddGestureRecognizer(new UITapGestureRecognizer (() => {
        if(listJobs.expanded == true){
          listJobs.expanded = false;
          jobTable.Hidden = true;
          UIView.Animate(.5,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              listJobs.jView.Frame = new CGRect(.01 * View.Bounds.Width, .2 * View.Bounds.Height, .98 * View.Bounds.Width, .15 * View.Bounds.Height);
            }, () => {});          
        } else {
          listJobs.expanded = true;         
          UIView.Animate(.5,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              listJobs.jView.Frame = new CGRect(.01 * View.Bounds.Width, .2 * View.Bounds.Height, .98 * View.Bounds.Width, .75 * View.Bounds.Height);
            }, () => {jobTable.Hidden = false;});
        }
      }));

      jobScroll = new AssociateJob(View, .07f * View.Bounds.Height);

      View.AddSubview(listSessions.sView);
      View.BringSubviewToFront(listSessions.sView);
      View.AddSubview(sessionButton);
      View.BringSubviewToFront(sessionButton);

      View.AddSubview(listJobs.jView);
      View.BringSubviewToFront(listJobs.jView);
      View.AddSubview(jobButton);
      View.BringSubviewToFront(jobButton);

      View.AddSubview(sessionTable);
      View.BringSubviewToFront(sessionTable);
      View.AddSubview(jobTable);
      View.BringSubviewToFront(jobTable);

      View.AddSubview(jobScroll.chooseJob);
      View.BringSubviewToFront(jobScroll.chooseJob);
    }
    /// <summary>
    /// Queries for all the sessions a user has recorded and lists them in a tableview
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public async void GetAllSessions(object sender, EventArgs e){
      listJobs.jView.Hidden = true;
      listSessions.sView.Hidden = false;
      jobSelected = false;
      jobButton.BackgroundColor = UIColor.LightGray;
      sessionSelected = true;
      sessionButton.BackgroundColor = UIColor.Blue;
      sessionButton.Enabled = false;
      jobTable.Hidden = true;
      if(listSessions.expanded)
        sessionTable.Hidden = false;

      if (listSessions.activityLoadingSessions != null)
        listSessions.activityLoadingSessions = null;
      
      listSessions.activityLoadingSessions = new UIActivityIndicatorView(new CGRect(0, 0, listSessions.sView.Bounds.Width, .75 * View.Bounds.Height));
      listSessions.activityLoadingSessions.Alpha = .4f;
      listSessions.activityLoadingSessions.Layer.CornerRadius = 8;
      listSessions.activityLoadingSessions.BackgroundColor = UIColor.DarkGray;

      listSessions.sView.AddSubview(listSessions.activityLoadingSessions);
      listSessions.sView.BringSubviewToFront(listSessions.activityLoadingSessions);

      listSessions.activityLoadingSessions.StartAnimating();

      var db = new SQLite.SQLiteConnection(_pathToDatabase);
      var result = db.Query<Session>("SELECT SID, sessionStart, sessionEnd FROM Session ORDER BY SID");
      List<SessionData> queriedSessions = new List<SessionData>();

      foreach (var item in result) {
        queriedSessions.Add(new SessionData(item.SID, item.sessionStart, item.sessionEnd));
      }
      ///table can only be 9 cells tall to allow for collapsing the menu still
      if(queriedSessions.Count > 9)
        sessionTable.Frame = new CGRect(.01 * View.Bounds.Width, .2 * View.Bounds.Height, listSessions.sView.Bounds.Width, 9 * .07 * View.Bounds.Height);
      else
        sessionTable.Frame = new CGRect(.01 * View.Bounds.Width, .2 * View.Bounds.Height, listSessions.sView.Bounds.Width, queriedSessions.Count * .07 * View.Bounds.Height);
      
      sessionTable.Source = new LoggingSessionSource(queriedSessions,.07f * View.Bounds.Height,jobScroll);
      sessionTable.ReloadData();

      await Task.Delay(TimeSpan.FromSeconds(2));
      listSessions.activityLoadingSessions.StopAnimating();
      sessionButton.Enabled = true;
    }

    public void GetAllMeasurementsForSession(){
      
    }

    public void GetAllMeasurements(){
      
    }
    /// <summary>
    /// Shows a textfield alert that lets the user enter a name for a new job that can be used to associate sessions
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void showNewJobAlert(object sender, EventArgs e){
      listJobs.createJob.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }

      UIAlertController createJobAlert = UIAlertController.Create ("Create Job", "Enter a name for the job you would like to create", UIAlertControllerStyle.Alert);
      createJobAlert.AddTextField(textField => {});
      createJobAlert.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
        createNewJob(createJobAlert.TextFields[0].Text);
      }));
      createJobAlert.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));
      vc.PresentViewController (createJobAlert, true, null);
    }
    /// <summary>
    /// Creates the Job table object with the name entered in the alert and then inserts that entry into the table
    /// </summary>
    /// <param name="Name">Name.</param>
    public void createNewJob(string Name){
      if (Name.Equals("")) {
        return;
      }
      var db = new SQLite.SQLiteConnection(_pathToDatabase);
      var job = new Job { jobName = Name };
      db.Insert(job);

      jobButton.SendActionForControlEvents(UIControlEvent.TouchUpInside);
    }

    public void AssociateSessionToJob(){
      
    }
    /// <summary>
    /// Queries for all the jobs a user has created and lists them in a tableview
    /// </summary>
    public async void GetAllJobs (object sender, EventArgs e){
      listSessions.sView.Hidden = true;
      listJobs.jView.Hidden = false;
      sessionSelected = false;
      sessionButton.BackgroundColor = UIColor.LightGray;
      jobSelected = true;
      jobButton.BackgroundColor = UIColor.Blue;
      jobButton.Enabled = false;
      sessionTable.Hidden = true;

      if(listJobs.expanded)
        jobTable.Hidden = false;

      if (listJobs.activityLoadingJobs != null)
        listJobs.activityLoadingJobs = null;

      listJobs.activityLoadingJobs = new UIActivityIndicatorView(new CGRect(0, 0, listJobs.jView.Bounds.Width, .75 * View.Bounds.Height));
      listJobs.activityLoadingJobs.Alpha = .4f;
      listJobs.activityLoadingJobs.Layer.CornerRadius = 8;
      listJobs.activityLoadingJobs.BackgroundColor = UIColor.DarkGray;

      listJobs.jView.AddSubview(listJobs.activityLoadingJobs);
      listJobs.jView.BringSubviewToFront(listJobs.activityLoadingJobs);

      listJobs.activityLoadingJobs.StartAnimating();

      var db = new SQLite.SQLiteConnection(_pathToDatabase);
      var result = db.Query<Job>("SELECT JID, jobName FROM Job ORDER BY JID");

      List<JobData> queriedJobs = new List<JobData>();
      foreach (var item in result) {
        queriedJobs.Add(new JobData(item.JID, item.jobName));
      }

      jobTable.Frame = new CGRect(.01 * View.Bounds.Width, .2 * View.Bounds.Height + listJobs.createJob.Bounds.Height, listJobs.jView.Bounds.Width, queriedJobs.Count * .07 * View.Bounds.Height);
      jobTable.Source = new LoggingJobSource(queriedJobs, .07f * View.Bounds.Height);
      jobTable.ReloadData();

      await Task.Delay(TimeSpan.FromSeconds(2));
      listJobs.activityLoadingJobs.StopAnimating();
      jobButton.Enabled = true;
    }

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


