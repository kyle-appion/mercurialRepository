/// <summary>
/// This contains the methods for setting up the analyzer view and subviews
/// </summary>


using System;
using System.Collections.Generic;
using UIKit;
using CoreGraphics;
using Foundation;
using CoreAnimation;
//using Robotics.Mobile.Core.Bluetooth.LE;
namespace ION.IOS.ViewController.Analyzer
{
	class AnalyserUtilities 
	{

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

//      analyzerSensors.snapRect1 = new CGRect (View.Center.X - 150, View.Center.Y - 195, 65, 65);
//      analyzerSensors.snapRect2 = new CGRect(View.Center.X - 150, View.Center.Y - 35, 65,65);
//      analyzerSensors.snapRect3 = new CGRect(View.Center.X - 80, View.Center.Y - 195, 65,65);
//      analyzerSensors.snapRect4 = new CGRect(View.Center.X - 80, View.Center.Y - 35, 65,65);
//      /// RIGHT SIDE
//      analyzerSensors.snapRect5 = new CGRect (View.Center.X + 15, View.Center.Y - 195, 65, 65);
//      analyzerSensors.snapRect6 = new CGRect(View.Center.X + 15, View.Center.Y - 35, 65,65);
//      analyzerSensors.snapRect7 = new CGRect(View.Center.X + 85, View.Center.Y - 195, 65,65);
//      analyzerSensors.snapRect8 = new CGRect(View.Center.X + 85, View.Center.Y - 35, 65,65);

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
      mentryView.popupText.Text = "Create Manual Entry";
      mentryView.popupText.TextAlignment = UITextAlignment.Center;
      mentryView.popupText.AdjustsFontSizeToFitWidth = true;
      mentryView.popupText.BackgroundColor = UIColor.FromRGB(9,221,255);
      mentryView.popupText.Layer.CornerRadius = 5f;
      mentryView.popupText.ClipsToBounds = false;
      mentryView.popupText.Font = UIFont.FromName("Helvetica-Bold", 27f);
      mentryView.mdeviceType.Text = "Device Type";
      mentryView.mdeviceType.AdjustsFontSizeToFitWidth = true;
      mentryView.dtypeButton.SetTitle("Pressure", UIControlState.Normal);
      mentryView.dtypeButton.SetTitleColor(UIColor.Blue, UIControlState.Normal);
      mentryView.dtypeButton.AccessibilityIdentifier = "Pressure";
      mentryView.dtypeButton.Font = UIFont.FromName("Helvetica-Bold", 20f);
      mentryView.mtextValue.Text = "";
      mentryView.mtextValue.Layer.BorderColor = UIColor.LightGray.CGColor;
      mentryView.mtextValue.Layer.BorderWidth = 1f;
      mentryView.mtextValue.Layer.CornerRadius = 5f;
      mentryView.mbuttonText.UserInteractionEnabled = false;
      mentryView.mbuttonText.Text = "psig";
      mentryView.mbuttonText.BackgroundColor = UIColor.FromRGB(243, 251, 0);
      mentryView.mbuttonText.TextAlignment = UITextAlignment.Center;
      mentryView.mcloseButton.SetTitleColor(UIColor.Gray, UIControlState.Normal);
      mentryView.mcloseButton.SetTitle("Close", UIControlState.Normal);
      mentryView.mcloseButton.ClipsToBounds = true;
      mentryView.mdoneButton.SetTitleColor(UIColor.Gray, UIControlState.Normal);
      mentryView.mdoneButton.SetTitle("OK-Done", UIControlState.Normal);
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
      sactionView.pactionButton.SetTitle("Actions", UIControlState.Normal);
      sactionView.pactionButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
      sactionView.pcloseButton.SetTitle("Close", UIControlState.Normal);
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
      sactionView.pdisplayLink.Text = "Display Link:";
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
      lowArea.LabelMiddle.Text = "Low Viewer Not Defined";
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
      highArea.LabelMiddle.Text = "High Viewer Not Defined";
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

      mainView.AddSubview (highArea.snapArea);
      mainView.AddSubview (highArea.subviewTable);   
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
			lowHighSensors.lowArea.LabelMiddle.Text = "Low Viewer Not Defined";
			lowHighSensors.lowArea.LabelMiddle.TextAlignment = UITextAlignment.Center;

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
      lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Clear;
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
			lowHighSensors.highArea.LabelMiddle.Text = "High Viewer Not Defined";
			lowHighSensors.highArea.LabelMiddle.TextAlignment = UITextAlignment.Center;

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
      lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Clear;
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
      removeSensor.lowArea.isManual = false;
      removeSensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);

