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
		UIKit.UIImageView batteryImage { get; set; }

		[Outlet]
		UIKit.UIButton buttonConnection { get; set; }

		[Outlet]
		UIKit.UIImageView imageAlarmIcon { get; set; }

		[Outlet]
		UIKit.UIImageView imageSensorIcon { get; set; }

		[Outlet]
		UIKit.UILabel labelConnectionStatus { get; set; }

		[Outlet]
		UIKit.UILabel labelHeader { get; set; }

		[Outlet]
		UIKit.UILabel labelMeasurement { get; set; }

		[Outlet]
		UIKit.UILabel labelUnit { get; set; }

		[Outlet]
		UIKit.UIView viewBackground { get; set; }
		
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

			if (imageSensorIcon != null) {
				imageSensorIcon.Dispose ();
				imageSensorIcon = null;
			}

			if (labelConnectionStatus != null) {
				labelConnectionStatus.Dispose ();
				labelConnectionStatus = null;
			}

			if (labelHeader != null) {
				labelHeader.Dispose ();
				labelHeader = null;
			}

			if (labelMeasurement != null) {
				labelMeasurement.Dispose ();
				labelMeasurement = null;
			}

			if (labelUnit != null) {
				labelUnit.Dispose ();
				labelUnit = null;
			}

			if (viewBackground != null) {
				viewBackground.Dispose ();
				viewBackground = null;
			}

			if (batteryImage != null) {
				batteryImage.Dispose ();
				batteryImage = null;
			}
		}
	}
}
