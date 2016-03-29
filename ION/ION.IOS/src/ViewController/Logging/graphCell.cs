using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.iOS;

namespace ION.IOS.ViewController.Logging
{
	public partial class graphCell : UITableViewCell
	{
		public PlotView plotView;
		public deviceReadings cellData;
		public LinearAxis LAX;
		public LinearAxis RAX;
		public DateTimeAxis BAX;
		public UILabel deviceName;
		public UILabel includeLabel;
		public UIButton includeButton;
		public UITableView graphTable;

		public graphCell(IntPtr handle) {
		
		}

		public void setupGraph(deviceReadings data, double cellWidth, double cellHeight, DateTime earliest, DateTime latest, 
							   UIView leftTrackerView, UIView rightTrackerView, double trackerHeight, UIView parentView, 
							   UIImageView leftTrackerCircle, UIImageView rightTrackerCircle, UITableView tableView, UILabel chosenDates)
		{
			cellData = data;
			graphTable = tableView;
			this.BackgroundColor = UIColor.Clear;
			this.Layer.BorderWidth = 1f;
			 
			//plotView = new PlotView(new CGRect(0,0, .8 * cellWidth, .8 *cellHeight)){
			plotView = new PlotView(new CGRect(0,0, .8 * cellWidth, cellHeight)){
				Model = CreatePlotModel(earliest, latest, leftTrackerView, rightTrackerView, trackerHeight, parentView, leftTrackerCircle, rightTrackerCircle, chosenDates),		
			};
			plotView.BackgroundColor = UIColor.Clear;

			//deviceName = new UILabel (new CGRect (.2 * cellWidth,.8 * cellHeight,.2 * cellWidth,.2 * cellHeight));
			deviceName = new UILabel (new CGRect (.8 * cellWidth,.8 * cellHeight,.2 * cellWidth,.2 * cellHeight));
			deviceName.Text = cellData.name;
			deviceName.AdjustsFontSizeToFitWidth = true;
			deviceName.TextAlignment = UITextAlignment.Center;

			//includeLabel = new UILabel (new CGRect (.825 * cellWidth,.2 * cellHeight,.15 * cellWidth,.25 * cellHeight));
			includeLabel = new UILabel (new CGRect (.825 * cellWidth,.1 * cellHeight,.15 * cellWidth,.25 * cellHeight));
			includeLabel.TextAlignment = UITextAlignment.Center;
			includeLabel.Text = "Include";
			includeLabel.AdjustsFontSizeToFitWidth = true;

			//includeButton = new UIButton (new CGRect (.85 * cellWidth,.45 * cellHeight,.1 * cellWidth, .1 * cellWidth));
			includeButton = new UIButton (new CGRect (.85 * cellWidth,.35 * cellHeight,.1 * cellWidth, .1 * cellWidth));
			if (ChosenDates.includeList.Contains(cellData.name)) {
        includeButton.SetBackgroundImage (UIImage.FromBundle ("ic_checkbox"), UIControlState.Normal);
			} else {
        includeButton.SetBackgroundImage (UIImage.FromBundle ("ic_unchecked"), UIControlState.Normal);
			}
			includeButton.Layer.CornerRadius = 8;
			includeButton.SetTitleColor (UIColor.White, UIControlState.Normal);

			includeButton.TouchUpInside += (sender, e) => {
				if (ChosenDates.includeList.Contains (cellData.name)) {
					ChosenDates.includeList.Remove (cellData.name);
					includeButton.SetBackgroundImage(UIImage.FromBundle ("ic_unchecked"), UIControlState.Normal);
				} else {
					ChosenDates.includeList.Add (cellData.name);
					includeButton.SetBackgroundImage(UIImage.FromBundle ("ic_checkbox"), UIControlState.Normal);
				}
				Console.WriteLine("Clicked " + data.name);
			};

			this.AddSubview (plotView);
			this.AddSubview (deviceName);
			this.BringSubviewToFront (deviceName);
			this.AddSubview (includeLabel);
			this.AddSubview (includeButton);
		}

