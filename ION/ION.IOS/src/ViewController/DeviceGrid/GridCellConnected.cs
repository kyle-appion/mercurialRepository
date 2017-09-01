using System;
using Foundation;
using CoreGraphics;
using UIKit;
using ION.IOS.Util;
using ION.Core.Devices;
using ION.Core.Sensors;
using ION.Core.App;
using System.Threading.Tasks;
using System.Drawing;
using ION.IOS.Devices;

namespace ION.IOS.ViewController.DeviceGrid {
  public class GridCellConnected : UICollectionViewCell{
    public UILabel typeLabel,measurementLabel, linkLabel1, linkLabel2, unitLabel;
    public UIView sensorInfoView, sensorStatusView;
    public UIImageView connectionImage, extraImage, workbenchImage, analyzerImage, pressureLinkImage, tempLinkImage;
    public nfloat textSize;
    public static NSString CellID = new NSString("connectedCell");
    public GaugeDeviceSensor slotSensor;
    IION ion;
    public bool actualLinked = true;

    [Export("initWithFrame:")]
    public GridCellConnected(CGRect frame) : base(frame) {
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
				textSize = 15f;  
			} else {
				textSize = 25f;
			}
      ion = AppState.context;
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

			linkLabel1 = new UILabel(new CGRect(.05 * sensorInfoView.Bounds.Width, .65 * sensorInfoView.Bounds.Height, .7 * sensorInfoView.Bounds.Width, .3 * sensorInfoView.Bounds.Height));
      linkLabel1.BackgroundColor = UIColor.Red;
      linkLabel1.TextAlignment = UITextAlignment.Center;
      linkLabel1.Font = UIFont.BoldSystemFontOfSize(textSize);
      linkLabel1.TextColor = UIColor.White;
      linkLabel1.Layer.CornerRadius = 5;
      linkLabel1.ClipsToBounds = true;
      linkLabel1.Text = "Actual";
			linkLabel1.AdjustsFontSizeToFitWidth = true;

			unitLabel = new UILabel(new CGRect(.5 * sensorInfoView.Bounds.Width, .65 * sensorInfoView.Bounds.Height,.5 * sensorInfoView.Bounds.Width, .3 * sensorInfoView.Bounds.Height));
			unitLabel.Font = UIFont.BoldSystemFontOfSize(textSize);
			unitLabel.AdjustsFontSizeToFitWidth = true;
      unitLabel.TextAlignment = UITextAlignment.Right;

			sensorInfoView.AddSubview(measurementLabel);
      sensorInfoView.BringSubviewToFront(measurementLabel);
			sensorInfoView.AddSubview(linkLabel1);
			sensorInfoView.AddSubview(unitLabel);

			sensorStatusView = new UIView(new CGRect(0, .76 * ContentView.Bounds.Height, ContentView.Bounds.Width, .24 * ContentView.Bounds.Height));
      sensorStatusView.BackgroundColor = UIColor.Black;
      sensorStatusView.Layer.CornerRadius = 5;
      sensorStatusView.ClipsToBounds = true;

			connectionImage = new UIImageView(new CGRect(.05 * sensorStatusView.Bounds.Width, .25 * sensorStatusView.Bounds.Height, .1 * sensorStatusView.Bounds.Width, .45 * sensorStatusView.Bounds.Height));
      connectionImage.Layer.CornerRadius = 8f;
      connectionImage.Layer.MasksToBounds = true;

			extraImage = new UIImageView(new CGRect(.25 * sensorStatusView.Bounds.Width, .1 * sensorStatusView.Bounds.Height, .25 * sensorStatusView.Bounds.Width, sensorStatusView.Bounds.Height));
      extraImage.BackgroundColor = UIColor.Black;

			workbenchImage = new UIImageView(new CGRect(.51 * sensorStatusView.Bounds.Width, .05 * sensorStatusView.Bounds.Width, .21 * sensorStatusView.Bounds.Width, .8 * sensorStatusView.Bounds.Height));
			workbenchImage.Image = UIImage.FromBundle("ic_nav_workbench");
			workbenchImage.Image = workbenchImage.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
			workbenchImage.TintColor = UIColor.White;

