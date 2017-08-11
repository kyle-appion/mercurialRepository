using System;
using Foundation;
using CoreGraphics;
using UIKit;
using ION.IOS.Util;
using ION.Core.Devices;
using ION.Core.Sensors;

namespace ION.IOS.ViewController.DeviceGrid {
  public class GridCellConnected : UICollectionViewCell{
    public UILabel typeLabel,measurementLabel, linkLabel1, linkLabel2, unitLabel;
    public UIView sensorInfoView, sensorStatusView;
    public UIImageView connectionImage, extraImage, workbenchImage, analyzerImage, pressureLinkImage, tempLinkImage;
    public nfloat textSize;
    public static NSString CellID = new NSString("connectedCell");
    public GaugeDeviceSensor slotSensor;

    [Export("initWithFrame:")]
    public GridCellConnected(CGRect frame) : base(frame) {
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
				textSize = 15f;  
			} else {
				textSize = 25f;
			}

      BackgroundView = new UIView { BackgroundColor = UIColor.Clear};

      ContentView.BackgroundColor = UIColor.Clear;

			typeLabel = new UILabel( new CGRect(0,0, ContentView.Bounds.Width, .23 * ContentView.Bounds.Height));
			typeLabel.Font = UIFont.BoldSystemFontOfSize(textSize);
			typeLabel.TextColor = UIColor.White;
      typeLabel.TextAlignment = UITextAlignment.Center;
      typeLabel.Layer.CornerRadius = 5;
      typeLabel.ClipsToBounds = true;
			typeLabel.AdjustsFontSizeToFitWidth = true;
			typeLabel.BackgroundColor = UIColor.Blue;

      sensorInfoView = new UIView(new CGRect(0,.2 * ContentView.Bounds.Height,ContentView.Bounds.Width,.6 * ContentView.Bounds.Height));
      sensorInfoView.BackgroundColor = UIColor.White;

			measurementLabel = new UILabel(new CGRect(0, 0, sensorInfoView.Bounds.Width, .6 * sensorInfoView.Bounds.Height));
      measurementLabel.Font = UIFont.BoldSystemFontOfSize(textSize + 10f);
      measurementLabel.AdjustsFontSizeToFitWidth = true;
      measurementLabel.TextAlignment = UITextAlignment.Right;

			linkLabel1 = new UILabel(new CGRect(.05 * sensorInfoView.Bounds.Width, .65 * sensorInfoView.Bounds.Height, .5 * sensorInfoView.Bounds.Width, .3 * sensorInfoView.Bounds.Height));
      linkLabel1.BackgroundColor = UIColor.Red;
      linkLabel1.TextAlignment = UITextAlignment.Center;
      linkLabel1.Font = UIFont.BoldSystemFontOfSize(textSize);
      linkLabel1.TextColor = UIColor.White;
      linkLabel1.Layer.CornerRadius = 5;
      linkLabel1.ClipsToBounds = true;
      linkLabel1.Text = "SC";
			linkLabel1.AdjustsFontSizeToFitWidth = true;

			linkLabel2 = new UILabel(new CGRect(.01 * sensorInfoView.Bounds.Width, .65 * sensorInfoView.Bounds.Height, .7 * sensorInfoView.Bounds.Width, .3 * sensorInfoView.Bounds.Height));
			linkLabel2.BackgroundColor = UIColor.White;
			linkLabel2.TextAlignment = UITextAlignment.Center;
			linkLabel2.Font = UIFont.BoldSystemFontOfSize(textSize);
			linkLabel2.TextColor = UIColor.Black;
			linkLabel2.Layer.CornerRadius = 5;
			linkLabel2.ClipsToBounds = true;
			linkLabel2.Text = "Live";
			linkLabel2.AdjustsFontSizeToFitWidth = true;
      linkLabel2.Hidden = true;

			unitLabel = new UILabel(new CGRect(.5 * sensorInfoView.Bounds.Width, .65 * sensorInfoView.Bounds.Height,.49 * sensorInfoView.Bounds.Width, .3 * sensorInfoView.Bounds.Height));
			unitLabel.Font = UIFont.BoldSystemFontOfSize(textSize);
			unitLabel.AdjustsFontSizeToFitWidth = true;
      unitLabel.TextAlignment = UITextAlignment.Right;
			//unitLabel.Text = "°F";  
      //unitLabel.Layer.BorderWidth = 1f;  

