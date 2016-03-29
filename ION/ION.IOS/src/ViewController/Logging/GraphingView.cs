using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;
using Foundation;
using CoreGraphics;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.iOS;

namespace ION.IOS.ViewController.Logging
{
	public struct deviceReadings {
		public string type;
		public string name;
		public List<double> readings;
		public List<DateTime> times;
		public bool included;
	}

	public static class ChosenDates {
		public static DateTime subLeft;
		public static DateTime subRight;
		public static List<string> includeList;
		//public static List<string> allTimes;
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
		public UIButton scrollUp;

		public UITableView graphTable;

		public UIPanGestureRecognizer leftDrag;
		public UIPanGestureRecognizer rightDrag;

		public DateTime earliest;
		public DateTime latest;

		public UIImageView leftTrackerCircle;
		public UIImageView rightTrackerCircle;

		public UILabel subDates;

		public double trackerHeight;
		public int topCell;
		public int bottomCell;

		public List<deviceReadings> selectedData;
		public int deviceCount;
		public double dateMultiplier;

		public GraphingView (UIView mainView, List<deviceReadings> pressuresTemperatures)
		{			
			selectedData = pressuresTemperatures;
			deviceCount = selectedData.Count;
			earliest = DateTime.UtcNow.AddDays (10);
			latest = DateTime.Parse("01/01/2000");
			topCell = 0;
			ChosenDates.includeList = new List<string> ();
			//ChosenDates.allTimes = new List<string> ();
			getEarliestAndLatest ();

      //gView = new UIView (new CGRect (.01 * mainView.Bounds.Width,.14 * mainView.Bounds.Height, .98 * mainView.Bounds.Width, .86 * mainView.Bounds.Height));
			gView = new UIView (new CGRect (0,0, mainView.Bounds.Width, mainView.Bounds.Height));
			gView.BackgroundColor = UIColor.White;
			gView.Layer.CornerRadius = 8;
			gView.Layer.BorderColor = UIColor.Black.CGColor;
			gView.Layer.BorderWidth = 1f;

			switch (deviceCount) {
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

			createButtons ();
      if (deviceCount > 0) {
        graphTable = new UITableView(new CGRect(.1 * gView.Bounds.Width, .075 * gView.Bounds.Height, .85 * gView.Bounds.Width, trackerHeight));
        graphTable.BackgroundColor = UIColor.Clear;
        graphTable.Layer.BorderColor = UIColor.Black.CGColor;
        graphTable.Layer.BorderWidth = 1f;
        graphTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
        graphTable.ScrollEnabled = false;
        graphTable.RegisterClassForCellReuse(typeof(graphCell), "graphingCell");

        CreateGraphTrackers(mainView);

        graphTable.Source = new graphingTableSource(selectedData, gView, earliest, latest, leftTrackerView, rightTrackerView, trackerHeight, leftTrackerCircle, rightTrackerCircle, subDates);
        graphTable.ReloadData();

        gView.AddSubview(graphTable);
        gView.AddSubview(leftTrackerView);
        gView.AddSubview(leftTrackerCircle);
        gView.AddSubview(rightTrackerView);
        gView.AddSubview(rightTrackerCircle);
        gView.AddSubview(subDates);

        gView.AddSubview(resetButton);
        gView.AddSubview(exportGraph);
        gView.AddSubview(menuButton);
        gView.AddSubview(scrollUp);
        gView.AddSubview(scrollDown);

        dateMultiplier = latest.Subtract(earliest).TotalHours / (.8 * graphTable.Bounds.Width);
      } else {
        var noData = new UILabel(new CGRect(0,0,gView.Bounds.Width,60));
        noData.Text = "No Data Available";
        gView.AddSubview(noData);
      }
		}

		/// <summary>
		/// Create the left and right side trackers for the graph for choosing
		/// a subsection of the data
		/// </summary>
		public void CreateGraphTrackers(UIView mainView){
			leftTrackerView = new UIView (new CGRect (.1 * gView.Bounds.Width,.075 * gView.Bounds.Height, 1, trackerHeight));
			leftTrackerView.BackgroundColor = UIColor.Gray;
			leftTrackerView.Alpha = .4f;

			leftTrackerCircle = new UIImageView (new CGRect (.1 * gView.Bounds.Width - 12, .075 * gView.Bounds.Height + trackerHeight, 24,26));
			leftTrackerCircle.Image = UIImage.FromBundle ("ic_tracker_circle");
			leftTrackerCircle.UserInteractionEnabled = true;

			leftDrag = new UIPanGestureRecognizer (() => {
				var xPlot = leftTrackerCircle.Center.X - (.1 * mainView.Bounds.Width);

				if(leftDrag.State == UIGestureRecognizerState.Changed){
					ChosenDates.subLeft = earliest.AddHours(xPlot * dateMultiplier);
					subDates.TextColor = UIColor.Red;
					subDates.Text = ChosenDates.subLeft.ToString() + " - " + ChosenDates.subRight.ToString();
				}
				if(leftDrag.LocationInView(mainView).X > (.1 * mainView.Bounds.Width) && leftDrag.LocationInView(mainView).X < (.75 * mainView.Bounds.Width)){
					if(rightTrackerCircle.Center.X - 12 > leftDrag.LocationInView(mainView).X){
						leftTrackerView.Frame = new CGRect(.1 * mainView.Bounds.Width,.065 * mainView.Bounds.Height, leftDrag.LocationInView(mainView).X - (.1 * mainView.Bounds.Width), trackerHeight);
						leftTrackerCircle.Frame = new CGRect(leftDrag.LocationInView(mainView).X - 12,.065 * mainView.Bounds.Height + trackerHeight,24,26);
					}
				}
				if (leftDrag.State == UIGestureRecognizerState.Ended){
					ChosenDates.subLeft = earliest.AddHours(xPlot * dateMultiplier);
					UIView.Transition(
						withView:subDates,
						duration:.5,
						options: UIViewAnimationOptions.TransitionCrossDissolve,
						animation: () =>{
							subDates.TextColor = UIColor.Black;
						},
						completion: () =>{}
					);
				}
			}); 
			 
			leftTrackerCircle.AddGestureRecognizer (leftDrag);

			rightTrackerView = new UIView (new CGRect (.915 * graphTable.Bounds.Width,.075 * gView.Bounds.Height, 1, trackerHeight));
			rightTrackerView.BackgroundColor = UIColor.Gray;
			rightTrackerView.Alpha = .4f; 

			rightTrackerCircle = new UIImageView (new CGRect (0, .075 * gView.Bounds.Height + trackerHeight,24, 26));
			rightTrackerCircle.Image = UIImage.FromBundle ("ic_tracker_circle");
			rightTrackerCircle.UserInteractionEnabled = true;

			var trackerRect = rightTrackerCircle.Center;
			trackerRect.X = rightTrackerView.Center.X - (.5f * rightTrackerView.Bounds.Width);
			rightTrackerCircle.Center = trackerRect;

			rightDrag = new UIPanGestureRecognizer (() => {
				var xPlot = rightTrackerCircle.Center.X - (.1 * mainView.Bounds.Width);
				if(rightDrag.State == UIGestureRecognizerState.Changed){
					ChosenDates.subRight = earliest.AddHours(xPlot * dateMultiplier);
					subDates.TextColor = UIColor.Red;
					subDates.Text = ChosenDates.subLeft.ToString() + " - " + ChosenDates.subRight.ToString();
				}
        //if(rightDrag.LocationInView(mainView).X > (.11 * mainView.Bounds.Width) && rightDrag.LocationInView(mainView).X < (.765 * mainView.Bounds.Width)){
        if(rightDrag.LocationInView(mainView).X > (.11 * mainView.Bounds.Width) && rightDrag.LocationInView(mainView).X < (.915 * graphTable.Bounds.Width)){
					if(leftTrackerCircle.Center.X + 12 < rightDrag.LocationInView(mainView).X){
						rightTrackerView.Frame = new CGRect(rightDrag.LocationInView(mainView).X,.065 * mainView.Bounds.Height,(.915 * graphTable.Bounds.Width) - rightDrag.LocationInView(mainView).X, trackerHeight);
						rightTrackerCircle.Frame = new CGRect(rightDrag.LocationInView(mainView).X - 12,.065 * mainView.Bounds.Height + trackerHeight,24,26);
					}
				}
				if (rightDrag.State == UIGestureRecognizerState.Ended){
					ChosenDates.subRight = earliest.AddHours(xPlot * dateMultiplier);

					UIView.Transition(
						withView:subDates,
						duration:.5,
						options: UIViewAnimationOptions.TransitionCrossDissolve,
						animation: () =>{
							subDates.TextColor = UIColor.Black;
						},
						completion: () =>{}
					);
				}
			});
				
			rightTrackerCircle.AddGestureRecognizer (rightDrag);

			subDates = new UILabel (new CGRect (.1 * gView.Bounds.Width,0,.8 * gView.Bounds.Width,.1 * gView.Bounds.Height));
			subDates.AdjustsFontSizeToFitWidth = true;
			subDates.TextColor = UIColor.Black;
			subDates.TextAlignment = UITextAlignment.Center;
			subDates.Text = ChosenDates.subLeft.ToString () + " - " + ChosenDates.subRight.ToString();
			subDates.Font = UIFont.FromName("Helvetica-Bold", 22f);
			subDates.MinimumFontSize = 11.5f;
		}
		/// <summary>
		/// Creates the buttons to navigate and manipulate the graph and its included data
		/// </summary>
		public void createButtons(){
			resetButton = new UIButton (new CGRect (.05 * gView.Bounds.Width, .92 * gView.Bounds.Height, .25 * gView.Bounds.Width, .08 * gView.Bounds.Height));
			resetButton.BackgroundColor = UIColor.Red;
			resetButton.SetTitle ("Reset", UIControlState.Normal);
			resetButton.Layer.CornerRadius = 8;

			resetButton.TouchUpInside += (sender, e) => {
				ChosenDates.subLeft = earliest;
				ChosenDates.subRight = latest;
				UIView.Animate(.15,0, UIViewAnimationOptions.CurveLinear,
					() =>{ 
						leftTrackerView.Frame = new CGRect(.1 * gView.Bounds.Width,.075 * gView.Bounds.Height, 1, trackerHeight);

						var trackerRect = leftTrackerCircle.Center;
						trackerRect.X = leftTrackerView.Center.X + (.5f * leftTrackerView.Bounds.Width);
						leftTrackerCircle.Center = trackerRect;
					},() => {});

				UIView.Animate(.15,0, UIViewAnimationOptions.CurveLinear,
					() =>{ 
						rightTrackerView.Frame = new CGRect(.915 * graphTable.Bounds.Width,.075 * gView.Bounds.Height, 1, trackerHeight);

						var trackerRect = rightTrackerCircle.Center;
						trackerRect.X = rightTrackerView.Center.X - (.5f * rightTrackerView.Bounds.Width);
						rightTrackerCircle.Center = trackerRect;
					},() => {});				
				resetButton.BackgroundColor = UIColor.Red;
				subDates.Text = ChosenDates.subLeft.ToString() + " - " + ChosenDates.subRight.ToString();
			};
			resetButton.TouchDown += (sender, e) => {resetButton.BackgroundColor = UIColor.Blue;};

			resetButton.TouchUpOutside += (sender, e) => {
				resetButton.BackgroundColor = UIColor.Red;
				graphTable.Source = new graphingTableSource (selectedData,gView,earliest, latest, leftTrackerView, rightTrackerView, .8 * gView.Bounds.Height, leftTrackerCircle, rightTrackerCircle, subDates);
				graphTable.ReloadData ();
			};

			exportGraph = new UIButton (new CGRect (.7 * gView.Bounds.Width, .92 * gView.Bounds.Height,.25 * gView.Bounds.Width,.08 * gView.Bounds.Height));
			exportGraph.BackgroundColor = UIColor.Green;
			exportGraph.SetTitle ("Export", UIControlState.Normal);
			exportGraph.Layer.CornerRadius = 8;

			exportGraph.TouchUpInside += (sender, e) => {
				Console.WriteLine("using graph data starting at " + ChosenDates.subLeft + " and ending at " + ChosenDates.subRight);
				exportGraph.BackgroundColor = UIColor.Green;
				foreach(var name in ChosenDates.includeList){
					Console.WriteLine("included in reporting: " + name);
				}
			};
			exportGraph.TouchDown += (sender, e) => {exportGraph.BackgroundColor = UIColor.Blue;};
			exportGraph.TouchUpOutside += (sender, e) => {exportGraph.BackgroundColor = UIColor.Green;};

			menuButton = new UIButton (new CGRect (.9 * gView.Bounds.Width,5,.075 * gView.Bounds.Width,.05 * gView.Bounds.Height));
			menuButton.SetBackgroundImage (UIImage.FromBundle ("ic_settings"), UIControlState.Normal);

			scrollUp = new UIButton (new CGRect (0,.075 *gView.Bounds.Height,.1 * gView.Bounds.Width, .1 * gView.Bounds.Width));
			scrollUp.SetImage (UIImage.FromBundle ("ic_scrollup"), UIControlState.Normal);
			scrollUp.Enabled = false;
			scrollUp.TouchUpInside += (sender, e) => {
				graphTable.ScrollToRow(NSIndexPath.FromRowSection(topCell,0), UITableViewScrollPosition.Top, true);
				if(topCell <= 0){ 
					topCell = 0;
					scrollUp.Enabled = false;
				} else {
					topCell = topCell - 1;
					bottomCell = bottomCell -1;
					scrollDown.Enabled = true;
				}
			};

			scrollDown = new UIButton (new CGRect (0,trackerHeight,.1 * gView.Bounds.Width, .1 * gView.Bounds.Width));
			scrollDown.SetImage (UIImage.FromBundle ("ic_scrolldown"), UIControlState.Normal);
			scrollDown.TouchUpInside += (sender, e) => {
				graphTable.ScrollToRow(NSIndexPath.FromRowSection(bottomCell,0), UITableViewScrollPosition.Bottom, true);
				if(bottomCell >= deviceCount - 1){
					bottomCell = deviceCount - 1;
					scrollDown.Enabled = false;
				} else {
					topCell = topCell + 1;
					bottomCell = bottomCell + 1;
					scrollUp.Enabled = true;
				}
			};
			if (deviceCount <= 4) {
				scrollUp.Hidden = true;
				scrollDown.Hidden = true;
			}
		}
		/// <summary>
		/// Goes through the session(s) and finds the earliest and latest logging dates
		/// </summary>
		public void getEarliestAndLatest(){
			var timeList = new List<string> ();
			foreach (var device in selectedData) {
				foreach (var time in device.times) {
					if (time < earliest) {
						earliest = time;
					}
					if (time > latest) {
						latest = time;
					}
					if (timeList.Contains (time.ToString())) {						
					} else {
						timeList.Add (time.ToString());
					}
				}
				ChosenDates.includeList.Add (device.name);
			}

			ChosenDates.subLeft = earliest;
			ChosenDates.subRight = latest;
			Console.WriteLine ("Final earliest " + earliest + " and latest " + latest);
			Console.WriteLine ("Final earliest year " + earliest.Year + " and latest year " + latest.Year);
			Console.WriteLine ("Final earliest month " + earliest.Month + " and latest month " + latest.Month);
			Console.WriteLine ("Final earliest day " + earliest.Day + " and latest day " + latest.Day);
			Console.WriteLine ("Final earliest hour " + earliest.Hour + " and latest hour " + latest.Hour);
			Console.WriteLine ("Final earliest minute " + earliest.Minute + " and latest minute " + latest.Minute);

//			foreach (var time in timeList) {
//				ChosenDates.allTimes.Add(time.ToString());
//			}
//			ChosenDates.allTimes.Sort ((x, y) => String.Compare (x, y));
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