using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using UIKit;
using Foundation;
using CoreGraphics;
using QuickLook;

using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.Text;

using ION.Core.Database;
using ION.Core.Report;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.iOS;

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

		public List<deviceReadings> selectedData;
		public double dateMultiplier;

		public GraphingView (UIView mainView, UIViewController mainVC, List<deviceReadings> pressuresTemperatures)
		{			
			selectedData = pressuresTemperatures;

			topCell = 0;

      foreach (var device in selectedData) {
        if (!ChosenDates.includeList.Contains(device.name)) {
          ChosenDates.includeList.Add(device.name);
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

        createButtons (mainVC);

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
		public void createButtons(UIViewController mainVC){
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

        ChooseReportType(mainVC);

//        var reportData = new List<deviceReadings>();
//        foreach(var sensor in selectedData){
//          if(ChosenDates.includeList.Contains(sensor.name)){
//            reportData.Add(sensor);
//          }
//        }
//        var report = new xmlReport(ChosenDates.subLeft, ChosenDates.subRight,reportData);
//        Console.WriteLine("Report will start at date: " + report.startDate.ToString() + " and end with date: " + report.endDate.ToString());
//        foreach(var deviceData in report.Sensors){
//          Console.WriteLine("Using deviceSN: " + deviceData.deviceSN + " from session: " + deviceData.SID);
//          if(deviceData.JID > 0){
//            Console.WriteLine("Associated to JID: " + deviceData.JID);
//          }
//        }
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
				if(bottomCell >= ChosenDates.includeList.Count - 1){
					bottomCell = ChosenDates.includeList.Count - 1;
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

    public void ChooseReportType(UIViewController mainVC){
      UIAlertView reportBox = new UIAlertView("Create Report", "Choose a format", null,"Cancel","Spreadsheet","PDF");
      reportBox.Show();
      reportBox.Clicked += (sender, e) => {
        if(e.ButtonIndex.Equals(1)){
          UIAlertView messageBox = new UIAlertView("Please Wait....", "Creating Spreadsheet", null,null,null);
          messageBox.Show();
          createSpreadsheet(messageBox,mainVC);
        } else if (e.ButtonIndex.Equals(2)){
          Console.WriteLine("Create A PDF");
          UIAlertView messageBox = new UIAlertView("Please Wait....", "Creating PDF", null,null,null);
          messageBox.Show();
          messageBox.DismissWithClickedButtonIndex(0,true);
          //createPDF(messageBox,mainVC);
        }
      };
    }

    public void createSpreadsheet(UIAlertView messageBox, UIViewController mainVC){
      Console.WriteLine("Using date range: " + ChosenDates.subLeft.ToString() + " - " + ChosenDates.subRight.ToString());
      foreach (var device in ChosenDates.includeList) {
        Console.WriteLine("Using device: " + device);
      }
      messageBox.DismissWithClickedButtonIndex(0,true);
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