      removeSensor.highArea.snapArea.Hidden = true;
      removeSensor.highArea.tableSubviews = new List<string> ();
      removeSensor.highArea.subviewTable.Source = null;
      removeSensor.highArea.subviewTable.ReloadData ();
      removeSensor.highArea.subviewTable.Hidden = true;
      removeSensor.highArea.max = 0;
      removeSensor.highArea.min = 0;
      removeSensor.highArea.isManual = false;
      removeSensor.highArea.subviewHide.SetImage(null, UIControlState.Normal);

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
		public static void RemoveDevice(actionPopup Sensor, LowHighArea lowHighSensors){
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
//        Sensor.pressedSensor.currentSensor.device.Dispose();
//        Sensor.pressedSensor.lowArea.currentSensor.device.Dispose();
//        Sensor.pressedSensor.highArea.currentSensor.device.Dispose();
        Sensor.pressedSensor.lowArea.manifold.Dispose();
        Sensor.pressedSensor.highArea.manifold.Dispose();
        Sensor.pressedSensor.currentSensor = null;
        Sensor.pressedSensor.sactionView.currentSensor = null;
        Sensor.pressedSensor.lowArea.currentSensor = null;
        Sensor.pressedSensor.highArea.currentSensor = null;
      }
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

      if (Sensor.pressedSensor.snapArea.AccessibilityIdentifier == lowHighSensors.lowArea.snapArea.AccessibilityIdentifier)
        lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
      else if (Sensor.pressedSensor.snapArea.AccessibilityIdentifier == lowHighSensors.highArea.snapArea.AccessibilityIdentifier)
        lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";

      Console.WriteLine("Low side identifier after single sensor " + Sensor.pressedView.AccessibilityIdentifier + " removal: " + lowHighSensors.lowArea.snapArea.AccessibilityIdentifier);
      Console.WriteLine("High side identifier after single sensor " + Sensor.pressedView.AccessibilityIdentifier + " removal: " + lowHighSensors.highArea.snapArea.AccessibilityIdentifier);
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

      addDeviceSheet = UIAlertController.Create (lhSensor.LabelTop.Text + " Actions", "", UIAlertControllerStyle.Alert);

      addDeviceSheet.AddAction (UIAlertAction.Create ("Add Subview", UIAlertActionStyle.Default, (action) => {
        subviewOptionChosen(lhSensor);
      }));

      addDeviceSheet.AddAction (UIAlertAction.Create ("Remove Sensor", UIAlertActionStyle.Default, (action) => {
        RemoveDevice (Sensor, lowHighSensors);
      }));

