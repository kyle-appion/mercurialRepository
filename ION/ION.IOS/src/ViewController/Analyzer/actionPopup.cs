using System;
using UIKit;

namespace ION.IOS.ViewController.Analyzer
{
	public class actionPopup
	{
		public actionPopup ()
		{
		}
    public sensor pressedSensor;
		public UIView pressedView;
		public UILongPressGestureRecognizer addLong;
		public UIPanGestureRecognizer addPan;
		public UILabel topLabel;
		public UILabel middleLabel;
		public UILabel bottomLabel;
	}
}

