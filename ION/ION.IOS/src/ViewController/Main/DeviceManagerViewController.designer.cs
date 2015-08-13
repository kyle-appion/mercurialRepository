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
	[Register ("DeviceManagerViewController")]
	partial class DeviceManagerViewController
	{
		[Outlet]
		UIKit.UIButton buttonScan { get; set; }

		[Outlet]
		UIKit.UIView deviceManagerViewController { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView spinnerScanActivity { get; set; }

		[Outlet]
		UIKit.UITableView tableDeviceList { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (buttonScan != null) {
				buttonScan.Dispose ();
				buttonScan = null;
			}

			if (deviceManagerViewController != null) {
				deviceManagerViewController.Dispose ();
				deviceManagerViewController = null;
			}

			if (spinnerScanActivity != null) {
				spinnerScanActivity.Dispose ();
				spinnerScanActivity = null;
			}

			if (tableDeviceList != null) {
				tableDeviceList.Dispose ();
				tableDeviceList = null;
			}
		}
	}
}
