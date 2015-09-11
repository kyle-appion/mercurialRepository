// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.DeviceManager
{
	[Register ("DeviceTableCell")]
	partial class DeviceTableCell
	{
		[Outlet]
		UIKit.UIActivityIndicatorView activityConnectStatus { get; set; }

		[Outlet]
		UIKit.UIButton buttonConnect { get; set; }

		[Outlet]
		UIKit.UIImageView imageDeviceIcon { get; set; }

		[Outlet]
		UIKit.UILabel labelDeviceName { get; set; }

		[Outlet]
		UIKit.UILabel labelDeviceType { get; set; }

		[Outlet]
		UIKit.UIView viewDivider { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imageDeviceIcon != null) {
				imageDeviceIcon.Dispose ();
				imageDeviceIcon = null;
			}

			if (buttonConnect != null) {
				buttonConnect.Dispose ();
				buttonConnect = null;
			}

			if (activityConnectStatus != null) {
				activityConnectStatus.Dispose ();
				activityConnectStatus = null;
			}

			if (viewDivider != null) {
				viewDivider.Dispose ();
				viewDivider = null;
			}

			if (labelDeviceType != null) {
				labelDeviceType.Dispose ();
				labelDeviceType = null;
			}

			if (labelDeviceName != null) {
				labelDeviceName.Dispose ();
				labelDeviceName = null;
			}
		}
	}
}
