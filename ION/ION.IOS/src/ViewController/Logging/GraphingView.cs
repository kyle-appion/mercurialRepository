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

using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.Text;

using ION.Core.Database;
using ION.Core.Report;
using ION.Core.App;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.iOS;

using FlexCel.Core;
using FlexCel.XlsAdapter;

using DocumentFormat.OpenXml;

namespace ION.IOS.ViewController.Logging
{
	public struct deviceReadings {
		public string type;
		public string name;
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

		public UIButton menuButton;
		public UIButton resetButton;
		public UIButton exportGraph;
		public UIButton scrollDown;
    public UIButton extendedDown;
		public UIButton scrollUp;
    public UIButton extendedUp;

		public UITableView graphTable;

		public UIPanGestureRecognizer leftDrag;
		public UIPanGestureRecognizer rightDrag;

		public UIImageView leftTrackerCircle;
		public UIImageView rightTrackerCircle;

		public UILabel subDates;

		public double trackerHeight;
		public int topCell;
		public int bottomCell;
    public string fileName;

		public List<deviceReadings> selectedData;
		public double dateMultiplier;

    public GraphingView (UIView mainView, UIViewController mainVC, List<deviceReadings> pressuresTemperatures,ObservableCollection<int> sessions)
		{			
			selectedData = pressuresTemperatures;

			topCell = 0;

      foreach (var device in selectedData) {
        if (!ChosenDates.includeList.Contains(device.name)) {
          ChosenDates.includeList.Add(device.name);
        }
      }
      for(int i = 0; i < ChosenDates.includeList.Count;i++){
        while(selectedData[i].name != ChosenDates.includeList[i]){
          var tmpMove = selectedData[i];
          selectedData.RemoveAt(i);
          selectedData.Add(tmpMove);
        }
      }
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

        createButtons (mainVC,sessions);

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
        Console.WriteLine("Each index is " + dateMultiplier + " pixels wide");
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

			leftDrag = new UIPanGestureRecognizer (() => {
				var xPlot = leftTrackerCircle.Center.X - (.1 * mainView.Bounds.Width);
				if(leftDrag.State == UIGestureRecognizerState.Changed){
          var index = (int)xPlot/(int)dateMultiplier;
          if(ChosenDates.allIndexes.ContainsKey(index)){
            ChosenDates.subLeft = DateTime.Parse(ChosenDates.allIndexes[index]);
          }
          subDates.Text = "Start: " + ChosenDates.subLeft.ToString () + "\nFinish: " + ChosenDates.subRight.ToString();
				}
				if(leftDrag.LocationInView(mainView).X > (.1 * mainView.Bounds.Width) && leftDrag.LocationInView(mainView).X < (.75 * mainView.Bounds.Width)){
					if(rightTrackerCircle.Center.X - 12 > leftDrag.LocationInView(mainView).X){
						leftTrackerView.Frame = new CGRect(.1 * mainView.Bounds.Width,.15 * mainView.Bounds.Height, leftDrag.LocationInView(mainView).X - (.1 * mainView.Bounds.Width), trackerHeight);
						leftTrackerCircle.Frame = new CGRect(leftDrag.LocationInView(mainView).X - 12,.15 * mainView.Bounds.Height + trackerHeight,24,26);
					}
				}
				if (leftDrag.State == UIGestureRecognizerState.Ended){          
          leftTrackerView.BackgroundColor = UIColor.Red;
          subDates.TextColor = UIColor.Red;
					UIView.Transition(
						withView:subDates,
						duration:.5,
						options: UIViewAnimationOptions.TransitionCrossDissolve,
						animation: () =>{
              subDates.TextColor = UIColor.Red;
							subDates.TextColor = UIColor.Black;
              leftTrackerView.BackgroundColor = UIColor.Gray;
						},
						completion: () =>{}
					);
				}
			}); 
			 
			leftTrackerCircle.AddGestureRecognizer (leftDrag);

			rightTrackerView = new UIView (new CGRect (.915 * graphTable.Bounds.Width,.15 * gView.Bounds.Height, 1, trackerHeight));
			rightTrackerView.BackgroundColor = UIColor.Gray;
			rightTrackerView.Alpha = .4f;

			rightTrackerCircle = new UIImageView (new CGRect (0, .15 * gView.Bounds.Height + trackerHeight,24, 26));
			rightTrackerCircle.Image = UIImage.FromBundle ("ic_tracker_circle");
			rightTrackerCircle.UserInteractionEnabled = true;

			var trackerRect = rightTrackerCircle.Center;
			trackerRect.X = rightTrackerView.Center.X - (.5f * rightTrackerView.Bounds.Width);
			rightTrackerCircle.Center = trackerRect;

			rightDrag = new UIPanGestureRecognizer (() => {
				var xPlot = rightTrackerCircle.Center.X - (.1 * mainView.Bounds.Width);
				if(rightDrag.State == UIGestureRecognizerState.Changed){          
          var index = (int)xPlot/(int)dateMultiplier;
          Console.WriteLine("Using index: " + index);
          if(ChosenDates.allIndexes.ContainsKey(index)){
            ChosenDates.subRight = DateTime.Parse(ChosenDates.allIndexes[index]);
          }
          subDates.Text = "Start: " + ChosenDates.subLeft.ToString () + "\nFinish: " + ChosenDates.subRight.ToString();
				}
        if(rightDrag.LocationInView(mainView).X > (.11 * mainView.Bounds.Width) && rightDrag.LocationInView(mainView).X < (.915 * graphTable.Bounds.Width)){
					if(leftTrackerCircle.Center.X + 12 < rightDrag.LocationInView(mainView).X){
						rightTrackerView.Frame = new CGRect(rightDrag.LocationInView(mainView).X,.15 * mainView.Bounds.Height,(.915 * graphTable.Bounds.Width) - rightDrag.LocationInView(mainView).X, trackerHeight);
						rightTrackerCircle.Frame = new CGRect(rightDrag.LocationInView(mainView).X - 12,.15 * mainView.Bounds.Height + trackerHeight,24,26);
					}
				}
				if (rightDrag.State == UIGestureRecognizerState.Ended){
          
          rightTrackerView.BackgroundColor = UIColor.FromRGB(49, 111, 18);
					UIView.Transition(
						withView:subDates,
						duration:.5,
						options: UIViewAnimationOptions.TransitionCrossDissolve,
						animation: () =>{
              subDates.TextColor = UIColor.Red;
							subDates.TextColor = UIColor.Black;
              rightTrackerView.BackgroundColor = UIColor.Gray;
						},
						completion: () =>{}
					);
				}
			});
				
			rightTrackerCircle.AddGestureRecognizer (rightDrag);

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
    public void createButtons(UIViewController mainVC,ObservableCollection<int> sessions){
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
			resetButton.TouchUpInside += (sender, e) => {
				resetButton.BackgroundColor = UIColor.Red;
				graphTable.Source = new graphingTableSource (selectedData, gView, .8 * gView.Bounds.Height);
				graphTable.ReloadData ();
			};

      exportGraph = new UIButton (new CGRect (.7 * gView.Bounds.Width, .89 * mainVC.View.Bounds.Height,.25 * gView.Bounds.Width,.08 * gView.Bounds.Height));
      exportGraph.BackgroundColor = UIColor.FromRGB(49, 111, 18);
			exportGraph.SetTitle ("Export", UIControlState.Normal); 
			exportGraph.Layer.CornerRadius = 8; 

			exportGraph.TouchUpInside += (sender, e) => {
        
				Console.WriteLine("using graph data starting at " + ChosenDates.subLeft + " and ending at " + ChosenDates.subRight);
        exportGraph.BackgroundColor = UIColor.FromRGB(49, 111, 18);

        ChooseReportType(mainVC,sessions);

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
        Console.WriteLine("Scrolling down to index: " + bottomCell);
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

    public async void ChooseReportType(UIViewController mainVC,ObservableCollection<int> sessions){
      UIAlertView reportBox = new UIAlertView("Create Report", "Choose a format", null,"Cancel","Spreadsheet","PDF");
      reportBox.Show();
      reportBox.Clicked += async (sender, e) => {
        if(e.ButtonIndex.Equals(1)){
          var sessionBreaks = new List<string>();
          var data = categorizeData(sessions,sessionBreaks);
          Console.WriteLine("Total number of sessions: " + sessionBreaks.Count);
          UIAlertView messageBox = new UIAlertView("Please Wait....", "Creating Spreadsheet", null,null,null);
          messageBox.Show();
          await Task.Delay(TimeSpan.FromMilliseconds (500));
          createSpreadsheet(messageBox,mainVC,data,sessionBreaks);
        } else if (e.ButtonIndex.Equals(2)){
          //var data = categorizeData(sessions);
          Console.WriteLine("Create A PDF");
          UIAlertView messageBox = new UIAlertView("Please Wait....", "Creating PDF", null,null,null);
          messageBox.Show();
          messageBox.DismissWithClickedButtonIndex(0,true);
          //createPDF(messageBox,mainVC);
        }
      };
    }

    public List<deviceReadings> categorizeData(ObservableCollection<int> sessions, List<string> sessionBreaks){
      var ion = AppState.context;
      var deviceList = new List<deviceReadings>();
      foreach (var session in sessions) {
        var endtime = ion.database.Query<SensorMeasurementRow>("SELECT recordedDate FROM SensorMeasurementRow WHERE frn_SID = ? ORDER BY recordedDate DESC LIMIT 1",session);
        sessionBreaks.Add(endtime[0].recordedDate.ToLocalTime().ToString());
      }
      var paramList = new List<string>();

      foreach (var num in sessions) {
        paramList.Add('"' + num.ToString() + '"');
      }
      foreach (var package in ChosenDates.includeList) {
        var device = new deviceReadings();
        var bundle = ion.database.Query<SensorMeasurementRow>("SELECT sensorIndex, recordedDate,measurement FROM SensorMeasurementRow Where frn_SID IN (" + string.Join(",",paramList.ToArray()) + ") AND serialNumber = ? AND recordedDate BETWEEN ? AND ? ORDER BY recordedDate ASC",package,ChosenDates.subLeft, ChosenDates.subRight.AddSeconds(5));
        device.name = package;
        device.readings = new List<double>();
        device.times = new List<DateTime>();
        //device.SID = session;
        foreach (var compare in selectedData) {
          if (compare.name.Equals(device.name)) {
            device.type = compare.type;
            break;
          }
        }
        foreach (var entry in bundle) {
          device.readings.Add(entry.measurement);
          device.times.Add(entry.recordedDate.ToLocalTime());
        }
        deviceList.Add(device);
      }

      return deviceList;
    }
    public void createSpreadsheet(UIAlertView messageBox, UIViewController mainVC, List<deviceReadings> dataList, List<string> sessionBreaks){
      messageBox.Dismissed += previewSpreadsheet;
      fileName = "test.xlsx";
      //fileName = DateTime.UtcNow.ToLocalTime();
      Console.WriteLine("Filename WOULD be: " + DateTime.UtcNow.ToLocalTime());
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
      var needOrdered = new List<DateTime>(); 
      var masterTimes = new List<string>(); 
     
      foreach (var device in dataList) {
        foreach (var time in device.times) {
          if(!needOrdered.Contains(time.Date)){
            needOrdered.Add(time);
          }
        }
      }
      needOrdered.Sort();

      foreach (var time in needOrdered) {
        if(!masterTimes.Contains(time.ToString())){
          masterTimes.Add(time.ToString());
          Console.WriteLine("Time: " + time.ToString());
        }
      }

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
        xls.SetCellValue(2, i, dataList[i - 2].name,2);
       
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
      xls.Save(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName));

      messageBox.DismissWithClickedButtonIndex(0, false);
    }

    public void previewSpreadsheet(object sender, EventArgs e){
      var window = UIApplication.SharedApplication.Windows[0].RootViewController;
      var vc = window;

      var dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);
      QLPreviewItemBundle prevItem = new QLPreviewItemBundle (fileName, dir);
      QLPreviewController previewController = new QLPreviewController ();
      previewController.DataSource = new PreviewControllerDS (prevItem);
      vc.PresentViewController (previewController, true, null);
    }

    public void createPDF(UIAlertView messageBox,UIViewController mainVC){
      var allGraphs = new UIScrollView(new CGRect(0,0,gView.Bounds.Width,gView.Bounds.Height));
      var reportHolder = new DataLoggingReport();

      reportHolder.title = "Logging Report";
      reportHolder.subtitle = ChosenDates.subLeft.ToString() + " - " + ChosenDates.subRight.ToString();

      var cellHeight = allGraphs.Bounds.Height / 8f;
      for (int i = 0; i < selectedData.Count; i++) {
        var cell = graphTable.DequeueReusableCell("graphingCell") as graphCell;
        if (cell == null) {
          cell = new UITableViewCell(UITableViewCellStyle.Default, "graphingCell") as graphCell;
        } 
        cell.setupGraph (selectedData [i], selectedData, allGraphs.Bounds.Width, cellHeight, trackerHeight, gView, graphTable);
        cell.plotView.Frame = new CGRect(0,i * cellHeight,allGraphs.Bounds.Width,cellHeight);

        allGraphs.AddSubview(cell.plotView);
      }

      var convertedImage = ION.IOS.UI.UIViewExtensions.Capture(allGraphs);

      reportHolder.screenshot = convertedImage.AsPNG().ToArray();
      Console.WriteLine("First byte array length: " + reportHolder.screenshot.Length);

      reportHolder.tableData = new string[selectedData.Count,4];

      for (int n = 0; n < selectedData.Count; n++) {
        var lowest = 99999999999.0;
        var highest = -9999999999.0;
        var total = 0;
        foreach (var measurement in selectedData[n].readings) {
          if (measurement > highest) {
            highest = measurement;
          }
          if (measurement < lowest) {
            lowest = measurement;
          }
          total++;
        }

        reportHolder.tableData[n, 0] = "Device SN: " + selectedData[n].name;
        reportHolder.tableData[n, 1] = "Lowest Measurement: " + lowest;
        reportHolder.tableData[n, 2] = "Highest Measurement: " + highest;
        reportHolder.tableData[n, 3] = "Number of Measurements: " + total;
      }

      string outputPath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);

      MemoryStream imageStream = new MemoryStream(reportHolder.screenshot);

      PdfFixedDocument document = new PdfFixedDocument();
      PdfStandardFont helveticaBoldTitle = new PdfStandardFont(PdfStandardFontFace.HelveticaBold, 16);
      PdfStandardFont helveticaSection = new PdfStandardFont(PdfStandardFontFace.Helvetica, 10);

      PdfPage page = document.Pages.Add();
     
      DrawImages(page, imageStream, helveticaBoldTitle, helveticaSection);

      page = document.Pages.Add();

      DrawExtraInfo(page, imageStream, helveticaBoldTitle, helveticaSection, reportHolder);

      PreviewOutputInfo output = new PreviewOutputInfo(document, "Graph_Testing.pdf");

      output.Document.Save(outputPath + "/" + output.FileName);

      messageBox.DismissWithClickedButtonIndex(0, true);

      QLPreviewController previewController = new QLPreviewController();
      previewController.DataSource = new PDFViewDataSource(output.FileName);
      mainVC.PresentViewController(previewController, true, null);
    }


