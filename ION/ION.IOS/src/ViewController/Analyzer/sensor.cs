﻿using System;
using System.Collections.Generic;
using UIKit;
using CoreGraphics;
using ION.Core.Devices;
using ION.Core.Sensors;
using ION.IOS.ViewController.SuperheatSubcool;
using ION.IOS.ViewController.PressureTemperatureChart;

namespace ION.IOS.ViewController.Analyzer
{
	//public delegate void sensorEvent (UILabel topLabel, UILabel middleLabel, UILabel bottomLabel, string identifier);
	
  public class sensor
	{
    //public event sensorEvent sensorChanged;
    public sensor (UIView mainView, AnalyzerViewController ViewController)
		{
      lowArea = new lowHighSensor(new CGRect(.028 * mainView.Bounds.Width, .572 * mainView.Bounds.Height, .465 * mainView.Bounds.Width, .202 * mainView.Bounds.Height), new CGRect(.028 * mainView.Bounds.Width,.771 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.214 * mainView.Bounds.Height), ViewController);
      highArea = new lowHighSensor (new CGRect (.515 * mainView.Bounds.Width, .572 * mainView.Bounds.Height, .465 * mainView.Bounds.Width, .202 * mainView.Bounds.Height), new CGRect(.515 * mainView.Bounds.Width,.771 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.214 * mainView.Bounds.Height), ViewController);
    }
		public UIView snapArea;
    public UIView availableView;
    public ActionView sactionView;
    public lowHighSensor lowArea;
    public lowHighSensor highArea;


    public UIImageView addIcon;// = new UIImageView(new CGRect(7,7,50,50));
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

    public void gaugeUpdating(Sensor sensor){
      middleLabel.Text = sensor.measurement.amount.ToString("N") + " ";
      bottomLabel.Text = sensor.measurement.unit.ToString();
    }
  }
	
}

