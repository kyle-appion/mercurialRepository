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
      //"Hold Reading (HOLD)","Maximum Reading (MAX)", "Minimum Reading (MIN)", "Alternate Unit(ALT)","Rate of Change (RoC)", "Superheat / Subcool (S/H or S/C)", "Pressure / Temperature (P/T)"
      Util.Strings.Workbench.Viewer.HOLD_DESC,Util.Strings.Workbench.Viewer.MAX_DESC, Util.Strings.Workbench.Viewer.MIN_DESC, Util.Strings.Workbench.Viewer.ALT_DESC,Util.Strings.Workbench.Viewer.ROC_DESC, Util.Strings.Analyzer.SHSCDESC, Util.Strings.Analyzer.PTDESC
		};
		
    public LowHighArea (UIView mainView, AnalyzerViewController ViewController, sensorGroup analyzerSensors)
		{
      lowArea = new lowHighSensor(new CGRect(.028 * mainView.Bounds.Width, .45 * mainView.Bounds.Height, .465 * mainView.Bounds.Width, .202 * mainView.Bounds.Height), new CGRect(.028 * mainView.Bounds.Width,.652 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.27 * mainView.Bounds.Height), ViewController, analyzerSensors.viewList);
      lowArea.location = "low";
			highArea = new lowHighSensor (new CGRect (.507 * mainView.Bounds.Width, .45 * mainView.Bounds.Height, .465 * mainView.Bounds.Width, .202 * mainView.Bounds.Height), new CGRect(.507 * mainView.Bounds.Width,.652 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.27 * mainView.Bounds.Height), ViewController, analyzerSensors.viewList);
			highArea.location = "high";
		}
	}
}

