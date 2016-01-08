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

    public static void CreateSensorViews(ManualView mentryView, ActionView sactionView, UIView View){
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

      sactionView.aView.BringSubviewToFront(sactionView.pconnection);

      View.AddSubview(mentryView.mView);
      View.AddSubview(sactionView.aView);
    }
		/// <summary>
		/// Sets up each single sensor UI and adds them as subviews to the main view
		/// </summary>
		/// <param name="analyzerSensors">singleSensor class object holding 8 sensors</param>
		/// <param name="mainView">Main view.</param>
		public static void ApplySnapArea(sensorGroup analyzerSensors, UIView mainView){
      //UIImageView compressor = new UIImageView(new CGRect(148,92,25,25));
      UIImageView compressor = new UIImageView(new CGRect(.462 * mainView.Bounds.Width,.161 * mainView.Bounds.Height,.078 * mainView.Bounds.Width,.047 * mainView.Bounds.Height));        
      compressor.Image = UIImage.FromBundle("ic_compressor");
      //UIImageView expansion = new UIImageView(new CGRect(148,286,25,25));
      UIImageView expansion = new UIImageView(new CGRect(.462 * mainView.Bounds.Width,.503 * mainView.Bounds.Height,.078 * mainView.Bounds.Width,.047 * mainView.Bounds.Height ));
      expansion.Image = UIImage.FromBundle("ic_expansionchamber");

      mainView.AddSubview(compressor);
      mainView.AddSubview(expansion);

			/// LEFT SIDE
      analyzerSensors.snapArea1.snapArea = new UIView (analyzerSensors.snapRect1){
        AccessibilityIdentifier = "1",

      };
      analyzerSensors.snapArea1.availableView = new UIView(new CGRect(0,0,analyzerSensors.snapArea1.snapArea.Bounds.Width,analyzerSensors.snapArea1.snapArea.Bounds.Height)){
        BackgroundColor = UIColor.FromRGB(204,153,0),
        Alpha = .4f,
        UserInteractionEnabled = true,
        AccessibilityIdentifier = "1",
        Hidden = false,
      };
      analyzerSensors.snapArea1.addIcon = new UIImageView(new CGRect(.107 * analyzerSensors.snapArea1.snapArea.Bounds.Width,.107 * analyzerSensors.snapArea1.snapArea.Bounds.Height,.769 * analyzerSensors.snapArea1.snapArea.Bounds.Width,.769 * analyzerSensors.snapArea1.snapArea.Bounds.Height));
      analyzerSensors.snapArea1.addIcon.Image = UIImage.FromBundle("ic_device_add");
      analyzerSensors.snapArea1.addIcon.BackgroundColor = UIColor.Clear;
			analyzerSensors.snapArea1.snapArea.Layer.CornerRadius = 10;
      analyzerSensors.snapArea1.availableView.Layer.CornerRadius = 10;
      analyzerSensors.snapArea1.snapArea.Layer.BorderColor = UIColor.Black.CGColor;
      analyzerSensors.snapArea1.snapArea.Layer.BorderWidth = 2f;
      analyzerSensors.snapArea1.snapArea.AddSubview(analyzerSensors.snapArea1.availableView);
      analyzerSensors.snapArea1.snapArea.AddSubview(analyzerSensors.snapArea1.addIcon);
      analyzerSensors.snapArea1.snapArea.BringSubviewToFront(analyzerSensors.snapArea1.addIcon);
			CreateSubviewLayout (analyzerSensors.snapArea1.snapArea, analyzerSensors.snapArea1.topLabel, analyzerSensors.snapArea1.tLabelBottom, analyzerSensors.snapArea1.middleLabel, analyzerSensors.snapArea1.bottomLabel);
			mainView.AddSubview (analyzerSensors.snapArea1.snapArea);
			analyzerSensors.viewList.Add (analyzerSensors.snapArea1);

      analyzerSensors.snapArea2.snapArea = new UIView(analyzerSensors.snapRect2){
        AccessibilityIdentifier = "2",

      };
      analyzerSensors.snapArea2.availableView = new UIView(new CGRect(0,0,analyzerSensors.snapArea2.snapArea.Bounds.Width,analyzerSensors.snapArea2.snapArea.Bounds.Height)){
				BackgroundColor = UIColor.FromRGB(204,153,0),
				Alpha = .4f,
				UserInteractionEnabled = true,
				AccessibilityIdentifier = "2",
        Hidden = false,
			};
      analyzerSensors.snapArea2.addIcon = new UIImageView(new CGRect(.107 * analyzerSensors.snapArea2.snapArea.Bounds.Width,.107 * analyzerSensors.snapArea2.snapArea.Bounds.Height,.769 * analyzerSensors.snapArea2.snapArea.Bounds.Width,.769 * analyzerSensors.snapArea2.snapArea.Bounds.Height));
      analyzerSensors.snapArea2.addIcon.Image = UIImage.FromBundle("ic_device_add");
      analyzerSensors.snapArea2.addIcon.BackgroundColor = UIColor.Clear;
			analyzerSensors.snapArea2.snapArea.Layer.CornerRadius = 10;
      analyzerSensors.snapArea2.availableView.Layer.CornerRadius = 10;
      analyzerSensors.snapArea2.snapArea.Layer.BorderColor = UIColor.Black.CGColor;
      analyzerSensors.snapArea2.snapArea.Layer.BorderWidth = 2f;
      analyzerSensors.snapArea2.snapArea.AddSubview(analyzerSensors.snapArea2.availableView);
      analyzerSensors.snapArea2.snapArea.AddSubview(analyzerSensors.snapArea2.addIcon);
      analyzerSensors.snapArea2.snapArea.BringSubviewToFront(analyzerSensors.snapArea2.addIcon);
      CreateSubviewLayout (analyzerSensors.snapArea2.snapArea, analyzerSensors.snapArea2.topLabel, analyzerSensors.snapArea2.tLabelBottom, analyzerSensors.snapArea2.middleLabel, analyzerSensors.snapArea2.bottomLabel);
			mainView.AddSubview (analyzerSensors.snapArea2.snapArea);
			analyzerSensors.viewList.Add (analyzerSensors.snapArea2);

      analyzerSensors.snapArea3.snapArea = new UIView(analyzerSensors.snapRect3){
        AccessibilityIdentifier = "3",

      };
      analyzerSensors.snapArea3.availableView = new UIView(new CGRect(0,0,analyzerSensors.snapArea3.snapArea.Bounds.Width,analyzerSensors.snapArea3.snapArea.Bounds.Height)){
				BackgroundColor = UIColor.FromRGB(204,153,0),
				Alpha = .4f,
				UserInteractionEnabled = true,
				AccessibilityIdentifier = "3",
        Hidden = false,
			};
      analyzerSensors.snapArea3.addIcon = new UIImageView(new CGRect(.107 * analyzerSensors.snapArea3.snapArea.Bounds.Width,.107 * analyzerSensors.snapArea3.snapArea.Bounds.Height,.769 * analyzerSensors.snapArea3.snapArea.Bounds.Width,.769 * analyzerSensors.snapArea3.snapArea.Bounds.Height));
      analyzerSensors.snapArea3.addIcon.Image = UIImage.FromBundle("ic_device_add");
      analyzerSensors.snapArea3.addIcon.BackgroundColor = UIColor.Clear;
			analyzerSensors.snapArea3.snapArea.Layer.CornerRadius = 10;
      analyzerSensors.snapArea3.availableView.Layer.CornerRadius = 10;
      analyzerSensors.snapArea3.snapArea.Layer.BorderColor = UIColor.Black.CGColor;
      analyzerSensors.snapArea3.snapArea.Layer.BorderWidth = 2f;
      analyzerSensors.snapArea3.snapArea.AddSubview(analyzerSensors.snapArea3.availableView);
      analyzerSensors.snapArea3.snapArea.AddSubview(analyzerSensors.snapArea3.addIcon);
      analyzerSensors.snapArea3.snapArea.BringSubviewToFront(analyzerSensors.snapArea3.addIcon);
      CreateSubviewLayout (analyzerSensors.snapArea3.snapArea, analyzerSensors.snapArea3.topLabel, analyzerSensors.snapArea3.tLabelBottom, analyzerSensors.snapArea3.middleLabel, analyzerSensors.snapArea3.bottomLabel);
			mainView.AddSubview (analyzerSensors.snapArea3.snapArea);
			analyzerSensors.viewList.Add (analyzerSensors.snapArea3);

      analyzerSensors.snapArea4.snapArea = new UIView(analyzerSensors.snapRect4){ 
        AccessibilityIdentifier = "4",

      };
      analyzerSensors.snapArea4.availableView = new UIView(new CGRect(0,0,analyzerSensors.snapArea4.snapArea.Bounds.Width,analyzerSensors.snapArea4.snapArea.Bounds.Height)){
				BackgroundColor = UIColor.FromRGB(204,153,0),
				Alpha = .4f,
				UserInteractionEnabled = true,
				AccessibilityIdentifier = "4",
        Hidden = false,
			};
      analyzerSensors.snapArea4.addIcon = new UIImageView(new CGRect(.107 * analyzerSensors.snapArea4.snapArea.Bounds.Width,.107 * analyzerSensors.snapArea4.snapArea.Bounds.Height,.769 * analyzerSensors.snapArea4.snapArea.Bounds.Width,.769 * analyzerSensors.snapArea4.snapArea.Bounds.Height));
      analyzerSensors.snapArea4.addIcon.Image = UIImage.FromBundle("ic_device_add");
      analyzerSensors.snapArea4.addIcon.BackgroundColor = UIColor.Clear;
			analyzerSensors.snapArea4.snapArea.Layer.CornerRadius = 10;
      analyzerSensors.snapArea4.availableView.Layer.CornerRadius = 10;
      analyzerSensors.snapArea4.snapArea.Layer.BorderColor = UIColor.Black.CGColor;
      analyzerSensors.snapArea4.snapArea.Layer.BorderWidth = 2f;
      analyzerSensors.snapArea4.snapArea.AddSubview(analyzerSensors.snapArea4.availableView);
      analyzerSensors.snapArea4.snapArea.AddSubview(analyzerSensors.snapArea4.addIcon);
      analyzerSensors.snapArea4.snapArea.BringSubviewToFront(analyzerSensors.snapArea4.addIcon);
      CreateSubviewLayout (analyzerSensors.snapArea4.snapArea, analyzerSensors.snapArea4.topLabel, analyzerSensors.snapArea4.tLabelBottom, analyzerSensors.snapArea4.middleLabel, analyzerSensors.snapArea4.bottomLabel);
			mainView.AddSubview (analyzerSensors.snapArea4.snapArea);
			analyzerSensors.viewList.Add (analyzerSensors.snapArea4);

      analyzerSensors.snapArea5.snapArea = new UIView(analyzerSensors.snapRect5){
        AccessibilityIdentifier = "5",

      };
      analyzerSensors.snapArea5.availableView = new UIView(new CGRect(0,0,analyzerSensors.snapArea5.snapArea.Bounds.Width,analyzerSensors.snapArea5.snapArea.Bounds.Height)){
				BackgroundColor = UIColor.FromRGB(204,153,0),
				Alpha = .4f,
				UserInteractionEnabled = true,
				AccessibilityIdentifier = "5",
        Hidden = false,
			};
      analyzerSensors.snapArea5.addIcon = new UIImageView(new CGRect(.107 * analyzerSensors.snapArea5.snapArea.Bounds.Width,.107 * analyzerSensors.snapArea5.snapArea.Bounds.Height,.769 * analyzerSensors.snapArea5.snapArea.Bounds.Width,.769 * analyzerSensors.snapArea5.snapArea.Bounds.Height));
      analyzerSensors.snapArea5.addIcon.Image = UIImage.FromBundle("ic_device_add");
      analyzerSensors.snapArea5.addIcon.BackgroundColor = UIColor.Clear;
			analyzerSensors.snapArea5.snapArea.Layer.CornerRadius = 10;
      analyzerSensors.snapArea5.availableView.Layer.CornerRadius = 10;
      analyzerSensors.snapArea5.snapArea.Layer.BorderColor = UIColor.Black.CGColor;
      analyzerSensors.snapArea5.snapArea.Layer.BorderWidth = 2f;
      analyzerSensors.snapArea5.snapArea.AddSubview(analyzerSensors.snapArea5.availableView);
      analyzerSensors.snapArea5.snapArea.AddSubview(analyzerSensors.snapArea5.addIcon);
      analyzerSensors.snapArea5.snapArea.BringSubviewToFront(analyzerSensors.snapArea5.addIcon);
      CreateSubviewLayout (analyzerSensors.snapArea5.snapArea, analyzerSensors.snapArea5.topLabel, analyzerSensors.snapArea5.tLabelBottom, analyzerSensors.snapArea5.middleLabel, analyzerSensors.snapArea5.bottomLabel);
			mainView.AddSubview (analyzerSensors.snapArea5.snapArea);
			analyzerSensors.viewList.Add (analyzerSensors.snapArea5);

      analyzerSensors.snapArea6.snapArea = new UIView(analyzerSensors.snapRect6){
        AccessibilityIdentifier = "6",

      };
      analyzerSensors.snapArea6.availableView = new UIView(new CGRect(0,0,analyzerSensors.snapArea6.snapArea.Bounds.Width,analyzerSensors.snapArea6.snapArea.Bounds.Height)){
				BackgroundColor = UIColor.FromRGB(204,153,0),
				Alpha = .4f,
				UserInteractionEnabled = true,
				AccessibilityIdentifier = "6",
        Hidden = false,
			};
      analyzerSensors.snapArea6.addIcon = new UIImageView(new CGRect(.107 * analyzerSensors.snapArea6.snapArea.Bounds.Width,.107 * analyzerSensors.snapArea6.snapArea.Bounds.Height,.769 * analyzerSensors.snapArea6.snapArea.Bounds.Width,.769 * analyzerSensors.snapArea6.snapArea.Bounds.Height));
      analyzerSensors.snapArea6.addIcon.Image = UIImage.FromBundle("ic_device_add");
      analyzerSensors.snapArea6.addIcon.BackgroundColor = UIColor.Clear;
			analyzerSensors.snapArea6.snapArea.Layer.CornerRadius = 10;
      analyzerSensors.snapArea6.availableView.Layer.CornerRadius = 10;
      analyzerSensors.snapArea6.snapArea.Layer.BorderColor = UIColor.Black.CGColor;
      analyzerSensors.snapArea6.snapArea.Layer.BorderWidth = 2f;
      analyzerSensors.snapArea6.snapArea.AddSubview(analyzerSensors.snapArea6.availableView);
      analyzerSensors.snapArea6.snapArea.AddSubview(analyzerSensors.snapArea6.addIcon);
      analyzerSensors.snapArea6.snapArea.BringSubviewToFront(analyzerSensors.snapArea6.addIcon);
      CreateSubviewLayout (analyzerSensors.snapArea6.snapArea, analyzerSensors.snapArea6.topLabel, analyzerSensors.snapArea6.tLabelBottom, analyzerSensors.snapArea6.middleLabel, analyzerSensors.snapArea6.bottomLabel);
			mainView.AddSubview (analyzerSensors.snapArea6.snapArea);
			analyzerSensors.viewList.Add (analyzerSensors.snapArea6);

      analyzerSensors.snapArea7.snapArea = new UIView(analyzerSensors.snapRect7){
        AccessibilityIdentifier = "7",

      };
      analyzerSensors.snapArea7.availableView = new UIView(new CGRect(0,0,analyzerSensors.snapArea7.snapArea.Bounds.Width,analyzerSensors.snapArea7.snapArea.Bounds.Height)){
				BackgroundColor = UIColor.FromRGB(204,153,0),
				Alpha = .4f,
				UserInteractionEnabled = true,
				AccessibilityIdentifier = "7",
        Hidden = false,
			};
      analyzerSensors.snapArea7.addIcon = new UIImageView(new CGRect(.107 * analyzerSensors.snapArea7.snapArea.Bounds.Width,.107 * analyzerSensors.snapArea7.snapArea.Bounds.Height,.769 * analyzerSensors.snapArea7.snapArea.Bounds.Width,.769 * analyzerSensors.snapArea7.snapArea.Bounds.Height));
      analyzerSensors.snapArea7.addIcon.Image = UIImage.FromBundle("ic_device_add");
      analyzerSensors.snapArea7.addIcon.BackgroundColor = UIColor.Clear;
			analyzerSensors.snapArea7.snapArea.Layer.CornerRadius = 10;
      analyzerSensors.snapArea7.availableView.Layer.CornerRadius = 10;
      analyzerSensors.snapArea7.snapArea.Layer.BorderColor = UIColor.Black.CGColor;
      analyzerSensors.snapArea7.snapArea.Layer.BorderWidth = 2f;
      analyzerSensors.snapArea7.snapArea.AddSubview(analyzerSensors.snapArea7.availableView);
      analyzerSensors.snapArea7.snapArea.AddSubview(analyzerSensors.snapArea7.addIcon);
      analyzerSensors.snapArea7.snapArea.BringSubviewToFront(analyzerSensors.snapArea7.addIcon);
      CreateSubviewLayout (analyzerSensors.snapArea7.snapArea, analyzerSensors.snapArea7.topLabel, analyzerSensors.snapArea7.tLabelBottom, analyzerSensors.snapArea7.middleLabel, analyzerSensors.snapArea7.bottomLabel);
			mainView.AddSubview (analyzerSensors.snapArea7.snapArea);
			analyzerSensors.viewList.Add (analyzerSensors.snapArea7);

      analyzerSensors.snapArea8.snapArea = new UIView(analyzerSensors.snapRect8){
        AccessibilityIdentifier = "8",
      };
      analyzerSensors.snapArea8.availableView = new UIView(new CGRect(0,0,analyzerSensors.snapArea8.snapArea.Bounds.Width,analyzerSensors.snapArea8.snapArea.Bounds.Height)){
				BackgroundColor = UIColor.FromRGB(204,153,0),
				Alpha = .4f,
				UserInteractionEnabled = true,
				AccessibilityIdentifier = "8",
        Hidden = false,
			};
      analyzerSensors.snapArea8.addIcon = new UIImageView(new CGRect(.107 * analyzerSensors.snapArea8.snapArea.Bounds.Width,.107 * analyzerSensors.snapArea8.snapArea.Bounds.Height,.769 * analyzerSensors.snapArea8.snapArea.Bounds.Width,.769 * analyzerSensors.snapArea8.snapArea.Bounds.Height));
      analyzerSensors.snapArea8.addIcon.Image = UIImage.FromBundle("ic_device_add");
      analyzerSensors.snapArea8.addIcon.BackgroundColor = UIColor.Clear;
			analyzerSensors.snapArea8.snapArea.Layer.CornerRadius = 10;
      analyzerSensors.snapArea8.availableView.Layer.CornerRadius = 10;
      analyzerSensors.snapArea8.snapArea.Layer.BorderColor = UIColor.Black.CGColor;
      analyzerSensors.snapArea8.snapArea.Layer.BorderWidth = 3f;
      analyzerSensors.snapArea8.snapArea.AddSubview(analyzerSensors.snapArea8.availableView);
      analyzerSensors.snapArea8.snapArea.AddSubview(analyzerSensors.snapArea8.addIcon);
      analyzerSensors.snapArea8.snapArea.BringSubviewToFront(analyzerSensors.snapArea8.addIcon);
      CreateSubviewLayout (analyzerSensors.snapArea8.snapArea, analyzerSensors.snapArea8.topLabel, analyzerSensors.snapArea8.tLabelBottom, analyzerSensors.snapArea8.middleLabel, analyzerSensors.snapArea8.bottomLabel);
			mainView.AddSubview (analyzerSensors.snapArea8.snapArea);
			analyzerSensors.viewList.Add (analyzerSensors.snapArea8);

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
      lowHighSensors.lowArea.snapArea.BringSubviewToFront(lowHighSensors.lowArea.headingDivider);
      lowHighSensors.lowArea.snapArea.AddSubview (lowHighSensors.lowArea.connectionColor);
      lowHighSensors.lowArea.snapArea.BringSubviewToFront(lowHighSensors.lowArea.Connection);
      lowHighSensors.lowArea.snapArea.AddSubview(lowHighSensors.lowArea.conDisButton);
      lowHighSensors.lowArea.snapArea.BringSubviewToFront(lowHighSensors.lowArea.conDisButton);

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
      lowHighSensors.highArea.snapArea.BringSubviewToFront(lowHighSensors.highArea.headingDivider);
      lowHighSensors.highArea.snapArea.AddSubview (lowHighSensors.highArea.connectionColor);
      lowHighSensors.highArea.snapArea.BringSubviewToFront(lowHighSensors.highArea.Connection);
      lowHighSensors.highArea.snapArea.AddSubview(lowHighSensors.highArea.conDisButton);
      lowHighSensors.highArea.snapArea.BringSubviewToFront(lowHighSensors.highArea.conDisButton);

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

      middleLabel.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect (0, .261 * subview.Bounds.Height, subview.Frame.Size.Width, .461 * subview.Frame.Size.Height);
			middleLabel.AdjustsFontSizeToFitWidth = true;
			middleLabel.Text = "0.00";
			middleLabel.Hidden = true;
			middleLabel.TextAlignment = UITextAlignment.Right;

      bottomLabel.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect (0, .676 * subview.Bounds.Height, .969 * subview.Frame.Size.Width, .261 * subview.Frame.Size.Height);
			bottomLabel.AdjustsFontSizeToFitWidth = true;
			bottomLabel.Text = "psig";
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
		public static void RemoveDevice(lowHighSensor Sensor, sensor removeSensor, string middleText){
			Sensor.LabelTop.BackgroundColor = UIColor.Clear;
			Sensor.LabelTop.TextColor = UIColor.Black;
			Sensor.tableSubviews = new List<string> ();
      Sensor.subviewTable.Source = null;
      Sensor.subviewTable.ReloadData ();
      Sensor.subviewTable.Hidden = true;

      if (Sensor.currentSensor != null) {
        //Sensor.currentSensor.onSensorStateChangedEvent -= Sensor.gaugeUpdating;
        Sensor.currentSensor = null;
      }
			if(middleText.Contains("Low")){
				Sensor.snapArea.AccessibilityIdentifier = "low";
			} else {
        Sensor.snapArea.AccessibilityIdentifier = "high";
			}

			Sensor.LabelTop.Text = "";
			Sensor.LabelMiddle.Text = middleText;
      Sensor.LabelMiddle.Font = UIFont.FromName("Helvetica", 12f);
      Sensor.LabelMiddle.TextAlignment = UITextAlignment.Center;
			Sensor.LabelBottom.Text = "";
			Sensor.LabelSubview.Text = "";
			Sensor.LabelSubview.BackgroundColor = UIColor.Clear;
      Sensor.DeviceImage.Hidden = true;
      Sensor.Connection.Hidden = true;
      Sensor.connectionColor.Hidden = true;
      Sensor.headingDivider.Hidden = true;
      Sensor.max = 0;
      Sensor.min = 0;
      Sensor.isManual = false;

			removeSensor.topLabel.BackgroundColor = UIColor.Clear;
      removeSensor.topLabel.TextColor = UIColor.Black;
      removeSensor.tLabelBottom.Hidden = true;
		}
		/// <summary>
		/// WHAT TODO WHEN THEY WANT TO REMOVE A SINGLE SENSOR
		/// </summary>
		public static void RemoveDevice(actionPopup Sensor, LowHighArea lowHighSensors){
      Sensor.pressedSensor.snapArea.RemoveGestureRecognizer (Sensor.addLong);
      Sensor.pressedSensor.snapArea.RemoveGestureRecognizer(Sensor.addPan);
      Sensor.pressedSensor.availableView.Hidden = false;
      Sensor.pressedView.BackgroundColor = UIColor.Clear;

      Sensor.pressedSensor.topLabel.Hidden = true;
      Sensor.pressedSensor.topLabel.BackgroundColor = UIColor.Clear;
      Sensor.pressedSensor.topLabel.TextColor = UIColor.Black;
      Sensor.pressedSensor.tLabelBottom.Hidden = true;
      Sensor.pressedSensor.middleLabel.Hidden = true;
      Sensor.pressedSensor.bottomLabel.Hidden = true;
      Sensor.pressedSensor.topLabel.Text = "Press " + Sensor.pressedSensor.snapArea.AccessibilityIdentifier;
      Sensor.pressedSensor.middleLabel.Text = "0.00";
      Sensor.pressedSensor.bottomLabel.Text = "psig";
      if (Sensor.pressedSensor.currentSensor != null) {
        Sensor.pressedSensor.currentSensor.device.Dispose();
      }
      //not sure if removing should disconnect the device.....
      //Sensor.pressedSensor.currentSensor.device.connection.Disconnect();
      if (Sensor.pressedSensor.isManual) {
        Sensor.pressedSensor.currentSensor.onSensorStateChangedEvent -= Sensor.pressedSensor.gaugeUpdating;      
      }
      Sensor.pressedSensor.currentSensor = null;
      if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == Sensor.pressedSensor.snapArea.AccessibilityIdentifier) {
        lowHighSensors.lowArea.currentSensor = null;
				lowHighSensors.lowArea.LabelTop.BackgroundColor = UIColor.Clear;
				lowHighSensors.lowArea.LabelTop.TextColor = UIColor.Black;
        lowHighSensors.lowArea.subviewTable.Source = new AnalyzerTableSource(new List<string>(), "lowHighCell", lowHighSensors.lowArea);
				lowHighSensors.lowArea.subviewTable.ReloadData ();
				lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
				lowHighSensors.lowArea.LabelTop.Text = "";
        lowHighSensors.lowArea.LabelMiddle.Font = UIFont.FromName("Helvetica", 12f);
				lowHighSensors.lowArea.LabelMiddle.Text = "Low Viewer Not Defined";
        lowHighSensors.lowArea.LabelMiddle.TextAlignment = UITextAlignment.Center;
        lowHighSensors.lowArea.LabelBottom.Text = "";
        lowHighSensors.lowArea.subviewDivider.Hidden = false;
				lowHighSensors.lowArea.LabelSubview.Text = "";
				lowHighSensors.lowArea.LabelSubview.ClipsToBounds = true;
				lowHighSensors.lowArea.LabelSubview.BackgroundColor = UIColor.Clear;
        lowHighSensors.lowArea.DeviceImage.Hidden = true;
        lowHighSensors.lowArea.Connection.Hidden = true;
        lowHighSensors.lowArea.connectionColor.Hidden = true;
        lowHighSensors.lowArea.headingDivider.Hidden = true;
				lowHighSensors.lowArea.subviewTable.Hidden = true;
				lowHighSensors.lowArea.tableSubviews = new List<string> ();
        lowHighSensors.lowArea.max = 0;
        lowHighSensors.lowArea.min = 0;
        if(lowHighSensors.lowArea.currentSensor != null){
          //lowHighSensors.lowArea.currentSensor.onSensorStateChangedEvent -= lowHighSensors.highArea.gaugeUpdating;
          lowHighSensors.lowArea.currentSensor.device.Dispose();
        }
        lowHighSensors.lowArea.isManual = false;

			}
      if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == Sensor.pressedSensor.snapArea.AccessibilityIdentifier) {
        lowHighSensors.highArea.currentSensor = null;
				lowHighSensors.highArea.LabelTop.BackgroundColor = UIColor.Clear;
				lowHighSensors.highArea.LabelTop.TextColor = UIColor.Black;
        lowHighSensors.highArea.subviewTable.Source = new AnalyzerTableSource(new List<string>(), "lowHighCell", lowHighSensors.lowArea);
				lowHighSensors.highArea.subviewTable.ReloadData ();
				lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
				lowHighSensors.highArea.LabelTop.Text = "";
        lowHighSensors.highArea.LabelMiddle.Font = UIFont.FromName("Helvetica", 12f);
				lowHighSensors.highArea.LabelMiddle.Text = "High Viewer Not Defined";
        lowHighSensors.highArea.LabelMiddle.TextAlignment = UITextAlignment.Center;
        lowHighSensors.highArea.LabelBottom.Text = "";
        lowHighSensors.highArea.subviewDivider.Hidden = false;
				lowHighSensors.highArea.LabelSubview.Text = "";
				lowHighSensors.highArea.LabelSubview.ClipsToBounds = true;
				lowHighSensors.highArea.LabelSubview.BackgroundColor = UIColor.Clear;
        lowHighSensors.highArea.DeviceImage.Hidden = true;
        lowHighSensors.highArea.Connection.Hidden = true;
        lowHighSensors.highArea.connectionColor.Hidden = true;
				lowHighSensors.highArea.subviewTable.Hidden = true;
        lowHighSensors.highArea.headingDivider.Hidden = true;
				lowHighSensors.highArea.tableSubviews = new List<string> ();
        lowHighSensors.highArea.max = 0;
        lowHighSensors.highArea.min = 0;
        if(lowHighSensors.highArea.currentSensor != null){
          //lowHighSensors.highArea.currentSensor.onSensorStateChangedEvent -= lowHighSensors.highArea.gaugeUpdating;
          lowHighSensors.highArea.currentSensor.device.Dispose();
        }
        lowHighSensors.highArea.isManual = false;
			}
      Sensor.pressedSensor.addIcon.Hidden = false;
		}
		/// <summary>
		/// Calculates the position for the Low and High subviews
		/// </summary>
		/// <param name="lowHighSensors">Low high sensors.</param>
		/// <param name="View">View.</param>
		public static void CreateLowHighArea(LowHighArea lowHighSensors, UIView View){
      Console.WriteLine("Low High has view with bounds: " + View.Bounds.ToString());
			////CREATE ANALYSER HIGH AND LOW AREAS
      //lowHighSensors.lowArea.areaRect = new CGRect(View.Center.X - 151, View.Center.Y + 35, 149, 115);
      lowHighSensors.lowArea.areaRect = new CGRect(.028 * View.Bounds.Width, .561 * View.Bounds.Height, .465 * View.Bounds.Width, .202 * View.Bounds.Height);
      //lowHighSensors.highArea.areaRect = new CGRect (View.Center.X + 4, View.Center.Y + 35, 149, 115); 
      lowHighSensors.highArea.areaRect = new CGRect (.512 * View.Bounds.Width, .561 * View.Bounds.Height, .465 * View.Bounds.Width, .202 * View.Bounds.Height);
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
    public static void replaceAlert(string title, string message, int type, sensor Sensor, sensor removeSensor, UITableView tableArea, LowHighArea lowHighSensors, sensorGroup analyzerSensors, UIView View){
			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;
			while (vc.PresentedViewController != null) {
				vc = vc.PresentedViewController;
			}
			bool goOn;
			UIAlertController addDeviceSheet;

			addDeviceSheet = UIAlertController.Create (title, message, UIAlertControllerStyle.Alert);


			addDeviceSheet.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, (action) => {

				if(type == 1){
          goOn = orderSensors(analyzerSensors, analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)),"low",View);
					if(goOn){
						removeSensor.topLabel.BackgroundColor = UIColor.Clear;
            removeSensor.topLabel.TextColor = UIColor.Black;
            removeSensor.tLabelBottom.Hidden = true;
						tableArea.Source = null;
            lowHighSensors.lowArea.tableSubviews = new List<string>();
						tableArea.ReloadData();

            Sensor.topLabel.BackgroundColor = UIColor.Blue;
            Sensor.tLabelBottom.BackgroundColor = UIColor.Blue;
            Sensor.tLabelBottom.Hidden = false;
            Sensor.topLabel.TextColor = UIColor.White;
            lowHighSensors.lowArea.LabelTop.Text = Sensor.topLabel.Text;
            lowHighSensors.lowArea.LabelMiddle.Text = Sensor.middleLabel.Text;
            lowHighSensors.lowArea.LabelMiddle.Font = UIFont.FromName("Helvetica-Bold", 42f);
            lowHighSensors.lowArea.LabelMiddle.TextAlignment = UITextAlignment.Right;
            lowHighSensors.lowArea.LabelBottom.Text = Sensor.bottomLabel.Text;
            lowHighSensors.lowArea.LabelSubview.Text = Sensor.topLabel.Text + "'s Subviews";
						lowHighSensors.lowArea.LabelSubview.BackgroundColor = UIColor.Blue;
            lowHighSensors.lowArea.DeviceImage.Image = Sensor.deviceImage.Image;
            lowHighSensors.lowArea.DeviceImage.Hidden = false;
            lowHighSensors.lowArea.Connection.Image = Sensor.connectionImage.Image;
            lowHighSensors.lowArea.Connection.Hidden = false;
            lowHighSensors.lowArea.connectionColor.Hidden = false;
            lowHighSensors.lowArea.currentSensor = Sensor.currentSensor;
            if(Sensor.currentSensor != null && Sensor.currentSensor.device.isConnected){
              lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Green;
            } else if(Sensor.currentSensor != null && !Sensor.currentSensor.device.isConnected){
              lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Red;
            } else {
              lowHighSensors.lowArea.Connection.Hidden = true;
              lowHighSensors.lowArea.connectionColor.Hidden = true;
              lowHighSensors.lowArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
            }

            lowHighSensors.lowArea.headingDivider.Hidden = false;
            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
            if(Sensor.isManual){
              lowHighSensors.lowArea.isManual = true;
            }
					}
				} else if (type == 2){
          goOn = orderSensors(analyzerSensors,analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)),"high",View);
					if(goOn){
            removeSensor.topLabel.BackgroundColor = UIColor.Clear;
            removeSensor.topLabel.TextColor = UIColor.Black;
            removeSensor.tLabelBottom.Hidden = true;
						tableArea.Source = null;
            lowHighSensors.highArea.tableSubviews = new List<string>();
						tableArea.ReloadData();


            Sensor.topLabel.BackgroundColor = UIColor.Red;
            Sensor.tLabelBottom.BackgroundColor = UIColor.Red;
            Sensor.tLabelBottom.Hidden = false;
            Sensor.topLabel.TextColor = UIColor.White;
            lowHighSensors.highArea.LabelTop.Text = Sensor.topLabel.Text;
            lowHighSensors.highArea.LabelMiddle.Text = Sensor.middleLabel.Text;
            lowHighSensors.highArea.LabelMiddle.Font = UIFont.FromName("Helvetica-Bold", 42f);
            lowHighSensors.highArea.LabelMiddle.TextAlignment = UITextAlignment.Right;
            lowHighSensors.highArea.LabelBottom.Text = Sensor.bottomLabel.Text;
            lowHighSensors.highArea.LabelSubview.Text = Sensor.topLabel.Text + "'s Subviews";
						lowHighSensors.highArea.LabelSubview.BackgroundColor = UIColor.Red;
            lowHighSensors.highArea.DeviceImage.Image = Sensor.deviceImage.Image;
            lowHighSensors.highArea.DeviceImage.Hidden = false;
            lowHighSensors.highArea.Connection.Image = Sensor.connectionImage.Image;
            lowHighSensors.highArea.Connection.Hidden = false;
            lowHighSensors.highArea.connectionColor.Hidden = false;
            lowHighSensors.highArea.currentSensor = Sensor.currentSensor;
            if(Sensor.currentSensor != null && Sensor.currentSensor.device.isConnected){
              lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Green;
            } else if (Sensor.currentSensor != null && Sensor.currentSensor.device.isConnected) {
              lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Red;
            } else {
              lowHighSensors.highArea.Connection.Hidden = true;
              lowHighSensors.highArea.connectionColor.Hidden = true;
              lowHighSensors.highArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
            }
            lowHighSensors.highArea.headingDivider.Hidden = false;
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
            if(Sensor.isManual){
              lowHighSensors.highArea.isManual = true;
            }
					}
				} else if (type == 3){
          goOn = orderSensors(analyzerSensors,analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "low",View);
					if(goOn){
            removeSensor.topLabel.BackgroundColor = UIColor.Clear;
            removeSensor.topLabel.TextColor = UIColor.Black;
            removeSensor.tLabelBottom.Hidden = true;
						tableArea.Source = null;
            lowHighSensors.highArea.tableSubviews = new List<string>();
						tableArea.ReloadData();

						lowHighSensors.highArea.LabelTop.Text = "";
						lowHighSensors.highArea.LabelMiddle.Text = "High Viewer Not Defined";
            lowHighSensors.highArea.LabelMiddle.Font = UIFont.FromName("Helvetica", 12f);
            lowHighSensors.highArea.LabelMiddle.TextAlignment = UITextAlignment.Center;
            lowHighSensors.highArea.LabelBottom.Text = "";
            lowHighSensors.highArea.subviewDivider.Hidden = false;
						lowHighSensors.highArea.LabelSubview.Text = "";
						lowHighSensors.highArea.LabelSubview.BackgroundColor = UIColor.Clear;
						lowHighSensors.highArea.snapArea.AccessibilityIdentifier = "high";
						lowHighSensors.highArea.DeviceImage.Hidden = true;
						lowHighSensors.highArea.Connection.Hidden = true;
            lowHighSensors.highArea.connectionColor.Hidden = true;
            lowHighSensors.highArea.headingDivider.Hidden = true;
            lowHighSensors.highArea.isManual = false;
            Sensor.topLabel.BackgroundColor = UIColor.Blue;
            Sensor.tLabelBottom.BackgroundColor = UIColor.Blue;
            Sensor.tLabelBottom.Hidden = false;
            Sensor.topLabel.TextColor = UIColor.White;
            lowHighSensors.lowArea.LabelTop.Text = Sensor.topLabel.Text;
            lowHighSensors.lowArea.LabelMiddle.Text = Sensor.middleLabel.Text;
            lowHighSensors.lowArea.LabelMiddle.Font = UIFont.FromName("Helvetica-Bold", 42f);
            lowHighSensors.lowArea.LabelMiddle.TextAlignment = UITextAlignment.Right;
            lowHighSensors.lowArea.LabelBottom.Text = Sensor.bottomLabel.Text;
            lowHighSensors.lowArea.LabelSubview.Text = Sensor.topLabel.Text + "'s Subviews";
						lowHighSensors.lowArea.LabelSubview.BackgroundColor = UIColor.Blue;
            lowHighSensors.lowArea.DeviceImage.Image = Sensor.deviceImage.Image;
            lowHighSensors.lowArea.DeviceImage.Hidden = false;
            lowHighSensors.lowArea.Connection.Image = Sensor.connectionImage.Image;
            lowHighSensors.lowArea.Connection.Hidden = false;
            lowHighSensors.lowArea.connectionColor.Hidden = false;
            lowHighSensors.lowArea.currentSensor = Sensor.currentSensor;
            if(lowHighSensors.highArea.currentSensor != null){
              //lowHighSensors.highArea.currentSensor.onSensorStateChangedEvent -= lowHighSensors.highArea.gaugeUpdating;
              lowHighSensors.highArea.currentSensor = null;
            }
            if(Sensor.currentSensor != null && Sensor.currentSensor.device.isConnected){
              lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Green;
            } else if (Sensor.currentSensor != null && !Sensor.currentSensor.device.isConnected) {
              lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Red;
            } else {
              lowHighSensors.lowArea.connectionColor.Hidden = true;
              lowHighSensors.lowArea.Connection.Hidden = true;
              lowHighSensors.lowArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
            }

            lowHighSensors.lowArea.headingDivider.Hidden = false;
            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
            if(Sensor.isManual){
              lowHighSensors.lowArea.isManual = true;
            }
						lowHighSensors.lowArea.subviewTable.Source = null;
            lowHighSensors.lowArea.tableSubviews = new List<string>();
						lowHighSensors.lowArea.subviewTable.ReloadData();
					}
				} else if (type == 4){		
          goOn = orderSensors(analyzerSensors,analyzerSensors.areaList.IndexOf(Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier)), "high",View);
					if(goOn){
            removeSensor.topLabel.BackgroundColor = UIColor.Clear;
            removeSensor.topLabel.TextColor = UIColor.Black;
            removeSensor.tLabelBottom.Hidden = true;
						tableArea.Source = null;
            lowHighSensors.lowArea.tableSubviews = new List<string>();
						tableArea.ReloadData();

						lowHighSensors.lowArea.LabelTop.Text = "";
						lowHighSensors.lowArea.LabelMiddle.Text = "Low Viewer Not Defined";
            lowHighSensors.lowArea.LabelMiddle.Font = UIFont.FromName("Helvetica", 12f);
            lowHighSensors.lowArea.LabelMiddle.TextAlignment = UITextAlignment.Center;
            lowHighSensors.lowArea.subviewDivider.Hidden = false;
            lowHighSensors.lowArea.LabelBottom.Text = "";
						lowHighSensors.lowArea.LabelSubview.Text = "";
						lowHighSensors.lowArea.LabelSubview.BackgroundColor = UIColor.Clear;
						lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = "low";
						lowHighSensors.lowArea.DeviceImage.Hidden = true;
						lowHighSensors.lowArea.Connection.Hidden = true;
            lowHighSensors.lowArea.connectionColor.Hidden = true;
            lowHighSensors.lowArea.headingDivider.Hidden = true;
            lowHighSensors.lowArea.isManual = false;
            Sensor.topLabel.BackgroundColor = UIColor.Red;
            Sensor.tLabelBottom.BackgroundColor = UIColor.Red;
            Sensor.tLabelBottom.Hidden = false;
            lowHighSensors.highArea.LabelTop.Text = Sensor.topLabel.Text;
            lowHighSensors.highArea.LabelMiddle.Text = Sensor.middleLabel.Text;
            lowHighSensors.highArea.LabelMiddle.Font = UIFont.FromName("Helvetica-Bold", 42f);
            lowHighSensors.highArea.LabelMiddle.TextAlignment = UITextAlignment.Right;
            lowHighSensors.highArea.LabelBottom.Text = Sensor.bottomLabel.Text;
            lowHighSensors.highArea.LabelSubview.Text = Sensor.topLabel.Text +"'s Subviews";
						lowHighSensors.highArea.LabelSubview.BackgroundColor = UIColor.Red;
            lowHighSensors.highArea.DeviceImage.Image = Sensor.deviceImage.Image;
            lowHighSensors.highArea.DeviceImage.Hidden = false;
            lowHighSensors.highArea.Connection.Image = Sensor.connectionImage.Image;
            lowHighSensors.highArea.Connection.Hidden = false;
            lowHighSensors.highArea.connectionColor.Hidden = false;
            lowHighSensors.highArea.currentSensor = Sensor.currentSensor;
            if(lowHighSensors.lowArea.currentSensor != null){
              //lowHighSensors.lowArea.currentSensor.onSensorStateChangedEvent -= lowHighSensors.lowArea.gaugeUpdating;
              lowHighSensors.lowArea.currentSensor = null;
            }
            if(Sensor.currentSensor != null && Sensor.currentSensor.device.isConnected){
              lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Green;
            } else if (Sensor.currentSensor != null && !Sensor.currentSensor.device.isConnected) {
              lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Red;
            } else {
              lowHighSensors.highArea.connectionColor.Hidden = true;
              lowHighSensors.highArea.Connection.Hidden = true;
              lowHighSensors.highArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
            }

            lowHighSensors.highArea.headingDivider.Hidden = false;
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
            if(Sensor.isManual){
              lowHighSensors.highArea.isManual = true;
            }
						lowHighSensors.highArea.subviewTable.Source = null;
            lowHighSensors.highArea.tableSubviews = new List<string>();
						lowHighSensors.highArea.subviewTable.ReloadData();
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
              replaceAlert ("Complete Swap?", message, 3, Sensor, analyzerSensors.snapArea1, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);					
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "2") {
              replaceAlert ("Complete Swap?", message, 3, Sensor, analyzerSensors.snapArea2, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "3") {
              replaceAlert ("Complete Swap?", message, 3, Sensor, analyzerSensors.snapArea3, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "4") {
              replaceAlert ("Complete Swap?", message, 3, Sensor, analyzerSensors.snapArea4, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "5") {
              replaceAlert ("Complete Swap?", message, 3, Sensor, analyzerSensors.snapArea5, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "6") {
              replaceAlert ("Complete Swap?", message, 3, Sensor, analyzerSensors.snapArea6, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "7") {
              replaceAlert ("Complete Swap?", message, 3, Sensor, analyzerSensors.snapArea7, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "8") {
              replaceAlert ("Complete Swap?", message, 3, Sensor, analyzerSensors.snapArea8, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "low") {
              replaceAlert ("Complete Swap?", message, 3, Sensor, analyzerSensors.snapArea1, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
            }
          }
				} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier != "low") {
          if(!freeSpot(analyzerSensors,"low")){
            UIAlertController fullPopup = UIAlertController.Create ("Cannot Move Sensor", "Not enough space", UIAlertControllerStyle.Alert);

            fullPopup.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, (action) => {}));            

            vc.PresentViewController (fullPopup, true, null);
          } else {
  					string message = "Completing this action will cause the Low side subview arrangement to be lost.";
  					if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "1") {
              replaceAlert ("Complete Swap?", message, 1, Sensor, analyzerSensors.snapArea1, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);					
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "2") {
              replaceAlert ("Complete Swap?", message, 1, Sensor, analyzerSensors.snapArea2, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "3") {
              replaceAlert ("Complete Swap?", message, 1, Sensor, analyzerSensors.snapArea3, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "4") {
              replaceAlert ("Complete Swap?", message, 1, Sensor, analyzerSensors.snapArea4, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "5") {
              replaceAlert ("Complete Swap?", message, 1, Sensor, analyzerSensors.snapArea5, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "6") {
              replaceAlert ("Complete Swap?", message, 1, Sensor, analyzerSensors.snapArea6, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "7") {
              replaceAlert ("Complete Swap?", message, 1, Sensor, analyzerSensors.snapArea7, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
  					} else if (lowHighSensors.lowArea.snapArea.AccessibilityIdentifier == "8") {
              replaceAlert ("Complete Swap?", message, 1, Sensor, analyzerSensors.snapArea8, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
            }
          }

				} else {
          bool goOn = orderSensors (analyzerSensors, analyzerSensors.areaList.IndexOf (Convert.ToInt32 (Sensor.snapArea.AccessibilityIdentifier)), "low", View);
					if (goOn) {
            lowHighSensors.lowArea.LabelTop.Text = Sensor.topLabel.Text;
            lowHighSensors.lowArea.LabelMiddle.Text = Sensor.middleLabel.Text;
            lowHighSensors.lowArea.LabelMiddle.Font = UIFont.FromName("Helvetica-Bold", 42f);
            lowHighSensors.lowArea.LabelMiddle.TextAlignment = UITextAlignment.Right;
            lowHighSensors.lowArea.LabelBottom.Text = Sensor.bottomLabel.Text;
            lowHighSensors.lowArea.LabelSubview.Text = Sensor.topLabel.Text + "'s Subviews";
						lowHighSensors.lowArea.LabelSubview.BackgroundColor = UIColor.Blue;
            lowHighSensors.lowArea.DeviceImage.Image = Sensor.deviceImage.Image;
						lowHighSensors.lowArea.DeviceImage.Hidden = false;
            lowHighSensors.lowArea.Connection.Image = Sensor.connectionImage.Image;
						lowHighSensors.lowArea.Connection.Hidden = false;
            lowHighSensors.lowArea.currentSensor = Sensor.currentSensor;
            lowHighSensors.lowArea.headingDivider.Hidden = false;
            lowHighSensors.lowArea.manifold = new ION.Core.Content.Manifold(Sensor.currentSensor);
            if (Sensor.currentSensor != null && lowHighSensors.lowArea.currentSensor.device.isConnected) {
              lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Green;
              lowHighSensors.lowArea.connectionColor.Hidden = false;
            } else if (Sensor.currentSensor != null && !lowHighSensors.lowArea.currentSensor.device.isConnected) {
              lowHighSensors.lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Red;
              lowHighSensors.lowArea.connectionColor.Hidden = false;      
            } else {
              lowHighSensors.lowArea.connectionColor.BackgroundColor = UIColor.Clear;
              lowHighSensors.lowArea.connectionColor.Hidden = true;
              lowHighSensors.lowArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
              lowHighSensors.lowArea.Connection.Hidden = true;
            }
            Sensor.topLabel.BackgroundColor = UIColor.Blue;
            Sensor.tLabelBottom.BackgroundColor = UIColor.Blue;
            Sensor.tLabelBottom.Hidden = false;
            Sensor.topLabel.TextColor = UIColor.White;
            lowHighSensors.lowArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
            if (Sensor.isManual) {
              lowHighSensors.lowArea.isManual = true;
            }
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
              replaceAlert("Complete Swap?", message, 4, Sensor, analyzerSensors.snapArea1, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);					
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "2") {
              replaceAlert("Complete Swap?", message, 4, Sensor, analyzerSensors.snapArea2, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "3") {
              replaceAlert("Complete Swap?", message, 4, Sensor, analyzerSensors.snapArea3, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "4") {
              replaceAlert("Complete Swap?", message, 4, Sensor, analyzerSensors.snapArea4, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "5") {
              replaceAlert("Complete Swap?", message, 4, Sensor, analyzerSensors.snapArea5, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "6") {
              replaceAlert("Complete Swap?", message, 4, Sensor, analyzerSensors.snapArea6, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "7") {
              replaceAlert("Complete Swap?", message, 4, Sensor, analyzerSensors.snapArea7, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "8") {
              replaceAlert("Complete Swap?", message, 4, Sensor, analyzerSensors.snapArea8, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "high") {
              replaceAlert("Complete Swap?", message, 4, Sensor, analyzerSensors.snapArea1, lowHighSensors.lowArea.subviewTable, lowHighSensors, analyzerSensors, View);
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
            if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "1") {
              replaceAlert("Complete Swap?", message, 2, Sensor, analyzerSensors.snapArea1, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);					
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "2") {
              replaceAlert("Complete Swap?", message, 2, Sensor, analyzerSensors.snapArea2, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "3") {
              replaceAlert("Complete Swap?", message, 2, Sensor, analyzerSensors.snapArea3, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "4") {
              replaceAlert("Complete Swap?", message, 2, Sensor, analyzerSensors.snapArea4, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "5") {
              replaceAlert("Complete Swap?", message, 2, Sensor, analyzerSensors.snapArea5, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "6") {
              replaceAlert("Complete Swap?", message, 2, Sensor, analyzerSensors.snapArea6, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "7") {
              replaceAlert("Complete Swap?", message, 2, Sensor, analyzerSensors.snapArea7, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else if (lowHighSensors.highArea.snapArea.AccessibilityIdentifier == "8") {
              replaceAlert("Complete Swap?", message, 2, Sensor, analyzerSensors.snapArea8, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
            } else {
              replaceAlert("Complete Swap?", message, 2, Sensor, analyzerSensors.snapArea1, lowHighSensors.highArea.subviewTable, lowHighSensors, analyzerSensors, View);
            }
          }
				} else {
          bool goOn = orderSensors (analyzerSensors, analyzerSensors.areaList.IndexOf (Convert.ToInt32 (Sensor.snapArea.AccessibilityIdentifier)), "high", View);
					if (goOn) {
            lowHighSensors.highArea.LabelTop.Text = Sensor.topLabel.Text;
            lowHighSensors.highArea.LabelMiddle.Text = Sensor.middleLabel.Text;
            lowHighSensors.highArea.LabelMiddle.Font = UIFont.FromName("Helvetica-Bold", 42f);
            lowHighSensors.highArea.LabelMiddle.TextAlignment = UITextAlignment.Right;
            lowHighSensors.highArea.LabelBottom.Text = Sensor.bottomLabel.Text;
            lowHighSensors.highArea.LabelSubview.Text = Sensor.topLabel.Text + "'s Subviews";
						lowHighSensors.highArea.LabelSubview.BackgroundColor = UIColor.Red;
            lowHighSensors.highArea.DeviceImage.Image = Sensor.deviceImage.Image;
						lowHighSensors.highArea.DeviceImage.Hidden = false;
            lowHighSensors.highArea.Connection.Image = Sensor.connectionImage.Image;
						lowHighSensors.highArea.Connection.Hidden = false;
            lowHighSensors.highArea.currentSensor = Sensor.currentSensor;
            lowHighSensors.highArea.headingDivider.Hidden = false;
            if (Sensor.currentSensor != null && Sensor.currentSensor.device.isConnected) {
              lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_connected");
              lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Green;
              lowHighSensors.highArea.connectionColor.Hidden = false;
            } else if (Sensor.currentSensor != null && !Sensor.currentSensor.device.isConnected) {
              lowHighSensors.highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
              lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Red;
              lowHighSensors.highArea.connectionColor.Hidden = false;
            } else {
              lowHighSensors.highArea.connectionColor.BackgroundColor = UIColor.Clear;
              lowHighSensors.highArea.connectionColor.Hidden = true;
              lowHighSensors.highArea.DeviceImage.Image = UIImage.FromBundle("ic_edit");
              lowHighSensors.highArea.Connection.Hidden = true;
            }
            Sensor.topLabel.BackgroundColor = UIColor.Red;
            Sensor.tLabelBottom.BackgroundColor = UIColor.Red;
            Sensor.tLabelBottom.Hidden = false;
            Sensor.topLabel.TextColor = UIColor.White;
            lowHighSensors.highArea.snapArea.AccessibilityIdentifier = Sensor.snapArea.AccessibilityIdentifier;
            if (Sensor.isManual) {
              lowHighSensors.highArea.isManual = true;
            }
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

