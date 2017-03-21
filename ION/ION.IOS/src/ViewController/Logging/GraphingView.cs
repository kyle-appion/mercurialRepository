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
using ION.Core.Devices;

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
    public int sensorIndex;
		public List<double> readings;
		public List<DateTime> times;
		public double min;
		public double max;
		public double avg;
	}

	public class GraphingView : UIView
	{

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}
		public ReportType exportSelect;

		public UIView gView;
		public UIView leftTrackerView;
		public UIView rightTrackerView;

    public IONPrimaryScreenController mainVC;

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
    public int fallOff = 0;
 		ObservableCollection<int> sessions;
		public List<deviceReadings> selectedData;
		public double dateMultiplier;

    public GraphingView (UIView mainView, IONPrimaryScreenController viewController, List<deviceReadings> pressuresTemperatures, ObservableCollection<int> selectedSessions)
		{
			selectedData = pressuresTemperatures;
			sessions = selectedSessions;
      mainVC = viewController;
			topCell = 0;
      foreach (var device in selectedData) {
        var combineName = device.serialNumber + "/" + device.sensorIndex;
        //Console.WriteLine("Looking at combined name " + combineName);
        if (!ChosenDates.includeList.Contains(combineName)) {
        	//Console.WriteLine("Adding device to list " + combineName);
          ChosenDates.includeList.Add(combineName);
        }
      }
      
      for(int i = 0; i < ChosenDates.includeList.Count;i++){
        var compareName = selectedData[i].serialNumber + "/" + selectedData[i].sensorIndex;
        //Console.WriteLine("Looking at name " + compareName);
        while(compareName != ChosenDates.includeList[i]){
					//Console.WriteLine("Moving through include list " + ChosenDates.includeList[i]);       
          var tmpMove = selectedData[i];
          selectedData.RemoveAt(i);
          selectedData.Add(tmpMove);
          compareName = selectedData[i].serialNumber + "/" + selectedData[i].sensorIndex;
        }
      }
      //Console.WriteLine("Getting earliest and latest");
      getEarliestAndLatest();

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
				exportSelect = new ReportType(gView);

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
				gView.AddSubview(exportSelect.blackoutView);
				gView.AddSubview(exportSelect.popupView);
				gView.BringSubviewToFront(exportSelect.popupView);        
        if(ChosenDates.allTimes[ChosenDates.latest.ToString()] == 0){
					dateMultiplier = 1;
				} else {
        	dateMultiplier = (.8 * graphTable.Bounds.Width) / ChosenDates.allTimes[ChosenDates.latest.ToString()];
				}      
			} else {
        var noData = new UILabel(new CGRect(0,0,gView.Bounds.Width,60));
        noData.Text = Util.Strings.Report.NODATA;
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

			leftTrackerCircle = new UIImageView (new CGRect (.1 * gView.Bounds.Width, .15 * gView.Bounds.Height + trackerHeight, 30,33));
			leftTrackerCircle.Image = UIImage.FromBundle ("ic_left_tracker");
			leftTrackerCircle.UserInteractionEnabled = true;

			lTrackerDrag = new UIPanGestureRecognizer (() => {
				var xPlot = (leftTrackerCircle.Center.X - .5 * leftTrackerCircle.Bounds.Width) - (.1 * mainView.Bounds.Width);
				if(lTrackerDrag.State == UIGestureRecognizerState.Changed){
          var index = (int)(xPlot/dateMultiplier);
          if(ChosenDates.allIndexes.ContainsKey(index)){
            ChosenDates.subLeft = DateTime.Parse(ChosenDates.allIndexes[index]);
          }
          subDates.Text = Util.Strings.Report.START + ": " + ChosenDates.subLeft.ToString () + "\n"+Util.Strings.Report.FINISH+": " + ChosenDates.subRight.ToString();
				}   
				if(lTrackerDrag.LocationInView(mainView).X > (.1 * mainView.Bounds.Width) && lTrackerDrag.LocationInView(mainView).X < (.75 * mainView.Bounds.Width)){
					if(rightTrackerCircle.Center.X - rightTrackerCircle.Bounds.Width > lTrackerDrag.LocationInView(mainView).X){
						leftTrackerView.Frame = new CGRect(.1 * mainView.Bounds.Width,.15 * mainView.Bounds.Height, lTrackerDrag.LocationInView(mainView).X - (.1 * mainView.Bounds.Width), trackerHeight);
						leftTrackerCircle.Frame = new CGRect(lTrackerDrag.LocationInView(mainView).X,.15 * mainView.Bounds.Height + trackerHeight,30,33);
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
        var xPlot = (leftTrackerCircle.Center.X - .5 * leftTrackerCircle.Bounds.Width) - (.1 * mainView.Bounds.Width);
        if(lViewDrag.State == UIGestureRecognizerState.Changed){
          var index = (int)(xPlot/dateMultiplier);
          if(ChosenDates.allIndexes.ContainsKey(index)){
            ChosenDates.subLeft = DateTime.Parse(ChosenDates.allIndexes[index]);
          }
          subDates.Text = Util.Strings.Report.START + ": " + ChosenDates.subLeft.ToString () + "\n"+Util.Strings.Report.FINISH+": " + ChosenDates.subRight.ToString();
        }
        if(lViewDrag.LocationInView(mainView).X > (.1 * mainView.Bounds.Width) && lViewDrag.LocationInView(mainView).X < (.75 * mainView.Bounds.Width)){
          if(rightTrackerCircle.Center.X - rightTrackerCircle.Bounds.Width > lViewDrag.LocationInView(mainView).X){
            leftTrackerView.Frame = new CGRect(.1 * mainView.Bounds.Width,.15 * mainView.Bounds.Height, lViewDrag.LocationInView(mainView).X - (.1 * mainView.Bounds.Width), trackerHeight);
            //leftTrackerCircle.Frame = new CGRect(lViewDrag.LocationInView(mainView).X - 15,.15 * mainView.Bounds.Height + trackerHeight,30,33);
            leftTrackerCircle.Frame = new CGRect(lViewDrag.LocationInView(mainView).X,.15 * mainView.Bounds.Height + trackerHeight,30,33);
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

			rightTrackerCircle = new UIImageView (new CGRect (0, .15 * gView.Bounds.Height + trackerHeight,30, 33));
			rightTrackerCircle.Image = UIImage.FromBundle ("ic_right_tracker");
			rightTrackerCircle.UserInteractionEnabled = true;

			var trackerRect = rightTrackerCircle.Center;
			//trackerRect.X = rightTrackerView.Center.X - (.5f * rightTrackerView.Bounds.Width);
			trackerRect.X = rightTrackerView.Center.X - 15;
			rightTrackerCircle.Center = trackerRect;
			
			rTrackerDrag = new UIPanGestureRecognizer (() => {
				var xPlot = (rightTrackerCircle.Center.X + .5 * rightTrackerCircle.Bounds.Width) - (.1 * mainView.Bounds.Width);
				if(rTrackerDrag.State == UIGestureRecognizerState.Changed){          
          var index = (int)(xPlot/dateMultiplier);
          if(ChosenDates.allIndexes.ContainsKey(index)){
            ChosenDates.subRight = DateTime.Parse(ChosenDates.allIndexes[index]);
          }
          subDates.Text = Util.Strings.Report.START + ": " + ChosenDates.subLeft.ToString () + "\n"+Util.Strings.Report.FINISH+": " + ChosenDates.subRight.ToString();
				}
        if(rTrackerDrag.LocationInView(mainView).X > (.11 * mainView.Bounds.Width) && rTrackerDrag.LocationInView(mainView).X < (.915 * graphTable.Bounds.Width)){
					if(leftTrackerCircle.Center.X + leftTrackerCircle.Bounds.Width < rTrackerDrag.LocationInView(mainView).X){
						rightTrackerView.Frame = new CGRect(rTrackerDrag.LocationInView(mainView).X,.15 * mainView.Bounds.Height,(.915 * graphTable.Bounds.Width) - rTrackerDrag.LocationInView(mainView).X, trackerHeight);
						//rightTrackerCircle.Frame = new CGRect(rTrackerDrag.LocationInView(mainView).X - 15,.15 * mainView.Bounds.Height + trackerHeight,30,33);
						rightTrackerCircle.Frame = new CGRect(rTrackerDrag.LocationInView(mainView).X - rightTrackerCircle.Bounds.Width,.15 * mainView.Bounds.Height + trackerHeight,30,33);
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
        var xPlot = (rightTrackerCircle.Center.X + .5 * rightTrackerCircle.Bounds.Width) - (.1 * mainView.Bounds.Width);
        if(rViewDrag.State == UIGestureRecognizerState.Changed){          
          var index = (int)(xPlot/dateMultiplier);
          if(ChosenDates.allIndexes.ContainsKey(index)){
            ChosenDates.subRight = DateTime.Parse(ChosenDates.allIndexes[index]);
          }
          subDates.Text = Util.Strings.Report.START + ": " + ChosenDates.subLeft.ToString () + "\n"+Util.Strings.Report.FINISH+": " + ChosenDates.subRight.ToString();
        }
        if(rViewDrag.LocationInView(mainView).X > (.11 * mainView.Bounds.Width) && rViewDrag.LocationInView(mainView).X < (.915 * graphTable.Bounds.Width)){
          if(leftTrackerCircle.Center.X + leftTrackerCircle.Bounds.Width < rViewDrag.LocationInView(mainView).X){
            rightTrackerView.Frame = new CGRect(rViewDrag.LocationInView(mainView).X,.15 * mainView.Bounds.Height,(.915 * graphTable.Bounds.Width) - rViewDrag.LocationInView(mainView).X, trackerHeight);
            rightTrackerCircle.Frame = new CGRect(rViewDrag.LocationInView(mainView).X - rightTrackerCircle.Bounds.Width,.15 * mainView.Bounds.Height + trackerHeight,30,33);
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
			subDates.Text = Util.Strings.Report.START + ": " + ChosenDates.subLeft.ToString () + "\n"+Util.Strings.Report.FINISH+": " + ChosenDates.subRight.ToString();
			subDates.Font = UIFont.FromName("Helvetica-Bold", 22f);
			subDates.MinimumFontSize = 11.5f;
		}
		/// <summary>
		/// Creates the buttons to navigate and manipulate the graph and its included data
		/// </summary>
    public void createButtons(ObservableCollection<int> sessions){
      var deviceCount = ChosenDates.includeList.Count;
      resetButton = new UIButton (new CGRect (.05 * gView.Bounds.Width, .82 * mainVC.View.Bounds.Height, .3 * gView.Bounds.Width, .08 * gView.Bounds.Height));
			resetButton.BackgroundColor = UIColor.Red;
			resetButton.SetTitle (Util.Strings.RESET, UIControlState.Normal);
      resetButton.Font = UIFont.ItalicSystemFontOfSize(22);
			resetButton.Layer.CornerRadius = 5f;

			resetButton.TouchUpInside += (sender, e) => {
        ChosenDates.subLeft = ChosenDates.earliest;
        ChosenDates.subRight = ChosenDates.latest;
				UIView.Animate(.15,0, UIViewAnimationOptions.CurveLinear,
					() =>{
						leftTrackerView.Frame = new CGRect(.1 * gView.Bounds.Width,.15 * gView.Bounds.Height, 1, trackerHeight);
						var trackerRect = leftTrackerCircle.Center;
						trackerRect.X = leftTrackerView.Center.X + (.5f * leftTrackerCircle.Bounds.Width);
						leftTrackerCircle.Center = trackerRect;
					},() => {});

				UIView.Animate(.15,0, UIViewAnimationOptions.CurveLinear,
					() =>{ 
						rightTrackerView.Frame = new CGRect(.915 * graphTable.Bounds.Width,.15 * gView.Bounds.Height, 1, trackerHeight);
						var trackerRect = rightTrackerCircle.Center;
						trackerRect.X = rightTrackerView.Center.X - (.5f * rightTrackerCircle.Bounds.Width);
						rightTrackerCircle.Center = trackerRect;
					},() => {});				
				resetButton.BackgroundColor = UIColor.Red;
        subDates.Text = Util.Strings.Report.START + ": " + ChosenDates.subLeft.ToString () + "\n"+Util.Strings.Report.FINISH+": " + ChosenDates.subRight.ToString();
			};
			resetButton.TouchDown += (sender, e) => {resetButton.BackgroundColor = UIColor.Blue;};
      resetButton.TouchUpOutside += (sender, e) => {resetButton.BackgroundColor = UIColor.Red;};

      exportGraph = new UIButton (new CGRect (.65 * gView.Bounds.Width, .82 * mainVC.View.Bounds.Height,.3 * gView.Bounds.Width,.08 * gView.Bounds.Height));
      exportGraph.BackgroundColor = UIColor.FromRGB(49, 111, 18);
			exportGraph.SetTitle (Util.Strings.Report.EXPORT, UIControlState.Normal); 
			exportGraph.Layer.CornerRadius = 5f; 

			exportGraph.TouchUpInside += (sender, e) => {
        exportGraph.BackgroundColor = UIColor.FromRGB(49, 111, 18);
	 			exportSelect.blackoutView.Hidden = false;
	 			exportSelect.popupView.Hidden = false;
			};
			exportGraph.TouchDown += (sender, e) => {exportGraph.BackgroundColor = UIColor.Blue;};
      exportGraph.TouchUpOutside += (sender, e) => {exportGraph.BackgroundColor = UIColor.FromRGB(49, 111, 18);};

			exportSelect.pdfExport.TouchUpInside += pdfExport;
			exportSelect.spreadsheetExport.TouchUpInside += spreadsheetExport;

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
      extendedDown = new UIButton(new CGRect(0,.75 * gView.Bounds.Height,.1 * gView.Bounds.Width,.15 * gView.Bounds.Height));
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
    /// exports to a pdf with a user's options
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public async void pdfExport(object sender, EventArgs e){
      exportSelect.closeExport();          
			var sessionBreaks = new List<string>();
      var data = categorizeData(sessions,sessionBreaks);
      UIAlertView messageBox = new UIAlertView(Util.Strings.Report.PLEASEWAIT, Util.Strings.Report.TOASTPDF, null,null,null);
      messageBox.Show();
      await Task.Delay(TimeSpan.FromMilliseconds (100));
      if(exportSelect.pdfType == 0){
      	createPDF(messageBox,data,sessionBreaks,sessions);
			} else {
				createDetailedPDF(messageBox, data, sessionBreaks, sessions);
				
			}
			NSUserDefaults.StandardUserDefaults.SetString(exportSelect.pdfType.ToString(),"user_pdf_default");          
			NSUserDefaults.StandardUserDefaults.SetString(Convert.ToInt32(exportSelect.rawData.On).ToString(),"user_data_default");
		}
		
		/// <summary>
		/// exports to a spreadsheet with a user's options
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
    public async void spreadsheetExport(object sender, EventArgs e){
      exportSelect.closeExport();
      var sessionBreaks = new List<string>();
      var data = categorizeData(sessions,sessionBreaks);
      UIAlertView messageBox = new UIAlertView(Util.Strings.Report.PLEASEWAIT, Util.Strings.Report.TOASTSPREADSHEET, null,null,null);
      messageBox.Show();
      await Task.Delay(TimeSpan.FromMilliseconds (100));
      createSpreadsheet(messageBox,data,sessionBreaks,sessions);
			NSUserDefaults.StandardUserDefaults.SetString(exportSelect.spreadsheetType.ToString(),"user_spreadsheet_default");
		}		
    /// <summary>
    /// Based on the devices included and the date range chosen, the times and measurements are collected for
    /// each device within the time window and packaged to each device
    /// </summary>
    /// <returns>The data.</returns>
    /// <param name="sessions">Sessions.</param>
    /// <param name="sessionBreaks">Session breaks.</param>
    public List<deviceReadings> categorizeData(ObservableCollection<int> sessions, List<string> sessionBreaks){
    	Console.WriteLine("CategorizeData called. Starting at time " + ChosenDates.subLeft + " and ending with time " + ChosenDates.subRight);
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
        package.sensorIndex = Convert.ToInt32(splits[1]);
        var iserial = SerialNumberExtensions.ParseSerialNumber(package.serialNumber);
        var gDevice = ion.deviceManager.deviceFactory.GetDeviceDefinition(iserial) as GaugeDeviceDefinition;
        package.type = gDevice[package.sensorIndex].sensorType.ToString();
				sIndex = package.sensorIndex;
				
				//Console.WriteLine("Pulling device information for serialNumber " + package.serialNumber);
        var certInfo = ion.database.Query<LoggingDeviceRow>("SELECT nistDate, name  FROM LoggingDeviceRow WHERE serialNumber = ? LIMIT 1", package.serialNumber);

        if (certInfo.Count > 0) {
        	//Console.WriteLine("Pulled device: " + certInfo[0].name + " with nist date: " + certInfo[0].nistDate);
          package.nistDate = certInfo[0].nistDate;
          package.name = certInfo[0].name;
        }

        var timesReadings = ion.database.Query<SensorMeasurementRow>("SELECT recordedDate, measurement FROM SensorMeasurementRow WHERE serialNumber = ? and sensorIndex = ?  AND frn_SID in (" + string.Join(",",paramList.ToArray()) + ") AND recordedDate >=? AND recordedDate <= ? ORDER BY MID ASC",package.serialNumber, sIndex,ChosenDates.subLeft, ChosenDates.subRight);
				
				///SANITIZE REPORTING DATA FOR SESSIONS WITH NO MEASUREMENTS 
				if(timesReadings.Count > 0){
					package.min = timesReadings[0].measurement;
					package.max = timesReadings[0].measurement;
					package.avg = 0;
				} else {
					if(package.type == "Pressure" || package.type == "Vacuum"){
						package.min = 0;
						package.max = 0;
					} else {
						package.min = 255.372;
						package.max = 255.372;
					}
					package.avg = 0;
				}
        foreach (var MID in timesReadings) {
        	Console.WriteLine("Time : " + MID.recordedDate.ToLocalTime() + " measurement: " + MID.measurement);
        	
          package.times.Add(MID.recordedDate.ToLocalTime());
          package.readings.Add(MID.measurement);
          if(MID.measurement < package.min){
						package.min = MID.measurement;
					}
					if(MID.measurement > package.max){
						package.max = MID.measurement;						
					}
					package.avg += MID.measurement;
        }
        package.avg /= timesReadings.Count;
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
    public void createSpreadsheet(UIAlertView messageBox, List<deviceReadings> dataList, List<string> sessionBreaks, ObservableCollection<int> sessions){
      messageBox.Dismissed += previewSpreadsheet;
      var numberFormat = "#,##0.00";
      if(exportSelect.spreadsheetType == 0){
      	fileName = DateTime.UtcNow.ToLocalTime().ToString("MM-dd-yy hh:mm:ss tt") + ".xlsx";
      } else {
      	fileName = DateTime.UtcNow.ToLocalTime().ToString("MM-dd-yy hh:mm:ss tt") + ".csv";
      	numberFormat = "F2";
      }
      
      var masterTimes = new List<string>(); 

      foreach (var device in dataList) {
        foreach (var time in device.times) {
          if(!masterTimes.Contains(time.ToString())){
            masterTimes.Add(time.ToString());
          }
        }
      }
      //masterTimes.Sort();

      XlsFile xls = new XlsFile(2, TExcelFileFormat.v2013, true);
      xls.AllowOverwritingFiles = true;
      xls.ActiveSheet = 1;
      xls.SheetName = Util.Strings.Report.DATALOGGED;
      xls.ActiveSheet = 2;
      xls.SheetName = Util.Strings.Report.DEVICEINFO;
      
      if(exportSelect.spreadsheetType == 0){
				var colWidth = 4317;      
				xls.SetColWidth(1,8,colWidth);
			}

      TFlxFormat blackout = xls.GetDefaultFormat; //1
      blackout.FillPattern = new TFlxFillPattern { Pattern = TFlxPatternStyle.Solid, FgColor = TExcelColor.FromIndex(1) };
      blackout.VAlignment = TVFlxAlignment.top;
      blackout.HAlignment = THFlxAlignment.left;
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

      TFlxFormat measurementNormal = xls.GetDefaultFormat; //4
      measurementNormal.VAlignment = TVFlxAlignment.top;
      measurementNormal.HAlignment = THFlxAlignment.center;
      measurementNormal.Format = numberFormat;
      xls.AddFormat(measurementNormal);
    
      TFlxFormat measurementBorder = xls.GetDefaultFormat; //5
      measurementBorder.Borders.Bottom.Color = TUIColor.FromArgb(0xFF, 0x33, 0x33);
      measurementBorder.Borders.Bottom.Style = TFlxBorderStyle.Medium;
      measurementBorder.VAlignment = TVFlxAlignment.top;
      measurementBorder.HAlignment = THFlxAlignment.center;
      measurementBorder.Format = numberFormat;
      xls.AddFormat(measurementBorder);

			TFlxFormat borderedCell = xls.GetDefaultFormat; // 6
			borderedCell.Borders.Top.Color = TUIColor.FromArgb(0,0,0);    
			borderedCell.Borders.Top.Style = TFlxBorderStyle.Thin;
			borderedCell.Borders.Left.Color = TUIColor.FromArgb(0,0,0);
			borderedCell.Borders.Left.Style = TFlxBorderStyle.Thin;   
			borderedCell.Borders.Right.Color = TUIColor.FromArgb(0,0,0);    
			borderedCell.Borders.Right.Style = TFlxBorderStyle.Thin;
			borderedCell.Borders.Bottom.Color = TUIColor.FromArgb(0,0,0);
			borderedCell.Borders.Bottom.Style = TFlxBorderStyle.Thin;
      borderedCell.ShrinkToFit = true;
      borderedCell.WrapText = true;
      borderedCell.VAlignment = TVFlxAlignment.top;
      borderedCell.HAlignment = THFlxAlignment.left;      
      xls.AddFormat(borderedCell);
      
      TFlxFormat shrinkFit = xls.GetDefaultFormat; // 7
			shrinkFit.Borders.Top.Color = TUIColor.FromArgb(0,0,0);    
			shrinkFit.Borders.Top.Style = TFlxBorderStyle.Thin;
			shrinkFit.Borders.Left.Color = TUIColor.FromArgb(0,0,0);
			shrinkFit.Borders.Left.Style = TFlxBorderStyle.Thin;   
			shrinkFit.Borders.Right.Color = TUIColor.FromArgb(0,0,0);    
			shrinkFit.Borders.Right.Style = TFlxBorderStyle.Thin;
			shrinkFit.Borders.Bottom.Color = TUIColor.FromArgb(0,0,0);
			shrinkFit.Borders.Bottom.Style = TFlxBorderStyle.Thin;       
      shrinkFit.ShrinkToFit = true;
      xls.AddFormat(shrinkFit);
      
      var ion = AppState.context;
      var paramList = new List<string>();
      foreach (var num in sessions) {
        paramList.Add('"' + num.ToString() + '"');
      }      
      var certInfo = ion.database.Query<SessionRow>("SELECT DISTINCT frn_JID  FROM SessionRow WHERE SID in (" + string.Join(",",paramList.ToArray()) + ")", 2);

			int sessionSpan = 0;
			int jobID = 0;
			foreach(var sesh in certInfo){
				if(sesh.frn_JID != 0){
					sessionSpan++;
					jobID = sesh.frn_JID;
				}    
			}
      if (sessionSpan == 1) {
      	var jobInfo = ion.database.Query<JobRow>("SELECT jobName, poNumber, customerNumber, dispatchNumber, techName, systemType, jobLocation, jobAddress FROM JobRow WHERE JID = ?",jobID);
					xls.MergeCells(1, 6, 1, 7);
		      xls.SetCellValue(1, 6, Util.Strings.Job.JOBINFO,1);
		      xls.MergeCells(2,6,3,6);
		      xls.SetCellValue(2, 6, Util.Strings.Job.JOBNAME,1);
		      xls.MergeCells(2,7,3,7);
		      xls.SetCellFormat(3,7,2);
		      xls.SetCellValue(2, 7, jobInfo[0].jobName,6);
		      xls.SetCellValue(4, 6, Util.Strings.Job.PONUMBER, 1);
		      xls.SetCellValue(4, 7, jobInfo[0].poNumber, 6);
		      xls.SetCellValue(5, 6, Util.Strings.Job.CUSTOMERNUMBER, 1);
		      xls.SetCellValue(5, 7, jobInfo[0].customerNumber, 6);
		      xls.SetCellValue(6, 6, Util.Strings.Job.DISPATCHNUMBER, 1);
		      xls.SetCellValue(6, 7, jobInfo[0].dispatchNumber, 6);
		      xls.SetCellValue(7, 6, Util.Strings.Job.TECHNAME, 1);
		      xls.SetCellValue(7, 7, jobInfo[0].techName, 6);
		      xls.MergeCells(8, 6, 10, 6);
		      xls.SetCellValue(8, 6, Util.Strings.Job.JOBADDRESS, 1);
		      xls.MergeCells(8, 7, 10, 7);
		      xls.SetCellFormat(9,7,2);
		      xls.SetCellFormat(10,7,2);
		      xls.SetCellValue(8, 7, jobInfo[0].jobAddress, 6);
		      xls.SetCellValue(11, 6, Util.Strings.Job.JOBLOCATION, 1);
		      xls.SetCellValue(11, 7, jobInfo[0].jobLocation, 7);
		      xls.MergeCells(12,6,13,6);
		      xls.SetCellValue(12, 6, Util.Strings.Job.SYSTEMTYPE, 1);
		      xls.MergeCells(12,7,13,7);
		      xls.SetCellValue(12, 7, jobInfo[0].systemType, 6);   
	      //xls.AutofitRow(1,5,true,true,1.1);  
      } else if (certInfo.Count > 1) {
				xls.MergeCells(1, 5, 1,6);
				xls.SetCellValue(1, 5, Util.Strings.Job.MULTIPLEJOBS,1);
	      xls.AutofitRow(1,true,1.1);	       
			}
      
      xls.MergeCells(1, 1, 1, 4);
      xls.SetCellValue(1, 1, Util.Strings.Report.DEVICESUSED,2);
      xls.SetCellValue(2, 1, Util.Strings.Device.SERIAL_NUMBER, 1);
      xls.SetCellValue(2, 2, Util.Strings.NAME, 1);
      xls.SetCellValue(2, 3, Util.Strings.Device.CERTDATE, 1);
      xls.SetCellValue(2, 4, Util.Strings.Report.DEVICEMODEL, 1);

      int deviceCellIndex = 3;
      foreach (var device in dataList) {
        xls.SetCellValue(deviceCellIndex, 1, device.serialNumber, 6);
        xls.SetCellValue(deviceCellIndex, 2, device.name, 6);
        if (string.IsNullOrEmpty(device.nistDate) || device.nistDate.Length > 10) {
          xls.SetCellValue(deviceCellIndex, 3, "N/A", 6);
        } else {
          xls.SetCellValue(deviceCellIndex, 3, device.nistDate, 6);
        }
				var gaugeSerial = SerialNumberExtensions.ParseSerialNumber(device.serialNumber);
				
	      xls.SetCellValue(deviceCellIndex,4, gaugeSerial.deviceModel.ToString(),6);        
        deviceCellIndex++;
      }
      
      deviceCellIndex++;
      xls.MergeCells(deviceCellIndex, 2, deviceCellIndex, 4);
      xls.SetCellValue(deviceCellIndex, 1, Util.Strings.Report.CREATED, 1);
      xls.SetCellValue(deviceCellIndex, 2, DateTime.Now.ToLocalTime().ToString(), 7);
      deviceCellIndex++;
      xls.MergeCells(deviceCellIndex, 1, deviceCellIndex, 4);
      xls.SetCellValue(deviceCellIndex, 1, Util.Strings.Report.REPORTDATES, 1);
      deviceCellIndex++;
      xls.MergeCells(deviceCellIndex, 1, deviceCellIndex, 4);
      xls.SetCellValue(deviceCellIndex, 1, ChosenDates.subLeft + " - " + ChosenDates.subRight, 7);
      deviceCellIndex += 2;

      xls.SetCellValue(deviceCellIndex,1,Util.Strings.Device.SERIAL_NUMBER,1);
      xls.SetCellValue(deviceCellIndex,2,Util.Strings.Measure.MINIMUM,1);
      xls.SetCellValue(deviceCellIndex,3,Util.Strings.Measure.MAXIMUM,1);
      xls.SetCellValue(deviceCellIndex,4,Util.Strings.Measure.AVERAGE,1);
      deviceCellIndex++;
     	var extraInfoRow = deviceCellIndex;
     
     	includeDeviceStats(xls,dataList,extraInfoRow,7);
			
			xls.ActiveSheet = 1;
			
      xls.SetCellValue(1, 1, " ", 1);
      xls.SetCellValue(2, 1, " ", 1);
      xls.SetCellValue(3, 1, Util.Strings.TIME,2);

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
        //if (defaultUnit.Equals("7")) {
        //  xls.SetCellValue(1, i, dataList[i - 2].type + "(psig/inHg)", 1);
        //} else if (defaultUnit.Equals("8")){
        //  xls.SetCellValue(1, i, dataList[i - 2].type + "(kg/cm²/cmHg)", 1);
        //} else {
          xls.SetCellValue(1, i, dataList[i - 2].type + "(" + lookup + ")", 1);
        //}
        xls.SetCellValue(2, i, dataList[i - 2].serialNumber,2);

        xls.SetCellValue(3, i, dataList[i - 2].name, 2);
       
        var standardUnit = lookup.standardUnit;
        var deviceType = ION.Core.Sensors.UnitLookup.GetSensorTypeFromCode(Convert.ToInt32(defaultUnit));
        var rowIndex = 4;
        var compareIndex = 0;

        for (int t = 0; t < masterTimes.Count; t++) {
          if (compareIndex < dataList[i - 2].times.Count) {
            if (masterTimes[t].Equals(dataList[i - 2].times[compareIndex].ToString())) {
              var workingValue = standardUnit.OfScalar(dataList[i - 2].readings[compareIndex]);
              var finalValue = workingValue.ConvertTo(lookup);
              string formatValue = "";
              if(exportSelect.spreadsheetType == 1 && deviceType == Core.Sensors.ESensorType.Vacuum){
									formatValue = finalValue.amount.ToString("F");
							} else {
									formatValue = ION.Core.Sensors.SensorUtils.ToFormattedString(deviceType, finalValue);
							}
              if (sessionBreaks.Contains(dataList[i - 2].times[compareIndex].ToString())) {
                //xls.SetCellValue(rowIndex, i, Convert.ToDecimal(formatValue),5);
                xls.SetCellValue(rowIndex, i, formatValue,5);
              } else {
                //xls.SetCellValue(rowIndex, i, Convert.ToDecimal(formatValue),4);
                xls.SetCellValue(rowIndex, i,formatValue,4);
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
      mainVC.navigation.PresentViewController (previewController, true, null);
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
    public void createPDF(UIAlertView messageBox, List<deviceReadings> dataList, List<string> sessionBreaks,ObservableCollection<int> sessions){   
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
      //masterTimes.Sort();

      var scaleReduction = 100 - (dataList.Count * 6);
      //XlsFile xls = new XlsFile(1, TExcelFileFormat.v2013, true);
      XlsFile xls = new XlsFile(2, TExcelFileFormat.v2013, true);
      xls.AllowOverwritingFiles = true;
			xls.ActiveSheet = 2;
      xls.PrintScale = scaleReduction;
			xls.ActiveSheet = 1;
      xls.PrintScale = 60;
      ///SET A UNIFORM CELL WIDTH FOR COVER PAGE ITEMS
      ///DEFAULT COL WIDTH = 3189 AND SETTING IT TO 1.3X THAT SO 4317
			var colWidth = 4317;
			xls.SetColWidth(1,8,colWidth);

      TFlxFormat blackout = xls.GetDefaultFormat; //1
      blackout.FillPattern = new TFlxFillPattern { Pattern = TFlxPatternStyle.Solid, FgColor = TExcelColor.FromIndex(1) };
      blackout.VAlignment = TVFlxAlignment.top;
      blackout.HAlignment = THFlxAlignment.left;
      blackout.Font.Color = TExcelColor.FromIndex(2);
      xls.AddFormat(blackout);

      TFlxFormat centerText = xls.GetDefaultFormat; //2
      centerText.VAlignment = TVFlxAlignment.top;
      centerText.HAlignment = THFlxAlignment.center;
      centerText.ShrinkToFit = true;
      xls.AddFormat(centerText);

      TFlxFormat borderColor = xls.GetDefaultFormat; //3
      borderColor.Borders.Bottom.Color = TUIColor.FromArgb(0xFF, 0x33, 0x33);
      borderColor.Borders.Bottom.Style = TFlxBorderStyle.Medium;
      borderColor.VAlignment = TVFlxAlignment.top;
      borderColor.HAlignment = THFlxAlignment.center;
      xls.AddFormat(borderColor);

      TFlxFormat measurementNormal = xls.GetDefaultFormat; //4
      measurementNormal.VAlignment = TVFlxAlignment.top;
      measurementNormal.HAlignment = THFlxAlignment.center;
      measurementNormal.Format = "#,##0.00";
      xls.AddFormat(measurementNormal);
    
      TFlxFormat measurementBorder = xls.GetDefaultFormat; //5
      measurementBorder.Borders.Bottom.Color = TUIColor.FromArgb(0xFF, 0x33, 0x33);
      measurementBorder.Borders.Bottom.Style = TFlxBorderStyle.Medium;
      measurementBorder.VAlignment = TVFlxAlignment.top;
      measurementBorder.HAlignment = THFlxAlignment.center;
      measurementBorder.Format = "#,##0.00";
      xls.AddFormat(measurementBorder);

			TFlxFormat leftTopCornerBorder = xls.GetDefaultFormat; // 6
			leftTopCornerBorder.Borders.Top.Color = TUIColor.FromArgb(0,0,0);
			leftTopCornerBorder.Borders.Left.Color = TUIColor.FromArgb(0,0,0);
			leftTopCornerBorder.Borders.Top.Style = TFlxBorderStyle.Medium;   
			leftTopCornerBorder.Borders.Left.Style = TFlxBorderStyle.Medium; 
      xls.AddFormat(leftTopCornerBorder);  
			
			TFlxFormat leftSideBorder = xls.GetDefaultFormat; // 7    
			leftSideBorder.Borders.Left.Color = TUIColor.FromArgb(0,0,0);
			leftSideBorder.Borders.Left.Style = TFlxBorderStyle.Medium; 
      xls.AddFormat(leftSideBorder);  
			
			TFlxFormat leftBottomCornerBorder = xls.GetDefaultFormat; // 8
			leftBottomCornerBorder.Borders.Left.Color = TUIColor.FromArgb(0,0,0);    
			leftBottomCornerBorder.Borders.Left.Style = TFlxBorderStyle.Medium;
			leftBottomCornerBorder.Borders.Bottom.Color = TUIColor.FromArgb(0,0,0);
			leftBottomCornerBorder.Borders.Bottom.Style = TFlxBorderStyle.Medium;   
      xls.AddFormat(leftBottomCornerBorder);			
			
			TFlxFormat bottomBorder = xls.GetDefaultFormat; // 9    
			bottomBorder.Borders.Bottom.Color = TUIColor.FromArgb(0,0,0);
			bottomBorder.Borders.Bottom.Style = TFlxBorderStyle.Medium;
      xls.AddFormat(bottomBorder);  
  
			TFlxFormat topBorder = xls.GetDefaultFormat; // 10 
			topBorder.Borders.Top.Color = TUIColor.FromArgb(0,0,0);
			topBorder.Borders.Top.Style = TFlxBorderStyle.Medium; 
      xls.AddFormat(topBorder);  
			
			TFlxFormat rightTopCornerBorder = xls.GetDefaultFormat; // 11    
			rightTopCornerBorder.Borders.Top.Color = TUIColor.FromArgb(0,0,0);
			rightTopCornerBorder.Borders.Right.Color = TUIColor.FromArgb(0,0,0);
			rightTopCornerBorder.Borders.Top.Style = TFlxBorderStyle.Medium;   
			rightTopCornerBorder.Borders.Right.Style = TFlxBorderStyle.Medium;
      xls.AddFormat(rightTopCornerBorder);  
			
			TFlxFormat rightSideBorder = xls.GetDefaultFormat; // 12    
			rightSideBorder.Borders.Right.Color = TUIColor.FromArgb(0,0,0);
			rightSideBorder.Borders.Right.Style = TFlxBorderStyle.Medium;
      xls.AddFormat(rightSideBorder);  
			
			TFlxFormat rightBottomCornerBorder = xls.GetDefaultFormat; // 13    
			rightBottomCornerBorder.Borders.Right.Color = TUIColor.FromArgb(0,0,0);    
			rightBottomCornerBorder.Borders.Right.Style = TFlxBorderStyle.Medium;
			rightBottomCornerBorder.Borders.Bottom.Color = TUIColor.FromArgb(0,0,0);
			rightBottomCornerBorder.Borders.Bottom.Style = TFlxBorderStyle.Medium;
      xls.AddFormat(rightBottomCornerBorder); 

			TFlxFormat borderedCell = xls.GetDefaultFormat; // 14
			borderedCell.Borders.Top.Color = TUIColor.FromArgb(0,0,0);    
			borderedCell.Borders.Top.Style = TFlxBorderStyle.Thin;
			borderedCell.Borders.Left.Color = TUIColor.FromArgb(0,0,0);
			borderedCell.Borders.Left.Style = TFlxBorderStyle.Thin;   
			borderedCell.Borders.Right.Color = TUIColor.FromArgb(0,0,0);    
			borderedCell.Borders.Right.Style = TFlxBorderStyle.Thin;
			borderedCell.Borders.Bottom.Color = TUIColor.FromArgb(0,0,0);
			borderedCell.Borders.Bottom.Style = TFlxBorderStyle.Thin;
      borderedCell.ShrinkToFit = true;
      borderedCell.WrapText = true;
      borderedCell.VAlignment = TVFlxAlignment.top;
      borderedCell.HAlignment = THFlxAlignment.left;      
      xls.AddFormat(borderedCell);
      
      TFlxFormat boldText = xls.GetDefaultFormat;// 15
      boldText.Font.Style = TFlxFontStyles.Bold;
      boldText.Font.Size20 = 400;
      xls.AddFormat(boldText);
      
      TFlxFormat shrinkFit = xls.GetDefaultFormat; // 16
			shrinkFit.Borders.Top.Color = TUIColor.FromArgb(0,0,0);    
			shrinkFit.Borders.Top.Style = TFlxBorderStyle.Thin;
			shrinkFit.Borders.Left.Color = TUIColor.FromArgb(0,0,0);
			shrinkFit.Borders.Left.Style = TFlxBorderStyle.Thin;   
			shrinkFit.Borders.Right.Color = TUIColor.FromArgb(0,0,0);    
			shrinkFit.Borders.Right.Style = TFlxBorderStyle.Thin;
			shrinkFit.Borders.Bottom.Color = TUIColor.FromArgb(0,0,0);
			shrinkFit.Borders.Bottom.Style = TFlxBorderStyle.Thin;      
      shrinkFit.ShrinkToFit = true;
      xls.AddFormat(shrinkFit);
      
			///ADD THE APPION LOGO TO THE TOP OF EACH PAGE
			var logoView = new UIImageView(new CGRect(0,0,150,50));
			logoView.Image = UIImage.FromBundle("appion_logo_black");
			
	    using( var ns = new NSAutoreleasePool())
	    {
        using(UIImage img = logoView.Image) {
          using (var data = img.AsPNG ())
          {
						using (Stream fs = data.AsStream())
						{
							TImageProperties ImgProps = new TImageProperties();
							ImgProps.Anchor = new TClientAnchor(TFlxAnchorType.MoveAndDontResize, 1, 0, 1, 0, 5, 255, 3, 1024);
							ImgProps.ShapeName = "Graph";
							xls.AddImage(fs, ImgProps);
						}
          }
      	}
	    }
	    
	    xls.MergeCells(4,4,5,6);
	    xls.SetCellValue(4,4,"DATA LOGGING REPORT",15);

			xls.MergeCells(7,1,7,4);
      xls.SetCellValue(7, 1, Util.Strings.Report.DEVICESUSED, 2);
      xls.SetCellValue(8, 1, Util.Strings.Device.SERIAL_NUMBER, 1);
      xls.SetCellValue(8, 2, Util.Strings.NAME, 1);
      xls.SetCellValue(8, 3, Util.Strings.Device.CERTDATE, 1);
      xls.SetCellValue(8, 4, Util.Strings.Report.DEVICEMODEL, 1);
			
			int deviceCellIndex = 9;
      foreach (var device in dataList) {
        xls.SetCellValue(deviceCellIndex, 1, device.serialNumber, 14);
        xls.SetCellValue(deviceCellIndex, 2, device.name, 14);
        if (string.IsNullOrEmpty(device.nistDate) || device.nistDate.Length > 10) {
          xls.SetCellValue(deviceCellIndex, 3, "N/A", 14);
        } else {
          xls.SetCellValue(deviceCellIndex, 3, device.nistDate, 14);
        }
				//todo	THIS WILL BREAK IF IT IS NOT A GAUGE SERIAL NUMBER
				var gaugeSerial = SerialNumberExtensions.ParseSerialNumber(device.serialNumber);
				
	      xls.SetCellValue(deviceCellIndex,4, gaugeSerial.deviceModel.ToString(),14);
        
        deviceCellIndex++;
      }
      
      var ion = AppState.context;
      var paramList = new List<string>();
      foreach (var num in sessions) {
        paramList.Add('"' + num.ToString() + '"');
      }      
      var certInfo = ion.database.Query<SessionRow>("SELECT DISTINCT frn_JID  FROM SessionRow WHERE SID in (" + string.Join(",",paramList.ToArray()) + ")");

			int sessionSpan = 0;
			int jobID = 0;
			foreach(var sesh in certInfo){
				if(sesh.frn_JID != 0){
					sessionSpan++;
					jobID = sesh.frn_JID;
				}
			}
      if (sessionSpan == 1) {
      	var jobInfo = ion.database.Query<JobRow>("SELECT jobName, poNumber, customerNumber, dispatchNumber, techName, systemType, jobLocation, jobAddress FROM JobRow WHERE JID = ?",jobID);
					xls.MergeCells(7, 6, 7, 7);
		      xls.SetCellValue(7, 6, Util.Strings.Job.JOBINFO,1);
		      xls.MergeCells(8,6,9,6);
		      xls.SetCellValue(8, 6, Util.Strings.Job.JOBNAME,1);
		      xls.MergeCells(8,7,9,7);
		      xls.SetCellFormat(9,7,14);
		      xls.SetCellValue(8, 7, jobInfo[0].jobName,14);
		      xls.SetCellValue(10, 6, Util.Strings.Job.PONUMBER, 1);
		      xls.SetCellValue(10, 7, jobInfo[0].poNumber, 14);
		      xls.SetCellValue(11, 6, Util.Strings.Job.CUSTOMERNUMBER, 1);
		      xls.SetCellValue(11, 7, jobInfo[0].customerNumber, 14);
		      xls.SetCellValue(12, 6, Util.Strings.Job.DISPATCHNUMBER, 1);
		      xls.SetCellValue(12, 7, jobInfo[0].dispatchNumber, 14);
		      xls.SetCellValue(13, 6, Util.Strings.Job.TECHNAME, 1);
		      xls.SetCellValue(13, 7, jobInfo[0].techName, 14);
		      xls.MergeCells(14, 6, 16, 6);
		      xls.SetCellValue(14, 6, Util.Strings.Job.JOBADDRESS, 1);
		      xls.MergeCells(14, 7, 16, 7);
		      xls.SetCellFormat(15,7,14);
		      xls.SetCellFormat(16,7,14);
		      xls.SetCellValue(14, 7, jobInfo[0].jobAddress, 14);
		      xls.SetCellValue(17, 6, Util.Strings.Job.JOBLOCATION, 1);
		      xls.SetCellValue(17, 7, jobInfo[0].jobLocation, 16);
		      xls.MergeCells(18,6,19,6);
		      xls.SetCellValue(18, 6, Util.Strings.Job.SYSTEMTYPE, 1);
		      xls.MergeCells(18,7,19,7);
		      xls.SetCellFormat(19,7,14);    
		      xls.SetCellValue(18, 7, jobInfo[0].systemType, 14);
      } else if (certInfo.Count > 1) {
				xls.MergeCells(7, 6, 7,7);
				xls.SetCellValue(7, 6, Util.Strings.Job.MULTIPLEJOBS,1);
			}
      
      deviceCellIndex++;

      xls.MergeCells(deviceCellIndex, 2, deviceCellIndex, 4);
      xls.SetCellValue(deviceCellIndex, 1, Util.Strings.Report.CREATED, 1);
      xls.SetCellValue(deviceCellIndex, 2, DateTime.Now.ToLocalTime().ToString(), 16);
			xls.SetCellFormat(deviceCellIndex,3,14);
			xls.SetCellFormat(deviceCellIndex,4,14);
      deviceCellIndex++;
      xls.MergeCells(deviceCellIndex, 1, deviceCellIndex, 4);
      xls.SetCellValue(deviceCellIndex, 1, Util.Strings.Report.REPORTDATES, 1);
      deviceCellIndex++;
      xls.MergeCells(deviceCellIndex, 1, deviceCellIndex, 4);
			xls.SetCellFormat(deviceCellIndex,2,14);
			xls.SetCellFormat(deviceCellIndex,3,14);
			xls.SetCellFormat(deviceCellIndex,4,14);
      xls.SetCellValue(deviceCellIndex, 1, ChosenDates.subLeft + " - " + ChosenDates.subRight, 16);
			xls.SetCellFormat(deviceCellIndex,1,14);
      deviceCellIndex+= 2;
      
      xls.SetCellValue(deviceCellIndex,1,Util.Strings.Device.SERIAL_NUMBER,1);
      xls.SetCellValue(deviceCellIndex,2,Util.Strings.Measure.MINIMUM,1);
      xls.SetCellValue(deviceCellIndex,3,Util.Strings.Measure.MAXIMUM,1);
      xls.SetCellValue(deviceCellIndex,4,Util.Strings.Measure.AVERAGE,1);
      deviceCellIndex++;
     	var extraInfoRow = deviceCellIndex;

     	includeDeviceStats(xls,dataList,extraInfoRow,14);     	
     	
			var imageRow = deviceCellIndex + dataList.Count + 1;
			if(imageRow < 21){
				imageRow = 21;
			}
			var imageCol = 2;
			
    	UIImageView[] graphViews = new UIImageView[graphTable.NumberOfRowsInSection(0)];
    	
    	for(int i = 0; i < graphTable.NumberOfRowsInSection(0); i++){
    		///THIS METHOD ONLY GETS "VISIBLE" CELLS FROM THE TABLE VIEW AND RETURNS NULL FOR ANY OTHERS
    		//var cell = graphTable.CellAt(NSIndexPath.FromRowSection(i,0)) as graphCell;
    		///THIS METHOD SEEMS TO GO THROUGH THE CORE DATA OF THE TABLE AND RETURNS EVERY CELL IT CONTAINS
    		var cell = graphTable.Source.GetCell(graphTable,NSIndexPath.FromRowSection(i,0)) as graphCell;
    		var includedName = cell.deviceName.Text+"/"+cell.cellData.sensorIndex.ToString();
    		if(ChosenDates.includeList.Contains(includedName)){
	    		graphViews[i] = new UIImageView(new CGRect(0,0,150,50));
					graphViews[i].Image = UI.UIViewExtensions.Capture(cell.plotView);
					//Console.WriteLine("Image done");
			    using( var ns = new NSAutoreleasePool() )
			    {
		        using(UIImage img = graphViews[i].Image) {
	            using (var data = img.AsPNG ()) 
	            {
								using (Stream fs = data.AsStream())
								{
									TImageProperties ImgProps = new TImageProperties();
									ImgProps.Anchor = new TClientAnchor(TFlxAnchorType.MoveAndDontResize, imageRow, 0, imageCol, 0, imageRow + 5, 255, imageCol + 2, 1024);
									ImgProps.ShapeName = "Graph";
									xls.AddImage(fs, ImgProps);
								}			                        
	            }
	        	}
			    }
			    xls.SetCellValue(imageRow + 2,imageCol - 1,cell.cellData.serialNumber,2);
			    xls.SetCellValue(imageRow + 3,imageCol - 1,"("+cell.cellData.type+")",2);
			    
			    /***************OUTLINE THE GRAPH IMAGE*****************/
			    ///LEFT EDGES OF GRAPH
			    xls.SetCellFormat(imageRow,imageCol,6);
			    xls.SetCellFormat(imageRow + 1,imageCol,imageRow + 4, imageCol,7);
			    //xls.SetCellFormat(imageRow + 2,imageCol,7);
			    //xls.SetCellFormat(imageRow + 3,imageCol,7);
			    //xls.SetCellFormat(imageRow + 4,imageCol,7);
			    xls.SetCellFormat(imageRow + 5,imageCol,8);
			    
			    ///BOTTOM OF GRAPH
			    xls.SetCellFormat(imageRow + 5,imageCol+1,9);
			    ///TOP OF GRAPH
			    xls.SetCellFormat(imageRow,imageCol+1,10);			    
			    ///RIGHT EDGES OF GRAPH
			    xls.SetCellFormat(imageRow,imageCol+2,11);
			    xls.SetCellFormat(imageRow + 1,imageCol+2,imageRow + 4,imageCol + 2,12);
			    //xls.SetCellFormat(imageRow + 2,imageCol+2,12);
			    //xls.SetCellFormat(imageRow + 3,imageCol+2,12);
			    //xls.SetCellFormat(imageRow + 4,imageCol+2,12);			    
			    xls.SetCellFormat(imageRow + 5,imageCol+2,13);			    
			    /*******************************************************/			    
			    
			    if((i + 1) % 2 == 0){
			    	imageRow += 9;		    
						imageCol = 2;
					} else {				
						imageCol = 6;
					}
				}
			}
			if(exportSelect.rawData.On){
				/**********************************************/				
				xls.ActiveSheet = 2;				
				/**********************************************/
				deviceCellIndex = 1;
	      TXlsNamedRange Range;
	      string RangeName;
	      RangeName = TXlsNamedRange.GetInternalName(InternalNameRange.Print_Titles);
	      Range = new TXlsNamedRange(RangeName, 2, 2, deviceCellIndex, 1, deviceCellIndex + 2, dataList.Count + 1, 32);
	      xls.SetNamedRange(Range);
	
	      xls.SetCellValue(deviceCellIndex, 1, " ", 1);
	      deviceCellIndex++;
	      xls.SetCellValue(deviceCellIndex, 1, " ", 1);
	      deviceCellIndex++;
	      xls.SetCellValue(deviceCellIndex, 1, Util.Strings.TIME,2);
	      deviceCellIndex++;
				var jobStartIndex = 0;
	      for (int i = deviceCellIndex; i < masterTimes.Count + deviceCellIndex; i++) { 
	        xls.SetCellValue(i, 1, masterTimes[i-deviceCellIndex], 2);
	        if (sessionBreaks.Contains(masterTimes[i-deviceCellIndex])) {
	          xls.SetCellValue(i, 1, masterTimes[i-deviceCellIndex], 3);
	        } else {
	          xls.SetCellValue(i,1, masterTimes[i-deviceCellIndex], 2);
	        }
	        jobStartIndex = i;
	      }
	      deviceCellIndex -= 3;
	      var measStartIndex = deviceCellIndex;
	      
	      for (int i = 2; i < dataList.Count + 2; i++) {
	      	//Console.WriteLine("pdf for device " + dataList[i-2].serialNumber + " si: " + dataList[i-2].sensorIndex);
	        var defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_pressure");
	
	        if (dataList[i - 2].type.Equals("Temperature")) {
	          defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_temperature");
	        } else if (dataList[i - 2].type.Equals("Vacuum")) {
	          defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_vacuum");
	        }
	
	        var lookup = ION.Core.Sensors.UnitLookup.GetUnit(Convert.ToInt32(defaultUnit));        
								
	        //if (defaultUnit.Equals("7")) {
	        //  xls.SetCellValue(measStartIndex, i, dataList[i - 2].type + "(psig/inHg)", 1);
	        //} else if (defaultUnit.Equals("8")){
	        //  xls.SetCellValue(measStartIndex, i, dataList[i - 2].type + "(kg/cm²/cmHg)", 1);
	        //} else {
	          xls.SetCellValue(measStartIndex, i, dataList[i - 2].type + "(" + lookup + ")", 1);
	        //}
	        measStartIndex++;
	        xls.SetCellValue(measStartIndex, i, dataList[i - 2].serialNumber,2);
	        measStartIndex++;
	        xls.SetCellValue(measStartIndex, i, dataList[i - 2].name, 2);
	        measStartIndex++;
	
	        var standardUnit = lookup.standardUnit;
	        var deviceType = ION.Core.Sensors.UnitLookup.GetSensorTypeFromCode(Convert.ToInt32(defaultUnit));
	        var rowIndex = measStartIndex;
	        var compareIndex = 0;
								
	        for (int t = 0; t < masterTimes.Count; t++) {
	          if (compareIndex < dataList[i - 2].times.Count) {
	            if (masterTimes[t].Equals(dataList[i - 2].times[compareIndex].ToString())) {
	              var workingValue = standardUnit.OfScalar(dataList[i - 2].readings[compareIndex]);
	              var finalValue = workingValue.ConvertTo(lookup);
								var formatValue = ION.Core.Sensors.SensorUtils.ToFormattedString(deviceType, finalValue);
								//Console.WriteLine("pdf at reading " + formatValue + " at time " + masterTimes[t]);
	              if (sessionBreaks.Contains(dataList[i - 2].times[compareIndex].ToString())) {
	                xls.SetCellValue(rowIndex, i, formatValue, 5);
	              } else {
	                xls.SetCellValue(rowIndex, i, formatValue, 4);
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
	      jobStartIndex += 2;
	
	      xls.AutofitCol(1, false, 1.1);
				/******************************************/
			}
			xls.ActiveSheet = 1;
			
			/******************************************/
      FlexCelPdfExport pdfExport = new FlexCelPdfExport(xls);
      pdfExport.AllowOverwritingFiles = true;

      var pdfPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);

      try{
        using(FileStream Pdf = new FileStream(pdfPath,FileMode.Create)){
          pdfExport.BeginExport(Pdf);

          pdfExport.ExportSheet();
          if(exportSelect.rawData.On){
						xls.ActiveSheet = 2;
          	pdfExport.ExportSheet(1,pdfExport.TotalPagesInSheet());
					}
          pdfExport.EndExport();
        }
      }catch (Exception e){
        Console.WriteLine("Exception: " + e);
      }

      messageBox.DismissWithClickedButtonIndex(0, false);
    }
		/// <summary>
		/// Creates the detailed pdf.
		/// </summary>
		/// <param name="messageBox">Message box.</param>
		/// <param name="dataList">Data list.</param>
		/// <param name="sessionBreaks">Session breaks.</param>
		/// <param name="sessions">Sessions.</param>
		public void createDetailedPDF(UIAlertView messageBox, List<deviceReadings> dataList, List<string> sessionBreaks,ObservableCollection<int> sessions){
      messageBox.Dismissed += previewPDF;
      fileName = DateTime.UtcNow.ToLocalTime().ToString("MM-dd-yy hh:mm:ss tt") + ".pdf";
 
      Console.WriteLine("There are " + dataList.Count + " sensor packages for reading");
      
      var masterTimes = new List<string>();

      foreach (var device in dataList) {
        foreach (var time in device.times) {
          if(!masterTimes.Contains(time.ToString())){
            masterTimes.Add(time.ToString());
          }
        }
      }
      //masterTimes.Sort();

      var scaleReduction = 100 - (ChosenDates.includeList.Count * 6);
      XlsFile xls = new XlsFile(ChosenDates.includeList.Count + 1, TExcelFileFormat.v2013, true);
      xls.AllowOverwritingFiles = true;

      TFlxFormat blackout = xls.GetDefaultFormat; //1
      blackout.FillPattern = new TFlxFillPattern { Pattern = TFlxPatternStyle.Solid, FgColor = TExcelColor.FromIndex(1) };
      blackout.VAlignment = TVFlxAlignment.top;
      blackout.HAlignment = THFlxAlignment.left;
      blackout.Font.Color = TExcelColor.FromIndex(2);
      xls.AddFormat(blackout);

      TFlxFormat centerText = xls.GetDefaultFormat; //2
      centerText.VAlignment = TVFlxAlignment.center;
      centerText.HAlignment = THFlxAlignment.center;
      centerText.ShrinkToFit = true;
      xls.AddFormat(centerText);

      TFlxFormat borderColor = xls.GetDefaultFormat; //3
      borderColor.Borders.Bottom.Color = TUIColor.FromArgb(0xFF, 0x33, 0x33);
      borderColor.Borders.Bottom.Style = TFlxBorderStyle.Medium;
      borderColor.VAlignment = TVFlxAlignment.top;
      borderColor.HAlignment = THFlxAlignment.center;
      xls.AddFormat(borderColor);

      TFlxFormat measurementNormal = xls.GetDefaultFormat; //4
      measurementNormal.VAlignment = TVFlxAlignment.top;
      measurementNormal.HAlignment = THFlxAlignment.center;
      measurementNormal.Format = "#,##0.00";
      xls.AddFormat(measurementNormal);
    
      TFlxFormat measurementBorder = xls.GetDefaultFormat; //5
      measurementBorder.Borders.Bottom.Color = TUIColor.FromArgb(0xFF, 0x33, 0x33);
      measurementBorder.Borders.Bottom.Style = TFlxBorderStyle.Medium;
      measurementBorder.VAlignment = TVFlxAlignment.top;
      measurementBorder.HAlignment = THFlxAlignment.center;
      measurementBorder.Format = "#,##0.00";
      xls.AddFormat(measurementBorder);  

			TFlxFormat leftTopCornerBorder = xls.GetDefaultFormat; // 6
			leftTopCornerBorder.Borders.Top.Color = TUIColor.FromArgb(0,0,0);
			leftTopCornerBorder.Borders.Left.Color = TUIColor.FromArgb(0,0,0);
			leftTopCornerBorder.Borders.Top.Style = TFlxBorderStyle.Medium;   
			leftTopCornerBorder.Borders.Left.Style = TFlxBorderStyle.Medium; 
      xls.AddFormat(leftTopCornerBorder);  
			
			TFlxFormat leftSideBorder = xls.GetDefaultFormat; // 7    
			leftSideBorder.Borders.Left.Color = TUIColor.FromArgb(0,0,0);
			leftSideBorder.Borders.Left.Style = TFlxBorderStyle.Medium; 
      xls.AddFormat(leftSideBorder);  
			
			TFlxFormat leftBottomCornerBorder = xls.GetDefaultFormat; // 8
			leftBottomCornerBorder.Borders.Left.Color = TUIColor.FromArgb(0,0,0);    
			leftBottomCornerBorder.Borders.Left.Style = TFlxBorderStyle.Medium;
			leftBottomCornerBorder.Borders.Bottom.Color = TUIColor.FromArgb(0,0,0);
			leftBottomCornerBorder.Borders.Bottom.Style = TFlxBorderStyle.Medium;   
      xls.AddFormat(leftBottomCornerBorder);			
			
			TFlxFormat bottomBorder = xls.GetDefaultFormat; // 9    
			bottomBorder.Borders.Bottom.Color = TUIColor.FromArgb(0,0,0);
			bottomBorder.Borders.Bottom.Style = TFlxBorderStyle.Medium;
      xls.AddFormat(bottomBorder);  
  
			TFlxFormat topBorder = xls.GetDefaultFormat; // 10 
			topBorder.Borders.Top.Color = TUIColor.FromArgb(0,0,0);
			topBorder.Borders.Top.Style = TFlxBorderStyle.Medium; 
      xls.AddFormat(topBorder);  
			
			TFlxFormat rightTopCornerBorder = xls.GetDefaultFormat; // 11    
			rightTopCornerBorder.Borders.Top.Color = TUIColor.FromArgb(0,0,0);
			rightTopCornerBorder.Borders.Right.Color = TUIColor.FromArgb(0,0,0);
			rightTopCornerBorder.Borders.Top.Style = TFlxBorderStyle.Medium;   
			rightTopCornerBorder.Borders.Right.Style = TFlxBorderStyle.Medium;
      xls.AddFormat(rightTopCornerBorder);  
			
			TFlxFormat rightSideBorder = xls.GetDefaultFormat; // 12    
			rightSideBorder.Borders.Right.Color = TUIColor.FromArgb(0,0,0);
			rightSideBorder.Borders.Right.Style = TFlxBorderStyle.Medium;
      xls.AddFormat(rightSideBorder);  
			
			TFlxFormat rightBottomCornerBorder = xls.GetDefaultFormat; // 13    
			rightBottomCornerBorder.Borders.Right.Color = TUIColor.FromArgb(0,0,0);    
			rightBottomCornerBorder.Borders.Right.Style = TFlxBorderStyle.Medium;
			rightBottomCornerBorder.Borders.Bottom.Color = TUIColor.FromArgb(0,0,0);
			rightBottomCornerBorder.Borders.Bottom.Style = TFlxBorderStyle.Medium;
      xls.AddFormat(rightBottomCornerBorder);  

			TFlxFormat borderedCell = xls.GetDefaultFormat; // 14
			borderedCell.Borders.Top.Color = TUIColor.FromArgb(0,0,0);    
			borderedCell.Borders.Top.Style = TFlxBorderStyle.Thin;
			borderedCell.Borders.Left.Color = TUIColor.FromArgb(0,0,0);
			borderedCell.Borders.Left.Style = TFlxBorderStyle.Thin;   
			borderedCell.Borders.Right.Color = TUIColor.FromArgb(0,0,0);    
			borderedCell.Borders.Right.Style = TFlxBorderStyle.Thin;
			borderedCell.Borders.Bottom.Color = TUIColor.FromArgb(0,0,0);
			borderedCell.Borders.Bottom.Style = TFlxBorderStyle.Thin;
      borderedCell.ShrinkToFit = true;
      borderedCell.WrapText = true;
      borderedCell.VAlignment = TVFlxAlignment.top;
      borderedCell.HAlignment = THFlxAlignment.left;      
      xls.AddFormat(borderedCell);
 
      TFlxFormat boldText = xls.GetDefaultFormat;// 15
      boldText.Font.Style = TFlxFontStyles.Bold;
      boldText.Font.Size20 = 400;
      xls.AddFormat(boldText);
 
     	TFlxFormat shrinkFit = xls.GetDefaultFormat; // 16
			shrinkFit.Borders.Top.Color = TUIColor.FromArgb(0,0,0);    
			shrinkFit.Borders.Top.Style = TFlxBorderStyle.Thin;
			shrinkFit.Borders.Left.Color = TUIColor.FromArgb(0,0,0);
			shrinkFit.Borders.Left.Style = TFlxBorderStyle.Thin;   
			shrinkFit.Borders.Right.Color = TUIColor.FromArgb(0,0,0);    
			shrinkFit.Borders.Right.Style = TFlxBorderStyle.Thin;
			shrinkFit.Borders.Bottom.Color = TUIColor.FromArgb(0,0,0);
			shrinkFit.Borders.Bottom.Style = TFlxBorderStyle.Thin;      
      shrinkFit.ShrinkToFit = true;
      xls.AddFormat(shrinkFit);
  
      ///SET PRINTSCALE FOR RAW DATA SHEET
			xls.ActiveSheet = dataList.Count + 1; 
      xls.PrintScale = scaleReduction;
      int deviceCellIndex = 3;
      
      ///LOOP THROUGH EACH DEVICE AND CREATE THE DATA FOR THAT SHEET
    	//for(int i = 1; i <= graphTable.NumberOfRowsInSection(0); i++){
    	for(int i = 1; i <= ChosenDates.includeList.Count; i++){
				xls.ActiveSheet = i;
	      xls.PrintScale = 67;
	      ///SET A UNIFORM CELL WIDTH FOR COVER PAGE ITEMS
	      ///DEFAULT COL WIDTH = 3189 AND SETTING IT TO 1.3X THAT SO 4317
				var colWidth = 4317;
				xls.SetColWidth(1,8,colWidth);
				
				///ADD THE APPION LOGO TO THE TOP OF EACH PAGE
				var logoView = new UIImageView(new CGRect(0,0,150,50));
				logoView.Image = UIImage.FromBundle("appion_logo_black");
				
		    using( var ns = new NSAutoreleasePool())
		    {
	        using(UIImage img = logoView.Image) {
            using (var data = img.AsPNG ())
            {
							using (Stream fs = data.AsStream())
							{
								TImageProperties ImgProps = new TImageProperties();
								ImgProps.Anchor = new TClientAnchor(TFlxAnchorType.MoveAndDontResize, 1, 0, 1, 0, 5, 255, 3, 1024);
								ImgProps.ShapeName = "Graph";
								xls.AddImage(fs, ImgProps);
							}
            }
        	}
		    }
		    
		    xls.MergeCells(4,4,5,6);
		    xls.SetCellValue(4,4,"DATA LOGGING REPORT",15);
	
	      xls.SetCellValue(7, 1, Util.Strings.Device.SERIAL_NUMBER, 1);
	      xls.SetCellValue(7, 2, Util.Strings.NAME, 1);
	      xls.SetCellValue(7, 3, Util.Strings.Device.CERTDATE, 1);
	      xls.SetCellValue(7, 4, Util.Strings.Report.DEVICEMODEL, 1);
				
				deviceCellIndex = 8;
				
	      xls.SetCellValue(deviceCellIndex,1,dataList[i-1].serialNumber,14);
	      xls.SetCellValue(deviceCellIndex,2, dataList[i-1].name,14);
	      if(string.IsNullOrEmpty(dataList[i-1].nistDate) || dataList[i-1].nistDate.Length > 10){
	          xls.SetCellValue(deviceCellIndex, 3, "N/A", 14);
				} else {
	          xls.SetCellValue(deviceCellIndex, 3, dataList[i-1].nistDate, 14);
				}
				//todo	THIS WILL BREAK IF IT IS NOT A GAUGE SERIAL NUMBER
				var gaugeSerial = SerialNumberExtensions.ParseSerialNumber(dataList[i-1].serialNumber);
				
	      xls.SetCellValue(deviceCellIndex,4, gaugeSerial.deviceModel.ToString(),14);
				
				deviceCellIndex+= 4;
	      
	      var ion = AppState.context;
	      var paramList = new List<string>();
	      foreach (var num in sessions) {
	        paramList.Add('"' + num.ToString() + '"');
	      }
	      var certInfo = ion.database.Query<SessionRow>("SELECT DISTINCT frn_JID  FROM SessionRow WHERE SID in (" + string.Join(",",paramList.ToArray()) + ")");
				
				int sessionSpan = 0;
				int jobID = 0;
				foreach(var sesh in certInfo){
					if(sesh.frn_JID != 0){
						sessionSpan++;
						jobID = sesh.frn_JID;
					}
				}
	      if (sessionSpan == 1) {
	      	var jobInfo = ion.database.Query<JobRow>("SELECT jobName, poNumber, customerNumber, dispatchNumber, techName, systemType, jobLocation, jobAddress FROM JobRow WHERE JID = ?", jobID);
					xls.MergeCells(7, 6, 7, 7);
		      xls.SetCellValue(7, 6, Util.Strings.Job.JOBINFO,1);
		      xls.MergeCells(8,6,9,6);
		      xls.SetCellValue(8, 6, Util.Strings.Job.JOBNAME,1);
		      xls.MergeCells(8,7,9,7);
		      xls.SetCellFormat(9,7,14);
		      xls.SetCellValue(8, 7, jobInfo[0].jobName,14);
		      xls.SetCellValue(10, 6, Util.Strings.Job.PONUMBER, 1);
		      xls.SetCellValue(10, 7, jobInfo[0].poNumber, 14);
		      xls.SetCellValue(11, 6, Util.Strings.Job.CUSTOMERNUMBER, 1);
		      xls.SetCellValue(11, 7, jobInfo[0].customerNumber, 14);
		      xls.SetCellValue(12, 6, Util.Strings.Job.DISPATCHNUMBER, 1);
		      xls.SetCellValue(12, 7, jobInfo[0].dispatchNumber, 14);
		      xls.SetCellValue(13, 6, Util.Strings.Job.TECHNAME, 1);
		      xls.SetCellValue(13, 7, jobInfo[0].techName, 14);
		      xls.MergeCells(14, 6, 16, 6);
		      xls.SetCellValue(14, 6, Util.Strings.Job.JOBADDRESS, 1);
		      xls.MergeCells(14, 7, 16, 7);
		      xls.SetCellFormat(15,7,14);
		      xls.SetCellFormat(16,7,14);
		      xls.SetCellValue(14, 7, jobInfo[0].jobAddress, 14);
		      xls.SetCellValue(17, 6, Util.Strings.Job.JOBLOCATION, 1);
		      xls.SetCellValue(17, 7, jobInfo[0].jobLocation, 16);
		      xls.MergeCells(18,6,19,6);
		      xls.SetCellValue(18, 6, Util.Strings.Job.SYSTEMTYPE, 1);
		      xls.MergeCells(18,7,19,7);
		      xls.SetCellFormat(19,7,14);    
		      xls.SetCellValue(18, 7, jobInfo[0].systemType, 14);
	      } else if (certInfo.Count > 1) {
					xls.MergeCells(7, 6, 7,7);
					xls.SetCellValue(7, 6, Util.Strings.Job.MULTIPLEJOBS,1);
				}

	      xls.MergeCells(deviceCellIndex, 2, deviceCellIndex, 4);
	      xls.SetCellValue(deviceCellIndex, 1, Util.Strings.Report.CREATED, 1);
	      xls.SetCellFormat(deviceCellIndex,3,14);
	      xls.SetCellFormat(deviceCellIndex,4,14);
	      xls.SetCellValue(deviceCellIndex, 2, DateTime.Now.ToLocalTime().ToString(), 2);
	      xls.SetCellFormat(deviceCellIndex,2,14);
	      deviceCellIndex++;
	      xls.MergeCells(deviceCellIndex, 1, deviceCellIndex, 4);
	      xls.SetCellValue(deviceCellIndex, 1, Util.Strings.Report.REPORTDATES, 1);
	      deviceCellIndex++;
	      xls.MergeCells(deviceCellIndex, 1, deviceCellIndex, 4);
	      xls.SetCellFormat(deviceCellIndex,2,14);
	      xls.SetCellFormat(deviceCellIndex,3,14);
	      xls.SetCellFormat(deviceCellIndex,4,14);
	      xls.SetCellValue(deviceCellIndex, 1, ChosenDates.subLeft + " - " + ChosenDates.subRight, 2);
	      xls.SetCellFormat(deviceCellIndex,1,14);
	      deviceCellIndex += 4;
	      
	      xls.SetCellValue(deviceCellIndex,1,Util.Strings.Device.SERIAL_NUMBER,1);
	      xls.SetCellValue(deviceCellIndex,2,Util.Strings.Measure.MINIMUM,1);
	      xls.SetCellValue(deviceCellIndex,3,Util.Strings.Measure.MAXIMUM,1);
	      xls.SetCellValue(deviceCellIndex,4,Util.Strings.Measure.AVERAGE,1);
	      deviceCellIndex++;
	     
	      var defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_pressure");

	      if (dataList[i-1].type.Equals("Temperature")) {
	        defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_temperature");
	      } else if (dataList[i-1].type.Equals("Vacuum")) {
	        defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_vacuum");
	      }
	
	      var lookup = ION.Core.Sensors.UnitLookup.GetUnit(Convert.ToInt32(defaultUnit));
	  		var standardUnit = lookup.standardUnit;
	  		var deviceType = ION.Core.Sensors.UnitLookup.GetSensorTypeFromCode(Convert.ToInt32(defaultUnit));
			
	      var minValue = standardUnit.OfScalar(dataList[i-1].min);
	      var finalMin = minValue.ConvertTo(lookup);
				var formatMin = ION.Core.Sensors.SensorUtils.ToFormattedString(deviceType, finalMin);
				
	      var maxValue = standardUnit.OfScalar(dataList[i-1].max);
	      var finalMax = maxValue.ConvertTo(lookup);
				var formatMax = ION.Core.Sensors.SensorUtils.ToFormattedString(deviceType, finalMax);
				
	      var avgValue = standardUnit.OfScalar(dataList[i-1].avg);
	      var finalAvg = avgValue.ConvertTo(lookup);
				var formatAvg = ION.Core.Sensors.SensorUtils.ToFormattedString(deviceType, finalAvg);
				
				xls.SetCellValue(deviceCellIndex,1, dataList[i-1].serialNumber,14);
				xls.SetCellValue(deviceCellIndex,2, formatMin + " " + lookup,14);
				xls.SetCellValue(deviceCellIndex,3, formatMax + " " + lookup,14);
				xls.SetCellValue(deviceCellIndex,4, formatAvg + " " + lookup,14);
	     	deviceCellIndex += 4;
				var imageRow = deviceCellIndex;
				var imageCol = 1;
				
    		///THIS METHOD ONLY GETS "VISIBLE" CELLS FROM THE TABLE VIEW AND RETURNS NULL FOR ANY OTHERS
    		//var cell = graphTable.CellAt(NSIndexPath.FromRowSection(i,0)) as graphCell;
    		///THIS METHOD SEEMS TO GO THROUGH THE CORE DATA OF THE TABLE AND RETURNS EVERY CELL IT CONTAINS
    		var cell = graphTable.Source.GetCell(graphTable,NSIndexPath.FromRowSection(0,0)) as graphCell;    		
    		var currentDevice = dataList[i-1].serialNumber+"/"+dataList[i-1].sensorIndex;
    		var includedName = cell.cellData.serialNumber+"/"+cell.cellData.sensorIndex;
    		var cellIndex = 1;
    		
    		if(!currentDevice.Equals(includedName)){
	    		do{ 
	    			cell = graphTable.Source.GetCell(graphTable,NSIndexPath.FromRowSection(cellIndex,0)) as graphCell;    		
   			
	    			includedName = cell.cellData.serialNumber+"/"+cell.cellData.sensorIndex;
						cellIndex++;
					} while(!currentDevice.Equals(includedName));
    		}
    		
    		if(ChosenDates.includeList.Contains(includedName)){
    			var originalFrame = cell.plotView.Frame;
    			
	    		UIImageView graphView = new UIImageView(new CGRect(0,0,2000,(2000 * (cell.plotView.Bounds.Height/ cell.plotView.Bounds.Width)))); /// W = 900 H = W * (H/W)
    			//Console.WriteLine("Graphview bounds " + graphView.Bounds);

    			var majorStep = cell.plotView.Model.Axes[0].Maximum / 10;
    			
    			cell.plotView.Model.Axes[0].MajorStep = majorStep;/// BOTTOM AXIS
    			cell.plotView.Model.Axes[0].IsAxisVisible = true;
    			cell.plotView.Model.Axes[0].AxisTitleDistance = 5;
    			cell.plotView.Model.Axes[0].AxisTickToLabelDistance = 1;
    			cell.plotView.Model.Axes[0].AxislineThickness = 1;
    			cell.plotView.Model.Axes[0].AxislineColor = OxyColor.FromRgb(0,0,0);
    			cell.plotView.Model.Axes[0].AxislineStyle = LineStyle.Solid;
    			cell.plotView.Model.Axes[0].MajorGridlineStyle = LineStyle.Solid;
    			cell.plotView.Model.Axes[0].MajorGridlineThickness = 1;
    			cell.plotView.Model.Axes[1].IsAxisVisible = true;/// LEFT AXIS
    			cell.plotView.Model.Axes[1].AxisTitleDistance = 20;
    			cell.plotView.Model.Axes[1].AxislineThickness = 2;
     			cell.plotView.Model.Axes[1].AxislineColor = OxyColor.FromRgb(0,0,0);
    			cell.plotView.Model.Axes[1].AxislineStyle = LineStyle.Solid;
    			cell.plotView.Model.Axes[1].MinorGridlineStyle = LineStyle.Solid;
    			cell.plotView.Model.Axes[1].MajorGridlineStyle = LineStyle.Solid;
    			cell.plotView.Model.Axes[1].MinorGridlineThickness = 1;
    			cell.plotView.Model.Axes[1].MajorGridlineThickness = 1;
    			cell.plotView.Model.PlotMargins = new OxyThickness(75,0,0,65);
    			
					cell.plotView.Frame = new CGRect(0,0,1100,293);
    			
					graphView.Image = UI.UIViewExtensions.Capture(cell.plotView);
					//Console.WriteLine("Image done");
			    using( var ns = new NSAutoreleasePool())
			    {
		        using(UIImage img = graphView.Image) {
	            using (var data = img.AsPNG ())
	            {
								using (Stream fs = data.AsStream())
								{
									TImageProperties ImgProps = new TImageProperties();
									ImgProps.Anchor = new TClientAnchor(TFlxAnchorType.MoveAndDontResize, imageRow, 0, imageCol, 0, imageRow + 18, 255, imageCol + 7, 1024);
									ImgProps.ShapeName = "Graph";
									xls.AddImage(fs, ImgProps);
								}
	            }
	        	}
			    }
    			
			    cell.plotView.Frame = originalFrame;
 
			    xls.MergeCells(imageRow - 1,4,imageRow - 1, 5);
			    xls.SetCellValue(imageRow - 1,4,cell.cellData.serialNumber+"("+cell.cellData.type+")",2);
			    
			    ///LEFT EDGE GRAPH BORDER			    
			    xls.SetCellFormat(imageRow,1,6);
			    xls.SetCellFormat(imageRow + 1,1,imageRow + 18,1,7);
			    xls.SetCellFormat(imageRow + 19,1,8);
			    
			    ///BOTTOM GRAPH BORDER
			    xls.SetCellFormat(imageRow + 19, 2,imageRow + 19,7,9);
			    
			    ///TOP GRAPH BORDER
			    xls.SetCellFormat(imageRow, 2, imageRow, 7, 10);
			    
			    /// RIGHT EDGE GRAPH BORDER
			    xls.SetCellFormat(imageRow,8,11);
			    xls.SetCellFormat(imageRow + 1,8,imageRow + 18,8,12);			    
			    xls.SetCellFormat(imageRow + 19,8,13);			    			    
				}
			}
			if(exportSelect.rawData.On){
				/**********************************************/				
				xls.ActiveSheet = dataList.Count + 1;				
				/**********************************************/
				deviceCellIndex = 1;
	      TXlsNamedRange Range;
	      string RangeName;
	      RangeName = TXlsNamedRange.GetInternalName(InternalNameRange.Print_Titles);
	      Range = new TXlsNamedRange(RangeName, xls.ActiveSheet, xls.ActiveSheet, deviceCellIndex, 1, deviceCellIndex + 2, dataList.Count + 1, 32);
	      xls.SetNamedRange(Range);
	
	      xls.SetCellValue(deviceCellIndex, 1, " ", 1);
	      deviceCellIndex++;
	      xls.SetCellValue(deviceCellIndex, 1, " ", 1);
	      deviceCellIndex++;
	      xls.SetCellValue(deviceCellIndex, 1, Util.Strings.TIME,2);
	      deviceCellIndex++;
				var jobStartIndex = 0;
	      for (int i = deviceCellIndex; i < masterTimes.Count + deviceCellIndex; i++) { 
	        xls.SetCellValue(i, 1, masterTimes[i-deviceCellIndex], 2);
	        if (sessionBreaks.Contains(masterTimes[i-deviceCellIndex])) {
	          xls.SetCellValue(i, 1, masterTimes[i-deviceCellIndex], 3);
	        } else {
	          xls.SetCellValue(i,1, masterTimes[i-deviceCellIndex], 2);
	        }
	        jobStartIndex = i;
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
								
	        //if (defaultUnit.Equals("7")) {
	        //  xls.SetCellValue(measStartIndex, i, dataList[i - 2].type + "(psig/inHg)", 1);
	        //} else if (defaultUnit.Equals("8")){
	        //  xls.SetCellValue(measStartIndex, i, dataList[i - 2].type + "(kg/cm²/cmHg)", 1);
	        //} else {
	          xls.SetCellValue(measStartIndex, i, dataList[i - 2].type + "(" + lookup + ")", 1);
	        //}
	        measStartIndex++;
	        xls.SetCellValue(measStartIndex, i, dataList[i - 2].serialNumber,2);
	        measStartIndex++;
	        xls.SetCellValue(measStartIndex, i, dataList[i - 2].name, 2);
	        measStartIndex++;
	
	        var standardUnit = lookup.standardUnit;
	        var deviceType = ION.Core.Sensors.UnitLookup.GetSensorTypeFromCode(Convert.ToInt32(defaultUnit));
	        var rowIndex = measStartIndex;
	        var compareIndex = 0;
								
	        for (int t = 0; t < masterTimes.Count; t++) {
	          if (compareIndex < dataList[i - 2].times.Count) {
	            if (masterTimes[t].Equals(dataList[i - 2].times[compareIndex].ToString())) {
	              var workingValue = standardUnit.OfScalar(dataList[i - 2].readings[compareIndex]);
	              var finalValue = workingValue.ConvertTo(lookup);
								var formatValue = ION.Core.Sensors.SensorUtils.ToFormattedString(deviceType, finalValue);
	              if (sessionBreaks.Contains(dataList[i - 2].times[compareIndex].ToString())) {
	                xls.SetCellValue(rowIndex, i, formatValue, 5);
	              } else {
	                xls.SetCellValue(rowIndex, i, formatValue, 4);
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
	      jobStartIndex += 2;
	
	      xls.AutofitCol(1, false, 1.1);
				/******************************************/
			}
			xls.ActiveSheet = 1;
			
			/******************************************/
      FlexCelPdfExport pdfExport = new FlexCelPdfExport(xls);
      pdfExport.AllowOverwritingFiles = true;

      var pdfPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);

      try{
        using(FileStream Pdf = new FileStream(pdfPath,FileMode.Create)){
          pdfExport.BeginExport(Pdf);

					for(int i = 1; i <= dataList.Count; i++){
						xls.ActiveSheet = i;
          	pdfExport.ExportSheet();
					}
          
          if(exportSelect.rawData.On){
						xls.ActiveSheet = dataList.Count + 1;
          	pdfExport.ExportSheet(1,pdfExport.TotalPagesInSheet());
					}
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
      mainVC.navigation.PresentViewController (previewController, true, null);
    }
      
    public void includeDeviceStats(XlsFile xls, List<deviceReadings> deviceList, int extraInfoRow, int styleIndex){
			foreach(var device in deviceList){
	      var defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_pressure");
	
	      if (device.type.Equals("Temperature")) {
	        defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_temperature");
	      } else if (device.type.Equals("Vacuum")) {
	        defaultUnit = NSUserDefaults.StandardUserDefaults.StringForKey("settings_units_default_vacuum");
	      }
	
	      var lookup = ION.Core.Sensors.UnitLookup.GetUnit(Convert.ToInt32(defaultUnit));					
    		var standardUnit = lookup.standardUnit;
    		var deviceType = ION.Core.Sensors.UnitLookup.GetSensorTypeFromCode(Convert.ToInt32(defaultUnit));
			
        var minValue = standardUnit.OfScalar(device.min);
        var finalMin = minValue.ConvertTo(lookup);
				var formatMin = ION.Core.Sensors.SensorUtils.ToFormattedString(deviceType, finalMin);
				
        var maxValue = standardUnit.OfScalar(device.max);
        var finalMax = maxValue.ConvertTo(lookup);
				var formatMax = ION.Core.Sensors.SensorUtils.ToFormattedString(deviceType, finalMax);
				
        var avgValue = standardUnit.OfScalar(device.avg);
        var finalAvg = avgValue.ConvertTo(lookup);
				var formatAvg = ION.Core.Sensors.SensorUtils.ToFormattedString(deviceType, finalAvg);
				
				xls.SetCellValue(extraInfoRow,1,device.serialNumber,styleIndex);
				xls.SetCellValue(extraInfoRow,2,formatMin + " " + lookup,styleIndex);
				xls.SetCellValue(extraInfoRow,3,formatMax + " " + lookup,styleIndex);
				xls.SetCellValue(extraInfoRow,4,formatAvg + " " + lookup,styleIndex);
				extraInfoRow++;
			}
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