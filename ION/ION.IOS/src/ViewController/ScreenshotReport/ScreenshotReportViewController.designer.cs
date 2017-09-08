// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.ScreenshotReport
{
	[Register ("ScreenshotReportViewController")]
	partial class ScreenshotReportViewController
	{
		[Outlet]
		UIKit.UIImageView imageScreenshot { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollview { get; set; }

		[Outlet]
		UIKit.UITableView table { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (scrollview != null) {
				scrollview.Dispose ();
				scrollview = null;
			}

			if (imageScreenshot != null) {
				imageScreenshot.Dispose ();
				imageScreenshot = null;
			}

			if (table != null) {
				table.Dispose ();
				table = null;
			}
		}
	}
}
