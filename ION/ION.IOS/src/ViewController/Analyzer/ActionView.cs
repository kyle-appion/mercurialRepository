using System;
using UIKit;
using CoreGraphics;
using Appion.Commons.Measure;
using ION.Core.Devices;
using ION.Core.Sensors;
using System.Threading.Tasks;
using ION.IOS.Devices;
using ION.Core.App;

namespace ION.IOS.ViewController.Analyzer {

  public class ActionView {
    public UIView aView;
    public UIView connectionColor;
    public UIButton pcloseButton;
    public UIButton pactionButton;
    public UIImageView pconnection;
    public UIButton conDisButton;
    public UIImageView pdeviceImage;
    public UIImageView pbatteryImage;
    public UILabel pdeviceName;
    public UILabel pgaugeValue;
    public UILabel pvalueType;
    public UILabel pconnectionStatus;
    public UIButton pLowHigh;
    public UILabel pdisplayLink;
    UIView abuttonBorder;
    UIView abuttonBorder2;
    UIView abuttonBorder3;
    public UIActivityIndicatorView activityConnectStatus;
    
    public ActionView(UIView mainView) {
      aView = new UIView(new CGRect(.062 * mainView.Bounds.Width, .176 * mainView.Bounds.Height, .875 * mainView.Bounds.Width, .343 * mainView.Bounds.Height));
      aView.ClipsToBounds = true;
			aView.BackgroundColor = UIColor.White;
			aView.Hidden = true;
			aView.Layer.BorderWidth = 2f;
			aView.Layer.BorderColor = UIColor.Black.CGColor;

      connectionColor = new UIView(new CGRect(.867 * aView.Bounds.Width, .035 * aView.Bounds.Height, .103 * aView.Bounds.Width, .179 * aView.Bounds.Height));      
      connectionColor.Layer.BorderColor = UIColor.Black.CGColor;
      connectionColor.Layer.BorderWidth = 1f;
      connectionColor.Layer.CornerRadius = 8;
			connectionColor.BackgroundColor = UIColor.Clear;

      pcloseButton = new UIButton(new CGRect(0, .8 * aView.Bounds.Height, .503 * aView.Bounds.Width, .2 * aView.Bounds.Height));
			pcloseButton.SetTitle(Util.Strings.CLOSE, UIControlState.Normal);
			pcloseButton.SetTitleColor(UIColor.Black, UIControlState.Normal);

      pactionButton = new UIButton(new CGRect(.5 * aView.Bounds.Width,.8 * aView.Bounds.Height,.5 * aView.Bounds.Width, .2 * aView.Bounds.Height));
			pactionButton.SetTitle(Util.Strings.ACTIONS, UIControlState.Normal);
			pactionButton.SetTitleColor(UIColor.Black, UIControlState.Normal);

      pconnection = new UIImageView(new CGRect(.867 * aView.Bounds.Width, .035 * aView.Bounds.Height, .103 * aView.Bounds.Width, .179 * aView.Bounds.Height));
			pconnection.Layer.BorderColor = UIColor.Black.CGColor;
			pconnection.Layer.BorderWidth = 1f;
			pconnection.Layer.CornerRadius = 5;
			pconnection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");

			activityConnectStatus = new UIActivityIndicatorView(new CGRect(.867 * aView.Bounds.Width, .035 * aView.Bounds.Height, .103 * aView.Bounds.Width, .179 * aView.Bounds.Height));
      activityConnectStatus.HidesWhenStopped = true;
      
      conDisButton = new UIButton(new CGRect(.867 * aView.Bounds.Width, .035 * aView.Bounds.Height, .103 * aView.Bounds.Width, .179 * aView.Bounds.Height));
      conDisButton.BackgroundColor = UIColor.Clear;
      conDisButton.Layer.CornerRadius = 8;
      
      pdeviceImage = new UIImageView(new CGRect(0, .215 * aView.Bounds.Height, .214 * aView.Bounds.Width, .214 * aView.Bounds.Width));
      pbatteryImage = new UIImageView(new CGRect(.614 * aView.Bounds.Width, .071 * aView.Bounds.Height, .203 * aView.Bounds.Width, .107 * aView.Bounds.Height));
      pdeviceName = new UILabel(new CGRect(.028 * aView.Bounds.Width, .035 * aView.Bounds.Height, .435 * aView.Bounds.Width, .107 *aView.Bounds.Height));
      pgaugeValue = new UILabel(new CGRect(.292 * aView.Bounds.Width, .184 * aView.Bounds.Height, .678 * aView.Bounds.Width, .348 * aView.Bounds.Height));
			pgaugeValue.AdjustsFontSizeToFitWidth = true;
			pgaugeValue.Font = UIFont.FromName("DroidSans-Bold", 54f);
			pgaugeValue.TextAlignment = UITextAlignment.Right;

      pvalueType = new UILabel(new CGRect(.682 * aView.Bounds.Width, .574 * aView.Bounds.Height, .289 * aView.Bounds.Width, .107 * aView.Bounds.Height));
			pvalueType.TextAlignment = UITextAlignment.Right;

			pconnectionStatus = new UILabel(new CGRect(.292 * aView.Bounds.Width, .574 * aView.Bounds.Height, .357 * aView.Bounds.Width, .107 * aView.Bounds.Height));
			pconnectionStatus.AdjustsFontSizeToFitWidth = true;

			pLowHigh = new UIButton(new CGRect(.701 * aView.Bounds.Width, .682 * aView.Bounds.Height, .295 * aView.Bounds.Width,.107 * aView.Bounds.Height));
			pLowHigh.Layer.CornerRadius = 5f;
			pLowHigh.Layer.BorderWidth = 1f;
			pLowHigh.ClipsToBounds = true;

      pdisplayLink = new UILabel(new CGRect(0, .682 * aView.Bounds.Height, .7 * aView.Bounds.Width,.107 * aView.Bounds.Height));
			pdisplayLink.Text = Util.Strings.Analyzer.DISPLAYLINK;
			pdisplayLink.TextAlignment = UITextAlignment.Right;
			pdisplayLink.Font = UIFont.FromName("Helvetica", 12f);

      abuttonBorder = new UIView(new CGRect(0, .8 * aView.Bounds.Height, aView.Bounds.Width ,1));
      abuttonBorder.BackgroundColor = UIColor.LightGray;
      abuttonBorder2 = new UIView(new CGRect(.5 * aView.Bounds.Width, .8 * aView.Bounds.Height, 1, .2 * aView.Bounds.Height));
      abuttonBorder2.BackgroundColor = UIColor.LightGray;
      abuttonBorder3 = new UIView(new CGRect(.017 * aView.Bounds.Width, .215 * aView.Bounds.Height, .964 * aView.Bounds.Width, 1));
      abuttonBorder3.BackgroundColor = UIColor.Black;

			aView.BringSubviewToFront(pconnection);

      aView.AddSubview(pcloseButton);
      aView.AddSubview(pactionButton);
      aView.AddSubview(conDisButton);
      aView.AddSubview(connectionColor);
			aView.AddSubview(pconnection);
			aView.AddSubview(pdeviceImage);
      aView.AddSubview(pbatteryImage);
      aView.AddSubview(pdeviceName);
      aView.AddSubview(pgaugeValue);
      aView.AddSubview(pvalueType);
      aView.AddSubview(pconnectionStatus);
      aView.AddSubview(pLowHigh);
      aView.AddSubview(pdisplayLink);
      aView.AddSubview(abuttonBorder);
      aView.AddSubview(abuttonBorder2);
      aView.AddSubview(abuttonBorder3);
      aView.AddSubview(activityConnectStatus);
      aView.BringSubviewToFront(activityConnectStatus);
      aView.BringSubviewToFront(conDisButton);


      conDisButton.TouchUpInside += delegate {
        if(currentSensor != null){
          if (activityConnectStatus != null)
            activityConnectStatus = null;

          activityConnectStatus = new UIActivityIndicatorView(new CGRect(.867 * aView.Bounds.Width, .035 * aView.Bounds.Height, .103 * aView.Bounds.Width, .179 * aView.Bounds.Height));
          aView.AddSubview(activityConnectStatus);

          if(currentSensor.device.isConnected){
            connectionSpinner(1);
          } else {
            connectionSpinner(2);
          }
        }
      };

      pcloseButton.TouchDown += delegate {
        pcloseButton.BackgroundColor = UIColor.Blue;
      };

      pcloseButton.TouchUpInside += delegate {
        pcloseButton.BackgroundColor = UIColor.Clear;
		    aView.Hidden = true;
	    };
      pcloseButton.TouchUpOutside += delegate {
        pcloseButton.BackgroundColor = UIColor.Clear;
      };

      pactionButton.TouchDown += delegate {
        pactionButton.BackgroundColor = UIColor.Blue;
      };

      pactionButton.TouchUpInside += delegate {
        pactionButton.BackgroundColor = UIColor.Clear;
      };

      pactionButton.TouchUpOutside += delegate {
        pactionButton.BackgroundColor = UIColor.Clear;
      };
    }

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

