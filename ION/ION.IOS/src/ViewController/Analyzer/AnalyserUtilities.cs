/// <summary>
/// This contains the methods for setting up the analyzer view and subviews
/// </summary>


using System;
using System.Collections.Generic;
using System.Globalization;
using UIKit;
using CoreGraphics;
using Foundation;
using CoreAnimation;

using Appion.Commons.Measure;
using Appion.Commons.Util;

using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Sensors;
using ION.Core.Sensors.Properties;
using ION.Core.Fluids;
using ION.Core.App;

using ION.IOS.ViewController.Alarms;
using ION.IOS.Util;
using System.Collections.ObjectModel;
using ION.IOS.App;
using System.Linq;
using ION.IOS.Devices;

namespace ION.IOS.ViewController.Analyzer
{
	class AnalyserUtilities 
	{
    public static IONPrimaryScreenController root { get; set; }
    public static IION ion = AppState.context;
		/// <summary>
		/// Calculates the locations and snap points for sensor subviews
		/// </summary>
		/// <param name="analyzerSensors">singleSensor class object holding 8 sensors</param>
		/// <param name="View">View.</param>
		public static void CreateSnapArea(sensorGroup analyzerSensors, UIView View){
			////CREATE STATIC SENSOR LOCATIONS 
			/// LEFT SIDE 
			/// 4  3
			/// 2  1
      //analyzerSensors.snapRect1 = new CGRect (.024 * View.Bounds.Width, .012 * View.Bounds.Height, .203 * View.Bounds.Width, .114 * View.Bounds.Height);
      analyzerSensors.snapRect1 = new CGRect (.25 * View.Bounds.Width, .304 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);
      analyzerSensors.snapRect2 = new CGRect(.024 * View.Bounds.Width, .304 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);
      analyzerSensors.snapRect3 = new CGRect(.25 * View.Bounds.Width, .012 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);
      //analyzerSensors.snapRect4 = new CGRect(.25 * View.Bounds.Width, .304 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);
      analyzerSensors.snapRect4 = new CGRect(.024 * View.Bounds.Width, .012 * View.Bounds.Height, .203 * View.Bounds.Width, .114 * View.Bounds.Height);
			/// RIGHT SIDE
			/// 7  8
			/// 5  6
      //analyzerSensors.snapRect5 = new CGRect (.546 * View.Bounds.Width, .012 * View.Bounds.Height, .203 * View.Bounds.Width, .114 * View.Bounds.Height);
      analyzerSensors.snapRect5 = new CGRect (.546 * View.Bounds.Width, .304 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);
      //analyzerSensors.snapRect6 = new CGRect(.546 * View.Bounds.Width, .304 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);
      analyzerSensors.snapRect6 = new CGRect(.769 * View.Bounds.Width, .304 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);
      //analyzerSensors.snapRect7 = new CGRect(.769 * View.Bounds.Width, .012 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);
      analyzerSensors.snapRect7 = new CGRect(.546 * View.Bounds.Width, .012 * View.Bounds.Height, .203 * View.Bounds.Width, .114 * View.Bounds.Height);
      //analyzerSensors.snapRect8 = new CGRect(.769 * View.Bounds.Width, .304 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);
      analyzerSensors.snapRect8 = new CGRect(.769 * View.Bounds.Width, .012 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);

			////CREATE CONSTANT POINT OF ORIGIN FOR EACH AREA
			/// LEFT SIDE
			analyzerSensors.snapPoint1 = new CGPoint (analyzerSensors.snapRect1.GetMidX (), analyzerSensors.snapRect1.GetMidY ());
			analyzerSensors.snapPoint2 = new CGPoint (analyzerSensors.snapRect2.GetMidX (), analyzerSensors.snapRect2.GetMidY ());
			analyzerSensors.snapPoint3 = new CGPoint (analyzerSensors.snapRect3.GetMidX (), analyzerSensors.snapRect3.GetMidY ());
			analyzerSensors.snapPoint4 = new CGPoint (analyzerSensors.snapRect4.GetMidX (), analyzerSensors.snapRect4.GetMidY ());
			///RIGHT SIDE
			analyzerSensors.snapPoint5 = new CGPoint (analyzerSensors.snapRect5.GetMidX (), analyzerSensors.snapRect5.GetMidY ());
			analyzerSensors.snapPoint6 = new CGPoint (analyzerSensors.snapRect6.GetMidX (), analyzerSensors.snapRect6.GetMidY ());
			analyzerSensors.snapPoint7 = new CGPoint (analyzerSensors.snapRect7.GetMidX (), analyzerSensors.snapRect7.GetMidY ());
			analyzerSensors.snapPoint8 = new CGPoint (analyzerSensors.snapRect8.GetMidX (), analyzerSensors.snapRect8.GetMidY ());  


			///STORE POINT LOCATIONS AND INITIALIZE SENSOR ORDER
			analyzerSensors.locationList.Add( analyzerSensors.snapPoint1);
			analyzerSensors.locationList.Add( analyzerSensors.snapPoint2);
			analyzerSensors.locationList.Add( analyzerSensors.snapPoint3);
			analyzerSensors.locationList.Add( analyzerSensors.snapPoint4);
			analyzerSensors.locationList.Add( analyzerSensors.snapPoint5);
			analyzerSensors.locationList.Add( analyzerSensors.snapPoint6);
			analyzerSensors.locationList.Add( analyzerSensors.snapPoint7);
			analyzerSensors.locationList.Add( analyzerSensors.snapPoint8);
		}
    /// <summary>
    /// SETS UP THE UI FOR THE MANUAL ENTRY VIEW
    /// </summary>
    /// <param name="mentryView">MANUAL VIEW OBJECT</param>
    /// <param name="View">THE MAIN VIEW THAT WILL HOLD THE MANUAL VIEW</param>
    public static void CreateManualViews(ManualView mentryView, UIView View){
      mentryView.mView.BackgroundColor = UIColor.White;
      mentryView.mView.Hidden = true;
      mentryView.mView.Layer.CornerRadius = 5;
      mentryView.mView.Layer.BorderWidth = 1f;
      mentryView.mView.Layer.BorderColor = UIColor.LightGray.CGColor; 
      mentryView.popupText.Text = Util.Strings.Analyzer.CREATEMANUAL;
      mentryView.popupText.TextAlignment = UITextAlignment.Center;
      mentryView.popupText.AdjustsFontSizeToFitWidth = true;
      mentryView.popupText.BackgroundColor = UIColor.FromRGB(9,221,255);
      mentryView.popupText.Layer.CornerRadius = 5;
      mentryView.popupText.ClipsToBounds = false;
      mentryView.popupText.Font = UIFont.FromName("Helvetica-Bold", 27f);
      mentryView.mdeviceType.Text = Util.Strings.Device.TYPE + ":";
      mentryView.mdeviceType.AdjustsFontSizeToFitWidth = true;
      mentryView.dtypeButton.SetTitle(Util.Strings.Sensor.Type.PRESSURE, UIControlState.Normal);
      mentryView.dtypeButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      mentryView.dtypeButton.AccessibilityIdentifier = "Pressure";
      mentryView.dtypeButton.Font = UIFont.FromName("Helvetica-Bold", 20f);
      mentryView.mtextValue.Layer.BorderColor = UIColor.LightGray.CGColor;
      mentryView.mtextValue.Layer.BorderWidth = 1f;
      mentryView.mtextValue.Layer.CornerRadius = 5;
      mentryView.mbuttonText.UserInteractionEnabled = false;
      mentryView.mbuttonText.Text = "psig";
      mentryView.mbuttonText.Font = UIFont.FromName("Helvetica-Bold", 20f);
      mentryView.mbuttonText.TextColor = UIColor.Black;
      mentryView.mbuttonText.TextAlignment = UITextAlignment.Center;
      mentryView.mcloseButton.SetTitleColor(UIColor.Gray, UIControlState.Normal);
      mentryView.mcloseButton.SetTitle(Util.Strings.CLOSE, UIControlState.Normal);
      mentryView.mcloseButton.ClipsToBounds = true;
      mentryView.mdoneButton.SetTitleColor(UIColor.Gray, UIControlState.Normal);
      mentryView.mdoneButton.SetTitle(Util.Strings.OKDONE, UIControlState.Normal);
      mentryView.mdoneButton.ClipsToBounds = true;

      View.AddSubview(mentryView.mView);
    }
    /// <summary>
    /// SETS UP THE UI FOR EACH SENSOR'S ACTION VIEW
    /// </summary>
    /// <param name="sactionView">ACTION VIEW OBJECT BELONGING TO A SENSOR</param>
    public static void CreateActionViews(ActionView sactionView){
      sactionView.aView.BackgroundColor = UIColor.White;
      sactionView.aView.Hidden = true;
      sactionView.aView.Layer.BorderWidth = 2f;
      sactionView.aView.Layer.BorderColor = UIColor.Black.CGColor;
      sactionView.pactionButton.SetTitle(Util.Strings.ACTIONS, UIControlState.Normal);
      sactionView.pactionButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      sactionView.pcloseButton.SetTitle(Util.Strings.CLOSE, UIControlState.Normal);
      sactionView.pcloseButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      sactionView.pvalueType.TextAlignment = UITextAlignment.Right;
      sactionView.pLowHigh.Layer.CornerRadius = 5f;
      sactionView.pLowHigh.Layer.BorderWidth = 1f;
      sactionView.pLowHigh.ClipsToBounds = true;
      sactionView.pgaugeValue.AdjustsFontSizeToFitWidth = true;
      sactionView.pgaugeValue.Font = UIFont.FromName("Helvetica-Bold", 54f);
      sactionView.pgaugeValue.TextAlignment = UITextAlignment.Right;
      sactionView.pdisplayLink.Text = Util.Strings.Analyzer.DISPLAYLINK;
      sactionView.pdisplayLink.TextAlignment = UITextAlignment.Right;
      sactionView.pdisplayLink.Font = UIFont.FromName("Helvetica", 12f);
      sactionView.pconnectionStatus.AdjustsFontSizeToFitWidth = true;
      sactionView.connectionColor.BackgroundColor = UIColor.Clear;
      sactionView.connectionColor.Layer.CornerRadius = 5;
      sactionView.connectionColor.Layer.BorderColor = UIColor.Black.CGColor;
      sactionView.connectionColor.Layer.BorderWidth = 1f;
      sactionView.pconnection.Layer.BorderColor = UIColor.Black.CGColor;
      sactionView.pconnection.Layer.BorderWidth = 1f;
      sactionView.pconnection.Layer.CornerRadius = 5;

      sactionView.aView.BringSubviewToFront(sactionView.pconnection);
    }
		/// <summary>
		/// Sets up each single sensor UI and adds them as subviews to the main view
		/// </summary>
		/// <param name="analyzerSensors">singleSensor class object holding 8 sensors</param>
		/// <param name="mainView">Main view.</param>
    public static void ApplySnapArea(sensor Sensor,string identifier, CGRect sensorRect, sensorGroup analyzerSensors, LowHighArea lowHighSensors, UIView mainView){
      Sensor.snapArea = new UIView (sensorRect){
        AccessibilityIdentifier = identifier,
      };

      Sensor.availableView = new UIView(new CGRect(0,0,Sensor.snapArea.Bounds.Width,Sensor.snapArea.Bounds.Height)){
        BackgroundColor = UIColor.FromRGB(204,153,0),
        Alpha = .4f,
        UserInteractionEnabled = true,
        AccessibilityIdentifier = identifier,
        Hidden = false,
      };
      Sensor.snapArea.ClipsToBounds = true;

      Sensor.sactionView = new ActionView(mainView);
      Sensor.sactionView.aView.Layer.CornerRadius = 5;
      CreateActionViews(Sensor.sactionView);
      Sensor.addIcon = new UIImageView(new CGRect(.107 * Sensor.snapArea.Bounds.Width,.107 * Sensor.snapArea.Bounds.Height,.786 * Sensor.snapArea.Bounds.Width,.786 * Sensor.snapArea.Bounds.Height));
      Sensor.addIcon.Image = UIImage.FromBundle("ic_device_add");
      Sensor.addIcon.BackgroundColor = UIColor.Clear;
      Sensor.snapArea.Layer.CornerRadius = 5;
      Sensor.availableView.Layer.CornerRadius = 5;
      Sensor.snapArea.Layer.BorderColor = UIColor.Black.CGColor;
      Sensor.snapArea.Layer.BorderWidth = 2f;
      Sensor.snapArea.AddSubview(Sensor.availableView);
      Sensor.snapArea.AddSubview(Sensor.addIcon);
      Sensor.snapArea.BringSubviewToFront(Sensor.addIcon);

      //applyLowHighGestures(Sensor.lowArea, Sensor.highArea, lowHighSensors, Sensor);
      CreateSubviewLayout (Sensor.snapArea, Sensor.topLabel, Sensor.middleLabel, Sensor.bottomLabel);

      mainView.AddSubview (Sensor.snapArea);
      mainView.AddSubview (Sensor.sactionView.aView);
      analyzerSensors.viewList.Add (Sensor);
		}
		/// <summary>
		/// Sets up the Low and High Side sensor ui elements
		/// </summary>
		/// <param name="mainView">Main view.</param>
		/// <param name="snapArea">Snap area.</param>
		/// <param name="topLabel">Top label.</param>
		/// <param name="middleLabel">Middle label.</param>
		/// <param name="bottomLabel">Bottom label.</param>
		/// <param name="subviewLabel">Subview label.</param>
		/// <param name="middleText">Middle text.</param>
		public static void AddHighLowArea(LowHighArea lowHighSensors, UIView mainView){
			lowHighSensors.lowArea.snapArea.BackgroundColor = UIColor.White;
			lowHighSensors.lowArea.snapArea.Alpha = 1f;
			lowHighSensors.lowArea.snapArea.UserInteractionEnabled = true;
			lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";				
			lowHighSensors.lowArea.snapArea.Layer.CornerRadius = 5;

			lowHighSensors.lowArea.LabelTop.AdjustsFontSizeToFitWidth = true;
			lowHighSensors.lowArea.LabelTop.Text = "";
			lowHighSensors.lowArea.LabelTop.Layer.CornerRadius = 5;
			lowHighSensors.lowArea.LabelTop.ClipsToBounds = true;

			lowHighSensors.lowArea.LabelMiddle.AdjustsFontSizeToFitWidth = true;
      lowHighSensors.lowArea.LabelMiddle.Text = Util.Strings.Analyzer.LOWUNDEFINED;
			lowHighSensors.lowArea.LabelMiddle.TextAlignment = UITextAlignment.Right;

			lowHighSensors.lowArea.LabelBottom.AdjustsFontSizeToFitWidth = true;
			lowHighSensors.lowArea.LabelBottom.Text = "";
			lowHighSensors.lowArea.LabelBottom.TextAlignment = UITextAlignment.Right;
			lowHighSensors.lowArea.LabelBottom.BackgroundColor = UIColor.Clear;

			lowHighSensors.lowArea.LabelSubview.AdjustsFontSizeToFitWidth = true;
			lowHighSensors.lowArea.LabelSubview.Text = "";
			lowHighSensors.lowArea.LabelSubview.TextColor = UIColor.White;
			lowHighSensors.lowArea.LabelSubview.ClipsToBounds = true;

			lowHighSensors.lowArea.subviewTable.BackgroundColor = UIColor.Clear;
			lowHighSensors.lowArea.subviewTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			lowHighSensors.lowArea.subviewTable.Hidden = true;
			lowHighSensors.lowArea.subviewTable.Source = null;

      lowHighSensors.lowArea.subviewHide.BackgroundColor = UIColor.Blue;
      lowHighSensors.lowArea.subviewHide.SetImage(null, UIControlState.Normal);
      lowHighSensors.lowArea.subviewHide.Hidden = true;

      lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
      lowHighSensors.lowArea.Connection.BackgroundColor = UIColor.Clear;
      lowHighSensors.lowArea.Connection.Hidden = true;
      lowHighSensors.lowArea.connectionColor.Layer.CornerRadius = 5;
      lowHighSensors.lowArea.connectionColor.Layer.BorderColor = UIColor.Black.CGColor;
      lowHighSensors.lowArea.connectionColor.Layer.BorderWidth = 1f;
      lowHighSensors.lowArea.connectionColor.Hidden = true;
      lowHighSensors.lowArea.subviewDivider.BackgroundColor = UIColor.Black;
      lowHighSensors.lowArea.headingDivider.Hidden = true;
      lowHighSensors.lowArea.headingDivider.BackgroundColor = UIColor.Black;

			lowHighSensors.lowArea.snapArea.AddSubview (lowHighSensors.lowArea.LabelTop);
			lowHighSensors.lowArea.snapArea.AddSubview (lowHighSensors.lowArea.LabelMiddle);
			lowHighSensors.lowArea.snapArea.AddSubview (lowHighSensors.lowArea.LabelBottom);
			lowHighSensors.lowArea.snapArea.AddSubview (lowHighSensors.lowArea.LabelSubview);
      lowHighSensors.lowArea.snapArea.AddSubview (lowHighSensors.lowArea.DeviceImage);
      lowHighSensors.lowArea.snapArea.AddSubview (lowHighSensors.lowArea.Connection);
      lowHighSensors.lowArea.snapArea.AddSubview (lowHighSensors.lowArea.subviewDivider);
      lowHighSensors.lowArea.snapArea.AddSubview (lowHighSensors.lowArea.headingDivider);
      lowHighSensors.lowArea.snapArea.AddSubview(lowHighSensors.lowArea.subviewHide);
      lowHighSensors.lowArea.snapArea.BringSubviewToFront(lowHighSensors.lowArea.headingDivider);
      lowHighSensors.lowArea.snapArea.AddSubview (lowHighSensors.lowArea.connectionColor);
      lowHighSensors.lowArea.snapArea.BringSubviewToFront(lowHighSensors.lowArea.Connection);
      lowHighSensors.lowArea.snapArea.AddSubview(lowHighSensors.lowArea.conDisButton);
      lowHighSensors.lowArea.snapArea.BringSubviewToFront(lowHighSensors.lowArea.conDisButton);
      lowHighSensors.lowArea.snapArea.BringSubviewToFront(lowHighSensors.lowArea.subviewDivider);

			mainView.AddSubview (lowHighSensors.lowArea.snapArea);
			mainView.AddSubview (lowHighSensors.lowArea.subviewTable);

			lowHighSensors.highArea.snapArea.BackgroundColor = UIColor.White;
			lowHighSensors.highArea.snapArea.Alpha = 1f;
			lowHighSensors.highArea.snapArea.UserInteractionEnabled = true;
			lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
			lowHighSensors.highArea.snapArea.Layer.CornerRadius = 5;

			lowHighSensors.highArea.LabelTop.AdjustsFontSizeToFitWidth = true;
			lowHighSensors.highArea.LabelTop.Text = "";
			lowHighSensors.highArea.LabelTop.Layer.CornerRadius = 5;
			lowHighSensors.highArea.LabelTop.ClipsToBounds = true;

			lowHighSensors.highArea.LabelMiddle.AdjustsFontSizeToFitWidth = true;
      lowHighSensors.highArea.LabelMiddle.Text = Util.Strings.Analyzer.HIGHUNDEFINED;
			lowHighSensors.highArea.LabelMiddle.TextAlignment = UITextAlignment.Right;

			lowHighSensors.highArea.LabelBottom.AdjustsFontSizeToFitWidth = true;
			lowHighSensors.highArea.LabelBottom.Text = "";
			lowHighSensors.highArea.LabelBottom.TextAlignment = UITextAlignment.Right;

			lowHighSensors.highArea.LabelSubview.AdjustsFontSizeToFitWidth = true;
			lowHighSensors.highArea.LabelSubview.Text = "";
			lowHighSensors.highArea.LabelSubview.TextColor = UIColor.White;
			lowHighSensors.highArea.LabelSubview.ClipsToBounds = true;

			lowHighSensors.highArea.subviewTable.BackgroundColor = UIColor.Clear;
			lowHighSensors.highArea.subviewTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			lowHighSensors.highArea.subviewTable.Hidden = true;
			lowHighSensors.highArea.subviewTable.Source = null;

      lowHighSensors.highArea.subviewHide.BackgroundColor = UIColor.Red;
      lowHighSensors.highArea.subviewHide.SetImage(null, UIControlState.Normal);
      lowHighSensors.highArea.subviewHide.Hidden = true;

      lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
      lowHighSensors.highArea.Connection.Hidden = true;
      lowHighSensors.highArea.Connection.BackgroundColor = UIColor.Clear;
      lowHighSensors.highArea.connectionColor.Layer.CornerRadius = 5;
      lowHighSensors.highArea.connectionColor.Layer.BorderColor = UIColor.Black.CGColor;
      lowHighSensors.highArea.connectionColor.Layer.BorderWidth = 1f;
      lowHighSensors.highArea.connectionColor.Hidden = true;
      lowHighSensors.highArea.subviewDivider.BackgroundColor = UIColor.Black;
      lowHighSensors.highArea.headingDivider.Hidden = true;
      lowHighSensors.highArea.headingDivider.BackgroundColor = UIColor.Black;

			lowHighSensors.highArea.snapArea.AddSubview (lowHighSensors.highArea.LabelTop);
			lowHighSensors.highArea.snapArea.AddSubview (lowHighSensors.highArea.LabelMiddle);
			lowHighSensors.highArea.snapArea.AddSubview (lowHighSensors.highArea.LabelBottom);
			lowHighSensors.highArea.snapArea.AddSubview (lowHighSensors.highArea.LabelSubview);
      lowHighSensors.highArea.snapArea.AddSubview (lowHighSensors.highArea.DeviceImage);
      lowHighSensors.highArea.snapArea.AddSubview (lowHighSensors.highArea.Connection);
      lowHighSensors.highArea.snapArea.AddSubview (lowHighSensors.highArea.subviewDivider);
      lowHighSensors.highArea.snapArea.AddSubview (lowHighSensors.highArea.headingDivider);
      lowHighSensors.highArea.snapArea.AddSubview(lowHighSensors.highArea.subviewHide);
      lowHighSensors.highArea.snapArea.BringSubviewToFront(lowHighSensors.highArea.headingDivider);
      lowHighSensors.highArea.snapArea.AddSubview (lowHighSensors.highArea.connectionColor);
      lowHighSensors.highArea.snapArea.BringSubviewToFront(lowHighSensors.highArea.Connection);
      lowHighSensors.highArea.snapArea.AddSubview(lowHighSensors.highArea.conDisButton);
      lowHighSensors.highArea.snapArea.BringSubviewToFront(lowHighSensors.highArea.conDisButton);
      lowHighSensors.highArea.snapArea.BringSubviewToFront(lowHighSensors.highArea.subviewDivider);

			mainView.AddSubview (lowHighSensors.highArea.snapArea);
			mainView.AddSubview (lowHighSensors.highArea.subviewTable);
		}
		/// <summary>
		/// Sets up each of the 8 single sensor ui elements
		/// </summary>
		/// <param name="subview">Subview.</param>
		/// <param name="topLabel">Top label.</param>
		/// <param name="middleLabel">Middle label.</param>
		/// <param name="bottomLabel">Bottom label.</param>
		/// <param name="origin">Origin.</param>
		public static void CreateSubviewLayout(UIView subview, UILabel topLabel,  UILabel middleLabel, UILabel bottomLabel){
      topLabel.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect (0, 0, .99 * subview.Frame.Size.Width, .307 * subview.Frame.Size.Height);
			topLabel.AdjustsFontSizeToFitWidth = true;
			topLabel.Text = " Press " + subview.AccessibilityIdentifier;
			topLabel.Hidden = true;
			topLabel.ClipsToBounds = true;
			topLabel.TextColor = UIColor.Gray;
			topLabel.TextAlignment = UITextAlignment.Center;

      middleLabel.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect (0, .261 * subview.Bounds.Height, .984 * subview.Frame.Size.Width, .461 * subview.Frame.Size.Height);
			middleLabel.AdjustsFontSizeToFitWidth = true;
			middleLabel.Text = "0.00 ";
			middleLabel.Hidden = true;
			middleLabel.TextAlignment = UITextAlignment.Right;
			middleLabel.Font = UIFont.BoldSystemFontOfSize(20);

      bottomLabel.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect (0, .676 * subview.Bounds.Height, .99 * subview.Frame.Size.Width, .3 * subview.Frame.Size.Height);
			bottomLabel.AdjustsFontSizeToFitWidth = true;
			bottomLabel.Text = "";
			bottomLabel.Hidden = true;
			bottomLabel.ClipsToBounds = true;
			bottomLabel.TextAlignment = UITextAlignment.Center;
			bottomLabel.TextColor = UIColor.Gray;

			subview.AddSubview (topLabel);
			subview.AddSubview (middleLabel);
			subview.AddSubview (bottomLabel);
      subview.BringSubviewToFront(topLabel);
		}