      addDeviceSheet.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, (action) => {}));

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

      UIAlertController subviewAlert = UIAlertController.Create ("Choose a subview", "", UIAlertControllerStyle.Alert);
      foreach (string subview in pressedArea.availableSubviews) {
        var splits = subview.Split(' ');

        if (!pressedArea.tableSubviews.Contains (splits[0])) {          
          subviewAlert.AddAction (UIAlertAction.Create (subview, UIAlertActionStyle.Default, (action) => {
            pressedArea.tableSubviews.Add(splits[0]);
            pressedArea.subviewTable.Source = new AnalyzerTableSource(pressedArea.tableSubviews, splits[0], pressedArea);
            pressedArea.subviewTable.ReloadData();
            if(pressedArea.subviewTable.Hidden)
              pressedArea.subviewTable.Hidden = false;
            pressedArea.subviewHide.SetImage(UIImage.FromBundle("ic_arrow_downwhite"), UIControlState.Normal);
            pressedArea.subviewHide.Hidden = false;
          }));
        }
      }

      subviewAlert.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, (action) => {}));
      vc.PresentViewController (subviewAlert, true, null);
    }
		/// <summary>
		/// ARRANGES THE SINGLE SENSOR SUBVIEWS TO HOW THE USER HAS ORDERED THEM
		/// </summary>
		/// <param name="touchPoint">LOCATION OF SUBVIEW WHEN FINGER WAS REMOVED</param>
		/// <param name="position">WHICH SUBVIEW WAS MOVING</param>
		public static void sensorSwap(sensorGroup analyzerSensors, int position, CGPoint touchPoint, UIView View){
			int start = analyzerSensors.areaList.IndexOf(position);
			int swap;

			////CHECK LOCATION OF SUBVIEW WHEN TOUCH ENDED TO DETERMINE INDEX PLACEMENT
			if (analyzerSensors.snapRect1.Contains (touchPoint)) {
				swap = analyzerSensors.areaList[0];
				analyzerSensors.areaList [0] = position;
				analyzerSensors.areaList [start] = swap;

			} else if (analyzerSensors.snapRect2.Contains (touchPoint)) {
				swap = analyzerSensors.areaList[1];
				analyzerSensors.areaList [1] = position;
				analyzerSensors.areaList [start] = swap;

			} else if (analyzerSensors.snapRect3.Contains (touchPoint)) {
				swap = analyzerSensors.areaList[2];
				analyzerSensors.areaList [2] = position;
				analyzerSensors.areaList [start] = swap;
;
			} else if (analyzerSensors.snapRect4.Contains (touchPoint)) {
				swap = analyzerSensors.areaList[3];
				analyzerSensors.areaList [3] = position;
				analyzerSensors.areaList [start] = swap;


			} else if (analyzerSensors.snapRect5.Contains (touchPoint)) {
				swap = analyzerSensors.areaList[4];
				analyzerSensors.areaList [4] = position;
				analyzerSensors.areaList [start] = swap;


			} else if (analyzerSensors.snapRect6.Contains (touchPoint)) {
				swap = analyzerSensors.areaList[5];
				analyzerSensors.areaList [5] = position;
				analyzerSensors.areaList [start] = swap;

			} else if (analyzerSensors.snapRect7.Contains (touchPoint)) {
				swap = analyzerSensors.areaList[6];
				analyzerSensors.areaList [6] = position;
				analyzerSensors.areaList [start] = swap;

			} else if (analyzerSensors.snapRect8.Contains (touchPoint)) {
				swap = analyzerSensors.areaList[7];
				analyzerSensors.areaList [7] = position;
				analyzerSensors.areaList [start] = swap;

			}

			////MOVE SENSORS BASED ON THEIR LOCATION
			for (int i = 0; i < 8; i++) {
				analyzerSensors.animator = new UIDynamicAnimator(View);
				if (analyzerSensors.areaList [i] == 1) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea1.snapArea, analyzerSensors.locationList [i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				} else if (analyzerSensors.areaList [i] == 2) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea2.snapArea, analyzerSensors.locationList [i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				} else if (analyzerSensors.areaList [i] == 3) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea3.snapArea, analyzerSensors.locationList[i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				} else if (analyzerSensors.areaList[i] == 4) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea4.snapArea, analyzerSensors.locationList[i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				} else if (analyzerSensors.areaList[i] == 5) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea5.snapArea, analyzerSensors.locationList[i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				} else if (analyzerSensors.areaList[i] == 6) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea6.snapArea, analyzerSensors.locationList[i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				} else if (analyzerSensors.areaList[i] == 7) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea7.snapArea, analyzerSensors.locationList[i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				} else if (analyzerSensors.areaList[i] == 8) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea8.snapArea, analyzerSensors.locationList[i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				}
			}
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
			bool goOn;
			UIAlertController addDeviceSheet;

			addDeviceSheet = UIAlertController.Create ("Complete Swap?", message, UIAlertControllerStyle.Alert);


			addDeviceSheet.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, (action) => {
        if(Sensor.isManual.Equals(false)){
          if(Sensor.currentSensor.device.isConnected){
            Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Green;
            Sensor.highArea.connectionColor.BackgroundColor = UIColor.Green;
          } else {
            Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Red;
            Sensor.highArea.connectionColor.BackgroundColor = UIColor.Red;
          }
        }
				if(type == 1){
          goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)),"low",View);
					if(goOn){
            removeSensor.topLabel.BackgroundColor = UIColor.Clear;
            removeSensor.topLabel.TextColor = UIColor.Black;
            removeSensor.tLabelBottom.Hidden = true;
            removeSensor.lowArea.snapArea.Hidden = true;
            removeSensor.lowArea.max = 0;
            removeSensor.lowArea.min = 0;
            removeSensor.lowArea.tableSubviews = new List<string>();
            removeSensor.lowArea.subviewTable.Source = null;
            removeSensor.lowArea.subviewTable.ReloadData();
            removeSensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);

            removeSensor.highArea.snapArea.Hidden = true;
            removeSensor.highArea.max = 0;
            removeSensor.highArea.min = 0;
            removeSensor.highArea.subviewTable.Source = null;
            removeSensor.highArea.tableSubviews = new List<string>();
            removeSensor.highArea.subviewTable.ReloadData();
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
				} else if (type == 2){
          goOn = orderSensors(analyzerSensors,analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)),"high",View);
					if(goOn){
            removeSensor.topLabel.BackgroundColor = UIColor.Clear;
            removeSensor.topLabel.TextColor = UIColor.Black;
            removeSensor.tLabelBottom.Hidden = true;
            removeSensor.lowArea.snapArea.Hidden = true;
            removeSensor.lowArea.max = 0;
            removeSensor.lowArea.min = 0;
            removeSensor.lowArea.tableSubviews = new List<string>();
            removeSensor.lowArea.subviewTable.Source = null;
            removeSensor.lowArea.subviewTable.ReloadData();
            removeSensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);

            removeSensor.highArea.snapArea.Hidden = true;
            removeSensor.highArea.max = 0;
            removeSensor.highArea.min = 0;
            removeSensor.highArea.subviewTable.Source = null;
            removeSensor.highArea.tableSubviews = new List<string>();
            removeSensor.highArea.subviewTable.ReloadData();
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
				} else if (type == 3){
          goOn = orderSensors(analyzerSensors,analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "low",View);
					if(goOn){
            removeSensor.topLabel.BackgroundColor = UIColor.Clear;
            removeSensor.topLabel.TextColor = UIColor.Black;
            removeSensor.tLabelBottom.Hidden = true;
            removeSensor.lowArea.snapArea.Hidden = true;
            removeSensor.lowArea.max = 0;
            removeSensor.lowArea.min = 0;
            removeSensor.lowArea.tableSubviews = new List<string>();
            removeSensor.lowArea.subviewTable.Source = null;
            removeSensor.lowArea.subviewTable.ReloadData();
            removeSensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);

            removeSensor.highArea.snapArea.Hidden = true;
            removeSensor.highArea.max = 0;
            removeSensor.highArea.min = 0;
            removeSensor.highArea.subviewTable.Source = null;
            removeSensor.highArea.tableSubviews = new List<string>();
            removeSensor.highArea.subviewTable.ReloadData();
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
				} else if (type == 4){		
          goOn = orderSensors(analyzerSensors,analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "high",View);
					if(goOn){
            removeSensor.topLabel.BackgroundColor = UIColor.Clear;
            removeSensor.topLabel.TextColor = UIColor.Black;
            removeSensor.tLabelBottom.Hidden = true;
            removeSensor.lowArea.snapArea.Hidden = true;
            removeSensor.lowArea.max = 0;
            removeSensor.lowArea.min = 0;
            removeSensor.lowArea.tableSubviews = new List<string>();
            removeSensor.lowArea.subviewTable.Source = null;
            removeSensor.lowArea.subviewTable.ReloadData();
            removeSensor.lowArea.subviewHide.SetImage(null, UIControlState.Normal);

            removeSensor.highArea.snapArea.Hidden = true;
            removeSensor.highArea.max = 0;
            removeSensor.highArea.min = 0;
            removeSensor.highArea.subviewTable.Source = null;
            removeSensor.highArea.tableSubviews = new List<string>();
            removeSensor.highArea.subviewTable.ReloadData();
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
			}));

			addDeviceSheet.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, (action) => {

			}));

			vc.PresentViewController (addDeviceSheet, true, null);

		}
	/// <summary>
    /// SETS THE LOW OR HIGH AREAS TO MONITOR THE SENSOR THAT WAS DROPPED ON IT
  /// </summary>
  /// <param name="touchPoint">Location on the screen where single sensor was dropped</param>
  /// <param name="Sensor">Single sensor used to update low high sensor</param>
  /// <param name="lowHighSensors">Low high sensors being updated</param>
  /// <param name="analyzerSensors">Collection of single sensor information</param>
  /// <param name="View">View.</param>
    public static void updateLowHighArea(CGPoint touchPoint, sensor Sensor, LowHighArea lowHighSensors, sensorGroup analyzerSensors,UIView View){
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }
      if( lowHighSensors.lowArea.areaRect.Contains(touchPoint)){
        if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == Sensor.snapArea.AccessibilityIdentifier) {
          if(!freeSpot(analyzerSensors,"low")){
            UIAlertController fullPopup = UIAlertController.Create ("Cannot Move Sensor", "Not enough space", UIAlertControllerStyle.Alert);

            fullPopup.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, (action) => {}));            

            vc.PresentViewController (fullPopup, true, null);
          } else {
  					string message = "Completing this action will cause the High side subview arrangement to be lost.";

  					if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "1") {
              Console.WriteLine("Sending sensor 1 to remove. Sensor from high area");
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea1, lowHighSensors, analyzerSensors, View);					
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "2") {
              Console.WriteLine("Sending sensor 2 to remove. Sensor from high area");
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea2, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "3") {
              Console.WriteLine("Sending sensor 3 to remove. Sensor from high area");
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea3, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "4") {
              Console.WriteLine("Sending sensor 4 to remove. Sensor from high area");
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea4, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "5") {
              Console.WriteLine("Sending sensor 5 to remove. Sensor from high area");
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea5, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "6") {
              Console.WriteLine("Sending sensor 6 to remove. Sensor from high area");
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea6, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "7") {
              Console.WriteLine("Sending sensor 7 to remove. Sensor from high area");
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea7, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "8") {
              Console.WriteLine("Sending sensor 8 to remove. Sensor from high area");
              replaceAlert (message, 3, Sensor, analyzerSensors.snapArea8, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "low") {
              Console.WriteLine("Low side wasn't empty but wasn't attached to a 1 - 8 sensor");
              //replaceAlert (message, 3, Sensor, analyzerSensors.snapArea1, lowHighSensors, analyzerSensors, View);
              var goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)),"low",View);
              if (goOn) {
                lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
                lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
                View.BringSubviewToFront(Sensor.lowArea.snapArea);
                Sensor.topLabel.BackgroundColor = UIColor.Blue;
                Sensor.tLabelBottom.BackgroundColor = UIColor.Blue;
                if (Sensor.currentSensor.device.isConnected) {
                  Sensor.highArea.connectionColor.BackgroundColor = UIColor.Green;
                  Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Green;
                } else {
                  Sensor.highArea.connectionColor.BackgroundColor = UIColor.Red;
                  Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Red;
                }

                Sensor.lowArea.snapArea.Hidden = false;
                Sensor.highArea.snapArea.Hidden = true;
              }
            }
          }
				} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier != "low") {
          if(!freeSpot(analyzerSensors,"low")){
            UIAlertController fullPopup = UIAlertController.Create ("Cannot Move Sensor", "Not enough space", UIAlertControllerStyle.Alert);

            fullPopup.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, (action) => {}));            

            vc.PresentViewController (fullPopup, true, null);
          } else {
  					string message = "Completing this action will cause the Low side subview arrangement to be lost.";
            if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "1" && Sensor.snapArea.AccessibilityIdentifier != "1") {
              Console.WriteLine("Sending sensor 1 to remove. Sensor was unattached");
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea1, lowHighSensors, analyzerSensors, View);					
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "2" && Sensor.snapArea.AccessibilityIdentifier != "2") {
              Console.WriteLine("Sending sensor 2 to remove. Sensor was unattached");
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea2, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "3" && Sensor.snapArea.AccessibilityIdentifier != "3") {
              Console.WriteLine("Sending sensor 3 to remove. Sensor was unattached");
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea3, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "4" && Sensor.snapArea.AccessibilityIdentifier != "4") {
              Console.WriteLine("Sending sensor 4 to remove. Sensor was unattached");
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea4, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "5" && Sensor.snapArea.AccessibilityIdentifier != "5") {
              Console.WriteLine("Sending sensor 5 to remove. Sensor was unattached");
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea5, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "6" && Sensor.snapArea.AccessibilityIdentifier != "6") {
              Console.WriteLine("Sending sensor 6 to remove. Sensor was unattached");
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea6, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "7" && Sensor.snapArea.AccessibilityIdentifier != "7") {
              Console.WriteLine("Sending sensor 7 to remove. Sensor was unattached");
              replaceAlert (message, 1, Sensor, analyzerSensors.snapArea7, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "8" && Sensor.snapArea.AccessibilityIdentifier != "8") {
              Console.WriteLine("Sending sensor 8 to remove. Sensor was unattached");
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

          UIAlertController fullPopup = UIAlertController.Create ("Cannot Move Sensor", "Not enough space", UIAlertControllerStyle.Alert);

          fullPopup.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, (action) => {}));            

          vc.PresentViewController (fullPopup, true, null);
				}

			} else if (lowHighSensors.highArea.areaRect.Contains (touchPoint)){
        if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == Sensor.snapArea.AccessibilityIdentifier) {
          if(!freeSpot(analyzerSensors,"high")){
            UIAlertController fullPopup = UIAlertController.Create ("Cannot Move Sensor", "Not enough space", UIAlertControllerStyle.Alert);

            fullPopup.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, (action) => {}));            

            vc.PresentViewController (fullPopup, true, null);
          } else {
            string message = "Completing this action will cause the Low side subview arrangement to be lost.";
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
              Console.WriteLine("High side wasn't empty but wasn't attached to a 1 - 8 sensor");
              //replaceAlert(message, 4, Sensor, analyzerSensors.snapArea1, lowHighSensors, analyzerSensors, View);
              var goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)),"high",View);
              if (goOn) {
                lowHighSensors.highArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
                lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
                View.BringSubviewToFront(Sensor.highArea.snapArea);
                Sensor.topLabel.BackgroundColor = UIColor.Red;
                Sensor.tLabelBottom.BackgroundColor = UIColor.Red;
                if (Sensor.currentSensor.device.isConnected) {
                  Sensor.highArea.connectionColor.BackgroundColor = UIColor.Green;
                  Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Green;
                } else {
                  Sensor.highArea.connectionColor.BackgroundColor = UIColor.Red;
                  Sensor.lowArea.connectionColor.BackgroundColor = UIColor.Red;
                }

                Sensor.highArea.snapArea.Hidden = false;
                Sensor.lowArea.snapArea.Hidden = true;
              }
            } 
          }
				} else if(lowHighSensors.highArea.snapArea.AccessibilityIdentifier != "high"){
          if (!freeSpot(analyzerSensors, "high")) {
            UIAlertController fullPopup = UIAlertController.Create("Cannot Move Sensor", "Not enough space", UIAlertControllerStyle.Alert);

            fullPopup.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (action) => {
            }));            

            vc.PresentViewController(fullPopup, true, null);
          } else {
            string message = "Completing this action will cause the High side subview arrangement to be lost.";
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

          UIAlertController fullPopup = UIAlertController.Create ("Cannot Move Sensor", "Not enough space", UIAlertControllerStyle.Alert);

          fullPopup.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, (action) => {}));            

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

			////MOVE SENSORS BASED ON THEIR LOCATION
			for (int i = 0; i < 8; i++) {
				analyzerSensors.animator = new UIDynamicAnimator(View);
				if (analyzerSensors.areaList [i] == 1) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea1.snapArea, analyzerSensors.locationList [i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				} else if (analyzerSensors.areaList [i] == 2) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea2.snapArea, analyzerSensors.locationList [i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				} else if (analyzerSensors.areaList [i] == 3) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea3.snapArea, analyzerSensors.locationList[i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				} else if (analyzerSensors.areaList[i] == 4) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea4.snapArea, analyzerSensors.locationList[i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				} else if (analyzerSensors.areaList[i] == 5) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea5.snapArea, analyzerSensors.locationList[i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				} else if (analyzerSensors.areaList[i] == 6) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea6.snapArea, analyzerSensors.locationList[i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				} else if (analyzerSensors.areaList[i] == 7) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea7.snapArea, analyzerSensors.locationList[i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				} else if (analyzerSensors.areaList[i] == 8) {

					analyzerSensors.snap = new UISnapBehavior (analyzerSensors.snapArea8.snapArea, analyzerSensors.locationList[i]);
					analyzerSensors.animator.AddBehavior (analyzerSensors.snap);

				}
			}
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


    public static bool freeSpot(sensorGroup analyzerSensors, string identifier){
      if (identifier == "low") {
        for (int i = 0; i < 4; i++) {
          if (!analyzerSensors.viewList[i].availableView.Hidden) {
            return true;
          }
        }
      } else if (identifier == "high") {
        for (int i = 4; i < 8; i++) {
          if (!analyzerSensors.viewList[i].availableView.Hidden) {
            return true;
          }      
        }
      }
      return false;
    }
	}
}

