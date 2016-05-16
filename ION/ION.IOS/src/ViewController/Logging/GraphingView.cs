using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using UIKit;
using Foundation;
using CoreGraphics;
using QuickLook;

using ION.Core.Database;
using ION.Core.Report;
using ION.Core.App;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.iOS;

using FlexCel.Core;
using FlexCel.Render;
using FlexCel.XlsAdapter;

namespace ION.IOS.ViewController.Logging
{
	public struct deviceReadings {
		public string type;
		public string serialNumber;
    public string name;
    public string nistDate;
    public int frnJID;
    public int SID;
		public List<double> readings;
		public List<DateTime> times;
	}

	public class GraphingView : UIView
	{

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public UIView gView;
		public UIView leftTrackerView;
		public UIView rightTrackerView;

    public UIViewController mainVC;

		public UIButton menuButton;
		public UIButton resetButton;
		public UIButton exportGraph;
		public UIButton scrollDown;
    public UIButton extendedDown;
		public UIButton scrollUp;
    public UIButton extendedUp;

		public UITableView graphTable;

    public UIPanGestureRecognizer lTrackerDrag;
		public UIPanGestureRecognizer lViewDrag;
    public UIPanGestureRecognizer rTrackerDrag;
		public UIPanGestureRecognizer rViewDrag;

		public UIImageView leftTrackerCircle;
		public UIImageView rightTrackerCircle;

		public UILabel subDates;

		public double trackerHeight;
		public int topCell;
		public int bottomCell;
    public string fileName;

		public List<deviceReadings> selectedData;
		public double dateMultiplier;

    public GraphingView (UIView mainView, UIViewController viewController, List<deviceReadings> pressuresTemperatures,ObservableCollection<int> sessions)
		{			
			selectedData = pressuresTemperatures;
      mainVC = viewController;
			topCell = 0;

      foreach (var device in selectedData) {
        var combineName = device.serialNumber + "/" + device.type;
        if (!ChosenDates.includeList.Contains(combineName)) {
          ChosenDates.includeList.Add(combineName);
        }
      }

      for(int i = 0; i < ChosenDates.includeList.Count;i++){
        var compareName = selectedData[i].serialNumber + "/" + selectedData[i].type;
        while(compareName != ChosenDates.includeList[i]){          
          var tmpMove = selectedData[i];
          selectedData.RemoveAt(i);
          selectedData.Add(tmpMove);
          compareName = selectedData[i].serialNumber + "/" + selectedData[i].type;
        }
      }
      getEarliestAndLatest();
      Console.WriteLine("Got earliest and latest");
			gView = new UIView (new CGRect (0,0, mainView.Bounds.Width, mainView.Bounds.Height));
			gView.BackgroundColor = UIColor.White;
			gView.Layer.CornerRadius = 8;
			gView.Layer.BorderColor = UIColor.Black.CGColor;
			gView.Layer.BorderWidth = 1f;

      if (ChosenDates.includeList.Count > 0) {
  			switch (ChosenDates.includeList.Count) {
          case 0:
            trackerHeight = 0;
            bottomCell = 0;
            break;
  				case 1:
  					trackerHeight = .175 * gView.Bounds.Height;
  					bottomCell = 0;
  					break;
  				case 2:
  					trackerHeight = .35 * gView.Bounds.Height;
  					bottomCell = 1;
  					break;
  				case 3:
  					trackerHeight = .525 * gView.Bounds.Height;
  					bottomCell = 2;
  					break;
  				case 4:
  					trackerHeight = .7 * gView.Bounds.Height;
  					bottomCell = 3;
  					break;
  				default:
  					trackerHeight = .7 * gView.Bounds.Height;
  					bottomCell = 4;
  					break;
  			}

        createButtons (sessions);

        graphTable = new UITableView(new CGRect(.1 * gView.Bounds.Width, .15 * gView.Bounds.Height, .85 * gView.Bounds.Width, trackerHeight));
        graphTable.BackgroundColor = UIColor.Clear;
        graphTable.Layer.BorderColor = UIColor.Black.CGColor;
        graphTable.Layer.BorderWidth = 1f;
        graphTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
        graphTable.ScrollEnabled = false;
        graphTable.RegisterClassForCellReuse(typeof(graphCell), "graphingCell");

        CreateGraphTrackers(mainView);

        graphTable.Source = new graphingTableSource(selectedData, gView, trackerHeight);
        graphTable.ReloadData();

        gView.AddSubview(graphTable);
        gView.AddSubview(leftTrackerView);
        gView.AddSubview(leftTrackerCircle);
        gView.AddSubview(rightTrackerView);
        gView.AddSubview(rightTrackerCircle);
        gView.AddSubview(subDates);
        gView.AddSubview(menuButton);
        gView.AddSubview(scrollUp);
        gView.AddSubview(extendedUp);
        gView.AddSubview(scrollDown);
        gView.AddSubview(extendedDown);
        dateMultiplier = (.8 * graphTable.Bounds.Width) / ChosenDates.allTimes[ChosenDates.latest.ToString()];
      } else {
        var noData = new UILabel(new CGRect(0,0,gView.Bounds.Width,60));
        noData.Text = "No Data Available";
        noData.TextAlignment = UITextAlignment.Center;
        gView.AddSubview(noData);
      }
		}

