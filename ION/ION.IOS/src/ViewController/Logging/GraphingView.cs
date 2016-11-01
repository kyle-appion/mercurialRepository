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
using ION.Core.Devices;

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
		int[] types = {3,4,5};
		public double trackerHeight;
		public int topCell;
		public int bottomCell;
    public string fileName;
    public int fallOff = 0;

		public List<deviceReadings> selectedData;
		public double dateMultiplier;

    public GraphingView (UIView mainView, IONPrimaryScreenController viewController, List<deviceReadings> pressuresTemperatures,ObservableCollection<int> sessions)
		{
			selectedData = pressuresTemperatures;
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
			//exportSelect = new ReportType(gView);
			//gView.AddSubview(exportSelect.blackoutView);
			//gView.AddSubview(exportSelect.popupView);
			//gView.BringSubviewToFront(exportSelect.popupView);
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
			resetButton.SetTitle ("Reset", UIControlState.Normal);
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
			exportGraph.SetTitle ("Export", UIControlState.Normal); 
			exportGraph.Layer.CornerRadius = 5f; 

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
    /// Shows a popup that lets the user choose a spreadsheet or a pdf document of their chosen session/device/time values
    /// </summary>
    /// <param name="sessions">List of sessions included in the graphing</param>
    public void ChooseReportType(ObservableCollection<int> sessions){
		/***************************DETAILED VERSION*****************************/	  
 		//	exportSelect.blackoutView.Hidden = false;
 		//	exportSelect.popupView.Hidden = false;
 			
 		//	exportSelect.pdfExport.TouchUpInside += async (sender, e) => {
   //       var sessionBreaks = new List<string>();
   //       var data = categorizeData(sessions,sessionBreaks);
   //       UIAlertView messageBox = new UIAlertView("Please Wait....", "Creating PDF", null,null,null);
   //       messageBox.Show();
   //       await Task.Delay(TimeSpan.FromMilliseconds (100));
   //       createPDF(messageBox,data,sessionBreaks,sessions);
			//		NSUserDefaults.StandardUserDefaults.SetString(exportSelect.pdfType.ToString(),"user_pdf_default");          
			//		NSUserDefaults.StandardUserDefaults.SetString(Convert.ToInt32(exportSelect.rawData.On).ToString(),"user_data_default");
   //       exportSelect.closeExport();
			//};
			
  	//	exportSelect.spreadsheetExport.TouchUpInside += async (sender, e) => {
   //       var sessionBreaks = new List<string>();
   //       var data = categorizeData(sessions,sessionBreaks);
   //       UIAlertView messageBox = new UIAlertView("Please Wait....", "Creating Spreadsheet", null,null,null);
   //       messageBox.Show();
   //       await Task.Delay(TimeSpan.FromMilliseconds (100));
   //       createSpreadsheet(messageBox,data,sessionBreaks,sessions);
   //       exportSelect.closeExport();
			//		NSUserDefaults.StandardUserDefaults.SetString(exportSelect.spreadsheetType.ToString(),"user_spreadsheet_default");
			//};

			/*********************ORIGINAL VERSION***************************/			
      UIAlertView reportBox = new UIAlertView(Util.Strings.Report.SELF, Util.Strings.Report.CHOOSEFORMAT, null,Util.Strings.CANCEL,Util.Strings.Report.CREATESPREADSHEET,Util.Strings.Report.CREATEPDF);
      reportBox.Show();
      reportBox.Clicked += async (sender, e) => {
        
        if(e.ButtonIndex.Equals(1)){          
          var sessionBreaks = new List<string>();
          var data = categorizeData(sessions,sessionBreaks);
          UIAlertView messageBox = new UIAlertView(Util.Strings.Report.PLEASEWAIT, Util.Strings.Report.TOASTSPREADSHEET, null,null,null);
          messageBox.Show();
          await Task.Delay(TimeSpan.FromMilliseconds (100));
          createSpreadsheet(messageBox,data,sessionBreaks,sessions);

        } else if (e.ButtonIndex.Equals(2)){          
          var sessionBreaks = new List<string>();
          var data = categorizeData(sessions,sessionBreaks);
          UIAlertView messageBox = new UIAlertView(Util.Strings.Report.PLEASEWAIT, Util.Strings.Report.TOASTPDF, null,null,null);
          messageBox.Show();
          await Task.Delay(TimeSpan.FromMilliseconds (100));
          createPDF(messageBox,data,sessionBreaks,sessions);
          
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
    	//Console.WriteLine("CategorizeData called");
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

        var timesReadings = ion.database.Query<SensorMeasurementRow>("SELECT recordedDate, measurement FROM SensorMeasurementRow WHERE serialNumber = ? and sensorIndex = ?  AND frn_SID in (" + string.Join(",",paramList.ToArray()) + ") ORDER BY recordedDate ASC",package.serialNumber,sIndex);
				
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
      //if(exportSelect.spreadsheetType == 0){
      //	fileName = DateTime.UtcNow.ToLocalTime().ToString("MM-dd-yy hh:mm:ss tt") + ".xlsx";
      //} else {
      //	fileName = DateTime.UtcNow.ToLocalTime().ToString("MM-dd-yy hh:mm:ss tt") + ".csv";
      //	numberFormat = "0.00";
      //}
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

      XlsFile xls = new XlsFile(2, TExcelFileFormat.v2013, true);
      xls.AllowOverwritingFiles = true;
      xls.ActiveSheet = 1;
      xls.SheetName = Util.Strings.Report.DATALOGGED;
      xls.ActiveSheet = 2;
      xls.SheetName = Util.Strings.Report.DEVICEINFO;

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
      	var jobInfo = ion.database.Query<JobRow>("SELECT jobName, poNumber, customerNumber, dispatchNumber FROM JobRow WHERE JID = ?",jobID);
				xls.MergeCells(1, 5, 1, 6);
	      xls.SetCellValue(1, 5, Util.Strings.Job.JOBINFO,1);
	      xls.SetCellValue(2, 5, Util.Strings.Job.JOBNAME,1);
	      xls.SetCellValue(2, 6, jobInfo[0].jobName,2);
	      xls.SetCellValue(3, 5, Util.Strings.Job.PONUMBER, 1);
	      xls.SetCellValue(3, 6, jobInfo[0].poNumber, 2);
	      xls.SetCellValue(4, 5, Util.Strings.Job.CUSTOMERNUMBER, 1);
	      xls.SetCellValue(4, 6, jobInfo[0].customerNumber, 2);
	      xls.SetCellValue(5, 5, Util.Strings.Job.DISPATCHNUMBER, 1);
	      xls.SetCellValue(5, 6, jobInfo[0].dispatchNumber, 2);   
	      xls.AutofitRow(1,5,true,true,1.1);
				//xls.KeepRowsTogether(1,5,1,false);
				//xls.AutoPageBreaks();   
      } else if (certInfo.Count > 1) {
				xls.MergeCells(2, 1, 2,2);
				xls.SetCellValue(2, 1, Util.Strings.Job.MULTIPLEJOBS,1);
	      xls.AutofitRow(1,true,1.1);	      
				//xls.KeepRowsTogether(1,3,1,false);
				//xls.AutoPageBreaks();   
			}
			
    //  var headerStartIndex = 3;

    //  if (dataList.Count > 3) {
    //    headerStartIndex = (dataList.Count + 1) / 2;
    //    if(headerStartIndex < 3){
				//	headerStartIndex = 3;
				//}
    //  }
      
      xls.MergeCells(1, 1, 1, 3);
      xls.SetCellValue(1, 1, "Devices Used",2);
      xls.SetCellValue(2, 1, "Serial Number", 1);
      xls.SetCellValue(2, 2, "Name", 1);
      xls.SetCellValue(2, 3, "Certification Date", 1);

      int deviceCellIndex = 3;
      foreach (var device in dataList) {
        xls.SetCellValue(deviceCellIndex, 1, device.serialNumber, 2);
        xls.SetCellValue(deviceCellIndex, 2, device.name, 2);
        if (string.IsNullOrEmpty(device.nistDate) || device.nistDate.Length > 10) {
          xls.SetCellValue(deviceCellIndex, 3, "N/A", 2);
        } else {
          xls.SetCellValue(deviceCellIndex, 3, device.nistDate, 2);
        }
        deviceCellIndex++;
      }
      
      deviceCellIndex++;
      xls.MergeCells(deviceCellIndex, 2, deviceCellIndex, 3);
      xls.SetCellValue(deviceCellIndex, 1, "Report Created", 1);
      xls.SetCellValue(deviceCellIndex, 2, DateTime.Now.ToLocalTime().ToString(), 2);
      deviceCellIndex++;
      xls.MergeCells(deviceCellIndex, 1, deviceCellIndex, 3);
      xls.SetCellValue(deviceCellIndex, 1, "Report Dates", 1);
      deviceCellIndex++;
      xls.MergeCells(deviceCellIndex, 1, deviceCellIndex, 3);
      xls.SetCellValue(deviceCellIndex, 1, ChosenDates.subLeft + " - " + ChosenDates.subRight, 2);
      deviceCellIndex+= 2;
      
			xls.AutofitCol(3,false,1.1);
			xls.AutofitCol(4,false,1.1);
			xls.AutofitCol(5,false,1.1);
			xls.AutofitCol(6,false,1.1);
			
			xls.ActiveSheet = 1;
			
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
              var formatValue = ION.Core.Sensors.SensorUtils.ToFormattedString(deviceType, finalValue);
              
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
      masterTimes.Sort();

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
      blackout.HAlignment = THFlxAlignment.center;
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

      xls.MergeCells(1, 2, 1, 4);
      xls.SetCellValue(1, 2, "Devices Used",2);
      xls.SetCellValue(2, 2, "Serial Number", 1);
      xls.SetCellValue(2, 3, "Name", 1);
      xls.SetCellValue(2, 4, "Certification Date", 1);

      int deviceCellIndex = 3;
      foreach (var device in dataList) {
        xls.SetCellValue(deviceCellIndex, 2, device.serialNumber, 2);
        xls.SetCellValue(deviceCellIndex, 3, device.name, 2);
        if (string.IsNullOrEmpty(device.nistDate) || device.nistDate.Length > 10) {
          xls.SetCellValue(deviceCellIndex, 4, "N/A", 2);
        } else {
          xls.SetCellValue(deviceCellIndex, 4, device.nistDate, 2);
        }
        deviceCellIndex++;
      }
      
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
      	var jobInfo = ion.database.Query<JobRow>("SELECT jobName, poNumber, customerNumber, dispatchNumber FROM JobRow WHERE JID = ?",jobID);
				xls.MergeCells(2, 6, 2, 7);
	      xls.SetCellValue(2, 6, "Job Info",1);
	      xls.SetCellValue(3, 6, "Job Name",1);
	      xls.SetCellValue(3, 7, jobInfo[0].jobName,2);
	      xls.SetCellValue(4, 6, "PO Number", 1);
	      xls.SetCellValue(4, 7, jobInfo[0].poNumber, 2);
	      xls.SetCellValue(5, 6, "Customer Number", 1);
	      xls.SetCellValue(5, 7, jobInfo[0].customerNumber, 2);
	      xls.SetCellValue(6, 6, "Dispatch Number", 1);
	      xls.SetCellValue(6, 7, jobInfo[0].dispatchNumber, 2);
				//xls.KeepRowsTogether(1,5,1,false);
				//xls.AutoPageBreaks();
      } else if (certInfo.Count > 1) {
				xls.MergeCells(2, 6, 2,7);
				xls.SetCellValue(2, 6, "Spans multiple jobs",1);
				//xls.KeepRowsTogether(1,3,1,false);
				//xls.AutoPageBreaks();
			}
      
      deviceCellIndex++;
      xls.MergeCells(deviceCellIndex, 3, deviceCellIndex, 4);
      xls.SetCellValue(deviceCellIndex, 2, "Report Created", 1);
      xls.SetCellValue(deviceCellIndex, 3, DateTime.Now.ToLocalTime().ToString(), 2);
      deviceCellIndex++;
      xls.MergeCells(deviceCellIndex, 2, deviceCellIndex, 4);
      xls.SetCellValue(deviceCellIndex, 2, "Report Dates", 1);
      deviceCellIndex++;
      xls.MergeCells(deviceCellIndex, 2, deviceCellIndex, 4);
      xls.SetCellValue(deviceCellIndex, 2, ChosenDates.subLeft + " - " + ChosenDates.subRight, 2);
      deviceCellIndex+= 2;
      
      xls.SetCellValue(deviceCellIndex,3,"Serial #",1);
      xls.SetCellValue(deviceCellIndex,4,"Minimum ",1);
      xls.SetCellValue(deviceCellIndex,5,"Maximum",1);
      xls.SetCellValue(deviceCellIndex,6,"Average ",1);
      deviceCellIndex++;
     	var extraInfoRow = deviceCellIndex;
     
     	includeDeviceStats(xls,dataList,extraInfoRow);
     	
			var imageRow = deviceCellIndex + dataList.Count + 1;
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
					Console.WriteLine("Image done");
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
			    if((i + 1) % 2 == 0){
			    	imageRow += 9;		    
						imageCol = 2;
					} else {				
						imageCol = 6;
					}
				}
			}
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
      xls.SetCellValue(deviceCellIndex, 1, "Time",2);
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
			
			xls.ActiveSheet = 1;
			
			/******************************************/
      FlexCelPdfExport pdfExport = new FlexCelPdfExport(xls);
      pdfExport.AllowOverwritingFiles = true;

      var pdfPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);

      try{
        using(FileStream Pdf = new FileStream(pdfPath,FileMode.Create)){
          pdfExport.BeginExport(Pdf);

          pdfExport.ExportSheet();
					xls.ActiveSheet = 2;
          pdfExport.ExportSheet(1,pdfExport.TotalPagesInSheet());

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
      
    public void includeDeviceStats(XlsFile xls, List<deviceReadings> deviceList, int extraInfoRow){
					xls.ActiveSheet = 1;
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
						
						xls.SetCellValue(extraInfoRow,3,device.serialNumber,2);
						xls.SetCellValue(extraInfoRow,4,formatMin + " " + lookup,2);
						xls.SetCellValue(extraInfoRow,5,formatMax + " " + lookup,2);
						xls.SetCellValue(extraInfoRow,6,formatAvg + " " + lookup,2);
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