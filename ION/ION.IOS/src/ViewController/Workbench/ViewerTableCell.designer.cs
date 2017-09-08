// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Workbench
{
	[Register ("ViewerTableCell")]
	partial class ViewerTableCell
	{
		[Outlet]
		UIKit.UIActivityIndicatorView activityConnectStatus { get; set; }

		[Outlet]
		UIKit.UIButton buttonConnection { get; set; }

		[Outlet]
		UIKit.UIImageView imageAlarmIcon { get; set; }

		[Outlet]
		UIKit.UIImageView imageBattery { get; set; }

		[Outlet]
		UIKit.UIImageView imageSensorIcon { get; set; }

		[Outlet]
		UIKit.UILabel labelAlarm { get; set; }

		[Outlet]
		UIKit.UILabel labelConnectionStatus { get; set; }

		[Outlet]
		UIKit.UILabel labelHeader { get; set; }

		[Outlet]
		UIKit.UILabel labelLinked { get; set; }

		[Outlet]
		UIKit.UILabel labelMeasurement { get; set; }

		[Outlet]
		UIKit.UILabel labelSerialNumber { get; set; }

		[Outlet]
		UIKit.UILabel labelUnit { get; set; }

		[Outlet]
		UIKit.UIView viewBackground { get; set; }

		[Outlet]
		UIKit.UIView viewDivider { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (activityConnectStatus != null) {
				activityConnectStatus.Dispose ();
				activityConnectStatus = null;
			}

			if (buttonConnection != null) {
				buttonConnection.Dispose ();
				buttonConnection = null;
			}

			if (imageAlarmIcon != null) {
				imageAlarmIcon.Dispose ();
				imageAlarmIcon = null;
			}

			if (imageBattery != null) {
				imageBattery.Dispose ();
				imageBattery = null;
			}

			if (imageSensorIcon != null) {
				imageSensorIcon.Dispose ();
				imageSensorIcon = null;
			}

			if (labelAlarm != null) {
				labelAlarm.Dispose ();
				labelAlarm = null;
			}

			if (labelConnectionStatus != null) {
				labelConnectionStatus.Dispose ();
				labelConnectionStatus = null;
			}

			if (labelHeader != null) {
				labelHeader.Dispose ();
				labelHeader = null;
			}

			if (labelLinked != null) {
				labelLinked.Dispose ();
				labelLinked = null;
			}

			if (labelMeasurement != null) {
				labelMeasurement.Dispose ();
				labelMeasurement = null;
			}

			if (labelSerialNumber != null) {
				labelSerialNumber.Dispose ();
				labelSerialNumber = null;
			}

			if (labelUnit != null) {
				labelUnit.Dispose ();
				labelUnit = null;
			}

			if (viewBackground != null) {
				viewBackground.Dispose ();
				viewBackground = null;
			}

			if (viewDivider != null) {
				viewDivider.Dispose ();
				viewDivider = null;
			}
		}
	}
}
