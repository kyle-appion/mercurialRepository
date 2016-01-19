using System;
using UIKit;
using CoreGraphics;
using ION.Core.Devices;
using ION.Core.Sensors;

namespace ION.IOS.ViewController.Analyzer {

  public class ActionView {

    public ActionView(UIView mainView) {
      aView = new UIView(new CGRect(.062 * mainView.Bounds.Width, .176 * mainView.Bounds.Height, .875 * mainView.Bounds.Width, .343 * mainView.Bounds.Height));
      connectionColor = new UIView(new CGRect(.867 * aView.Bounds.Width, .035 * aView.Bounds.Height, .103 * aView.Bounds.Width, .179 * aView.Bounds.Height));
      connectionColor.Layer.BorderColor = UIColor.Black.CGColor;
      connectionColor.Layer.BorderWidth = 1f;
      connectionColor.Layer.CornerRadius = 8;
      pcloseButton = new UIButton(new CGRect(0, .8 * aView.Bounds.Height, .503 * aView.Bounds.Width, .2 * aView.Bounds.Height));
      pactionButton = new UIButton(new CGRect(.5 * aView.Bounds.Width,.8 * aView.Bounds.Height,.5 * aView.Bounds.Width, .2 * aView.Bounds.Height));
      pconnection = new UIImageView(new CGRect(.867 * aView.Bounds.Width, .035 * aView.Bounds.Height, .103 * aView.Bounds.Width, .179 * aView.Bounds.Height));
      conDisButton = new UIButton(new CGRect(.867 * aView.Bounds.Width, .035 * aView.Bounds.Height, .103 * aView.Bounds.Width, .179 * aView.Bounds.Height));
      conDisButton.BackgroundColor = UIColor.Clear;
      conDisButton.Layer.CornerRadius = 8;
      pdeviceImage = new UIImageView(new CGRect(0, .215 * aView.Bounds.Height, .196 * aView.Bounds.Width, .282 * aView.Bounds.Height));
      pbatteryImage = new UIImageView(new CGRect(.614 * aView.Bounds.Width, .071 * aView.Bounds.Height, .203 * aView.Bounds.Width, .107 * aView.Bounds.Height));
      pdeviceName = new UILabel(new CGRect(.028 * aView.Bounds.Width, .035 * aView.Bounds.Height, .435 * aView.Bounds.Width, .107 *aView.Bounds.Height));
      pgaugeValue = new UILabel(new CGRect(.292 * aView.Bounds.Width, .184 * aView.Bounds.Height, .678 * aView.Bounds.Width, .348 * aView.Bounds.Height));
      pvalueType = new UILabel(new CGRect(.682 * aView.Bounds.Width, .574 * aView.Bounds.Height, .289 * aView.Bounds.Width, .107 * aView.Bounds.Height));
      pconnectionStatus = new UILabel(new CGRect(.292 * aView.Bounds.Width, .574 * aView.Bounds.Height, .357 * aView.Bounds.Width, .107 * aView.Bounds.Height));
      pLowHigh = new UILabel(new CGRect(.728 * aView.Bounds.Width, .682 * aView.Bounds.Height, .267 * aView.Bounds.Width,.107 * aView.Bounds.Height));
      pdisplayLink = new UILabel(new CGRect(0, .682 * aView.Bounds.Height, .7 * aView.Bounds.Width,.107 * aView.Bounds.Height));
      abuttonBorder = new UIView(new CGRect(0, .8 * aView.Bounds.Height, aView.Bounds.Width ,1));
      abuttonBorder.BackgroundColor = UIColor.LightGray;
      abuttonBorder2 = new UIView(new CGRect(.5 * aView.Bounds.Width, .8 * aView.Bounds.Height, 1, .2 * aView.Bounds.Height));
      abuttonBorder2.BackgroundColor = UIColor.LightGray;
      abuttonBorder3 = new UIView(new CGRect(.017 * aView.Bounds.Width, .215 * aView.Bounds.Height, .964 * aView.Bounds.Width, 1));
      abuttonBorder3.BackgroundColor = UIColor.Black;

      aView.AddSubview(pcloseButton);
      aView.AddSubview(pactionButton);
      aView.AddSubview(pconnection);
      aView.AddSubview(conDisButton);
      aView.AddSubview(connectionColor);
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
      aView.BringSubviewToFront(conDisButton);

      conDisButton.TouchUpInside += delegate {
        if(currentSensor != null){
          if(currentSensor.device.isConnected){
            currentSensor.device.connection.Disconnect();
          } else {
            currentSensor.device.connection.Connect();
          }
        }
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

    //public UIView aView = new UIView(new CGRect(20,100,280,195));
    public UIView aView;
    //public UIView connectionColor = new UIView(new CGRect(243, 7, 29, 35));
    public UIView connectionColor;
    //public UIButton pcloseButton = new UIButton(new CGRect(0, 156, 141, 39));
    public UIButton pcloseButton;
    //public UIButton pactionButton = new UIButton(new CGRect(140,156,140, 39));
    public UIButton pactionButton;
    //public UIImageView pconnection = new UIImageView(new CGRect(243, 7, 29, 35));
    public UIImageView pconnection;
    //public UIImageView pconnection = new UIImageView(new CGRect(243, 7, 29, 35));
    public UIButton conDisButton;
    //public UIImageView pdeviceImage = new UIImageView(new CGRect(0,60,60,60));
    public UIImageView pdeviceImage;
    //public UIImageView pbatteryImage = new UIImageView(new CGRect(172,14,63,21));
    public UIImageView pbatteryImage;
    //public UILabel pdeviceName = new UILabel(new CGRect(8, 7, 122, 21));
    public UILabel pdeviceName;
    //public UILabel pgaugeValue = new UILabel(new CGRect(82,36,190,68));
    public UILabel pgaugeValue;
    //public UILabel pvalueType = new UILabel(new CGRect(191,112,81,21));
    public UILabel pvalueType;
    //public UILabel pconnectionStatus = new UILabel(new CGRect(82,112,100,21));
    public UILabel pconnectionStatus;
    //public UILabel pLowHigh = new UILabel(new CGRect(204,133,75,21));
    public UILabel pLowHigh;
    //public UILabel pdisplayLink = new UILabel(new CGRect(0,133,196,21));
    public UILabel pdisplayLink;
    //UIView abuttonBorder = new UIView(new CGRect(0, 156, 281 ,1));
    UIView abuttonBorder;
    //UIView abuttonBorder2 = new UIView(new CGRect(141,156,1,39));
    UIView abuttonBorder2;
    //UIView abuttonBorder3 = new UIView(new CGRect(5,42, 270, 1));
    UIView abuttonBorder3;

    public void gaugeUpdating(Sensor sensor){
      if (currentSensor.device.isConnected) {
        pconnection.Image = UIImage.FromBundle("ic_bluetooth_connected");
        pconnection.BackgroundColor = UIColor.Green;
        pconnectionStatus.Hidden = true;
      } else {
        pconnection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
        pconnection.BackgroundColor = UIColor.Red;
        pconnectionStatus.Hidden = false;
      }

      pgaugeValue.Text = sensor.measurement.amount.ToString();
      pvalueType.Text = sensor.measurement.unit.ToString();
    }
  }

}

