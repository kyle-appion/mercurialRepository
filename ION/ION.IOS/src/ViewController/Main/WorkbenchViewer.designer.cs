// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Main
{
	[Register ("WorkbenchViewer")]
	partial class WorkbenchViewer
	{
		[Outlet]
		public UIKit.UIImageView iconAlarm { get; set; }

		[Outlet]
    public UIKit.UIImageView iconArrow { get; set; }

		[Outlet]
    public UIKit.UIImageView iconBattery { get; set; }

		[Outlet]
    public UIKit.UIImageView iconConnect { get; set; }

		[Outlet]
    public UIKit.UIImageView iconSensor { get; set; }

		[Outlet]
    public UIKit.UILabel labelAlarm { get; set; }

		[Outlet]
    public UIKit.UILabel labelConnectionStatus { get; set; }

		[Outlet]
    public UIKit.UILabel labelHeader { get; set; }

		[Outlet]
    public UIKit.UILabel labelMeasurement { get; set; }

		[Outlet]
    public UIKit.UILabel labelSerialNumber { get; set; }

		[Outlet]
    public UIKit.UILabel labelUnit { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelHeader != null) {
				labelHeader.Dispose ();
				labelHeader = null;
			}

			if (iconConnect != null) {
				iconConnect.Dispose ();
				iconConnect = null;
			}

			if (iconBattery != null) {
				iconBattery.Dispose ();
				iconBattery = null;
			}

			if (iconSensor != null) {
				iconSensor.Dispose ();
				iconSensor = null;
			}

			if (labelMeasurement != null) {
				labelMeasurement.Dispose ();
				labelMeasurement = null;
			}

			if (labelAlarm != null) {
				labelAlarm.Dispose ();
				labelAlarm = null;
			}

			if (iconAlarm != null) {
				iconAlarm.Dispose ();
				iconAlarm = null;
			}

			if (labelConnectionStatus != null) {
				labelConnectionStatus.Dispose ();
				labelConnectionStatus = null;
			}

			if (labelUnit != null) {
				labelUnit.Dispose ();
				labelUnit = null;
			}

			if (labelSerialNumber != null) {
				labelSerialNumber.Dispose ();
				labelSerialNumber = null;
			}

			if (iconArrow != null) {
				iconArrow.Dispose ();
				iconArrow = null;
			}
		}
	}
}
