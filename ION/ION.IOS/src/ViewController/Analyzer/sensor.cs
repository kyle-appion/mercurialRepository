using System.Collections.Generic;
using UIKit;
using CoreGraphics;
using Appion.Commons.Measure;
using ION.Core.Devices;
using ION.Core.Sensors;
using System;
using ION.Core.App;

namespace ION.IOS.ViewController.Analyzer
{
  public class sensor
	{
		public UIView snapArea;
    public UIView availableView;

    public UIImageView addIcon;
    public UIImageView connectionImage = new UIImageView();
    public UIImageView deviceImage = new UIImageView ();

		public UILabel topLabel = new UILabel();

		public UILabel middleLabel = new UILabel();
		public UILabel bottomLabel = new UILabel();
		public UITapGestureRecognizer shortPressGesture;
		public UIPanGestureRecognizer panGesture;
		public UILongPressGestureRecognizer holdGesture;
    public IION ion;
    public GaugeDeviceSensor currentSensor {
      get { return __currentSensor;}
      set { if (__currentSensor != null) {
          __currentSensor.onSensorStateChangedEvent -= gaugeUpdating;
            }
        __currentSensor = value;

        if (__currentSensor != null) {
          __currentSensor.onSensorStateChangedEvent += gaugeUpdating;
        }
      }
    } GaugeDeviceSensor __currentSensor;

    public ManualSensor manualSensor{
      get { return __manualSensor; }
      set {
        __manualSensor = value;  
      }
    } ManualSensor __manualSensor;
       
    public bool isManual = false;
    
    public sensor (CGRect sensorRect, string identifier, UIView mainView)
		{
      ion = AppState.context;
			snapArea = new UIView(sensorRect) {
				AccessibilityIdentifier = identifier,
			};
			snapArea.ClipsToBounds = true;

			availableView = new UIView(new CGRect(0, 0, snapArea.Bounds.Width, snapArea.Bounds.Height)) {
				BackgroundColor = UIColor.FromRGB(204, 153, 0),
				Alpha = .4f,
				UserInteractionEnabled = true,
				AccessibilityIdentifier = identifier,
				Hidden = false,
			};


			addIcon = new UIImageView(new CGRect(.107 * snapArea.Bounds.Width, .107 * snapArea.Bounds.Height, .786 * snapArea.Bounds.Width, .786 * snapArea.Bounds.Height));
			addIcon.Image = UIImage.FromBundle("ic_device_add");
			addIcon.BackgroundColor = UIColor.Clear;
			snapArea.Layer.CornerRadius = 5;
			availableView.Layer.CornerRadius = 5;
			snapArea.Layer.BorderColor = UIColor.Black.CGColor;
			snapArea.Layer.BorderWidth = 2f;
			snapArea.AddSubview(availableView);
			snapArea.AddSubview(addIcon);
			snapArea.BringSubviewToFront(addIcon);

			mainView.AddSubview(snapArea);

			topLabel.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect(0, 0, .99 * snapArea.Frame.Size.Width, .307 * snapArea.Frame.Size.Height);
			topLabel.AdjustsFontSizeToFitWidth = true;
			topLabel.Text = " Press " + snapArea.AccessibilityIdentifier;
			topLabel.Hidden = true;
			topLabel.ClipsToBounds = true;
			topLabel.TextColor = UIColor.Gray;
			topLabel.TextAlignment = UITextAlignment.Center;

			middleLabel.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect(0, .261 * snapArea.Bounds.Height, .984 * snapArea.Frame.Size.Width, .461 * snapArea.Frame.Size.Height);
			middleLabel.AdjustsFontSizeToFitWidth = true;
			middleLabel.Text = "0.00 ";
			middleLabel.Hidden = true;
			middleLabel.TextAlignment = UITextAlignment.Right;
			middleLabel.Font = UIFont.BoldSystemFontOfSize(20);

			bottomLabel.ViewForBaselineLayout.Frame = new CoreGraphics.CGRect(0, .676 * snapArea.Bounds.Height, .99 * snapArea.Frame.Size.Width, .3 * snapArea.Frame.Size.Height);
			bottomLabel.AdjustsFontSizeToFitWidth = true;
			bottomLabel.Text = "";
			bottomLabel.Hidden = true;
			bottomLabel.ClipsToBounds = true;
			bottomLabel.TextAlignment = UITextAlignment.Center;
			bottomLabel.TextColor = UIColor.Gray;

			snapArea.AddSubview(topLabel);
			snapArea.AddSubview(middleLabel);
			snapArea.AddSubview(bottomLabel);
			snapArea.BringSubviewToFront(topLabel);

		}

    public void updateUI(){
			deviceImage.Image = Devices.DeviceUtil.GetUIImageFromDeviceModel(currentSensor.device.serialNumber.deviceModel);
			connectionImage.Image = UIImage.FromBundle("ic_bluetooth_connected");
			snapArea.BackgroundColor = UIColor.White;
			availableView.Hidden = true;
			snapArea.AddGestureRecognizer(panGesture);
			topLabel.Text = " " + currentSensor.device.name;
			topLabel.Hidden = false;
			if (currentSensor.unit != Units.Vacuum.MICRON) {
				middleLabel.Text = currentSensor.measurement.amount.ToString("N") + " ";
			} else {
				middleLabel.Text = currentSensor.measurement.amount + " ";
			}

			middleLabel.Hidden = false;
			bottomLabel.Text = currentSensor.measurement.unit.ToString();
			bottomLabel.Hidden = false;
			addIcon.Hidden = true;
			//isManual = false;

			if (ion.currentAnalyzer.lowSideManifold != null && ion.currentAnalyzer.lowSideManifold.secondarySensor != null && ion.currentAnalyzer.lowSideManifold.secondarySensor == currentSensor) {
				topLabel.BackgroundColor = UIColor.Blue;
				topLabel.TextColor = UIColor.Gray;
			}

			if (ion.currentAnalyzer.highSideManifold != null && ion.currentAnalyzer.highSideManifold.secondarySensor != null && ion.currentAnalyzer.highSideManifold.secondarySensor == currentSensor) {
				topLabel.BackgroundColor = UIColor.Red;
				topLabel.TextColor = UIColor.Gray;
			}
    }

    public void gaugeUpdating(Sensor sensor){
      if (sensor.unit != Units.Vacuum.MICRON) {
        middleLabel.Text = sensor.measurement.amount.ToString("N") + " ";
      } else {
        middleLabel.Text = sensor.measurement.amount.ToString() + " ";
      }
      bottomLabel.Text = sensor.measurement.unit.ToString() + " ";
    }
  }
	
}

