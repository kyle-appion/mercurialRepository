using System.Collections.Generic;
using UIKit;
using CoreGraphics;
using Appion.Commons.Measure;
using ION.Core.Devices;
using ION.Core.Sensors;

namespace ION.IOS.ViewController.Analyzer
{
  public class sensor
	{
		public UIView snapArea;
    public UIView availableView;
    public ActionView sactionView;
    public lowHighSensor lowArea;
    public lowHighSensor highArea;

    public UIImageView addIcon;
    public UIImageView connectionImage = new UIImageView();
    public UIImageView deviceImage = new UIImageView ();

		public UILabel topLabel = new UILabel();
    public UILabel tLabelBottom = new UILabel();
		public UILabel middleLabel = new UILabel();
		public UILabel bottomLabel = new UILabel();
		public UITapGestureRecognizer shortPressGesture;
		public UIPanGestureRecognizer panGesture;
		public UILongPressGestureRecognizer holdGesture;

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
    
    public sensor (UIView mainView, AnalyzerViewController ViewController, List<sensor> viewList, List<int> areaList)
		{
      lowArea = new lowHighSensor(new CGRect(.028 * mainView.Bounds.Width, .45 * mainView.Bounds.Height, .465 * mainView.Bounds.Width, .202 * mainView.Bounds.Height), new CGRect(.028 * mainView.Bounds.Width,.652 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.27 * mainView.Bounds.Height), ViewController, viewList, areaList);
      highArea = new lowHighSensor (new CGRect (.507 * mainView.Bounds.Width, .45 * mainView.Bounds.Height, .465 * mainView.Bounds.Width, .202 * mainView.Bounds.Height), new CGRect(.507 * mainView.Bounds.Width,.652 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.27 * mainView.Bounds.Height), ViewController, viewList, areaList);
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

