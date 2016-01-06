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
	[Register ("DeviceManagerViewController")]
	partial class DeviceManagerViewController
	{
		[Outlet]
		UIKit.UILabel labelEmpty { get; set; }

		[Outlet]
		UIKit.UITableView tableContent { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelEmpty != null) {
				labelEmpty.Dispose ();
				labelEmpty = null;
			}

			if (tableContent != null) {
				tableContent.Dispose ();
				tableContent = null;
			}
		}
	}
}
