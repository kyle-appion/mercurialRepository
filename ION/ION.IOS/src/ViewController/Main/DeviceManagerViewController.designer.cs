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
		public UIKit.UIView deviceManagerViewController { get; set; }

		[Outlet]
		public UIKit.UITableView tableDeviceList { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (deviceManagerViewController != null) {
				deviceManagerViewController.Dispose ();
				deviceManagerViewController = null;
			}

			if (tableDeviceList != null) {
				tableDeviceList.Dispose ();
				tableDeviceList = null;
			}
		}
	}
}