			sensorInfoView.AddSubview(measurementLabel);
      sensorInfoView.BringSubviewToFront(measurementLabel);
			sensorInfoView.AddSubview(linkLabel1);
			sensorInfoView.AddSubview(linkLabel2);
			sensorInfoView.AddSubview(unitLabel);

			sensorStatusView = new UIView(new CGRect(0, .76 * ContentView.Bounds.Height, ContentView.Bounds.Width, .24 * ContentView.Bounds.Height));
      sensorStatusView.BackgroundColor = UIColor.Black;
      sensorStatusView.Layer.CornerRadius = 5;
      sensorStatusView.ClipsToBounds = true;

			connectionImage = new UIImageView(new CGRect(.05 * sensorStatusView.Bounds.Width, .25 * sensorStatusView.Bounds.Height, .1 * sensorStatusView.Bounds.Width, .5 * sensorStatusView.Bounds.Height));
      connectionImage.Layer.CornerRadius = 8;
      connectionImage.BackgroundColor = UIColor.Green;

			extraImage = new UIImageView(new CGRect(.25 * sensorStatusView.Bounds.Width, .1 * sensorStatusView.Bounds.Height, .25 * sensorStatusView.Bounds.Width, sensorStatusView.Bounds.Height));
      extraImage.BackgroundColor = UIColor.Black;

			workbenchImage = new UIImageView(new CGRect(.5 * sensorStatusView.Bounds.Width, 0, .25 * sensorStatusView.Bounds.Width, sensorStatusView.Bounds.Height));
      workbenchImage.Image = UIImage.FromBundle("ic_nav_workbench");
      workbenchImage.TintColor = new UIColor(Colors.WHITE);

			analyzerImage = new UIImageView(new CGRect(.75 * sensorStatusView.Bounds.Width,0,.25 * sensorStatusView.Bounds.Width, sensorStatusView.Bounds.Height));
			analyzerImage.Image = UIImage.FromBundle("ic_nav_analyzer");
			analyzerImage.TintColor = new UIColor(Colors.WHITE);

			sensorStatusView.AddSubview(connectionImage);
			sensorStatusView.AddSubview(extraImage);
			sensorStatusView.AddSubview(workbenchImage);
			sensorStatusView.AddSubview(analyzerImage);

			ContentView.AddSubview(typeLabel);
			ContentView.AddSubview(sensorInfoView);
			ContentView.AddSubview(sensorStatusView);
      ContentView.BringSubviewToFront(sensorInfoView);
    }

    public void UpdateCell(GaugeDeviceSensor sensor){
      slotSensor = sensor;
			if(slotSensor == null){
				ContentView.Hidden = true;
				BackgroundView.Hidden = true;
      } else {
				slotSensor.onSensorStateChangedEvent += gaugeUpdating;

				typeLabel.Text += " " + slotSensor.device.serialNumber.rawSerial;
				measurementLabel.Text = slotSensor.measurement.amount.ToString();
				unitLabel.Text = slotSensor.measurement.unit.ToString();

				if(slotSensor.type == Core.Sensors.ESensorType.Pressure){
          pressureLinkImage = new UIImageView(new CGRect(ContentView.Bounds.Width, .3 * ContentView.Bounds.Height,.23 * ContentView.Bounds.Width, .12 * ContentView.Bounds.Height));
          pressureLinkImage.Image = UIImage.FromBundle("gold_arrow_right");
          pressureLinkImage.BackgroundColor = UIColor.Clear;
					ContentView.AddSubview(pressureLinkImage);
				} else if (slotSensor.type == Core.Sensors.ESensorType.Temperature && slotSensor.index != 0) {
					tempLinkImage = new UIImageView(new CGRect(-.23 * ContentView.Bounds.Width, .5 * ContentView.Bounds.Height, .23 * ContentView.Bounds.Width, .12 * ContentView.Bounds.Height));
					tempLinkImage.Image = UIImage.FromBundle("gold_arrow_left");
					tempLinkImage.BackgroundColor = UIColor.Clear;
					ContentView.AddSubview(tempLinkImage);
        }
				ContentView.Hidden = false;
				BackgroundView.Hidden = false;
			}
    }

		public async void gaugeUpdating(Sensor sensor) {
      measurementLabel.Text = sensor.measurement.amount.ToString();
      unitLabel.Text = sensor.measurement.unit.ToString();
		}
  }
}