		/// <summary>
		/// Create the left and right side trackers for the graph for choosing
		/// a subsection of the data
		/// </summary>
		public void CreateGraphTrackers(UIView mainView){
			leftTrackerView = new UIView (new CGRect (.1 * gView.Bounds.Width,.15 * gView.Bounds.Height, 1, trackerHeight));
			leftTrackerView.BackgroundColor = UIColor.Gray;
			leftTrackerView.Alpha = .4f;

			leftTrackerCircle = new UIImageView (new CGRect (.1 * gView.Bounds.Width - 12, .15 * gView.Bounds.Height + trackerHeight, 24,26));
			leftTrackerCircle.Image = UIImage.FromBundle ("ic_tracker_circle");
			leftTrackerCircle.UserInteractionEnabled = true;

			lTrackerDrag = new UIPanGestureRecognizer (() => {
				var xPlot = leftTrackerCircle.Center.X - (.1 * mainView.Bounds.Width);
				if(lTrackerDrag.State == UIGestureRecognizerState.Changed){
          var index = (int)xPlot/(int)dateMultiplier;
          if(ChosenDates.allIndexes.ContainsKey(index)){
            ChosenDates.subLeft = DateTime.Parse(ChosenDates.allIndexes[index]);
          }
          subDates.Text = "Start: " + ChosenDates.subLeft.ToString () + "\nFinish: " + ChosenDates.subRight.ToString();
				}
				if(lTrackerDrag.LocationInView(mainView).X > (.1 * mainView.Bounds.Width) && lTrackerDrag.LocationInView(mainView).X < (.75 * mainView.Bounds.Width)){
					if(rightTrackerCircle.Center.X - 12 > lTrackerDrag.LocationInView(mainView).X){
						leftTrackerView.Frame = new CGRect(.1 * mainView.Bounds.Width,.15 * mainView.Bounds.Height, lTrackerDrag.LocationInView(mainView).X - (.1 * mainView.Bounds.Width), trackerHeight);
						leftTrackerCircle.Frame = new CGRect(lTrackerDrag.LocationInView(mainView).X - 12,.15 * mainView.Bounds.Height + trackerHeight,24,26);
					}
				}
				if (lTrackerDrag.State == UIGestureRecognizerState.Ended){          
          leftTrackerView.BackgroundColor = UIColor.FromRGB(49, 111, 18);
          subDates.TextColor = UIColor.Green;
					UIView.Transition(
						withView:subDates,
						duration:.5,
						options: UIViewAnimationOptions.TransitionCrossDissolve,
						animation: () =>{
              subDates.TextColor = UIColor.FromRGB(49, 111, 18);
							subDates.TextColor = UIColor.Black;
              leftTrackerView.BackgroundColor = UIColor.Gray;
						},
						completion: () =>{}
					);
				}
			});

      lViewDrag = new UIPanGestureRecognizer (() => {
        var xPlot = leftTrackerCircle.Center.X - (.1 * mainView.Bounds.Width);
        if(lViewDrag.State == UIGestureRecognizerState.Changed){
          var index = (int)xPlot/(int)dateMultiplier;
          if(ChosenDates.allIndexes.ContainsKey(index)){
            ChosenDates.subLeft = DateTime.Parse(ChosenDates.allIndexes[index]);
          }
          subDates.Text = "Start: " + ChosenDates.subLeft.ToString () + "\nFinish: " + ChosenDates.subRight.ToString();
        }
        if(lViewDrag.LocationInView(mainView).X > (.1 * mainView.Bounds.Width) && lViewDrag.LocationInView(mainView).X < (.75 * mainView.Bounds.Width)){
          if(rightTrackerCircle.Center.X - 12 > lViewDrag.LocationInView(mainView).X){
            leftTrackerView.Frame = new CGRect(.1 * mainView.Bounds.Width,.15 * mainView.Bounds.Height, lViewDrag.LocationInView(mainView).X - (.1 * mainView.Bounds.Width), trackerHeight);
            leftTrackerCircle.Frame = new CGRect(lViewDrag.LocationInView(mainView).X - 12,.15 * mainView.Bounds.Height + trackerHeight,24,26);
          }
        }
        if (lViewDrag.State == UIGestureRecognizerState.Ended){          
          leftTrackerView.BackgroundColor = UIColor.FromRGB(49, 111, 18);
          subDates.TextColor = UIColor.Green;
          UIView.Transition(
            withView:subDates,
            duration:.5,
            options: UIViewAnimationOptions.TransitionCrossDissolve,
            animation: () =>{
              subDates.TextColor = UIColor.FromRGB(49, 111, 18);
              subDates.TextColor = UIColor.Black;
              leftTrackerView.BackgroundColor = UIColor.Gray;
            },
            completion: () =>{}
          );
        }
      }); 
			 
			leftTrackerCircle.AddGestureRecognizer (lTrackerDrag);
      leftTrackerView.AddGestureRecognizer(lViewDrag);

			rightTrackerView = new UIView (new CGRect (.915 * graphTable.Bounds.Width,.15 * gView.Bounds.Height, 1, trackerHeight));
			rightTrackerView.BackgroundColor = UIColor.Gray;
			rightTrackerView.Alpha = .4f;

			rightTrackerCircle = new UIImageView (new CGRect (0, .15 * gView.Bounds.Height + trackerHeight,24, 26));
			rightTrackerCircle.Image = UIImage.FromBundle ("ic_tracker_circle");
			rightTrackerCircle.UserInteractionEnabled = true;

			var trackerRect = rightTrackerCircle.Center;
			trackerRect.X = rightTrackerView.Center.X - (.5f * rightTrackerView.Bounds.Width);
			rightTrackerCircle.Center = trackerRect;

			rTrackerDrag = new UIPanGestureRecognizer (() => {
				var xPlot = rightTrackerCircle.Center.X - (.1 * mainView.Bounds.Width);
				if(rTrackerDrag.State == UIGestureRecognizerState.Changed){          
          var index = (int)xPlot/(int)dateMultiplier;
          Console.WriteLine("Using index: " + index);
          if(ChosenDates.allIndexes.ContainsKey(index)){
            ChosenDates.subRight = DateTime.Parse(ChosenDates.allIndexes[index]);
          }
          subDates.Text = "Start: " + ChosenDates.subLeft.ToString () + "\nFinish: " + ChosenDates.subRight.ToString();
				}
        if(rTrackerDrag.LocationInView(mainView).X > (.11 * mainView.Bounds.Width) && rTrackerDrag.LocationInView(mainView).X < (.915 * graphTable.Bounds.Width)){
					if(leftTrackerCircle.Center.X + 12 < rTrackerDrag.LocationInView(mainView).X){
						rightTrackerView.Frame = new CGRect(rTrackerDrag.LocationInView(mainView).X,.15 * mainView.Bounds.Height,(.915 * graphTable.Bounds.Width) - rTrackerDrag.LocationInView(mainView).X, trackerHeight);
						rightTrackerCircle.Frame = new CGRect(rTrackerDrag.LocationInView(mainView).X - 12,.15 * mainView.Bounds.Height + trackerHeight,24,26);
					}
				}
				if (rTrackerDrag.State == UIGestureRecognizerState.Ended){
          
          rightTrackerView.BackgroundColor = UIColor.FromRGB(49, 111, 18);
					UIView.Transition(
						withView:subDates,
						duration:.5,
						options: UIViewAnimationOptions.TransitionCrossDissolve,
						animation: () =>{
              subDates.TextColor = UIColor.FromRGB(49, 111, 18);
							subDates.TextColor = UIColor.Black;
              rightTrackerView.BackgroundColor = UIColor.Gray;
						},
						completion: () =>{}
					);
				}
			});

      rViewDrag = new UIPanGestureRecognizer (() => {
        var xPlot = rViewDrag.LocationInView(mainView).X - (.1 * mainView.Bounds.Width);
        if(rViewDrag.State == UIGestureRecognizerState.Changed){          
          var index = (int)xPlot/(int)dateMultiplier;
          Console.WriteLine("Using index: " + index);
          if(ChosenDates.allIndexes.ContainsKey(index)){
            ChosenDates.subRight = DateTime.Parse(ChosenDates.allIndexes[index]);
          }
          subDates.Text = "Start: " + ChosenDates.subLeft.ToString () + "\nFinish: " + ChosenDates.subRight.ToString();
        }
        if(rViewDrag.LocationInView(mainView).X > (.11 * mainView.Bounds.Width) && rViewDrag.LocationInView(mainView).X < (.915 * graphTable.Bounds.Width)){
          if(leftTrackerCircle.Center.X + 12 < rViewDrag.LocationInView(mainView).X){
            rightTrackerView.Frame = new CGRect(rViewDrag.LocationInView(mainView).X,.15 * mainView.Bounds.Height,(.915 * graphTable.Bounds.Width) - rViewDrag.LocationInView(mainView).X, trackerHeight);
            rightTrackerCircle.Frame = new CGRect(rViewDrag.LocationInView(mainView).X - 12,.15 * mainView.Bounds.Height + trackerHeight,24,26);
          }
        }
        if (rViewDrag.State == UIGestureRecognizerState.Ended){

          rightTrackerView.BackgroundColor = UIColor.FromRGB(49, 111, 18);
          UIView.Transition(
            withView:subDates,
            duration:.5,
            options: UIViewAnimationOptions.TransitionCrossDissolve,
            animation: () =>{
              subDates.TextColor = UIColor.FromRGB(49, 111, 18);
              subDates.TextColor = UIColor.Black;
              rightTrackerView.BackgroundColor = UIColor.Gray;
            },
            completion: () =>{}
          );
        }
      });
				
			rightTrackerCircle.AddGestureRecognizer (rTrackerDrag);
      rightTrackerView.AddGestureRecognizer(rViewDrag);

			subDates = new UILabel (new CGRect (.1 * gView.Bounds.Width,0,.8 * gView.Bounds.Width,.1 * gView.Bounds.Height));
			subDates.AdjustsFontSizeToFitWidth = true;
			subDates.TextColor = UIColor.Black;
			subDates.TextAlignment = UITextAlignment.Left;
      subDates.Lines = 0;
			subDates.Text = "Start: " + ChosenDates.subLeft.ToString () + "\nFinish: " + ChosenDates.subRight.ToString();
			subDates.Font = UIFont.FromName("Helvetica-Bold", 22f);
			subDates.MinimumFontSize = 11.5f;
		}
		/// <summary>
		/// Creates the buttons to navigate and manipulate the graph and its included data
		/// </summary>
    public void createButtons(ObservableCollection<int> sessions){
      var deviceCount = ChosenDates.includeList.Count;
      resetButton = new UIButton (new CGRect (.05 * gView.Bounds.Width, .89 * mainVC.View.Bounds.Height, .25 * gView.Bounds.Width, .08 * gView.Bounds.Height));
			resetButton.BackgroundColor = UIColor.Red;
			resetButton.SetTitle ("Reset", UIControlState.Normal);
      resetButton.Font = UIFont.ItalicSystemFontOfSize(22);
			resetButton.Layer.CornerRadius = 8;

			resetButton.TouchUpInside += (sender, e) => {
        ChosenDates.subLeft = ChosenDates.earliest;
        ChosenDates.subRight = ChosenDates.latest;
				UIView.Animate(.15,0, UIViewAnimationOptions.CurveLinear,
					() =>{ 
						leftTrackerView.Frame = new CGRect(.1 * gView.Bounds.Width,.15 * gView.Bounds.Height, 1, trackerHeight);

						var trackerRect = leftTrackerCircle.Center;
						trackerRect.X = leftTrackerView.Center.X + (.5f * leftTrackerView.Bounds.Width);
						leftTrackerCircle.Center = trackerRect;
					},() => {});

				UIView.Animate(.15,0, UIViewAnimationOptions.CurveLinear,
					() =>{ 
						rightTrackerView.Frame = new CGRect(.915 * graphTable.Bounds.Width,.15 * gView.Bounds.Height, 1, trackerHeight);

						var trackerRect = rightTrackerCircle.Center;
						trackerRect.X = rightTrackerView.Center.X - (.5f * rightTrackerView.Bounds.Width);
						rightTrackerCircle.Center = trackerRect;
					},() => {});				
				resetButton.BackgroundColor = UIColor.Red;
        subDates.Text = "Start: " + ChosenDates.subLeft.ToString () + "\nFinish: " + ChosenDates.subRight.ToString();
			};
			resetButton.TouchDown += (sender, e) => {resetButton.BackgroundColor = UIColor.Blue;};
      resetButton.TouchUpOutside += (sender, e) => {resetButton.BackgroundColor = UIColor.Red;};

      exportGraph = new UIButton (new CGRect (.7 * gView.Bounds.Width, .89 * mainVC.View.Bounds.Height,.25 * gView.Bounds.Width,.08 * gView.Bounds.Height));
      exportGraph.BackgroundColor = UIColor.FromRGB(49, 111, 18);
			exportGraph.SetTitle ("Export", UIControlState.Normal); 
			exportGraph.Layer.CornerRadius = 8; 

			exportGraph.TouchUpInside += (sender, e) => {
        exportGraph.BackgroundColor = UIColor.FromRGB(49, 111, 18);

        ChooseReportType(sessions);
			}; 
			exportGraph.TouchDown += (sender, e) => {exportGraph.BackgroundColor = UIColor.Blue;};
      exportGraph.TouchUpOutside += (sender, e) => {exportGraph.BackgroundColor = UIColor.FromRGB(49, 111, 18);};

      menuButton = new UIButton (new CGRect (.9 * gView.Bounds.Width,5,.1 * gView.Bounds.Width,.075 * gView.Bounds.Width));
			menuButton.SetBackgroundImage (UIImage.FromBundle ("ic_settings"), UIControlState.Normal);

      var scrollPosition = .24 * gView.Bounds.Height;
      if (UserInterfaceIdiomIsPhone) {
        scrollPosition = .26 * gView.Bounds.Height;
      }
      scrollUp = new UIButton (new CGRect (0,scrollPosition,.1 * gView.Bounds.Width, .1 * gView.Bounds.Width));
			scrollUp.SetImage (UIImage.FromBundle ("ic_scrollup"), UIControlState.Normal);
			scrollUp.Enabled = false;

			scrollUp.TouchUpInside += (sender, e) => {
				graphTable.ScrollToRow(NSIndexPath.FromRowSection(topCell,0), UITableViewScrollPosition.Top, true);
				if(topCell <= 0){
					topCell = 0;
					scrollUp.Enabled = false;
          scrollDown.Enabled = true;
				} else {
					topCell = topCell - 1;
					bottomCell = bottomCell -1;
					scrollDown.Enabled = true;
				}
			};
      extendedUp = new UIButton(new CGRect(0,0,.1 * gView.Bounds.Width,scrollPosition));
      extendedUp.TouchUpInside += (sender, e) => {scrollUp.SendActionForControlEvents(UIControlEvent.TouchUpInside);};
      extendedUp.TouchDown += (sender, e) => {scrollUp.SendActionForControlEvents(UIControlEvent.TouchDown);};

      scrollDown = new UIButton (new CGRect (0, .65 * gView.Bounds.Height,.1 * gView.Bounds.Width, .1 * gView.Bounds.Width));
			scrollDown.SetImage (UIImage.FromBundle ("ic_scrolldown"), UIControlState.Normal);
			scrollDown.TouchUpInside += (sender, e) => {
				graphTable.ScrollToRow(NSIndexPath.FromRowSection(bottomCell,0), UITableViewScrollPosition.Bottom, true);
				if(bottomCell >= deviceCount - 1){
					bottomCell = deviceCount - 1;
					scrollDown.Enabled = false;
          scrollUp.Enabled = true;
				} else {
					topCell = topCell + 1;
					bottomCell = bottomCell + 1;
					scrollUp.Enabled = true;
				}
			};
      extendedDown = new UIButton(new CGRect(0,.75 * gView.Bounds.Height,.1 * gView.Bounds.Width,.25 * gView.Bounds.Height));
      extendedDown.TouchUpInside += (sender, e) => {scrollDown.SendActionForControlEvents(UIControlEvent.TouchUpInside);};
      extendedDown.TouchDown += (sender, e) => {scrollDown.SendActionForControlEvents(UIControlEvent.TouchDown);};

			if (ChosenDates.includeList.Count <= 4) {
				scrollUp.Hidden = true;
				scrollDown.Hidden = true;
			}
		}
    /// <summary>
    /// Gets the earliest and latest dates from the sessions chosen
    /// </summary>
    public void getEarliestAndLatest(){
      ChosenDates.earliest = DateTime.Now.AddDays(5);
      ChosenDates.latest = DateTime.Parse("1/1/1900 12:00:00");

      foreach (var entry in ChosenDates.allTimes) {
        if (DateTime.Parse(entry.Key) < ChosenDates.earliest) {
          ChosenDates.earliest = DateTime.Parse(entry.Key);
        }
        if (DateTime.Parse(entry.Key) > ChosenDates.latest) {
          ChosenDates.latest = DateTime.Parse(entry.Key);
        }
      }
      ChosenDates.subLeft = ChosenDates.earliest;
      ChosenDates.subRight = ChosenDates.latest;
    }
    /// <summary>
    /// Shows a popup that lets the user choose a spreadsheet or a pdf document of their chosen session/device/time values
    /// </summary>
    /// <param name="sessions">List of sessions included in the graphing</param>
    public void ChooseReportType(ObservableCollection<int> sessions){
      UIAlertView reportBox = new UIAlertView("Report", "Choose a format", null,"Cancel","Create Spreadsheet","Create PDF");
      reportBox.Show();
      reportBox.Clicked += async (sender, e) => {
        
        if(e.ButtonIndex.Equals(1)){          
          var sessionBreaks = new List<string>();
          var data = categorizeData(sessions,sessionBreaks);
          UIAlertView messageBox = new UIAlertView("Please Wait....", "Creating Spreadsheet", null,null,null);
          messageBox.Show();
          await Task.Delay(TimeSpan.FromMilliseconds (100));
          createSpreadsheet(messageBox,data,sessionBreaks);

        } else if (e.ButtonIndex.Equals(2)){          
          var sessionBreaks = new List<string>();
          var data = categorizeData(sessions,sessionBreaks);
          UIAlertView messageBox = new UIAlertView("Please Wait....", "Creating PDF", null,null,null);
          messageBox.Show();
          await Task.Delay(TimeSpan.FromMilliseconds (100));
          createPDF(messageBox,data,sessionBreaks);
        }
      };
    }
    /// <summary>
    /// Based on the devices included and the date range chosen, the times and measurements are collected for
    /// each device within the time window and packaged to each device
    /// </summary>
    /// <returns>The data.</returns>
    /// <param name="sessions">Sessions.</param>
    /// <param name="sessionBreaks">Session breaks.</param>
    public List<deviceReadings> categorizeData(ObservableCollection<int> sessions, List<string> sessionBreaks){
      var ion = AppState.context;
      var deviceList = new List<deviceReadings>();

      var paramList = new List<string>();

      foreach (var num in sessions) {
        paramList.Add('"' + num.ToString() + '"');
      }

      for (int i = 0; i < sessions.Count; i++) {
        var endtime = ion.database.Query<SensorMeasurementRow>("SELECT recordedDate FROM SensorMeasurementRow WHERE frn_SID = ? ORDER BY recordedDate DESC LIMIT 1",sessions[i]);
        if (endtime.Count > 0) {
          sessionBreaks.Add(endtime[0].recordedDate.ToLocalTime().ToString());
        }
      }

      foreach (var included in ChosenDates.includeList) {
        var package = new deviceReadings();
        package.times = new List<DateTime>();
        package.readings = new List<double>();

        var splits = included.Split('/');
        var sIndex = 0;
        package.serialNumber = splits[0];
        package.type = splits[1];

        if(splits[0].StartsWith("PT")){
          if (splits[1] == "Temperature") {
            sIndex = 1;
          }
        }

        var certInfo = ion.database.Query<LoggingDeviceRow>("SELECT nistDate, name  FROM LoggingDeviceRow WHERE serialNumber = ? ORDER BY nistDate DESC LIMIT 1", package.serialNumber);

        if (certInfo.Count > 0) {
          package.nistDate = certInfo[0].nistDate;
          package.name = certInfo[0].name;
        }

        var timesReadings = ion.database.Query<SensorMeasurementRow>("SELECT recordedDate, measurement FROM SensorMeasurementRow WHERE serialNumber = ? and sensorIndex = ?  AND recordedDate BETWEEN ? AND ? ORDER BY recordedDate ASC",package.serialNumber,sIndex,ChosenDates.subLeft, ChosenDates.subRight);

        foreach (var MID in timesReadings) {          
          package.times.Add(MID.recordedDate.ToLocalTime());
          package.readings.Add(MID.measurement);
        }
        deviceList.Add(package);      
      }

      return deviceList;
    }
    /// <summary>
    /// Takes the packaged data for included devices and loops through to associate each device's measurement
    /// to the correct timestamp
    /// </summary>
    /// <param name="messageBox">Pop up alert to dismiss after calculations are completed</param>
    /// <param name="dataList">List of the device packages</param>
    /// <param name="sessionBreaks">list of when a session ends to mark it</param>
    public void createSpreadsheet(UIAlertView messageBox, List<deviceReadings> dataList, List<string> sessionBreaks){
      messageBox.Dismissed += previewSpreadsheet;
      fileName = DateTime.UtcNow.ToLocalTime().ToString("MM-dd-yy hh:mm:ss tt") + ".xlsx";
       
      var masterTimes = new List<string>(); 

      foreach (var device in dataList) {
        foreach (var time in device.times) {
          if(!masterTimes.Contains(time.ToString())){
            masterTimes.Add(time.ToString());
          }
        }
      }
      masterTimes.Sort();

      XlsFile xls = new XlsFile(1, TExcelFileFormat.v2013, true);
      xls.AllowOverwritingFiles = true; 

      TFlxFormat blackout = xls.GetDefaultFormat; //1
      blackout.FillPattern = new TFlxFillPattern { Pattern = TFlxPatternStyle.Solid, FgColor = TExcelColor.FromIndex(1) };
      blackout.VAlignment = TVFlxAlignment.top;
      blackout.HAlignment = THFlxAlignment.center;
      blackout.Font.Color = TExcelColor.FromIndex(2);
      xls.AddFormat(blackout);

      TFlxFormat centerText = xls.GetDefaultFormat; //2
      centerText.VAlignment = TVFlxAlignment.top;
      centerText.HAlignment = THFlxAlignment.center;
      xls.AddFormat(centerText);

      TFlxFormat borderColor = xls.GetDefaultFormat; //3
      borderColor.Borders.Bottom.Color = TUIColor.FromArgb(0xFF, 0x33, 0x33);
      borderColor.Borders.Bottom.Style = TFlxBorderStyle.Medium;
      borderColor.Borders.Top.Style = TFlxBorderStyle.Thin;
      borderColor.Borders.Top.Color = TUIColor.FromArgb(171, 171, 171);
      borderColor.Borders.Left.Style = TFlxBorderStyle.Thin;
      borderColor.Borders.Left.Color = TUIColor.FromArgb(171, 171, 171);
      borderColor.Borders.Right.Style = TFlxBorderStyle.Thin;
      borderColor.Borders.Right.Color = TUIColor.FromArgb(171, 171, 171);
      borderColor.VAlignment = TVFlxAlignment.top;
      borderColor.HAlignment = THFlxAlignment.center;
      xls.AddFormat(borderColor);     

      xls.SetCellValue(1, 1, " ", 1);
      xls.SetCellValue(2, 1, " ", 1);
      xls.SetCellValue(3, 1, "Time",2);

      for (int i = 4; i < masterTimes.Count + 4; i++) { 
          xls.SetCellValue(i, 1, masterTimes[i-4], 2);
        if (sessionBreaks.Contains(masterTimes[i-4])) {
          xls.SetCellValue(i, 1, masterTimes[i-4], 3);
        } else {
          xls.SetCellValue(i,1, masterTimes[i-4], 2);
        }
      }

      for (int i = 2; i < dataList.Count + 2; i++) {

        var defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_pressure");

        if (dataList[i - 2].type.Equals("Temperature")) {
          defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_temperature");
        } else if (dataList[i - 2].type.Equals("Vacuum")) {
          defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_vacuum");
        }
        var lookup = ION.Core.Sensors.UnitLookup.GetUnit(Convert.ToInt32(defaultUnit));
        if (defaultUnit.Equals("7")) {
          xls.SetCellValue(1, i, dataList[i - 2].type + "(psig/inHg)", 1);
        } else if (defaultUnit.Equals("8")){
          xls.SetCellValue(1, i, dataList[i - 2].type + "(kg/cm²/cmHg)", 1);
        } else {
          xls.SetCellValue(1, i, dataList[i - 2].type + "(" + lookup + ")", 1);
        }
        xls.SetCellValue(2, i, dataList[i - 2].serialNumber,2);

        xls.SetCellValue(3, i, dataList[i - 2].name, 2);
       
        var standardUnit = lookup.standardUnit;
        var rowIndex = 4;
        var compareIndex = 0;

        for (int t = 0; t < masterTimes.Count; t++) {
          if (compareIndex < dataList[i - 2].times.Count) {
            if (masterTimes[t].Equals(dataList[i - 2].times[compareIndex].ToString())) {
              var workingValue = standardUnit.OfScalar(dataList[i - 2].readings[compareIndex]);
              var finalValue = workingValue.ConvertTo(lookup);
              var formatValue = finalValue.amount.ToString("N");

              if (sessionBreaks.Contains(dataList[i - 2].times[compareIndex].ToString())) {
                xls.SetCellValue(rowIndex, i, Convert.ToDouble(formatValue), 3);
              } else {
                xls.SetCellValue(rowIndex, i, Convert.ToDouble(formatValue), 2);
              }
              compareIndex++;
            } else {
              if (sessionBreaks.Contains(masterTimes[t].ToString())) {
                xls.SetCellValue(rowIndex, i, " ", 3);
              } else {
                xls.SetCellValue(rowIndex, i, " ", 2);
              }
            }
          } else {
            if (sessionBreaks.Contains(masterTimes[t].ToString())) {
              xls.SetCellValue(rowIndex, i, " ", 3);
            } else {
              xls.SetCellValue(rowIndex, i, " ", 2);
            }
          }
          rowIndex++;
        }
        xls.AutofitCol(i, false, 1.1);
      }

      xls.AutofitCol(1, false, 1.1);

      //var dir = AppState.context.fileManager.GetApplicationInternalDirectory();
      xls.Save(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName));
      //xls.Save(dir.fullPath + fileName);