		/// <summary>
		/// Removes 
		/// </summary>
		public static void RemoveRemoteDevice(sensor removeSensor, LowHighArea lowHighSensors, sensorGroup analyzerSensors){
			//Console.WriteLine("Removing Device: " + removeSensor.currentSensor.name);
      //removeSensor.snapArea.RemoveGestureRecognizer (removeSensor.holdGesture);
  //    removeSensor.snapArea.RemoveGestureRecognizer(removeSensor.panGesture);
  //    removeSensor.snapArea.BackgroundColor = UIColor.Clear;
  //    removeSensor.availableView.Hidden = false;
  //    removeSensor.sactionView.aView.Hidden = true;
  //    removeSensor.addIcon.Hidden = false;
  //    removeSensor.topLabel.Hidden = true;
  //    removeSensor.topLabel.BackgroundColor = UIColor.Clear;
  //    removeSensor.topLabel.TextColor = UIColor.Gray;
  //    removeSensor.middleLabel.Hidden = true;
  //    removeSensor.bottomLabel.Hidden = true;
  //    removeSensor.topLabel.Text = "Press ";
  //    removeSensor.middleLabel.Text = "0.00";
  //    removeSensor.bottomLabel.Text = "";
  //    //not sure if removing should disconnect the device.....
  //    if (removeSensor.isManual.Equals(false)) {
  //      removeSensor.currentSensor.onSensorStateChangedEvent -= removeSensor.gaugeUpdating;
  //      removeSensor.sactionView.currentSensor.onSensorStateChangedEvent -= removeSensor.sactionView.gaugeUpdating;
  //      removeSensor.lowArea.currentSensor.onSensorStateChangedEvent -= removeSensor.lowArea.gaugeUpdating;
  //      removeSensor.highArea.currentSensor.onSensorStateChangedEvent -= removeSensor.lowArea.gaugeUpdating;

  //      removeSensor.lowArea.manifold.Dispose();
  //      removeSensor.highArea.manifold.Dispose();
  //      removeSensor.currentSensor = null;
  //      removeSensor.sactionView.currentSensor = null;
  //      removeSensor.lowArea.currentSensor = null;
  //      removeSensor.highArea.currentSensor = null;
  //    }
  //    removeSensor.isManual = false;
  //    removeSensor.lowArea.snapArea.Hidden = true;
  //    removeSensor.lowArea.isManual = false;
  //    removeSensor.lowArea.max = 0;
  //    removeSensor.lowArea.min = 0;
  //    removeSensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);
  //    removeSensor.lowArea.subviewTable.Source = null;
  //    removeSensor.lowArea.subviewTable.ReloadData ();
  //    removeSensor.lowArea.subviewTable.Hidden = true;
  //    //removeSensor.lowArea.tableSubviews = new List<string> ();
  //    removeSensor.lowArea.tableSubviews.Clear();

  //    removeSensor.highArea.snapArea.Hidden = true;
  //    removeSensor.highArea.isManual = false;
  //    removeSensor.highArea.max = 0;
  //    removeSensor.highArea.min = 0;
  //    removeSensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);
  //    removeSensor.highArea.subviewTable.Source = null;
  //    removeSensor.highArea.subviewTable.ReloadData ();
  //    removeSensor.highArea.subviewTable.Hidden = true;
  //    //removeSensor.highArea.tableSubviews = new List<string> ();
  //    removeSensor.highArea.tableSubviews.Clear();

  //    if (removeSensor.lowArea.attachedSensor != null) {
  //      removeSensor.lowArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
  //     	removeSensor.lowArea.attachedSensor.topLabel.TextColor = UIColor.Gray;     
  //      removeSensor.lowArea.attachedSensor = null;

  //      removeSensor.highArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
  //      removeSensor.highArea.attachedSensor.topLabel.TextColor = UIColor.Gray;     
  //      removeSensor.highArea.attachedSensor = null;
  //    } else {
  //      for (int i = 0; i < 8; i++) {
  //        if (analyzerSensors.viewList[i].lowArea.attachedSensor != null && analyzerSensors.viewList[i].lowArea.attachedSensor == removeSensor) {
  //          analyzerSensors.viewList[i].lowArea.attachedSensor = null;
  //          analyzerSensors.viewList[i].highArea.attachedSensor = null;

  //          if (analyzerSensors.viewList[i].currentSensor != null) {
  //            analyzerSensors.viewList[i].lowArea.manifold = new Manifold(analyzerSensors.viewList[i].currentSensor);
  //            analyzerSensors.viewList[i].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].lowArea.ion, Fluid.EState.Dew);
  //            analyzerSensors.viewList[i].highArea.manifold = new Manifold(analyzerSensors.viewList[i].currentSensor);
  //            analyzerSensors.viewList[i].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].lowArea.ion, Fluid.EState.Dew);
  //          } else if (analyzerSensors.viewList[i].manualSensor != null) {
  //            analyzerSensors.viewList[i].lowArea.manifold = new Manifold(analyzerSensors.viewList[i].manualSensor);
  //            analyzerSensors.viewList[i].lowArea.manifold.primarySensor.unit = analyzerSensors.viewList[i].lowArea.manualSensor.unit;
  //            analyzerSensors.viewList[i].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].lowArea.ion, Fluid.EState.Dew);

  //            analyzerSensors.viewList[i].highArea.manifold = new Manifold(analyzerSensors.viewList[i].manualSensor);
  //            analyzerSensors.viewList[i].highArea.manifold.primarySensor.unit = analyzerSensors.viewList[i].lowArea.manualSensor.unit;
  //            analyzerSensors.viewList[i].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].lowArea.ion, Fluid.EState.Dew);
  //          }
  //        }
  //      }
  //    } 

  //    if (removeSensor.snapArea.AccessibilityIdentifier == lowHighSensors.lowArea.snapArea.AccessibilityIdentifier){
  //      lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
  //      ion.currentAnalyzer.lowAccessibility = "low";
  //    }
  //    else if (removeSensor.snapArea.AccessibilityIdentifier == lowHighSensors.highArea.snapArea.AccessibilityIdentifier){
  //      lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
		//		ion.currentAnalyzer.highAccessibility = "high";
		//	}
		}		
		