		/// <summary>
		/// Setup the axes and setup the plot points for each
		/// series(recording session) for the device/sensor
		/// </summary>
		/// <returns>The plot model.</returns>	
		public PlotModel CreatePlotModel( DateTime earliest, DateTime latest, UIView leftTrackerView, UIView rightTrackerView, 
										  double trackerHeight, UIView parentView,UIImageView leftTrackerCircle, UIImageView rightTrackerCircle,
										  UILabel chosenDates) {
			var lowValue = 9999.9;
			var highValue = -9999.9;

			foreach (var reading in cellData.readings) {
				if (reading < lowValue) {
					lowValue = reading;
				}
				if (reading > highValue) {
					highValue = reading;
				} 
			}

			var plotModel = new PlotModel();

			plotModel.Background = OxyColors.Transparent;
			plotModel.PlotAreaBackground = OxyColors.White;
			plotModel.DefaultFontSize = 0;
			plotModel.PlotAreaBorderThickness = new OxyThickness(0,0,0,0);
			plotModel.PlotMargins = new OxyThickness(-5,-5,-5,4);
			//plotModel.Background = OxyColors.SteelBlue;

			/// The bottom axis of the graph which will be a datetime one 
			BAX = new DateTimeAxis {
				Position = AxisPosition.Bottom,
				StringFormat = "HH:mm",
				Maximum = DateTimeAxis.ToDouble (latest),
				AbsoluteMaximum = DateTimeAxis.ToDouble (latest),
				Minimum = DateTimeAxis.ToDouble (earliest),
				AbsoluteMinimum = DateTimeAxis.ToDouble (earliest),
				IntervalLength = latest.Subtract(earliest).TotalMinutes,
				MinorIntervalType = DateTimeIntervalType.Seconds,
				IntervalType = DateTimeIntervalType.Minutes,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 0,
				MaximumPadding = 0,
				AxislineThickness = 0,
			};
			/// left axis of the graph that adds a pad on the top and bottom to the lowest and highest value 
			/// this will be used for pressure measurements
			LAX = new LinearAxis {
				Position = AxisPosition.Left,
				Maximum = highValue + 4,
				AbsoluteMaximum = highValue + 4,
				Minimum = lowValue - 4,
				AbsoluteMinimum = lowValue - 4,
				Key = "left",
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 0,
				MaximumPadding = 0,
				AxislineThickness = 0,
			};
			/// right axis of the graph that adds a pad on the top and bottom to the lowest and highest value 
			/// this will be used for temperature measurements
			RAX = new LinearAxis {
				Position = AxisPosition.Right,
				Maximum = highValue + 4,
				AbsoluteMaximum = highValue + 4,
				Minimum = lowValue - 4,
				AbsoluteMinimum = lowValue - 4,
				Key = "right",
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 0,
				MaximumPadding = 0,
				AxislineThickness= 0,
			};

			plotModel.Axes.Add (BAX);
			plotModel.Axes.Add (LAX);
			plotModel.Axes.Add (RAX);
		
			var color = OxyColors.Blue;

//			if(cellData.type.Equals("temperature")){
//				color = OxyColors.Red;
//			} else if (cellData.type.Equals("vacuum")){
//				color = OxyColors.Maroon;
//			}

			var series = new LineSeries {
				MarkerType = MarkerType.Circle,
				MarkerSize = 1,
				MarkerStroke = color,
				MarkerFill = color,
				LineStyle = LineStyle.Solid,
				Color = color,
				Title = cellData.name,
			};

			for (int i = 0; i < cellData.readings.Count; i++) {
				series.Points.Add(DateTimeAxis.CreateDataPoint(cellData.times[i], cellData.readings[i]));
				//Console.WriteLine ("Device " + cellData.name + " " + cellData.times [i] + " " + cellData.readings [i]);
			}
			//Console.WriteLine (Environment.NewLine);
			plotModel.Series.Add (series);

			plotModel.IsLegendVisible = false;

			/// when a user drags a spot on the graph the corresponding
			/// left or right tracker will move to reach it based on changing delta
//			plotModel.TouchDelta += (sender, e) => {
//				if(e.Position.X <= plotModel.PlotArea.Center.X){
//					if(e.Position.X < plotModel.PlotArea.Left){
//						UIView.Animate(.15,0, UIViewAnimationOptions.CurveLinear,
//							() =>{  
//								leftTrackerView.Frame = new CGRect(.1 * parentView.Bounds.Width,.075 * parentView.Bounds.Height, 1, trackerHeight);
//
//								var trackerRect = leftTrackerCircle.Center;
//								trackerRect.X = leftTrackerView.Center.X + (.5f * leftTrackerView.Bounds.Width);
//								leftTrackerCircle.Center = trackerRect;
//							},() => {});
//						ChosenDates.subLeft = earliest;
//					} else {
//						UIView.Animate(.15,0, UIViewAnimationOptions.CurveLinear,
//							() =>{ 
//								leftTrackerView.Frame = new CGRect(.1 * parentView.Bounds.Width,.075 * parentView.Bounds.Height, e.Position.X, trackerHeight);
//
//								var trackerRect = leftTrackerCircle.Center;
//								trackerRect.X = leftTrackerView.Center.X + (.5f * leftTrackerView.Bounds.Width);
//								leftTrackerCircle.Center = trackerRect;
//							},() => {}); 
//						
//						var dateDouble = series.InverseTransform(e.Position).X;
//						ChosenDates.subLeft = DateTime.FromOADate(dateDouble).AddDays(1);
//					}
//				} else if (e.Position.X >= plotModel.PlotArea.Center.X) {
//					if(e.Position.X > plotModel.PlotArea.Right){
//						UIView.Animate(.15,0, UIViewAnimationOptions.CurveLinear,
//							() =>{ 
//								rightTrackerView.Frame = new CGRect(.915 * graphTable.Bounds.Width,.075 * parentView.Bounds.Height, 1, trackerHeight);
//
//								var trackerRect = rightTrackerCircle.Center;
//								trackerRect.X = rightTrackerView.Center.X - rightTrackerView.Bounds.Width;
//								rightTrackerCircle.Center = trackerRect;
//							},() => {});							
//						ChosenDates.subRight = latest;
//					} else {
//						UIView.Animate(.15,0, UIViewAnimationOptions.CurveLinear,
//							() =>{ 
//								rightTrackerView.Frame = new CGRect(e.Position.X + .1 * parentView.Bounds.Width, .075 * parentView.Bounds.Height, plotModel.PlotArea.Right - e.Position.X, trackerHeight);
//	
//								var trackerRect = rightTrackerCircle.Center;
//								trackerRect.X = rightTrackerView.Center.X - (.5f * rightTrackerView.Bounds.Width);
//								rightTrackerCircle.Center = trackerRect;
//							},() => {});
//						
//						var dateDouble = series.InverseTransform(e.Position).X;
//						ChosenDates.subRight = DateTime.FromOADate(dateDouble).AddDays(1);
//					}
//				}
//				var middleDate = series.InverseTransform(plotModel.PlotArea.Center).X;
//				Console.WriteLine("The middle value of the graph is " + DateTime.FromOADate(middleDate).AddDays(1));
//				chosenDates.Text = ChosenDates.subLeft.ToString() + " - " + ChosenDates.subRight.ToString();
//			};
//
//			/// when a user just taps a spot on the graph the corresponding
//			/// left or right tracker will move to reach it
//			plotModel.TouchCompleted += (sender, e) => {
//				if(e.Position.X <= plotModel.PlotArea.Center.X){
//					if(e.Position.X < plotModel.PlotArea.Left){
//						UIView.Animate(.15,0, UIViewAnimationOptions.CurveLinear,
//							() =>{ 
//								leftTrackerView.Frame = new CGRect(.1 * parentView.Bounds.Width,.075 * parentView.Bounds.Height, 1, trackerHeight);
//
//								var trackerRect = leftTrackerCircle.Center;
//								trackerRect.X = leftTrackerView.Center.X + (.5f * leftTrackerView.Bounds.Width);
//								leftTrackerCircle.Center = trackerRect;
//							},() => {});							
//						ChosenDates.subLeft = earliest;
//					} else {
//						UIView.Animate(.15,0, UIViewAnimationOptions.CurveLinear,
//							() =>{ 
//								leftTrackerView.Frame = new CGRect(.1 * parentView.Bounds.Width,.075 * parentView.Bounds.Height, e.Position.X, trackerHeight);
//
//								var trackerRect = leftTrackerCircle.Center;
//								trackerRect.X = leftTrackerView.Center.X + (.5f * leftTrackerView.Bounds.Width);
//								leftTrackerCircle.Center = trackerRect;
//							},() => {}); 
//						
//						var dateDouble = series.InverseTransform(e.Position).X;
//						ChosenDates.subLeft = DateTime.FromOADate(dateDouble).AddDays(1);
//					}
//				} else if (e.Position.X > plotModel.PlotArea.Center.X) {
//					if(e.Position.X > plotModel.PlotArea.Right){
//						UIView.Animate(.15,0, UIViewAnimationOptions.CurveLinear,
//							() =>{ 
//								rightTrackerView.Frame = new CGRect(.915 * graphTable.Bounds.Width,.075 * parentView.Bounds.Height, 1, trackerHeight);
//
//								var trackerRect = rightTrackerCircle.Center;
//								trackerRect.X = rightTrackerView.Center.X - (.5f * rightTrackerView.Bounds.Width);
//								rightTrackerCircle.Center = trackerRect;
//							},() => {});							
//						ChosenDates.subRight = latest;
//					} else {
//						UIView.Animate(.15,0, UIViewAnimationOptions.CurveLinear,
//							() =>{ 
//								rightTrackerView.Frame = new CGRect(e.Position.X + .1 * parentView.Bounds.Width, .075 * parentView.Bounds.Height, plotModel.PlotArea.Right - e.Position.X, trackerHeight);
//
//								var trackerRect = rightTrackerCircle.Center;
//								trackerRect.X = rightTrackerView.Center.X - (.5f * rightTrackerView.Bounds.Width);
//								rightTrackerCircle.Center = trackerRect;
//							},() => {});
//						
//						var dateDouble = series.InverseTransform(e.Position).X;
//						ChosenDates.subRight = DateTime.FromOADate(dateDouble).AddDays(1);
//					}
//				}
//				Console.WriteLine("touch completed " + ChosenDates.subLeft + " " + ChosenDates.subRight);
//				Console.WriteLine("plot area with axis area " + plotModel.PlotAndAxisArea + 
//								  " plot area " + plotModel.PlotArea + " plot area border thickness" 
//								  + plotModel.PlotAreaBorderThickness);
//				Console.WriteLine("plotmargins " + plotModel.PlotMargins + " actual plot margins " + plotModel.ActualPlotMargins);
//				chosenDates.Text = ChosenDates.subLeft.ToString() + " - " + ChosenDates.subRight.ToString();
//			};

			return plotModel;
		}
	}
}

