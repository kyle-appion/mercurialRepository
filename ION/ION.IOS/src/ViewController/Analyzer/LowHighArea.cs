using System;
using System.Collections.Generic;
using UIKit;
using CoreGraphics;

namespace ION.IOS.ViewController.Analyzer
{


	public class LowHighArea
	{

    public lowHighSensor lowArea;
    public lowHighSensor highArea;

		public List<string> availableSubviews = new List<string> {
      "Hold Reading (HOLD)","Maximum Reading (MAX)", "Minimum Reading (MIN)", "Alternate Unit(ALT)","Rate of Change (RoC)", "Superheat / Subcool (S/H or S/C)", "Pressure / Temperature (P/T)"
		};
		
    public LowHighArea (UIView mainView, AnalyzerViewController ViewController)
		{
      lowArea = new lowHighSensor(new CGRect(.028 * mainView.Bounds.Width, .45 * mainView.Bounds.Height, .465 * mainView.Bounds.Width, .202 * mainView.Bounds.Height), new CGRect(.028 * mainView.Bounds.Width,.652 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.27 * mainView.Bounds.Height), ViewController);
      lowArea.location = "low";
			highArea = new lowHighSensor (new CGRect (.507 * mainView.Bounds.Width, .45 * mainView.Bounds.Height, .465 * mainView.Bounds.Width, .202 * mainView.Bounds.Height), new CGRect(.507 * mainView.Bounds.Width,.652 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.27 * mainView.Bounds.Height), ViewController);
			highArea.location = "high";
		}
	}
}

