﻿/// <summary>
/// This contains the methods for setting up the analyzer view and subviews
/// </summary>


using System;
using System.Collections.Generic;
using System.Globalization;
using UIKit;
using CoreGraphics;
using Foundation;
using CoreAnimation;

using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Util;
using ION.Core.Sensors;
using ION.Core.Sensors.Properties;
using ION.Core.Fluids;
using ION.Core.Measure;
using ION.Core.App;

using ION.IOS.ViewController.Alarms;
using ION.IOS.Util;

namespace ION.IOS.ViewController.Analyzer
{
	class AnalyserUtilities 
	{
    public static IONPrimaryScreenController root { get; set; }
		/// <summary>
		/// Calculates the locations and snap points for sensor subviews
		/// </summary>
		/// <param name="analyzerSensors">singleSensor class object holding 8 sensors</param>
		/// <param name="View">View.</param>
		public static void CreateSnapArea(sensorGroup analyzerSensors, UIView View){
			////CREATE STATIC SENSOR LOCATIONS
			/// LEFT SIDE
      analyzerSensors.snapRect1 = new CGRect (.031 * View.Bounds.Width, .156 * View.Bounds.Height, .203 * View.Bounds.Width, .114 * View.Bounds.Height);
      analyzerSensors.snapRect2 = new CGRect(.031 * View.Bounds.Width, .438 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);
      analyzerSensors.snapRect3 = new CGRect(.25 * View.Bounds.Width, .156 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);
      analyzerSensors.snapRect4 = new CGRect(.25 * View.Bounds.Width, .438 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);
			/// RIGHT SIDE
      analyzerSensors.snapRect5 = new CGRect (.546 * View.Bounds.Width, .156 * View.Bounds.Height, .203 * View.Bounds.Width, .114 * View.Bounds.Height);
      analyzerSensors.snapRect6 = new CGRect(.546 * View.Bounds.Width, .438 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);
      analyzerSensors.snapRect7 = new CGRect(.765 * View.Bounds.Width, .156 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);
      analyzerSensors.snapRect8 = new CGRect(.765 * View.Bounds.Width, .438 * View.Bounds.Height, .203 * View.Bounds.Width,.114 * View.Bounds.Height);

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
      mentryView.mView.Layer.CornerRadius = 10f;
      mentryView.mView.Layer.BorderWidth = 1f;
      mentryView.mView.Layer.BorderColor = UIColor.LightGray.CGColor;
      mentryView.popupText.Text = Util.Strings.Analyzer.CREATEMANUAL;
      mentryView.popupText.TextAlignment = UITextAlignment.Center;
      mentryView.popupText.AdjustsFontSizeToFitWidth = true;
      mentryView.popupText.BackgroundColor = UIColor.FromRGB(9,221,255);
      mentryView.popupText.Layer.CornerRadius = 5f;
      mentryView.popupText.ClipsToBounds = false;
      mentryView.popupText.Font = UIFont.FromName("Helvetica-Bold", 27f);
      mentryView.mdeviceType.Text = Util.Strings.Analyzer.DEVICETYPE + ":";
      mentryView.mdeviceType.AdjustsFontSizeToFitWidth = true;
      mentryView.dtypeButton.SetTitle(Util.Strings.Analyzer.PRESSURE, UIControlState.Normal);
      mentryView.dtypeButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      mentryView.dtypeButton.AccessibilityIdentifier = "Pressure";
      mentryView.dtypeButton.Font = UIFont.FromName("Helvetica-Bold", 20f);
      mentryView.mtextValue.Layer.BorderColor = UIColor.LightGray.CGColor;
      mentryView.mtextValue.Layer.BorderWidth = 1f;
      mentryView.mtextValue.Layer.CornerRadius = 5f;
      mentryView.mbuttonText.UserInteractionEnabled = false;
      mentryView.mbuttonText.Text = "psig";
      mentryView.mbuttonText.Font = UIFont.FromName("Helvetica-Bold", 20f);
      mentryView.mbuttonText.TextColor = UIColor.Black;
      mentryView.mbuttonText.TextAlignment = UITextAlignment.Center;
      mentryView.mcloseButton.SetTitleColor(UIColor.Gray, UIControlState.Normal);
      mentryView.mcloseButton.SetTitle(Util.Strings.Analyzer.CLOSE, UIControlState.Normal);
      mentryView.mcloseButton.ClipsToBounds = true;
      mentryView.mdoneButton.SetTitleColor(UIColor.Gray, UIControlState.Normal);
      mentryView.mdoneButton.SetTitle(Util.Strings.Analyzer.OKDONE, UIControlState.Normal);
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
      sactionView.aView.Layer.BorderWidth = 1f;
      sactionView.aView.Layer.BorderColor = UIColor.LightGray.CGColor;
      sactionView.pactionButton.SetTitle(Util.Strings.Analyzer.ACTIONS, UIControlState.Normal);
      sactionView.pactionButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      sactionView.pcloseButton.SetTitle(Util.Strings.Analyzer.CLOSE, UIControlState.Normal);
      sactionView.pcloseButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      sactionView.pvalueType.TextAlignment = UITextAlignment.Right;
      sactionView.pLowHigh.AdjustsFontSizeToFitWidth = true;
      sactionView.pLowHigh.Layer.CornerRadius = 5f;
      sactionView.pLowHigh.Layer.BorderWidth = 1f;
      sactionView.pLowHigh.ClipsToBounds = true;
      sactionView.pLowHigh.TextAlignment = UITextAlignment.Center;
      sactionView.pgaugeValue.AdjustsFontSizeToFitWidth = true;
      sactionView.pgaugeValue.Font = UIFont.FromName("Helvetica-Bold", 54f);
      sactionView.pgaugeValue.TextAlignment = UITextAlignment.Right;
      sactionView.pdisplayLink.Text = Util.Strings.Analyzer.DISPLAYLINK;
      sactionView.pdisplayLink.TextAlignment = UITextAlignment.Right;
      sactionView.pdisplayLink.Font = UIFont.FromName("Helvetica", 12f);
      sactionView.pconnectionStatus.AdjustsFontSizeToFitWidth = true;
      sactionView.connectionColor.BackgroundColor = UIColor.Clear;
      sactionView.connectionColor.Layer.CornerRadius = 8;
      sactionView.connectionColor.Layer.BorderColor = UIColor.Black.CGColor;
      sactionView.connectionColor.Layer.BorderWidth = 1f;
      sactionView.pconnection.Layer.BorderColor = UIColor.Black.CGColor;
      sactionView.pconnection.Layer.BorderWidth = 1f;
      sactionView.pconnection.Layer.CornerRadius = 8;

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

      Sensor.sactionView = new ActionView(mainView);
      Sensor.sactionView.aView.Layer.CornerRadius = 10;
      CreateActionViews(Sensor.sactionView);
      Sensor.addIcon = new UIImageView(new CGRect(.107 * Sensor.snapArea.Bounds.Width,.107 * Sensor.snapArea.Bounds.Height,.769 * Sensor.snapArea.Bounds.Width,.769 * Sensor.snapArea.Bounds.Height));
      Sensor.addIcon.Image = UIImage.FromBundle("ic_device_add");
      Sensor.addIcon.BackgroundColor = UIColor.Clear;
      Sensor.snapArea.Layer.CornerRadius = 10;
      Sensor.availableView.Layer.CornerRadius = 10;
      Sensor.snapArea.Layer.BorderColor = UIColor.Black.CGColor;
      Sensor.snapArea.Layer.BorderWidth = 2f;
      Sensor.snapArea.AddSubview(Sensor.availableView);
      Sensor.snapArea.AddSubview(Sensor.addIcon);
      Sensor.snapArea.BringSubviewToFront(Sensor.addIcon);

      CreateLowHighArea(Sensor,  mainView);
      AddHighLowArea(Sensor.lowArea, Sensor.highArea, mainView);
      applyLowHighGestures(Sensor.lowArea, Sensor.highArea, lowHighSensors, Sensor);
      CreateSubviewLayout (Sensor.snapArea, Sensor.topLabel, Sensor.tLabelBottom, Sensor.middleLabel, Sensor.bottomLabel);
      mainView.AddSubview (Sensor.snapArea);
      mainView.AddSubview (Sensor.sactionView.aView);
      mainView.AddSubview (Sensor.lowArea.snapArea);
      mainView.BringSubviewToFront(Sensor.lowArea.snapArea);
      mainView.AddSubview (Sensor.highArea.snapArea);
      mainView.BringSubviewToFront(Sensor.highArea.snapArea);
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
    public static void AddHighLowArea(lowHighSensor lowArea, lowHighSensor highArea, UIView mainView){
      lowArea.snapArea.BackgroundColor = UIColor.White;
      lowArea.snapArea.Alpha = 1f;
      lowArea.snapArea.UserInteractionEnabled = true;
      lowArea.snapArea.AccessibilityIdentifier = "low";        
      lowArea.snapArea.Layer.CornerRadius = 10;
      lowArea.snapArea.Hidden = true;

      lowArea.LabelTop.AdjustsFontSizeToFitWidth = true;
      lowArea.LabelTop.Text = "";
      lowArea.LabelTop.Layer.CornerRadius = 10;
      lowArea.LabelTop.ClipsToBounds = true;

      lowArea.LabelMiddle.AdjustsFontSizeToFitWidth = true;
      lowArea.LabelMiddle.Text = Util.Strings.Analyzer.LOWUNDEFINED;
      lowArea.LabelMiddle.Font = UIFont.FromName("Helvetica-Bold", 42f);
      lowArea.LabelMiddle.TextAlignment = UITextAlignment.Right;

      lowArea.LabelBottom.AdjustsFontSizeToFitWidth = true;
      lowArea.LabelBottom.Text = "";
      lowArea.LabelBottom.TextAlignment = UITextAlignment.Right;

      lowArea.LabelSubview.AdjustsFontSizeToFitWidth = true;
      lowArea.LabelSubview.BackgroundColor = UIColor.Blue;
      lowArea.LabelSubview.Text = "";
      lowArea.LabelSubview.TextColor = UIColor.White;
      lowArea.LabelSubview.ClipsToBounds = true;

      lowArea.subviewTable.BackgroundColor = UIColor.Clear;
      lowArea.subviewTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      lowArea.subviewTable.Hidden = true;
      lowArea.subviewTable.Source = null;

      lowArea.subviewHide.BackgroundColor = UIColor.Blue;
      lowArea.subviewHide.SetImage(null, UIControlState.Normal);

      lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
      lowArea.Connection.BackgroundColor = UIColor.Clear;
      lowArea.connectionColor.BackgroundColor = UIColor.Clear;
      lowArea.connectionColor.Layer.CornerRadius = 6;
      lowArea.connectionColor.Layer.BorderColor = UIColor.Black.CGColor;
      lowArea.connectionColor.Layer.BorderWidth = 1f;
      lowArea.subviewDivider.BackgroundColor = UIColor.Black;
      lowArea.headingDivider.BackgroundColor = UIColor.Black;

      lowArea.snapArea.AddSubview (lowArea.LabelTop);
      lowArea.snapArea.AddSubview (lowArea.LabelMiddle);
      lowArea.snapArea.AddSubview (lowArea.LabelBottom);
      lowArea.snapArea.AddSubview (lowArea.LabelSubview);
      lowArea.snapArea.AddSubview (lowArea.DeviceImage);
      lowArea.snapArea.AddSubview (lowArea.Connection);
      lowArea.snapArea.AddSubview (lowArea.subviewDivider);
      lowArea.snapArea.AddSubview (lowArea.headingDivider);
      lowArea.snapArea.AddSubview(lowArea.subviewHide);
      lowArea.snapArea.BringSubviewToFront(lowArea.headingDivider);
      lowArea.snapArea.AddSubview (lowArea.connectionColor);
      lowArea.snapArea.BringSubviewToFront(lowArea.Connection);
      lowArea.snapArea.AddSubview(lowArea.conDisButton);
      lowArea.snapArea.BringSubviewToFront(lowArea.conDisButton);
      lowArea.snapArea.BringSubviewToFront(lowArea.subviewDivider);
      lowArea.snapArea.AddSubview(lowArea.activityConnectStatus);

      mainView.AddSubview (lowArea.snapArea);
      mainView.AddSubview (lowArea.subviewTable);

      highArea.snapArea.BackgroundColor = UIColor.White;
      highArea.snapArea.Alpha = 1f;
      highArea.snapArea.UserInteractionEnabled = true;
      highArea.snapArea.AccessibilityIdentifier = "high";
      highArea.snapArea.Layer.CornerRadius = 10;
      highArea.snapArea.Hidden = true;

      highArea.LabelTop.AdjustsFontSizeToFitWidth = true;
      highArea.LabelTop.Text = "";
      highArea.LabelTop.Layer.CornerRadius = 10;
      highArea.LabelTop.ClipsToBounds = true;

      highArea.LabelMiddle.AdjustsFontSizeToFitWidth = true;
      highArea.LabelMiddle.Text = Util.Strings.Analyzer.HIGHUNDEFINED;
      highArea.LabelMiddle.Font = UIFont.FromName("Helvetica-Bold", 42f);
      highArea.LabelMiddle.TextAlignment = UITextAlignment.Right;

      highArea.LabelBottom.AdjustsFontSizeToFitWidth = true;
      highArea.LabelBottom.Text = "";
      highArea.LabelBottom.TextAlignment = UITextAlignment.Right;

      highArea.LabelSubview.AdjustsFontSizeToFitWidth = true;
      highArea.LabelSubview.BackgroundColor = UIColor.Red;
      highArea.LabelSubview.Text = "";
      highArea.LabelSubview.TextColor = UIColor.White;
      highArea.LabelSubview.ClipsToBounds = true;

      highArea.subviewTable.BackgroundColor = UIColor.Clear;
      highArea.subviewTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      highArea.subviewTable.Hidden = true;
      highArea.subviewTable.Source = null;

      highArea.subviewHide.BackgroundColor = UIColor.Red;
      highArea.subviewHide.SetImage(null, UIControlState.Normal);

      highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
      highArea.Connection.BackgroundColor = UIColor.Clear;
      highArea.connectionColor.BackgroundColor = UIColor.Clear;
      highArea.connectionColor.Layer.CornerRadius = 6;
      highArea.connectionColor.Layer.BorderColor = UIColor.Black.CGColor;
      highArea.connectionColor.Layer.BorderWidth = 1f;
      highArea.subviewDivider.BackgroundColor = UIColor.Black;
      highArea.headingDivider.BackgroundColor = UIColor.Black;

      highArea.snapArea.AddSubview (highArea.LabelTop);
      highArea.snapArea.AddSubview (highArea.LabelMiddle);
      highArea.snapArea.AddSubview (highArea.LabelBottom);
      highArea.snapArea.AddSubview (highArea.LabelSubview);
      highArea.snapArea.AddSubview (highArea.DeviceImage);
      highArea.snapArea.AddSubview (highArea.Connection);
      highArea.snapArea.AddSubview (highArea.subviewDivider);
      highArea.snapArea.AddSubview (highArea.headingDivider);
      highArea.snapArea.AddSubview(highArea.subviewHide);
      highArea.snapArea.BringSubviewToFront(highArea.headingDivider);
      highArea.snapArea.AddSubview (highArea.connectionColor);
      highArea.snapArea.BringSubviewToFront(highArea.Connection);
      highArea.snapArea.AddSubview(highArea.conDisButton);
      highArea.snapArea.BringSubviewToFront(highArea.conDisButton);
      highArea.snapArea.BringSubviewToFront(highArea.subviewDivider);
      highArea.snapArea.AddSubview(highArea.activityConnectStatus);

      mainView.AddSubview (highArea.snapArea);
      mainView.AddSubview (highArea.subviewTable);

      lowArea.subviewTable.ContentInset = new UIEdgeInsets(0, 0, 20, 0);
      highArea.subviewTable.ContentInset = new UIEdgeInsets(0, 0, 20, 0);
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
			lowHighSensors.lowArea.snapArea.Layer.CornerRadius = 10;

			lowHighSensors.lowArea.LabelTop.AdjustsFontSizeToFitWidth = true;
			lowHighSensors.lowArea.LabelTop.Text = "";
			lowHighSensors.lowArea.LabelTop.Layer.CornerRadius = 10;
			lowHighSensors.lowArea.LabelTop.ClipsToBounds = true;

			lowHighSensors.lowArea.LabelMiddle.AdjustsFontSizeToFitWidth = true;
      lowHighSensors.lowArea.LabelMiddle.Text = Util.Strings.Analyzer.LOWUNDEFINED;
			lowHighSensors.lowArea.LabelMiddle.TextAlignment = UITextAlignment.Left;

			lowHighSensors.lowArea.LabelBottom.AdjustsFontSizeToFitWidth = true;
			lowHighSensors.lowArea.LabelBottom.Text = "";
			lowHighSensors.lowArea.LabelBottom.TextAlignment = UITextAlignment.Right;

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
      //lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Clear;
      lowHighSensors.lowArea.Connection.Hidden = true;
      lowHighSensors.lowArea.connectionColor.Layer.CornerRadius = 6;
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
			lowHighSensors.highArea.snapArea.Layer.CornerRadius = 10;

			lowHighSensors.highArea.LabelTop.AdjustsFontSizeToFitWidth = true;
			lowHighSensors.highArea.LabelTop.Text = "";
			lowHighSensors.highArea.LabelTop.Layer.CornerRadius = 10;
			lowHighSensors.highArea.LabelTop.ClipsToBounds = true;

			lowHighSensors.highArea.LabelMiddle.AdjustsFontSizeToFitWidth = true;
      lowHighSensors.highArea.LabelMiddle.Text = Util.Strings.Analyzer.HIGHUNDEFINED;
			lowHighSensors.highArea.LabelMiddle.TextAlignment = UITextAlignment.Left;

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
      //lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Clear;
      lowHighSensors.highArea.connectionColor.Layer.CornerRadius = 6;
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
		public static void CreateSubviewLayout(UIView subview, UILabel topLabel, UILabel tLabelBottom, UILabel middleLabel, UILabel bottomLabel){
      topLabel.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect (0, 0, subview.Frame.Size.Width, .307 * subview.Frame.Size.Height);
			topLabel.AdjustsFontSizeToFitWidth = true;
			topLabel.Text = " Press " + subview.AccessibilityIdentifier;
			topLabel.Hidden = true;
			topLabel.ClipsToBounds = true;
      topLabel.Layer.CornerRadius = 10;

      tLabelBottom.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect(0, .2 * subview.Bounds.Height, subview.Bounds.Width, .12 * subview.Bounds.Height);
      tLabelBottom.BackgroundColor = UIColor.Blue;
      tLabelBottom.Hidden = true;

      middleLabel.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect (0, .261 * subview.Bounds.Height, .984 * subview.Frame.Size.Width, .461 * subview.Frame.Size.Height);
			middleLabel.AdjustsFontSizeToFitWidth = true;
			middleLabel.Text = "0.00";
			middleLabel.Hidden = true;
			middleLabel.TextAlignment = UITextAlignment.Right;

      bottomLabel.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect (0, .676 * subview.Bounds.Height, .969 * subview.Frame.Size.Width, .261 * subview.Frame.Size.Height);
			bottomLabel.AdjustsFontSizeToFitWidth = true;
			bottomLabel.Text = "";
			bottomLabel.Hidden = true;
			bottomLabel.ClipsToBounds = true;
			bottomLabel.TextAlignment = UITextAlignment.Right;

			subview.AddSubview (topLabel);
			subview.AddSubview (middleLabel);
			subview.AddSubview (bottomLabel);
      subview.AddSubview(tLabelBottom);
      subview.BringSubviewToFront(topLabel);
		}
		/// <summary>
		/// WHAT TODO WHEN THEY WANT TO REMOVE A SENSOR FROM LOW/HIGH AREA
		/// </summary>
		/// <param name="pressedArea">LOW/HIGH VIEW</param>
		/// <param name="topLabel">LOW/HIGH TOP LABEL</param>
		/// <param name="middleLabel">LOW/HIGH TOP LABEL</param>
		/// <param name="bottomLabel">LOW/HIGH TOP LABEL</param>
		/// <param name="removeView">ATTACHED SENSOR TOP LABEL</param>
		public static void RemoveDevice(sensor removeSensor, LowHighArea lowHighSensors){      
      removeSensor.topLabel.BackgroundColor = UIColor.Clear;
      removeSensor.topLabel.TextColor = UIColor.Black;
      removeSensor.tLabelBottom.Hidden = true;

      removeSensor.lowArea.snapArea.Hidden = true;
      removeSensor.lowArea.tableSubviews = new List<string> ();
      removeSensor.lowArea.subviewTable.Source = null;
      removeSensor.lowArea.subviewTable.ReloadData ();
      removeSensor.lowArea.subviewTable.Hidden = true;
      removeSensor.lowArea.max = 0;
      removeSensor.lowArea.min = 0;
      if (removeSensor.lowArea.attachedSensor != null) {
        removeSensor.lowArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
        removeSensor.lowArea.attachedSensor.topLabel.TextColor = UIColor.Black;     
        removeSensor.lowArea.attachedSensor.tLabelBottom.Hidden = true;
        removeSensor.lowArea.attachedSensor = null;

        removeSensor.highArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
        removeSensor.highArea.attachedSensor.topLabel.TextColor = UIColor.Black;     
        removeSensor.highArea.attachedSensor.tLabelBottom.Hidden = true;
        removeSensor.highArea.attachedSensor = null;
      }
      removeSensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);

      removeSensor.highArea.snapArea.Hidden = true;
      removeSensor.highArea.tableSubviews = new List<string> ();
      removeSensor.highArea.subviewTable.Source = null;
      removeSensor.highArea.subviewTable.ReloadData ();
      removeSensor.highArea.subviewTable.Hidden = true;
      removeSensor.highArea.max = 0;
      removeSensor.highArea.min = 0;

      removeSensor.highArea.subviewHide.SetImage(null, UIControlState.Normal);
      if (removeSensor.currentSensor != null) {
        removeSensor.lowArea.manifold = new Manifold(removeSensor.currentSensor);
        removeSensor.lowArea.manifold.ptChart = PTChart.New(removeSensor.lowArea.ion, Fluid.EState.Dew);
        removeSensor.highArea.manifold = new Manifold(removeSensor.currentSensor);
        removeSensor.highArea.manifold.ptChart = PTChart.New(removeSensor.lowArea.ion, Fluid.EState.Dew);
      } else if (removeSensor.manualSensor != null) {
        removeSensor.lowArea.manifold = new Manifold(removeSensor.manualSensor);
        removeSensor.lowArea.manifold.primarySensor.unit = removeSensor.manualSensor.unit;
        removeSensor.lowArea.manifold.ptChart = PTChart.New(removeSensor.lowArea.ion, Fluid.EState.Dew);

        removeSensor.highArea.manifold = new Manifold(removeSensor.manualSensor);
        removeSensor.highArea.manifold.primarySensor.unit = removeSensor.manualSensor.unit;
        removeSensor.highArea.manifold.ptChart = PTChart.New(removeSensor.highArea.ion, Fluid.EState.Dew);
      }

      if (removeSensor.snapArea.AccessibilityIdentifier == lowHighSensors.lowArea.snapArea.AccessibilityIdentifier)
        lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
      else if (removeSensor.snapArea.AccessibilityIdentifier == lowHighSensors.highArea.snapArea.AccessibilityIdentifier)
        lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";

      Console.WriteLine("Low side identifier after LH removal: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier);
      Console.WriteLine("High side identifier after LH removal: " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
		}
		/// <summary>
		/// WHAT TODO WHEN THEY WANT TO REMOVE A SINGLE SENSOR
		/// </summary>
    public static void RemoveDevice(actionPopup Sensor, LowHighArea lowHighSensors, sensorGroup analyzerSensors, List<Sensor> sensorList){
      for(int i = 0; i < sensorList.Count; i++){
        if (sensorList[i] != null) {
          if (sensorList[i].name == Sensor.pressedSensor.currentSensor.device.name && sensorList[i].type == Sensor.pressedSensor.currentSensor.type) {
            sensorList.Remove(Sensor.pressedSensor.currentSensor);
          }
        }
      }

      Sensor.pressedSensor.snapArea.RemoveGestureRecognizer (Sensor.addLong);
      Sensor.pressedSensor.snapArea.RemoveGestureRecognizer(Sensor.addPan);
      Sensor.pressedSensor.availableView.Hidden = false;
      Sensor.pressedView.BackgroundColor = UIColor.Clear;
      Sensor.pressedSensor.sactionView.aView.Hidden = true;
      Sensor.pressedSensor.addIcon.Hidden = false;
      Sensor.pressedSensor.topLabel.Hidden = true;
      Sensor.pressedSensor.topLabel.BackgroundColor = UIColor.Clear;
      Sensor.pressedSensor.topLabel.TextColor = UIColor.Black;
      Sensor.pressedSensor.tLabelBottom.Hidden = true;
      Sensor.pressedSensor.middleLabel.Hidden = true;
      Sensor.pressedSensor.bottomLabel.Hidden = true;
      Sensor.pressedSensor.topLabel.Text = "Press " + Sensor.pressedSensor.snapArea.AccessibilityIdentifier;
      Sensor.pressedSensor.middleLabel.Text = "0.00";
      Sensor.pressedSensor.bottomLabel.Text = "psig";
      //not sure if removing should disconnect the device.....
      if (Sensor.pressedSensor.isManual.Equals(false)) {
        Sensor.pressedSensor.currentSensor.onSensorStateChangedEvent -= Sensor.pressedSensor.gaugeUpdating;
        Sensor.pressedSensor.sactionView.currentSensor.onSensorStateChangedEvent -= Sensor.pressedSensor.sactionView.gaugeUpdating;
        Sensor.pressedSensor.lowArea.currentSensor.onSensorStateChangedEvent -= Sensor.pressedSensor.lowArea.gaugeUpdating;
        Sensor.pressedSensor.highArea.currentSensor.onSensorStateChangedEvent -= Sensor.pressedSensor.lowArea.gaugeUpdating;

        Sensor.pressedSensor.lowArea.manifold.Dispose();
        Sensor.pressedSensor.highArea.manifold.Dispose();
        Sensor.pressedSensor.currentSensor = null;
        Sensor.pressedSensor.sactionView.currentSensor = null;
        Sensor.pressedSensor.lowArea.currentSensor = null;
        Sensor.pressedSensor.highArea.currentSensor = null;
      }
      Sensor.pressedSensor.isManual = false;
      Sensor.pressedSensor.lowArea.snapArea.Hidden = true;
      Sensor.pressedSensor.lowArea.isManual = false;
      Sensor.pressedSensor.lowArea.max = 0;
      Sensor.pressedSensor.lowArea.min = 0;
      Sensor.pressedSensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);
      Sensor.pressedSensor.lowArea.subviewTable.Source = null;
      Sensor.pressedSensor.lowArea.subviewTable.ReloadData ();
      Sensor.pressedSensor.lowArea.subviewTable.Hidden = true;
      Sensor.pressedSensor.lowArea.tableSubviews = new List<string> ();

      Sensor.pressedSensor.highArea.snapArea.Hidden = true;
      Sensor.pressedSensor.highArea.isManual = false;
      Sensor.pressedSensor.highArea.max = 0;
      Sensor.pressedSensor.highArea.min = 0;
      Sensor.pressedSensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);
      Sensor.pressedSensor.highArea.subviewTable.Source = null;
      Sensor.pressedSensor.highArea.subviewTable.ReloadData ();
      Sensor.pressedSensor.highArea.subviewTable.Hidden = true;
      Sensor.pressedSensor.highArea.tableSubviews = new List<string> ();

      if (Sensor.pressedSensor.lowArea.attachedSensor != null) {
        Sensor.pressedSensor.lowArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
        Sensor.pressedSensor.lowArea.attachedSensor.topLabel.TextColor = UIColor.Black;     
        Sensor.pressedSensor.lowArea.attachedSensor.tLabelBottom.Hidden = true;
        Sensor.pressedSensor.lowArea.attachedSensor = null;

        Sensor.pressedSensor.highArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
        Sensor.pressedSensor.highArea.attachedSensor.topLabel.TextColor = UIColor.Black;     
        Sensor.pressedSensor.highArea.attachedSensor.tLabelBottom.Hidden = true;
        Sensor.pressedSensor.highArea.attachedSensor = null;
      } else {
        for (int i = 0; i < 8; i++) {
          if (analyzerSensors.viewList[i].lowArea.attachedSensor != null && analyzerSensors.viewList[i].lowArea.attachedSensor == Sensor.pressedSensor) {
            analyzerSensors.viewList[i].lowArea.attachedSensor = null;
            analyzerSensors.viewList[i].highArea.attachedSensor = null;

            if (analyzerSensors.viewList[i].currentSensor != null) {
              analyzerSensors.viewList[i].lowArea.manifold = new Manifold(analyzerSensors.viewList[i].currentSensor);
              analyzerSensors.viewList[i].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].lowArea.ion, Fluid.EState.Dew);
              analyzerSensors.viewList[i].highArea.manifold = new Manifold(analyzerSensors.viewList[i].currentSensor);
              analyzerSensors.viewList[i].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].lowArea.ion, Fluid.EState.Dew);
            } else if (analyzerSensors.viewList[i].manualSensor != null) {
              analyzerSensors.viewList[i].lowArea.manifold = new Manifold(analyzerSensors.viewList[i].manualSensor);
              analyzerSensors.viewList[i].lowArea.manifold.primarySensor.unit = analyzerSensors.viewList[i].lowArea.manualSensor.unit;
              analyzerSensors.viewList[i].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].lowArea.ion, Fluid.EState.Dew);