      messageBox.DismissWithClickedButtonIndex(0, false);
    }
    /// <summary>
    /// Launches a viewcontroller with the created spreadsheet so a user can inspect what was created
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void previewSpreadsheet(object sender, EventArgs e){
      var dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);
      QLPreviewItemBundle prevItem = new QLPreviewItemBundle (fileName, dir);     
      QLPreviewController previewController = new QLPreviewController ();
      previewController.DataSource = new PreviewControllerDS (prevItem);
      mainVC.NavigationController.PushViewController (previewController, true);
    }

    public void sendSpreadsheetEmail(object sender, EventArgs e){
      Console.WriteLine("Sending an email");
    }

    /// <summary>
    /// Takes the packaged device data to create a pdf with each device's times and measurements
    /// </summary>
    /// <param name="messageBox">Pop up alert to dismiss after calculations are completed</param>

    /// <param name="dataList">list of packaged device data</param>
    /// <param name="sessionBreaks">list of session break times to mark report</param>
    public void createPDF(UIAlertView messageBox, List<deviceReadings> dataList, List<string> sessionBreaks){
      messageBox.Dismissed += previewPDF;
      fileName = DateTime.UtcNow.ToLocalTime().ToString("MM-dd-yy hh:mm:ss tt") + ".pdf";
       
      var masterTimes = new List<string>();

      foreach (var device in dataList) {
        foreach (var time in device.times) {
          if(!masterTimes.Contains(time.ToString())){
            masterTimes.Add(time.ToString());
          }
        }
      }
      masterTimes.Sort();

      var scaleReduction = 100 - (dataList.Count * 6);
      XlsFile xls = new XlsFile(1, TExcelFileFormat.v2013, true);
      xls.AllowOverwritingFiles = true; 
      xls.PrintScale = scaleReduction;

      TFlxFormat blackout = xls.GetDefaultFormat; //1
      blackout.FillPattern = new TFlxFillPattern { Pattern = TFlxPatternStyle.Solid, FgColor = TExcelColor.FromIndex(1) };
      blackout.VAlignment = TVFlxAlignment.top;
      blackout.HAlignment = THFlxAlignment.center;
      blackout.Font.Color = TExcelColor.FromIndex(2);
      xls.AddFormat(blackout);

      TFlxFormat centerText = xls.GetDefaultFormat; //2
      centerText.VAlignment = TVFlxAlignment.top;
      centerText.HAlignment = THFlxAlignment.center;
      xls.AddFormat(centerText);

      TFlxFormat borderColor = xls.GetDefaultFormat; //3
      borderColor.Borders.Bottom.Color = TUIColor.FromArgb(0xFF, 0x33, 0x33);
      borderColor.Borders.Bottom.Style = TFlxBorderStyle.Medium;
      borderColor.VAlignment = TVFlxAlignment.top;
      borderColor.HAlignment = THFlxAlignment.center;
      xls.AddFormat(borderColor);

      var headerStartIndex = 3;

      if (dataList.Count > 3) {
        headerStartIndex = (dataList.Count + 1) / 2;
      }

      xls.MergeCells(1, headerStartIndex, 1, headerStartIndex + 2);
      xls.SetCellValue(1, headerStartIndex, "Devices Used",2);
      xls.SetCellValue(2, headerStartIndex, "Serial Number", 1);
      xls.SetCellValue(2, headerStartIndex + 1, "Name", 1);
      xls.SetCellValue(2, headerStartIndex + 2, "Nist Date", 1);

      int deviceCellIndex = 3;
      foreach (var device in dataList) {
        xls.SetCellValue(deviceCellIndex, headerStartIndex, device.serialNumber, 2);
        xls.SetCellValue(deviceCellIndex, headerStartIndex + 1, device.name, 2);
        if (device.nistDate == "") {
          xls.SetCellValue(deviceCellIndex, headerStartIndex + 2, "N/A", 2);
        } else {
          xls.SetCellValue(deviceCellIndex, headerStartIndex + 2, device.nistDate, 2);        
        }
        deviceCellIndex++;
      }
      deviceCellIndex++;
      xls.MergeCells(deviceCellIndex, headerStartIndex + 1, deviceCellIndex, headerStartIndex + 2);
      xls.SetCellValue(deviceCellIndex, headerStartIndex, "Report Created", 1);
      xls.SetCellValue(deviceCellIndex, headerStartIndex + 1, DateTime.Now.ToLocalTime().ToString(), 2);
      deviceCellIndex++;
      xls.MergeCells(deviceCellIndex, headerStartIndex, deviceCellIndex, headerStartIndex + 2);
      xls.SetCellValue(deviceCellIndex, headerStartIndex, "Report Dates", 1);
      deviceCellIndex++;
      xls.MergeCells(deviceCellIndex, headerStartIndex, deviceCellIndex, headerStartIndex + 2);
      xls.SetCellValue(deviceCellIndex, headerStartIndex, ChosenDates.subLeft + " - " + ChosenDates.subRight, 2);
      deviceCellIndex+= 2;

      TXlsNamedRange Range;
      string RangeName;
      RangeName = TXlsNamedRange.GetInternalName(InternalNameRange.Print_Titles);
      Range = new TXlsNamedRange(RangeName, 1, 1, deviceCellIndex, 1, deviceCellIndex + 2, dataList.Count + 1, 32);
      xls.SetNamedRange(Range);

      xls.SetCellValue(deviceCellIndex, 1, " ", 1);
      deviceCellIndex++;
      xls.SetCellValue(deviceCellIndex, 1, " ", 1);
      deviceCellIndex++;
      xls.SetCellValue(deviceCellIndex, 1, "Time",2);
      deviceCellIndex++;

      for (int i = deviceCellIndex; i < masterTimes.Count + deviceCellIndex; i++) { 
        xls.SetCellValue(i, 1, masterTimes[i-deviceCellIndex], 2);
        if (sessionBreaks.Contains(masterTimes[i-deviceCellIndex])) {
          xls.SetCellValue(i, 1, masterTimes[i-deviceCellIndex], 3);
        } else {
          xls.SetCellValue(i,1, masterTimes[i-deviceCellIndex], 2);
        }
      }
      deviceCellIndex -= 3;
      var measStartIndex = deviceCellIndex;
      for (int i = 2; i < dataList.Count + 2; i++) {

        var defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_pressure");

        if (dataList[i - 2].type.Equals("Temperature")) {
          defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_temperature");
        } else if (dataList[i - 2].type.Equals("Vacuum")) {
          defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_vacuum");
        }

        var lookup = ION.Core.Sensors.UnitLookup.GetUnit(Convert.ToInt32(defaultUnit));

        if (defaultUnit.Equals("7")) {
          xls.SetCellValue(measStartIndex, i, dataList[i - 2].type + "(psig/inHg)", 1);
        } else if (defaultUnit.Equals("8")){
          xls.SetCellValue(measStartIndex, i, dataList[i - 2].type + "(kg/cm²/cmHg)", 1);
        } else {
          xls.SetCellValue(measStartIndex, i, dataList[i - 2].type + "(" + lookup + ")", 1);
        }
        measStartIndex++;
        xls.SetCellValue(measStartIndex, i, dataList[i - 2].serialNumber,2);
        measStartIndex++;
        xls.SetCellValue(measStartIndex, i, dataList[i - 2].name, 2);
        measStartIndex++;

        var standardUnit = lookup.standardUnit;
        var rowIndex = measStartIndex;
        var compareIndex = 0;

        for (int t = 0; t < masterTimes.Count; t++) {
          if (compareIndex < dataList[i - 2].times.Count) {
            if (masterTimes[t].Equals(dataList[i - 2].times[compareIndex].ToString())) {
              var workingValue = standardUnit.OfScalar(dataList[i - 2].readings[compareIndex]);
              var finalValue = workingValue.ConvertTo(lookup);
              var formatValue = finalValue.amount.ToString("N");

              if (sessionBreaks.Contains(dataList[i - 2].times[compareIndex].ToString())) {
                xls.SetCellValue(rowIndex, i, Convert.ToDouble(formatValue), 3);
              } else {
                xls.SetCellValue(rowIndex, i, Convert.ToDouble(formatValue), 2);
              }
              compareIndex++;
            } else {
              if (sessionBreaks.Contains(masterTimes[t].ToString())) {
                xls.SetCellValue(rowIndex, i, " ", 3);
              } else {
                xls.SetCellValue(rowIndex, i, " ", 2);
              }
            }
          } else {
            if (sessionBreaks.Contains(masterTimes[t].ToString())) {
              xls.SetCellValue(rowIndex, i, " ", 3);
            } else {
              xls.SetCellValue(rowIndex, i, " ", 2);
            }
          }
          rowIndex++;
        }
        xls.AutofitCol(i, false, 1.1);
        measStartIndex = deviceCellIndex;
      }

      xls.AutofitCol(1, false, 1.1);

      FlexCelPdfExport pdfExport = new FlexCelPdfExport(xls);
      pdfExport.AllowOverwritingFiles = true;

      var pdfPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);

      try{
        using(FileStream Pdf = new FileStream(pdfPath,FileMode.Create)){
          pdfExport.BeginExport(Pdf);

          pdfExport.ExportSheet();

          pdfExport.EndExport();
        }
      }catch (Exception e){
        Console.WriteLine("Exception: " + e);
      }

      messageBox.DismissWithClickedButtonIndex(0, false);
    }

    public void previewPDF(object sender, EventArgs e){
      var dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);
      QLPreviewItemBundle prevItem = new QLPreviewItemBundle (fileName, dir);
      QLPreviewController previewController = new QLPreviewController ();
      previewController.DataSource = new PreviewControllerDS (prevItem);
      mainVC.NavigationController.PushViewController (previewController, true);
    }
      
    /*
    /// <summary>
    /// Adjusts the second y axis to scale along side the first y axis and the x axis
    /// </summary>
    private void AdjustY1Extent()
    {
      RAX.Zoom (LAX.ActualMinimum, LAX.ActualMaximum);
    }
    */
	}
}