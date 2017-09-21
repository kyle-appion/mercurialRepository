using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.IOS.ViewController;
using ION.Core.App;
using System.Globalization;
using System.Threading.Tasks;

namespace ION.IOS.ViewController.JobManager {
  public partial class JobViewController : BaseIONViewController {
		/// <summary>
		/// The delegate that is used to update an active job setting.
		/// </summary>
		public delegate void SetActiveJob();

		/// <summary>
		/// The action that will be fired when the user selects a job to be active
		/// </summary>
		/// </summary>
		public SetActiveJob activeJobChosen { get; set; }

    public UIRefreshControl jobRefresh;
    IION ion;
    public bool pushed = false;
    public UILabel savedJobLabel;
    public UIButton activeJobImage;
    public UITableView jobTable;
    public UIView activeJobView;
		public UILabel jobIDLabel;
		public UILabel jobNameLabel;
		public UILabel customerNumberLabel;
		public UILabel dispatchNumberLabel;
		public UILabel poNumberLabel;

		public UILabel idValueLabel;
		public UILabel nameValueLabel;
		public UILabel customerValueLabel;
		public UILabel dispatchValueLabel;
		public UILabel poValueLabel;

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

      Title = Util.Strings.Job.JOBSETTINGS;
      var addJobButton = new UIButton(new CGRect(0,0,50,43));
      addJobButton.TouchUpInside += (sender, e) => {
        var jevc = this.InflateViewController<JobEditViewController>(BaseIONViewController.VC_EDIT_JOB);
        jevc.frnJID = 0;
        this.NavigationController.PushViewController(jevc,true);
      };  
      addJobButton.SetImage(UIImage.FromBundle("ic_device_add"), UIControlState.Normal);
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
      activeJobChosen += setupActiveJob;
      setupManager(jobList);
    }