    private static void DrawImages(PdfPage page, Stream imageStream, PdfFont titleFont, PdfFont sectionFont)
    {
      PdfBrush brush = new PdfBrush();

      PdfPngImage jpeg = new PdfPngImage(imageStream);

      page.Graphics.DrawString(ChosenDates.subLeft.ToString() + "-" + ChosenDates.subRight.ToString(), titleFont, brush, 160, 50);

      // Draw the image on the page
      page.Graphics.DrawImage(jpeg, 3, 90, 600, 600);

      page.Graphics.CompressAndClose();
    }

    private static void DrawExtraInfo(PdfPage page, Stream imageStream, PdfFont titleFont, PdfFont sectionFont, DataLoggingReport report)
    {
      PdfBrush brush = new PdfBrush();

      PdfPen blackPen = new PdfPen(PdfRgbColor.Black, 0.5);
      page.Graphics.DrawRectangle(blackPen, 50, 20, 500, 750, 0);

      var pageSize = 750;
      var eInfoSpace = (double)pageSize / report.tableData.GetLength(0);
      var yAxis = 20.0;
      for(int i = 0; i < report.tableData.GetLength(0);i++) {
        page.Graphics.DrawString(report.tableData[i,0], titleFont, brush, 180, yAxis);
       
        page.Graphics.DrawString(report.tableData[i,1], titleFont, brush, 52, yAxis + (.25 * eInfoSpace));

        page.Graphics.DrawString(report.tableData[i,2], titleFont, brush, 52, yAxis + (.5 * eInfoSpace));

        page.Graphics.DrawString(report.tableData[i,3], titleFont, brush, 52, yAxis + (.75 * eInfoSpace));
        yAxis = ((i + 1) * eInfoSpace) + 20;
      }

      page.Graphics.CompressAndClose();
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