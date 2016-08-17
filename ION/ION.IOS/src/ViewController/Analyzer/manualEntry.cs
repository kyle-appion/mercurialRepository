using System;
using System.Collections.Generic;
using ION.Core.Devices;
using ION.Core.Sensors;
using UIKit;

namespace ION.IOS.ViewController.Analyzer
{
	public class manualEntry
	{
		public manualEntry ()
		{
		}
    public sensor pressedSensor;
    public lowHighSensor lhSensor;
		public UIView pressedView;
    public UIView availableView;
		public UILongPressGestureRecognizer addLong;
		public UIPanGestureRecognizer addPan;
		public UILabel topLabel;
		public UILabel middleLabel;
		public UILabel bottomLabel;
		public UILabel subviewLabel;
    public bool isManual;
    public UIImageView addIcon;

		public List<string> pressures = new List<string>{
			"psig","bar","inHg","cmHg","kg/cm2","Pa","kPa","mPa"
		};

		public List<string> deviceType = new List<string> {
			"Temperature", "Pressure", "Vaccum"
		};

		public List<string> temperatures = new List<string> {
			"ºF", "ºC", "ºK"
		};

		public List<string> vacuum = new List<string> {
			"micron", "inHg", "mTorr", "mbar", "psia", "kPa"
		};
	}
}

