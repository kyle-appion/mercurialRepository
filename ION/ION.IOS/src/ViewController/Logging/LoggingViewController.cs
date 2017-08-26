namespace ION.IOS.ViewController.Logging {   

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using CoreGraphics;
	using UIKit;
	using System.IO;
	//using System.IO.IsolatedStorage;
	//using SQLite;
	//using System.Threading.Tasks;

	using Appion.Commons.Util;


	using ION.Core.App;
	using ION.Core.Devices;
  using System.Threading.Tasks;

  public static class ChosenDates {
    public static DateTime subLeft;
    public static DateTime subRight;
    public static DateTime earliest;
    public static DateTime latest;
    public static List<string> includeList;
    public static Dictionary<string,int> allTimes;
    public static Dictionary<int,string> allIndexes;
    public static int extraPlots;
    public static int breakPoints;
  }

  public partial class LoggingViewController : BaseIONViewController {

    public ChooseReporting reportingSection;
    public ChooseData dataSection;
    public ChooseSaved savedReportsSection;
    //public ChooseGraphing graphingSection;   
    private IION ion;
    public UIActivityIndicatorView activityLoadingGraphs;
    public ObservableCollection<int> selectedSessions;
		public UIButton showGraphButton;

		public LoggingViewController(IntPtr handle) : base(handle) {
    
    }

    public override void ViewDidLoad() {
      base.ViewDidLoad();
      // Perform any additional setup after loading the view, typically from a nib.
      View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));
      InitNavigationBar("ic_graph_menu", false);
      AutomaticallyAdjustsScrollViewInsets = false;
      backAction = () => {
        root.navigation.ToggleMenu();
      };
      Title = Util.Strings.Report.REPORTS;
      ChosenDates.includeList = new List<string> ();
      selectedSessions = new ObservableCollection<int>();
      ChosenDates.allTimes = new Dictionary<string,int> ();
      ion = AppState.context;

      selectedSessions.CollectionChanged += checkForSelected;
      SetupLoggingUI();
    }
    /// <summary>
    /// Creates the job and session views and adds their views/subviews to the mainview
    /// </summary>
    public async void SetupLoggingUI(){
      await Task.Delay(TimeSpan.FromMilliseconds(2));
      reportingSection = new ChooseReporting(containerView);
      reportingSection.savedReports.TouchUpInside += loadSavedReports;
      reportingSection.newReport.TouchUpInside += showDataSection;

      dataSection = new ChooseData(containerView,selectedSessions,this);

      savedReportsSection = new ChooseSaved(containerView);

      //graphingSection = new ChooseGraphing(containerView, dataSection);

			showGraphButton = new UIButton(new CGRect(.55 * containerView.Bounds.Width, .895 * containerView.Bounds.Height, .4 * containerView.Bounds.Width, .07 * containerView.Bounds.Height));
      showGraphButton.Layer.CornerRadius = 5f;
			showGraphButton.Layer.BorderColor = UIColor.Black.CGColor;
			showGraphButton.Layer.BorderWidth = 2f;
			showGraphButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			showGraphButton.SetTitle(Util.Strings.Report.BUILDREPORT, UIControlState.Normal);
			showGraphButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			showGraphButton.Enabled = false;
			showGraphButton.Alpha = .6f;
			showGraphButton.ClipsToBounds = true;

			///user feedback for button press
			showGraphButton.TouchUpInside += showGraphSection;

			containerView.AddSubview(reportingSection.typeView);
      containerView.AddSubview(dataSection.DataType);
      containerView.AddSubview(savedReportsSection.showReports);
      //containerView.AddSubview(graphingSection.graphingType);
      containerView.AddSubview(showGraphButton);
    }
     /// <summary>
     /// Loads the saved reports.
     /// </summary>
     /// <param name="sender">Sender.</param>
     /// <param name="e">E.</param>
    public void loadSavedReports (object sender, EventArgs e){
			dataSection.DataType.Hidden = true;       
      dataSection.selectedSessions.Clear();

      showGraphButton.Enabled = false;
      showGraphButton.Alpha = .6f;
      showGraphButton.Hidden = true;

      savedReportsSection.showReports.Hidden = false;
    }
    /// <summary>
    /// Triggers the job and session section to appear/expand from the new reports button in the reporting section
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void showDataSection (object sender, EventArgs e){
      savedReportsSection.showReports.Hidden = true;

      dataSection.DataType.Hidden = false;
      showGraphButton.Hidden = false;
    }

    /// <summary>
    /// Expands the graphing section 
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void showGraphSection(object sender, EventArgs e){
      ////launch new viewcontroller that handles chosen graphing section functionality
      var gvc = InflateViewController<GraphingViewController>(VC_GRAPHING);
      gvc.checkData = dataSection;
      gvc.selectedSessions = selectedSessions;
      NavigationController.PushViewController(gvc, true);
    }

		public void checkForSelected(object sender, EventArgs e) {
			if (selectedSessions.Count > 0) {
				showGraphButton.Enabled = true;
				showGraphButton.Alpha = 1f;
			} else {
				showGraphButton.Enabled = false;
				showGraphButton.Alpha = .6f;
			}
		}

    public override void ViewDidAppear(bool animated) {
      if (dataSection != null && dataSection.DataType != null) {
        dataSection.ReloadAllJobs();
      }
    }

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
      Console.WriteLine("MEMORY HOLY CRAP ITS GONE!");
    }
  }
}