    public async void setupManager(List<int> jobList){
      await Task.Delay(TimeSpan.FromMilliseconds(2));
      Console.WriteLine("Bounds: " + View.Bounds);
      savedJobLabel = new UILabel(new CGRect(0,110,containerView.Bounds.Width,40));
      savedJobLabel.Text = "Saved Jobs";
      savedJobLabel.TextAlignment = UITextAlignment.Center;
      savedJobLabel.Font = UIFont.BoldSystemFontOfSize(17f);
      savedJobLabel.BackgroundColor = UIColor.FromRGB(230, 103, 39);

      activeJobView = new UIView(new CGRect(.03 * View.Bounds.Width, 0, .94 * containerView.Bounds.Width, 102));
      activeJobView.BackgroundColor = UIColor.White;
      activeJobView.Layer.CornerRadius = 5f;

      jobIDLabel = new UILabel(new CGRect(5, 5, .4 * activeJobView.Bounds.Width, .19 * activeJobView.Bounds.Height));
      jobIDLabel.Text = "Job ID: ";
      jobIDLabel.Font = UIFont.BoldSystemFontOfSize(15f);

			jobNameLabel = new UILabel(new CGRect(5, .19 * activeJobView.Bounds.Height + 5, .4 * activeJobView.Bounds.Width, .19 * activeJobView.Bounds.Height));
			jobNameLabel.Text = "Job Name: ";
			jobNameLabel.Font = UIFont.BoldSystemFontOfSize(15f);

			customerNumberLabel = new UILabel(new CGRect(5, .38 * activeJobView.Bounds.Height + 5, .4 * activeJobView.Bounds.Width, .19 * activeJobView.Bounds.Height));
			customerNumberLabel.Text = "Customer #: ";
			customerNumberLabel.Font = UIFont.BoldSystemFontOfSize(15f);

			dispatchNumberLabel = new UILabel(new CGRect(5, .57 * activeJobView.Bounds.Height + 5, .4 * activeJobView.Bounds.Width, .19 * activeJobView.Bounds.Height));
			dispatchNumberLabel.Text = "Dispatch #: ";
			dispatchNumberLabel.Font = UIFont.BoldSystemFontOfSize(15f);

			poNumberLabel = new UILabel(new CGRect(5, .76 * activeJobView.Bounds.Height + 5, .4 * activeJobView.Bounds.Width, .19 * activeJobView.Bounds.Height));
			poNumberLabel.Text = "Purchase Order #: ";
			poNumberLabel.Font = UIFont.BoldSystemFontOfSize(15f);

			idValueLabel = new UILabel(new CGRect(.4 * activeJobView.Bounds.Width, 5, .4 * activeJobView.Bounds.Width, (.19 * activeJobView.Bounds.Height) - 5));
			nameValueLabel = new UILabel(new CGRect(.4 * activeJobView.Bounds.Width, (.19 * activeJobView.Bounds.Height) + 5, .4 * activeJobView.Bounds.Width, (.19 * activeJobView.Bounds.Height) - 5));
			customerValueLabel = new UILabel(new CGRect(.4 * activeJobView.Bounds.Width, (.38 * activeJobView.Bounds.Height) + 5, .4 * activeJobView.Bounds.Width, (.19 * activeJobView.Bounds.Height) - 5));
			dispatchValueLabel = new UILabel(new CGRect(.4 * activeJobView.Bounds.Width, (.57 * activeJobView.Bounds.Height) + 5, .4 * activeJobView.Bounds.Width, (.19 * activeJobView.Bounds.Height) - 5));
			poValueLabel = new UILabel(new CGRect(.4 * activeJobView.Bounds.Width, (.76 * activeJobView.Bounds.Height) + 5, .4 * activeJobView.Bounds.Width, (.19 * activeJobView.Bounds.Height) - 5));

			jobTable = new UITableView(new CGRect(.03 * containerView.Bounds.Width,150, .94 * containerView.Bounds.Width, containerView.Bounds.Height - 150));
      jobTable.Layer.CornerRadius = 5f;
			jobTable.BackgroundColor = UIColor.Clear;
			jobTable.RegisterClassForCellReuse(typeof(CreatedJobCell), "createdJobCell");
			jobTable.ClipsToBounds = true;
			jobTable.ContentInset = UIEdgeInsets.Zero;
			jobTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			jobTable.Source = new CreatedJobSource(jobList, activeJobChosen, this);
			jobTable.ReloadData();

			jobRefresh = new UIRefreshControl();
			jobRefresh.ValueChanged += (sender, e) => {
				refreshJobList(this);
			};

			jobTable.InsertSubview(jobRefresh, 0);
			jobTable.SendSubviewToBack(jobRefresh);

      activeJobImage = new UIButton(new CGRect(.84 * activeJobView.Bounds.Width, .25 * activeJobView.Bounds.Height, .5 * activeJobView.Bounds.Height, .5 * activeJobView.Bounds.Height));
			activeJobImage.SetImage(UIImage.FromBundle("ic_bookmark").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
      activeJobImage.TintColor = UIColor.Yellow; 
			activeJobImage.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			activeJobImage.Layer.CornerRadius = 5f;
			activeJobImage.ContentMode = UIViewContentMode.Center;

			activeJobImage.TouchUpInside += (sender, e) => {
        Console.WriteLine("Unlinking active job");
        ion.preferences.jobs.activeJob = 0;
        setupActiveJob();
			};

      containerView.AddSubview(savedJobLabel);
			containerView.AddSubview(activeJobView);
			containerView.AddSubview(jobTable);
			activeJobView.AddSubview(jobIDLabel);
			activeJobView.AddSubview(jobNameLabel);
			activeJobView.AddSubview(customerNumberLabel);
			activeJobView.AddSubview(dispatchNumberLabel);
			activeJobView.AddSubview(poNumberLabel);

			activeJobView.AddSubview(idValueLabel);
			activeJobView.AddSubview(nameValueLabel);
			activeJobView.AddSubview(customerValueLabel);
			activeJobView.AddSubview(dispatchValueLabel);
			activeJobView.AddSubview(poValueLabel);

			activeJobView.AddSubview(activeJobImage);
    }

    public void refreshJobList(JobViewController manageVC){
      var jobs = ion.database.Query<ION.Core.Database.JobRow>("SELECT DISTINCT JID FROM JobRow ORDER BY JID DESC");

			var jobList = new List<int>();
      foreach (var job in jobs) {
        jobList.Add(job.JID);
      }

      jobTable.Source = new CreatedJobSource(jobList, activeJobChosen, this);
      jobTable.ReloadData();
      jobRefresh.EndRefreshing();
    }

    public async void setupActiveJob(){
      await Task.Delay(TimeSpan.FromMilliseconds(2));
      if(ion.preferences.jobs.activeJob == 0){
        activeJobView.Frame = new CGRect(.03 * View.Bounds.Width, 0, .94 * containerView.Bounds.Width, 1);
        activeJobView.Hidden = true;
        savedJobLabel.Frame = new CGRect(0, 5, containerView.Bounds.Width, 40);
        jobTable.Frame = new CGRect(.03 * containerView.Bounds.Width, 45, .94 * containerView.Bounds.Width, containerView.Bounds.Height - 45);
      } else {
				activeJobView.Frame = new CGRect(.03 * View.Bounds.Width, 0, .94 * containerView.Bounds.Width, 102);
				savedJobLabel.Frame = new CGRect(0, 110, containerView.Bounds.Width, 40);
				jobTable.Frame = new CGRect(.03 * containerView.Bounds.Width, 150, .94 * containerView.Bounds.Width, containerView.Bounds.Height - 150);

				var jobQuery = ion.database.Query<ION.Core.Database.JobRow>("SELECT * FROM JobRow WHERE JID = ?", ion.preferences.jobs.activeJob);

				foreach (var job in jobQuery) {
					idValueLabel.Text = job.JID.ToString();
					nameValueLabel.Text = job.jobName;
					customerValueLabel.Text = job.customerNumber;
					dispatchValueLabel.Text = job.dispatchNumber;
					poValueLabel.Text = job.poNumber;
				}

				activeJobView.Hidden = false;
			}
      jobTable.ReloadData();
    }
    
    public override void ViewDidAppear(bool animated) {
      if (jobTable != null) {
				var jobQuery = ion.database.Query<ION.Core.Database.JobRow>("SELECT DISTINCT JID FROM JobRow ORDER BY JID DESC");
				var jobList = new List<int>();
				foreach (var id in jobQuery) {
					jobList.Add(id.JID);
				}

        jobTable.Source = new CreatedJobSource(jobList, activeJobChosen, this);
        jobTable.ReloadData();
      }
      setupActiveJob();
    }

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


