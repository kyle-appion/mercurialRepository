// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ION.IOS.ViewController.Main
{
	[Register ("DeviceManagerViewController")]
	partial class DeviceManagerViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton buttonScan { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView deviceManagerViewController { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIActivityIndicatorView spinnerScanActivity { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView tableDeviceList { get; set; }

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