              analyzerSensors.viewList[i].highArea.manifold = new Manifold(analyzerSensors.viewList[i].manualSensor);
              analyzerSensors.viewList[i].highArea.manifold.primarySensor.unit = analyzerSensors.viewList[i].lowArea.manualSensor.unit;
              analyzerSensors.viewList[i].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].lowArea.ion, Fluid.EState.Dew);
            }
          }
        }
      } 

      if (Sensor.pressedSensor.snapArea.AccessibilityIdentifier == lowHighSensors.lowArea.snapArea.AccessibilityIdentifier)
        lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
      else if (Sensor.pressedSensor.snapArea.AccessibilityIdentifier == lowHighSensors.highArea.snapArea.AccessibilityIdentifier)
        lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
		}
		/// <summary>
		/// Calculates the position for the Low and High subviews
		/// </summary>
		/// <param name="lowHighSensors">Low high sensors.</param>
		/// <param name="View">View.</param>
		public static void CreateLowHighArea(LowHighArea lowHighSensors, UIView View){
			////CREATE ANALYSER HIGH AND LOW AREAS
      //lowHighSensors.lowArea.areaRect = new CGRect(View.Center.X - 151, View.Center.Y + 35, 149, 115);
      lowHighSensors.lowArea.areaRect = new CGRect(.028 * View.Bounds.Width, .561 * View.Bounds.Height, .465 * View.Bounds.Width, .202 * View.Bounds.Height);
      //lowHighSensors.highArea.areaRect = new CGRect (View.Center.X + 4, View.Center.Y + 35, 149, 115); 
      lowHighSensors.highArea.areaRect = new CGRect (.512 * View.Bounds.Width, .561 * View.Bounds.Height, .465 * View.Bounds.Width, .202 * View.Bounds.Height);
		}
    /// <summary>
    /// Calculates the position for the Low and High subviews
    /// </summary>
    /// <param name="lowHighSensors">Low high sensors.</param>
    /// <param name="View">View.</param>
    public static void CreateLowHighArea(sensor Sensor, UIView View){      
      ////CREATE ANALYSER HIGH AND LOW AREAS
      //lowHighSensors.lowArea.areaRect = new CGRect(View.Center.X - 151, View.Center.Y + 35, 149, 115);
      Sensor.lowArea.areaRect = new CGRect(.028 * View.Bounds.Width, .561 * View.Bounds.Height, .465 * View.Bounds.Width, .202 * View.Bounds.Height);
      //lowHighSensors.highArea.areaRect = new CGRect (View.Center.X + 4, View.Center.Y + 35, 149, 115); 
      Sensor.highArea.areaRect = new CGRect (.512 * View.Bounds.Width, .561 * View.Bounds.Height, .465 * View.Bounds.Width, .202 * View.Bounds.Height);
    }

    public static void applyLowHighGestures(lowHighSensor lowArea, lowHighSensor highArea, LowHighArea lowHighSensors, sensor Sensor){
      lowArea.shortPress = new UITapGestureRecognizer (() => {
        ShowPopup(lowArea, lowHighSensors, Sensor);
      });
      lowArea.snapArea.AddGestureRecognizer(lowArea.shortPress);
      highArea.shortPress = new UITapGestureRecognizer (() => {
        ShowPopup(highArea, lowHighSensors, Sensor);
      });
      highArea.snapArea.AddGestureRecognizer(highArea.shortPress);
    }

    /// <summary>
    /// POPUP TO DETERMINE LOW/HIGH AREA ACTIONS
    /// </summary>
    /// <param name="pressedArea">LOCATION OF SENSOR</param>
    public static void ShowPopup(lowHighSensor lhSensor, LowHighArea lowHighSensors, sensor Sensor){
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }

      UIAlertController addDeviceSheet;

      addDeviceSheet = UIAlertController.Create (lhSensor.LabelTop.Text + " " + Util.Strings.ACTIONS, "", UIAlertControllerStyle.Alert);

      addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.Analyzer.ALARMS, UIAlertActionStyle.Default, (action) => {
        alarmRequestViewer(Sensor);
      }));

      addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.Analyzer.ADDSUBVIEW, UIAlertActionStyle.Default, (action) => {
        subviewOptionChosen(lhSensor);
      }));

      addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.Analyzer.REMOVESENSOR, UIAlertActionStyle.Default, (action) => {
        RemoveDevice (Sensor, lowHighSensors);
      }));

      addDeviceSheet.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));

      vc.PresentViewController (addDeviceSheet, true, null);
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
        if (splits[0].Equals("Superheat") && pressedArea.manifold.primarySensor.type == ESensorType.Vacuum) {
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
            //pressedArea.tableSubviews.Add(splits[0]);
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
    public static void sensorSwap(sensorGroup analyzerSensors,LowHighArea lowHighSensors, int position, CGPoint touchPoint, UIView View){
			int start = analyzerSensors.areaList.IndexOf(position);
			int swap = 0;
      bool removeLH = false;
			////CHECK LOCATION OF SUBVIEW WHEN TOUCH ENDED TO DETERMINE INDEX PLACEMENT
			if (analyzerSensors.snapRect1.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[0];
        analyzerSensors.areaList[0] = position;
        analyzerSensors.areaList[start] = swap;
        if (start > 3) {          
          swap = 0;
          removeLH = true;
        }
			} else if (analyzerSensors.snapRect2.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[1];
        analyzerSensors.areaList[1] = position;
        analyzerSensors.areaList[start] = swap;
        if (start > 3) {
          swap = 1;
          removeLH = true;
        }
			} else if (analyzerSensors.snapRect3.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[2];
        analyzerSensors.areaList[2] = position;
        analyzerSensors.areaList[start] = swap;
        if (start > 3) {
          swap = 2;
          removeLH = true;
        }
			} else if (analyzerSensors.snapRect4.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[3];
        analyzerSensors.areaList[3] = position;
        analyzerSensors.areaList[start] = swap;
        if (start > 3) {
          swap = 3;
          removeLH = true;
        }
			} else if (analyzerSensors.snapRect5.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[4];
        analyzerSensors.areaList[4] = position;
        analyzerSensors.areaList[start] = swap;
        if (start < 4) {
          swap = 4;
          removeLH = true;
        }
			} else if (analyzerSensors.snapRect6.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[5];
        analyzerSensors.areaList[5] = position;
        analyzerSensors.areaList[start] = swap;
        if (start < 4) {
          swap = 5;
          removeLH = true;
        }
			} else if (analyzerSensors.snapRect7.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[6];
        analyzerSensors.areaList[6] = position;
        analyzerSensors.areaList[start] = swap;
        if (start < 4) {
          swap = 6;
          removeLH = true;
        }
			} else if (analyzerSensors.snapRect8.Contains (touchPoint)) {
        swap = analyzerSensors.areaList[7];
        analyzerSensors.areaList[7] = position;
        analyzerSensors.areaList[start] = swap;
        if (start < 4) {
          swap = 7;
          removeLH = true;
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
      if (removeLH) {
        if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[start].snapArea.AccessibilityIdentifier &&
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[swap].snapArea.AccessibilityIdentifier) {
          analyzerSensors.viewList[start].topLabel.BackgroundColor = UIColor.Red;
          analyzerSensors.viewList[start].tLabelBottom.BackgroundColor = UIColor.Red;
          lowHighSensors.highArea.snapArea.AccessibilityIdentifier = analyzerSensors.viewList[start].snapArea.AccessibilityIdentifier;
          View.BringSubviewToFront(analyzerSensors.viewList[start].highArea.snapArea);
          if (!analyzerSensors.viewList[start].isManual) {
            if(analyzerSensors.viewList[start].currentSensor.device.isConnected){
              analyzerSensors.viewList[start].highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              analyzerSensors.viewList[start].highArea.Connection.Hidden = false;
              analyzerSensors.viewList[start].lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              analyzerSensors.viewList[start].lowArea.Connection.Hidden = false;
              analyzerSensors.viewList[start].highArea.connectionColor.BackgroundColor = UIColor.Green;
              analyzerSensors.viewList[start].lowArea.connectionColor.BackgroundColor = UIColor.Green;
              View.BringSubviewToFront(analyzerSensors.viewList[start].lowArea.Connection);
              View.BringSubviewToFront(analyzerSensors.viewList[start].highArea.Connection);
            } else {
              analyzerSensors.viewList[start].highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              analyzerSensors.viewList[start].highArea.Connection.Hidden = false;
              analyzerSensors.viewList[start].lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              analyzerSensors.viewList[start].lowArea.Connection.Hidden = false;
              analyzerSensors.viewList[start].highArea.connectionColor.BackgroundColor = UIColor.Red;
              analyzerSensors.viewList[start].lowArea.connectionColor.BackgroundColor = UIColor.Red;
              View.BringSubviewToFront(analyzerSensors.viewList[start].lowArea.Connection);
              View.BringSubviewToFront(analyzerSensors.viewList[start].highArea.Connection);
            }
          }
          analyzerSensors.viewList[start].highArea.snapArea.Hidden = false;
          analyzerSensors.viewList[start].highArea.tableSubviews = analyzerSensors.viewList[start].lowArea.tableSubviews;
          analyzerSensors.viewList[start].highArea.subviewTable.Source = new AnalyzerTableSource(analyzerSensors.viewList[start].highArea.tableSubviews,analyzerSensors.viewList[start].highArea);

          SubviewSwap(analyzerSensors.viewList[start].highArea.tableSubviews,analyzerSensors.viewList[start].highArea, analyzerSensors.viewList[start].lowArea);

          analyzerSensors.viewList[start].highArea.subviewTable.ReloadData();
          analyzerSensors.viewList[start].highArea.subviewTable.Hidden = false;

          analyzerSensors.viewList[start].lowArea.snapArea.Hidden = true;
          analyzerSensors.viewList[start].lowArea.subviewTable.Source = null;
          analyzerSensors.viewList[start].lowArea.tableSubviews = new List<string>();
          analyzerSensors.viewList[start].lowArea.subviewTable.ReloadData();
          analyzerSensors.viewList[start].lowArea.subviewTable.Hidden = true;
          analyzerSensors.viewList[start].lowArea.subviewHide.SetImage(null, UIControlState.Normal);

          if (analyzerSensors.viewList[start].lowArea.attachedSensor != null && analyzerSensors.viewList[start].highArea.attachedSensor != null) {
            analyzerSensors.viewList[start].lowArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            analyzerSensors.viewList[start].lowArea.attachedSensor.topLabel.TextColor = UIColor.Black;
            analyzerSensors.viewList[start].lowArea.attachedSensor.tLabelBottom.Hidden = true;
            analyzerSensors.viewList[start].lowArea.attachedSensor = null;

            analyzerSensors.viewList[start].highArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            analyzerSensors.viewList[start].highArea.attachedSensor.topLabel.TextColor = UIColor.Black;
            analyzerSensors.viewList[start].highArea.attachedSensor.tLabelBottom.Hidden = true;
            analyzerSensors.viewList[start].highArea.attachedSensor = null;
          }
          if (analyzerSensors.viewList[start].currentSensor != null) {
            analyzerSensors.viewList[start].lowArea.manifold = new Manifold(analyzerSensors.viewList[start].currentSensor);
            analyzerSensors.viewList[start].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[start].lowArea.ion, Fluid.EState.Dew);
            analyzerSensors.viewList[start].highArea.manifold = new Manifold(analyzerSensors.viewList[start].currentSensor);
            analyzerSensors.viewList[start].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[start].lowArea.ion, Fluid.EState.Dew);
          } else if (analyzerSensors.viewList[start].manualSensor != null) {
            analyzerSensors.viewList[start].lowArea.manifold = new Manifold(analyzerSensors.viewList[start].manualSensor);
            analyzerSensors.viewList[start].lowArea.manifold.primarySensor.unit = analyzerSensors.viewList[start].manualSensor.unit;
            analyzerSensors.viewList[start].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[start].lowArea.ion, Fluid.EState.Dew);

            analyzerSensors.viewList[start].highArea.manifold = new Manifold(analyzerSensors.viewList[start].manualSensor);
            analyzerSensors.viewList[start].highArea.manifold.primarySensor.unit = analyzerSensors.viewList[start].manualSensor.unit;
            analyzerSensors.viewList[start].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[start].lowArea.ion, Fluid.EState.Dew);
          }

          analyzerSensors.viewList[swap].topLabel.BackgroundColor = UIColor.Blue;
          analyzerSensors.viewList[swap].tLabelBottom.BackgroundColor = UIColor.Blue;

          if (!analyzerSensors.viewList[swap].isManual) {
            if(analyzerSensors.viewList[swap].currentSensor.device.isConnected){
              analyzerSensors.viewList[swap].highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              analyzerSensors.viewList[swap].highArea.Connection.Hidden = false;
              analyzerSensors.viewList[swap].lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              analyzerSensors.viewList[swap].lowArea.Connection.Hidden = false;
              analyzerSensors.viewList[swap].highArea.connectionColor.BackgroundColor = UIColor.Green;
              analyzerSensors.viewList[swap].lowArea.connectionColor.BackgroundColor = UIColor.Green;
              View.BringSubviewToFront(analyzerSensors.viewList[swap].lowArea.Connection);
              View.BringSubviewToFront(analyzerSensors.viewList[swap].highArea.Connection);
            } else {
              analyzerSensors.viewList[swap].highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              analyzerSensors.viewList[swap].highArea.Connection.Hidden = false;
              analyzerSensors.viewList[swap].lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              analyzerSensors.viewList[swap].lowArea.Connection.Hidden = false;
              analyzerSensors.viewList[swap].highArea.connectionColor.BackgroundColor = UIColor.Red;
              analyzerSensors.viewList[swap].lowArea.connectionColor.BackgroundColor = UIColor.Red;
              View.BringSubviewToFront(analyzerSensors.viewList[swap].lowArea.Connection);
              View.BringSubviewToFront(analyzerSensors.viewList[swap].highArea.Connection);
            }
          }
          lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = analyzerSensors.viewList[swap].snapArea.AccessibilityIdentifier;
          View.BringSubviewToFront(analyzerSensors.viewList[swap].lowArea.snapArea);

          analyzerSensors.viewList[swap].lowArea.snapArea.Hidden = false;
          analyzerSensors.viewList[swap].lowArea.tableSubviews = analyzerSensors.viewList[swap].highArea.tableSubviews;
          analyzerSensors.viewList[swap].lowArea.subviewTable.Source = new AnalyzerTableSource(analyzerSensors.viewList[swap].lowArea.tableSubviews,analyzerSensors.viewList[swap].lowArea);

          SubviewSwap(analyzerSensors.viewList[swap].lowArea.tableSubviews,analyzerSensors.viewList[swap].lowArea, analyzerSensors.viewList[swap].highArea);

          analyzerSensors.viewList[swap].lowArea.subviewTable.ReloadData();
          analyzerSensors.viewList[swap].lowArea.subviewTable.Hidden = false;

          analyzerSensors.viewList[swap].highArea.snapArea.Hidden = true;
          analyzerSensors.viewList[swap].highArea.subviewTable.Source = null;
          analyzerSensors.viewList[swap].highArea.tableSubviews = new List<string>();
          analyzerSensors.viewList[swap].highArea.subviewTable.ReloadData();
          analyzerSensors.viewList[swap].highArea.subviewTable.Hidden = true;
          analyzerSensors.viewList[swap].highArea.subviewHide.SetImage(null, UIControlState.Normal);

          if (analyzerSensors.viewList[swap].lowArea.attachedSensor != null && analyzerSensors.viewList[swap].highArea.attachedSensor != null) {
            analyzerSensors.viewList[swap].lowArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            analyzerSensors.viewList[swap].lowArea.attachedSensor.topLabel.TextColor = UIColor.Black;
            analyzerSensors.viewList[swap].lowArea.attachedSensor.tLabelBottom.Hidden = true;
            analyzerSensors.viewList[swap].lowArea.attachedSensor = null;

            analyzerSensors.viewList[swap].highArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            analyzerSensors.viewList[swap].highArea.attachedSensor.topLabel.TextColor = UIColor.Black;
            analyzerSensors.viewList[swap].highArea.attachedSensor.tLabelBottom.Hidden = true;
            analyzerSensors.viewList[swap].highArea.attachedSensor = null;
          }

          if (analyzerSensors.viewList[swap].currentSensor != null) {
            analyzerSensors.viewList[swap].lowArea.manifold = new Manifold(analyzerSensors.viewList[swap].currentSensor);
            analyzerSensors.viewList[swap].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[swap].lowArea.ion, Fluid.EState.Dew);
            analyzerSensors.viewList[swap].highArea.manifold = new Manifold(analyzerSensors.viewList[swap].currentSensor);
            analyzerSensors.viewList[swap].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[swap].lowArea.ion, Fluid.EState.Dew);
          } else if (analyzerSensors.viewList[swap].manualSensor != null) {
            analyzerSensors.viewList[swap].lowArea.manifold = new Manifold(analyzerSensors.viewList[swap].manualSensor);
            analyzerSensors.viewList[swap].lowArea.manifold.primarySensor.unit = analyzerSensors.viewList[swap].manualSensor.unit;
            analyzerSensors.viewList[swap].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[swap].lowArea.ion, Fluid.EState.Dew);

            analyzerSensors.viewList[swap].highArea.manifold = new Manifold(analyzerSensors.viewList[swap].manualSensor);
            analyzerSensors.viewList[swap].highArea.manifold.primarySensor.unit = analyzerSensors.viewList[swap].manualSensor.unit;
            analyzerSensors.viewList[swap].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[swap].lowArea.ion, Fluid.EState.Dew);
          }

        } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[start].snapArea.AccessibilityIdentifier &&
                   lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[swap].snapArea.AccessibilityIdentifier) {
          analyzerSensors.viewList[start].topLabel.BackgroundColor = UIColor.Blue;
          analyzerSensors.viewList[start].tLabelBottom.BackgroundColor = UIColor.Blue;

          if (!analyzerSensors.viewList[start].isManual) {
            if(analyzerSensors.viewList[start].currentSensor.device.isConnected){
              analyzerSensors.viewList[start].highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              analyzerSensors.viewList[start].highArea.Connection.Hidden = false;
              analyzerSensors.viewList[start].lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              analyzerSensors.viewList[start].lowArea.Connection.Hidden = false;
              analyzerSensors.viewList[start].highArea.connectionColor.BackgroundColor = UIColor.Green;
              analyzerSensors.viewList[start].lowArea.connectionColor.BackgroundColor = UIColor.Green;
              View.BringSubviewToFront(analyzerSensors.viewList[start].lowArea.Connection);
              View.BringSubviewToFront(analyzerSensors.viewList[start].highArea.Connection);
            } else {
              analyzerSensors.viewList[start].highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              analyzerSensors.viewList[start].highArea.Connection.Hidden = false;
              analyzerSensors.viewList[start].lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              analyzerSensors.viewList[start].lowArea.Connection.Hidden = false;
              analyzerSensors.viewList[start].highArea.connectionColor.BackgroundColor = UIColor.Red;
              analyzerSensors.viewList[start].lowArea.connectionColor.BackgroundColor = UIColor.Red;
              View.BringSubviewToFront(analyzerSensors.viewList[start].lowArea.Connection);
              View.BringSubviewToFront(analyzerSensors.viewList[start].highArea.Connection);
            }
          }
          lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = analyzerSensors.viewList[start].snapArea.AccessibilityIdentifier;

          analyzerSensors.viewList[start].lowArea.snapArea.Hidden = false;
          analyzerSensors.viewList[start].lowArea.tableSubviews = analyzerSensors.viewList[start].highArea.tableSubviews;
          analyzerSensors.viewList[start].lowArea.subviewTable.Source = new AnalyzerTableSource(analyzerSensors.viewList[start].lowArea.tableSubviews,analyzerSensors.viewList[start].lowArea);

          SubviewSwap(analyzerSensors.viewList[start].lowArea.tableSubviews,analyzerSensors.viewList[start].lowArea, analyzerSensors.viewList[start].highArea);

          analyzerSensors.viewList[start].lowArea.subviewTable.ReloadData();
          analyzerSensors.viewList[start].lowArea.subviewTable.Hidden = false;

          View.BringSubviewToFront(analyzerSensors.viewList[start].lowArea.snapArea);

          analyzerSensors.viewList[start].highArea.snapArea.Hidden = true;
          analyzerSensors.viewList[start].highArea.subviewTable.Source = null;
          analyzerSensors.viewList[start].highArea.tableSubviews = new List<string>();
          analyzerSensors.viewList[start].highArea.subviewTable.ReloadData();
          analyzerSensors.viewList[start].highArea.subviewTable.Hidden = true;
          analyzerSensors.viewList[start].highArea.subviewHide.SetImage(null, UIControlState.Normal);

          //Remove any secondary sensors attached for low and high side
          if (analyzerSensors.viewList[start].lowArea.attachedSensor != null && analyzerSensors.viewList[start].highArea.attachedSensor != null) {
            analyzerSensors.viewList[start].lowArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            analyzerSensors.viewList[start].lowArea.attachedSensor.topLabel.TextColor = UIColor.Black;
            analyzerSensors.viewList[start].lowArea.attachedSensor.tLabelBottom.Hidden = true;
            analyzerSensors.viewList[start].lowArea.attachedSensor = null;

            analyzerSensors.viewList[start].highArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            analyzerSensors.viewList[start].highArea.attachedSensor.topLabel.TextColor = UIColor.Black;
            analyzerSensors.viewList[start].highArea.attachedSensor.tLabelBottom.Hidden = true;
            analyzerSensors.viewList[start].highArea.attachedSensor = null;
          }
          if (analyzerSensors.viewList[start].currentSensor != null) {
            analyzerSensors.viewList[start].lowArea.manifold = new Manifold(analyzerSensors.viewList[start].currentSensor);
            analyzerSensors.viewList[start].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[start].lowArea.ion, Fluid.EState.Dew);
            analyzerSensors.viewList[start].highArea.manifold = new Manifold(analyzerSensors.viewList[start].currentSensor);
            analyzerSensors.viewList[start].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[start].lowArea.ion, Fluid.EState.Dew);
          } else if (analyzerSensors.viewList[start].manualSensor != null) {
            analyzerSensors.viewList[start].lowArea.manifold = new Manifold(analyzerSensors.viewList[start].manualSensor);
            analyzerSensors.viewList[start].lowArea.manifold.primarySensor.unit = analyzerSensors.viewList[start].manualSensor.unit;
            analyzerSensors.viewList[start].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[start].lowArea.ion, Fluid.EState.Dew);

            analyzerSensors.viewList[start].highArea.manifold = new Manifold(analyzerSensors.viewList[start].manualSensor);
            analyzerSensors.viewList[start].highArea.manifold.primarySensor.unit = analyzerSensors.viewList[start].manualSensor.unit;
            analyzerSensors.viewList[start].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[start].lowArea.ion, Fluid.EState.Dew);
          }
          analyzerSensors.viewList[swap].topLabel.BackgroundColor = UIColor.Red;
          analyzerSensors.viewList[swap].tLabelBottom.BackgroundColor = UIColor.Red;

          if (!analyzerSensors.viewList[swap].isManual) {
            if(analyzerSensors.viewList[swap].currentSensor.device.isConnected){
              analyzerSensors.viewList[swap].highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              analyzerSensors.viewList[swap].highArea.Connection.Hidden = false;
              analyzerSensors.viewList[swap].lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              analyzerSensors.viewList[swap].lowArea.Connection.Hidden = false;
              analyzerSensors.viewList[swap].highArea.connectionColor.BackgroundColor = UIColor.Green;
              analyzerSensors.viewList[swap].lowArea.connectionColor.BackgroundColor = UIColor.Green;
              View.BringSubviewToFront(analyzerSensors.viewList[swap].lowArea.Connection);
              View.BringSubviewToFront(analyzerSensors.viewList[swap].highArea.Connection);
            } else {
              analyzerSensors.viewList[swap].highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              analyzerSensors.viewList[swap].highArea.Connection.Hidden = false;
              analyzerSensors.viewList[swap].lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              analyzerSensors.viewList[swap].lowArea.Connection.Hidden = false;
              analyzerSensors.viewList[swap].highArea.connectionColor.BackgroundColor = UIColor.Red;
              analyzerSensors.viewList[swap].lowArea.connectionColor.BackgroundColor = UIColor.Red;
              View.BringSubviewToFront(analyzerSensors.viewList[swap].lowArea.Connection);
              View.BringSubviewToFront(analyzerSensors.viewList[swap].highArea.Connection);
            }
          }
          lowHighSensors.highArea.snapArea.AccessibilityIdentifier = analyzerSensors.viewList[swap].snapArea.AccessibilityIdentifier;
          View.BringSubviewToFront(analyzerSensors.viewList[swap].highArea.snapArea);

          analyzerSensors.viewList[swap].highArea.snapArea.Hidden = false;
          analyzerSensors.viewList[swap].highArea.tableSubviews = analyzerSensors.viewList[swap].lowArea.tableSubviews;
          analyzerSensors.viewList[swap].highArea.subviewTable.Source = new AnalyzerTableSource(analyzerSensors.viewList[swap].highArea.tableSubviews,analyzerSensors.viewList[swap].highArea);

          SubviewSwap(analyzerSensors.viewList[swap].highArea.tableSubviews,analyzerSensors.viewList[swap].highArea, analyzerSensors.viewList[swap].lowArea);

          analyzerSensors.viewList[swap].highArea.subviewTable.ReloadData();
          analyzerSensors.viewList[swap].highArea.subviewTable.Hidden = false;

          analyzerSensors.viewList[swap].lowArea.snapArea.Hidden = true;
          analyzerSensors.viewList[swap].lowArea.subviewTable.Source = null;
          analyzerSensors.viewList[swap].lowArea.tableSubviews = new List<string>();
          analyzerSensors.viewList[swap].lowArea.subviewTable.ReloadData();
          analyzerSensors.viewList[swap].lowArea.subviewTable.Hidden = true;
          analyzerSensors.viewList[swap].lowArea.subviewHide.SetImage(null, UIControlState.Normal);

          ///Remove any secondary sensors attached for low and high side
          if (analyzerSensors.viewList[swap].lowArea.attachedSensor != null && analyzerSensors.viewList[swap].highArea.attachedSensor != null) {
            analyzerSensors.viewList[swap].lowArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            analyzerSensors.viewList[swap].lowArea.attachedSensor.topLabel.TextColor = UIColor.Black;
            analyzerSensors.viewList[swap].lowArea.attachedSensor.tLabelBottom.Hidden = true;
            analyzerSensors.viewList[swap].lowArea.attachedSensor = null;

            analyzerSensors.viewList[swap].highArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            analyzerSensors.viewList[swap].highArea.attachedSensor.topLabel.TextColor = UIColor.Black;
            analyzerSensors.viewList[swap].highArea.attachedSensor.tLabelBottom.Hidden = true;
            analyzerSensors.viewList[swap].highArea.attachedSensor = null;
          }

          if (analyzerSensors.viewList[swap].currentSensor != null) {
            analyzerSensors.viewList[swap].lowArea.manifold = new Manifold(analyzerSensors.viewList[swap].currentSensor);
            analyzerSensors.viewList[swap].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[swap].lowArea.ion, Fluid.EState.Dew);
            analyzerSensors.viewList[swap].highArea.manifold = new Manifold(analyzerSensors.viewList[swap].currentSensor);
            analyzerSensors.viewList[swap].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[swap].lowArea.ion, Fluid.EState.Dew);
          } else if (analyzerSensors.viewList[swap].manualSensor != null) {
            analyzerSensors.viewList[swap].lowArea.manifold = new Manifold(analyzerSensors.viewList[swap].manualSensor);
            analyzerSensors.viewList[swap].lowArea.manifold.primarySensor.unit = analyzerSensors.viewList[swap].manualSensor.unit;
            analyzerSensors.viewList[swap].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[swap].lowArea.ion, Fluid.EState.Dew);

            analyzerSensors.viewList[swap].highArea.manifold = new Manifold(analyzerSensors.viewList[swap].manualSensor);
            analyzerSensors.viewList[swap].highArea.manifold.primarySensor.unit = analyzerSensors.viewList[swap].manualSensor.unit;
            analyzerSensors.viewList[swap].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[swap].lowArea.ion, Fluid.EState.Dew);
          }

        } else {
          analyzerSensors.viewList[start].lowArea.snapArea.Hidden = true;
          analyzerSensors.viewList[start].highArea.snapArea.Hidden = true;
          analyzerSensors.viewList[start].topLabel.BackgroundColor = UIColor.Clear;
          analyzerSensors.viewList[start].topLabel.TextColor = UIColor.Black;
          analyzerSensors.viewList[start].tLabelBottom.Hidden = true;

          if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[start].snapArea.AccessibilityIdentifier) {
            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
          }
          if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[start].snapArea.AccessibilityIdentifier) {
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
          }

          analyzerSensors.viewList[start].lowArea.subviewTable.Source = null;
          analyzerSensors.viewList[start].lowArea.tableSubviews = new List<string>();
          analyzerSensors.viewList[start].lowArea.subviewTable.ReloadData();
          analyzerSensors.viewList[start].lowArea.subviewTable.Hidden = true;
          analyzerSensors.viewList[start].lowArea.subviewHide.SetImage(null, UIControlState.Normal);

          analyzerSensors.viewList[start].highArea.subviewTable.Source = null;
          analyzerSensors.viewList[start].highArea.tableSubviews = new List<string>();
          analyzerSensors.viewList[start].highArea.subviewTable.ReloadData();
          analyzerSensors.viewList[start].highArea.subviewTable.Hidden = true;
          analyzerSensors.viewList[start].highArea.subviewHide.SetImage(null, UIControlState.Normal);

          //Remove any secondary sensors attached for low and high side
          if (analyzerSensors.viewList[swap].lowArea.attachedSensor != null && analyzerSensors.viewList[swap].highArea.attachedSensor != null) {
            analyzerSensors.viewList[swap].lowArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            analyzerSensors.viewList[swap].lowArea.attachedSensor.topLabel.TextColor = UIColor.Black;
            analyzerSensors.viewList[swap].lowArea.attachedSensor.tLabelBottom.Hidden = true;
            analyzerSensors.viewList[swap].lowArea.attachedSensor = null;

            analyzerSensors.viewList[swap].highArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            analyzerSensors.viewList[swap].highArea.attachedSensor.topLabel.TextColor = UIColor.Black;
            analyzerSensors.viewList[swap].highArea.attachedSensor.tLabelBottom.Hidden = true;
            analyzerSensors.viewList[swap].highArea.attachedSensor = null;
          } else {
            for (int i = 0; i < 8; i++) {
              if (analyzerSensors.viewList[i].lowArea.attachedSensor != null && analyzerSensors.viewList[i].lowArea.attachedSensor == analyzerSensors.viewList[swap]) {
                analyzerSensors.viewList[i].lowArea.attachedSensor = null;
                analyzerSensors.viewList[i].highArea.attachedSensor = null;

                if (analyzerSensors.viewList[i].currentSensor != null) {
                  analyzerSensors.viewList[i].lowArea.manifold = new Manifold(analyzerSensors.viewList[i].currentSensor);
                  analyzerSensors.viewList[i].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].lowArea.ion, Fluid.EState.Dew);
                  analyzerSensors.viewList[i].highArea.manifold = new Manifold(analyzerSensors.viewList[i].currentSensor);
                  analyzerSensors.viewList[i].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].lowArea.ion, Fluid.EState.Dew);
                } else if (analyzerSensors.viewList[i].manualSensor != null) {
                  analyzerSensors.viewList[i].lowArea.manifold = new Manifold(analyzerSensors.viewList[i].manualSensor);
                  analyzerSensors.viewList[i].lowArea.manifold.primarySensor.unit = AnalyserUtilities.getManualUnit(analyzerSensors.viewList[i].manualSensor.type,analyzerSensors.viewList[i].bottomLabel.Text);
                  analyzerSensors.viewList[i].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].lowArea.ion, Fluid.EState.Dew);

                  analyzerSensors.viewList[i].highArea.manifold = new Manifold(analyzerSensors.viewList[i].manualSensor);
                  analyzerSensors.viewList[i].highArea.manifold.primarySensor.unit = AnalyserUtilities.getManualUnit(analyzerSensors.viewList[i].manualSensor.type,analyzerSensors.viewList[i].bottomLabel.Text);
                  analyzerSensors.viewList[i].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[i].lowArea.ion, Fluid.EState.Dew);
                }
              }
            }
          }
          if (analyzerSensors.viewList[swap].currentSensor != null) {
            analyzerSensors.viewList[swap].lowArea.manifold = new Manifold(analyzerSensors.viewList[swap].currentSensor);
            analyzerSensors.viewList[swap].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[swap].lowArea.ion, Fluid.EState.Dew);
            analyzerSensors.viewList[swap].highArea.manifold = new Manifold(analyzerSensors.viewList[swap].currentSensor);
            analyzerSensors.viewList[swap].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[swap].lowArea.ion, Fluid.EState.Dew);
          } else if (analyzerSensors.viewList[swap].manualSensor != null) {
            analyzerSensors.viewList[swap].lowArea.manifold = new Manifold(analyzerSensors.viewList[swap].manualSensor);
            analyzerSensors.viewList[swap].lowArea.manifold.primarySensor.unit = AnalyserUtilities.getManualUnit(analyzerSensors.viewList[swap].manualSensor.type,analyzerSensors.viewList[swap].bottomLabel.Text);
            analyzerSensors.viewList[swap].lowArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[swap].lowArea.ion, Fluid.EState.Dew);

            analyzerSensors.viewList[swap].highArea.manifold = new Manifold(analyzerSensors.viewList[swap].manualSensor);
            analyzerSensors.viewList[swap].highArea.manifold.primarySensor.unit = AnalyserUtilities.getManualUnit(analyzerSensors.viewList[swap].manualSensor.type,analyzerSensors.viewList[swap].bottomLabel.Text);
            analyzerSensors.viewList[swap].highArea.manifold.ptChart = PTChart.New(analyzerSensors.viewList[swap].lowArea.ion, Fluid.EState.Dew);
          }

          analyzerSensors.viewList[swap].lowArea.snapArea.Hidden = true;
          analyzerSensors.viewList[swap].highArea.snapArea.Hidden = true;
          analyzerSensors.viewList[swap].topLabel.BackgroundColor = UIColor.Clear;
          analyzerSensors.viewList[swap].topLabel.TextColor = UIColor.Black;
          analyzerSensors.viewList[swap].tLabelBottom.Hidden = true;

          if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[swap].snapArea.AccessibilityIdentifier) {
            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
          }
          if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[swap].snapArea.AccessibilityIdentifier) {
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
          }

          analyzerSensors.viewList[swap].lowArea.subviewTable.Source = null;
          analyzerSensors.viewList[swap].lowArea.tableSubviews = new List<string>();
          analyzerSensors.viewList[swap].lowArea.subviewTable.ReloadData();
          analyzerSensors.viewList[swap].lowArea.subviewTable.Hidden = true;
          analyzerSensors.viewList[swap].lowArea.subviewHide.SetImage(null, UIControlState.Normal);

          analyzerSensors.viewList[swap].highArea.subviewTable.Source = null;
          analyzerSensors.viewList[swap].highArea.tableSubviews = new List<string>();
          analyzerSensors.viewList[swap].highArea.subviewTable.ReloadData();
          analyzerSensors.viewList[swap].highArea.subviewTable.Hidden = true;
          analyzerSensors.viewList[swap].highArea.subviewHide.SetImage(null, UIControlState.Normal);
        }
      }
		}
    /// <summary>
    /// Checks if the sensor being moved is moving from low to high or vice versa and if it is associated to a low or high area
    /// </summary>
    /// <param name="analyzerSensors">Analyzer sensors.</param>
    /// <param name="lowHighSensors">Low high sensors.</param>
    /// <param name="position">Position.</param>
    /// <param name="touchPoint">Touch point.</param>
    /// <param name="View">View.</param>
    public static void LHSwapCheck(sensorGroup analyzerSensors,LowHighArea lowHighSensors, int position, CGPoint touchPoint, UIView View){
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
        if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[start].snapArea.AccessibilityIdentifier &&
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[swap].snapArea.AccessibilityIdentifier) {
          Console.WriteLine("low and high areas are swapping with the low area being the start");
          LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, View);
        } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[start].snapArea.AccessibilityIdentifier &&
                   lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[swap].snapArea.AccessibilityIdentifier) {
          Console.WriteLine("low and high areas are swapping with the high area being the start");
          LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, View);
        } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[start].snapArea.AccessibilityIdentifier ||
                   lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[start].snapArea.AccessibilityIdentifier) {
          Console.WriteLine("low or high area is starting a swap with a sensor not of the opposite peer");
          LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, View);
        } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[swap].snapArea.AccessibilityIdentifier ||
                   lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == analyzerSensors.viewList[swap].snapArea.AccessibilityIdentifier) {
          Console.WriteLine("low or high area is a swapie with a sensor not of the opposite peer");
          LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, View, analyzerSensors.viewList[swap]);
        } else {
          bool foundAssociation = false;
          foreach (var item in analyzerSensors.viewList){
            if (item.lowArea.attachedSensor != null) {
              if (item.lowArea.attachedSensor.currentSensor != null && (item.lowArea.attachedSensor.currentSensor == analyzerSensors.viewList[start].currentSensor || item.lowArea.attachedSensor.currentSensor == analyzerSensors.viewList[swap].currentSensor)) {
                LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, View, item);
                foundAssociation = true;
              } else if (item.lowArea.attachedSensor.manualSensor != null && (item.lowArea.attachedSensor.manualSensor == analyzerSensors.viewList[start].manualSensor || item.lowArea.attachedSensor.manualSensor == analyzerSensors.viewList[swap].manualSensor)){
                LHSwapAlert(analyzerSensors, lowHighSensors, position, touchPoint, View, item);
                foundAssociation = true;
              }
            }
          }
          if (foundAssociation) {
            return;
          } else {
            sensorSwap(analyzerSensors, lowHighSensors, position, touchPoint, View);
          }
        }
      } else {
        sensorSwap (analyzerSensors, lowHighSensors, position, touchPoint, View);
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
    public static void LHSwapAlert(sensorGroup analyzerSensors,LowHighArea lowHighSensors, int position, CGPoint touchPoint, UIView View, sensor item = null){
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }

      UIAlertController addDeviceSheet;

      addDeviceSheet = UIAlertController.Create(Util.Strings.Analyzer.ACTION, "Do you want to remove the current sensor setup?", UIAlertControllerStyle.Alert);
      addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {

        if(item != null){
          if(item.lowArea.currentSensor != null){
            item.lowArea.manifold = new Manifold(item.lowArea.currentSensor);
            item.highArea.manifold = new Manifold(item.highArea.currentSensor);
          } else if (item.lowArea.manualSensor != null){
            item.lowArea.manifold = new Manifold(item.lowArea.manualSensor);         
            item.highArea.manifold = new Manifold(item.highArea.manualSensor);
          }
          item.lowArea.manifold.SetSecondarySensor(null);
          item.highArea.manifold.SetSecondarySensor(null);
          item.lowArea.attachedSensor = null;
          item.highArea.attachedSensor = null;
        }
        sensorSwap (analyzerSensors, lowHighSensors, position, touchPoint, View);
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
      ////MOVE SENSORS BASED ON THEIR LOCATION
      for (int i = 0; i < 8; i++) {
        if (analyzerSensors.areaList [i] == 1) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea1.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (analyzerSensors.areaList [i] == 2) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea2.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (analyzerSensors.areaList [i] == 3) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea3.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (analyzerSensors.areaList[i] == 4) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea4.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (analyzerSensors.areaList[i] == 5) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea5.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (analyzerSensors.areaList[i] == 6) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea6.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (analyzerSensors.areaList[i] == 7) {
          UIView.Animate(.3,0, UIViewAnimationOptions.CurveEaseInOut,
            () =>{ 
              analyzerSensors.snapArea7.snapArea.Center = analyzerSensors.locationList[i];
            },() => {});
        } else if (analyzerSensors.areaList[i] == 8) {
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
     public static void replaceAlert(string message, int type, sensor Sensor, sensor removeSensor, LowHighArea lowHighSensors, sensorGroup analyzerSensors, UIView View){
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }

      if (Sensor.isManual.Equals(true) || removeSensor.isManual.Equals(true)) {
        bool spotOpen = secondarySlotSpot(Sensor, removeSensor,analyzerSensors, type);

        if (Sensor.isManual.Equals(true) && removeSensor.isManual.Equals(true)) {         
          if (Sensor.manualSensor.type == removeSensor.manualSensor.type) {
            spotOpen = true;
          } else {
            if (Sensor.manualSensor.type == ESensorType.Vacuum || removeSensor.manualSensor.type == ESensorType.Vacuum) {              
            } else {
              message = "Do you want to add this " + Sensor.manualSensor.type.ToString() + " sensor to the current " + removeSensor.manualSensor.type.ToString() + " sensor?";
            }
          }
        } else if (Sensor.isManual.Equals(true) && removeSensor.isManual.Equals(false)) {
          if(Sensor.manualSensor.type == removeSensor.currentSensor.type){
            spotOpen = true;
          }else{
            if(Sensor.manualSensor.type == ESensorType.Vacuum || removeSensor.currentSensor.type == ESensorType.Vacuum){
            } else {
              message = "Do you want to add this " + Sensor.manualSensor.type.ToString() + " sensor to the current " + removeSensor.currentSensor.type.ToString() + " sensor?";
            }
          }
        } else if (Sensor.isManual.Equals(false) && removeSensor.isManual.Equals(true)) {         
          if (Sensor.currentSensor.type == removeSensor.manualSensor.type) {
            spotOpen = true;
          } else {
            if(Sensor.currentSensor.type == ESensorType.Vacuum || removeSensor.manualSensor.type == ESensorType.Vacuum){
            } else {
              message = "Do you want to add this " + Sensor.currentSensor.type.ToString() + " sensor to the current " + removeSensor.manualSensor.type.ToString() + " sensor?";
            }
          }
        }

        if (spotOpen.Equals(false)) {
          UIAlertController noneAvailable;
          noneAvailable = UIAlertController.Create(Util.Strings.Analyzer.CANTADD, Util.Strings.Analyzer.SAMESIDE, UIAlertControllerStyle.Alert);
          noneAvailable.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));
          vc.PresentViewController(noneAvailable, true, null);
          return;
        }
        UIAlertController addDeviceSheet;

        addDeviceSheet = UIAlertController.Create(Util.Strings.Analyzer.ACTION, message, UIAlertControllerStyle.Alert);
        addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
          if(removeSensor.lowArea.attachedSensor != null){
            Console.WriteLine("low high sensor has a linked sensor already");
            Console.WriteLine("attached sensor is from device " + removeSensor.lowArea.attachedSensor.manualSensor.name);
            removeSensor.lowArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            removeSensor.lowArea.attachedSensor.tLabelBottom.BackgroundColor = UIColor.Clear;
            removeSensor.lowArea.attachedSensor.topLabel.TextColor = UIColor.Black;

            removeSensor.highArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            removeSensor.highArea.attachedSensor.tLabelBottom.BackgroundColor = UIColor.Clear;
            removeSensor.highArea.attachedSensor.topLabel.TextColor = UIColor.Black;

            removeSensor.lowArea.attachedSensor = null;
            removeSensor.highArea.attachedSensor = null;
          }
          if (Sensor.isManual.Equals(true) && removeSensor.isManual.Equals(true)) {
            Console.WriteLine("Both sensors are manual");
            if (removeSensor.manualSensor.type == ESensorType.Pressure && Sensor.manualSensor.type == ESensorType.Temperature) {
              Console.WriteLine("low high sensor is pressure and adding sensor is temperature");
              removeSensor.lowArea.manifold.SetSecondarySensor(Sensor.manualSensor);
              removeSensor.highArea.manifold.SetSecondarySensor(Sensor.manualSensor);
              Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
              Sensor.topLabel.TextColor = UIColor.White;
              Sensor.tLabelBottom.BackgroundColor = removeSensor.tLabelBottom.BackgroundColor;
              Sensor.tLabelBottom.Hidden = false;
              removeSensor.lowArea.attachedSensor = Sensor;
              removeSensor.highArea.attachedSensor = Sensor;
            } else if (removeSensor.manualSensor.type == ESensorType.Temperature && Sensor.manualSensor.type == ESensorType.Pressure) {
              Console.WriteLine("low high sensor is temperature and adding sensor is pressure");
              removeSensor.lowArea.manifold = new Manifold(Sensor.manualSensor);
              removeSensor.lowArea.manifold.SetSecondarySensor(removeSensor.manualSensor);
              removeSensor.lowArea.manifold.ptChart = PTChart.New(removeSensor.lowArea.ion, Fluid.EState.Dew);
              removeSensor.highArea.manifold = new Manifold(Sensor.manualSensor);
              removeSensor.highArea.manifold.SetSecondarySensor(removeSensor.manualSensor);
              removeSensor.highArea.manifold.ptChart = PTChart.New(removeSensor.highArea.ion, Fluid.EState.Dew);
              Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
              Sensor.topLabel.TextColor = UIColor.White;
              Sensor.tLabelBottom.BackgroundColor = removeSensor.tLabelBottom.BackgroundColor;
              Sensor.tLabelBottom.Hidden = false;
              removeSensor.lowArea.attachedSensor = Sensor;
              removeSensor.highArea.attachedSensor = Sensor;
            } else {
              Console.WriteLine("dealing with a vacuum sensor");
              if(type == 1){
                replaceLowUnattached(Sensor, removeSensor, lowHighSensors, analyzerSensors, View);
              } else if (type == 2){
                replaceHighUnattached(Sensor, removeSensor, lowHighSensors, analyzerSensors, View);
              } else if (type == 3) {
                replaceLowAttached(Sensor, removeSensor, lowHighSensors, analyzerSensors, View);
              } else if (type ==4){
                replaceHighAttached(Sensor, removeSensor, lowHighSensors, analyzerSensors, View);
              }
            }
          } else if (Sensor.isManual.Equals(true) && removeSensor.isManual.Equals(false)) {
            Console.WriteLine("low high sensor is manual and adding sensor is not");
            if (removeSensor.currentSensor.type == ESensorType.Pressure && Sensor.manualSensor.type == ESensorType.Temperature) {
              Console.WriteLine("low high sensor is pressure and adding sensor is temperature");
              removeSensor.lowArea.manifold.SetSecondarySensor(Sensor.manualSensor);
              removeSensor.highArea.manifold.SetSecondarySensor(Sensor.manualSensor);
              Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
              Sensor.topLabel.TextColor = UIColor.White;
              Sensor.tLabelBottom.BackgroundColor = removeSensor.tLabelBottom.BackgroundColor;
              Sensor.tLabelBottom.Hidden = false;
              removeSensor.lowArea.attachedSensor = Sensor;
              removeSensor.highArea.attachedSensor = Sensor;
            } else if (removeSensor.currentSensor.type == ESensorType.Temperature && Sensor.manualSensor.type == ESensorType.Pressure) {
              Console.WriteLine("low high sensor is temperature and adding sensor is pressure");
              removeSensor.lowArea.manifold = new Manifold(Sensor.manualSensor);
              removeSensor.lowArea.manifold.SetSecondarySensor(removeSensor.currentSensor);
              removeSensor.lowArea.manifold.ptChart = PTChart.New(removeSensor.lowArea.ion, Fluid.EState.Dew);
              removeSensor.highArea.manifold = new Manifold(Sensor.manualSensor);
              removeSensor.highArea.manifold.SetSecondarySensor(removeSensor.currentSensor);
              removeSensor.highArea.manifold.ptChart = PTChart.New(removeSensor.highArea.ion, Fluid.EState.Dew);
              Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
              Sensor.topLabel.TextColor = UIColor.White;
              Sensor.tLabelBottom.BackgroundColor = removeSensor.tLabelBottom.BackgroundColor;
              Sensor.tLabelBottom.Hidden = false;
              removeSensor.lowArea.attachedSensor = Sensor;
              removeSensor.highArea.attachedSensor = Sensor;
            } else {
              if(type == 1){
                replaceLowUnattached(Sensor, removeSensor, lowHighSensors, analyzerSensors, View);
              } else if (type == 2){
                replaceHighUnattached(Sensor, removeSensor, lowHighSensors, analyzerSensors, View);
              } else if (type == 3) {
                replaceLowAttached(Sensor, removeSensor, lowHighSensors, analyzerSensors, View);
              } else if (type ==4){
                replaceHighAttached(Sensor, removeSensor, lowHighSensors, analyzerSensors, View);
              }
            }
          } else if (Sensor.isManual.Equals(false) && removeSensor.isManual.Equals(true)) {
            Console.WriteLine("low high sensor is not manual and adding sensor is manual");
            if (removeSensor.manualSensor.type == ESensorType.Pressure && Sensor.currentSensor.type == ESensorType.Temperature) {
              Console.WriteLine("low high sensor is pressure and adding sensor is temperature");
              removeSensor.lowArea.manifold.SetSecondarySensor(Sensor.currentSensor);
              removeSensor.highArea.manifold.SetSecondarySensor(Sensor.currentSensor);
              Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
              Sensor.topLabel.TextColor = UIColor.White;
              Sensor.tLabelBottom.BackgroundColor = removeSensor.tLabelBottom.BackgroundColor;
              Sensor.tLabelBottom.Hidden = false;
              removeSensor.lowArea.attachedSensor = Sensor;
              removeSensor.highArea.attachedSensor = Sensor;
            } else if (removeSensor.manualSensor.type == ESensorType.Temperature && Sensor.currentSensor.type == ESensorType.Pressure) {
              Console.WriteLine("low high sensor is temperature and adding sensor is pressure");
              removeSensor.lowArea.manifold = new Manifold(Sensor.currentSensor);
              removeSensor.lowArea.manifold.SetSecondarySensor(removeSensor.manualSensor);
              removeSensor.lowArea.manifold.ptChart = PTChart.New(removeSensor.lowArea.ion, Fluid.EState.Dew);
              removeSensor.highArea.manifold = new Manifold(Sensor.currentSensor);
              removeSensor.highArea.manifold.SetSecondarySensor(removeSensor.manualSensor);
              removeSensor.highArea.manifold.ptChart = PTChart.New(removeSensor.highArea.ion, Fluid.EState.Dew);
              Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
              Sensor.topLabel.TextColor = UIColor.White;
              Sensor.tLabelBottom.BackgroundColor = removeSensor.tLabelBottom.BackgroundColor;
              Sensor.tLabelBottom.Hidden = false;
              removeSensor.lowArea.attachedSensor = Sensor;
              removeSensor.highArea.attachedSensor = Sensor;
            } else {
              if(type == 1){
                replaceLowUnattached(Sensor, removeSensor, lowHighSensors, analyzerSensors, View);
              } else if (type == 2){
                replaceHighUnattached(Sensor, removeSensor, lowHighSensors, analyzerSensors, View);
              } else if (type == 3) {
                replaceLowAttached(Sensor, removeSensor, lowHighSensors, analyzerSensors, View);
              } else if (type ==4){
                replaceHighAttached(Sensor, removeSensor, lowHighSensors, analyzerSensors, View);
              }
            }
          }
        }));
        addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));

        vc.PresentViewController(addDeviceSheet, true, null);
      } else {

        if (removeSensor.currentSensor.type == ION.Core.Sensors.ESensorType.Pressure && Sensor.currentSensor.type == ION.Core.Sensors.ESensorType.Temperature) {
          var spotOpen = secondarySlotSpot(Sensor, removeSensor,analyzerSensors, type);
          if (spotOpen.Equals(false)) {
            UIAlertController noneAvailable;
            noneAvailable = UIAlertController.Create(Util.Strings.Analyzer.CANTADD, Util.Strings.Analyzer.SAMESIDE, UIAlertControllerStyle.Alert);
            noneAvailable.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));
            vc.PresentViewController(noneAvailable, true, null);
            return;
          }
          message = Util.Strings.Analyzer.ADDTEMP;
        } else if (removeSensor.currentSensor.type == ION.Core.Sensors.ESensorType.Temperature && Sensor.currentSensor.type == ION.Core.Sensors.ESensorType.Pressure) {
          var spotOpen = secondarySlotSpot(Sensor, removeSensor,analyzerSensors, type);
          if (spotOpen.Equals(false)) {
            UIAlertController noneAvailable;
            noneAvailable = UIAlertController.Create(Util.Strings.Analyzer.CANTADD, Util.Strings.Analyzer.SAMESIDE, UIAlertControllerStyle.Alert);
            noneAvailable.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));
            vc.PresentViewController(noneAvailable, true, null);
            return;
          }
          message = Util.Strings.Analyzer.ADDPRESS;
        }

        UIAlertController addDeviceSheet;

        addDeviceSheet = UIAlertController.Create(Util.Strings.Analyzer.ACTION, message, UIAlertControllerStyle.Alert);

        addDeviceSheet.AddAction(UIAlertAction.Create(Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
          if (Sensor.isManual.Equals(false)) {
            if (Sensor.currentSensor.device.isConnected) {
              Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Green;
              Sensor.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              Sensor.highArea.connectionColor.BackgroundColor = UIColor.Green;
              Sensor.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
            } else {
              Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Red;
              Sensor.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              Sensor.highArea.connectionColor.BackgroundColor = UIColor.Red;
              Sensor.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
            }
          }
          if(removeSensor.lowArea.attachedSensor != null){
            Console.WriteLine("lh sensor has attached sensor already and both are real devices");
            removeSensor.lowArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            removeSensor.lowArea.attachedSensor.tLabelBottom.BackgroundColor = UIColor.Clear;
            removeSensor.lowArea.attachedSensor.topLabel.TextColor = UIColor.Black;

            removeSensor.highArea.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
            removeSensor.highArea.attachedSensor.tLabelBottom.BackgroundColor = UIColor.Clear;
            removeSensor.highArea.attachedSensor.topLabel.TextColor = UIColor.Black;

            removeSensor.lowArea.attachedSensor = null;
            removeSensor.highArea.attachedSensor = null;
          }
          if (removeSensor.currentSensor.type == ION.Core.Sensors.ESensorType.Pressure && Sensor.currentSensor.type == ION.Core.Sensors.ESensorType.Temperature) {
            //Console.WriteLine("Adding temp device " + Sensor.currentSensor.device.name + "'s sensor as device " + removeSensor.currentSensor.device.name + "'s secondary sensor");
            removeSensor.lowArea.manifold.SetSecondarySensor(Sensor.currentSensor);
            removeSensor.highArea.manifold.SetSecondarySensor(Sensor.currentSensor);
            Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
            Sensor.topLabel.TextColor = UIColor.White;
            Sensor.tLabelBottom.BackgroundColor = removeSensor.tLabelBottom.BackgroundColor;
            Sensor.tLabelBottom.Hidden = false;
            removeSensor.lowArea.attachedSensor = Sensor;
            removeSensor.highArea.attachedSensor = Sensor;
          } else if (removeSensor.currentSensor.type == ION.Core.Sensors.ESensorType.Temperature && Sensor.currentSensor.type == ION.Core.Sensors.ESensorType.Pressure) {
            //Console.WriteLine("Swapping the current  " + removeSensor.currentSensor.device.name + "'s manifold's primary sensor with device " + Sensor.currentSensor.device.name + "'s sensor");
            removeSensor.lowArea.manifold = new Manifold(Sensor.currentSensor);
            removeSensor.lowArea.manifold.SetSecondarySensor(removeSensor.currentSensor);
            removeSensor.lowArea.manifold.ptChart = PTChart.New(removeSensor.lowArea.ion, Fluid.EState.Dew);
            removeSensor.highArea.manifold = new Manifold(Sensor.currentSensor);
            removeSensor.highArea.manifold.SetSecondarySensor(removeSensor.currentSensor);
            removeSensor.highArea.manifold.ptChart = PTChart.New(removeSensor.lowArea.ion, Fluid.EState.Dew);
            Sensor.topLabel.BackgroundColor = removeSensor.topLabel.BackgroundColor;
            Sensor.topLabel.TextColor = UIColor.White;
            Sensor.tLabelBottom.BackgroundColor = removeSensor.tLabelBottom.BackgroundColor;
            Sensor.tLabelBottom.Hidden = false;
            removeSensor.lowArea.attachedSensor = Sensor;
            removeSensor.highArea.attachedSensor = Sensor;
          } else {
            if (type == 1) {         
                replaceLowUnattached(Sensor,removeSensor,lowHighSensors,analyzerSensors,View);            
            } else if (type == 2) {
                replaceHighUnattached(Sensor,removeSensor,lowHighSensors,analyzerSensors,View);           
            } else if (type == 3) {
                replaceLowAttached(Sensor,removeSensor,lowHighSensors,analyzerSensors,View);           
            } else if (type == 4) {
                replaceHighAttached(Sensor,removeSensor,lowHighSensors,analyzerSensors,View);
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
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }
      if( lowHighSensors.lowArea.areaRect.Contains(touchPoint)){
        if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == Sensor.snapArea.AccessibilityIdentifier) {
          if(!freeSpot(analyzerSensors,Sensor,"low")){
            UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

            fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

            vc.PresentViewController (fullPopup, true, null);
          } else {
            string message = Util.Strings.Analyzer.HIGHLOST;

  					if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "1") {
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea1, lowHighSensors, analyzerSensors, View);					
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "2") {
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea2, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "3") {
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea3, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "4") {
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea4, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "5") {
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea5, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "6") {
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea6, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "7") {
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea7, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "8") {
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea8, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "low") {
              UIAlertController switchSide = UIAlertController.Create (Util.Strings.Analyzer.ACTION, Util.Strings.Analyzer.HIGHLOST, UIAlertControllerStyle.Alert);

              switchSide.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
                var goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)),"low",View);
                if (goOn) {                
                  lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
                  lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
                  View.BringSubviewToFront(Sensor.lowArea.snapArea);
                  Sensor.topLabel.BackgroundColor = UIColor.Blue;
                  Sensor.tLabelBottom.BackgroundColor = UIColor.Blue;
                  if (Sensor.currentSensor != null && Sensor.currentSensor.device.isConnected) {
                    Sensor.highArea.connectionColor.BackgroundColor = UIColor.Green;
                    Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Green;
                  } else if (Sensor.manualSensor != null){
                    
                  } else {
                    Sensor.highArea.connectionColor.BackgroundColor = UIColor.Red;
                    Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Red;
                  }

                  Sensor.lowArea.snapArea.Hidden = false;
                  Sensor.highArea.snapArea.Hidden = true;
                  Sensor.highArea.subviewTable.Source = null;
                  Sensor.highArea.tableSubviews = new List<string>();
                  Sensor.highArea.subviewTable.ReloadData();
                  Sensor.highArea.subviewTable.Hidden = true;
                  Sensor.highArea.subviewHide.SetImage(null, UIControlState.Normal);
                }
              }));            
              switchSide.AddAction(UIAlertAction.Create(Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));
              vc.PresentViewController (switchSide, true, null);
            }
          }
				} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier != "low") {
          if(!freeSpot(analyzerSensors,Sensor,"low")){
            UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

            fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

            vc.PresentViewController (fullPopup, true, null);
          } else {
            string message = Util.Strings.Analyzer.LOWLOST;
            if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "1" && Sensor.snapArea.AccessibilityIdentifier != "1") {
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea1, lowHighSensors, analyzerSensors, View);					
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "2" && Sensor.snapArea.AccessibilityIdentifier != "2") {
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea2, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "3" && Sensor.snapArea.AccessibilityIdentifier != "3") {
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea3, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "4" && Sensor.snapArea.AccessibilityIdentifier != "4") {
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea4, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "5" && Sensor.snapArea.AccessibilityIdentifier != "5") {
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea5, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "6" && Sensor.snapArea.AccessibilityIdentifier != "6") {
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea6, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "7" && Sensor.snapArea.AccessibilityIdentifier != "7") {
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea7, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "8" && Sensor.snapArea.AccessibilityIdentifier != "8") {
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea8, lowHighSensors, analyzerSensors, View);
            }
          }

				} else {
          bool goOn = orderSensors (analyzerSensors, analyzerSensors.areaList.IndexOf (Convert.ToInt32 (Sensor.snapArea.AccessibilityIdentifier)), "low", View);
					if (goOn) {
            Sensor.topLabel.BackgroundColor = UIColor.Blue;
            Sensor.tLabelBottom.BackgroundColor = UIColor.Blue;
            Sensor.tLabelBottom.Hidden = false;
            Sensor.topLabel.TextColor = UIColor.White;

            if (Sensor.currentSensor != null && Sensor.currentSensor.device.isConnected) {
              Sensor.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              Sensor.lowArea.Connection.Hidden = false;
              Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Green;
              Sensor.lowArea.connectionColor.Hidden = false;
            } else if (Sensor.currentSensor != null && !Sensor.currentSensor.device.isConnected) {
              Sensor.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              Sensor.lowArea.Connection.Hidden = false;
              Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Red;
              Sensor.lowArea.connectionColor.Hidden = false;             
            } else {
              Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Clear;
              Sensor.lowArea.connectionColor.Hidden = true;
              Sensor.lowArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
              Sensor.lowArea.Connection.Hidden = true;            
            }
            View.BringSubviewToFront(Sensor.lowArea.snapArea);
            Sensor.lowArea.snapArea.Hidden = false;
            Sensor.highArea.snapArea.Hidden = true;
            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
            return;
					}

          UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

          fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

          vc.PresentViewController (fullPopup, true, null);
				}

			} else if (lowHighSensors.highArea.areaRect.Contains (touchPoint)){
        if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == Sensor.snapArea.AccessibilityIdentifier) {
          if(!freeSpot(analyzerSensors,Sensor,"high")){
            UIAlertController fullPopup = UIAlertController.Create (Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

            fullPopup.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {}));            

            vc.PresentViewController (fullPopup, true, null);
          } else {
            string message = Util.Strings.Analyzer.LOWLOST;
            if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "1") {
              replaceAlert(message, 4, Sensor, analyzerSensors.snapArea1, lowHighSensors, analyzerSensors, View);					
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "2") {
              replaceAlert(message, 4, Sensor, analyzerSensors.snapArea2, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "3") {
              replaceAlert(message, 4, Sensor, analyzerSensors.snapArea3, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "4") {
              replaceAlert(message, 4, Sensor, analyzerSensors.snapArea4, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "5") {
              replaceAlert(message, 4, Sensor, analyzerSensors.snapArea5, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "6") {
              replaceAlert(message, 4, Sensor, analyzerSensors.snapArea6, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "7") {
              replaceAlert(message, 4, Sensor, analyzerSensors.snapArea7, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "8") {
              replaceAlert(message, 4, Sensor, analyzerSensors.snapArea8, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "high") {
              UIAlertController switchSide = UIAlertController.Create (Util.Strings.Analyzer.ACTION, Util.Strings.Analyzer.LOWLOST, UIAlertControllerStyle.Alert);

              switchSide.AddAction (UIAlertAction.Create (Util.Strings.OK, UIAlertActionStyle.Default, (action) => {
                var goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)),"high",View);
                if (goOn) {
                  lowHighSensors.highArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
                  lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
                  View.BringSubviewToFront(Sensor.highArea.snapArea);
                  Sensor.topLabel.BackgroundColor = UIColor.Red;
                  Sensor.tLabelBottom.BackgroundColor = UIColor.Red;
                  if (Sensor.currentSensor != null && Sensor.currentSensor.device.isConnected) {
                    Sensor.highArea.connectionColor.BackgroundColor = UIColor.Green;
                    Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Green;
                  } else {
                    Sensor.highArea.connectionColor.BackgroundColor = UIColor.Red;
                    Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Red;
                  }

                  Sensor.highArea.snapArea.Hidden = false;
                  Sensor.lowArea.snapArea.Hidden = true;
                  Sensor.lowArea.subviewTable.Source = null;
                  Sensor.lowArea.tableSubviews = new List<string>();
                  Sensor.lowArea.subviewTable.ReloadData();
                  Sensor.lowArea.subviewTable.Hidden = true;
                  Sensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);
                }
              }));

              switchSide.AddAction(UIAlertAction.Create(Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {}));
              vc.PresentViewController (switchSide, true, null);

            } 
          }
				} else if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier != "high"){
          if (!freeSpot(analyzerSensors,Sensor, "high")) {
            UIAlertController fullPopup = UIAlertController.Create(Util.Strings.Analyzer.CANTMOVE, Util.Strings.Analyzer.NOSPACE, UIAlertControllerStyle.Alert);

            fullPopup.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (action) => {
            }));

            vc.PresentViewController(fullPopup, true, null);
          } else {
            string message = Util.Strings.Analyzer.HIGHLOST;
            if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "1" && Sensor.snapArea.AccessibilityIdentifier != "1") {
              replaceAlert(message, 2, Sensor, analyzerSensors.snapArea1, lowHighSensors, analyzerSensors, View);					
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "2" && Sensor.snapArea.AccessibilityIdentifier != "2") {
              replaceAlert(message, 2, Sensor, analyzerSensors.snapArea2, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "3" && Sensor.snapArea.AccessibilityIdentifier != "3") {
              replaceAlert(message, 2, Sensor, analyzerSensors.snapArea3, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "4" && Sensor.snapArea.AccessibilityIdentifier != "4") {
              replaceAlert(message, 2, Sensor, analyzerSensors.snapArea4, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "5" && Sensor.snapArea.AccessibilityIdentifier != "5") {
              replaceAlert(message, 2, Sensor, analyzerSensors.snapArea5, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "6" && Sensor.snapArea.AccessibilityIdentifier != "6") {
              replaceAlert(message, 2, Sensor, analyzerSensors.snapArea6, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "7" && Sensor.snapArea.AccessibilityIdentifier != "7") {
              replaceAlert(message, 2, Sensor, analyzerSensors.snapArea7, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "8" && Sensor.snapArea.AccessibilityIdentifier != "8") {
              replaceAlert(message, 2, Sensor, analyzerSensors.snapArea8, lowHighSensors, analyzerSensors, View);
            } 
          }
				} else {
          bool goOn = orderSensors (analyzerSensors, analyzerSensors.areaList.IndexOf (Convert.ToInt32 (Sensor.snapArea.AccessibilityIdentifier)), "high", View);
					if (goOn) {
            Sensor.topLabel.BackgroundColor = UIColor.Red;
            Sensor.topLabel.TextColor = UIColor.White;
            Sensor.tLabelBottom.BackgroundColor = UIColor.Red;
            Sensor.tLabelBottom.Hidden = false;

            if (Sensor.currentSensor != null && Sensor.currentSensor.device.isConnected) {
              Sensor.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              Sensor.highArea.Connection.Hidden = false;
              Sensor.highArea.connectionColor.BackgroundColor = UIColor.Green;
              Sensor.highArea.connectionColor.Hidden = false;
            } else if (Sensor.currentSensor != null && !Sensor.currentSensor.device.isConnected) {
              Sensor.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              Sensor.highArea.Connection.Hidden = false;
              Sensor.highArea.connectionColor.BackgroundColor = UIColor.Red;
              Sensor.highArea.connectionColor.Hidden = false;             
            } else {
              Sensor.highArea.connectionColor.BackgroundColor = UIColor.Clear;
              Sensor.highArea.connectionColor.Hidden = true;
              Sensor.highArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
              Sensor.highArea.Connection.Hidden = true;            
            }
            View.BringSubviewToFront(Sensor.highArea.snapArea);
            Sensor.highArea.snapArea.Hidden = false;
            Sensor.lowArea.snapArea.Hidden = true;
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
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
			if (side == "low") {
				if (viewLocation != 0 && viewLocation != 1 && viewLocation != 2 && viewLocation != 3) {					
					if (!analyzerSensors.viewList [0].availableView.Hidden) {
						int end = analyzerSensors.areaList [0];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [0] = start;
						analyzerSensors.areaList [viewLocation] = end;
						available = true;
          } else if (!analyzerSensors.viewList [1].availableView.Hidden) {
						int end = analyzerSensors.areaList [1];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [1] = start;
						analyzerSensors.areaList [viewLocation] = end;
						available = true;
          } else if (!analyzerSensors.viewList [2].availableView.Hidden) {
						int end = analyzerSensors.areaList [2];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [2] = start;
						analyzerSensors.areaList [viewLocation] = end;
						available = true;
          } else if (!analyzerSensors.viewList [3].availableView.Hidden) {
						int end = analyzerSensors.areaList [3];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [3] = start;
						analyzerSensors.areaList [viewLocation] = end;
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
						available = true;
          } else if (!analyzerSensors.viewList [5].availableView.Hidden) {
						int end = analyzerSensors.areaList [5];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [5] = start;
						analyzerSensors.areaList [viewLocation] = end;
						available = true;
          } else if (!analyzerSensors.viewList [6].availableView.Hidden) {
						int end = analyzerSensors.areaList [6];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [6] = start;
						analyzerSensors.areaList [viewLocation] = end;
						available = true;
          } else if (!analyzerSensors.viewList [7].availableView.Hidden) {
						int end = analyzerSensors.areaList [7];
						int start = analyzerSensors.areaList [viewLocation];
						analyzerSensors.areaList [7] = start;
						analyzerSensors.areaList [viewLocation] = end;
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
        case ESensorType.Mass:
          return  Units.Mass.KILOGRAM;
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
    public static void replaceLowUnattached(sensor Sensor, sensor removeSensor, LowHighArea lowHighSensors, sensorGroup analyzerSensors, UIView View){
      var goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "low", View);
      if (goOn) {
        removeSensor.topLabel.BackgroundColor = UIColor.Clear;
        removeSensor.topLabel.TextColor = UIColor.Black;
        removeSensor.tLabelBottom.Hidden = true;

        removeSensor.lowArea.snapArea.Hidden = true;
        removeSensor.lowArea.tableSubviews = new List<string>();
        removeSensor.lowArea.subviewTable.Source = null;
        removeSensor.lowArea.subviewTable.ReloadData();
        removeSensor.lowArea.subviewTable.Hidden = true;
        removeSensor.lowArea.max = 0;
        removeSensor.lowArea.min = 0;
        removeSensor.lowArea.isManual = false;
        removeSensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);

        removeSensor.highArea.snapArea.Hidden = true;
        removeSensor.highArea.tableSubviews = new List<string>();
        removeSensor.highArea.subviewTable.Source = null;
        removeSensor.highArea.subviewTable.ReloadData();
        removeSensor.highArea.subviewTable.Hidden = true;
        removeSensor.highArea.max = 0;
        removeSensor.highArea.min = 0;
        removeSensor.highArea.isManual = false;
        removeSensor.highArea.subviewHide.SetImage(null, UIControlState.Normal);

        Sensor.topLabel.BackgroundColor = UIColor.Blue;
        Sensor.topLabel.TextColor = UIColor.White;
        Sensor.tLabelBottom.BackgroundColor = UIColor.Blue;
        Sensor.tLabelBottom.Hidden = false;
        Sensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);
        Sensor.lowArea.tableSubviews = new List<string>();
        Sensor.lowArea.subviewTable.Source = null;
        Sensor.lowArea.subviewTable.ReloadData();
        Sensor.lowArea.max = 0;
        Sensor.lowArea.min = 0;
        Sensor.highArea.subviewHide.SetImage(null, UIControlState.Normal);
        Sensor.highArea.tableSubviews = new List<string>();
        Sensor.highArea.subviewTable.Source = null;
        Sensor.highArea.subviewTable.ReloadData();
        Sensor.highArea.max = 0;
        Sensor.highArea.min = 0;
        View.BringSubviewToFront(Sensor.lowArea.snapArea);
        Sensor.lowArea.snapArea.Hidden = false;
        Sensor.highArea.snapArea.Hidden = true;
        lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
        Console.WriteLine("Occupied Low side given unattached sensor with identifier " + Sensor.snapArea.AccessibilityIdentifier + ":" + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier);
        Console.WriteLine("The high side is currently identified with sensor " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
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
    public static void replaceHighUnattached(sensor Sensor, sensor removeSensor, LowHighArea lowHighSensors, sensorGroup analyzerSensors, UIView View){
      var goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "high", View);
      if (goOn) {
        removeSensor.topLabel.BackgroundColor = UIColor.Clear;
        removeSensor.topLabel.TextColor = UIColor.Black;
        removeSensor.tLabelBottom.Hidden = true;

        removeSensor.lowArea.snapArea.Hidden = true;
        removeSensor.lowArea.tableSubviews = new List<string>();
        removeSensor.lowArea.subviewTable.Source = null;
        removeSensor.lowArea.subviewTable.ReloadData();
        removeSensor.lowArea.subviewTable.Hidden = true;
        removeSensor.lowArea.max = 0;
        removeSensor.lowArea.min = 0;
        removeSensor.lowArea.isManual = false;
        removeSensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);

        removeSensor.highArea.snapArea.Hidden = true;
        removeSensor.highArea.tableSubviews = new List<string>();
        removeSensor.highArea.subviewTable.Source = null;
        removeSensor.highArea.subviewTable.ReloadData();
        removeSensor.highArea.subviewTable.Hidden = true;
        removeSensor.highArea.max = 0;
        removeSensor.highArea.min = 0;
        removeSensor.highArea.isManual = false;
        removeSensor.highArea.subviewHide.SetImage(null, UIControlState.Normal);

        Sensor.topLabel.BackgroundColor = UIColor.Red;
        Sensor.topLabel.TextColor = UIColor.White;
        Sensor.tLabelBottom.BackgroundColor = UIColor.Red;
        Sensor.tLabelBottom.Hidden = false;
        Sensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);
        Sensor.lowArea.tableSubviews = new List<string>();
        Sensor.lowArea.subviewTable.Source = null;
        Sensor.lowArea.subviewTable.ReloadData();
        Sensor.lowArea.max = 0;
        Sensor.lowArea.min = 0;
        Sensor.highArea.subviewHide.SetImage(null, UIControlState.Normal);
        Sensor.highArea.tableSubviews = new List<string>();
        Sensor.highArea.subviewTable.Source = null;
        Sensor.highArea.subviewTable.ReloadData();
        Sensor.highArea.max = 0;
        Sensor.highArea.min = 0;
        View.BringSubviewToFront(Sensor.highArea.snapArea);
        Sensor.highArea.snapArea.Hidden = false;
        Sensor.lowArea.snapArea.Hidden = true;
        lowHighSensors.highArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
        Console.WriteLine("Occupied High side given unattached sensor with identifier " + Sensor.snapArea.AccessibilityIdentifier + ":" + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
        Console.WriteLine("The low side is currently identified with sensor " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier);
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
    public static void replaceLowAttached(sensor Sensor, sensor removeSensor, LowHighArea lowHighSensors, sensorGroup analyzerSensors, UIView View){
      var goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "low", View);
      if (goOn) {
        removeSensor.topLabel.BackgroundColor = UIColor.Clear;
        removeSensor.topLabel.TextColor = UIColor.Black;
        removeSensor.tLabelBottom.Hidden = true;

        removeSensor.lowArea.snapArea.Hidden = true;
        removeSensor.lowArea.tableSubviews = new List<string>();
        removeSensor.lowArea.subviewTable.Source = null;
        removeSensor.lowArea.subviewTable.ReloadData();
        removeSensor.lowArea.subviewTable.Hidden = true;
        removeSensor.lowArea.max = 0;
        removeSensor.lowArea.min = 0;
        removeSensor.lowArea.isManual = false;
        removeSensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);

        removeSensor.highArea.snapArea.Hidden = true;
        removeSensor.highArea.tableSubviews = new List<string>();
        removeSensor.highArea.subviewTable.Source = null;
        removeSensor.highArea.subviewTable.ReloadData();
        removeSensor.highArea.subviewTable.Hidden = true;
        removeSensor.highArea.max = 0;
        removeSensor.highArea.min = 0;
        removeSensor.highArea.isManual = false;
        removeSensor.highArea.subviewHide.SetImage(null, UIControlState.Normal);

        Sensor.topLabel.BackgroundColor = UIColor.Blue;
        Sensor.topLabel.TextColor = UIColor.White;
        Sensor.tLabelBottom.BackgroundColor = UIColor.Blue;
        Sensor.tLabelBottom.Hidden = false;
        Sensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);
        Sensor.lowArea.tableSubviews = new List<string>();
        Sensor.lowArea.subviewTable.Source = null;
        Sensor.lowArea.subviewTable.ReloadData();
        Sensor.lowArea.max = 0;
        Sensor.lowArea.min = 0;
        Sensor.highArea.subviewHide.SetImage(null, UIControlState.Normal);
        Sensor.highArea.tableSubviews = new List<string>();
        Sensor.highArea.subviewTable.Source = null;
        Sensor.highArea.subviewTable.ReloadData();
        Sensor.highArea.max = 0;
        Sensor.highArea.min = 0;
        View.BringSubviewToFront(Sensor.lowArea.snapArea);
        Sensor.lowArea.snapArea.Hidden = false;
        Sensor.highArea.snapArea.Hidden = true;
        lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
        lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
        Console.WriteLine("Occupied Low side given High side sensor with identifier " + Sensor.snapArea.AccessibilityIdentifier + ":" + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier);
        Console.WriteLine("The high side is currently identified with sensor " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
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
    public static void replaceHighAttached(sensor Sensor, sensor removeSensor, LowHighArea lowHighSensors, sensorGroup analyzerSensors, UIView View){
      var goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "high", View);
      if (goOn) {
        removeSensor.topLabel.BackgroundColor = UIColor.Clear;
        removeSensor.topLabel.TextColor = UIColor.Black;
        removeSensor.tLabelBottom.Hidden = true;

        removeSensor.lowArea.snapArea.Hidden = true;
        removeSensor.lowArea.tableSubviews = new List<string>();
        removeSensor.lowArea.subviewTable.Source = null;
        removeSensor.lowArea.subviewTable.ReloadData();
        removeSensor.lowArea.subviewTable.Hidden = true;
        removeSensor.lowArea.max = 0;
        removeSensor.lowArea.min = 0;
        removeSensor.lowArea.isManual = false;
        removeSensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);

        removeSensor.highArea.snapArea.Hidden = true;
        removeSensor.highArea.tableSubviews = new List<string>();
        removeSensor.highArea.subviewTable.Source = null;
        removeSensor.highArea.subviewTable.ReloadData();
        removeSensor.highArea.subviewTable.Hidden = true;
        removeSensor.highArea.max = 0;
        removeSensor.highArea.min = 0;
        removeSensor.highArea.isManual = false;
        removeSensor.highArea.subviewHide.SetImage(null, UIControlState.Normal);

        Sensor.topLabel.BackgroundColor = UIColor.Red;
        Sensor.topLabel.TextColor = UIColor.White;
        Sensor.tLabelBottom.BackgroundColor = UIColor.Red;
        Sensor.tLabelBottom.Hidden = false;
        Sensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);
        Sensor.lowArea.tableSubviews = new List<string>();
        Sensor.lowArea.subviewTable.Source = null;
        Sensor.lowArea.subviewTable.ReloadData();
        Sensor.lowArea.max = 0;
        Sensor.lowArea.min = 0;
        Sensor.highArea.subviewHide.SetImage(null, UIControlState.Normal);
        Sensor.highArea.tableSubviews = new List<string>();
        Sensor.highArea.subviewTable.Source = null;
        Sensor.highArea.subviewTable.ReloadData();
        Sensor.highArea.max = 0;
        Sensor.highArea.min = 0;
        View.BringSubviewToFront(Sensor.highArea.snapArea);
        Sensor.highArea.snapArea.Hidden = false;
        Sensor.lowArea.snapArea.Hidden = true;
        lowHighSensors.highArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
        lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
        Console.WriteLine("Occupied High side given Low side sensor with identifier " + Sensor.snapArea.AccessibilityIdentifier + ":" + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
        Console.WriteLine("The Low side is currently identified with sensor " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier);
      }
    }

    public static void alarmRequestViewer(sensor area) {
      var alarm = Analyzer.AnalyzerViewController.arvc.InflateViewController<SensorAlarmViewController>("viewControllerSensorAlarms");
      alarm.sensor = area.currentSensor;
      Analyzer.AnalyzerViewController.arvc.NavigationController.PushViewController(alarm, true);
    }
	}
}