			analyzerImage = new UIImageView(new CGRect(.76 * sensorStatusView.Bounds.Width, .05 * sensorStatusView.Bounds.Width, .21 * sensorStatusView.Bounds.Width, .8 * sensorStatusView.Bounds.Height));
			analyzerImage.Image = UIImage.FromBundle("ic_nav_analyzer");
			analyzerImage.Image = analyzerImage.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
			analyzerImage.TintColor = UIColor.White;

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
			slotSensor.onSensorStateChangedEvent -= gaugeUpdating;
			slotSensor = sensor;
			if(slotSensor == null){
				ContentView.Hidden = true;
				BackgroundView.Hidden = true;
      } else {
				slotSensor.onSensorStateChangedEvent += gaugeUpdating;

				typeLabel.Text = slotSensor.device.serialNumber.deviceModel.GetTypeString();
				measurementLabel.Text = slotSensor.measurement.amount.ToString();
				unitLabel.Text = slotSensor.measurement.unit.ToString();

				if(slotSensor.type == Core.Sensors.ESensorType.Pressure){
          pressureLinkImage = new UIImageView(new CGRect(ContentView.Bounds.Width, .3 * ContentView.Bounds.Height,.23 * ContentView.Bounds.Width, .12 * ContentView.Bounds.Height));
          pressureLinkImage.Image = UIImage.FromBundle("gold_arrow_right");
          pressureLinkImage.BackgroundColor = UIColor.Clear;
					ContentView.AddSubview(pressureLinkImage);
          linkLabel1.Hidden = true;
				} else if (slotSensor.type == Core.Sensors.ESensorType.Temperature && slotSensor.index != 0) {
					tempLinkImage = new UIImageView(new CGRect(-.23 * ContentView.Bounds.Width, .5 * ContentView.Bounds.Height, .23 * ContentView.Bounds.Width, .12 * ContentView.Bounds.Height));
					tempLinkImage.Image = UIImage.FromBundle("gold_arrow_left");
					tempLinkImage.BackgroundColor = UIColor.Clear;
					ContentView.AddSubview(tempLinkImage);
          linkLabel1.Hidden = false;
				} else if (slotSensor.type == Core.Sensors.ESensorType.Vacuum){
					linkLabel1.Hidden = true;
				}
				ContentView.Hidden = false;
				BackgroundView.Hidden = false;

				if (ion.currentWorkbench.ContainsSensor(sensor)) {
					workbenchImage.Hidden = false;
				} else {
					workbenchImage.Hidden = true;
				}

				if (ion.currentAnalyzer.sensorList.Contains(sensor)) {
					analyzerImage.Hidden = false;
				} else {
					analyzerImage.Hidden = true;
				}
			}
    }

		public async void gaugeUpdating(Sensor sensor) {
			await Task.Delay(TimeSpan.FromMilliseconds(1));
      var gaugeSensor = sensor as GaugeDeviceSensor;

      if (gaugeSensor.device.isConnected) {
        connectionImage.BackgroundColor = UIColor.Green;
        measurementLabel.Text = sensor.measurement.amount.ToString();
        unitLabel.Text = sensor.measurement.unit.ToString();
      } else if (gaugeSensor.device.connection.connectionState == Core.Connections.EConnectionState.Broadcasting) {
				connectionImage.BackgroundColor = UIColor.Blue;
				measurementLabel.Text = sensor.measurement.amount.ToString();
				unitLabel.Text = sensor.measurement.unit.ToString();
      } else {
        if (gaugeSensor.device.isNearby) {
					connectionImage.BackgroundColor = UIColor.Yellow;
					measurementLabel.Text = "---";
					unitLabel.Text = "";
        } else {
          connectionImage.BackgroundColor = UIColor.Red;
          measurementLabel.Text = "---";
          unitLabel.Text = "";
        }
			}
		}
  }
}
