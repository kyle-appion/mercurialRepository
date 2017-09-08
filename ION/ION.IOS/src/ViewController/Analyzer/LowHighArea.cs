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
      Util.Strings.Workbench.Viewer.HOLD_DESC,Util.Strings.Workbench.Viewer.MAX_DESC, Util.Strings.Workbench.Viewer.MIN_DESC, Util.Strings.Workbench.Viewer.ALT_DESC,Util.Strings.Workbench.Viewer.ROC_DESC, Util.Strings.Analyzer.SHSCDESC, Util.Strings.Analyzer.PTDESC
		};
		
    public LowHighArea (UIView mainView, AnalyzerViewController ViewController, sensorGroup analyzerSensors)
		{
      lowArea = new lowHighSensor(new CGRect(.028 * mainView.Bounds.Width, .45 * mainView.Bounds.Height, .465 * mainView.Bounds.Width, .202 * mainView.Bounds.Height), new CGRect(.028 * mainView.Bounds.Width,.652 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.25 * mainView.Bounds.Height), ViewController, analyzerSensors.viewList);
      lowArea.location = "low";
      lowArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
      lowArea.LabelSubview.BackgroundColor = UIColor.Blue;
      
			highArea = new lowHighSensor (new CGRect (.507 * mainView.Bounds.Width, .45 * mainView.Bounds.Height, .465 * mainView.Bounds.Width, .202 * mainView.Bounds.Height), new CGRect(.507 * mainView.Bounds.Width,.652 * mainView.Bounds.Height,.465 * mainView.Bounds.Width,.25 * mainView.Bounds.Height), ViewController, analyzerSensors.viewList);
			highArea.location = "high";
			highArea.Connection.Image = UIImage.FromBundle("ic_bluetooth_disconnected");
      highArea.LabelSubview.BackgroundColor = UIColor.Red;
      
      hideView(lowArea);  
      hideView(highArea);
		}
		
		public void hideView(lowHighSensor lowHigh){
			lowHigh.LabelTop.Hidden = true;
			lowHigh.Connection.Hidden = true;
			lowHigh.DeviceImage.Hidden = true;
			lowHigh.LabelBottom.Hidden = true;
			lowHigh.LabelSubview.Hidden = true;
			lowHigh.subviewTable.Hidden = true;
      lowHigh.headingDivider.Hidden = true;
      lowHigh.connectionColor.Hidden = true;
      lowHigh.subviewHide.Hidden = true;
      lowHigh.tableSubviews = new List<string> ();
      lowHigh.tableSubviews.Clear();
      lowHigh.subviewTable.Source = null;
      lowHigh.subviewTable.ReloadData ();
      lowHigh.subviewTable.Hidden = true;
      lowHigh.max = 0;
      lowHigh.min = 0;      
      lowHigh.LabelMiddle.Font = UIFont.FromName("Helvetica", 18f);
  
      if (lowHigh.attachedSensor != null) {
        lowHigh.attachedSensor.topLabel.BackgroundColor = UIColor.Clear;
        lowHigh.attachedSensor.topLabel.TextColor = UIColor.Gray;     
        lowHigh.attachedSensor = null;
      }
      lowHigh.subviewHide.SetImage(null, UIControlState.Normal);
			lowHigh.currentSensor = null;
			lowHigh.manualSensor = null; 
			lowHigh.isManual = false;   
		}
		
		public void showView(lowHighSensor lowHigh){
			Console.WriteLine("Showing " + lowHigh.location + " area ui");
			lowHigh.LabelTop.Hidden = false;
			lowHigh.DeviceImage.Hidden = false;
			lowHigh.LabelMiddle.Hidden = false;
			lowHigh.LabelBottom.Hidden = false;
			lowHigh.LabelSubview.Hidden = false;
			lowHigh.subviewHide.Hidden = false;
			lowHigh.subviewTable.Hidden = false;
      lowHigh.headingDivider.Hidden = false;
      lowHigh.LabelMiddle.Font = UIFont.FromName("Helvetica-Bold", 42f);
      if(!lowHigh.isManual){
				lowHigh.Connection.Hidden = false;
      	lowHigh.connectionColor.Hidden = false;
			} else {
				lowHigh.Connection.Hidden = true;
      	lowHigh.connectionColor.Hidden = true;
			}
		}
	}
}

