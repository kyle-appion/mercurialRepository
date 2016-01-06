using System;
using System.Collections.Generic;
using UIKit;
using CoreGraphics;
using AllianceCustomPicker;

namespace ION.IOS.ViewController.Analyzer
{


	public class LowHighArea
	{

    public LowHighArea (UIView mainView)
		{
      lowArea = new lowHighSensor(new CGRect(.028 * mainView.Bounds.Width, .572 * mainView.Bounds.Height, .465 * mainView.Bounds.Width, .202 * mainView.Bounds.Height), new CGRect(.028 * mainView.Bounds.Width,.771 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.214 * mainView.Bounds.Height));//, new CGRect(0,0,.859 * mainView.Bounds.Width,.044 * mainView.Bounds.Height), new CGRect(0,.0017 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.07 * mainView.Bounds.Height), new CGRect(0,.114 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.044 * mainView.Bounds.Height),new CGRect(0,.158 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.044 * mainView.Bounds.Height), new CGRect(.028 * mainView.Bounds.Width,.771 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.214 * mainView.Bounds.Height));
      highArea = new lowHighSensor (new CGRect (.515 * mainView.Bounds.Width, .572 * mainView.Bounds.Height, .465 * mainView.Bounds.Width, .202 * mainView.Bounds.Height), new CGRect(.515 * mainView.Bounds.Width,.771 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.214 * mainView.Bounds.Height));//, new CGRect(0,0,.465 * mainView.Bounds.Width,.044 * mainView.Bounds.Height), new CGRect(0,.0017 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.07 * mainView.Bounds.Height), new CGRect(0,.114 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.044 * mainView.Bounds.Height), new CGRect(0,.158 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.044 * mainView.Bounds.Height), new CGRect(.515 * mainView.Bounds.Width,.771 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.214 * mainView.Bounds.Height));
		}

    //public lowHighSensor lowArea = new lowHighSensor (new CGRect(9, 325, 149,115), new CGRect(0,0,128,25), new CGRect(0,25,149,40), new CGRect(0,65,149,25),new CGRect(0,90,149,25), new CGRect(9,438,149,122));
    public lowHighSensor lowArea;
    //public lowHighSensor highArea = new lowHighSensor (new CGRect (165, 325, 149, 115), new CGRect(0,0,149,25), new CGRect(0,25,149,40), new CGRect(0,65,149,25), new CGRect(0,90,149,25), new CGRect(165,438,149,122));
    public lowHighSensor highArea;

		public List<string> availableSubviews = new List<string> {
      "Hold Reading (HOLD)","Maximum Reading (MAX)", "Minimum Reading (MIN)", "Alternate Unit(ALT)","Rate of Change (RoC)", "Superheat / Subcool (S/H or S/C)", "Pressure / Temperature (P/T)"
		};
	}
}