		public ManualSensor manualSensor {
			get { return __manualSensor; }
			set {
				__manualSensor = value;
			}
		}
		ManualSensor __manualSensor;

    public async void gaugeUpdating(Sensor sensor){
      pconnection.Hidden = false;  
      if (currentSensor.device.isConnected) {
        pconnection.Image = UIImage.FromBundle("ic_bluetooth_connected");
        pconnection.BackgroundColor = UIColor.Green;
        pconnectionStatus.Hidden = true;
				pgaugeValue.Font = UIFont.FromName("DroidSans-Bold", 54f);
				pgaugeValue.TextColor = UIColor.Black;
			} else {
        pconnection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
        pconnection.BackgroundColor = UIColor.Red;
        pconnectionStatus.Hidden = false;
				pgaugeValue.Font = UIFont.FromName("DroidSans", 54f);
        pgaugeValue.TextColor = UIColor.Gray;
			}
      if (sensor.unit != Units.Vacuum.MICRON) {
        pgaugeValue.Text = sensor.measurement.amount.ToString("N");
      } else {
        pgaugeValue.Text = sensor.measurement.amount.ToString();
      }
      pvalueType.Text = sensor.measurement.unit.ToString();
    }

    public async void connectionSpinner(int conn){ 
      pconnection.BackgroundColor = UIColor.Clear;

      activityConnectStatus.StartAnimating();

      if (conn == 1) {
        currentSensor.device.connection.Disconnect();
      } else if (conn == 2) {
        currentSensor.device.connection.Connect();
      }

      await Task.Delay(TimeSpan.FromSeconds(2));
      activityConnectStatus.StopAnimating();

      if (currentSensor.device.isConnected) {
        pconnection.Image = UIImage.FromBundle("ic_bluetooth_connected");
        pconnection.BackgroundColor = UIColor.Green;
        pconnectionStatus.Hidden = true;
      } else {
        pconnection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
        pconnection.BackgroundColor = UIColor.Red;
        pconnectionStatus.Hidden = false;
      }
    }

