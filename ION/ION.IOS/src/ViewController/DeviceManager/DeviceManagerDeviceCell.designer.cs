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
	partial class DeviceManagerDeviceCell
	{
		[Outlet]
		UIKit.UIActivityIndicatorView activityConnectActivity { get; set; }

		[Outlet]
		UIKit.UIImageView iconDevice { get; set; }

		[Outlet]
		UIKit.UIImageView iconExpandIndicator { get; set; }

		[Outlet]
		UIKit.UILabel labelDeviceName { get; set; }

		[Outlet]
		UIKit.UILabel labelDeviceType { get; set; }

		[Outlet]
		UIKit.UILabel labelSerialNumber { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (iconDevice != null) {
				iconDevice.Dispose ();
				iconDevice = null;
			}

			if (labelDeviceType != null) {
				labelDeviceType.Dispose ();
				labelDeviceType = null;
			}

			if (labelDeviceName != null) {
				labelDeviceName.Dispose ();
				labelDeviceName = null;
			}

			if (iconExpandIndicator != null) {
				iconExpandIndicator.Dispose ();
				iconExpandIndicator = null;
			}

			if (activityConnectActivity != null) {
				activityConnectActivity.Dispose ();
				activityConnectActivity = null;
			}

			if (labelSerialNumber != null) {
				labelSerialNumber.Dispose ();
				labelSerialNumber = null;
			}
		}
	}
}
