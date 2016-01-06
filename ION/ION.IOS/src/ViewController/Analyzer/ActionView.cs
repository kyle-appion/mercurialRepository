using System;
using UIKit;
using CoreGraphics;
using ION.Core.Devices;
using ION.Core.Sensors;

namespace ION.IOS.ViewController.Analyzer {

  public class ActionView {

    public ActionView() {
      aView.AddSubview(pcloseButton);
      aView.AddSubview(pactionButton);
      aView.AddSubview(pconnection);
      aView.AddSubview(connectionColor);
      aView.AddSubview(pdeviceImage);
      aView.AddSubview(pbatteryImage);
      aView.AddSubview(pdeviceName);
      aView.AddSubview(pgaugeValue);
      aView.AddSubview(pvalueType);
      aView.AddSubview(pconnectionStatus);
      aView.AddSubview(pLowHigh);
      aView.AddSubview(pdisplayLink);
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

    public UIView aView = new UIView(new CGRect(20,100,280,195));
    public UIView connectionColor = new UIView(new CGRect(243, 7, 29, 35));
    public UIButton pcloseButton = new UIButton(new CGRect(0, 156, 141, 39));
    public UIButton pactionButton = new UIButton(new CGRect(140,156,140, 39));
    public UIImageView pconnection = new UIImageView(new CGRect(243, 7, 29, 35));
    public UIImageView pdeviceImage = new UIImageView(new CGRect(0,42,60,60));
    public UIImageView pbatteryImage = new UIImageView(new CGRect(172,14,63,21));
    public UILabel pdeviceName = new UILabel(new CGRect(8, 7, 122, 21));
    public UILabel pgaugeValue = new UILabel(new CGRect(82,36,190,68));
    public UILabel pvalueType = new UILabel(new CGRect(191,112,81,21));
    public UILabel pconnectionStatus = new UILabel(new CGRect(82,112,100,21));
    public UILabel pLowHigh = new UILabel(new CGRect(204,133,75,21));
    public UILabel pdisplayLink = new UILabel(new CGRect(0,133,196,21));

    public void gaugeUpdating(Sensor sensor){
      pgaugeValue.Text = sensor.measurement.amount.ToString();
      pvalueType.Text = sensor.measurement.unit.ToString();
    }
  }

}

