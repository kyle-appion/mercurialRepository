using System;
using Foundation;
using CoreGraphics;
using UIKit;
using ION.IOS.Util;
using ION.Core.Devices;
using ION.Core.Sensors;

namespace ION.IOS.ViewController.DeviceGrid {
  public partial class GridCellDisconnected : UICollectionViewCell {
		public UILabel typeLabel, typeLabelMask, sensorStatusMask;
		public UIView  sensorStatusView;
		public UIImageView connectionImage, extraImage, workbenchImage, analyzerImage;
		public nfloat textSize;
		public static NSString CellID = new NSString("disconnectedCell");
		public GaugeDeviceSensor slotSensor;

		[Export("initWithFrame:")]
		public GridCellDisconnected(CGRect frame) : base(frame) {
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
				textSize = 15f;
			} else {
				textSize = 25f;
			}

			BackgroundView = new UIView { BackgroundColor = UIColor.Clear };

			ContentView.BackgroundColor = UIColor.Clear;

			typeLabel = new UILabel(new CGRect(0, 0, ContentView.Bounds.Width, .5 * ContentView.Bounds.Height));
			typeLabel.Font = UIFont.BoldSystemFontOfSize(textSize);
			typeLabel.TextColor = UIColor.White;
			typeLabel.TextAlignment = UITextAlignment.Center;
			typeLabel.Layer.CornerRadius = 5;
			typeLabel.ClipsToBounds = true;
			typeLabel.BackgroundColor = UIColor.Red;
      typeLabel.AdjustsFontSizeToFitWidth = true;

      typeLabelMask = new UILabel(new CGRect(0,.8 * typeLabel.Bounds.Height, typeLabel.Bounds.Width, .2 * typeLabel.Bounds.Height));
      typeLabelMask.BackgroundColor = UIColor.Red;
      typeLabelMask.ClipsToBounds = false;

			sensorStatusView = new UIView(new CGRect(0, .5 * ContentView.Bounds.Height, ContentView.Bounds.Width, .5 * ContentView.Bounds.Height));
			sensorStatusView.BackgroundColor = UIColor.Black;
			sensorStatusView.Layer.CornerRadius = 5;
			sensorStatusView.ClipsToBounds = true;

      sensorStatusMask = new UILabel(new CGRect(0,.5 * ContentView.Bounds.Height,sensorStatusView.Bounds.Width, .2 * sensorStatusView.Bounds.Height));
      sensorStatusMask.BackgroundColor = UIColor.Black;

			connectionImage = new UIImageView(new CGRect(.05 * sensorStatusView.Bounds.Width, .25 * sensorStatusView.Bounds.Height, .1 * sensorStatusView.Bounds.Width, .5 * sensorStatusView.Bounds.Height));
			connectionImage.Layer.CornerRadius = 8;
			connectionImage.BackgroundColor = UIColor.Green;

			extraImage = new UIImageView(new CGRect(.25 * sensorStatusView.Bounds.Width, .1 * sensorStatusView.Bounds.Height, .25 * sensorStatusView.Bounds.Width, sensorStatusView.Bounds.Height));
			extraImage.BackgroundColor = UIColor.Black;

			workbenchImage = new UIImageView(new CGRect(.5 * sensorStatusView.Bounds.Width, 0, .25 * sensorStatusView.Bounds.Width, sensorStatusView.Bounds.Height));
			workbenchImage.Image = UIImage.FromBundle("ic_nav_workbench");
			workbenchImage.TintColor = new UIColor(Colors.WHITE);

			analyzerImage = new UIImageView(new CGRect(.75 * sensorStatusView.Bounds.Width, 0, .25 * sensorStatusView.Bounds.Width, sensorStatusView.Bounds.Height));
			analyzerImage.Image = UIImage.FromBundle("ic_nav_analyzer");
			analyzerImage.TintColor = new UIColor(Colors.WHITE);


			sensorStatusView.AddSubview(connectionImage);
			sensorStatusView.AddSubview(extraImage);
			sensorStatusView.AddSubview(workbenchImage);
			sensorStatusView.AddSubview(analyzerImage);

			ContentView.AddSubview(typeLabel);
			ContentView.AddSubview(sensorStatusView);
			ContentView.AddSubview(typeLabelMask);
			ContentView.AddSubview(sensorStatusMask);
		}

		public void UpdateCell(GaugeDeviceSensor sensor) {
			slotSensor = sensor;
			if (slotSensor == null) {
				ContentView.Hidden = true;
				BackgroundView.Hidden = true;
			} else {
				typeLabel.Text += " " + slotSensor.device.serialNumber.rawSerial;

        if(slotSensor.device.isConnected){
					connectionImage.BackgroundColor = UIColor.Green;

				} else {
					connectionImage.BackgroundColor = UIColor.Red;

				}
				ContentView.Hidden = false;
				BackgroundView.Hidden = false;
			}
		}

	}
}