    public void setUI(ManualSensor lowManual, ManualSensor highManual){
			pLowHigh.SetTitle(Util.Strings.Analyzer.UNSPECIFIED, UIControlState.Normal);
			pLowHigh.BackgroundColor = UIColor.White;
			pLowHigh.SetTitleColor(UIColor.Black, UIControlState.Normal);

      if(currentSensor == null){
				pdeviceName.Text = currentSensor.name;
				pdeviceImage.Image = UIImage.FromBundle("ic_edit");
				pconnectionStatus.Text = "";
				pconnection.Image = null;
				pconnection.Hidden = true;
				pbatteryImage.Image = null;
				connectionColor.Hidden = true;
				conDisButton.Hidden = true;
				
				if (lowManual != null && manualSensor != null && lowManual == manualSensor) {
					pLowHigh.SetTitle(Util.Strings.Analyzer.LOWSIDE, UIControlState.Normal);
					pLowHigh.BackgroundColor = UIColor.Blue;
					pLowHigh.Layer.CornerRadius = 6f;
					pLowHigh.SetTitleColor(UIColor.White, UIControlState.Normal);
				} else if (highManual != null && manualSensor != null && highManual == manualSensor) {
					pLowHigh.SetTitle(Util.Strings.Analyzer.HIGHSIDE, UIControlState.Normal);
					pLowHigh.BackgroundColor = UIColor.Red;
					pLowHigh.Layer.CornerRadius = 6f;
					pLowHigh.SetTitleColor(UIColor.White, UIControlState.Normal);
				}

				if (manualSensor.unit != Units.Vacuum.MICRON) {
					pgaugeValue.Text = manualSensor.measurement.amount.ToString("N");
				} else {
					pgaugeValue.Text = manualSensor.measurement.amount.ToString();
				}
				pvalueType.Text = manualSensor.unit.ToString();

			} else {
        pdeviceName.Text = currentSensor.name;
        if(currentSensor.device.isConnected){
					pconnection.Image = UIImage.FromBundle("ic_bluetooth_connected");
					connectionColor.BackgroundColor = UIColor.Green;
					pconnectionStatus.Text = "";
					if (currentSensor.device.battery > 75) {
						pbatteryImage.Image = UIImage.FromBundle("img_battery_100");
					} else if (currentSensor.device.battery > 50) {
						pbatteryImage.Image = UIImage.FromBundle("img_battery_75");
					} else if (currentSensor.device.battery > 25) {
						pbatteryImage.Image = UIImage.FromBundle("img_battery_50");
					} else if (currentSensor.device.battery > 0) {
						pbatteryImage.Image = UIImage.FromBundle("img_battery_25");
					} else {
						pbatteryImage.Image = UIImage.FromBundle("img_battery_0");
					}
        } else {
					pconnection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
					pconnectionStatus.Text = Util.Strings.Device.DISCONNECTED;
					pconnectionStatus.TextColor = UIColor.Red;
					
					connectionColor.BackgroundColor = UIColor.Red;
        }

				if (currentSensor.unit != Units.Vacuum.MICRON) {
					pgaugeValue.Text = currentSensor.measurement.amount.ToString("N");
				} else {
					pgaugeValue.Text = currentSensor.measurement.amount.ToString();
				}
				pvalueType.Text = currentSensor.unit.ToString();

				pconnection.Hidden = false;
				connectionColor.Hidden = false;
				conDisButton.Hidden = false;
				pdeviceImage.Image = DeviceUtil.GetUIImageFromDeviceModel(currentSensor.device.serialNumber.deviceModel);
        if (AppState.context.currentAnalyzer.lowSideManifold != null && AppState.context.currentAnalyzer.lowSideManifold.primarySensor != null && AppState.context.currentAnalyzer.lowSideManifold.primarySensor == currentSensor){
					pLowHigh.SetTitle(Util.Strings.Analyzer.LOWSIDE, UIControlState.Normal);
					pLowHigh.SetTitleColor(UIColor.White, UIControlState.Normal);
					pLowHigh.BackgroundColor = UIColor.Blue;
        } else if (AppState.context.currentAnalyzer.highSideManifold != null && AppState.context.currentAnalyzer.highSideManifold.primarySensor != null && AppState.context.currentAnalyzer.highSideManifold.primarySensor == currentSensor){
					pLowHigh.SetTitle(Util.Strings.Analyzer.HIGHSIDE, UIControlState.Normal);
					pLowHigh.SetTitleColor(UIColor.White, UIControlState.Normal);
					pLowHigh.BackgroundColor = UIColor.Red;					
        }
        aView.Hidden = false;
      }
    }

    public void hideUI(){
      
    }
  }

}

