// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS
{
	[Register ("WorkbenchViewer")]
	partial class WorkbenchViewer
	{
		[Outlet]
		UIKit.UIImageView iconAlarm { get; set; }

		[Outlet]
		UIKit.UIImageView iconArrow { get; set; }

		[Outlet]
		UIKit.UIImageView iconBattery { get; set; }

		[Outlet]
		UIKit.UIImageView iconConnect { get; set; }

		[Outlet]
		UIKit.UIImageView iconSensor { get; set; }

		[Outlet]
		UIKit.UILabel labelAlarm { get; set; }

		[Outlet]
		UIKit.UILabel labelConnectionStatus { get; set; }

		[Outlet]
		UIKit.UILabel labelHeader { get; set; }

		[Outlet]
		UIKit.UILabel labelMeasurement { get; set; }

		[Outlet]
		UIKit.UILabel labelSerialNumber { get; set; }

		[Outlet]
		UIKit.UILabel labelUnit { get; set; }
		
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