		/// <summary>
		/// WHAT TODO WHEN THEY WANT TO REMOVE A SENSOR FROM LOW/HIGH AREA
		/// </summary>
		/// <param name="pressedArea">LOW/HIGH VIEW</param>
		/// <param name="topLabel">LOW/HIGH TOP LABEL</param>
		/// <param name="middleLabel">LOW/HIGH TOP LABEL</param>
		/// <param name="bottomLabel">LOW/HIGH TOP LABEL</param>
		/// <param name="removeView">ATTACHED SENSOR TOP LABEL</param>
		public static string RemoveDevice(sensor removeSensor, LowHighArea lowHighSensors){
			Console.WriteLine("AnalyzerUtilities RemoveDevice from low/high area. Sensor name " + removeSensor.topLabel.Text + " and low area name " + lowHighSensors.lowArea.LabelTop.Text + " and high area name " + lowHighSensors.highArea.LabelTop.Text);
 			var attached = "null";
			removeSensor.topLabel.BackgroundColor = UIColor.Clear;
      removeSensor.topLabel.TextColor = UIColor.Gray;
      
			if(removeSensor.isManual){
				////CHECK WHICH AREA THE MANUAL SENSOR WAS ATTACHED TO
	      if (removeSensor.manualSensor == lowHighSensors.lowArea.manualSensor){
					/////CHECK IF LOW AREA HAD AN ATTACHED SENSOR
					if(lowHighSensors.lowArea.attachedSensor != null){
						attached = "low";
					}	      
	        lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
	        ion.currentAnalyzer.lowAccessibility = "low";
	        //REMOVE THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
	        Console.WriteLine("AnalyserUtilities RemoveDevice low/high removing analyzer low manifold");
	        ion.currentAnalyzer.lowFluid = null;
	        lowHighSensors.lowArea.tableSubviews.Clear();
	        lowHighSensors.lowArea.currentSensor = null;
	        lowHighSensors.lowArea.manualSensor = null;
	        lowHighSensors.lowArea.manifold = null;
	        lowHighSensors.lowArea.attachedSensor = null;
	        lowHighSensors.lowArea.LabelMiddle.Text = Strings.Analyzer.LOWUNDEFINED;
					lowHighSensors.hideView(lowHighSensors.lowArea);
	      }
	      else if (removeSensor.manualSensor == lowHighSensors.highArea.manualSensor){
					/////CHECK IF HIGH AREA HAD AN ATTACHED SENSOR
					if(lowHighSensors.highArea.attachedSensor != null){
						attached = "high";
					}	      
	        lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
	        ion.currentAnalyzer.highAccessibility = "high";
	        //REMOVE THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
	        Console.WriteLine("AnalyserUtilities RemoveDevice low/high removing analyzer high manifold");
	        ion.currentAnalyzer.highFluid = null;
	        lowHighSensors.highArea.tableSubviews.Clear();
	        lowHighSensors.highArea.currentSensor = null;
	        lowHighSensors.highArea.manualSensor = null;
	        lowHighSensors.highArea.manifold = null;
	        lowHighSensors.highArea.attachedSensor = null;        
	        lowHighSensors.highArea.LabelMiddle.Text = Strings.Analyzer.HIGHUNDEFINED;	        
					lowHighSensors.hideView(lowHighSensors.highArea);
				}				
			} else {
				////CHECK WHICH AREA THE GAUGE SENSOR WAS ATTACHED TO
	      if (removeSensor.currentSensor == lowHighSensors.lowArea.currentSensor){
					/////CHECK IF LOW AREA HAD AN ATTACHED SENSOR
					if(lowHighSensors.lowArea.attachedSensor != null){
						attached = "low";
					}	      
	        lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
	        ion.currentAnalyzer.lowAccessibility = "low";
	        //REMOVE THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
	        Console.WriteLine("AnalyserUtilities RemoveDevice low/high removing analyzer low manifold");
	        ion.currentAnalyzer.lowFluid = null;
	        lowHighSensors.lowArea.tableSubviews.Clear();
	        lowHighSensors.lowArea.currentSensor = null;
	        lowHighSensors.lowArea.manualSensor = null;
	        lowHighSensors.lowArea.manifold = null;
	        lowHighSensors.lowArea.attachedSensor = null;
	        lowHighSensors.lowArea.LabelMiddle.Text = Strings.Analyzer.LOWUNDEFINED;
					lowHighSensors.hideView(lowHighSensors.lowArea);
	      }
	      else if (removeSensor.currentSensor == lowHighSensors.highArea.currentSensor){
					/////CHECK IF HIGH AREA HAD AN ATTACHED SENSOR
					if(lowHighSensors.highArea.attachedSensor != null){
						attached = "high";
					}
	        lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
	        ion.currentAnalyzer.highAccessibility = "high";
	        //REMOVE THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
	        Console.WriteLine("AnalyserUtilities RemoveDevice low/high removing analyzer high manifold");
	        ion.currentAnalyzer.highFluid = null;
	        lowHighSensors.highArea.tableSubviews.Clear();
	        lowHighSensors.highArea.currentSensor = null;
	        lowHighSensors.highArea.manualSensor = null;
	        lowHighSensors.highArea.manifold = null;
	        lowHighSensors.highArea.attachedSensor = null;        
	        lowHighSensors.highArea.LabelMiddle.Text = Strings.Analyzer.LOWUNDEFINED;

					lowHighSensors.hideView(lowHighSensors.highArea);
				}			
			}

      Console.WriteLine("Low side identifier after LH removal: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier);
      Console.WriteLine("High side identifier after LH removal: " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
      Console.WriteLine("Sending back attached: " + attached);
      return attached;
		}
		
		/// <summary>
		/// WHAT TODO WHEN THEY WANT TO REMOVE A SINGLE SENSOR
		/// </summary>
    public static void RemoveDevice(actionPopup Sensor, LowHighArea lowHighSensors, sensorGroup analyzerSensors, List<Sensor> sensorList){
    	////REMOVE SENSOR FROM ACTIVE SENSOR LIST
      for(int i = 0; i < sensorList.Count; i++){
        if (sensorList[i] != null) {
          if (Sensor.pressedSensor.currentSensor != null) {
            if (sensorList[i].name == Sensor.pressedSensor.currentSensor.device.name && sensorList[i].type == Sensor.pressedSensor.currentSensor.type) {
              sensorList.Remove(Sensor.pressedSensor.currentSensor);
            }
          }
        }
      }
			/////RESET SENSOR MOUNT TO BE BLANK
      Sensor.pressedSensor.snapArea.RemoveGestureRecognizer (Sensor.addLong);
      Sensor.pressedSensor.snapArea.RemoveGestureRecognizer(Sensor.addPan);
      Sensor.pressedSensor.availableView.Hidden = false;
      Sensor.pressedView.BackgroundColor = UIColor.Clear;
      Sensor.pressedSensor.sactionView.aView.Hidden = true;
      Sensor.pressedSensor.addIcon.Hidden = false;
      Sensor.pressedSensor.topLabel.Hidden = true;
      Sensor.pressedSensor.topLabel.BackgroundColor = UIColor.Clear;
      Sensor.pressedSensor.topLabel.TextColor = UIColor.Gray;
      Sensor.pressedSensor.middleLabel.Hidden = true;
      Sensor.pressedSensor.bottomLabel.Hidden = true;
      Sensor.pressedSensor.topLabel.Text = "Press " + Sensor.pressedSensor.snapArea.AccessibilityIdentifier;
      Sensor.pressedSensor.middleLabel.Text = "0.00";
      Sensor.pressedSensor.bottomLabel.Text = "psig";
      //not sure if removing should disconnect the device.....
      if (Sensor.pressedSensor.isManual.Equals(false)) {
        Sensor.pressedSensor.currentSensor.onSensorStateChangedEvent -= Sensor.pressedSensor.gaugeUpdating;
        Sensor.pressedSensor.sactionView.currentSensor.onSensorStateChangedEvent -= Sensor.pressedSensor.sactionView.gaugeUpdating;
        ////CHECK IF REMOVING GAUGE SENSOR IS THE LOW SIDE
        if(lowHighSensors.lowArea.currentSensor != null && Sensor.pressedSensor.currentSensor == lowHighSensors.lowArea.currentSensor){
					lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
					lowHighSensors.lowArea.tableSubviews.Clear();
	        ion.currentAnalyzer.lowAccessibility = "low";
	        lowHighSensors.lowArea.LabelMiddle.Text = Strings.Analyzer.LOWUNDEFINED;
	        //REMOVE THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
	        Console.WriteLine("AnalyserUtilities RemoveDevice single removing analyzer low manifold");
	        ion.currentAnalyzer.lowFluid = null; 
	        /////CHECK IF LOW HIGH AREA BEING REMOVED HAS AN ATTACHED SENSOR
					if(lowHighSensors.lowArea.attachedSensor != null){
						//////CHECK IF ATTACHED SENSOR IS A MANUAL SENSOR OR A GAUGE SENSOR
						if(lowHighSensors.lowArea.attachedSensor.isManual){
				      for (int i = 0; i < 8; i++) {
				        if (analyzerSensors.viewList[i].manualSensor != null && lowHighSensors.lowArea.attachedSensor.manualSensor == analyzerSensors.viewList[i].manualSensor) {
				          analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
				          analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
				        }
				      }
						}
						else {
				      for (int i = 0; i < 8; i++) {
				        if (analyzerSensors.viewList[i].currentSensor != null && lowHighSensors.lowArea.attachedSensor.currentSensor == analyzerSensors.viewList[i].currentSensor) {
				          analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
				          analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
				        }
				      }
			      }
				    lowHighSensors.lowArea.attachedSensor = null;			      
					}
					lowHighSensors.hideView(lowHighSensors.lowArea);
				} 
        ////CHECK IF REMOVING GAUGE SENSOR IS THE HIGH SIDE
				else if (lowHighSensors.highArea.currentSensor != null && Sensor.pressedSensor.currentSensor == lowHighSensors.highArea.currentSensor){
	        lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
					lowHighSensors.highArea.tableSubviews.Clear();
	        ion.currentAnalyzer.highAccessibility = "high";
	        lowHighSensors.highArea.LabelMiddle.Text = Strings.Analyzer.LOWUNDEFINED;
	        //REMOVE THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
	        Console.WriteLine("AnalyserUtilities RemoveDevice single removing analyzer high manifold");
	        ion.currentAnalyzer.highFluid = null;
				
	        /////CHECK IF LOW HIGH AREA BEING REMOVED HAS AN ATTACHED SENSOR
					if(lowHighSensors.highArea.attachedSensor != null){
						//////CHECK IF ATTACHED SENSOR IS A MANUAL SENSOR OR A GAUGE SENSOR
						if(lowHighSensors.highArea.attachedSensor.isManual){
				      for (int i = 0; i < 8; i++) {
				        if (analyzerSensors.viewList[i].manualSensor != null && lowHighSensors.highArea.attachedSensor.manualSensor == analyzerSensors.viewList[i].manualSensor) {
				          analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
				          analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
				        }
				      }
						}
						else {
				      for (int i = 0; i < 8; i++) {
				        if (analyzerSensors.viewList[i].currentSensor != null && lowHighSensors.highArea.attachedSensor.currentSensor == analyzerSensors.viewList[i].currentSensor) {
				          analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
				          analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
				        }
				      }
			      }
				    lowHighSensors.highArea.attachedSensor = null;
					}					
					lowHighSensors.hideView(lowHighSensors.highArea);					
				}
      } else {
        ////CHECK IF REMOVING MANUAL SENSOR IS THE LOW SIDE
        if(lowHighSensors.lowArea.manualSensor != null && Sensor.pressedSensor.manualSensor == lowHighSensors.lowArea.manualSensor){
					lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
	        ion.currentAnalyzer.lowAccessibility = "low";
	        lowHighSensors.lowArea.LabelMiddle.Text = Strings.Analyzer.LOWUNDEFINED;
	        //REMOVE THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
	        Console.WriteLine("AnalyserUtilities RemoveDevice single removing analyzer low manifold");
	        ion.currentAnalyzer.lowFluid = null;
	        /////CHECK IF LOW HIGH AREA BEING REMOVED HAS AN ATTACHED SENSOR
					if(lowHighSensors.lowArea.attachedSensor != null){
						//////CHECK IF ATTACHED SENSOR IS A MANUAL SENSOR OR A GAUGE SENSOR
						if(lowHighSensors.lowArea.attachedSensor.isManual){
				      for (int i = 0; i < 8; i++) {
				        if (analyzerSensors.viewList[i].manualSensor != null && lowHighSensors.lowArea.attachedSensor.manualSensor == analyzerSensors.viewList[i].manualSensor) {
				          analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
				          analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
				        }
				      }
						}
						else {
				      for (int i = 0; i < 8; i++) {
				        if (analyzerSensors.viewList[i].currentSensor != null && lowHighSensors.lowArea.attachedSensor.currentSensor == analyzerSensors.viewList[i].currentSensor) {
				          analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
				          analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
				        }
				      }
			      }
				    lowHighSensors.lowArea.attachedSensor = null;
					}					
					lowHighSensors.hideView(lowHighSensors.lowArea);
				} 
        ////CHECK IF REMOVING MANUAL SENSOR IS THE HIGH SIDE
				else if (lowHighSensors.highArea.manualSensor != null && Sensor.pressedSensor.manualSensor == lowHighSensors.highArea.manualSensor){
	        lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
	        ion.currentAnalyzer.highAccessibility = "high";
	        lowHighSensors.highArea.LabelMiddle.Text = Strings.Analyzer.HIGHUNDEFINED;
	        //REMOVE THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
	        Console.WriteLine("AnalyserUtilities RemoveDevice single removing analyzer high manifold");
	        ion.currentAnalyzer.highFluid = null;
				
	        /////CHECK IF LOW HIGH AREA BEING REMOVED HAS AN ATTACHED SENSOR
					if(lowHighSensors.highArea.attachedSensor != null){
						//////CHECK IF ATTACHED SENSOR IS A MANUAL SENSOR OR A GAUGE SENSOR
						if(lowHighSensors.highArea.attachedSensor.isManual){
				      for (int i = 0; i < 8; i++) {
				        if (analyzerSensors.viewList[i].manualSensor != null && lowHighSensors.highArea.attachedSensor.manualSensor == analyzerSensors.viewList[i].manualSensor) {
				          analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
				          analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
				        }
				      }
						}
						else {
				      for (int i = 0; i < 8; i++) {
				        if (analyzerSensors.viewList[i].currentSensor != null && lowHighSensors.highArea.attachedSensor.currentSensor == analyzerSensors.viewList[i].currentSensor) {
				          analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
				          analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
				        }
				      }
			      }
				    lowHighSensors.highArea.attachedSensor = null;
					}				
					lowHighSensors.hideView(lowHighSensors.highArea);
				}
			}
			Sensor.pressedSensor.currentSensor = null;
			Sensor.pressedSensor.manualSensor = null;
			Sensor.pressedSensor.isManual = false;
		}

    /// <summary>
    /// Popup to show available subview options to add to LOW / HIGH area
    /// </summary>
    /// <param name="pressedArea">The LOW or High section pressed</param>
    public static void subviewOptionChosen(lowHighSensor pressedArea){
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }

      UIAlertController subviewAlert = UIAlertController.Create (Util.Strings.Analyzer.SUBVIEW, "", UIAlertControllerStyle.Alert);

      foreach (string subview in pressedArea.availableSubviews) {
        var splits = subview.Split(' ');
        if (splits[0].Equals("Linked") && pressedArea.manifold.secondarySensor == null) {
          continue;
        }
        if ((splits[0].Equals("Superheat") || splits[0].Equals("Pressure")) && pressedArea.manifold.primarySensor.type == ESensorType.Vacuum) {
          continue;
        }

        if (!pressedArea.tableSubviews.Contains (splits[0])) {
          subviewAlert.AddAction (UIAlertAction.Create (subview, UIAlertActionStyle.Default, (action) => {
            ////set linked sensor to always be first in the table
            if(splits[0].Equals("Linked")){
              pressedArea.tableSubviews.Insert(0,splits[0]);
            } else {
              pressedArea.tableSubviews.Add(splits[0]);
            }

            pressedArea.subviewTable.Source = new AnalyzerTableSource(pressedArea.tableSubviews, pressedArea);
            pressedArea.subviewTable.ReloadData();
            if(pressedArea.subviewTable.Hidden)
              pressedArea.subviewTable.Hidden = false;
            pressedArea.subviewHide.SetImage(UIImage.FromBundle("ic_arrow_downwhite"), UIControlState.Normal);
            pressedArea.subviewHide.Hidden = false;
          }));
        }
      }

      subviewAlert.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));
      vc.PresentViewController (subviewAlert, true, null);
    }
		/// <summary>
		/// ARRANGES THE SINGLE SENSOR SUBVIEWS TO HOW THE USER HAS ORDERED THEM
		/// </summary>
		/// <param name="touchPoint">LOCATION OF SUBVIEW WHEN FINGER WAS REMOVED</param>
		/// <param name="position">WHICH SUBVIEW WAS MOVING</param>
    public static void sensorSwap(sensorGroup analyzerSensors,LowHighArea lowHighSensors, int position, CGPoint touchPoint, UIView View,ION.Core.Content.Analyzer currentAnalyzer){
    	Console.WriteLine("AnalyserUtilities sensorSwap called. Low association: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier + " high association: " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
			int start = analyzerSensors.areaList.IndexOf(position);
			int start2 = currentAnalyzer.sensorPositions.IndexOf(position);
			int swap = 0;
			int swap2 = 0;
      bool removeLH = false;
      Console.WriteLine("layout started");
      foreach(var spot in analyzerSensors.areaList){
				Console.WriteLine(spot);
			}
			Console.WriteLine(Environment.NewLine);

			////CHECK LOCATION OF SUBVIEW WHEN TOUCH ENDED TO DETERMINE INDEX PLACEMENT
			if (analyzerSensors.snapRect1.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[0];
        swap2 = currentAnalyzer.sensorPositions[0];
        analyzerSensors.areaList[0] = position;
        analyzerSensors.areaList[start] = swap;

        currentAnalyzer.sensorPositions[0] = position;
        currentAnalyzer.sensorPositions[start2] = swap2;

        if (start > 3) {
          removeLH = true;
        }
         swap = 0;
        
			} else if (analyzerSensors.snapRect2.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[1];
        swap2 = currentAnalyzer.sensorPositions[1];
        analyzerSensors.areaList[1] = position;
        analyzerSensors.areaList[start] = swap;

        currentAnalyzer.sensorPositions[1] = position;
        currentAnalyzer.sensorPositions[start2] = swap2;

        if (start > 3) {
          removeLH = true;
        }
          swap = 1;
			} else if (analyzerSensors.snapRect3.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[2];
        swap2 = currentAnalyzer.sensorPositions[2];
        analyzerSensors.areaList[2] = position;
        analyzerSensors.areaList[start] = swap;

       	currentAnalyzer.sensorPositions[2] = position;
        currentAnalyzer.sensorPositions[start2] = swap2;
			
        if (start > 3) {
          removeLH = true;
        }
          swap = 2;
			} else if (analyzerSensors.snapRect4.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[3];
        swap2 = currentAnalyzer.sensorPositions[3];
        analyzerSensors.areaList[3] = position;
        analyzerSensors.areaList[start] = swap;

       	currentAnalyzer.sensorPositions[3] = position;
        currentAnalyzer.sensorPositions[start2] = swap2;

        if (start > 3) {
          removeLH = true;
        }
          swap = 3;
			} else if (analyzerSensors.snapRect5.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[4];
        swap2 = currentAnalyzer.sensorPositions[4];
        analyzerSensors.areaList[4] = position;
        analyzerSensors.areaList[start] = swap;

       	currentAnalyzer.sensorPositions[4] = position;
        currentAnalyzer.sensorPositions[start2] = swap2;

        if (start < 4) {
          removeLH = true;
        }
          swap = 4;
			} else if (analyzerSensors.snapRect6.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[5];
        swap2 = currentAnalyzer.sensorPositions[5];
        analyzerSensors.areaList[5] = position;
        analyzerSensors.areaList[start] = swap;

       	currentAnalyzer.sensorPositions[5] = position;
        currentAnalyzer.sensorPositions[start2] = swap2;

        if (start < 4) {
          removeLH = true;
        }
          swap = 5;
			} else if (analyzerSensors.snapRect7.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[6];
        swap2 = currentAnalyzer.sensorPositions[6];
        analyzerSensors.areaList[6] = position;
        analyzerSensors.areaList[start] = swap;

       	currentAnalyzer.sensorPositions[6] = position;
        currentAnalyzer.sensorPositions[start2] = swap2;
        if (start < 4) {
          removeLH = true;
        }
          swap = 6;
			} else if (analyzerSensors.snapRect8.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[7];
        swap2 = currentAnalyzer.sensorPositions[7];
        analyzerSensors.areaList[7] = position;
        analyzerSensors.areaList[start] = swap;

       	currentAnalyzer.sensorPositions[7] = position;
        currentAnalyzer.sensorPositions[start2] = swap2;

        if (start < 4) {
          removeLH = true;
        }
          swap = 7;
			} else {
				swap = start;
			}
      confirmLayout(analyzerSensors, View);
      Console.WriteLine("layout ended");
   //   foreach(var spot in analyzerSensors.areaList){
			//	Console.WriteLine(spot);
			//}
			Console.WriteLine(Environment.NewLine);
			arrangeViews(analyzerSensors);
			
			Console.WriteLine("swap ended as " + swap);
			
			int swapLocation = analyzerSensors.areaList.IndexOf(position);

			//////SENSOR MOVED TO OPPOSITE "SIDE" AND NEEDS TO CHECK IF IT IS ASSOCIATED TO THE CORRESPONDING LOW OR HIGH AREA
      if (removeLH) {
        if(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == start.ToString() && lowHighSensors.highArea.snapArea.AccessibilityIdentifier == swap.ToString()){
          Console.WriteLine("In utilities low start sensorSwap and set low accessibility to " + start + " and high accessibility to " + swap);
					//////SWAP THE SENSOR MOUNT COLORS TO CORRESPOND TO THEIR NEW LOW HIGH ASSOCIATIONS
          analyzerSensors.viewList[start].topLabel.BackgroundColor = UIColor.Blue;
          analyzerSensors.viewList[swap].topLabel.BackgroundColor = UIColor.Red;
          
          
					///////REPLACE THE LOW AND HIGH AREA NAMES TO MATCH THE NEW SENSOR MOUNT ASSOCIATIONS
          lowHighSensors.lowArea.LabelTop.Text = analyzerSensors.viewList[start].topLabel.Text;
          lowHighSensors.lowArea.LabelSubview.Text =" " + lowHighSensors.lowArea.LabelTop.Text + Strings.Analyzer.LHTABLE;
          
          lowHighSensors.highArea.LabelTop.Text = analyzerSensors.viewList[swap].topLabel.Text;
          lowHighSensors.highArea.LabelSubview.Text =" " + lowHighSensors.highArea.LabelTop.Text + Strings.Analyzer.LHTABLE;
   
          ///////////////////// change the high manifold to be based on moved location instead of created position
          lowHighSensors.highArea.snapArea.AccessibilityIdentifier = swap.ToString();
          ion.currentAnalyzer.highAccessibility = swap.ToString();

          ///////////////////// change the low manifold to be based on moved location instead of created position
          ion.currentAnalyzer.lowAccessibility = start.ToString();
          lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = start.ToString();          
         
	        //////SET THE HIGH AND LOW FLUIDS 
        	Console.WriteLine("AnalyserUtilities SensorSwap swapping analyzer high first manifold");
	        ion.currentAnalyzer.highFluid  = lowHighSensors.highArea.manifold.ptChart.fluid;
	        ion.currentAnalyzer.lowFluid  = lowHighSensors.lowArea.manifold.ptChart.fluid;
          
          //////ASSOCIATE THE LOW SIDE TO THE MOVING SENSOR'S INFORMATION
          if (!analyzerSensors.viewList[start].isManual) {
          	Console.WriteLine("Low side is going to look at gauge sensor " + analyzerSensors.viewList[start].topLabel.Text);
          	lowHighSensors.lowArea.manualSensor = null;
          	lowHighSensors.lowArea.currentSensor = analyzerSensors.viewList[start].currentSensor;
          	lowHighSensors.lowArea.manifold = new Manifold(analyzerSensors.viewList[start].currentSensor);
          	lowHighSensors.lowArea.manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
          	lowHighSensors.lowArea.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(analyzerSensors.viewList[start].currentSensor.device.serialNumber.deviceModel);
          	lowHighSensors.lowArea.isManual = false;

          	var startMeasurement = SensorUtils.ToFormattedString(analyzerSensors.viewList[start].currentSensor.type, analyzerSensors.viewList[start].currentSensor.measurement, true).Split(' ');
          	lowHighSensors.lowArea.LabelMiddle.Text = startMeasurement[0];
          	lowHighSensors.lowArea.LabelBottom.Text = analyzerSensors.viewList[start].currentSensor.unit.ToString();
          	
            if(analyzerSensors.viewList[start].currentSensor.device.isConnected){
              lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");              
              lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Green;
              lowHighSensors.showView(lowHighSensors.lowArea);
            } else {
              lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");              
              lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Red;
              lowHighSensors.showView(lowHighSensors.lowArea);
            }
          } else {
          	Console.WriteLine("Low side is going to look at manual sensor " + analyzerSensors.viewList[start].topLabel.Text);
          	lowHighSensors.lowArea.isManual = true;
          	lowHighSensors.lowArea.currentSensor = null;
          	lowHighSensors.lowArea.manualSensor = analyzerSensors.viewList[start].manualSensor;
          	lowHighSensors.lowArea.manifold = new Manifold(analyzerSensors.viewList[start].manualSensor);
          	lowHighSensors.lowArea.manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
          	lowHighSensors.lowArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
          	
          	var startMeasurement = SensorUtils.ToFormattedString(analyzerSensors.viewList[start].manualSensor.type, analyzerSensors.viewList[start].manualSensor.measurement, true).Split(' ');
          	lowHighSensors.lowArea.LabelMiddle.Text = startMeasurement[0];
          	lowHighSensors.lowArea.LabelBottom.Text = analyzerSensors.viewList[start].manualSensor.unit.ToString();
            lowHighSensors.showView(lowHighSensors.lowArea);
					}
          ////TRADE SUBVIEWS BETWEEN THE LOW AND HIGH SIDES
          var highSubviews = new List<string>(lowHighSensors.highArea.tableSubviews);
          
          lowHighSensors.highArea.tableSubviews = new List<string>(lowHighSensors.lowArea.tableSubviews);
          lowHighSensors.lowArea.tableSubviews = new List<string>(highSubviews);
          
          lowHighSensors.highArea.subviewTable.Source = new AnalyzerTableSource(lowHighSensors.highArea.tableSubviews,lowHighSensors.highArea);
          lowHighSensors.lowArea.subviewTable.Source = new AnalyzerTableSource(lowHighSensors.lowArea.tableSubviews,lowHighSensors.lowArea);
 					Console.WriteLine("Low side has " + lowHighSensors.lowArea.tableSubviews.Count + " subviews and High side has " + lowHighSensors.highArea.tableSubviews.Count + " subviews");
          ////HIDE SUBVIEW TOGGLE FOR LOW SIDE EMPTY SUBVIEW TABLES
					if(lowHighSensors.lowArea.tableSubviews.Count == 0){
	      		lowHighSensors.lowArea.subviewHide.SetImage(null, UIControlState.Normal);
					} else {					
	      		lowHighSensors.lowArea.subviewHide.SetImage(UIImage.FromBundle("ic_arrow_downwhite"), UIControlState.Normal);
					}
          ////HIDE SUBVIEW TOGGLE FOR HIGH SIDE EMPTY SUBVIEW TABLES
					if(lowHighSensors.highArea.tableSubviews.Count == 0){
	      		lowHighSensors.highArea.subviewHide.SetImage(null, UIControlState.Normal);
					} else {					
	      		lowHighSensors.highArea.subviewHide.SetImage(UIImage.FromBundle("ic_arrow_downwhite"), UIControlState.Normal);
					}
         
          ion.currentAnalyzer.highSubviews = lowHighSensors.highArea.tableSubviews;
          ion.currentAnalyzer.lowSubviews = lowHighSensors.lowArea.tableSubviews;

          SubviewSwap(lowHighSensors.highArea.tableSubviews,lowHighSensors.highArea, lowHighSensors.lowArea);

					//////RELOAD THE SUBVIEW TABLES WITH THEIR NEW SUBVIEW SETUPS
          lowHighSensors.lowArea.subviewTable.ReloadData();
          lowHighSensors.highArea.subviewTable.ReloadData();
          lowHighSensors.lowArea.subviewTable.Hidden = false;
          lowHighSensors.highArea.subviewTable.Hidden = false;

					/////REMOVE THE LOW SIDE ATTACHED SENSOR IF IT EXISTS
          if (lowHighSensors.lowArea.attachedSensor != null) {
          	//Console.WriteLine("low and high area attached sensor are null");
            for(int i = 0; i < 8; i++){
							if(analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == lowHighSensors.lowArea.currentSensor){
		            analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Clear;
		            analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;            
							}
						}
						lowHighSensors.lowArea.attachedSensor = null;
          }

					/////REMOVE THE HIGH SIDE ATTACHED SENSOR IF IT EXISTS
          if (lowHighSensors.highArea.attachedSensor != null) {
            for(int i = 0; i < 8; i++){
							if(analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == lowHighSensors.highArea.currentSensor){
		            analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Clear;
		            analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;            
							}
						}
						lowHighSensors.highArea.attachedSensor = null;
          }
          
          //////ASSOCIATE THE HIGH SIDE TO THE SWAPPING SENSOR'S INFORMATION
          if (!analyzerSensors.viewList[swap].isManual) {
          	Console.WriteLine("High side is going to look at gauge sensor " + analyzerSensors.viewList[swap].topLabel.Text);
          	lowHighSensors.highArea.isManual = false;
          	lowHighSensors.highArea.manualSensor = null;
          	lowHighSensors.highArea.currentSensor = analyzerSensors.viewList[swap].currentSensor;
          	lowHighSensors.highArea.manifold = new Manifold(analyzerSensors.viewList[swap].currentSensor);
          	lowHighSensors.highArea.manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
           	lowHighSensors.highArea.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(analyzerSensors.viewList[swap].currentSensor.device.serialNumber.deviceModel);

          	var startMeasurement = SensorUtils.ToFormattedString(analyzerSensors.viewList[swap].currentSensor.type, analyzerSensors.viewList[swap].currentSensor.measurement, true).Split(' ');
          	lowHighSensors.highArea.LabelMiddle.Text = startMeasurement[0];
          	lowHighSensors.highArea.LabelBottom.Text = analyzerSensors.viewList[swap].currentSensor.unit.ToString();
         	
            if(analyzerSensors.viewList[swap].currentSensor.device.isConnected){
              lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");              
              lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Green;
              lowHighSensors.showView(lowHighSensors.highArea);
            } else {
              lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");              
              lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Red;
              lowHighSensors.showView(lowHighSensors.highArea);
            	lowHighSensors.showView(lowHighSensors.highArea);
            }
            
          } else {
          	Console.WriteLine("High side is going to look at manual sensor " + analyzerSensors.viewList[swap].topLabel.Text);
          	lowHighSensors.highArea.isManual = true;
          	lowHighSensors.highArea.currentSensor = null;
          	lowHighSensors.highArea.manualSensor = analyzerSensors.viewList[swap].manualSensor;
          	lowHighSensors.highArea.manifold = new Manifold(analyzerSensors.viewList[swap].manualSensor);
          	lowHighSensors.highArea.manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
          	lowHighSensors.highArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
          	
          	var startMeasurement = SensorUtils.ToFormattedString(analyzerSensors.viewList[swap].manualSensor.type, analyzerSensors.viewList[swap].manualSensor.measurement, true).Split(' ');
          	lowHighSensors.highArea.LabelMiddle.Text = startMeasurement[0];
          	lowHighSensors.highArea.LabelBottom.Text = analyzerSensors.viewList[swap].manualSensor.unit.ToString();
            lowHighSensors.showView(lowHighSensors.highArea);
					}      
        } 
        else if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier == start.ToString() && lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == swap.ToString()){
    
          Console.WriteLine("In utilities high start sensorSwap and set low accessibility to " + swap + " and high accessibility to " + start);
					//////SWAP THE SENSOR MOUNT COLORS TO CORRESPOND TO THEIR NEW LOW HIGH ASSOCIATIONS
          analyzerSensors.viewList[swap].topLabel.BackgroundColor = UIColor.Blue;
          analyzerSensors.viewList[start].topLabel.BackgroundColor = UIColor.Red;
					
					///////REPLACE THE LOW AND HIGH AREA NAMES TO MATCH THE NEW SENSOR MOUNT ASSOCIATIONS
          lowHighSensors.lowArea.LabelTop.Text = analyzerSensors.viewList[swap].topLabel.Text;
          lowHighSensors.lowArea.LabelSubview.Text =" " + lowHighSensors.lowArea.LabelTop.Text + Strings.Analyzer.LHTABLE;
          
          lowHighSensors.highArea.LabelTop.Text = analyzerSensors.viewList[start].topLabel.Text;
          lowHighSensors.highArea.LabelSubview.Text =" " + lowHighSensors.highArea.LabelTop.Text + Strings.Analyzer.LHTABLE;
          ///////////////////// change the low associations to be based on moved location instead of created position
          ion.currentAnalyzer.lowAccessibility = swap.ToString();
          lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = swap.ToString();          
          /////////////////////

          ///////////////////// change the high associations to be based on moved location instead of created position
          ion.currentAnalyzer.highAccessibility = start.ToString();
          lowHighSensors.highArea.snapArea.AccessibilityIdentifier = start.ToString();          
          ///////////////////// 

	        ///SET THE HIGH AND LOW FLUIDS 
	        ion.currentAnalyzer.highFluid  = lowHighSensors.highArea.manifold.ptChart.fluid;  
	        ion.currentAnalyzer.lowFluid  = lowHighSensors.lowArea.manifold.ptChart.fluid;          
 
          //////ASSOCIATE THE HIGH SIDE TO THE MOVING SENSOR'S INFORMATION
          if (!analyzerSensors.viewList[start].isManual) {
          	Console.WriteLine("High side is going to look at gauge sensor " + analyzerSensors.viewList[start].topLabel.Text);
          	lowHighSensors.highArea.manualSensor = null;
          	lowHighSensors.highArea.currentSensor = analyzerSensors.viewList[start].currentSensor;
          	lowHighSensors.highArea.manifold = new Manifold(analyzerSensors.viewList[start].currentSensor);
          	lowHighSensors.highArea.manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
          	lowHighSensors.highArea.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(analyzerSensors.viewList[start].currentSensor.device.serialNumber.deviceModel);
						lowHighSensors.highArea.isManual = false; 
						
          	var startMeasurement = SensorUtils.ToFormattedString(analyzerSensors.viewList[start].currentSensor.type, analyzerSensors.viewList[start].currentSensor.measurement, true).Split(' ');
          	lowHighSensors.highArea.LabelMiddle.Text = startMeasurement[0];
          	lowHighSensors.highArea.LabelBottom.Text = analyzerSensors.viewList[start].currentSensor.unit.ToString();
          	
            if(analyzerSensors.viewList[start].currentSensor.device.isConnected){
              lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");              
              lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Green;
              lowHighSensors.showView(lowHighSensors.highArea);
            } else {
              lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");              
              lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Red;
              lowHighSensors.showView(lowHighSensors.lowArea);
            }
          } else {
          	Console.WriteLine("High side is going to look at manual sensor " + analyzerSensors.viewList[start].topLabel.Text);
						lowHighSensors.highArea.isManual = true; 
          	lowHighSensors.highArea.currentSensor = null;
          	lowHighSensors.highArea.manualSensor = analyzerSensors.viewList[start].manualSensor;
          	lowHighSensors.highArea.manifold = new Manifold(analyzerSensors.viewList[start].manualSensor);
          	lowHighSensors.highArea.manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
          	lowHighSensors.highArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
          	
          	var startMeasurement = SensorUtils.ToFormattedString(analyzerSensors.viewList[start].manualSensor.type, analyzerSensors.viewList[start].manualSensor.measurement, true).Split(' ');
          	lowHighSensors.highArea.LabelMiddle.Text = startMeasurement[0];
          	lowHighSensors.highArea.LabelBottom.Text = analyzerSensors.viewList[start].manualSensor.unit.ToString();
            lowHighSensors.showView(lowHighSensors.highArea);
					}
 
          ////TRADE SUBVIEWS BETWEEN THE LOW AND HIGH SIDES
          var highSubviews = new List<string>(lowHighSensors.highArea.tableSubviews);
          
          lowHighSensors.highArea.tableSubviews = new List<string>(lowHighSensors.lowArea.tableSubviews);
          lowHighSensors.lowArea.tableSubviews = new List<string>(highSubviews);
          
          lowHighSensors.highArea.subviewTable.Source = new AnalyzerTableSource(lowHighSensors.highArea.tableSubviews,lowHighSensors.highArea);
          lowHighSensors.lowArea.subviewTable.Source = new AnalyzerTableSource(lowHighSensors.lowArea.tableSubviews,lowHighSensors.lowArea);
 					Console.WriteLine("Low side has " + lowHighSensors.lowArea.tableSubviews.Count + " subviews and High side has " + lowHighSensors.highArea.tableSubviews.Count + " subviews");

          ////HIDE SUBVIEW TOGGLE FOR LOW SIDE EMPTY SUBVIEW TABLES
					if(lowHighSensors.lowArea.tableSubviews.Count == 0){
	      		lowHighSensors.lowArea.subviewHide.SetImage(null, UIControlState.Normal);
					}else {					
	      		lowHighSensors.lowArea.subviewHide.SetImage(UIImage.FromBundle("ic_arrow_downwhite"), UIControlState.Normal);
					}
          ////HIDE SUBVIEW TOGGLE FOR HIGH SIDE EMPTY SUBVIEW TABLES
					if(lowHighSensors.highArea.tableSubviews.Count == 0){
	      		lowHighSensors.highArea.subviewHide.SetImage(null, UIControlState.Normal);
					}else {					
	      		lowHighSensors.highArea.subviewHide.SetImage(UIImage.FromBundle("ic_arrow_downwhite"), UIControlState.Normal);
					}
					
          ion.currentAnalyzer.highSubviews = lowHighSensors.highArea.tableSubviews;
          ion.currentAnalyzer.lowSubviews = lowHighSensors.lowArea.tableSubviews;
					
          SubviewSwap(lowHighSensors.highArea.tableSubviews,lowHighSensors.highArea, lowHighSensors.lowArea);

					//////RELOAD THE SUBVIEW TABLES WITH THEIR NEW SUBVIEW SETUPS
          lowHighSensors.lowArea.subviewTable.ReloadData();
          lowHighSensors.highArea.subviewTable.ReloadData();
          lowHighSensors.lowArea.subviewTable.Hidden = false;
          lowHighSensors.highArea.subviewTable.Hidden = false;
        
					/////REMOVE THE LOW SIDE ATTACHED SENSOR IF IT EXISTS
          if (lowHighSensors.lowArea.attachedSensor != null) {
          	//Console.WriteLine("low and high area attached sensor are null");
            for(int i = 0; i < 8; i++){
							if(analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == lowHighSensors.lowArea.currentSensor){
		            analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Clear;
		            analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;            
							}
						}
						lowHighSensors.lowArea.attachedSensor = null;
          }

					/////REMOVE THE HIGH SIDE ATTACHED SENSOR IF IT EXISTS
          if (lowHighSensors.highArea.attachedSensor != null) {
          	//Console.WriteLine("low and high area attached sensor are null");
            for(int i = 0; i < 8; i++){
							if(analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == lowHighSensors.highArea.currentSensor){
		            analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.Clear;
		            analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;            
							}
						}
						lowHighSensors.highArea.attachedSensor = null;
          }
          					
            //////ASSOCIATE THE LOW SIDE TO THE SWAPPING SENSOR'S INFORMATION
          if (!analyzerSensors.viewList[swap].isManual) {
          	Console.WriteLine("Low side is going to look at gauge sensor " + analyzerSensors.viewList[swap].topLabel.Text);
						lowHighSensors.lowArea.isManual = false; 
          	lowHighSensors.lowArea.manualSensor = null;
          	lowHighSensors.lowArea.currentSensor = analyzerSensors.viewList[swap].currentSensor;
          	lowHighSensors.lowArea.manifold = new Manifold(analyzerSensors.viewList[swap].currentSensor);
          	lowHighSensors.lowArea.manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
           	lowHighSensors.lowArea.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(analyzerSensors.viewList[swap].currentSensor.device.serialNumber.deviceModel);

          	var startMeasurement = SensorUtils.ToFormattedString(analyzerSensors.viewList[swap].currentSensor.type, analyzerSensors.viewList[swap].currentSensor.measurement, true).Split(' ');
          	lowHighSensors.lowArea.LabelMiddle.Text = startMeasurement[0];
          	lowHighSensors.lowArea.LabelBottom.Text = analyzerSensors.viewList[swap].currentSensor.unit.ToString();
         	
            if(analyzerSensors.viewList[swap].currentSensor.device.isConnected){
              lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");              
              lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Green;
              lowHighSensors.showView(lowHighSensors.lowArea);
            } else {
              lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");              
              lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Red;
              lowHighSensors.showView(lowHighSensors.lowArea);
            }
            
          } else {
          	Console.WriteLine("Low side is going to look at manual sensor " + analyzerSensors.viewList[swap].topLabel.Text);
						lowHighSensors.lowArea.isManual = true; 
          	lowHighSensors.lowArea.currentSensor = null;
          	lowHighSensors.lowArea.manualSensor = analyzerSensors.viewList[swap].manualSensor;
          	lowHighSensors.lowArea.manifold = new Manifold(analyzerSensors.viewList[swap].manualSensor);
          	lowHighSensors.lowArea.manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
          	lowHighSensors.lowArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
          	
          	var startMeasurement = SensorUtils.ToFormattedString(analyzerSensors.viewList[swap].manualSensor.type, analyzerSensors.viewList[swap].manualSensor.measurement, true).Split(' ');
          	lowHighSensors.lowArea.LabelMiddle.Text = startMeasurement[0];
          	lowHighSensors.lowArea.LabelBottom.Text = analyzerSensors.viewList[swap].manualSensor.unit.ToString();
            lowHighSensors.showView(lowHighSensors.lowArea);
					}

        } else {
          analyzerSensors.viewList[start].topLabel.BackgroundColor = UIColor.Clear;
          analyzerSensors.viewList[start].topLabel.TextColor = UIColor.Gray;

          if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == start.ToString()) {
            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
            lowHighSensors.hideView(lowHighSensors.lowArea);
            ion.currentAnalyzer.lowAccessibility = "low";
		        //REMOVE THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
        		Console.WriteLine("AnalyserUtilities SensorSwap moving from low manifold to empty high sensor mount");
        		ion.currentAnalyzer.lowFluid = null;
		        ion.currentAnalyzer.SetRemoteManifold(Core.Content.Analyzer.ESide.Low,null,null);
						
						if(lowHighSensors.lowArea.attachedSensor != null){
	            for (int i = 0; i < 8; i++) {
	            	
	              if (analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == lowHighSensors.lowArea.attachedSensor.currentSensor) {
	                analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
	                analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;

	              } else if (analyzerSensors.viewList[i].manualSensor != null && analyzerSensors.viewList[i].manualSensor == lowHighSensors.lowArea.attachedSensor.manualSensor) {
	                analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
	                analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
	              }
	              
	            }
						}
						lowHighSensors.highArea.currentSensor = lowHighSensors.lowArea.currentSensor;
						lowHighSensors.highArea.attachedSensor = null;
						lowHighSensors.lowArea.currentSensor = null;
						lowHighSensors.lowArea.attachedSensor = null;
						lowHighSensors.lowArea.LabelMiddle.Text = Strings.Analyzer.LOWUNDEFINED;
						lowHighSensors.hideView(lowHighSensors.lowArea);                   
          }
          if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == start.ToString()) {
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
            lowHighSensors.hideView(lowHighSensors.highArea);
            ion.currentAnalyzer.highAccessibility = "high";
		        //REMOVE THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
        		Console.WriteLine("AnalyserUtilities SensorSwap moving from high manifold to empty low manifold");
        		ion.currentAnalyzer.highFluid = null;

		        ion.currentAnalyzer.SetRemoteManifold(Core.Content.Analyzer.ESide.High,null,null);
						
						if(lowHighSensors.highArea.attachedSensor != null){
	            for (int i = 0; i < 8; i++) {
	            	
	              if (analyzerSensors.viewList[i].currentSensor != null && analyzerSensors.viewList[i].currentSensor == lowHighSensors.highArea.attachedSensor.currentSensor) {
	                analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
	                analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;

	              } else if (analyzerSensors.viewList[i].manualSensor != null && analyzerSensors.viewList[i].manualSensor == lowHighSensors.highArea.attachedSensor.manualSensor) {
	                analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
	                analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
	              }
	              
	            }
						}
						lowHighSensors.lowArea.currentSensor = lowHighSensors.highArea.currentSensor;
						lowHighSensors.lowArea.attachedSensor = null;
						lowHighSensors.highArea.currentSensor = null;
						lowHighSensors.highArea.attachedSensor = null;
						lowHighSensors.highArea.LabelMiddle.Text = Strings.Analyzer.HIGHUNDEFINED;
						lowHighSensors.hideView(lowHighSensors.highArea);           
          }

          analyzerSensors.viewList[swap].topLabel.BackgroundColor = UIColor.Clear;
          analyzerSensors.viewList[swap].topLabel.TextColor = UIColor.Gray;

          if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == swap.ToString()) {
            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
            ion.currentAnalyzer.lowAccessibility = "low";
		        //REMOVE THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
        		Console.WriteLine("AnalyserUtilities SensorSwap removing low manifold");
        		ion.currentAnalyzer.lowFluid = null;
          }
          if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == swap.ToString()) {
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
            ion.currentAnalyzer.highAccessibility = "high";
		        //REMOVE THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
        		Console.WriteLine("AnalyserUtilities SensorSwap removing high manifold");
        		ion.currentAnalyzer.highFluid = null;
          }
        }
      } else {
      	Console.WriteLine("Same side sensor mount swap. Moving mount location: " + start + " swapping mount location: " + swap + " swaplocation: " + swapLocation);
				////////////////////UPDATE THE LOW HIGH CORRESPONDING BASED SAME SIDE MOVE
				if(start.ToString() == lowHighSensors.lowArea.snapArea.AccessibilityIdentifier){
					Console.WriteLine("Low side start should now be looking at position " + swap);
					currentAnalyzer.lowAccessibility = swap.ToString();
					lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = swap.ToString();
				} else if (start.ToString() == lowHighSensors.highArea.snapArea.AccessibilityIdentifier){
					Console.WriteLine("High side start should now be looking at position " + swap);
					currentAnalyzer.highAccessibility = swap.ToString();
					lowHighSensors.highArea.snapArea.AccessibilityIdentifier = swap.ToString();
				} else if (swap.ToString() == lowHighSensors.lowArea.snapArea.AccessibilityIdentifier){
					Console.WriteLine("Low side swap should now be looking at position " + start);
					currentAnalyzer.lowAccessibility = start.ToString();
					lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = start.ToString();
				} else if (swap.ToString() == lowHighSensors.highArea.snapArea.AccessibilityIdentifier){
					Console.WriteLine("High side swap should now be looking at position " + start);
					currentAnalyzer.highAccessibility = start.ToString();
					lowHighSensors.highArea.snapArea.AccessibilityIdentifier = start.ToString();
				}
				
				Console.WriteLine("Low side associated to slot: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier + " High side associated to slot: " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
				
			}
    	Console.WriteLine("AnalyserUtilities sensorSwap ended. Low association: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier + " high association: " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
				
		}
    /// <summary>
    /// Checks if the sensor being moved is moving from low to high or vice versa and if it is associated to a low or high area
    /// </summary>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="lowHighSensors">Low high sensors.</param>
    /// <param name="position">Position.</param>
    /// <param name="touchPoint">Touch point.</param>
    /// <param name="View">View.</param>
    public static void LHSwapCheck(sensorGroup analyzerSensors,LowHighArea lowHighSensors, int position, CGPoint touchPoint, UIView View, ION.Core.Content.Analyzer currentAnalyzer){
    	Console.WriteLine("AnalyzerUtilities LHSwapCheck");
      int start = analyzerSensors.areaList.IndexOf(position);
      int swap = 0;
      bool removeLH = false;

      ////CHECK LOCATION OF SUBVIEW WHEN TOUCH ENDED TO DETERMINE INDEX PLACEMENT
      if (analyzerSensors.snapRect1.Contains (touchPoint)) {
        if (start > 3) {          
          swap = 0;
          removeLH = true;
        }
      } else if (analyzerSensors.snapRect2.Contains (touchPoint)) {
        if (start > 3) {
          swap = 1;
          removeLH = true;
        }
      } else if (analyzerSensors.snapRect3.Contains (touchPoint)) {
        if (start > 3) {
          swap = 2;
          removeLH = true;
        }
      } else if (analyzerSensors.snapRect4.Contains (touchPoint)) {
        if (start > 3) {
          swap = 3;
          removeLH = true;
        }
      } else if (analyzerSensors.snapRect5.Contains (touchPoint)) {
        if (start < 4) {
          swap = 4;
          removeLH = true;
        }
      } else if (analyzerSensors.snapRect6.Contains (touchPoint)) {
        if (start < 4) {
          swap = 5;
          removeLH = true;
        }
      } else if (analyzerSensors.snapRect7.Contains (touchPoint)) {
        if (start < 4) {
          swap = 6;
          removeLH = true;
        }
      } else if (analyzerSensors.snapRect8.Contains (touchPoint)) {
        if (start < 4) {
          swap = 7;
          removeLH = true;
        }
      }
      if (removeLH) {
        if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == start.ToString() && lowHighSensors.highArea.snapArea.AccessibilityIdentifier == swap.ToString()) {
          Console.WriteLine("occupied low area is moving to occupied high side");
          LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, View,currentAnalyzer);
        } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == start.ToString() && lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == swap.ToString()) {
          Console.WriteLine("occupied high area is moving to occupied low area");
          LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, View,currentAnalyzer);
        } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == start.ToString() || lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == start.ToString()) {
          Console.WriteLine("low or high area is starting a swap with a sensor not of the opposite peer");
          LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, View,currentAnalyzer);
        } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == swap.ToString() || lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == swap.ToString()) {
          Console.WriteLine("low or high area is a swapie with a sensor not of the opposite peer");
          LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, View,currentAnalyzer, analyzerSensors.viewList[swap]);
        } else {
        	Console.WriteLine("In the else part of swap check because it did not find a low or high association.");
          bool foundAssociation = false;
          foreach (var item in analyzerSensors.viewList){
            if (lowHighSensors.lowArea.attachedSensor != null) {
            	Console.WriteLine("Low area attached sensor is not null for sensor " + item.topLabel.Text + " " + item.bottomLabel.Text);
              if (lowHighSensors.lowArea.attachedSensor.currentSensor != null && item.currentSensor != null && lowHighSensors.lowArea.attachedSensor.currentSensor == item.currentSensor) {
                LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, View,currentAnalyzer, item);
                foundAssociation = true;
              } else if (lowHighSensors.lowArea.attachedSensor.manualSensor != null && item.manualSensor != null && lowHighSensors.lowArea.attachedSensor.manualSensor == item.manualSensor){
								Console.WriteLine("Low area attached manual sensor is not null for sensor " + item.manualSensor.name + " " + item.manualSensor.type);
                LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, View,currentAnalyzer, item);
                foundAssociation = true;
              } else {
								Console.WriteLine("low area Didn't fit in any category");   
							}
            } else if( lowHighSensors.highArea.attachedSensor != null){
							if (lowHighSensors.highArea.attachedSensor.currentSensor != null && item.currentSensor != null && lowHighSensors.highArea.attachedSensor.currentSensor == item.currentSensor){
            		Console.WriteLine("High area attached sensor is not null for sensor " + item.topLabel.Text + " " + item.bottomLabel.Text);
                LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, View,currentAnalyzer, item);
                foundAssociation = true;
							} else if (lowHighSensors.highArea.attachedSensor.manualSensor != null && item.manualSensor != null && lowHighSensors.highArea.attachedSensor.manualSensor == item.manualSensor){
								Console.WriteLine("High area attached manual sensor is not null for sensor " + item.manualSensor.name + " " + item.manualSensor.type);
                LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, View,currentAnalyzer, item);
                foundAssociation = true;
							} else {
								Console.WriteLine("high area Didn't fit in any category");
							}
						}
          }
          if (foundAssociation) {
          	Console.WriteLine("Found an association to high or low");
            return;
          } else {
          	Console.WriteLine("Didn't find an association to high or low");
            sensorSwap(analyzerSensors, lowHighSensors, position, touchPoint, View, currentAnalyzer);
          }
        }
      } else {
      	Console.WriteLine("Skipped the removeLH");
        sensorSwap (analyzerSensors, lowHighSensors, position, touchPoint, View, currentAnalyzer);
      }
    }
    /// <summary>
    /// Creates an alert to confirm a swap if there are any high low associations
    /// </summary>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="lowHighSensors">Low high sensors.</param>
    /// <param name="position">Position.</param>
    /// <param name="touchPoint">Touch point.</param>
    /// <param name="View">View.</param>
    public static void LHSwapAlert(sensorGroup analyzerSensors,LowHighArea lowHighSensors, int position, CGPoint touchPoint, UIView View, ION.Core.Content.Analyzer currentAnalyzer, sensor item = null){
    	//Console.WriteLine("AnalyzerUtilities LHSwapAlert");
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }

      UIAlertController addDeviceSheet; 

      addDeviceSheet = UIAlertController.Create(Util.Strings.Analyzer.ACTION, Util.Strings.Analyzer.REMOVESETUP, UIAlertControllerStyle.Alert);
      addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {

				/////DISASSOCIATE THE LOW OR HIGH AREA WITH A SENSOR BEING SWAPPED TO ANOTHER SIDE
        if(item != null){
        	Console.WriteLine("sent a sensor item");
        	if(item.currentSensor != null){
        		////CHECK IF SENT GAUGE SENSOR IS ASSOCIATED TO THE LOW OR HIGH AREAS
						if(lowHighSensors.lowArea.currentSensor == item.currentSensor){
        			Console.WriteLine("low area gauge sensor matches sent gauge sensor");
							lowHighSensors.lowArea.LabelMiddle.Text = Strings.Analyzer.LOWUNDEFINED;
							lowHighSensors.hideView(lowHighSensors.lowArea);
						} else if (lowHighSensors.highArea.currentSensor == item.currentSensor){
        			Console.WriteLine("high area gauge sensor matches sent gauge sensor");
							lowHighSensors.lowArea.LabelMiddle.Text = Strings.Analyzer.HIGHUNDEFINED;
							lowHighSensors.hideView(lowHighSensors.highArea);
						}
        		////CHECK IF SENT SENSOR IS THE SECONDAY SENSOR FOR THE LOW OR HIGH AREA
        		if(lowHighSensors.lowArea.attachedSensor != null && lowHighSensors.lowArea.attachedSensor.currentSensor != null && lowHighSensors.lowArea.attachedSensor.currentSensor == item.currentSensor){
        			Console.WriteLine("low area attached gauge sensor matches sent gauge sensor");
        			lowHighSensors.lowArea.attachedSensor = null;
        			lowHighSensors.lowArea.manifold.SetSecondarySensor(null);
						}else if (lowHighSensors.highArea.attachedSensor != null && lowHighSensors.highArea.attachedSensor.currentSensor != null && lowHighSensors.highArea.attachedSensor.currentSensor == item.currentSensor){						
        			Console.WriteLine("low area attached gauge sensor matches sent gauge sensor");
        			lowHighSensors.highArea.attachedSensor = null;
        			lowHighSensors.highArea.manifold.SetSecondarySensor(null);
						}
					} else {
        		////CHECK IF SENT MANUAL SENSOR IS ASSOCIATED TO THE LOW OR HIGH AREAS
						if(lowHighSensors.lowArea.manualSensor == item.manualSensor){
        			Console.WriteLine("low area manual sensor matches sent manual sensor");
							lowHighSensors.lowArea.LabelMiddle.Text = Strings.Analyzer.LOWUNDEFINED;
							lowHighSensors.hideView(lowHighSensors.lowArea);
						} else if (lowHighSensors.highArea.manualSensor == item.manualSensor){
        			Console.WriteLine("high area manual sensor matches sent manual sensor");
							lowHighSensors.lowArea.LabelMiddle.Text = Strings.Analyzer.HIGHUNDEFINED;
							lowHighSensors.hideView(lowHighSensors.highArea);
						}
        		////CHECK IF SENT SENSOR IS THE SECONDAY SENSOR FOR THE LOW OR HIGH AREA
        		if(lowHighSensors.lowArea.attachedSensor != null && lowHighSensors.lowArea.attachedSensor.manualSensor != null && lowHighSensors.lowArea.attachedSensor.manualSensor == item.manualSensor){
        			Console.WriteLine("low area attached manual sensor matches sent manual sensor");
        			lowHighSensors.lowArea.attachedSensor = null;        		
        			lowHighSensors.lowArea.manifold.SetSecondarySensor(null);
						}else if (lowHighSensors.highArea.attachedSensor != null && lowHighSensors.highArea.attachedSensor.manualSensor != null && lowHighSensors.highArea.attachedSensor.manualSensor == item.manualSensor){
        			Console.WriteLine("low area attached manual sensor matches sent manual sensor");
        			lowHighSensors.highArea.attachedSensor = null;						
        			lowHighSensors.highArea.manifold.SetSecondarySensor(null);
						}
					}
        }
        sensorSwap (analyzerSensors, lowHighSensors, position, touchPoint, View, currentAnalyzer);
      }));
      addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {
        confirmLayout(analyzerSensors, View);
      }));
      vc.DismissViewController(false, null);
      vc.PresentViewController (addDeviceSheet, true, null);
       confirmLayout(analyzerSensors, View);
    }
    /// <summary>
    /// Ensures the sensors are in their correct placement after swapping
    /// UIDYNAMICBEHAVIOR SUCKS
    /// </summary>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="View">View.</param>
    public static void confirmLayout(sensorGroup analyzerSensors, UIView View){
    	var analyzer = AppState.context.currentAnalyzer;
   	
   //		Console.WriteLine("revert list");
   //		foreach(var spot in analyzer.revertPositions){
			//	Console.Write(spot + " ");
			//}
			//Console.WriteLine(Environment.NewLine);
   //		Console.WriteLine("area list");
   //		foreach(var spot in analyzerSensors.areaList){
			//	Console.Write(spot + " ");
			//}
			//Console.WriteLine(Environment.NewLine);
   //		Console.WriteLine("position list");
   //		foreach(var spot in analyzer.sensorPositions){
			//	Console.Write(spot + " ");
			//}
			//Console.WriteLine(Environment.NewLine);
   //		Console.WriteLine("view list");
   //		for(int v = 0; v < analyzerSensors.viewList.Count; v++){
   //			if(Convert.ToInt32(analyzerSensors.viewList[v].snapArea.AccessibilityIdentifier) != analyzer.sensorPositions[v]){
			//		Console.WriteLine("View area " + analyzer.sensorPositions[v] + " should be in the " +v +" spot but " + analyzerSensors.viewList[v].snapArea.AccessibilityIdentifier + " is instead");
			//	}
			//	Console.Write(analyzerSensors.viewList[v].snapArea.AccessibilityIdentifier + " ");
			//}
			//Console.WriteLine(Environment.NewLine);
      ////MOVE SENSORS BASED ON THEIR LOCATION
      for (int i = 0; i < 8; i++) {
      	
        if (analyzer.sensorPositions [i] == 1) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{
              analyzerSensors.snapArea1.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (analyzer.sensorPositions [i] == 2) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea2.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (analyzer.sensorPositions [i] == 3) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea3.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (analyzer.sensorPositions[i] == 4) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea4.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (analyzer.sensorPositions[i] == 5) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea5.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (analyzer.sensorPositions[i] == 6) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea6.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (analyzer.sensorPositions[i] == 7) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea7.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (analyzer.sensorPositions[i] == 8) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea8.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        }
      }
    }
		/// <summary>
		/// TRIGGERS AN ALERT TO ASK IF THE USER WANTS TO REPLACE THE CURRENT LOW OR HIGH AREA INFORMATION WITH ANOTHER SENSOR'S DATA
		/// </summary>
		/// <param name="title">HEADER FOR THE ALERT</param>
		/// <param name="message">INSTRUCTIONS FOR THE ALERT</param>
		/// <param name="type">HOW THE ALERT IS BEING REPLACED 1 = OCCUPIED LOW SIDE GIVEN UNATTACHED SENSOR 2 = OCCUPIED HIGH SIDE GIVEN UNATTACHED SENSOR 3 = MOVING HIGH SENSOR TO LOW SIDE 4 = MOVING LOW SENSOR TO HIGH SIDE</param>
		/// <param name="topLabel">NEW SENSOR NAME LABEL</param>
		/// <param name="middleLabel">NEW SENSOR READING LABEL</param>
		/// <param name="bottomLabel">NEW SENSOR MEASUREMENT TYPE LABEL</param>
		/// <param name="origin">IDENTIFIER FOR FROM NEW SENSOR TO MATCH LOW/HIGH SIDE</param>
		/// <param name="removeLabel">OLD SENSOR REMOVING ITS BACKGROUND COLOR</param>
		/// <param name="tableArea">SUBVIEW TABLEVIEW TO REMOVE ELEMENTS</param>
     public static void replaceAlert(string message, int type, sensor Sensor, sensor removeSensor, lowHighSensor lowHighSensor, sensorGroup analyzerSensors, UIView View, LowHighArea lowHigh){
     	Console.WriteLine("AnalyzerUtilities replaceAlert of type " + type);
     	///Don't allow temperature sensors to be the main focus of the low/high viewer area
			if(Sensor.isManual){
				if(Sensor.manualSensor.type == ESensorType.Temperature){
					if(removeSensor.isManual){
						if(removeSensor.manualSensor.type != ESensorType.Pressure){
							return;
						}
					} else {
						if(removeSensor.currentSensor.type != ESensorType.Pressure){
							return;
						}
					}
				}
			} else {
				if(Sensor.currentSensor.type == ESensorType.Temperature){
					if(removeSensor.isManual){
						if(removeSensor.manualSensor.type != ESensorType.Pressure){
							return;
						}
					} else {
						if(removeSensor.currentSensor.type != ESensorType.Pressure){
							return;
						}
					}
				}
			}
			
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }

      if (Sensor.isManual.Equals(true) || removeSensor.isManual.Equals(true)) {
      	//Console.WriteLine("One of the sensors is manual");
        bool spotOpen = secondarySlotSpot(Sensor, removeSensor,analyzerSensors, type);

        if (Sensor.isManual.Equals(true) && removeSensor.isManual.Equals(true)) {         
          if (Sensor.manualSensor.type == removeSensor.manualSensor.type) {
            spotOpen = true;
          } else {
            if (Sensor.manualSensor.type == ESensorType.Vacuum || removeSensor.manualSensor.type == ESensorType.Vacuum) {              
            } else {
              message = string.Format(Util.Strings.Analyzer.ADDSECONDARY,Sensor.manualSensor.type.ToString(),removeSensor.manualSensor.type.ToString());
            }
          }
        } else if (Sensor.isManual.Equals(true) && removeSensor.isManual.Equals(false)) {
          if(Sensor.manualSensor.type == removeSensor.currentSensor.type){
            spotOpen = true;
          }else{
            if(Sensor.manualSensor.type == ESensorType.Vacuum || removeSensor.currentSensor.type == ESensorType.Vacuum){
            } else {
              message = string.Format(Util.Strings.Analyzer.ADDSECONDARY,Sensor.manualSensor.type.ToString(),removeSensor.currentSensor.type.ToString());
            }
          }
        } else if (Sensor.isManual.Equals(false) && removeSensor.isManual.Equals(true)) {         
          if (Sensor.currentSensor.type == removeSensor.manualSensor.type) {  
            spotOpen = true;
          } else {
            if(Sensor.currentSensor.type == ESensorType.Vacuum || removeSensor.manualSensor.type == ESensorType.Vacuum){
            } else {
              message = string.Format(Util.Strings.Analyzer.ADDSECONDARY,Sensor.currentSensor.type.ToString(),removeSensor.manualSensor.type.ToString());
            }
          }
        }

        if (spotOpen.Equals(false)) {
        	//Console.WriteLine("First same side");
          UIAlertController noneAvailable;
          noneAvailable = UIAlertController.Create(Util.Strings.Analyzer.CANTADD, Util.Strings.Analyzer.SAMESIDE, UIAlertControllerStyle.Alert);
          noneAvailable.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));
          vc.PresentViewController(noneAvailable, true, null);
          return;
        }
        UIAlertController addDeviceSheet;

        addDeviceSheet = UIAlertController.Create(Util.Strings.Analyzer.ACTION, message, UIAlertControllerStyle.Alert);
        addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
 
          if (Sensor.isManual.Equals(true) && removeSensor.isManual.Equals(true)) {
            Console.WriteLine("Both sensors are manual");
            if (removeSensor.manualSensor.type == ESensorType.Pressure && Sensor.manualSensor.type == ESensorType.Temperature) {
              //Console.WriteLine("low high sensor is pressure and adding sensor is temperature");
              Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
              Sensor.topLabel.TextColor = UIColor.White;

            	lowHighSensor.attachedSensor = Sensor;
            	lowHighSensor.manifold.SetSecondarySensor(Sensor.manualSensor);

            } else {
              //Console.WriteLine("dealing with a vacuum sensor");
              if(type == 1){
                replaceLowUnattached(Sensor, removeSensor, lowHighSensor, analyzerSensors, View);
              } else if (type == 2){
                replaceHighUnattached(Sensor, removeSensor, lowHighSensor, analyzerSensors, View);
              } else if (type == 3) {
                replaceLowAttached(Sensor, removeSensor, lowHighSensor, lowHigh.highArea, analyzerSensors, View);
              } else if (type ==4){
                replaceHighAttached(Sensor, removeSensor, lowHighSensor, lowHigh.lowArea, analyzerSensors, View);
              }
            }
          } else if (Sensor.isManual.Equals(true) && removeSensor.isManual.Equals(false)) {
            Console.WriteLine("low high sensor is manual and adding sensor is not");
            if (removeSensor.currentSensor.type == ESensorType.Pressure && Sensor.manualSensor.type == ESensorType.Temperature) {
              Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
              Sensor.topLabel.TextColor = UIColor.White;
              lowHighSensor.attachedSensor = Sensor;
              lowHighSensor.manifold.SetSecondarySensor(Sensor.manualSensor);
            }else {
            	//Console.WriteLine("calling the replace methods");		
              if(type == 1){
                replaceLowUnattached(Sensor, removeSensor, lowHighSensor, analyzerSensors, View);
              } else if (type == 2){
                replaceHighUnattached(Sensor, removeSensor, lowHighSensor, analyzerSensors, View);
              } else if (type == 3) {
                replaceLowAttached(Sensor, removeSensor, lowHighSensor, lowHigh.highArea, analyzerSensors, View);
              } else if (type ==4){
                replaceHighAttached(Sensor, removeSensor, lowHighSensor, lowHigh.lowArea, analyzerSensors, View);
              }
            }
          } else if (Sensor.isManual.Equals(false) && removeSensor.isManual.Equals(true)) {
            Console.WriteLine("low high sensor is not manual and adding sensor is manual");
            if (removeSensor.manualSensor.type == ESensorType.Pressure && Sensor.currentSensor.type == ESensorType.Temperature) {
              Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
              Sensor.topLabel.TextColor = UIColor.White;
              lowHighSensor.attachedSensor = Sensor;
              lowHighSensor.manifold.SetSecondarySensor(Sensor.currentSensor);
            } else {
            	//Console.WriteLine("calling the replace methods");		
              if(type == 1){
                replaceLowUnattached(Sensor, removeSensor, lowHighSensor, analyzerSensors, View);
              } else if (type == 2){
                replaceHighUnattached(Sensor, removeSensor, lowHighSensor, analyzerSensors, View);
              } else if (type == 3) {
                replaceLowAttached(Sensor, removeSensor, lowHighSensor, lowHigh.highArea, analyzerSensors, View);
              } else if (type ==4){
                replaceHighAttached(Sensor, removeSensor, lowHighSensor, lowHigh.lowArea, analyzerSensors, View);
              }
            }
          }
        }));
        addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));

        vc.PresentViewController(addDeviceSheet, true, null);
      } else {
				//Console.WriteLine("neither of the sensors are manual");
        if ( removeSensor.currentSensor != null && removeSensor.currentSensor.type == ION.Core.Sensors.ESensorType.Pressure && Sensor.currentSensor.type == ION.Core.Sensors.ESensorType.Temperature) {
          var spotOpen = secondarySlotSpot(Sensor, removeSensor,analyzerSensors, type);
          if (spotOpen.Equals(false)) {
            UIAlertController noneAvailable;
            noneAvailable = UIAlertController.Create(Util.Strings.Analyzer.CANTADD, Util.Strings.Analyzer.SAMESIDE, UIAlertControllerStyle.Alert);
            noneAvailable.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));
            vc.PresentViewController(noneAvailable, true, null);
            return;
          }
          message = Util.Strings.Analyzer.ADDTEMP;
        }

        UIAlertController addDeviceSheet;

        addDeviceSheet = UIAlertController.Create(Util.Strings.Analyzer.ACTION, message, UIAlertControllerStyle.Alert);

        addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
        ///REMOVE THE ATTACHED SENSORS
				removeAttachedSensor(lowHighSensor, analyzerSensors);
					
          if (removeSensor.currentSensor != null && removeSensor.currentSensor.type == ION.Core.Sensors.ESensorType.Pressure && Sensor.currentSensor.type == ION.Core.Sensors.ESensorType.Temperature) {
            //Console.WriteLine("Adding temp device " + Sensor.currentSensor.device.name + "'s sensor as device " + removeSensor.currentSensor.device.name + "'s secondary sensor");
            Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
            Sensor.topLabel.TextColor = UIColor.White;
						Console.WriteLine("Setting attached sensor for " + lowHighSensor.LabelTop.Text);
            	lowHighSensor.attachedSensor = Sensor;
          		lowHighSensor.manifold.SetSecondarySensor(Sensor.currentSensor);

          } else {
            if (type == 1) {         
                replaceLowUnattached(Sensor,removeSensor,lowHighSensor,analyzerSensors,View);            
            } else if (type == 2) {
                replaceHighUnattached(Sensor,removeSensor,lowHighSensor,analyzerSensors,View);           
            } else if (type == 3) {
                replaceLowAttached(Sensor,removeSensor,lowHighSensor, lowHigh.highArea, analyzerSensors,View);           
            } else if (type == 4) {
                replaceHighAttached(Sensor,removeSensor,lowHighSensor, lowHigh.lowArea, analyzerSensors,View);
            }
          }
        }));

        addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));

        vc.PresentViewController(addDeviceSheet, true, null);
      }
		}
	/// <summary>
    /// SETS THE LOW OR HIGH AREAS TO MONITOR THE SENSOR THAT WAS DROPPED ON IT
  /// </summary>
  /// <param name="touchPoint">Location on the screen where single sensor was dropped</param>
  /// <param name="Sensor">Single sensor used to update low high sensor</param>
  /// <param name="lowHighSensors">Low high sensors being updated</param>
  /// <param name="analyzerSensors">Collection of single sensor information</param>
  /// <param name="View">View.</param>
  /// HOW THE ALERT IS BEING REPLACED 1 = OCCUPIED LOW SIDE GIVEN UNATTACHED SENSOR 2 = OCCUPIED HIGH SIDE GIVEN UNATTACHED SENSOR 3 = MOVING HIGH SENSOR TO LOW SIDE 4 = MOVING LOW SENSOR TO HIGH SIDE</param>
    public static void updateLowHighArea(CGPoint touchPoint, sensor Sensor, LowHighArea lowHighSensors, sensorGroup analyzerSensors,UIView View){
    	Console.WriteLine("AnalyzerUtilities updateLowHighArea. Low area: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier + " high area: " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }
      if( lowHighSensors.lowArea.areaRect.Contains(touchPoint)){
      	Console.WriteLine("Dragged to low area. Low area accessibility identifier: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier + " high area accessiblity identifier " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
        if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList.IndexOf(Sensor).ToString()) {
        	Console.WriteLine("high area was originally attached to this sensor");
          if(!freeSpot(analyzerSensors,Sensor,"low")){
            UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

            fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

            vc.PresentViewController (fullPopup, true, null);
          } else {
            string message = Util.Strings.Analyzer.HIGHLOST;

  					if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "0") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[0], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "1") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[1], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "2") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[2], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "3") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[3], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "4") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[4], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "5") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[5], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "6") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[6], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "7") {
              replaceAlert (message, 3, Sensor, analyzerSensors.viewList[7], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "low") {
              UIAlertController switchSide = UIAlertController.Create (Util.Strings.Analyzer.ACTION, Util.Strings.Analyzer.HIGHLOST, UIAlertControllerStyle.Alert);

              switchSide.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
              	Console.WriteLine("Changed ok");
                var goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)),"low",View);
                if (goOn) {
				          var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
                
			            if(Sensor.currentSensor != null){
			            	Sensor.currentSensor.analyzerSlot = areaIndex;
			            	lowHighSensors.lowArea.currentSensor = Sensor.currentSensor;
			            	lowHighSensors.lowArea.manifold = new Manifold(Sensor.currentSensor);
			            	lowHighSensors.lowArea.LabelTop.Text = Sensor.currentSensor.name;
			            	lowHighSensors.lowArea.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(Sensor.currentSensor.device.serialNumber.deviceModel);
						      	var startMeasurement = SensorUtils.ToFormattedString(Sensor.currentSensor.type, Sensor.currentSensor.measurement, true).Split(' ');
			            	lowHighSensors.lowArea.LabelMiddle.Text = startMeasurement[0];
			            	lowHighSensors.lowArea.LabelBottom.Text = Sensor.currentSensor.unit.ToString();
			            } else {
			            	Sensor.manualSensor.analyzerSlot = areaIndex;
			            	lowHighSensors.lowArea.isManual = true;
			            	lowHighSensors.lowArea.manualGType = Sensor.topLabel.Text;
			            	lowHighSensors.lowArea.manualSensor = Sensor.manualSensor;
			            	lowHighSensors.lowArea.manifold = new Manifold(Sensor.manualSensor);
			            	lowHighSensors.lowArea.LabelTop.Text = Sensor.topLabel.Text;
			            	lowHighSensors.lowArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
						      	var startMeasurement = SensorUtils.ToFormattedString(Sensor.manualSensor.type, Sensor.manualSensor.measurement, true).Split(' ');
			            	lowHighSensors.lowArea.LabelMiddle.Text = startMeasurement[0];
			            	lowHighSensors.lowArea.LabelBottom.Text = Sensor.manualSensor.unit.ToString();
			            	lowHighSensors.lowArea.DeviceImage.Hidden = true;
									}                
                                
                	lowHighSensors.lowArea.manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);

			            lowHighSensors.lowArea.LabelSubview.Text = " " + lowHighSensors.lowArea.LabelTop.Text + Util.Strings.Analyzer.LHTABLE;   
									lowHighSensors.highArea.tableSubviews.Clear();
			            
			            if(Sensor.currentSensor != null && Sensor.currentSensor.device.isConnected){
										lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
										lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Green;
									} else if (Sensor.currentSensor != null && !Sensor.currentSensor.device.isConnected){
										lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
										lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Red;
									} else {
										lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Clear;
										lowHighSensors.lowArea.Connection.Hidden = true;
										lowHighSensors.lowArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
									}            
			            
			            lowHighSensors.showView(lowHighSensors.lowArea);
                
				          ///////////////////// change the high manifold to be based on moved location instead of created position
				          Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change highAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
				          
				          ion.currentAnalyzer.lowAccessibility = areaIndex.ToString();
                  lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = areaIndex.ToString();				          
				          /////////////////////                     
                  
                  lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
                  ion.currentAnalyzer.highAccessibility = "high";                  
                  //SET THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
        					//Console.WriteLine("AnalyserUtilities updateLowHigh 4 high set to " + Sensor.lowArea.manifold.ptChart.fluid.name + " low removed");
                  ion.currentAnalyzer.lowFluid =  lowHighSensors.highArea.manifold.ptChart.fluid;
                  ion.currentAnalyzer.highFluid = null;
									ion.currentAnalyzer.lowSubviews = lowHighSensors.lowArea.tableSubviews;            
                  
                  Sensor.topLabel.BackgroundColor = UIColor.Blue;
                  Sensor.topLabel.TextColor = UIColor.White;
               	  
									lowHighSensors.highArea.currentSensor = null;
                	lowHighSensors.highArea.manifold = null;
                	lowHighSensors.highArea.LabelMiddle.Text = Strings.Analyzer.HIGHUNDEFINED;
                	lowHighSensors.hideView(lowHighSensors.highArea);                  
                }
              }));            
              switchSide.AddAction(UIAlertAction.Create(Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));
              vc.PresentViewController (switchSide, true, null);
            }
          }
				} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier != "low") {
					Console.WriteLine("low area was not empty ");
          if(!freeSpot(analyzerSensors,Sensor,"low")){
            UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

            fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

            vc.PresentViewController (fullPopup, true, null);
          } else {
            string message = Util.Strings.Analyzer.LOWLOST;
            
            var lowStart = Convert.ToInt32(lowHighSensors.lowArea.snapArea.AccessibilityIdentifier);
            
            if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "0" && analyzerSensors.viewList.IndexOf(Sensor) != 0) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);					
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "1" && analyzerSensors.viewList.IndexOf(Sensor) != 1) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "2" && analyzerSensors.viewList.IndexOf(Sensor) != 2) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "3" && analyzerSensors.viewList.IndexOf(Sensor) != 3) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "4" && analyzerSensors.viewList.IndexOf(Sensor) != 4) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "5" && analyzerSensors.viewList.IndexOf(Sensor) != 5) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "6" && analyzerSensors.viewList.IndexOf(Sensor) != 6) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "7" && analyzerSensors.viewList.IndexOf(Sensor) != 7) {
              replaceAlert (message, 1, Sensor, analyzerSensors.viewList[lowStart], lowHighSensors.lowArea, analyzerSensors, View, lowHighSensors);
            }
          }

				} else {
					Console.WriteLine("Low side was empty and moving sensor was not attached to the high area");
          if(Sensor.isManual){
						if(Sensor.manualSensor.type == ESensorType.Temperature){
							UIAlertController tempAlert = UIAlertController.Create (Util.Strings.Analyzer.SETUP, Util.Strings.Analyzer.SETUPPRESSURE,UIAlertControllerStyle.Alert);
							tempAlert.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Cancel,null));
							vc.PresentViewController(tempAlert,true,null);
							return;
						}
					} else {
						if(Sensor.currentSensor.type == ESensorType.Temperature){
							UIAlertController tempAlert = UIAlertController.Create (Util.Strings.Analyzer.SETUP, Util.Strings.Analyzer.SETUPPRESSURE, UIAlertControllerStyle.Alert);
							tempAlert.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Cancel,null));
							vc.PresentViewController(tempAlert,true,null);
							return;
						}
					}
          bool goOn = orderSensors (analyzerSensors, analyzerSensors.areaList.IndexOf (Convert.ToInt32 (Sensor.snapArea.AccessibilityIdentifier)), "low", View);
					if (goOn) {
	          var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
	          
            Sensor.topLabel.BackgroundColor = UIColor.Blue;
            Sensor.topLabel.TextColor = UIColor.White;

            if(Sensor.currentSensor != null){
            	Sensor.currentSensor.analyzerSlot = areaIndex;
            	lowHighSensors.lowArea.currentSensor = Sensor.currentSensor;
            	lowHighSensors.lowArea.manifold = new Manifold(Sensor.currentSensor);
            	lowHighSensors.lowArea.LabelTop.Text = Sensor.currentSensor.name;
            	lowHighSensors.lowArea.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(Sensor.currentSensor.device.serialNumber.deviceModel);
			      	var startMeasurement = SensorUtils.ToFormattedString(Sensor.currentSensor.type, Sensor.currentSensor.measurement, true).Split(' ');
            	lowHighSensors.lowArea.LabelMiddle.Text = startMeasurement[0];
            	lowHighSensors.lowArea.LabelBottom.Text = Sensor.currentSensor.unit.ToString();
            } else {
            	Sensor.manualSensor.analyzerSlot = areaIndex;
            	lowHighSensors.lowArea.isManual = true;
            	lowHighSensors.lowArea.manualGType = Sensor.manualSensor.type.ToString();
            	lowHighSensors.lowArea.manualSensor = Sensor.manualSensor;
            	lowHighSensors.lowArea.manifold = new Manifold(Sensor.manualSensor);
            	lowHighSensors.lowArea.LabelTop.Text =  Sensor.topLabel.Text;
            	lowHighSensors.lowArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
			      	var startMeasurement = SensorUtils.ToFormattedString(Sensor.manualSensor.type, Sensor.manualSensor.measurement, true).Split(' ');
            	lowHighSensors.lowArea.LabelMiddle.Text = startMeasurement[0];
            	lowHighSensors.lowArea.LabelBottom.Text = Sensor.manualSensor.unit.ToString();
            	lowHighSensors.lowArea.DeviceImage.Hidden = true;
						}
            
            lowHighSensors.lowArea.manifold.ptChart = PTChart.New(ion,Fluid.EState.Dew);
            lowHighSensors.lowArea.LabelSubview.Text = " " + lowHighSensors.lowArea.LabelTop.Text + Util.Strings.Analyzer.LHTABLE;           
            
            if(Sensor.currentSensor != null && Sensor.currentSensor.device.isConnected){
							lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
							lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Green;
						} else if (Sensor.currentSensor != null && !Sensor.currentSensor.device.isConnected){
							lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
							lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Red;
						} else {
							lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Clear;
							lowHighSensors.lowArea.Connection.Hidden = true;
							lowHighSensors.lowArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
						}            
            
            lowHighSensors.showView(lowHighSensors.lowArea);

	          ///////////////////// change the low manifold to be based on moved location instead of created position
	          Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change lowAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
	          ion.currentAnalyzer.lowAccessibility = areaIndex.ToString();
            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = areaIndex.ToString();	          
	          /////////////////////            
            ion.currentAnalyzer.lowSubviews = lowHighSensors.lowArea.tableSubviews;
            //SET THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
        		Console.WriteLine("AnalyserUtilities updateLowHigh 3/1 else low set");
        		ion.currentAnalyzer.lowFluid = lowHighSensors.lowArea.manifold.ptChart.fluid;

            return;
					}

          UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

          fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

          vc.PresentViewController (fullPopup, true, null);
				}

			} else if (lowHighSensors.highArea.areaRect.Contains (touchPoint)){
				Console.WriteLine("Dragged to high area. Low area: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier +  " high area: " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
        if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList.IndexOf(Sensor).ToString()) {
        	Console.WriteLine("sensor was associated to the low area");
          if(!freeSpot(analyzerSensors,Sensor,"high")){
            UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

            fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

            vc.PresentViewController (fullPopup, true, null);
          } else {
            string message = Util.Strings.Analyzer.LOWLOST;
            if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "0") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[0], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);					
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "1") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[1], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "2") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[2], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "3") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[3], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "4") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[4], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "5") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[5], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "6") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[6], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "7") {
              replaceAlert(message, 4, Sensor, analyzerSensors.viewList[7], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "high") {
            	Console.WriteLine("High side was empty");
              UIAlertController switchSide = UIAlertController.Create (Util.Strings.Analyzer.ACTION, Util.Strings.Analyzer.LOWLOST, UIAlertControllerStyle.Alert);

              switchSide.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
                var goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)),"high",View);
                if (goOn) {
				          var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
                
			            if(Sensor.currentSensor != null){
			            	Sensor.currentSensor.analyzerSlot = areaIndex;
			            	lowHighSensors.highArea.currentSensor = Sensor.currentSensor;
			            	lowHighSensors.highArea.manifold = new Manifold(Sensor.currentSensor);
			            	lowHighSensors.highArea.LabelTop.Text = Sensor.currentSensor.name;
			            	lowHighSensors.highArea.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(Sensor.currentSensor.device.serialNumber.deviceModel);
						      	var startMeasurement = SensorUtils.ToFormattedString(Sensor.currentSensor.type, Sensor.currentSensor.measurement, true).Split(' ');
			            	lowHighSensors.highArea.LabelMiddle.Text = startMeasurement[0];
			            	lowHighSensors.highArea.LabelBottom.Text = Sensor.currentSensor.unit.ToString();
			            } else {
			            	Sensor.manualSensor.analyzerSlot = areaIndex;
			            	lowHighSensors.highArea.isManual = true;
			            	lowHighSensors.highArea.manualGType = Sensor.manualSensor.type.ToString();
			            	lowHighSensors.highArea.manualSensor = Sensor.manualSensor;
			            	lowHighSensors.highArea.manifold = new Manifold(Sensor.manualSensor);
			            	lowHighSensors.highArea.LabelTop.Text =  Sensor.topLabel.Text;
			            	lowHighSensors.highArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
						      	var startMeasurement = SensorUtils.ToFormattedString(Sensor.manualSensor.type, Sensor.manualSensor.measurement, true).Split(' ');
			            	lowHighSensors.highArea.LabelMiddle.Text = startMeasurement[0]; 
			            	lowHighSensors.highArea.LabelBottom.Text = Sensor.manualSensor.unit.ToString();
			            	lowHighSensors.highArea.DeviceImage.Hidden = true;
									}                
                
                	lowHighSensors.highArea.manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);

			            lowHighSensors.highArea.LabelSubview.Text = " " + lowHighSensors.highArea.LabelTop.Text + Util.Strings.Analyzer.LHTABLE;   
									lowHighSensors.lowArea.tableSubviews.Clear();
			            
			            if(Sensor.currentSensor != null && Sensor.currentSensor.device.isConnected){
										lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
										lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Green;
									} else if (Sensor.currentSensor != null && !Sensor.currentSensor.device.isConnected){
										lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
										lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Red;
									} else {
										lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Clear;
										lowHighSensors.highArea.Connection.Hidden = true;
										lowHighSensors.highArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
									}            
			            
			            lowHighSensors.showView(lowHighSensors.highArea);
                
				          ///////////////////// change the high manifold to be based on moved location instead of created position
				          Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change highAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
				          ion.currentAnalyzer.highAccessibility = areaIndex.ToString();
                  lowHighSensors.highArea.snapArea.AccessibilityIdentifier = areaIndex.ToString();				          
				          /////////////////////                     
                  
                  lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
                  ion.currentAnalyzer.lowAccessibility = "low";                  
                  //SET THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
        					//Console.WriteLine("AnalyserUtilities updateLowHigh 4 high set to " + Sensor.lowArea.manifold.ptChart.fluid.name + " low removed");
									ion.currentAnalyzer.highFluid =  lowHighSensors.lowArea.manifold.ptChart.fluid;
                  ion.currentAnalyzer.lowFluid = null;                  
                  ion.currentAnalyzer.highSubviews = lowHighSensors.highArea.tableSubviews;
                  
                  Sensor.topLabel.BackgroundColor = UIColor.Red;
                  Sensor.topLabel.TextColor = UIColor.White;
               	  
									lowHighSensors.lowArea.currentSensor = null;
                	lowHighSensors.lowArea.manifold = null;
                	lowHighSensors.lowArea.LabelMiddle.Text = Strings.Analyzer.LOWUNDEFINED;
                	lowHighSensors.hideView(lowHighSensors.lowArea);                  
                }
              }));

              switchSide.AddAction(UIAlertAction.Create(Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));
              vc.PresentViewController (switchSide, true, null);

            } 
          }
				} else if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier != "high"){
					Console.WriteLine("High area was not empty and has a sensor attached");
          if (!freeSpot(analyzerSensors,Sensor, "high")) {
            UIAlertController fullPopup = UIAlertController.Create(Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

            fullPopup.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
            }));

            vc.PresentViewController(fullPopup, true, null);
          } else {
            string message = Util.Strings.Analyzer.HIGHLOST;

            var highStart = Convert.ToInt32(lowHighSensors.highArea.snapArea.AccessibilityIdentifier);

            if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "0" && analyzerSensors.viewList.IndexOf(Sensor) != 0) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);					
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "1" && analyzerSensors.viewList.IndexOf(Sensor) != 1) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "2" && analyzerSensors.viewList.IndexOf(Sensor) != 2) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "3" && analyzerSensors.viewList.IndexOf(Sensor) != 3) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "4" && analyzerSensors.viewList.IndexOf(Sensor) != 4) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "5" && analyzerSensors.viewList.IndexOf(Sensor) != 5) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "6" && analyzerSensors.viewList.IndexOf(Sensor) != 6) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "7" && analyzerSensors.viewList.IndexOf(Sensor) != 7) {
              replaceAlert(message, 2, Sensor, analyzerSensors.viewList[highStart], lowHighSensors.highArea, analyzerSensors, View, lowHighSensors);
            } 
          }
				} else {
					Console.WriteLine("High area is empty and sensor was not attached to low area");
          if(Sensor.isManual){
						if(Sensor.manualSensor.type == ESensorType.Temperature){
							UIAlertController tempAlert = UIAlertController.Create (Util.Strings.Analyzer.SETUP, Util.Strings.Analyzer.SETUPPRESSURE, UIAlertControllerStyle.Alert);
							tempAlert.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Cancel,null));
							vc.PresentViewController(tempAlert,true,null);
							return;
						}
					} else {
						if(Sensor.currentSensor.type == ESensorType.Temperature){
							UIAlertController tempAlert = UIAlertController.Create (Util.Strings.Analyzer.SETUP, Util.Strings.Analyzer.SETUPPRESSURE, UIAlertControllerStyle.Alert);
							tempAlert.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Cancel,null));
							vc.PresentViewController(tempAlert,true,null);
							return;
						}
					}
          bool goOn = orderSensors (analyzerSensors, analyzerSensors.areaList.IndexOf (Convert.ToInt32 (Sensor.snapArea.AccessibilityIdentifier)), "high", View);
					if (goOn) {
	          var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
	          
            Sensor.topLabel.BackgroundColor = UIColor.Red;
            Sensor.topLabel.TextColor = UIColor.White;
            
             if(Sensor.currentSensor != null){
             	Sensor.currentSensor.analyzerSlot = areaIndex;
             	lowHighSensors.highArea.isManual = false;
            	lowHighSensors.highArea.currentSensor = Sensor.currentSensor;
            	lowHighSensors.highArea.manifold = new Manifold(Sensor.currentSensor);
            	lowHighSensors.highArea.LabelTop.Text = Sensor.currentSensor.name;
            	lowHighSensors.highArea.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(Sensor.currentSensor.device.serialNumber.deviceModel);
			      	var startMeasurement = SensorUtils.ToFormattedString(Sensor.currentSensor.type, Sensor.currentSensor.measurement, true).Split(' ');
            	lowHighSensors.highArea.LabelMiddle.Text = startMeasurement[0];
            	lowHighSensors.highArea.LabelBottom.Text = Sensor.currentSensor.unit.ToString();
            } else {
             	Sensor.manualSensor.analyzerSlot = areaIndex;
            	lowHighSensors.highArea.isManual = true;
            	lowHighSensors.highArea.manualGType = Sensor.manualSensor.type.ToString();
            	lowHighSensors.highArea.manualSensor = Sensor.manualSensor;
            	lowHighSensors.highArea.manifold = new Manifold(Sensor.manualSensor);
            	lowHighSensors.highArea.LabelTop.Text =  Sensor.topLabel.Text;
            	lowHighSensors.highArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
			      	var startMeasurement = SensorUtils.ToFormattedString(Sensor.manualSensor.type, Sensor.manualSensor.measurement, true).Split(' ');
            	lowHighSensors.highArea.LabelMiddle.Text = startMeasurement[0];
            	lowHighSensors.highArea.LabelBottom.Text = Sensor.manualSensor.unit.ToString();
            	lowHighSensors.highArea.DeviceImage.Hidden = true;
						}           
            
            lowHighSensors.highArea.manifold.ptChart = PTChart.New(ion,Fluid.EState.Dew);
            lowHighSensors.highArea.LabelSubview.Text = " " + lowHighSensors.highArea.LabelTop.Text + Util.Strings.Analyzer.LHTABLE;           
            
            if(Sensor.currentSensor != null && Sensor.currentSensor.device.isConnected){
							lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
							lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Green;
						} else if (Sensor.currentSensor != null && !Sensor.currentSensor.device.isConnected){
							lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
							lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Red;
						} else {
							lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Clear;
							lowHighSensors.highArea.Connection.Hidden = true;
							lowHighSensors.highArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
						}            
            
            lowHighSensors.showView(lowHighSensors.highArea);
	          ///////////////////// change the high manifold to be based on moved location instead of created position
	          Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change highAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
	          ion.currentAnalyzer.highAccessibility = areaIndex.ToString();
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = areaIndex.ToString();	          
	          /////////////////////
          	//Console.WriteLine("In utilities update lowhigh second area and set high accessibility to " + ion.currentAnalyzer.highAccessibility);
            //SET THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
        		Console.WriteLine("AnalyserUtilities updateLowHigh 4/2 else high set");
        		ion.currentAnalyzer.highSubviews = lowHighSensors.highArea.tableSubviews;
            ion.currentAnalyzer.highFluid =  lowHighSensors.highArea.manifold.ptChart.fluid;
  
            return;
					}

          UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

          fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

          vc.PresentViewController (fullPopup, true, null);
				}
			}
		}

		public static bool orderSensors(sensorGroup analyzerSensors, int viewLocation ,string side, UIView View){
			bool available = false;
			var currentAnalyzer = AppState.context.currentAnalyzer;
			if (side == "low") {
				if (viewLocation != 0 && viewLocation != 1 && viewLocation != 2 && viewLocation != 3) {					
					if (!analyzerSensors.viewList [0].availableView.Hidden) {
						int end = analyzerSensors.areaList [0];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [0] = start;
						analyzerSensors.areaList [viewLocation] = end;
		       	currentAnalyzer.sensorPositions[0] = start;
		        currentAnalyzer.sensorPositions[viewLocation] = end;						
						available = true;
          } else if (!analyzerSensors.viewList [1].availableView.Hidden) {
						int end = analyzerSensors.areaList [1];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [1] = start;
						analyzerSensors.areaList [viewLocation] = end;
		       	currentAnalyzer.sensorPositions[1] = start;
		        currentAnalyzer.sensorPositions[viewLocation] = end;
						available = true;
          } else if (!analyzerSensors.viewList [2].availableView.Hidden) {
						int end = analyzerSensors.areaList [2];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [2] = start;
						analyzerSensors.areaList [viewLocation] = end;
		       	currentAnalyzer.sensorPositions[2] = start;
		        currentAnalyzer.sensorPositions[viewLocation] = end;
						available = true;
          } else if (!analyzerSensors.viewList [3].availableView.Hidden) {
						int end = analyzerSensors.areaList [3];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [3] = start;
						analyzerSensors.areaList [viewLocation] = end;
		       	currentAnalyzer.sensorPositions[3] = start;
		        currentAnalyzer.sensorPositions[viewLocation] = end;
						available = true;
					} 
				} else {

					available = true;
				}
			} else if (side == "high") {
				if (viewLocation != 4 && viewLocation != 5 && viewLocation != 6 && viewLocation != 7) {
          if (!analyzerSensors.viewList [4].availableView.Hidden) {
						int end = analyzerSensors.areaList [4];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [4] = start;
						analyzerSensors.areaList [viewLocation] = end;
		       	currentAnalyzer.sensorPositions[4] = start;
		        currentAnalyzer.sensorPositions[viewLocation] = end;						
						available = true;
          } else if (!analyzerSensors.viewList [5].availableView.Hidden) {
						int end = analyzerSensors.areaList [5];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [5] = start;
						analyzerSensors.areaList [viewLocation] = end;
		       	currentAnalyzer.sensorPositions[5] = start;
		        currentAnalyzer.sensorPositions[viewLocation] = end;
						available = true;
          } else if (!analyzerSensors.viewList [6].availableView.Hidden) {
						int end = analyzerSensors.areaList [6];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [6] = start;
						analyzerSensors.areaList [viewLocation] = end;
		       	currentAnalyzer.sensorPositions[6] = start;
		        currentAnalyzer.sensorPositions[viewLocation] = end;
						available = true;
          } else if (!analyzerSensors.viewList [7].availableView.Hidden) {
						int end = analyzerSensors.areaList [7];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [7] = start;
						analyzerSensors.areaList [viewLocation] = end;
		       	currentAnalyzer.sensorPositions[7] = start;
		        currentAnalyzer.sensorPositions[viewLocation] = end;
						available = true;
					}
				} else {
					available = true;
				}
			}
      confirmLayout(analyzerSensors, View);

			////ARRANGE SENSOR LIST BASED ON THEIR SNAP POINT ASSOCIATIONS
			analyzerSensors.viewList = new List<sensor> ();
			for(int i = 0; i < analyzerSensors.areaList.Count; i++) {
				if (analyzerSensors.areaList [i] == 1) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea1);
				} else if (analyzerSensors.areaList [i] == 2) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea2);
				} else if (analyzerSensors.areaList [i] == 3) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea3);
				} else if (analyzerSensors.areaList [i] == 4) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea4);
				} else if (analyzerSensors.areaList [i] == 5) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea5);
				} else if (analyzerSensors.areaList [i] == 6) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea6);
				} else if (analyzerSensors.areaList [i] == 7) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea7);
				} else if (analyzerSensors.areaList [i] == 8) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea8);
				}
			}
			return available;
		}
    /// <summary>
    /// Checks if a sensor is on the correct side of the analyzer before adding it as a secondary sensor to a high or low area
    /// </summary>
    /// <returns><c>true</c>, if sensor is on the same side as the low/high addition, <c>false</c> otherwise.</returns>
    /// <param name="Sensor">The sensor being added as a secondary sensor to the existing sensor</param>
    /// <param name="existingSensor">The sensor being added to</param>
    /// <param name="analyzerSensors">holds the positions of all the sensors</param>
    public static bool secondarySlotSpot(sensor Sensor, sensor existingSensor, sensorGroup analyzerSensors, int type){
      bool available = false;
      if (type == 1 || type == 3) {
        for (int i = 0; i < 4; i++) {
          if (analyzerSensors.viewList[i] == Sensor) {
            available = true;
            break;
          }
        }
      } else {
        for (int i = 4; i < 8; i++) {
          if (analyzerSensors.viewList[i] == Sensor) {
            available = true;
            break;
          }
        }
      }
      return available;
    }
    /// <summary>
    /// Swaps the low high subviews when swapping low high sensors
    /// </summary>
    public static void SubviewSwap(List<string> tableSubviews, lowHighSensor updateSensor, lowHighSensor originalSensor){
    	Console.WriteLine("AnalyzerUtilities SubviewSwap");
      foreach (string subview in tableSubviews) {
        if (subview.Equals("Maximum")) {       
          updateSensor.max = originalSensor.max;
          updateSensor.maxType = originalSensor.maxType;
          updateSensor.maxReading.Text = updateSensor.max.ToString("N") + " " + updateSensor.maxType;
        } 
        if (subview.Equals("Minimum")) {
          updateSensor.min = originalSensor.min;
          updateSensor.minType = originalSensor.minType;
          updateSensor.minReading.Text = updateSensor.min.ToString("N") + " " + updateSensor.minType;
        } 
        if (subview.Equals("Hold")) {          
          updateSensor.holdReading.Text = originalSensor.holdReading.Text;
          updateSensor.holdValue = originalSensor.holdValue;
          updateSensor.holdType = originalSensor.holdType;
        }
        if (subview.Equals("Alternate")) {
          var tempUnit = originalSensor.alt.unit;
          updateSensor.alt = originalSensor.alt;
          originalSensor.alt.Dispose();
          updateSensor.alt.unit = tempUnit;
          updateSensor.altReading.Text = SensorUtils.ToFormattedString(updateSensor.alt.sensor.type, updateSensor.alt.modifiedMeasurement, true);      
        }       
      }
      if (tableSubviews.Count > 0) {
        updateSensor.subviewHide.SetImage(UIImage.FromBundle("ic_arrow_downwhite"),UIControlState.Normal);
      }
    }
    /// <summary>
    /// checks if the low or high side has a free area spot
    /// </summary>
    /// <returns><c>true</c>, if spot was free, <c>false</c> otherwise.</returns>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="Sensor">Sensor.</param>
    /// <param name="identifier">Identifier.</param>
    public static bool freeSpot(sensorGroup analyzerSensors, sensor Sensor, string identifier){      
      if (identifier == "low") {
        for (int i = 0; i < 4; i++) {
          if (!analyzerSensors.viewList[i].availableView.Hidden || analyzerSensors.viewList.LastIndexOf(Sensor) < 4) {
            return true;
          }
        }
      } else if (identifier == "high") {
        for (int i = 4; i < 8; i++) {
          if (!analyzerSensors.viewList[i].availableView.Hidden || analyzerSensors.viewList.LastIndexOf(Sensor) > 3) {
            return true;
          }
        }
      }
      return false;
    }

    public static Unit getManualUnit(ESensorType sensorType, string code){
      switch (sensorType) {
        case ESensorType.Humidity:
          return Units.Humidity.RELATIVE_HUMIDITY;
        case ESensorType.Length:
          return Units.Length.FOOT;
				case ESensorType.Weight:
					return  Units.Weight.KILOGRAM;
        case ESensorType.Pressure:
          switch (code) {
            case "pa":
              return Units.Pressure.PASCAL;
            case "kpa":
              return Units.Pressure.KILOPASCAL;
            case "mpa":
              return Units.Pressure.MEGAPASCAL;
            case "bar":
              return Units.Pressure.BAR;
            case "millibar":
              return Units.Pressure.MILLIBAR;
            case "atmo":
              return Units.Pressure.ATMOSPHERE;
            case "inhg":
              return Units.Pressure.IN_HG;
            case "cmhg":
              return Units.Pressure.CM_HG;
            case "kg/cm2":
              return Units.Pressure.KG_CM;
            case "psia":
              return Units.Pressure.PSIA;
            case "psig":
              return Units.Pressure.PSIG;
            default:
              return Units.Pressure.PSIG;
          }
        case ESensorType.Temperature:
          switch (code) {
            case "ºc":
              return Units.Temperature.CELSIUS;
            case "ºf":
              return Units.Temperature.FAHRENHEIT;
            case "ºk":
              return Units.Temperature.KELVIN;
            default:
              return Units.Temperature.FAHRENHEIT;
          }
        case ESensorType.Vacuum:
          switch (code) {
            case "pa":
              return Units.Vacuum.PASCAL;
            case "kpa":
              return Units.Vacuum.KILOPASCAL;
            case "mbar":
              return Units.Vacuum.BAR;
            case "millibar":
              return Units.Vacuum.MILLIBAR;
            case "atmo":
              return Units.Vacuum.ATMOSPHERE;
            case "inhg":
              return Units.Vacuum.IN_HG;
            case "cmhg":
              return Units.Vacuum.CM_HG;
            case "kgcm":
              return Units.Vacuum.KG_CM;
            case "psia":
              return Units.Vacuum.PSIA;
            case "mtorr":
              return Units.Vacuum.TORR;
            case "millitorr":
              return Units.Vacuum.MILLITORR;
            case "micron":
              return Units.Vacuum.MICRON;
            default:
              return Units.Vacuum.MICRON;
          }
        default:
          return Units.Pressure.PSIG;   
      }
    }
    /// <summary>
    /// The low area has a sensor association already and is swapping with another sensor without a high area association
    /// </summary>
    /// <param name="Sensor">Sensor.</param>
    /// <param name="removeSensor">Remove sensor.</param>
    /// <param name="lowHighSensors">Low high sensors.</param>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="View">View.</param>
    public static void replaceLowUnattached(sensor Sensor, sensor removeSensor, lowHighSensor lhSensor, sensorGroup analyzerSensors, UIView View){
    	Console.WriteLine("AnalyzerUtilities replaceLowUnattached. Moving sensor is " + Sensor.topLabel.Text + " and removing sensor is " + removeSensor.topLabel.Text);
      var goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "low", View);
      if (goOn) {
        var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
        
        removeSensor.topLabel.BackgroundColor = UIColor.Clear;
        removeSensor.topLabel.TextColor = UIColor.Gray;
   
       	lhSensor.tableSubviews.Clear();
        lhSensor.subviewTable.ReloadData();
     
        if(Sensor.isManual){
        	Sensor.manualSensor.analyzerSlot = areaIndex;
 					lhSensor.isManual = true;
 					lhSensor.manualGType = Sensor.manualSensor.type.ToString();
        	lhSensor.currentSensor = null;        	
        	var holdFluidState = lhSensor.manifold.ptChart.state;
					lhSensor.manualSensor = Sensor.manualSensor;
					lhSensor.manifold = new Manifold(Sensor.manualSensor);
					lhSensor.manifold.ptChart = PTChart.New(ion,holdFluidState);
					lhSensor.DeviceImage.Image = UIImage.FromBundle("ic_edit");
					lhSensor.connectionColor.Hidden = true;
					lhSensor.Connection.Hidden = true;
				} else {
        	Sensor.currentSensor.analyzerSlot = areaIndex;
 					lhSensor.isManual = false;
        	var holdFluidState = lhSensor.manifold.ptChart.state;
        	lhSensor.manualSensor = null;
					lhSensor.currentSensor = Sensor.currentSensor;
					lhSensor.manifold = new Manifold(Sensor.currentSensor);
					lhSensor.manifold.ptChart = PTChart.New(ion,holdFluidState);
					lhSensor.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(Sensor.currentSensor.device.serialNumber.deviceModel);
	
					if(Sensor.currentSensor.device.isConnected){
						lhSensor.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
						lhSensor.connectionColor.BackgroundColor = UIColor.Green;
					}
					else {
						lhSensor.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
						lhSensor.connectionColor.BackgroundColor = UIColor.Red;
					}	
					lhSensor.connectionColor.Hidden = false;
					lhSensor.Connection.Hidden = false;			
				}

				lhSensor.LabelTop.Text = Sensor.topLabel.Text;
				lhSensor.LabelMiddle.Text = Sensor.middleLabel.Text;
				lhSensor.LabelBottom.Text = Sensor.bottomLabel.Text;
				lhSensor.LabelSubview.Text = " " + Sensor.topLabel.Text + Strings.Analyzer.LHTABLE;				
				
        Sensor.topLabel.BackgroundColor = UIColor.Blue;
        Sensor.topLabel.TextColor = UIColor.White;

        ///////////////////// change the low manifold to be based on moved location instead of created position
        Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change lowAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
        ion.currentAnalyzer.lowAccessibility = areaIndex.ToString();
        lhSensor.snapArea.AccessibilityIdentifier = areaIndex.ToString();        
        /////////////////////        
        ion.currentAnalyzer.lowSubviews = lhSensor.tableSubviews;
        //SET THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
        Console.WriteLine("AnalyserUtilities replaceLowUnattached low set");
        ion.currentAnalyzer.lowFluid = lhSensor.manifold.ptChart.fluid;

        Console.WriteLine("Occupied Low side given unattached sensor with identifier " + areaIndex);
        //Console.WriteLine("The high side is currently identified with sensor " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
      }
    }
    /// <summary>
    /// The high area has a sensor association already and is swapping with another sensor without a low area association
    /// </summary>
    /// <param name="Sensor">Sensor.</param>
    /// <param name="removeSensor">Remove sensor.</param>
    /// <param name="lowHighSensors">Low high sensors.</param>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="View">View.</param>
    public static void replaceHighUnattached(sensor Sensor, sensor removeSensor, lowHighSensor lhSensor, sensorGroup analyzerSensors, UIView View){
    	Console.WriteLine("AnalyzerUtilities replaceHighUnattached. Moving sensor is " + Sensor.topLabel.Text + " and removing sensor is " + removeSensor.topLabel.Text);
      var goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "high", View);
      if (goOn) {
        var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
        
        removeSensor.topLabel.BackgroundColor = UIColor.Clear;
        removeSensor.topLabel.TextColor = UIColor.Gray;
 
      	lhSensor.tableSubviews.Clear();
      	lhSensor.subviewTable.ReloadData();
       
        if(Sensor.isManual){
        	Sensor.manualSensor.analyzerSlot = areaIndex;
 					lhSensor.isManual = true;
 					lhSensor.manualGType = Sensor.manualSensor.type.ToString();
        	var holdFluidState = lhSensor.manifold.ptChart.state;
        	lhSensor.currentSensor = null;
					lhSensor.manualSensor = Sensor.manualSensor;
					lhSensor.manifold = new Manifold(Sensor.manualSensor);
					lhSensor.manifold.ptChart = PTChart.New(ion,holdFluidState);
					lhSensor.DeviceImage.Image = UIImage.FromBundle("ic_edit");
					lhSensor.connectionColor.Hidden = true;
					lhSensor.Connection.Hidden = true;
				} else {
        	Sensor.currentSensor.analyzerSlot = areaIndex;
 					lhSensor.isManual = false;
        	var holdFluidState = lhSensor.manifold.ptChart.state;
        	lhSensor.manualSensor = null;
					lhSensor.currentSensor = Sensor.currentSensor;
					lhSensor.manifold = new Manifold(Sensor.currentSensor);
					lhSensor.manifold.ptChart = PTChart.New(ion,holdFluidState);
					lhSensor.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(Sensor.currentSensor.device.serialNumber.deviceModel);
					
					if(Sensor.currentSensor.device.isConnected){
						lhSensor.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
						lhSensor.connectionColor.BackgroundColor = UIColor.Green;
					}
					else {
						lhSensor.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
						lhSensor.connectionColor.BackgroundColor = UIColor.Red;
					}
					
					lhSensor.connectionColor.Hidden = false;
					lhSensor.Connection.Hidden = false;					
				}
				
				lhSensor.LabelTop.Text = Sensor.topLabel.Text;
				lhSensor.LabelMiddle.Text = Sensor.middleLabel.Text;
				lhSensor.LabelBottom.Text = Sensor.bottomLabel.Text;				
				lhSensor.LabelSubview.Text = " " + Sensor.topLabel.Text + Strings.Analyzer.LHTABLE;

        Sensor.topLabel.BackgroundColor = UIColor.Red;
        Sensor.topLabel.TextColor = UIColor.White;

        ///////////////////// change the high manifold to be based on moved location instead of created position
        Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change highAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
        ion.currentAnalyzer.highAccessibility = areaIndex.ToString();
        lhSensor.snapArea.AccessibilityIdentifier = areaIndex.ToString();        
        /////////////////////
        
        Console.WriteLine("AnalyserUtilities replaceHighUnattached high set");
       
        ion.currentAnalyzer.highSubviews = lhSensor.tableSubviews;
        //SET THE HIGH SIDE MANIFOLD FOR ANALYZER INSTANCE
        ion.currentAnalyzer.highFluid = lhSensor.manifold.ptChart.fluid;        
        
        Console.WriteLine("Occupied High side given unattached sensor with identifier " + areaIndex + ":" + lhSensor.snapArea.AccessibilityIdentifier);
        //Console.WriteLine("The low side is currently identified with sensor " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier);
      }
    }
    /// <summary>
    /// The low area has a sensor association already and is swapping with another sensor with a high area association
    /// </summary>
    /// <param name="Sensor">Sensor.</param>
    /// <param name="removeSensor">Remove sensor.</param>
    /// <param name="lowHighSensors">Low high sensors.</param>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="View">View.</param>
    public static void replaceLowAttached(sensor Sensor, sensor removeSensor, lowHighSensor lhAdd, lowHighSensor lhRemove, sensorGroup analyzerSensors, UIView View){
    	Console.WriteLine("AnalyzerUtilities replaceLowAttached. Moving sensor is " + Sensor.topLabel.Text + " and removing sensor is " + removeSensor.topLabel.Text);
      var goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "low", View);
      if (goOn) {
        var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
        
        removeSensor.topLabel.BackgroundColor = UIColor.Clear;
        removeSensor.topLabel.TextColor = UIColor.Gray;
				
				lhAdd.tableSubviews.Clear();
				lhAdd.subviewTable.ReloadData();
				
        if(Sensor.isManual){
					Sensor.manualSensor.analyzerSlot = areaIndex;       	
					lhAdd.isManual = true;
					lhAdd.manualGType = Sensor.manualSensor.type.ToString();     	
        	var holdFluidState = lhAdd.manifold.ptChart.state;
					lhAdd.manualSensor = Sensor.manualSensor;
					lhAdd.manifold = new Manifold(Sensor.manualSensor);
					lhAdd.manifold.ptChart = PTChart.New(ion,holdFluidState);
					lhAdd.LabelTop.Text = Sensor.topLabel.Text;
					lhAdd.Connection.Hidden = true;
					lhAdd.connectionColor.Hidden = true;
					lhAdd.DeviceImage.Image = UIImage.FromBundle("ic_edit");
					lhAdd.LabelBottom.Text = Sensor.manualSensor.unit.ToString();
				} else {
					Sensor.currentSensor.analyzerSlot = areaIndex;       	
					lhAdd.isManual = false;
        	var holdFluidState = lhAdd.manifold.ptChart.state;
					lhAdd.currentSensor = Sensor.currentSensor;
					lhAdd.manifold = new Manifold(Sensor.currentSensor);
					lhAdd.manifold.ptChart = PTChart.New(ion,holdFluidState);
					lhAdd.LabelTop.Text = Sensor.currentSensor.name;
					lhAdd.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(Sensor.currentSensor.device.serialNumber.deviceModel);
					lhAdd.LabelBottom.Text = Sensor.currentSensor.unit.ToString();
					if(Sensor.currentSensor.device.isConnected){
						lhAdd.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
						lhAdd.connectionColor.BackgroundColor = UIColor.Green;
					}
					else{
						lhAdd.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
						lhAdd.connectionColor.BackgroundColor = UIColor.Red;
					}
					lhAdd.Connection.Hidden = false;
					lhAdd.connectionColor.Hidden = false;
				}
				lhAdd.LabelSubview.Text =  " " + lhAdd.LabelTop.Text + Util.Strings.Analyzer.LHTABLE;				

        Sensor.topLabel.BackgroundColor = UIColor.Blue;
        Sensor.topLabel.TextColor = UIColor.White;

        ///////////////////// change the low manifold to be based on moved location instead of created position
        Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change lowAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
        ion.currentAnalyzer.lowAccessibility = areaIndex.ToString();
        lhAdd.snapArea.AccessibilityIdentifier = areaIndex.ToString();
        /////////////////////        
        ion.currentAnalyzer.lowSubviews = lhAdd.tableSubviews;
        lhRemove.snapArea.AccessibilityIdentifier = "high";
        ion.currentAnalyzer.highAccessibility = "high";
         //SET THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
        Console.WriteLine("AnalyserUtilities replaceLowAttached low set high removed");
        ion.currentAnalyzer.lowFluid = lhAdd.manifold.ptChart.fluid;
        ion.currentAnalyzer.highFluid = null;
        
        ////////////////////CLEAR OUT THE HIGH SIDE MANIFOLD
				lhRemove.isManual = false;     	
				lhRemove.LabelTop.Hidden = true;
				lhRemove.Connection.Hidden = true;
				lhRemove.DeviceImage.Hidden = true;
				lhRemove.LabelBottom.Hidden = true;
				lhRemove.LabelMiddle.Text = Strings.Analyzer.HIGHUNDEFINED;
				lhRemove.LabelSubview.Hidden = true;
				lhRemove.subviewTable.Hidden = true;
	      lhRemove.headingDivider.Hidden = true;
	      lhRemove.connectionColor.Hidden = true;
	      lhRemove.subviewHide.Hidden = true;
	      lhRemove.tableSubviews.Clear();
	      lhRemove.subviewTable.Source = null;
	      lhRemove.subviewTable.ReloadData ();
	      lhRemove.subviewTable.Hidden = true;
	      lhRemove.max = 0;
	      lhRemove.min = 0;      
	      lhRemove.LabelMiddle.Font = UIFont.FromName("Helvetica", 18f);
	  
	      if (lhRemove.attachedSensor != null) {
	        lhRemove.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
	        lhRemove.attachedSensor.topLabel.TextColor = UIColor.Gray;     
	        lhRemove.attachedSensor = null;
	      }
	      lhRemove.subviewHide.SetImage(null, UIControlState.Normal);
				lhRemove.currentSensor = null;
				lhRemove.manualSensor = null; 
				ion.currentAnalyzer.highSubviews = lhRemove.tableSubviews;       
 				/////////////////////////////////////////////////////////
 				
        Console.WriteLine("Occupied Low side given High side sensor with identifier " + areaIndex + ":" + lhAdd.snapArea.AccessibilityIdentifier);
        Console.WriteLine("The high side is currently identified with sensor " + lhRemove.snapArea.AccessibilityIdentifier);
      } 
    }
    /// <summary>
    /// The high area has a sensor association already and is swapping with another sensor with a low area association
    /// </summary>
    /// <param name="Sensor">Sensor.</param>
    /// <param name="removeSensor">Remove sensor.</param>
    /// <param name="lowHighSensors">Low high sensors.</param>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="View">View.</param>
    public static void replaceHighAttached(sensor Sensor, sensor removeSensor, lowHighSensor lhAdd, lowHighSensor lhRemove, sensorGroup analyzerSensors, UIView View){
    	Console.WriteLine("AnalyzerUtilities replaceHighAttached. Moving sensor is " + Sensor.topLabel.Text + " and removing sensor is " + removeSensor.topLabel.Text);
      var goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "high", View);
      if (goOn) {
        var areaIndex = analyzerSensors.viewList.IndexOf(Sensor);
        
        removeSensor.topLabel.BackgroundColor = UIColor.Clear;
        removeSensor.topLabel.TextColor = UIColor.Gray;
				
				lhAdd.tableSubviews.Clear();
				lhAdd.subviewTable.ReloadData();
				
        if(Sensor.isManual){
        	Sensor.manualSensor.analyzerSlot = areaIndex;
					lhAdd.isManual = true;     	
        	var holdFluidState = lhAdd.manifold.ptChart.state;
					lhAdd.manualSensor = Sensor.manualSensor;
					lhAdd.manifold = new Manifold(Sensor.manualSensor);
					lhAdd.manifold.ptChart = PTChart.New(ion,holdFluidState);
					lhAdd.LabelTop.Text = Sensor.topLabel.Text;
					lhAdd.Connection.Hidden = true;
					lhAdd.connectionColor.Hidden = true;
					lhAdd.DeviceImage.Image = UIImage.FromBundle("ic_edit");
					lhAdd.LabelMiddle.Text = Sensor.middleLabel.Text;
					lhAdd.LabelBottom.Text = Sensor.manualSensor.unit.ToString();					
				} else {
        	Sensor.currentSensor.analyzerSlot = areaIndex;
					lhAdd.isManual = false;     	
        	var holdFluidState = lhAdd.manifold.ptChart.state;
					lhAdd.currentSensor = Sensor.currentSensor;
					lhAdd.manifold = new Manifold(Sensor.currentSensor);
					lhAdd.manifold.ptChart = PTChart.New(ion,holdFluidState);
					lhAdd.LabelTop.Text = Sensor.currentSensor.name;
          var startMeasurement = SensorUtils.ToFormattedString(Sensor.currentSensor.type, Sensor.currentSensor.measurement, true).Split(' ');
					lhAdd.LabelMiddle.Text = startMeasurement[0];
					lhAdd.DeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(Sensor.currentSensor.device.serialNumber.deviceModel);
					lhAdd.LabelBottom.Text = Sensor.currentSensor.unit.ToString();
					if(Sensor.currentSensor.device.isConnected){
						lhAdd.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
						lhAdd.connectionColor.BackgroundColor = UIColor.Green;
					}
					else{
						lhAdd.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
						lhAdd.connectionColor.BackgroundColor = UIColor.Red;
					}
					lhAdd.connectionColor.Hidden = false;
					lhAdd.Connection.Hidden = false;						
				}				
				lhAdd.LabelSubview.Text =  " " + lhAdd.LabelTop.Text + Util.Strings.Analyzer.LHTABLE;				

        Sensor.topLabel.BackgroundColor = UIColor.Red;
        Sensor.topLabel.TextColor = UIColor.White;

        ///////////////////// change the low manifold to be based on moved location instead of created position
        Console.WriteLine("sensorSwap removeLH doesn't exist on analyzer yet. Should change highAccessibility to " + areaIndex + " instead of " + Sensor.snapArea.AccessibilityIdentifier);
        ion.currentAnalyzer.highAccessibility = areaIndex.ToString();
        lhAdd.snapArea.AccessibilityIdentifier = areaIndex.ToString();
        /////////////////////        
        ion.currentAnalyzer.highSubviews = lhAdd.tableSubviews;
        lhRemove.snapArea.AccessibilityIdentifier = "low";
        ion.currentAnalyzer.lowAccessibility = "low";
         //SET THE LOW SIDE MANIFOLD FOR ANALYZER INSTANCE
        Console.WriteLine("AnalyserUtilities replaceLowAttached low set high removed");
        ion.currentAnalyzer.highFluid = lhAdd.manifold.ptChart.fluid;
        ion.currentAnalyzer.lowFluid = null;
        
        ////////////////////CLEAR OUT THE LOW SIDE MANIFOLD
        lhRemove.isManual = false;
				lhRemove.LabelTop.Hidden = true;
				lhRemove.Connection.Hidden = true;
				lhRemove.DeviceImage.Hidden = true;
				lhRemove.LabelMiddle.Text = Strings.Analyzer.LOWUNDEFINED;
				lhRemove.LabelBottom.Hidden = true;
				lhRemove.LabelSubview.Hidden = true;
				lhRemove.subviewTable.Hidden = true;
	      lhRemove.headingDivider.Hidden = true;
	      lhRemove.connectionColor.Hidden = true;
	      lhRemove.subviewHide.Hidden = true;
	      lhRemove.tableSubviews.Clear();
	      lhRemove.subviewTable.Source = null;
	      lhRemove.subviewTable.ReloadData ();
	      lhRemove.subviewTable.Hidden = true;
	      lhRemove.max = 0;
	      lhRemove.min = 0;      
	      lhRemove.LabelMiddle.Font = UIFont.FromName("Helvetica", 18f);
	  
	      if (lhRemove.attachedSensor != null) {
	        lhRemove.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
	        lhRemove.attachedSensor.topLabel.TextColor = UIColor.Gray;     
	        lhRemove.attachedSensor = null;
	      }
	      lhRemove.subviewHide.SetImage(null, UIControlState.Normal);
				lhRemove.currentSensor = null;
				lhRemove.manualSensor = null;
        ion.currentAnalyzer.lowSubviews = lhRemove.tableSubviews;
        
        Console.WriteLine("Occupied High side given Low side sensor with identifier " + areaIndex + ":" + lhAdd.snapArea.AccessibilityIdentifier);
        Console.WriteLine("The Low side is currently identified with sensor " + lhRemove.snapArea.AccessibilityIdentifier);
      }
    }

    public static void alarmRequestViewer(sensor area) {
      var alarm = Analyzer.AnalyzerViewController.arvc.InflateViewController<SensorAlarmViewController>("viewControllerSensorAlarms");
      alarm.sensor = area.currentSensor;
      Analyzer.AnalyzerViewController.arvc.NavigationController.PushViewController(alarm, true);
    }

    /// <summary>
    /// Shows the popup to rename a sensor
    /// </summary>
    public static void renamePopup(sensor Sensor, lowHighSensor lhArea){
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }

      UIAlertController textAlert = UIAlertController.Create (Util.Strings.Analyzer.ENTERNAME, Sensor.topLabel.Text, UIAlertControllerStyle.Alert);
      textAlert.AddTextField(textField => {});
      textAlert.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, UIAlertAction => {}));
      textAlert.AddAction (UIAlertAction.Create (Util.Strings.OK_SAVE, UIAlertActionStyle.Default, UIAlertAction => {
        Sensor.topLabel.Text = " " + textAlert.TextFields[0].Text;
        Sensor.sactionView.pdeviceName.Text = textAlert.TextFields[0].Text;
        
				lhArea.LabelTop.Text = " " + textAlert.TextFields[0].Text;
				lhArea.LabelSubview.Text = " " + lhArea.LabelTop.Text + Strings.Analyzer.LHTABLE;
					
        if(Sensor.currentSensor != null){
          AppState.context.database.Query<ION.Core.Database.LoggingDeviceRow>("UPDATE LoggingDeviceRow SET name = ? WHERE serialNumber = ?",textAlert.TextFields[0].Text,Sensor.currentSensor.device.serialNumber.ToString());
          AppState.context.database.Query<ION.Core.Database.DeviceRow>("UPDATE DeviceRow SET name = ? WHERE serialNumber = ?",textAlert.TextFields[0].Text,Sensor.currentSensor.device.serialNumber.ToString());
        } 
      }));
      vc.PresentViewController(textAlert, true, null);
    }
    /// <summary>
    /// Removes the Low or high manifold association for a remote sensor update
    /// </summary>
    /// <param name="attachSensor">Attach sensor.</param>
		public static void RemoveLHAssociation(sensor attachSensor){
			//Console.WriteLine("Removing sensor: " + attachSensor.currentSensor.name);
			attachSensor.topLabel.BackgroundColor = UIColor.Clear;
			attachSensor.topLabel.TextColor = UIColor.Gray;
		}
		
		/// <summary>
		/// Shows the low/high area for the active sensor
		/// </summary>
		public static void addLHSensorAssociation(string LHArea, sensor activeSensor, LowHighArea lhArea){
			if(LHArea == "low"){
				//Console.WriteLine("Setting low area sensor color for " + activeSensor.topLabel.Text);
				activeSensor.topLabel.BackgroundColor = UIColor.Blue;
				activeSensor.topLabel.TextColor = UIColor.White;
				lhArea.lowArea.currentSensor = activeSensor.currentSensor;
				lhArea.lowArea.manifold = new Manifold(activeSensor.currentSensor);
				lhArea.lowArea.manifold.SetSecondarySensor(ion.currentAnalyzer.lowSideManifold.secondarySensor);
				lhArea.lowArea.manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew,ion.currentAnalyzer.lowSideManifold.ptChart.fluid);
				lhArea.lowArea.LabelTop.Text = activeSensor.currentSensor.name;        	
				lhArea.lowArea.LabelMiddle.Text = SensorUtils.ToFormattedString(activeSensor.currentSensor.type, activeSensor.currentSensor.measurement, false);
				lhArea.lowArea.LabelBottom.Text = activeSensor.currentSensor.unit.ToString();
				lhArea.lowArea.LabelSubview.Text = " " + lhArea.lowArea.LabelTop.Text + Strings.Analyzer.LHTABLE;

        if(activeSensor.currentSensor.device.isConnected){
          lhArea.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");              
          lhArea.lowArea.connectionColor.BackgroundColor = UIColor.Green;          
        } else {
          lhArea.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");              
          lhArea.lowArea.connectionColor.BackgroundColor = UIColor.Red;
        }
				
				lhArea.showView(lhArea.lowArea);
			} else {
				//Console.WriteLine("AnalyserUtilities Setting high area sensor color for " + activeSensor.topLabel.Text);
				activeSensor.topLabel.BackgroundColor = UIColor.Red;
				activeSensor.topLabel.TextColor = UIColor.White;
				lhArea.highArea.currentSensor = activeSensor.currentSensor;
				lhArea.highArea.manifold = new Manifold(activeSensor.currentSensor);
				lhArea.highArea.manifold.SetSecondarySensor(ion.currentAnalyzer.highSideManifold.secondarySensor);
				lhArea.highArea.manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew,ion.currentAnalyzer.highSideManifold.ptChart.fluid);
				lhArea.highArea.LabelTop.Text = activeSensor.currentSensor.name;          	
				lhArea.highArea.LabelMiddle.Text = SensorUtils.ToFormattedString(activeSensor.currentSensor.type, activeSensor.currentSensor.measurement, false);
				lhArea.highArea.LabelBottom.Text = activeSensor.currentSensor.unit.ToString();
				lhArea.highArea.LabelSubview.Text = " " + lhArea.highArea.LabelTop.Text + Strings.Analyzer.LHTABLE;
				
        if(activeSensor.currentSensor.device.isConnected){
          lhArea.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");              
          lhArea.highArea.connectionColor.BackgroundColor = UIColor.Green;          
        } else {
          lhArea.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");              
          lhArea.highArea.connectionColor.BackgroundColor = UIColor.Red;
        }
				
				lhArea.showView(lhArea.highArea);
			}
		}

		public static void arrangeViews(sensorGroup analyzerSensors){
			////ARRANGE SENSOR LIST BASED ON THEIR SNAP POINT ASSOCIATIONS
			analyzerSensors.viewList = new List<sensor> ();
			for(int i = 0; i < analyzerSensors.areaList.Count; i++) {
				if (analyzerSensors.areaList [i] == 1) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea1);
					if(analyzerSensors.snapArea1.currentSensor != null){
						analyzerSensors.snapArea1.currentSensor.analyzerSlot = i;
					}
				} else if (analyzerSensors.areaList [i] == 2) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea2);
					if(analyzerSensors.snapArea2.currentSensor != null){
						analyzerSensors.snapArea2.currentSensor.analyzerSlot = i;
					}
				} else if (analyzerSensors.areaList [i] == 3) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea3);
					if(analyzerSensors.snapArea3.currentSensor != null){
						analyzerSensors.snapArea3.currentSensor.analyzerSlot = i;
					}
				} else if (analyzerSensors.areaList [i] == 4) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea4);
					if(analyzerSensors.snapArea4.currentSensor != null){
						analyzerSensors.snapArea4.currentSensor.analyzerSlot = i;
					}
				} else if (analyzerSensors.areaList [i] == 5) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea5);
					if(analyzerSensors.snapArea5.currentSensor != null){
						analyzerSensors.snapArea5.currentSensor.analyzerSlot = i;
					}
				} else if (analyzerSensors.areaList [i] == 6) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea6);
					if(analyzerSensors.snapArea6.currentSensor != null){
						analyzerSensors.snapArea6.currentSensor.analyzerSlot = i;
					}
				} else if (analyzerSensors.areaList [i] == 7) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea7);
					if(analyzerSensors.snapArea7.currentSensor != null){
						analyzerSensors.snapArea7.currentSensor.analyzerSlot = i;
					}
				} else if (analyzerSensors.areaList [i] == 8) {
					analyzerSensors.viewList.Insert (i, analyzerSensors.snapArea8);
					if(analyzerSensors.snapArea8.currentSensor != null){
						analyzerSensors.snapArea8.currentSensor.analyzerSlot = i;
					}
				}
			}
		}		
		
		public static void removeAttachedSensor(lowHighSensor lowHigh, sensorGroup analyzerSensors){
			if(lowHigh.attachedSensor == null){
				return;
			} else {
        for(int i = 0; i < 8; i++){
					if(analyzerSensors.viewList[i].currentSensor != null && lowHigh.attachedSensor.currentSensor == analyzerSensors.viewList[i].currentSensor){
						analyzerSensors.viewList[i].topLabel.BackgroundColor = UIColor.White;
						analyzerSensors.viewList[i].topLabel.TextColor = UIColor.Gray;
						lowHigh.attachedSensor = null;
						break;
					}
				}
			}
		}
	}
}

